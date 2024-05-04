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

        private readonly string _filePath;

        public Server(string filePath, int port)
        {
            _filePath = filePath;
            this.Port = port;
        }

        public void Start()
        {
            TcpListener listener = null;
            TcpClient client = null;

            try
            {
                listener = new TcpListener(IPAddress.Any, Port);
                listener.Start();
              //  DialogResult res = MessageBox.Show("Сервер запущен. Ожидание подключения клиента...", "Подтверждение удаления", 
              //      MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                while (!listener.Pending())
                {
                    //  MessageBox.Show("Сервер запущен. Ожидание подключения клиента...");
                    DialogResult res = MessageBox.Show("Сервер запущен. Ожидание подключения клиента...", "Ожидание подключения",
                      MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                    if (res == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                
                client = listener.AcceptTcpClient();
                MessageBox.Show("Клиент подключен.");

                using (var fileStream = new FileStream(_filePath, FileMode.Open))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        client.GetStream().Write(buffer, 0, bytesRead);
                    }
                }

                Console.WriteLine("Файл отправлен клиенту");
                MessageBox.Show("Файл успешно отправлен клиенту");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при передаче файла: " + ex.Message);
            }
            finally
            {
                client?.Close();
                listener?.Stop();
            }
        }
    }
}
