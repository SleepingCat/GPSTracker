namespace ServerConfigurator
{
    partial class UserEditPassword
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPass2 = new System.Windows.Forms.TextBox();
            this.bPassChange = new System.Windows.Forms.Button();
            this.tbPass1 = new System.Windows.Forms.TextBox();
            this.lStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 92);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(385, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Еще раз";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Введите новый пароль";
            // 
            // tbPass2
            // 
            this.tbPass2.Location = new System.Drawing.Point(141, 33);
            this.tbPass2.Name = "tbPass2";
            this.tbPass2.PasswordChar = '*';
            this.tbPass2.Size = new System.Drawing.Size(234, 20);
            this.tbPass2.TabIndex = 5;
            // 
            // bPassChange
            // 
            this.bPassChange.Location = new System.Drawing.Point(277, 59);
            this.bPassChange.Name = "bPassChange";
            this.bPassChange.Size = new System.Drawing.Size(98, 22);
            this.bPassChange.TabIndex = 8;
            this.bPassChange.Text = "Сменить";
            this.bPassChange.UseVisualStyleBackColor = true;
            this.bPassChange.Click += new System.EventHandler(this.bPassChange_Click);
            // 
            // tbPass1
            // 
            this.tbPass1.Location = new System.Drawing.Point(141, 9);
            this.tbPass1.Name = "tbPass1";
            this.tbPass1.PasswordChar = '*';
            this.tbPass1.Size = new System.Drawing.Size(234, 20);
            this.tbPass1.TabIndex = 4;
            // 
            // lStatus
            // 
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(22, 17);
            this.lStatus.Text = "     ";
            // 
            // UserEditPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 114);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPass2);
            this.Controls.Add(this.bPassChange);
            this.Controls.Add(this.tbPass1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "UserEditPassword";
            this.Text = "UserEdit";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPass2;
        private System.Windows.Forms.Button bPassChange;
        private System.Windows.Forms.TextBox tbPass1;
        private System.Windows.Forms.ToolStripStatusLabel lStatus;
    }
}