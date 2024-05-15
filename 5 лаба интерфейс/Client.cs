using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5_лаба_интерфейс
{
    class Client
    {
        private string ServerIpAddress ; // Укажите IP-адрес сервера
        private int ServerPort ; // Укажите порт сервера


        private List<string> fileList = new List<string>();
        private readonly string _savePath;

        TcpClient client;

        public Client( string ip, int port)
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
                  //  TcpClient client = new TcpClient();
                //    client.Connect(ServerIpAddress, ServerPort);
                if (!client.Connected)
                {
                    client.Connect(ServerIpAddress, ServerPort);
                    MessageBox.Show("Успешное подключение к серверу.");
                }

                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());
                    // Отправляем запрос на сервер
                   writer.WriteLine("DOWNLOAD_FILE");
                    writer.WriteLine(fileName);
                    writer.Flush();
                 Thread.Sleep(1000);
                    // Читаем ответ от сервера
                  //  string response ;
                if (client.Available != 0)
                {
                    // Получаем имя файла из ответа сервера
                //    string receivedFileName = "dddsad";

                    // Создаем файл на клиенте и записываем в него данные от сервера
                    using (FileStream fileStream = new FileStream(directory, FileMode.Create))
                    {
                        byte[] buffer = new byte[4096];
                        int bytesRead;

                        while ((bytesRead = client.GetStream().Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fileStream.Write(buffer, 0, bytesRead);
                        }
                    }

                        MessageBox.Show($"Файл '{fileName}' успешно скачан и сохранен.");
                        writer.WriteLine("FILE_SEND");
                        writer.Flush();
                        return;
                }
                else
                {
                    MessageBox.Show($"Файл '{fileName}' не найден на сервере.");
                }


       //         client.Close();
         //       MessageBox.Show("Завершение соединения.");
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка при работе клиента(скачивание): " + e.Message);
            }
            finally
            {
                 client?.Close();
                MessageBox.Show("Завершение соединения.");
            }
        }

        public void getFile()
        {
            try
            {
                 using (var fileStream = new FileStream(_savePath, FileMode.Create))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = client.GetStream().Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении файла: " + ex.Message);
            }
            finally
            {
                // client?.Close();
            }
        }

        public async Task StartDownloadAsync(string fileName)
        {
            try
            {
                TcpClient client = new TcpClient();
                await client.ConnectAsync(ServerIpAddress, ServerPort);
                MessageBox.Show("Успешное подключение к серверу.");

                using (StreamReader reader = new StreamReader(client.GetStream()))
                using (StreamWriter writer = new StreamWriter(client.GetStream()))
                {
                    // Отправляем пустую строку в качестве запроса на получение списка файлов
                    await writer.WriteLineAsync();
                    await writer.FlushAsync();

                    // Читаем список файлов от сервера
                    List<string> fileList = new List<string>();
                    string line;
                    while ((line = await reader.ReadLineAsync()) != string.Empty)
                    {
                        fileList.Add(line);
                    }

                    // Проверяем, есть ли запрошенный файл в списке файлов
                    if (fileList.Contains(fileName))
                    {
                        // Отправляем запрос на сервер с именем файла
                        await writer.WriteLineAsync(fileName);
                        await writer.FlushAsync();

                        // Читаем ответ от сервера
                        string response = await reader.ReadLineAsync();
                        if (!string.IsNullOrEmpty(response))
                        {
                            // Получаем имя файла из ответа сервера
                            string receivedFileName = Path.GetFileName(response);

                            // Создаем диалоговое окно для сохранения файла
                            SaveFileDialog saveFileDialog = new SaveFileDialog();
                            saveFileDialog.FileName = receivedFileName;
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                // Создаем файл на клиенте и записываем в него данные от сервера
                                using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                                {
                                    await client.GetStream().CopyToAsync(fileStream);
                                }

                                MessageBox.Show($"Файл '{receivedFileName}' успешно скачан и сохранен.");
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Файл '{fileName}' не найден на сервере.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Список файлов не содержит запрошенного файла.");
                    }
                }

                client.Close();
                MessageBox.Show("Завершение соединения.");
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка при работе клиента: " + e.Message);
            }
        }

        public bool Connect()
        {

            MessageBox.Show("Ожидание подключения");
            try
            {
                client = new TcpClient(ServerIpAddress, ServerPort);
                MessageBox.Show("Подключение к серверу успешно.");
                if (client.Connected == true) return true;
                else return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении файла: " + ex.Message);
                return false;
            }

        }

        public List<string> getListFile()
        {
            return fileList;
        }

        public void CloseConnect()
        {
            client?.Close();
            MessageBox.Show("Завершение соединения.");
        }

    }
}
