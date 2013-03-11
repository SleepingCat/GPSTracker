using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace GPSTrackingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server srv = new Server();
            srv.Start();
            srv.SendToAll(Console.ReadLine());
            Console.ReadKey();
            srv.Stop();
        }
    }
}
