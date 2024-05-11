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

                MessageBox.Show("Сервер запущен. Ожидание подключения клиента...");

                while (true)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    MessageBox.Show("Клиент подключен.");

                    _ = HandleClientAsync(client);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка при работе сервера: " + e.Message);
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
                    foreach (string filePath in FilePaths)
                    {
                        await writer.WriteLineAsync(Path.GetFileName(filePath));
                    }
                    await writer.WriteLineAsync(); // Пустая строка для обозначения окончания списка файлов
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
                            MessageBox.Show($"Файл '{requestedFile}' отправлен клиенту.");
                        }
                        else
                        {
                            MessageBox.Show($"Файл '{requestedFile}' не найден на сервере.");
                        }

                        // Очищаем буфер и отправляем сигнал клиенту, что передача файла завершена
                        await writer.FlushAsync();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка при обработке клиента: " + e.Message);
            }
            finally
            {
                client.Close();
                MessageBox.Show("Завершение соединения.");
            }
        }
    }
}
