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
            this.label6 = new System.Windows.Forms.Label();
            this.cbSendingPeriod = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbLostPackagesLimit = new System.Windows.Forms.ComboBox();
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
            this.close.Text = "Cancel";
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
            this.tbPassword.PasswordChar = '*';
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
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 20);
            this.label6.Text = "Sending Period";
            // 
            // cbSendingPeriod
            // 
            this.cbSendingPeriod.Items.Add("1");
            this.cbSendingPeriod.Items.Add("2");
            this.cbSendingPeriod.Items.Add("3");
            this.cbSendingPeriod.Items.Add("5");
            this.cbSendingPeriod.Items.Add("10");
            this.cbSendingPeriod.Items.Add("20");
            this.cbSendingPeriod.Items.Add("30");
            this.cbSendingPeriod.Items.Add("60");
            this.cbSendingPeriod.Items.Add("120");
            this.cbSendingPeriod.Location = new System.Drawing.Point(142, 140);
            this.cbSendingPeriod.Name = "cbSendingPeriod";
            this.cbSendingPeriod.Size = new System.Drawing.Size(59, 22);
            this.cbSendingPeriod.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 20);
            this.label7.Text = "Lost Packages Limit";
            // 
            // cbLostPackagesLimit
            // 
            this.cbLostPackagesLimit.Items.Add("0");
            this.cbLostPackagesLimit.Items.Add("1");
            this.cbLostPackagesLimit.Items.Add("5");
            this.cbLostPackagesLimit.Items.Add("10");
            this.cbLostPackagesLimit.Items.Add("20");
            this.cbLostPackagesLimit.Items.Add("30");
            this.cbLostPackagesLimit.Items.Add("50");
            this.cbLostPackagesLimit.Items.Add("100");
            this.cbLostPackagesLimit.Items.Add("500");
            this.cbLostPackagesLimit.Items.Add("1000");
            this.cbLostPackagesLimit.Location = new System.Drawing.Point(142, 109);
            this.cbLostPackagesLimit.Name = "cbLostPackagesLimit";
            this.cbLostPackagesLimit.Size = new System.Drawing.Size(59, 22);
            this.cbLostPackagesLimit.TabIndex = 8;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.cbLostPackagesLimit);
            this.Controls.Add(this.cbSendingPeriod);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.tbLogin);
            this.Controls.Add(this.tbHost);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbSendingPeriod;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbLostPackagesLimit;
    }
}