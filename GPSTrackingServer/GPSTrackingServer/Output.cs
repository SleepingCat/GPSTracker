using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using ConfigurationLibrary;

namespace GPSTrackerServer
{
    public class Output
    {
        /* private Log log;

        private Configuration cfg;
        public Output(Configuration _cfg)
        { 
            cfg = _cfg;

        }

        public Output()
        {
            cfg = Server.cfg;
            log = new Log(cfg.LogPath);
        }
         * */
        // 0 - console commands
        // 1 - error
        // 2 - system
        // 3 - client msg
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_message"></param>
        /// <param name="_msgCode">        
        /// 0 - console commands
        /// 1 - error
        /// 2 - system
        /// 3 - client msg
        ///</param>
        public static void Write(string _message, int _msgCode)
        {
            Configuration cfg = Server.cfg;
            if (cfg.Console || cfg.Log)
            switch (_msgCode)
            {
                // 1 - error
                case 1: if (cfg.ShowError) WriteConsole(_message); if (cfg.LogError) { WriteLog(_message); } break;
                // 2 - system
                case 2: if (cfg.ShowConnectMessages) WriteConsole(_message); if (cfg.ShowConnectMessages) { WriteLog(_message); } break;
                // 3 - client msg
                case 3: if (cfg.ShowClientGPSData) WriteConsole(_message); break;
                // 0 - console commands
                case 0:
                default: Console.WriteLine(_message); break;
            }
        }

        private static void WriteConsole(string _msg)
        {
            if (Server.cfg.Console) Console.WriteLine(_msg);
        }

        private static void WriteLog(string _msg)
        {
            //if (log == null) { log = new Log(cfg.LogPath); }
            if (Server.cfg.Log) { Log.Write(_msg); }
        }
    }
    public static class Log
    {
        /*
        private string logPath;
        public Log(string _logPath) { logPath = _logPath; }
        */
         
        public static void Write(string lines)
        {
            StreamWriter file = new StreamWriter(Server.cfg.LogPath, true);
            file.WriteLine(lines);
            file.Close();
        }
    }
}
