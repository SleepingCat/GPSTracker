using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

// Подключение к MySQL БД осуществляется при помощи MySQL .Net Connector, скачать который можно по ссылке ниже
// http://dev.mysql.com/downloads/connector/net/6.6.html#downloads
// P.S. из исходников у меня запускаться отказался, поэтому лучше качать .msi
using MySql.Data.MySqlClient;

namespace GPSTrackingServer
{
    class ClientConnection
    {
        // Подключение БД
        protected string ConnectionString = "Database=БАЗА;Data Source=ХОСТ;User Id=ПОЛЬЗОВАТЕЛЬ;Password=ПАРОЛЬ";
        //Переменная ConnectionString - это строка подключения в которой:
        //БАЗА - Имя базы в MySQL
        //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
        //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
        //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

        /// <summary>
        /// Конструктор сервера
        /// </summary>

        public string ClientName {private set; get;} // Имя клиента (устанавливается при успешной авторизации)
        private Socket Sock; // Сокет клиента
        private SocketAsyncEventArgs SockAsyncEventArgs; // объект необходимый для передачи сообщений  
        private byte[] buff;

        /// <summary>
        /// Конструктор, задающий основные параметры клиентского подключения
        /// </summary>
        /// <param name="AcceptedSocket">Сокет подключенного клиента</param>
        public ClientConnection(Socket AcceptedSocket)
        {
            ClientName = "Anton";
            buff = new byte[1024];
            Sock = AcceptedSocket;
            SockAsyncEventArgs = new SocketAsyncEventArgs();
            SockAsyncEventArgs.Completed += SockAsyncAuth_Completed;
            SockAsyncEventArgs.SetBuffer(buff, 0, buff.Length);
            ReceiveAsync(SockAsyncEventArgs);
        }

        //TODO: авторизация прикрутить
        private bool authorization(string str)
        {
                //if (str == "desu@123")
                {
                    ClientName = str;
                    Console.WriteLine("Auth Success :{0} ({1}) ",ClientName,Sock.RemoteEndPoint);
                    return true;
                }
                Console.WriteLine("Auth fail : {0}",Sock.RemoteEndPoint);
            return false;
        }

        //TODO: Сделать запрос пользователя для авторизации
        private void Select(string Query)
        {

            MySqlCommand myCommand = new MySqlCommand(Query);

        }

        /// <summary>
        /// Добавляет данные в таблицу
        /// </summary>
        /// <param name="query">Insert-запрос</param>
        public void InsertQuery(string query)
        {
            MySqlConnection Connection = new MySqlConnection(ConnectionString);
            //string InsertQuery = "INSERT INTO Orders (id, customerId, amount) Values(1001, 23, 30.66)";
            MySqlCommand Command = new MySqlCommand(query);
            Command.Connection = Connection;
            try
            {
                Connection.Open(); //Устанавливаем соединение с базой данных.
                Command.ExecuteNonQuery();
                Connection.Close(); //Обязательно закрываем соединение!
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open) { Connection.Close(); }
            }
        }

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
                Connection.Open(); //Устанавливаем соединение с базой данных.

                reader = cmd.ExecuteReader();
                while (reader.Read()) // перебираем полученные данные
                {
                    // тут мы их куда-то запихиваем
                }

                Connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (Connection.State == System.Data.ConnectionState.Open) Connection.Close();
            }
        }

       /// <summary>
       /// В зависимости от вызванной асинхронной операции выбираем обработчик операции
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       private void SockAsyncEventArgs_Completed(object sender, SocketAsyncEventArgs e)
       {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
            }
        }

        /// <summary>
        /// Обработчик события авторизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SockAsyncAuth_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAuth(e); // проверяет логин пароль, в случае неудачи закрывает соединение
            e.Completed -= SockAsyncAuth_Completed; // при успешной авторизации меняет обработчик событий на рабочий
            e.Completed += SockAsyncEventArgs_Completed;
        }

        /// <summary>
        /// Обработка отправки сообщения
        /// </summary>
        /// <param name="e"></param>
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if ((bool)SockAsyncEventArgs.UserToken == false) //проверяем находится ли наш сокет в состоянии отправки
                if (e.SocketError == SocketError.Success)
                    ReceiveAsync(SockAsyncEventArgs);
        }

        /// <summary>
        /// Обработка авторизации
        /// </summary>
        /// <param name="e"></param>
        private void ProcessAuth(SocketAsyncEventArgs e)
        {

            if (e.SocketError == SocketError.Success)
            {
                SockAsyncEventArgs.UserToken = false;
                string str = Encoding.UTF8.GetString(e.Buffer, 0, e.BytesTransferred);
                if (authorization(str))
                {
                    SendAsync("Success");
                }
                else
                {
                    SendAsync("Fail");
                    Sock.Shutdown(SocketShutdown.Both);
                }
            }
        }

        /// <summary>
        /// Обработка приема сообщения
        /// </summary>
        /// <param name="e"></param>
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            
            if (e.SocketError == SocketError.Success)
            {
                SockAsyncEventArgs.UserToken = false;
                string str = Encoding.UTF8.GetString(e.Buffer, 0, e.BytesTransferred);
                Console.WriteLine("Incoming msg from #{0}: {1}", ClientName, str);
                //SendAsync("You send " + str);
            }
        }

        /// <summary>
        /// Получает сообщения от клиента
        /// </summary>
        /// <param name="e"></param>
        private void ReceiveAsync(SocketAsyncEventArgs e)
        {
            if (Sock.ReceiveAsync(e)) ProcessReceive(e);
        }

        /// <summary>
        /// Отправляет сообщение клиенту
        /// </summary>
        /// <param name="data">Сообщение</param>
        public void SendAsync(string data)
        {
            byte [] buff = Encoding.UTF8.GetBytes(data);
            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.Completed += SockAsyncEventArgs_Completed;
            e.SetBuffer(buff, 0, buff.Length);
            SendAsync(e);
        }

        /// <summary>
        /// Отправляет сообщение клиенту
        /// </summary>
        /// <param name="e"></param>
        private void SendAsync(SocketAsyncEventArgs e)
        {
            if (Sock.SendAsync(e)) { ProcessSend(e); }
        }

        /// <summary>
        /// Закрывает подключение.
        /// </summary>
        public void CloseConnection()
        {
            Sock.Close();
        }
    }
}
