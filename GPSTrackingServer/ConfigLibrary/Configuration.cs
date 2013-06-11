using System;
using System.Xml.Serialization;
using System.IO;
using System.Net;

namespace ConfigurationLibrary
{
    [Serializable]
    public class Configuration
    {
        //server
        public int Port { get; set; }
        public int MaxClients { get; set; }
        public int KeepAliveTime { get; set; }
        public int AuthTime { get; set; }

        // console
        public bool Console { get; set; }
        public bool ShowError { get; set; }
        public bool ShowConnectMessages { get; set; }
        public bool ShowClientGPSData { get; set; }

        //log
        public bool Log { get; set; }
        public bool LogError { get; set; }
        public bool LogConnectMessages { get; set; }
        public string LogPath { get; set; }

        //"Database=GPSTracker;Data Source=192.168.1.8;User Id=GPSTracker;Password=nanodesu";
        public string DB { get; set; }
        public string DBhost { get; set; }
        public string DBuser { get; set; }
        public string DBpassword { get; set; }
    }

    public class FillConfiguration
    {
        const string ConfigFilePath = "./config.xml";

        public Configuration ReadConfigFile() { return this.ReadConfigFile(ConfigFilePath); }

        public Configuration ReadConfigFile(string _FullFileName)
        {
            Configuration _cfg = null;
            XmlSerializer xmlserialazer = new XmlSerializer(typeof(Configuration));
            try
            {
                using (StreamReader r = new StreamReader(_FullFileName))
                {
                    _cfg = (Configuration)xmlserialazer.Deserialize(r);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Can't read input file. Set Default options.");
                _cfg = SetDefault();
            }
            return _cfg;
        }
        public Configuration SetDefault()
        {
            Configuration tmp = new Configuration();
            tmp.Port = 4505;
            tmp.MaxClients = 10;
            tmp.AuthTime = 60 * 5;
            tmp.KeepAliveTime = 60 * 5;
            tmp.Log = true;
            tmp.Console = true;
            tmp.ShowError = true;
            tmp.LogError = true;
            tmp.ShowConnectMessages = true;
            tmp.LogConnectMessages = true;
            tmp.ShowClientGPSData = true;
            tmp.ShowClientGPSData = true;
            tmp.LogPath = "./Log.txt";
            tmp.DBhost = "localhost";
            tmp.DB = "GPSTracker";
            tmp.DBuser = "GPSTracker";
            tmp.DBpassword = "root";
            return tmp;
        }

        public string WriteConfigFile(Configuration cfg)
        {
            try
            {
                XmlSerializer xmlserialazer = new XmlSerializer(typeof(Configuration));
                using (TextWriter myStreamWriter = new StreamWriter(ConfigFilePath))
                {
                    xmlserialazer.Serialize(myStreamWriter, cfg);
                }
            }
            catch (Exception ex) { return ex.Message; }
            return "Writing was successful";
        }
    }
}
