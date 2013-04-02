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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Data;
using Microsoft.WindowsMobile.Location;

namespace GPSTrackerClient
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

        private int KeepAliveCounter = 0;

        private EventHandler updateDataHandler;
        private EventHandler updateStatusHandler;
        GpsDeviceState device = null;
        GpsPosition position = null;

        Settings settings = new Settings();

        Client Client = new Client();

        Gps gps = new Gps();
        private TextBox tbHiddenInput;

        Coordinates Coords;

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
            this.tbHiddenInput = new System.Windows.Forms.TextBox();
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
            // tbHiddenInput
            // 
            this.tbHiddenInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbHiddenInput.Location = new System.Drawing.Point(0, 247);
            this.tbHiddenInput.Name = "tbHiddenInput";
            this.tbHiddenInput.Size = new System.Drawing.Size(240, 21);
            this.tbHiddenInput.TabIndex = 1;
            this.tbHiddenInput.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tbHiddenInput);
            this.Controls.Add(this.status);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "GPSTracker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closed += new System.EventHandler(this.Form1_Closed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
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
            updateStatusHandler = new EventHandler(UpdateStatus);

            Coords = new Coordinates(Convert.ToInt16(settings.SendingPeriod));
       
            status.Text = "";
            
            status.Width = Screen.PrimaryScreen.WorkingArea.Width;
            status.Height = Screen.PrimaryScreen.WorkingArea.Height;

            gps.DeviceStateChanged += new DeviceStateChangedEventHandler(gps_DeviceStateChanged);
            gps.LocationChanged += new LocationChangedEventHandler(gps_LocationChanged);

            Client.Connected += new Client.ConnectionEventDelegate(Client_Connected);

            KeepAliveCounter = settings.LostPackagesLimit;

            //Slipknot.DisableDeviceSleep();
        }

        void Client_Connected(string _status)
        {
            _st = _status;
            Invoke(updateStatusHandler);
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

        void UpdateStatus(object sender, System.EventArgs args)
        {
            //status.Text = _st;
            switch (_st)
            {
                case "Still Alive =)":
                    {
                        if (settings.LostPackagesLimit > 0)
                        {
                            KeepAliveCounter++;
                            if (KeepAliveCounter > settings.SendingPeriod * settings.LostPackagesLimit) { StopAllNow("Connection lost"); }
                        }
                    } break;
                case "Auth Success": { gps.Open(); status.Text="gps open"; }break;
                case "Can't recieve. Server down":
                case "Can't send. Server down":
                case "Recieve Error":
                case "Server not found":
                case "Client inner Error":
                case "Auth Failed": { StopAllNow(_st); } break;
                default: status.Text = _st; break;
            }
        }

        void UpdateData(object sender, System.EventArgs args)
        {
            //this.Text = _st;
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
                        Coords.Latitude = position.Latitude;
                        //message += position.Latitude + "|";
                    }

                    if (position.LongitudeValid)
                    {
                        str += "Longitude (DD):\n   " + position.Longitude + "\n";
                        str += "Longitude (D,M,S):\n   " + position.LongitudeInDegreesMinutesSeconds + "\n";
                        //message += position.Longitude + "|";
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
                        Coords.Speed = position.Speed;
                        //message += position.Speed + "|";
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
                        Coords.Time = position.Time;
                        //message += position.Time;
                    }

                    if (position.LatitudeValid && position.LongitudeValid && position.SpeedValid)
                    {
                        Coords.Longitude = position.Longitude;
                        Coords.Latitude = position.Latitude;
                        Coords.Speed = position.Speed;
                        Coords.Counter++;
                    }

                    if (Coords.Counter >= settings.SendingPeriod )
                    {
                        Client.Send(Coords.Latitude + "|" + Coords.Longitude + "|" + Coords.Speed + "|" + Coords.Time);
                        Coords.Counter = 0;
                        KeepAliveCounter = 0;
                    }
                }
                status.Text = str;
            }
        }

        private void StopAllNow(string _msg)
        {
            status.Text = _msg;
            status.Refresh();
            StopAllNow();
        }

        private void StopAllNow()
        {
            if (Client.IsConnected) { Client.Stop(); }
            if (gps.Opened) { gps.Close(); }
            GPGmenu.Text = "Start";
        }

        private void Form1_Closed(object sender, System.EventArgs e)
        {
            StopAllNow();
            Slipknot.EnableDeviceSleep();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (gps.Opened) { gps.Close(); }
            if (!Client.IsConnected)
            {
                /*Client = new AsynchronousClient(settings);*/
                status.Text = "connection to server";
                status.Refresh();
                Client.Start();
                if (Client.IsConnected)GPGmenu.Text = "Stop";
            }
            else
            {
                Client.Stop();
                GPGmenu.Text = "Start";
                status.Text = "Stoped";
            }
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            FormSettings _settings = new FormSettings(settings);
            _settings.ShowDialog();
        }

        bool Code1 = false;
        bool Code2 = false;
        bool Code3 = false;
        bool Code4 = false;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                Code1 = false;
                Code2 = false;
                Code3 = false;
                Code4 = false;
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                // Down
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                if (Code3) { Code4 = true; }
                else { Code1 = true; Code3 = false; Code2 = false; }
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                if (Code1)
                {
                    if (Code2) { Code3 = true; }
                    Code2 = true;
                }
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                if (tbHiddenInput.Visible && tbHiddenInput.Text != "") { Client.Send(tbHiddenInput.Text); }
                else if (Code4) { tbHiddenInput.Visible = true; }
            }

        }
    }

    class Coordinates
    {
        static int size;

        public int Counter { get; set; }

        List<double> _latitude = new List<double>(size);
        List<double> _longitude = new List<double>(size);
        List<double> _speed = new List<double>(size);
        public DateTime Time { get; set; }

        public Coordinates(int _size)
        {
            size = _size;
            Counter = 0;
        }

        public double Latitude
        {
            get 
            {
                double Avg = 0.0;
                lock (_latitude) 
                {
                    foreach (double val in _latitude)
                    {
                        Avg += val;
                    }
                    Avg /= _latitude.Count;
                    _latitude.Clear();
                }
                return Math.Round(Avg,7);
            }
            set { _latitude.Add(value); }
        }

        public double Longitude
        {
            get
            {
                double Avg = 0.0;
                lock (_longitude)
                {
                    foreach (double val in _longitude)
                    {
                        Avg += val;
                    }
                    Avg /= _longitude.Count;
                    _longitude.Clear();
                }
                return Math.Round(Avg, 7);
            }
            set { _longitude.Add(value); }
        }

        public double Speed
        {        
            get
            {
                double Avg = 0.0;
                lock (_speed)
                {
                    foreach (double val in _speed)
                    {
                        Avg += val;
                    }
                    Avg /= _speed.Count;
                    _speed.Clear();
                }
                return Math.Round(Avg, 7);
            }
            set { _speed.Add(value); }
        }
    }
}
