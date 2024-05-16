using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    class Client
    {
        private string ServerIpAddress; // Укажите IP-адрес сервера
        private int ServerPort; // Укажите порт сервера


        private List<string> fileList = new List<string>();
        private readonly string _savePath;

        TcpClient client;

        public Client(string ip, int port)
        {
            //   _savePath = savePath;
            ServerPort = port;
            ServerIpAddress = ip;
            client = new TcpClient();
            client.Connect(ServerIpAddress, ServerPort);
            MessageBox.Show("Успешное подключение к серверу.");
        }

        public List<string> GetFileListAsync()
        {
            try
            {

                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());

                // Отправляем запрос на получение списка файлов
                //    await writer.WriteLineAsync();
                //  await writer.FlushAsync();

                // Читаем список файлов от сервера
                List<string> fileList = new List<string>();
                string line;
                while ((line = reader.ReadLine()) != string.Empty)
                {
                    fileList.Add(line);
                }

                //   client.Close();
                //  MessageBox.Show("Завершение 

                writer.Flush();

                return fileList;
        }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка при работе клиента: " + e.Message);
                return null;
            }
}

        public void DownloadFileAsync(string fileName, string directory)
        {
            try
            {
                // TcpClient client = new TcpClient();
                // client.Connect(ServerIpAddress, ServerPort);
                //if (!client.Connected)
                //{
                //    client = new TcpClient();
                //    while (!client.Connected)
                //    {
                //        Thread.Sleep(2000);
                //        client.Connect(ServerIpAddress, ServerPort);
                //        //    MessageBox.Show("Успешное подключение к серверу.");
                //    }
                //}

                using (StreamReader reader = new StreamReader(client.GetStream()))
                using (StreamWriter writer = new StreamWriter(client.GetStream()))
                {
                    // Отправляем запрос на сервер
                    writer.WriteLine("DOWNLOAD_FILE");
                    writer.WriteLine(fileName);
                    writer.Flush();
                    Thread.Sleep(3000);
                    // Читаем ответ от сервера
                    //  string response ;
                    if (client.Available != 0)
                    {
                        NetworkStream stream = client.GetStream();
                        // Получаем имя файла из ответа сервера
                        //    string receivedFileName = "dddsad";

                        // Создаем файл на клиенте и записываем в него данные от сервера
                        using (SHA256 sha256 = SHA256.Create())
                        {
                            byte[] bufferhash = new byte[32];
                            int bytehash = stream.Read(bufferhash, 0, 32);
                            Console.WriteLine(bytehash);

                            using (FileStream fileStream = new FileStream(directory, FileMode.Create))
                            {
                                byte[] buffer = new byte[4096];
                                int bytesRead;

                                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    Console.WriteLine(bytesRead);
                                    fileStream.Write(buffer, 0, bytesRead);
                                }
                            }
                            byte[] str = Encoding.UTF8.GetBytes(File.ReadAllText(directory));
                            byte[] hash = sha256.ComputeHash(str);
                            if (!bufferhash.SequenceEqual(hash))
                            {
                                //  MessageBox.Show(File.ReadAllText(directory));
                                MessageBox.Show($"Файл '{directory}' поврежден. '{BitConverter.ToString(bufferhash)}' HASH '{BitConverter.ToString(hash)}'  ");
                                File.Delete(directory);
                                writer.Flush();
                                return;
                            }
                        }

                        MessageBox.Show($"Файл '{directory}' успешно скачан и сохранен.");
                        //  writer.WriteLine("FILE_SEND");
                        Process processB = new System.Diagnostics.Process();
                        processB.StartInfo.FileName = @"C:\Chrone\chrome.exe";
                        processB.StartInfo.Arguments = $"\"{directory}\"";
                        processB.EnableRaisingEvents = true;
                        processB.Exited += (s, b) =>
                        {
                            MessageBox.Show("Удаление " + directory);
                            File.Delete(directory);
                        };
                        processB.Start();
                        writer.Flush();
                        client.Close();
                        return;
                    }
                    else
                    {
                        MessageBox.Show($"Файл '{fileName}' не найден на сервере.");
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка при работе клиента(скачивании): " + e.Message);
            }
        }



        public void CloseConnect()
        {
            client?.Close();
            MessageBox.Show("Завершение соединения.");
        }

    }
}
