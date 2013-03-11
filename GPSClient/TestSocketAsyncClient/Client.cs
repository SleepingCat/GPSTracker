using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace TestSocketAsyncClient
{
    class Client
    {
        private Socket Sock;
        private SocketAsyncEventArgs SockAsyncArgs;
        private byte[] buff;
        public string Login { set; private get; }
        public string Password { set; private get; }
        //public delegate void AuthHandler(string Auth);
        //public event AuthHandler Auth;
        public bool Connected = false;

        public Client(string login, string password)
        {
            this.Login = login;
            this.Password = password;
            SetInitialValues();
        }

        public Client()
        {
            SetInitialValues();
        }

        private void SetInitialValues() 
        {
            buff = new byte[1024];
            Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SockAsyncArgs = new SocketAsyncEventArgs();
            SockAsyncArgs.SetBuffer(buff, 0, buff.Length);
            SockAsyncArgs.Completed += SockAsyncArgs_Completed;
        }
        /*
        public bool ConnectAuthAsync(string Address, int Port, string login, string password)
        {
            this.Login = login;
            this.Password = password;
            if (String.IsNullOrEmpty(Login) || String.IsNullOrEmpty(Password))
            {
                throw new AutentificationFailureException();
            }
            SockAsyncArgs.RemoteEndPoint = new DnsEndPoint(Address, Port);
            ConnectAsync(SockAsyncArgs);
            SendAsync("!" + Login + "@" + Password);
        }
        */
        public void Auth()
        {
            SetInitialValues();
            Console.Write("Логин: ");
            Login = Console.ReadLine();
            Console.Write("Пароль: ");
            Password = Console.ReadLine();
            ConnectAsync("127.0.0.1", 4505);
        }
        public void ConnectAsync(string Address, int Port)
        {
            if (String.IsNullOrEmpty(Login) || String.IsNullOrEmpty(Password))
            {
                Console.WriteLine("ololo");
                Auth();
                return;
            }
            SockAsyncArgs.RemoteEndPoint = new DnsEndPoint(Address, Port);
            buff = Encoding.UTF8.GetBytes("!" + Login + "@" + Password);
            SockAsyncArgs.SetBuffer(buff, 0, buff.Length);
            buff = new byte[1024];
            ConnectAsync(SockAsyncArgs);
            //SendAsync("!" + Login + "@" + Password);
        }
        

        private void ConnectAsync(SocketAsyncEventArgs e)
        {
            bool willRaiseEvent = Sock.ConnectAsync(e);
            if (!willRaiseEvent)
                ProcessConnect(e);
        }

        public void SendAsync(string data)
        {
            if (Sock.Connected && data.Length > 0)
            {
                byte[] buff = Encoding.UTF8.GetBytes(data);
                SocketAsyncEventArgs e = new SocketAsyncEventArgs();
                e.SetBuffer(buff, 0, buff.Length);
                e.Completed += SockAsyncArgs_Completed;
                SendAsync(e);
            }
        }
        private void SendAsync(SocketAsyncEventArgs e)
        {
            bool willRaiseEvent = Sock.SendAsync(e);
            //if ((bool)UserToken == false)
            if (!willRaiseEvent)
                ProcessSend(e);
        }  
        
        private void ReceiveAsync(SocketAsyncEventArgs e)
        {
            bool willRaiseEvent = Sock.ReceiveAsync(e);
            if (!willRaiseEvent)
                ProcessReceive(e);
        }
         

        void SockAsyncArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                    ProcessConnect(e);
                    break;
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    //ProcessSend(e);
                    break;
            }
        }

        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                ReceiveAsync(SockAsyncArgs);
            }
            else
            {
                Console.WriteLine("Dont send");
            }
        }

        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success && e.BytesTransferred > 0)
            {
                string str = Encoding.UTF8.GetString(e.Buffer, 0, e.BytesTransferred);
                //if (str == "Fail") { Console.WriteLine("fail"); Auth(); return; }
                //else if (str == "Success") { Console.WriteLine("Success"); }
                Console.WriteLine("Receive: {0}", str);
                ReceiveAsync(e);
            }
            else
            {
                Console.WriteLine("Dont recieve. Connection lost?");
            }
        }
        private void ProcessConnect(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                Console.WriteLine("Connected to {0}...", e.RemoteEndPoint.ToString());
                
                SockAsyncArgs.SetBuffer(buff, 0, buff.Length);

                ReceiveAsync(SockAsyncArgs);
            }
            else
            {
                Console.WriteLine("Dont connect to {0}", e.RemoteEndPoint.ToString());
            }
        }
    }
}
