using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConfigurationLibrary;

namespace ServerConfigurator
{
    public partial class Settings : Form
    {

        FillConfiguration _fcfg = new FillConfiguration();

        public Settings()
        {
            InitializeComponent();
            this.Fill();
        }

        void Fill() 
        {
            //server
            tbAuthTime.Text = Program.cfg.AuthTime.ToString();
            tbKeepAliveTime.Text = Program.cfg.KeepAliveTime.ToString();
            tbMaxConnections.Text = Program.cfg.MaxClients.ToString();
            tbPort.Text = Program.cfg.Port.ToString();

            //Console
            cbConsole.Checked = Program.cfg.Console;
            cbConErrors.Checked = Program.cfg.ShowError;
            cbConConnectMessages.Checked = Program.cfg.ShowConnectMessages;
            cbConClientGPSData.Checked = Program.cfg.ShowClientGPSData;

            //log
            cbLog.Checked = Program.cfg.Log;
            cbLogErrors.Checked = Program.cfg.LogError;
            cbLogConnectMessages.Checked = Program.cfg.LogConnectMessages;
            tbLogPath.Text = Program.cfg.LogPath;

            //DB
            tbDBhost.Text = Program.cfg.DBhost;
            tbDBuser.Text = Program.cfg.DBuser;
            tbDBpassword.Text = Program.cfg.DBpassword;
            tbDBname.Text = Program.cfg.DB;
        }

        void WriteConfig()
        {
            //server
            Program.cfg.AuthTime = Convert.ToInt16(tbAuthTime.Text);
            Program.cfg.KeepAliveTime = Convert.ToInt16(tbKeepAliveTime.Text);
            Program.cfg.MaxClients = Convert.ToInt16(tbMaxConnections.Text);
            Program.cfg.Port = Convert.ToInt16(tbPort.Text);

            //Console
            Program.cfg.Console = cbConsole.Checked;
            Program.cfg.ShowError = cbConErrors.Checked;
            Program.cfg.ShowConnectMessages = cbConConnectMessages.Checked;
            Program.cfg.ShowClientGPSData = cbConClientGPSData.Checked;

            //log
            Program.cfg.Log = cbLog.Checked;
            Program.cfg.LogError = cbLogErrors.Checked;
            Program.cfg.LogConnectMessages = cbLogConnectMessages.Checked;
            Program.cfg.LogPath = tbLogPath.Text;

            //DB
            Program.cfg.DBhost = Dns.GetHostEntry(tbDBhost.Text).AddressList[0].ToString();
            Program.cfg.DBuser = tbDBuser.Text;
            Program.cfg.DBpassword = tbDBpassword.Text;
            Program.cfg.DB = tbDBname.Text;

            _fcfg.WriteConfigFile(Program.cfg);
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            WriteConfig();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bDefault_Click(object sender, EventArgs e)
        {
            Program.cfg = _fcfg.SetDefault();
            this.Fill();
        }

        private void добавитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.ShowDialog();
        }

        private void создатьПустуюБазуДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = @"
                CREATE TABLE `NewTable` (
                `UserName`  varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL ,
                `Password`  varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL ,
                `Invite`  varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL ,
                `Friends`  text CHARACTER SET utf8 COLLATE utf8_general_ci NULL ,
                `UserID`  int(11) NOT NULL AUTO_INCREMENT ,
                `Permissions`  int(1) NOT NULL DEFAULT 0 ,
                PRIMARY KEY (`UserID`, `UserName`)
                )
                ENGINE=MyISAM
                DEFAULT CHARACTER SET=utf8 COLLATE=utf8_bin
                AUTO_INCREMENT=5
                CHECKSUM=0
                ROW_FORMAT=DYNAMIC
                DELAY_KEY_WRITE=0
                ;go;";
            Program._dbConnection.ExecuteQuery(query);
        }

        private void bSaveAndStart_Click(object sender, EventArgs e)
        {
            WriteConfig();
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("GPSTrackingServer.exe");
            System.Diagnostics.Process.Start(psi);
            Environment.Exit(0);
        }
    }
}
