//
// Copyright (c) Microsoft Corporation.  All rights reserved.
//
//
// Use of this sample source code is subject to the terms of the Microsoft
// license agreement under which you licensed this sample source code. If
// you did not accept the terms of the license agreement, you are not
// authorized to use this sample source code. For the terms of the license,
// please see the license agreement between you and Microsoft or, if applicable,
// see the LICENSE.RTF on your install media or the root of your tools installation.
// THE SAMPLE SOURCE CODE IS PROVIDED "AS IS", WITH NO WARRANTIES OR INDEMNITIES.
//
//
// Copyright (c) Microsoft Corporation.  All rights reserved.
//
//
// Use of this source code is subject to the terms of the Microsoft end-user
// license agreement (EULA) under which you licensed this SOFTWARE PRODUCT.
// If you did not accept the terms of the EULA, you are not authorized to use
// this source code. For a copy of the EULA, please see the LICENSE.RTF on your
// install media.
using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using Microsoft.WindowsMobile.Location;

namespace GPSWinMobileConfigurator
{

    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private System.Windows.Forms.MenuItem Settings;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.Label status;
        private MenuItem GPGmenu;
        private string _st;

        private EventHandler updateDataHandler;
        GpsDeviceState device = null;
        GpsPosition position = null;

        Settings settings = new Settings();

        AsynchronousClient Client = new AsynchronousClient();

        Gps gps = new Gps();

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            base.Dispose( disposing );
        }
        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.Settings = new System.Windows.Forms.MenuItem();
            this.GPGmenu = new System.Windows.Forms.MenuItem();
            this.status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.Settings);
            this.mainMenu1.MenuItems.Add(this.GPGmenu);
            // 
            // Settings
            // 
            this.Settings.Text = "Settings";
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // GPGmenu
            // 
            this.GPGmenu.Text = "Start";
            this.GPGmenu.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // status
            // 
            this.status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.status.Location = new System.Drawing.Point(0, 0);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(240, 268);
            this.status.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.status);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "GPSTracker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closed += new System.EventHandler(this.Form1_Closed);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static void Main() 
        {
            Application.Run(new Form1());
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            updateDataHandler = new EventHandler(UpdateData);
         
            status.Text = "";
            
            status.Width = Screen.PrimaryScreen.WorkingArea.Width;
            status.Height = Screen.PrimaryScreen.WorkingArea.Height;

            gps.DeviceStateChanged += new DeviceStateChangedEventHandler(gps_DeviceStateChanged);
            gps.LocationChanged += new LocationChangedEventHandler(gps_LocationChanged);

            Client.Connected += new AsynchronousClient.ConnectionEventDelegate(Client_Connected);

            Slipknot.DisableDeviceSleep();
        }

        void Client_Connected(string status)
        {
            _st = status;
            Invoke(updateDataHandler);
        }

        protected void gps_LocationChanged(object sender, LocationChangedEventArgs args)
        {
            position = args.Position;

            // call the UpdateData method via the updateDataHandler so that we
            // update the UI on the UI thread
            Invoke(updateDataHandler);
        }

        void gps_DeviceStateChanged(object sender, DeviceStateChangedEventArgs args)
        {
            device = args.DeviceState;

            // call the UpdateData method via the updateDataHandler so that we
            // update the UI on the UI thread
            Invoke(updateDataHandler);
        }

        void UpdateData(object sender, System.EventArgs args)
        {
            if (gps.Opened)
            {
                string str = "";
                if (device != null)
                {
                    str = device.FriendlyName + " " + device.ServiceState + ", " + device.DeviceState + "\n";
                }

                string message = String.Empty;

                if (position != null)
                {

                    if (position.LatitudeValid)
                    {
                        str += "Latitude (DD):\n   " + position.Latitude + "\n";
                        str += "Latitude (D,M,S):\n   " + position.LatitudeInDegreesMinutesSeconds + "\n";
                        message += position.Latitude + "|";
                    }

                    if (position.LongitudeValid)
                    {
                        str += "Longitude (DD):\n   " + position.Longitude + "\n";
                        str += "Longitude (D,M,S):\n   " + position.LongitudeInDegreesMinutesSeconds + "\n";
                        message += position.Longitude + "|";
                    }

                    if (position.SatellitesInSolutionValid &&
                        position.SatellitesInViewValid &&
                        position.SatelliteCountValid)
                    {
                        str += "Satellite Count:\n   " + position.GetSatellitesInSolution().Length + "/" +
                            position.GetSatellitesInView().Length + " (" +
                            position.SatelliteCount + ")\n";
                    }

                    if (position.SpeedValid)
                    {
                        str += "Speed:\n" + position.Speed + "\n";
                        message += position.Speed + "|";
                    }

                    if (position.EllipsoidAltitudeValid)
                    {
                        str += "Altitude: \n" + position.EllipsoidAltitude + "\n";
                    }

                    if (position.PositionDilutionOfPrecisionValid)
                    {
                        str += "PositionDilutionOfPrecision: \n" + position.PositionDilutionOfPrecision + "\n";
                    }

                    if (position.SeaLevelAltitudeValid)
                    {
                        str += "SeaLevelAltitude: \n" + position.SeaLevelAltitude + "\n";
                    }
                    
                    if (position.TimeValid)
                    {
                        str += "Time:\n   " + position.Time.ToString() + "\n";
                        message += position.Time;
                    }
                }
                status.Text = str;
                Client.Send(message);
            }
        }

        private void Form1_Closed(object sender, System.EventArgs e)
        {
            if (Client.IsConnected) { Client.Stop(); }
            if (gps.Opened) { gps.Close(); }
            Slipknot.EnableDeviceSleep();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (!Client.IsConnected) { Client = new AsynchronousClient(settings); Client.Start(); } else { Client.Stop(); }
            if (!gps.Opened) { gps = new Gps(); gps.Open(); } else { gps.Close(); }
            if (Client.IsConnected && gps.Opened) { GPGmenu.Text = "Stop"; }
            else 
            {
                if (gps.Opened) gps.Close();
                if (Client.IsConnected) Client.Stop();
                GPGmenu.Text = "Start";
            }
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            FormSettings _settings = new FormSettings(settings);
            _settings.ShowDialog();
        }
    }
}
