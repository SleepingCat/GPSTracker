using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Timers;
using ConfigurationLibrary;

namespace GPSTrackerServer
{
    class Coordinates
    {
        public bool IsGood { get; private set; }
        private string _datetime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        public string Time { get { return _datetime; } }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Speed { get; set; }

        public Coordinates() { }
        public Coordinates(string str) { GetCoords(str); }

        public void GetCoords(string _str)
        {
            try
            {
                string[] separator = { "|" };
                string[] tmp = _str.Replace(",",".").Split(separator, StringSplitOptions.RemoveEmptyEntries);
                Latitude = tmp[0];
                Longitude = tmp[1];
                Speed = tmp[2];
                IsGood = true;
            }
            catch (Exception) { IsGood = false; }
        }
    }

    class ClientConnection
    {
        public string ClientName { private set; get; }      // Имя клиента (устанавливается при успешной авторизации)
        private Socket Sock;                                // Сокет клиента
        private SocketAsyncEventArgs SockAsyncEventArgs;    // объект необходимый для передачи сообщений 
        private byte[] buff;
        private DBConnection db = new DBConnection(Server.cfg);       // подключение к базе данных

        public event ConnectionEvent AuthorizationFaild;    // Событие успешной авторизации
        public event ConnectionEvent AuthorizationSuccess;  // Событие не совсем успешной авторизации
        public event ConnectionEvent Disconnected;           // Событие не совсем успешной авторизации
        public delegate void ConnectionEvent(ClientConnection sender, string message);   

        System.Timers.Timer AuthTime = new System.Timers.Timer(Server.cfg.AuthTime); // время на авторизацию

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

            AuthTime.Elapsed += new ElapsedEventHandler(delegate(object a, ElapsedEventArgs b)
                    { 
                        AuthTime.Stop();
                        AuthorizationFaild(this,"Time of authorization ended");
                    });
                AuthTime.Start();
             
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
            ProcessAuth(e);
            e.Completed -= SockAsyncAuth_Completed;
            e.Completed += SockAsyncEventArgs_Completed;
        }

        /// <summary>
        /// Обработка отправки сообщения
        /// </summary>
        /// <param name="e"></param>
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if ((bool)SockAsyncEventArgs.UserToken == false)
                if (e.SocketError == SocketError.Success)
                    ReceiveAsync(SockAsyncEventArgs);
        }

        /// <summary>
        /// Сама авторизация с кучей проверок
        /// </summary>
        /// <param name="str">Строка авторизации</param>
        private void Authorization(string str)
        {
            if (string.IsNullOrEmpty(str)) AuthorizationFaild(this, Sock.RemoteEndPoint + ": Auth string is empty");
            string[] SplitedString = str.Replace("!", "").Split('@');
            if (SplitedString.Count() != 2) AuthorizationFaild(this, Sock.RemoteEndPoint + ": Auth string haz wrong format");
            string result = db.GetUser("select Password from Users where UserName='" + SplitedString[0] + "'");
            if (result == "User not found") AuthorizationFaild(this, string.Format("{0}: User {1} not found", Sock.RemoteEndPoint, SplitedString[0]));
            if (result != SplitedString[1]) AuthorizationFaild(this, string.Format("{0}: {1} - Wrong password", Sock.RemoteEndPoint, SplitedString[0]));
            ClientName = SplitedString[0];
            AuthorizationSuccess(this, string.Format("{0}: {1} Auth Success", Sock.RemoteEndPoint, ClientName));
        }

        /// <summary>
        /// Обработка авторизации
        /// </summary>
        /// <param name="e"></param>
        private void ProcessAuth(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                AuthTime.Stop();
                SockAsyncEventArgs.UserToken = false;
                string str = Encoding.UTF8.GetString(e.Buffer, 0, e.BytesTransferred);
                Authorization(str);
            }
            else AuthorizationFaild(this, "Lost connection");
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
                if (string.IsNullOrEmpty(str)) { AuthorizationFaild(this,  ClientName + " - Connection Lost"); return; }
                ReceiveAsync(e);
                Output.Write("Incoming msg from #" + ClientName + ": " + str, 3);
                //Console.WriteLine("Incoming msg from #{0}: {1}", ClientName, str);
                //Console.WriteLine(str);
                Coordinates coords = new Coordinates(str);
                if (coords.IsGood) db.InsertQuery(string.Format("insert into {0}(Latitude, Longitude, Speed, Time) values('{1}','{2}','{3}','{4}')",
                    ClientName, coords.Latitude, coords.Longitude, coords.Speed, coords.Time));
                //SendAsync("You send " + str);
            }
        }

        /// <summary>
        /// Получает сообщения от клиента
        /// </summary>
        /// <param name="e"></param>
        private void ReceiveAsync(SocketAsyncEventArgs e)
        {
            bool willRaiseEvent = Sock.ReceiveAsync(e);
            e.UserToken = true;
            if (!willRaiseEvent)
                ProcessReceive(e);
        }

        /// <summary>
        /// Отправляет сообщение клиенту
        /// </summary>
        /// <param name="data">Сообщение</param>
        public void SendAsync(string data)
        {
            byte[] buff = Encoding.UTF8.GetBytes(data);
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
            if (Sock.Connected)
            {
                bool willRaiseEvent = Sock.SendAsync(e);
                if (!willRaiseEvent)
                    ProcessSend(e);
            }
            else { Disconnected(this, "Client lost"); }
        }

        /// <summary>
        /// Закрывает подключение.
        /// </summary>
        public void CloseConnection()
        {
            Sock.Shutdown(SocketShutdown.Both);
            db.Close();
        }
    }
}
