//--------------------------------------------------------------------
// класс вывходных сообщений сервера
// выводит системные сообщения в консоль сервера и записывает из в лог
//--------------------------------------------------------------------
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
        /// <summary>
        /// выводит сообщения в консоль и(или) записывает их в лог
        /// </summary>
        /// <param name="_message">сообщение</param>
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

        /// <summary>
        /// выводит сообщение в консоль
        /// </summary>
        /// <param name="_msg">сообщение</param>
        private static void WriteConsole(string _msg)
        {
            if (Server.cfg.Console) Console.WriteLine(_msg);
        }

        /// <summary>
        /// пишет сообщение в лог
        /// </summary>
        /// <param name="_msg">сообщение</param>
        private static void WriteLog(string _msg)
        {
            if (Server.cfg.Log) { Log.Write(_msg); }
        }
    }

    /// <summary>
    /// ведет лог работы сервера
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// записывает в лог-файл
        /// </summary>
        /// <param name="lines">сообщение</param>
        public static void Write(string lines)
        {
            StreamWriter file = new StreamWriter(Server.cfg.LogPath, true);
            file.WriteLine(lines);
            file.Close();
        }
    }
}
