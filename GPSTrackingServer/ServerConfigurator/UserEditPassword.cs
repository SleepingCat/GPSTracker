using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ServerConfigurator
{
    public partial class UserEditPassword : Form
    {
        string id;

        public UserEditPassword(string _id)
        {
            InitializeComponent();
            id = _id;
        }
        private void bPassChange_Click(object sender, EventArgs e)
        {
            if (tbPass1.Text == tbPass2.Text) { Program._dbConnection.ChangePassword(id, MD5HashGenerate.GetMD5(tbPass1.Text)); lStatus.Text = "Пароль изменен"; }
            else { lStatus.Text = "Пароли не совпадают"; }
        }
    }
}
