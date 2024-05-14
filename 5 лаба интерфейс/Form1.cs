using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        Client client;
        Server server;

        List<string> ListFile = new List<string> { };
        Dictionary<int, string> DownloadListFile = new Dictionary<int, string> { };

        public Form1()
        {
            InitializeComponent();
            listFileBox.DropDownStyle = ComboBoxStyle.DropDownList;
            portbox.Text = "8081";
            ipadresbox.Text = "127.0.0.1";
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
            portbox.Visible = true;
            ipadresbox.Visible = false;
            label1.Visible = true;
            label2.Visible = false;
            connectButton.Visible = false;
            addfile.Visible = true;
        }

        private void ClientBox_CheckedChanged(object sender, EventArgs e)
        {
         //   ClientBox.Checked = true;
            ServerBox.Checked = false;
            getbutton.Visible = true;
            postbutton.Visible = false;
            openfile.Visible = true;
            portbox.Visible = true;
            ipadresbox.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            connectButton.Visible = true;
            addfile.Visible = false;
        }

        private void postbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (listFileBox.Items.Count == 0)
                {
                    MessageBox.Show("Не выбран ни один файл");
                    return;
                }
                //   if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                //     return;

                //   string filePath = openFileDialog1.FileName;  // Укажите путь к вашему HTML-файлу
                // Thread threadserver = new Thread(createServer);
                // threadserver.Start(ListFile);
                server = new Server((List<string>)ListFile, int.Parse(portbox.Text));
                Task.Run( () => server.StartAsync() );
            //    _ = server.DownloadAsync();
                //  threadserver.Abort();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error :" + exp.Message);
            }
        }

        private void createServer(object filePath)
        {
            try
            {
                server = new Server((List<string>)filePath, int.Parse(portbox.Text));
                server.StartAsync();
            } catch(Exception exp)
            {
                MessageBox.Show("Error :" + exp.Message);
            }
        }

        private void getbutton_Click(object sender, EventArgs e)
        {
            try
            {
                //saveFileDialog1.ShowDialog();

                       // string savePath = @"C:\test.html";
                if (client != null)
                {
                    if (listFileBox.SelectedIndex == -1)
                    {
                        MessageBox.Show("Файл не выбран");
                        return;
                    }
                    if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                        return;
                    savePath = saveFileDialog1.FileName;
                    DownloadListFile.Add(listFileBox.SelectedIndex,savePath);
                    //  Thread threadclient = new Thread(createClient);
                    //  threadclient.Start(savePath);
                    //createClient(savePath);
                    //server.
                    string file = listFileBox.SelectedItem.ToString();
                    //  client.downloadFile(file,savePath);
                    Task.Run(() => client.DownloadFileAsync(file, savePath) );
                    //client.StartDownloadAsync();
                } else
                {
                    MessageBox.Show("Ошибка при введении имени файла");
                }
                //  threadclient.Abort();
            }
            catch (Exception ex)
            {
                // Используем Invoke для вызова MessageBox.Show в главном потоке
                Invoke(new Action(() => MessageBox.Show("Ошибка при записи файла: " + ex.Message)));
            }
        }

        private void createClientAsync(object savePath)
        {
            try
            {
                client = new Client( ipadresbox.Text, int.Parse(portbox.Text));
               // client.Start();
           //    client.RequestFileAsync(
                ListFile = Task.Run( ()  => client.GetFileListAsync()).Result;
                listFileBox.Items.Clear();
                listFileBox.Items.AddRange(ListFile.ToArray());
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error :" + exp.Message);
            }
        }

        private void openfile_Click(object sender, EventArgs e)
        {
            try
            {
                if (listFileBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Файл не выбран");
                    return;
                }
              /*  if (DownloadListFile.ContainsKey(listFileBox.SelectedIndex))
                {
                    MessageBox.Show("Файл не скачан");
                    return;
                } */
                string filename = $"\"{DownloadListFile[listFileBox.SelectedIndex]}\"";
                Process.Start("msedge.exe",filename);
            }
            catch(NullReferenceException nex)
            {
                MessageBox.Show("Ошибка при чтении файла: " + nex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при чтении файла: " + ex.Message);
            }
        }

        private void addfile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            ListFile.Add(openFileDialog1.FileName);
            listFileBox.Items.Add(Path.GetFileNameWithoutExtension(openFileDialog1.FileName));
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                //saveFileDialog1.ShowDialog();
                // if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                //    return;
                //   savePath = saveFileDialog1.FileName;
                // string savePath = @"C:\test.html";

                //  Thread threadclient = new Thread(createClient);
                //  threadclient.Start(savePath);
                createClientAsync(savePath);
                
               
                  //  MessageBox.Show("Ошибка при введении имени файла");
                
                //  threadclient.Abort();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при записи файла: " + ex.Message);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ServerBox.Checked == true)
                {
                    server.closeConnect();
                    server = null;
                }
                else
                {
                    if (client == null) return;
                    client.CloseConnect();
                    client = null;
                }
                deleteAllFiles();
                MessageBox.Show("Соединение завершено");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при закрытии соединения: " + ex.Message);
            }
        }

        private  void deleteAllFiles()
        {
            try
            {
                foreach (string str in DownloadListFile.Values)
                {
                    File.Delete(str);
                }
                DownloadListFile.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении файлов: " + ex.Message);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            deleteAllFiles();
        }

        private void downloadButton_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (listFileBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Файл не выбран");
                    return;
                }
                if (!DownloadListFile.ContainsKey(listFileBox.SelectedIndex))
                {
                    savePath = Directory.GetCurrentDirectory() + "\\file" + listFileBox.SelectedIndex + ".html";
                    string file = listFileBox.SelectedItem.ToString();
                    client.DownloadFileAsync(file, savePath);
                    DownloadListFile.Add(listFileBox.SelectedIndex, savePath);
                }
                string filename = $"\"{DownloadListFile[listFileBox.SelectedIndex]}\"";
                Process.Start("msedge.exe", filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении файлов: " + ex.Message);
            }
        }
    }
}
