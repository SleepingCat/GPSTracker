﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Diagnostics;

namespace GPSWinMobileClient
{
    // State object for receiving data from remote device.
    public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 256;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }

    public class AsynchronousClient
    {
        // The port number for the remote device.
        private string _host = "192.168.1.5";
        private int _port = 4505;
        // Работает ли клиент
        public bool IsConnected { get; private set; }

        // ManualResetEvent instances signal completion.
        private ManualResetEvent connectDone = new ManualResetEvent(false);
        private ManualResetEvent sendDone = new ManualResetEvent(false);
        private ManualResetEvent AuthDone = new ManualResetEvent(false);
        private Socket client;

        public AsynchronousClient(string host, int port)
        {
            IsConnected = false;
            _host = "host";
            _port = port;
        }

        public AsynchronousClient()
        {
            IsConnected = false;
        }
        Thread th;

        public void Start()
        {
            IsConnected = false;
            try
            {
                // Establish the remote endpoint for the socket.
                // The name of the 
                // remote device is "host.contoso.com".

                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Convert.ToInt16("4505"));

                // Create a TCP/IP socket.
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.
                client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();
                if (!IsConnected) { return; }

                // Receive the response from the remote device.
                //th = new Thread(delegate()
                //{
                    Receive(client);
                //});
                //th.IsBackground = true;
                //th.Start();

                // Send test data to the remote device.
                Send(client, "!desu@202cb962ac59075b964b07152d234b70");
                //Send(client, string.Format("!{0}@{1}", settings.UserName, settings.Password));
                //sendDone.WaitOne();
                AuthDone.WaitOne();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.
                IsConnected = true;
            }
            catch (Exception e)
            {
                IsConnected = false;
                Console.WriteLine(e.ToString());
            }
            finally { connectDone.Set(); }
        }

        public void Receive(Socket client)
        {
            try
            {
                // Create the state object.
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                IsConnected = false;
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket 
                // from the asynchronous state object.
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                int bytesRead = 0;

                try // дело в том, что этот кэллбэк запускается в отдельном потоке,а при уничтожении объекта сокета посылает пустое значение, к которому этот, еще живой кэллбэк, пытается применить метод уже уничтоженного сокета.
                {
                    // Read data from the remote device.
                    bytesRead = client.EndReceive(ar);
                }
                catch (ObjectDisposedException) { }

                        // The response from the remote device.
                StringBuilder response = new StringBuilder();

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    if (state.sb.ToString() == "Still Alive =)") {  }
                    else
                    {
                        if (state.sb.ToString() == "Auth Success") { AuthDone.Set(); }
                        if (state.sb.ToString() == "Auth Failed") { AuthDone.Set(); return; }
                        Console.WriteLine(state.sb.ToString());
                    }
                    state.sb = new StringBuilder();
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                }
            }
            catch (Exception e)
            {
                IsConnected = false;
                Console.WriteLine(e.ToString());
            }
        }

        public void Send(string msg)
        {
            Send(client,msg);
        }

        private void Send(Socket client, String data)
        {
            try
            {
                // Convert the string data to byte data using ASCII encoding.
                byte[] byteData = Encoding.ASCII.GetBytes(data);

                // Begin sending the data to the remote device.
                client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
            }
            catch (Exception ex)
            {
                IsConnected = false;
                Console.WriteLine(ex.Message);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);
            }
            catch (Exception e)
            {
                IsConnected = false;
                Console.WriteLine(e.ToString());
            }
            // Signal that all bytes have been sent.
            finally { sendDone.Set(); }
        }

        public void Stop()
        {
            // Release the socket.
            IsConnected = false;
            th.Abort();
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            AsynchronousClient Cl = new AsynchronousClient();
            Cl.Start();
            /*
            while (true)
            {
                string msg = Console.ReadLine();
                Cl.Send(msg);
            }
             * */
            Console.ReadKey();
            Cl.Stop();
        }
    }
}
