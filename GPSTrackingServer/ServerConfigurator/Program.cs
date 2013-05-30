using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ConfigurationLibrary;

namespace ServerConfigurator
{
    static class Program
    {
        public static Configuration cfg;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        public static DBConnect _dbConnection = new DBConnect();
        [STAThread]
        static void Main()
        {
            FillConfiguration fcfg = new FillConfiguration();
            cfg = fcfg.ReadConfigFile();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Settings());
        }
    }
}
