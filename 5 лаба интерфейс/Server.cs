using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5_лаба_интерфейс
{
    class Server
    {
        private int Port ; // Укажите порт, на котором будет работать сервер

        private readonly List<string> FilePaths;

       // private bool running = true;

        TcpListener listener = null;
        TcpClient client = null;

        public Server(List<string> filePath, int port)
        {
            FilePaths = filePath;
            this.Port = port;
        }

        public async Task StartAsync()
        {
            TcpListener listener = null;

            try
            {
                listener = new TcpListener(IPAddress.Any, Port);
                listener.Start();

                Console.WriteLine("Сервер запущен. Ожидание подключения клиента...");

                while (true)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    Console.WriteLine("Клиент подключен.");

                    await HandleClientAsync(client);

                    Console.WriteLine("Завершение соединения.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при работе сервера: " + e.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                using (StreamReader reader = new StreamReader(client.GetStream()))
                using (StreamWriter writer = new StreamWriter(client.GetStream()))
                {
                    // Отправляем список файлов клиенту
                    foreach (string filePath1 in FilePaths)
                    {
                        writer.WriteLine(Path.GetFileName(filePath1));
                    }
                    writer.WriteLine(); // Пустая строка для обозначения окончания списка файлов
                    await writer.FlushAsync();

                    while (true)
                    {
                        // Читаем запрос клиента
                        string requestedFile = await reader.ReadLineAsync();
                        if (string.IsNullOrEmpty(requestedFile))
                            break;

                        string filePath = FilePaths.Find(path => Path.GetFileName(path) == requestedFile);
                        if (!string.IsNullOrEmpty(filePath))
                        {
                            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                            {
                                await fileStream.CopyToAsync(client.GetStream());
                            }
                            Console.WriteLine($"Файл '{requestedFile}' отправлен клиенту.");
                        }
                        else
                        {
                            Console.WriteLine($"Файл '{requestedFile}' не найден на сервере.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при обработке клиента: " + e.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
