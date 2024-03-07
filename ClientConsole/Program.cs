using System;
using System.Net;


// A C# program for Client
namespace ClientConsole
{
    public class Program
    {
        // Main Method
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        // ExecuteClient() Method
        static void ExecuteClient()
        {
            try
            {
                // #1: We establish the remote endpoint for the socket. 
                // This example uses port 11111 on the local computer.
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress iPAddress = ipHost.AddressList[0];
                IPEndPoint localEndPoint  = new IPEndPoint(iPAddress, 11111); 
                
                // #2: We create a TCP/IP Socket using the Socket Class Constructor

            }

        }
    }
}