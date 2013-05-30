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
    public partial class AddFriends : Form
    {
        string id;

        public AddFriends(string _id)
        {
            InitializeComponent();
            id = _id;
            FillData();
        }

        void FillData()
        {
            User u = Program._dbConnection.GetUser(id);
            this.tbSecret.Text = u.Invite;
            this.Text = u.Name;
            string[] friends = u.Friends.Split(';');
            this.clbFriends.Items.Clear();
            foreach (string friend in friends)
            {
                User usr = Program._dbConnection.GetUser(friend);
                if (!string.IsNullOrEmpty(friend))
                    this.clbFriends.Items.Add(usr);
            }
        }

        private void bGenerate_Click(object sender, EventArgs e)
        {
            this.tbSecret.Text = Program._dbConnection.CreateInvite(id);
            Program._dbConnection.ExecuteQuery("Update Users set Invite='" + this.tbSecret.Text + "' where UserID='" + id + "'");
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            string FriendID = Program._dbConnection.GetUserIDbySecret(tbAddFriend.Text);
            if (string.IsNullOrEmpty(FriendID)) { MessageBox.Show("Пользователь с таким кодом не найден."); }
            else 
            {
                User u = Program._dbConnection.GetUser(FriendID);
                this.clbFriends.Items.Add(u);
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            Program._dbConnection.AddFriends(this.id, this.clbFriends);
        }

        bool AllChecked = false;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.clbFriends.Items.Count; i++)
            {
                this.clbFriends.SetItemChecked(i, !AllChecked);
                AllChecked = !AllChecked;
            }
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.clbFriends.Items.Count; i++)
            {
                if (this.clbFriends.CheckedItems.Contains(this.clbFriends.Items[i])) this.clbFriends.Items.Remove(this.clbFriends.Items[i]);
            }
        }
    }
}
