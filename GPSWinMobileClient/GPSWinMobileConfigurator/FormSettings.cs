using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GPSWinMobileConfigurator
{
    public enum SendTime { }

    public partial class FormSettings : Form
    {
        Settings _settings;

        public FormSettings(Settings settings)
        {
            _settings = settings;
            InitializeComponent();
        }

        private void save_Click(object sender, EventArgs e)
        {
            _settings.Host = tbHost.Text;
            _settings.Port = tbPort.Text;
            _settings.UserName = tbLogin.Text;
            if (_settings.Password != tbPassword.Text) _settings.Password = tbPassword.Text;
            _settings.SendingPeriod = cbSendingPeriod.SelectedItem.ToString() == "" ? 1 : int.Parse(cbSendingPeriod.SelectedItem.ToString());
            _settings.LostPackagesLimit = cbLostPackagesLimit.SelectedItem.ToString() == "" ? 0 : int.Parse(cbLostPackagesLimit.SelectedItem.ToString());
            _settings.Save();
            this.Close();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            tbHost.Text = _settings.Host;
            tbPort.Text = _settings.Port;
            tbLogin.Text = _settings.UserName;
            tbPassword.Text = _settings.Password;
            cbSendingPeriod.SelectedItem = _settings.SendingPeriod.ToString();
            cbLostPackagesLimit.SelectedItem = _settings.LostPackagesLimit.ToString();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}