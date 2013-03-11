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
                Clients.Add(Client);
            }
            e.AcceptSocket = null;
            AcceptAsync(AcceptAsyncArgs);
        }
        
        /// <summary>
        /// Принимает асинхронное подключение
        /// </summary>
        /// <param name="e"></param>
        private void AcceptAsync(SocketAsyncEventArgs e)
        {
            if (Sock.AcceptAsync(e)) { AcceptCompleted(Sock,e); }
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
