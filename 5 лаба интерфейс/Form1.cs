using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5_лаба_интерфейс
{
    public partial class Form1 : Form
    {

        string savePath;

        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.Filter = "HTML файл (*.html)|*.html|Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileDialog1.Filter = "HTML файл (*.html)|*.html|Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
        }

        private void Server_CheckedChanged(object sender, EventArgs e)
        {
          //  ServerBox.Checked = true;
            ClientBox.Checked = false;
            postbutton.Visible = true;
            getbutton.Visible = false;
            openfile.Visible = false;
        }

        private void ClientBox_CheckedChanged(object sender, EventArgs e)
        {
         //   ClientBox.Checked = true;
            ServerBox.Checked = false;
            getbutton.Visible = true;
            postbutton.Visible = false;
            openfile.Visible = true;
        }

        private void postbutton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filePath = openFileDialog1.FileName;  // Укажите путь к вашему HTML-файлу
            Thread threadserver = new Thread(createServer);
            threadserver.Start(filePath);
          //  threadserver.Abort();
        }

        private void createServer(object filePath)
        {

            var server = new Server((string)filePath);
            server.Start();
        }

        private void getbutton_Click(object sender, EventArgs e)
        {
            try
            {
                //saveFileDialog1.ShowDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                savePath = saveFileDialog1.FileName;
                // string savePath = @"C:\test.html";
                if (savePath.Length != 0)
                {
                    Thread threadclient = new Thread(createClient);
                    threadclient.Start(savePath);
                } else
                {
                    MessageBox.Show("Ошибка при введении имени файла");
                }
                //  threadclient.Abort();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при записи файла: " + ex.Message);
            }
        }

        private void createClient(object savePath)
        {
            var client = new Client((string)savePath);
            client.Start();
        }

        private void openfile_Click(object sender, EventArgs e)
        {
            try
            {
                if (savePath.Length != 0)
                {
                    string filename = $"\"{savePath}\"";
                    Process.Start("msedge.exe", filename);
                } else
                {
                    MessageBox.Show("Файл не был получен");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при чтении файла: " + ex.Message);
            }
        }
    }
}
