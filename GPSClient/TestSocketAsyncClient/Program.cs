using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSocketAsyncClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Client Cl = new Client();
            Cl.Auth();
            //Cl.SendAsync("desu@123");
            while (true)
            {
                string data = Console.ReadLine();
                Cl.SendAsync(data);
            }
        }
    }
}
