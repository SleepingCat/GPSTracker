namespace ServerConfigurator
{
    partial class Settings
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bSaveAndStart = new System.Windows.Forms.Button();
            this.bDefault = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tbMaxConnections = new System.Windows.Forms.TextBox();
            this.tbKeepAliveTime = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbAuthTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDBpassword = new System.Windows.Forms.TextBox();
            this.tbDBuser = new System.Windows.Forms.TextBox();
            this.tbDBname = new System.Windows.Forms.TextBox();
            this.tbDBhost = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bPath = new System.Windows.Forms.Button();
            this.tbLogPath = new System.Windows.Forms.TextBox();
            this.cbLog = new System.Windows.Forms.CheckBox();
            this.cbLogConnectMessages = new System.Windows.Forms.CheckBox();
            this.cbLogErrors = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbConsole = new System.Windows.Forms.CheckBox();
            this.cbConClientGPSData = new System.Windows.Forms.CheckBox();
            this.cbConErrors = new System.Windows.Forms.CheckBox();
            this.cbConConnectMessages = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.базаДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьПустуюБазуДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьПользователяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bSaveAndStart);
            this.groupBox1.Controls.Add(this.bDefault);
            this.groupBox1.Controls.Add(this.bSave);
            this.groupBox1.Controls.Add(this.bCancel);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(652, 278);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки";
            // 
            // bSaveAndStart
            // 
            this.bSaveAndStart.Location = new System.Drawing.Point(345, 249);
            this.bSaveAndStart.Name = "bSaveAndStart";
            this.bSaveAndStart.Size = new System.Drawing.Size(135, 23);
            this.bSaveAndStart.TabIndex = 5;
            this.bSaveAndStart.Text = "Сохранить и запустить";
            this.bSaveAndStart.UseVisualStyleBackColor = true;
            this.bSaveAndStart.Click += new System.EventHandler(this.bSaveAndStart_Click);
            // 
            // bDefault
            // 
            this.bDefault.Location = new System.Drawing.Point(12, 249);
            this.bDefault.Name = "bDefault";
            this.bDefault.Size = new System.Drawing.Size(208, 23);
            this.bDefault.TabIndex = 4;
            this.bDefault.Text = "Загрузить стандартные настройки";
            this.bDefault.UseVisualStyleBackColor = true;
            this.bDefault.Click += new System.EventHandler(this.bDefault_Click);
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(486, 249);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(75, 23);
            this.bSave.TabIndex = 6;
            this.bSave.Text = "Сохранить";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(567, 249);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 7;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.tbPort);
            this.groupBox5.Controls.Add(this.tbMaxConnections);
            this.groupBox5.Controls.Add(this.tbKeepAliveTime);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.tbAuthTime);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Location = new System.Drawing.Point(12, 19);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(312, 127);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Настройки сервера";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Порт";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(218, 18);
            this.tbPort.MaxLength = 5;
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(85, 20);
            this.tbPort.TabIndex = 0;
            // 
            // tbMaxConnections
            // 
            this.tbMaxConnections.Location = new System.Drawing.Point(218, 44);
            this.tbMaxConnections.MaxLength = 32;
            this.tbMaxConnections.Name = "tbMaxConnections";
            this.tbMaxConnections.Size = new System.Drawing.Size(85, 20);
            this.tbMaxConnections.TabIndex = 1;
            // 
            // tbKeepAliveTime
            // 
            this.tbKeepAliveTime.Location = new System.Drawing.Point(218, 70);
            this.tbKeepAliveTime.MaxLength = 32;
            this.tbKeepAliveTime.Name = "tbKeepAliveTime";
            this.tbKeepAliveTime.Size = new System.Drawing.Size(85, 20);
            this.tbKeepAliveTime.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Время на авторизацию";
            // 
            // tbAuthTime
            // 
            this.tbAuthTime.Location = new System.Drawing.Point(218, 96);
            this.tbAuthTime.MaxLength = 32;
            this.tbAuthTime.Name = "tbAuthTime";
            this.tbAuthTime.Size = new System.Drawing.Size(85, 20);
            this.tbAuthTime.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(179, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Время между KeepAlive пакетами";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(202, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Максимальное количество соеднений";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.tbDBpassword);
            this.groupBox4.Controls.Add(this.tbDBuser);
            this.groupBox4.Controls.Add(this.tbDBname);
            this.groupBox4.Controls.Add(this.tbDBhost);
            this.groupBox4.Location = new System.Drawing.Point(330, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(312, 127);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Настройки Базы Данных";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Пароль";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Пользователь";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Имя";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Хост";
            // 
            // tbDBpassword
            // 
            this.tbDBpassword.Location = new System.Drawing.Point(104, 99);
            this.tbDBpassword.MaxLength = 64;
            this.tbDBpassword.Name = "tbDBpassword";
            this.tbDBpassword.PasswordChar = '*';
            this.tbDBpassword.Size = new System.Drawing.Size(202, 20);
            this.tbDBpassword.TabIndex = 3;
            // 
            // tbDBuser
            // 
            this.tbDBuser.Location = new System.Drawing.Point(104, 73);
            this.tbDBuser.MaxLength = 64;
            this.tbDBuser.Name = "tbDBuser";
            this.tbDBuser.Size = new System.Drawing.Size(202, 20);
            this.tbDBuser.TabIndex = 2;
            // 
            // tbDBname
            // 
            this.tbDBname.Location = new System.Drawing.Point(104, 47);
            this.tbDBname.MaxLength = 64;
            this.tbDBname.Name = "tbDBname";
            this.tbDBname.Size = new System.Drawing.Size(202, 20);
            this.tbDBname.TabIndex = 1;
            // 
            // tbDBhost
            // 
            this.tbDBhost.Location = new System.Drawing.Point(104, 21);
            this.tbDBhost.Name = "tbDBhost";
            this.tbDBhost.Size = new System.Drawing.Size(202, 20);
            this.tbDBhost.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bPath);
            this.groupBox3.Controls.Add(this.tbLogPath);
            this.groupBox3.Controls.Add(this.cbLog);
            this.groupBox3.Controls.Add(this.cbLogConnectMessages);
            this.groupBox3.Controls.Add(this.cbLogErrors);
            this.groupBox3.Location = new System.Drawing.Point(330, 152);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(312, 95);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // bPath
            // 
            this.bPath.Location = new System.Drawing.Point(257, 21);
            this.bPath.Name = "bPath";
            this.bPath.Size = new System.Drawing.Size(49, 23);
            this.bPath.TabIndex = 2;
            this.bPath.Text = "Обзор";
            this.bPath.UseVisualStyleBackColor = true;
            // 
            // tbLogPath
            // 
            this.tbLogPath.Location = new System.Drawing.Point(6, 23);
            this.tbLogPath.Name = "tbLogPath";
            this.tbLogPath.Size = new System.Drawing.Size(246, 20);
            this.tbLogPath.TabIndex = 1;
            // 
            // cbLog
            // 
            this.cbLog.AutoSize = true;
            this.cbLog.Location = new System.Drawing.Point(6, 0);
            this.cbLog.Name = "cbLog";
            this.cbLog.Size = new System.Drawing.Size(76, 17);
            this.cbLog.TabIndex = 0;
            this.cbLog.Text = "Вести лог";
            this.cbLog.UseVisualStyleBackColor = true;
            // 
            // cbLogConnectMessages
            // 
            this.cbLogConnectMessages.AutoSize = true;
            this.cbLogConnectMessages.Location = new System.Drawing.Point(15, 72);
            this.cbLogConnectMessages.Name = "cbLogConnectMessages";
            this.cbLogConnectMessages.Size = new System.Drawing.Size(273, 17);
            this.cbLogConnectMessages.TabIndex = 4;
            this.cbLogConnectMessages.Text = "Сообщения Подключения/Отключения клиентов";
            this.cbLogConnectMessages.UseVisualStyleBackColor = true;
            // 
            // cbLogErrors
            // 
            this.cbLogErrors.AutoSize = true;
            this.cbLogErrors.Location = new System.Drawing.Point(15, 49);
            this.cbLogErrors.Name = "cbLogErrors";
            this.cbLogErrors.Size = new System.Drawing.Size(66, 17);
            this.cbLogErrors.TabIndex = 3;
            this.cbLogErrors.Text = "Ошибки";
            this.cbLogErrors.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbConsole);
            this.groupBox2.Controls.Add(this.cbConClientGPSData);
            this.groupBox2.Controls.Add(this.cbConErrors);
            this.groupBox2.Controls.Add(this.cbConConnectMessages);
            this.groupBox2.Location = new System.Drawing.Point(12, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(312, 95);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // cbConsole
            // 
            this.cbConsole.AutoSize = true;
            this.cbConsole.Location = new System.Drawing.Point(6, 0);
            this.cbConsole.Name = "cbConsole";
            this.cbConsole.Size = new System.Drawing.Size(202, 17);
            this.cbConsole.TabIndex = 0;
            this.cbConsole.Text = "Отображать сообщения в консоле";
            this.cbConsole.UseVisualStyleBackColor = true;
            // 
            // cbConClientGPSData
            // 
            this.cbConClientGPSData.AutoSize = true;
            this.cbConClientGPSData.Location = new System.Drawing.Point(15, 69);
            this.cbConClientGPSData.Name = "cbConClientGPSData";
            this.cbConClientGPSData.Size = new System.Drawing.Size(237, 17);
            this.cbConClientGPSData.TabIndex = 3;
            this.cbConClientGPSData.Text = "Сообщения и координаты пользователей";
            this.cbConClientGPSData.UseVisualStyleBackColor = true;
            // 
            // cbConErrors
            // 
            this.cbConErrors.AutoSize = true;
            this.cbConErrors.Location = new System.Drawing.Point(15, 23);
            this.cbConErrors.Name = "cbConErrors";
            this.cbConErrors.Size = new System.Drawing.Size(66, 17);
            this.cbConErrors.TabIndex = 1;
            this.cbConErrors.Text = "Ошибки";
            this.cbConErrors.UseVisualStyleBackColor = true;
            // 
            // cbConConnectMessages
            // 
            this.cbConConnectMessages.AutoSize = true;
            this.cbConConnectMessages.Location = new System.Drawing.Point(15, 46);
            this.cbConConnectMessages.Name = "cbConConnectMessages";
            this.cbConConnectMessages.Size = new System.Drawing.Size(273, 17);
            this.cbConConnectMessages.TabIndex = 2;
            this.cbConConnectMessages.Text = "Сообщения Подключения/Отключения клиентов";
            this.cbConConnectMessages.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 312);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(671, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.базаДанныхToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(671, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // базаДанныхToolStripMenuItem
            // 
            this.базаДанныхToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьПустуюБазуДанныхToolStripMenuItem,
            this.добавитьПользователяToolStripMenuItem});
            this.базаДанныхToolStripMenuItem.Name = "базаДанныхToolStripMenuItem";
            this.базаДанныхToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.базаДанныхToolStripMenuItem.Text = "База Данных";
            // 
            // создатьПустуюБазуДанныхToolStripMenuItem
            // 
            this.создатьПустуюБазуДанныхToolStripMenuItem.Name = "создатьПустуюБазуДанныхToolStripMenuItem";
            this.создатьПустуюБазуДанныхToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.создатьПустуюБазуДанныхToolStripMenuItem.Text = "Создать пустую Базу данных";
            this.создатьПустуюБазуДанныхToolStripMenuItem.Click += new System.EventHandler(this.создатьПустуюБазуДанныхToolStripMenuItem_Click);
            // 
            // добавитьПользователяToolStripMenuItem
            // 
            this.добавитьПользователяToolStripMenuItem.Name = "добавитьПользователяToolStripMenuItem";
            this.добавитьПользователяToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.добавитьПользователяToolStripMenuItem.Text = "Управление пользователями";
            this.добавитьПользователяToolStripMenuItem.Click += new System.EventHandler(this.добавитьПользователяToolStripMenuItem_Click);
            // 
            // lStatus
            // 
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(25, 17);
            this.lStatus.Text = "      ";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 334);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Settings";
            this.Text = "Конфигуратор сервера";
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbLog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbConsole;
        private System.Windows.Forms.CheckBox cbConClientGPSData;
        private System.Windows.Forms.CheckBox cbConErrors;
        private System.Windows.Forms.CheckBox cbConConnectMessages;
        private System.Windows.Forms.Button bPath;
        private System.Windows.Forms.TextBox tbLogPath;
        private System.Windows.Forms.CheckBox cbLogConnectMessages;
        private System.Windows.Forms.CheckBox cbLogErrors;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDBpassword;
        private System.Windows.Forms.TextBox tbDBuser;
        private System.Windows.Forms.TextBox tbDBname;
        private System.Windows.Forms.TextBox tbDBhost;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem базаДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьПустуюБазуДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьПользователяToolStripMenuItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbKeepAliveTime;
        private System.Windows.Forms.TextBox tbMaxConnections;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbAuthTime;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bDefault;
        private System.Windows.Forms.Button bSaveAndStart;
        private System.Windows.Forms.ToolStripStatusLabel lStatus;

    }
}

