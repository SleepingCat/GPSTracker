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
        public bool IsRun { get; set; }
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
            IsRun = false;
        }

        /// <summary>
        /// Отправляет сообщение Keep Alive по таймеру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeepAlive(object sender, ElapsedEventArgs e)
        {
            SendToAll("Still Alive =)");
        }

        private void AcceptCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                ClientConnection Client = new ClientConnection(e.AcceptSocket);
                Client.AuthorizationFaild += new ClientConnection.ConnectionEvent(Client_AuthorizationFaild);
                Client.AuthorizationSuccess += new ClientConnection.ConnectionEvent(Client_AuthorizationSuccess);
                Client.Disconnected += new ClientConnection.ConnectionEvent(Client_disconnected);

                Console.WriteLine("{0} Trying to connect", e.AcceptSocket.RemoteEndPoint);
            }
            e.AcceptSocket = null;
            AcceptAsync(AcceptAsyncArgs);
        }

        void Client_disconnected(ClientConnection sender, string message)
        {
            Clients.Remove(sender);
            Console.WriteLine(message);
        }

        void Client_AuthorizationSuccess(ClientConnection sender, string message)
        {
            Clients.Add(sender);
            sender.SendAsync("Auth Success");
            Console.WriteLine(message);
        }

        void Client_AuthorizationFaild(ClientConnection sender, string message)
        {
            sender.SendAsync("Auth Failed");
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
            if (IsRun)
            {
                bool WillRiseEvent = Sock.AcceptAsync(e);
                if (!WillRiseEvent) { AcceptCompleted(Sock, e); }
            }
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

        private ClientConnection GetDescriptorByUserName(string _username)
        {
            lock (Clients)
            {
                foreach (ClientConnection cl in Clients)
                    if (cl.ClientName == _username) return cl;
            }
            return null;
        }

        private void Send(string _usrname, string _msg )
        {
            ClientConnection cl = GetDescriptorByUserName(_usrname);
            if (cl == null) { Console.WriteLine("Client " + _usrname + " not found"); }
            else { cl.SendAsync(_msg); }
        }

        private bool KickUser(string _username)
        {
            ClientConnection cl = GetDescriptorByUserName(_username);
            if (cl == null) { Console.WriteLine("Client " + _username + " not found"); }
            else { cl.CloseConnection(); Clients.Remove(cl); return true; }
            return false;
        }

        private void UsersList()
        {
            lock (Clients)
            {
                foreach (ClientConnection cl in Clients)
                {
                    Console.WriteLine("\t " + cl.ClientName);
                }
            }
        }

        private string[] Explode(string inputString, string separator)
        {
            string[] exploded = new string[2];
            inputString = inputString.Trim();
            int pos = inputString.IndexOf(separator);
            if (pos != -1)
            {
                exploded[1] = inputString.Substring(pos).Trim();
                exploded[0] = inputString.Substring(0, pos).Trim();
            }
            else
            {
                exploded[0] = inputString;
            }
            return exploded;
        }

        public string Command(string input)
        {
            string[] result = Explode(input," ");
            string command = result[0].ToLower();
            string argument = result[1];
            switch (command)
            {
                case "all": { SendToAll(argument); } break;
                case "cls": { Console.Clear(); } break;
                case "stop":
                case "exit":
                case "shutdown": { Stop(); } break;
                case "kick": { KickUser(argument); } break;
                case "list": { UsersList(); } break;
                case "send":
                    {
                        if (argument != null)
                        {
                            result = Explode(argument," ");
                            string username = result[0];
                            string message = result[1];
                            if (message != null) { Send(username, message); }
                            else { Console.WriteLine("Wrong command format"); }
                        }
                        else { Console.WriteLine("Wrong command format"); }
                    } break;
                case "help": 
                    {
                        Console.WriteLine("List show list of connected users");
                        Console.WriteLine("Send <username> <message> - sending message to user");
                        Console.WriteLine("All <message> - sending message to all clients");
                        Console.WriteLine("Kick <username> - disconnect client");
                        Console.WriteLine("Cls - clear this screen");
                        Console.WriteLine("stop/exit/shutdown - shutdown this server");
                    } break;
                default: return "Command not found";
            }
            return "Done";
        }

        /// <summary>
        /// Запускает сервер
        /// </summary>
        public void Start()
        {
            IsRun = true;
            Sock.Bind(new IPEndPoint(IPAddress.Any,Port));
            Sock.Listen(50);
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
                Cl.SendAsync("Server shutdown");
                Cl.CloseConnection();
            }
            IsRun = false;
            Sock.Close();
        }
    }
}
