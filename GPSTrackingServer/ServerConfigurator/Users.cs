//---------------------------------------------------------------------
// отображает пользователей системы и элементы управленияпользователями
//---------------------------------------------------------------------
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
    public partial class Users : Form
    {
        BindingSource _bs = new BindingSource();

        public Users()
        {
            InitializeComponent();
            this.dataGridView1.DataSource = _bs;
            this.bindingNavigator1.BindingSource = _bs;        
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            _bs.DataSource = Program._dbConnection.LoadUsersTable();
            this.dataGridView1.Update();
        }

        private void bApplay_Click(object sender, EventArgs e)
        {
            Program._dbConnection.Apply();
            RefreshGrid();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            AddUser _addUser = new AddUser();
            _addUser.ShowDialog();
            RefreshGrid();
        }

        private void bindingNavigatorDeleteItem_MouseUp(object sender, MouseEventArgs e)
        {
            Program._dbConnection.Apply();
        }

        private void bAccess_Click(object sender, EventArgs e)
        {
            AddFriends addfr = new AddFriends(this.dataGridView1.CurrentRow.Cells["UserName"].Value.ToString());
            addfr.ShowDialog();
            RefreshGrid();
        }

        private void bCangePassword_Click(object sender, EventArgs e)
        {
            UserEditPassword _editPass = new UserEditPassword(this.dataGridView1.CurrentRow.Cells["UserName"].Value.ToString());
            _editPass.ShowDialog();
            RefreshGrid();
        }
    }
}
