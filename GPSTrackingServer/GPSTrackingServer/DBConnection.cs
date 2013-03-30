using System;
using System.Linq;
using System.Text;

// Подключение к MySQL БД осуществляется при помощи MySQL .Net Connector, скачать который можно по ссылке ниже
// http://dev.mysql.com/downloads/connector/net/6.6.html#downloads
// P.S. из исходников у меня запускаться отказался, поэтому лучше качать .msi
using MySql.Data.MySqlClient;

namespace GPSTrackingServer
{
    class DBConnection
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

        public DBConnection()
        {
            ConnectionString = "Database=GPSTracker;Data Source=192.168.1.8;User Id=GPSTracker;Password=nanodesu";
            Connect();
        }

        private void Connect()
        {
            if (string.IsNullOrEmpty(ConnectionString)) { throw new ArgumentNullException(ConnectionString); }
            Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
        }

        public string SelectOne(string Query)
        {
            try
            {
                //Connection.Open();
                myCommand = new MySqlCommand(Query, Connection);
                string result = myCommand.ExecuteScalar().ToString();
                //Connection.Close();
                return result;
            }
            catch (NullReferenceException)
            {
                return "User not found";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
                /*
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open) { Connection.Close(); }
            }
                 */
        }
        /// <summary>
        /// Добавляет данные в таблицу
        /// </summary>
        /// <param name="query">Insert-запрос</param>
        public void InsertQuery(string query)
        {
            //string InsertQuery = "INSERT INTO Orders (id, customerId, amount) Values(1001, 23, 30.66)";
            MySqlCommand Command = new MySqlCommand(query,Connection);
            try
            {
                //Connection.Open(); //Устанавливаем соединение с базой данных.
                Command.ExecuteNonQuery();
                //Connection.Close(); //Обязательно закрываем соединение!
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
                /*
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open) { Connection.Close(); }
            }
                 */
        }

        public void Open() { Connection.Open(); }
        public void Close() { Connection.Close(); }

        /// <summary>
        /// Запрашивает данные из таблицы (зачем? - х.з., чтобы было)
        /// </summary>
        /// <param name="query">Select-запрос</param>
        private void SelectQuery(string query)
        {

            MySqlConnection Connection = new MySqlConnection(ConnectionString);
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            MySqlDataReader reader = null;

            try
            {
                //Connection.Open(); //Устанавливаем соединение с базой данных.

                reader = cmd.ExecuteReader();
                while (reader.Read()) // перебираем полученные данные
                {
                    // тут мы их куда-то запихиваем
                }

                //Connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                //if (Connection.State == System.Data.ConnectionState.Open) Connection.Close();
            }
        }
    }
}
