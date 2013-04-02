using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace GPSTrackerClient
{
    // State object for receiving data from remote device.
    public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 32;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }

    public class Client
    {
        public delegate void ConnectionEventDelegate(string status);
        public event ConnectionEventDelegate Connected;

        Settings settings;

        public bool IsConnected { get; private set; }

        // ManualResetEvent instances signal completion.
        private ManualResetEvent connectDone;
        private ManualResetEvent sendDone;
        private ManualResetEvent AuthDone;
        private Socket client;

        public Client()
        {
            IsConnected = false;
        }

        /*
        public AsynchronousClient(Settings _settings)
        {
            settings = _settings;
            IsConnected = false;
        }
        */
        public void Start()
        {
            settings = new Settings();
            connectDone = new ManualResetEvent(false);
            sendDone = new ManualResetEvent(false);
            AuthDone = new ManualResetEvent(false);
            try
            {
                // Establish the remote endpoint for the socket.
                // The name of the 
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(settings.Host), Convert.ToInt16(settings.Port));

                // Create a TCP/IP socket.
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.
                client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();
                if (!IsConnected) { return; }
                
                Receive(client);

                // Send test data to the remote device.
                Send(client,"!" + settings.UserName + "@" + settings.Password);

                AuthDone.WaitOne();
            }
            catch (Exception)
            {
                //Allert.ShowMessage("Client inner Error");
                Connected("Client inner Error");
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

                //TODO: допилить инвок
                //Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.
                IsConnected = true;
                connectDone.Set();
            }
            catch (Exception)
            {
                //Allert.ShowMessage("Server not found");
                connectDone.Set();
                Connected("Server not found");
                IsConnected = false;
            }
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
            catch (Exception)
            {
                //Allert.ShowMessage("Recieve Error");
                Connected("Recieve Error");
                return;
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

                try // Дело в том, что этот кэллбэк запускается в отдельном потоке,а при уничтожении объекта сокета посылает пустое значение, к которому этот, еще живой кэллбэк, пытается применить метод уже уничтоженного сокета.
                    // Поэтому этот эксепшен может появиться только в случае закрытия клиента и не требует ни обработки ни вывода какого-либо сообщения.
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

                    if (state.sb.ToString() == "Auth Success") { AuthDone.Set(); }
                    if (state.sb.ToString() == "Auth Failed") { AuthDone.Set(); }
                    Connected(state.sb.ToString());
                    state.sb = new StringBuilder();
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                }
            }
            catch (Exception)
            {
                //Allert.ShowMessage("Server down");
                Connected("Can't recieve. Server down");
                return;
            }
        }

        public void Send(string _msg)
        {
            Send(client, _msg);
            sendDone.WaitOne();
        }

        private void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);
                //Console.WriteLine("Sent {0} bytes to server.", bytesSent);
                // Signal that all bytes have been sent.
                sendDone.Set();
            }
            catch (Exception)
            {
                //Allert.ShowMessage("Server down");
                sendDone.Set();
                Connected("Can't send. Server down");
                return;
            }
        }

        public void Stop()
        {
            // Release the socket.
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            IsConnected = false;
        }
    }
}
