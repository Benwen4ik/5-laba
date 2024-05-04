
namespace _5_лаба_интерфейс
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ServerBox = new System.Windows.Forms.CheckBox();
            this.ClientBox = new System.Windows.Forms.CheckBox();
            this.postbutton = new System.Windows.Forms.Button();
            this.getbutton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openfile = new System.Windows.Forms.Button();
            this.portbox = new System.Windows.Forms.TextBox();
            this.ipadresbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ServerBox
            // 
            this.ServerBox.AutoSize = true;
            this.ServerBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ServerBox.Location = new System.Drawing.Point(165, 12);
            this.ServerBox.Name = "ServerBox";
            this.ServerBox.Size = new System.Drawing.Size(80, 24);
            this.ServerBox.TabIndex = 0;
            this.ServerBox.Text = "Server";
            this.ServerBox.UseVisualStyleBackColor = true;
            this.ServerBox.CheckedChanged += new System.EventHandler(this.Server_CheckedChanged);
            // 
            // ClientBox
            // 
            this.ClientBox.AutoSize = true;
            this.ClientBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClientBox.Location = new System.Drawing.Point(277, 12);
            this.ClientBox.Name = "ClientBox";
            this.ClientBox.Size = new System.Drawing.Size(74, 24);
            this.ClientBox.TabIndex = 1;
            this.ClientBox.Text = "Client";
            this.ClientBox.UseVisualStyleBackColor = true;
            this.ClientBox.CheckedChanged += new System.EventHandler(this.ClientBox_CheckedChanged);
            // 
            // postbutton
            // 
            this.postbutton.Location = new System.Drawing.Point(218, 112);
            this.postbutton.Name = "postbutton";
            this.postbutton.Size = new System.Drawing.Size(75, 23);
            this.postbutton.TabIndex = 2;
            this.postbutton.Text = "Post file";
            this.postbutton.UseVisualStyleBackColor = true;
            this.postbutton.Visible = false;
            this.postbutton.Click += new System.EventHandler(this.postbutton_Click);
            // 
            // getbutton
            // 
            this.getbutton.Location = new System.Drawing.Point(218, 141);
            this.getbutton.Name = "getbutton";
            this.getbutton.Size = new System.Drawing.Size(75, 23);
            this.getbutton.TabIndex = 3;
            this.getbutton.Text = "Get file";
            this.getbutton.UseVisualStyleBackColor = true;
            this.getbutton.Visible = false;
            this.getbutton.Click += new System.EventHandler(this.getbutton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openfile
            // 
            this.openfile.Location = new System.Drawing.Point(218, 170);
            this.openfile.Name = "openfile";
            this.openfile.Size = new System.Drawing.Size(75, 23);
            this.openfile.TabIndex = 4;
            this.openfile.Text = "Open file";
            this.openfile.UseVisualStyleBackColor = true;
            this.openfile.Visible = false;
            this.openfile.Click += new System.EventHandler(this.openfile_Click);
            // 
            // portbox
            // 
            this.portbox.Location = new System.Drawing.Point(251, 56);
            this.portbox.Name = "portbox";
            this.portbox.Size = new System.Drawing.Size(100, 22);
            this.portbox.TabIndex = 5;
            this.portbox.Visible = false;
            // 
            // ipadresbox
            // 
            this.ipadresbox.Location = new System.Drawing.Point(251, 84);
            this.ipadresbox.Name = "ipadresbox";
            this.ipadresbox.Size = new System.Drawing.Size(100, 22);
            this.ipadresbox.TabIndex = 6;
            this.ipadresbox.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(178, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Port";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "IP adress";
            this.label2.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 205);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ipadresbox);
            this.Controls.Add(this.portbox);
            this.Controls.Add(this.openfile);
            this.Controls.Add(this.getbutton);
            this.Controls.Add(this.postbutton);
            this.Controls.Add(this.ClientBox);
            this.Controls.Add(this.ServerBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "Form1";
            this.Text = "зонби дабалатория v4.0;";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ServerBox;
        private System.Windows.Forms.CheckBox ClientBox;
        private System.Windows.Forms.Button postbutton;
        private System.Windows.Forms.Button getbutton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button openfile;
        private System.Windows.Forms.TextBox portbox;
        private System.Windows.Forms.TextBox ipadresbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

