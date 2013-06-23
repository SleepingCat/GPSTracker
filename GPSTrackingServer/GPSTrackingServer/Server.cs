using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using ConfigurationLibrary;

namespace GPSTrackerServer
{

    class Server
    {
        public bool IsRun { get; set; }

        public static Configuration cfg = null;
        FillConfiguration fcfg = new FillConfiguration();

        // Сокет для принятия подключений
        private Socket Sock; 
        private SocketAsyncEventArgs AcceptAsyncArgs;
        
        // Список клиентов
        private List<ClientConnection> Clients = new List<ClientConnection>();

        public Server()
        {
            Console.Title = "GPSTrackingServer";
            cfg = fcfg.ReadConfigFile();
            Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            AcceptAsyncArgs = new SocketAsyncEventArgs();
            AcceptAsyncArgs.Completed += AcceptCompleted;
            System.Timers.Timer timer = new System.Timers.Timer(cfg.KeepAliveTime*100);
            timer.Elapsed += KeepAlive;
            timer.Enabled = true;
            IsRun = false;
        }

        /// <summary>
        /// Запускает сервер
        /// </summary>
        public void Start()
        {
            IsRun = true;
            Sock.Bind(new IPEndPoint(IPAddress.Any, cfg.Port));
            Sock.Listen(cfg.MaxClients);
            AcceptAsync(AcceptAsyncArgs);
            Output.Write("Server started on port" + cfg.Port, 2);
            //Console.WriteLine("Server started on port {0}", cfg.Port);
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

                Output.Write(e.AcceptSocket.RemoteEndPoint + " Trying to connect", 2);
                //Console.WriteLine("{0} Trying to connect", e.AcceptSocket.RemoteEndPoint);
            }
            e.AcceptSocket = null;
            if(Clients.Count < cfg.MaxClients) AcceptAsync(AcceptAsyncArgs);
        }

        /// <summary>
        /// Обработчик события оключения клиента
        /// </summary>
        /// <param name="sender">Клиент</param>
        /// <param name="message">Сообщение о причине отключения</param>
        void Client_disconnected(ClientConnection sender, string message)
        {
            Clients.Remove(sender);
            Output.Write(message, 2);
        }

        /// <summary>
        /// Обработчик успешной авторизации клиента
        /// </summary>
        /// <param name="sender">Клиент</param>
        /// <param name="message">Сообщение об успешной авторизации</param>
        void Client_AuthorizationSuccess(ClientConnection sender, string message)
        {
            Clients.Add(sender);
            sender.SendAsync("Auth Success");
            Output.Write(message, 2);
        }

        /// <summary>
        /// Обработчик неудачной авторизации клиента
        /// </summary>
        /// <param name="sender">Клиент</param>
        /// <param name="message">Сообщение о неудачной авторизации</param>
        void Client_AuthorizationFaild(ClientConnection sender, string message)
        {
            sender.SendAsync("Auth Failed");
            sender.CloseConnection();
            Clients.Remove(sender);
            Output.Write(message, 2);
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
                catch (Exception ex)
                {
                    Output.Write(ex.Message, 1);
                    Clients.Remove(Cl);
                }
            }
        }
        
        /// <summary>
        /// Возвращает ссылку на объект клиентского подключения по имени пользователя
        /// </summary>
        /// <param name="_username">Имя пользователя</param>
        /// <returns>Клиентское подключение</returns>
        private ClientConnection GetDescriptorByUserName(string _username)
        {
            lock (Clients)
            {
                foreach (ClientConnection cl in Clients)
                    if (cl.ClientName == _username) return cl;
            }
            return null;
        }

        /// <summary>
        /// Отправляет пользователю сообщение
        /// </summary>
        /// <param name="_username">Имя поьзователя</param>
        /// <param name="_msg">Сообщение</param>
        private void Send(string _username, string _msg )
        {
            ClientConnection cl = GetDescriptorByUserName(_username);
            if (cl == null) { Output.Write("Client " + _username + " not found", 2);}
            else { cl.SendAsync(_msg); }
        }

        /// <summary>
        /// Отключает от серверауказанного пользователя
        /// </summary>
        /// <param name="_username">Имя пользователя</param>
        /// <returns></returns>
        private bool KickUser(string _username)
        {
            ClientConnection cl = GetDescriptorByUserName(_username);
            if (cl == null) { Output.Write("Client " + _username + " not found", 2); }
            else { cl.CloseConnection(); Clients.Remove(cl); return true; }
            return false;
        }

        /// <summary>
        /// Выводит список пользователей
        /// </summary>
        private void UsersList()
        {
            lock (Clients)
            {
                foreach (ClientConnection cl in Clients)
                {
                    Output.Write("\t " + cl.ClientName, 0);
                    //Console.WriteLine("\t " + cl.ClientName);
                }
            }
        }

        /// <summary>
        /// разбивает строку в массив
        /// </summary>
        /// <param name="inputString">входная строка</param>
        /// <param name="separator">разделитель</param>
        /// <returns></returns>
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

        /// <summary>
        /// Интерпретатор команд с консоли сервера
        /// </summary>
        /// <param name="input">команда</param>
        /// <returns></returns>
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
