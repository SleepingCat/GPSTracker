namespace ServerConfigurator
{
    partial class AddFriends
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.clbFriends = new System.Windows.Forms.CheckedListBox();
            this.tbSecret = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bDelete = new System.Windows.Forms.Button();
            this.bGenerate = new System.Windows.Forms.Button();
            this.tbAddFriend = new System.Windows.Forms.TextBox();
            this.bAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.bSave = new System.Windows.Forms.Button();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // clbFriends
            // 
            this.clbFriends.FormattingEnabled = true;
            this.clbFriends.Location = new System.Drawing.Point(12, 118);
            this.clbFriends.Name = "clbFriends";
            this.clbFriends.Size = new System.Drawing.Size(269, 304);
            this.clbFriends.TabIndex = 0;
            // 
            // tbSecret
            // 
            this.tbSecret.Location = new System.Drawing.Point(12, 30);
            this.tbSecret.Name = "tbSecret";
            this.tbSecret.ReadOnly = true;
            this.tbSecret.Size = new System.Drawing.Size(269, 20);
            this.tbSecret.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Секретный код";
            // 
            // bDelete
            // 
            this.bDelete.Location = new System.Drawing.Point(12, 428);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(87, 21);
            this.bDelete.TabIndex = 3;
            this.bDelete.Text = "Удалить";
            this.bDelete.UseVisualStyleBackColor = true;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // bGenerate
            // 
            this.bGenerate.Location = new System.Drawing.Point(194, 9);
            this.bGenerate.Name = "bGenerate";
            this.bGenerate.Size = new System.Drawing.Size(87, 21);
            this.bGenerate.TabIndex = 3;
            this.bGenerate.Text = "Генерировать";
            this.bGenerate.UseVisualStyleBackColor = true;
            this.bGenerate.Click += new System.EventHandler(this.bGenerate_Click);
            // 
            // tbAddFriend
            // 
            this.tbAddFriend.Location = new System.Drawing.Point(12, 70);
            this.tbAddFriend.Name = "tbAddFriend";
            this.tbAddFriend.Size = new System.Drawing.Size(269, 20);
            this.tbAddFriend.TabIndex = 1;
            // 
            // bAdd
            // 
            this.bAdd.Location = new System.Drawing.Point(194, 90);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(87, 21);
            this.bAdd.TabIndex = 3;
            this.bAdd.Text = "Добавить";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(235, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Добавить пользователя по секретному коду";
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(194, 428);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(87, 21);
            this.bSave.TabIndex = 3;
            this.bSave.Text = "Сохранить";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Location = new System.Drawing.Point(15, 101);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(96, 17);
            this.cbSelectAll.TabIndex = 4;
            this.cbSelectAll.Text = "Выбрать всех";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // AddFriends
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 454);
            this.Controls.Add(this.cbSelectAll);
            this.Controls.Add(this.bGenerate);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbAddFriend);
            this.Controls.Add(this.tbSecret);
            this.Controls.Add(this.clbFriends);
            this.Name = "AddFriends";
            this.Text = "AddFriends";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbFriends;
        private System.Windows.Forms.TextBox tbSecret;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.Button bGenerate;
        private System.Windows.Forms.TextBox tbAddFriend;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.CheckBox cbSelectAll;
    }
}