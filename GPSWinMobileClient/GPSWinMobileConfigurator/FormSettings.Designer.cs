namespace GPSWinMobileConfigurator
{
    partial class FormSettings
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс  следует удалить; иначе ложно.</param>
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.save = new System.Windows.Forms.MenuItem();
            this.close = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.tbHost = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.save);
            this.mainMenu1.MenuItems.Add(this.close);
            // 
            // save
            // 
            this.save.Text = "Save";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // close
            // 
            this.close.Text = "Close";
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(28, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.Text = "Server IP";
            // 
            // tbHost
            // 
            this.tbHost.Location = new System.Drawing.Point(3, 23);
            this.tbHost.MaxLength = 15;
            this.tbHost.Name = "tbHost";
            this.tbHost.Size = new System.Drawing.Size(125, 21);
            this.tbHost.TabIndex = 1;
            this.tbHost.Text = "255.255.255.255";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(142, 23);
            this.tbPort.MaxLength = 5;
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(93, 21);
            this.tbPort.TabIndex = 1;
            this.tbPort.Text = "4505";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(35, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 20);
            this.label2.Text = "Login";
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(3, 70);
            this.tbLogin.MaxLength = 15;
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(106, 21);
            this.tbLogin.TabIndex = 1;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(115, 70);
            this.tbPassword.MaxLength = 32;
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(122, 21);
            this.tbPassword.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(132, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 20);
            this.label3.Text = ":";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(167, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 20);
            this.label4.Text = "Port";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(150, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 20);
            this.label5.Text = "Password";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.tbLogin);
            this.Controls.Add(this.tbHost);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "FormSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem save;
        private System.Windows.Forms.MenuItem close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbHost;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}