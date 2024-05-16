using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5_лаба_интерфейс
{
    class Server
    {
        private int Port; // Укажите порт, на котором будет работать сервер

        private readonly List<string> FilePaths;

        private bool running = true;

        TcpListener listener = null;
        TcpClient client = null;

        public Server(List<string> filePath, int port)
        {
            FilePaths = filePath;
            this.Port = port;
        }

        public void StartAsync()
        {
            listener = null;

            try
            {
                listener = new TcpListener(IPAddress.Any, Port);
                listener.Start();

                MessageBox.Show("Сервер запущен. Ожидание подключения клиента...");

                while (running)
                {
                    client = listener.AcceptTcpClient();
                    MessageBox.Show("Клиент подключен.");

                    HandleClientAsync(client);
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

        private void HandleClientAsync(TcpClient client)
        {
            try
            {
                using (StreamReader reader = new StreamReader(client.GetStream()))
                using (StreamWriter writer = new StreamWriter(client.GetStream()))
                {
                    // Отправляем список файлов клиенту
                    foreach (string filePath in FilePaths)
                    {
                        writer.WriteLine(Path.GetFileName(filePath));
                    }
                    writer.WriteLine(); // Пустая строка для обозначения окончания списка файлов
                    writer.Flush();
                    while (running)
                    {
                        if (!client.Connected)
                        {
                            break;
                        }
                        //if (!client.Connected)
                        //{
                        //    listener.Stop();
                        //    listener = null;
                        //    listener.Start();
                        //    client = null;
                        //    while (!client.Connected)
                        //    {
                        //        client = listener.AcceptTcpClient();
                        //        //MessageBox.Show("Клиент отключен");
                        //        // break;
                        //    }
                        //}
                        if (reader.ReadLine() == "DOWNLOAD_FILE")
                        {
                            string requestedFile = reader.ReadLine();
                            if (!string.IsNullOrEmpty(requestedFile))
                            {
                                string filePath = FilePaths.Find(path => Path.GetFileName(path) == requestedFile);
                                if (!string.IsNullOrEmpty(filePath))
                                {
                                    using (SHA256 sha256 = SHA256.Create())
                                    {
                                        byte[] str = Encoding.UTF8.GetBytes(File.ReadAllText(filePath));
                                        byte[] hash = sha256.ComputeHash(str);
                                        client.GetStream().Write(hash, 0, hash.Length);
                                        using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                                        {
                                            byte[] buffer = new byte[4096];
                                            int bytesRead;
                                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                                            {
                                                client.GetStream().Write(buffer, 0, bytesRead);
                                            }
                                        }
                                    }


                                    writer.Flush();
                                    MessageBox.Show($"Файл '{requestedFile}' отправлен клиенту.");

                                    // Thread.Sleep(5000);
                                    client.Close();
                                    //return;
                                    // running = false;
                                }
                                else
                                {
                                    MessageBox.Show($"Файл '{requestedFile}' не найден на сервере. SERVER");
                                }
                            }
                        }
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
              //  MessageBox.Show("Клиент отключен");
                //       MessageBox.Show("Завершение соединения.");
            }
        }

        public void closeConnect()
        {
            running = false;
            //   MessageBox.Show("Завершение соединения.");
        }
    }
}
