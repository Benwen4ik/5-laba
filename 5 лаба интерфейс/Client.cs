using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5_лаба_интерфейс
{
    class Client
    {
        private const string ServerIpAddress = "127.0.0.1"; // Укажите IP-адрес сервера
        private const int ServerPort = 8080; // Укажите порт сервера

        private readonly string _savePath;

        TcpClient client = null;

        public Client(string savePath)
        {
            _savePath = savePath;
        }

        public void Start()
        {
            TcpClient client = null;

            MessageBox.Show("Ожидание подключения");
            try
            {
                client = new TcpClient(ServerIpAddress, ServerPort);
                MessageBox.Show("Подключение к серверу успешно.");
                using (var fileStream = new FileStream(_savePath, FileMode.Create))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = client.GetStream().Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                    }
                }

                Console.WriteLine("Файл получен от сервера");
                MessageBox.Show("Файл получен от сервера");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении файла: " + ex.Message);
            }
            finally
            {
                client?.Close();
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

    }
}
