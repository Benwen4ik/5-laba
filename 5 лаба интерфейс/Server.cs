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
            listener = null;

            try
            {
                listener = new TcpListener(IPAddress.Any, Port);
                listener.Start();

                MessageBox.Show("Сервер запущен. Ожидание подключения клиента...");

                while (true)
                {
                    client = await listener.AcceptTcpClientAsync();
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
                MessageBox.Show("Завершение соединения.");
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
                                byte[] buffer = new byte[4096];
                                int bytesRead;

                                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    client.GetStream().WriteAsync(buffer, 0, bytesRead);
                                }
                            }
                            MessageBox.Show($"Файл '{requestedFile}' отправлен клиенту.");
                            return;
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
         //       MessageBox.Show("Завершение соединения.");
            }
        }

        public void closeConnect()
        {
            if (client != null)
                client.Close();
            if (listener != null)
                listener.Stop();
         //   MessageBox.Show("Завершение соединения.");
        }
    }
}
