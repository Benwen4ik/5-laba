
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
            this.SuspendLayout();
            // 
            // ServerBox
            // 
            this.ServerBox.AutoSize = true;
            this.ServerBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ServerBox.Location = new System.Drawing.Point(12, 12);
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
            this.ClientBox.Location = new System.Drawing.Point(132, 13);
            this.ClientBox.Name = "ClientBox";
            this.ClientBox.Size = new System.Drawing.Size(74, 24);
            this.ClientBox.TabIndex = 1;
            this.ClientBox.Text = "Client";
            this.ClientBox.UseVisualStyleBackColor = true;
            this.ClientBox.CheckedChanged += new System.EventHandler(this.ClientBox_CheckedChanged);
            // 
            // postbutton
            // 
            this.postbutton.Location = new System.Drawing.Point(13, 43);
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
            this.getbutton.Location = new System.Drawing.Point(132, 42);
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
            this.openfile.Location = new System.Drawing.Point(132, 72);
            this.openfile.Name = "openfile";
            this.openfile.Size = new System.Drawing.Size(75, 23);
            this.openfile.TabIndex = 4;
            this.openfile.Text = "Open file";
            this.openfile.UseVisualStyleBackColor = true;
            this.openfile.Visible = false;
            this.openfile.Click += new System.EventHandler(this.openfile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 205);
            this.Controls.Add(this.openfile);
            this.Controls.Add(this.getbutton);
            this.Controls.Add(this.postbutton);
            this.Controls.Add(this.ClientBox);
            this.Controls.Add(this.ServerBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "Form1";
            this.Text = "Form1";
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
    }
}

