using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ConfigurationLibrary;

namespace ServerConfigurator
{
    static class Program
    {
        public static Configuration cfg;    // файл конфигурации
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        public static DBConnect _dbConnection = new DBConnect();    // объект для работы с базой данных
        [STAThread]
        static void Main()
        {
            FillConfiguration fcfg = new FillConfiguration();   // считывает настройки из файла конфигурации
            cfg = fcfg.ReadConfigFile();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Settings());
        }
    }
}
