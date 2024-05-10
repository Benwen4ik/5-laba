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
    class Client
    {
        private string ServerIpAddress ; // Укажите IP-адрес сервера
        private int ServerPort ; // Укажите порт сервера


        private List<string> fileList = new List<string>();
        private readonly string _savePath;

        TcpClient client = null;

        public Client( string ip, int port)
        {
         //   _savePath = savePath;
            ServerPort = port;
            ServerIpAddress = ip;
        }

        public async  Task<List<string>> GetFileListAsync()
        {
            try
            {
                TcpClient client = new TcpClient();
                await client.ConnectAsync(ServerIpAddress, ServerPort);
                MessageBox.Show("Успешное подключение к серверу.");

                using (StreamReader reader = new StreamReader(client.GetStream()))
                using (StreamWriter writer = new StreamWriter(client.GetStream()))
                {
                    // Отправляем запрос на получение списка файлов
                //    await writer.WriteLineAsync();
                  //  await writer.FlushAsync();

                    // Читаем список файлов от сервера
                    List<string> fileList = new List<string>();
                    string line;
                    while ((line = await reader.ReadLineAsync()) != string.Empty)
                    {
                        fileList.Add(line);
                    }

                    client.Close();
                    MessageBox.Show("Завершение соединения.");

                    return fileList;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка при работе клиента: " + e.Message);
                return null;
            }
        }

        public async Task DownloadFileAsync(string fileName)
        {
            try
            {
                TcpClient client = new TcpClient();
                await client.ConnectAsync(ServerIpAddress, ServerPort);
               MessageBox.Show("Успешное подключение к серверу.");

                using (StreamReader reader = new StreamReader(client.GetStream()))
                using (StreamWriter writer = new StreamWriter(client.GetStream()))
                {
                    // Отправляем запрос на сервер
                  //  await writer.WriteLineAsync("DOWNLOAD_FILE");
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

       //         client.Close();
                MessageBox.Show("Завершение соединения.");
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка при работе клиента: " + e.Message);
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

        public void downloadFile(string fileName, string savePath)
        {
           // TcpClient client = null;

            try
            {
                client = new TcpClient(ServerIpAddress, ServerPort);
                using (StreamWriter writer = new StreamWriter(client.GetStream()))
                {
                    writer.WriteLine(fileName);
                    writer.Flush();
                }

                string saveFilePath = Path.Combine(savePath, fileName);

                using (FileStream fileStream = new FileStream(saveFilePath, FileMode.Create))
                {
                    client.GetStream().CopyTo(fileStream);
                }

                MessageBox.Show($"Файл '{fileName}' успешно скачан и сохранен по пути: {saveFilePath}");
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка при загрузке файла: " + e.Message);
            }
            finally
            {
              //  if (client != null)
                   // client.Close();
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
        }

    }
}
