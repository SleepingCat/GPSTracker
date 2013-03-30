/*
Хранит информацию о подключении
*/

using System;
using System.Text;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace GPSWinMobileConfigurator
{
    public class Settings
    {
        RegistryKey rkTest;
        MD5 md5 = MD5.Create();

        private string GetMD5(string input)
        {
            // пример сперт с мсдна - костыль на костыле ИМХО. Что нельзя было сделать нормальный МД5 хотябы?
            byte[] tmp = md5.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();
 
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            // Это тормозной велосипед для конвертирования значений байт в hex строку. Для отображения в консоли, например. © kolorotur 
            for (int i = 0; i < tmp.Length; i++)
            {
                sBuilder.Append(tmp[i].ToString("x2"));
            }
 
            // Return the hexadecimal string.
            return sBuilder.ToString(); 
        }

        public string Host {get; set;}
        public string Port { get; set; }
        public string UserName { get; set; }
        public int SendingPeriod { get; set; }
        public int LostPackagesLimit { get; set; }
        private string _hashPassword;
        public string Password 
        {
            get
            {
                return _hashPassword;
            }
            set
            {
                _hashPassword = GetMD5(value);
            }
        }

        public Settings()
        {
            SetDefault();
            try
            {
                rkTest = Registry.CurrentUser.OpenSubKey("GPSTracker", false);
                if (rkTest.ValueCount > 0)
                {
                    Host = rkTest.GetValue("Host").ToString();
                    Port = rkTest.GetValue("Port").ToString();
                    UserName = rkTest.GetValue("UserName").ToString();
                    _hashPassword = rkTest.GetValue("Password").ToString();
                    SendingPeriod = int.Parse(rkTest.GetValue("SendingPeriod").ToString());
                    LostPackagesLimit = int.Parse(rkTest.GetValue("LostPackages").ToString());
                }
                else { throw new Exception("RegKeys not found.");}
                rkTest.Close();
            }
            catch
            {
                Registry.CurrentUser.CreateSubKey("GPSTracker");
                Save();
            }
        }

        private void SetDefault()
        {
            Host = "127.0.0.1";
            Port = "4505";
            UserName = "desu";
            Password = "123";
            SendingPeriod = 1;
            LostPackagesLimit = 0;
        }

        public void Save()
        {
            rkTest = Registry.CurrentUser.OpenSubKey("GPSTracker", true);
            rkTest.SetValue("Host", Host);
            rkTest.SetValue("Port", Port);
            rkTest.SetValue("UserName", UserName);
            rkTest.SetValue("Password", Password);
            rkTest.SetValue("SendingPeriod", SendingPeriod);
            rkTest.SetValue("LostPackages", LostPackagesLimit);
            rkTest.Close();
        }
    }
}
