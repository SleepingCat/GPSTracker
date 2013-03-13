using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;

namespace GPSTrackingServer
{
    /// <summary>
    /// Исключение генерируемое в случае невозможности отправки сообщения клиенту
    /// </summary>
    class ClientLostException : Exception
    {
        public override string Message
        {
            get
            {
                return "Невозможно отправить данные клиенту. Клиент отключился.";
            }
        }
    }

    class AuthorizationException : Exception
    {
        string _message = "Ошибка авторизации";
        public AuthorizationException(string message)
        {
            _message = message;
        }
        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }

    class Server
    {
        // Порт сервера
        int Port = 4505;

        // Сокет для принятия подключений
        private Socket Sock; 
        private SocketAsyncEventArgs AcceptAsyncArgs;
        
        // Список клиентов
        private List<ClientConnection> Clients = new List<ClientConnection>();

        public Server()
        {
            Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            AcceptAsyncArgs = new SocketAsyncEventArgs();
            AcceptAsyncArgs.Completed += AcceptCompleted;
            System.Timers.Timer timer = new System.Timers.Timer(10000);
            timer.Elapsed += KeepAlive;
            timer.Enabled = true;
        }

        /// <summary>
        /// Отправляет сообщение Keep Alive по таймеру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeepAlive(object sender, ElapsedEventArgs e)
        {
            SendToAll("Still alive =)");
        }

        private void AcceptCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                ClientConnection Client = new ClientConnection(e.AcceptSocket);
                Client.AuthorizationFaild += new ClientConnection.ConnectionEvent(Client_AuthorizationFaild);
                Client.AuthorizationSuccess += new ClientConnection.ConnectionEvent(Client_AuthorizationSuccess);
                Console.WriteLine("{0} Trying to connect", e.AcceptSocket.RemoteEndPoint);
            }
            e.AcceptSocket = null;
            AcceptAsync(AcceptAsyncArgs);
        }

        void Client_AuthorizationSuccess(ClientConnection sender, string message)
        {
            Clients.Add(sender);
            sender.SendAsync(message);
            Console.WriteLine(message);
        }

        void Client_AuthorizationFaild(ClientConnection sender, string message)
        {
            sender.SendAsync(message);
            sender.CloseConnection();
            Clients.Remove(sender);
            Console.WriteLine(message);
        }       
        
        /// <summary>
        /// Принимает асинхронное подключение
        /// </summary>
        /// <param name="e"></param>
        private void AcceptAsync(SocketAsyncEventArgs e)
        {
            bool WillRiseEvent = Sock.AcceptAsync(e);
            if (!WillRiseEvent){ AcceptCompleted(Sock,e); }
        }

        /// <summary>
        /// Отправляет сообщение всем клиентам
        /// </summary>
        /// <param name="data">Сообщение</param>
        public void SendToAll(string data)
        {
            foreach (ClientConnection Cl in Clients)
            {
                try
                {
                    Cl.SendAsync(data);
                }
                catch (ClientLostException ex)
                {
                    Console.WriteLine(ex.Message);
                    Clients.Remove(Cl);
                }
            }
        }

        /// <summary>
        /// Запускает сервер
        /// </summary>
        public void Start()
        {
            Sock.Bind(new IPEndPoint(IPAddress.Any,Port));
            Sock.Listen(50);
            //AcceptAsyncArgs = new SocketAsyncEventArgs();
            //AcceptAsyncArgs.Completed += AcceptCompleted;
            AcceptAsync(AcceptAsyncArgs);
            Console.WriteLine("Server started on port {0}", Port);
        }

        /// <summary>
        /// отключает всех клиентов и останавливает работу сервера
        /// </summary>
        public void Stop()
        {
            foreach (ClientConnection Cl in Clients)
            {
                Cl.CloseConnection();
            }
            //Sock.Shutdown(SocketShutdown.Both);
            Sock.Close();
        }
    }
}
