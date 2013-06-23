using System;
using System.Linq;
using System.Text;
using ConfigurationLibrary;

// Подключение к MySQL БД осуществляется при помощи MySQL .Net Connector, скачать который можно по ссылке ниже
// http://dev.mysql.com/downloads/connector/net/6.6.html#downloads
// P.S. из исходников у меня запускаться отказался, поэтому лучше качать .msi
using MySql.Data.MySqlClient;

namespace GPSTrackerServer
{
    public class DBConnection
    {
        // Подключение БД
        protected string ConnectionString;
        //Переменная ConnectionString - это строка подключения в которой:
        //БАЗА - Имя базы в MySQL
        //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
        //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
        //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
        MySqlConnection Connection;
        MySqlCommand myCommand;

        public DBConnection(string _conn) 
        {
            if (string.IsNullOrEmpty(_conn)) { throw new ArgumentNullException(_conn); }
            //ConnectionString = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false", host, user, password, db);
            ConnectionString = _conn;
            Connect();
        }

        public DBConnection(Configuration _cfg)
        {
            ConnectionString = "Database=" + _cfg.DB + ";Data Source=" + _cfg.DBhost + ";User Id=" + _cfg.DBuser + ";Password=" + _cfg.DBpassword + ";";
            Connect();
        }

        private void Connect()
        {
            if (string.IsNullOrEmpty(ConnectionString)) { throw new ArgumentNullException(ConnectionString); }
            try
            {
                Connection = new MySqlConnection(ConnectionString);
                Connection.Open();
            }
            catch (Exception ex) { Output.Write(ex.Message, 1); }
        }

        /// <summary>
        /// получает хэш паролья пользователя
        /// </summary>
        /// <param name="UserName">Имя пользователя</param>
        /// <returns>хэш пароля</returns>
        internal string GetUser(string UserName)
        {
            try
            {
                string query = "select Password from Users where UserName='" + UserName + "'";
                myCommand = new MySqlCommand(query, Connection);
                string result = myCommand.ExecuteScalar().ToString();
                return result;
            }
            catch (NullReferenceException)
            {
                return "User not found";
            }
            catch (Exception ex)
            {
                Output.Write(ex.Message, 1);
                return ex.Message;
            }
        }

        /// <summary>
        /// Добавляет данные в таблицу
        /// </summary>
        /// <param name="query">Insert-запрос</param>
        public void InsertQuery(string query)
        {
            MySqlCommand Command = new MySqlCommand(query,Connection);
            try
            {
                Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Output.Write(ex.Message + "(" +query+ ")", 1);
            }
        }

        public void Open() { Connection.Open(); }
        public void Close() { Connection.Close(); }

        internal string GetUserID(string ClientName)
        {
            try
            {
                string query = "select UserID from Users where UserName='" + ClientName + "'";
                myCommand = new MySqlCommand(query, Connection);
                string result = myCommand.ExecuteScalar().ToString();
                return result;
            }
            catch (NullReferenceException)
            {
                return "User not found";
            }
            catch (Exception ex)
            {
                Output.Write(ex.Message, 1);
                return ex.Message;
            }
        }
    }
}
