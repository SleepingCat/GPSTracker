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
            
            do
            {
                Console.WriteLine(srv.Command(Console.ReadLine()));
            }
            while (srv.IsRun);
             
            Console.WriteLine("Server shutdown, press any key to exit...");
            Console.ReadKey();
        }
    }
}
