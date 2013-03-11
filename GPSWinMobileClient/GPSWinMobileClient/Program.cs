using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace GPSWinMobileClient
{
    class Program
    {
        static void Main(string[] args)
        {
            AsynchronousClient Cl = new AsynchronousClient();
            Cl.Start();
        }
    }
}
