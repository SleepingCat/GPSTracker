using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ServerConfigurator
{
    public partial class AddUser : Form
    {

        public AddUser()
        {
            InitializeComponent();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            if (tbName.Text != "" && tbPassword.Text != "")
            {
                if (Regex.IsMatch(tbName.Text, @"^\w{3,}$")) { Program._dbConnection.AddUser(tbName.Text, MD5HashGenerate.GetMD5(tbPassword.Text), tbSecret.Text); }
                else { MessageBox.Show("Недопистимые символы в имени пользоателся \n\r (Используйте только цифры и буквы латинского алфавита)"); }
            }
        }

        private void bGenerateSecret_Click(object sender, EventArgs e)
        {
            tbSecret.Text = Program._dbConnection.CreateInvite(tbName.Text);
        }
    }
}
