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
            _bs.DataSource = Program._dbConnection.LoadUsersTable();
            dataGridView1.DataSource = _bs;
            bindingNavigator1.BindingSource = _bs;
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Program._dbConnection.Apply();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            AddUser _addUser = new AddUser();
            _addUser.ShowDialog();
            _bs.DataSource = Program._dbConnection.LoadUsersTable();
            this.dataGridView1.Update();
        }

        private void bindingNavigatorDeleteItem_MouseUp(object sender, MouseEventArgs e)
        {
            Program._dbConnection.Apply();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string dsff = dataGridView1.CurrentRow.Cells["UserID"].Value.ToString();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            AddFriends addfr = new AddFriends(this.dataGridView1.CurrentRow.Cells["UserName"].Value.ToString());
            addfr.ShowDialog();
            _bs.DataSource = Program._dbConnection.LoadUsersTable();
            this.dataGridView1.Update();
        }
    }
}
