using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

// A C# Program for Server
namespace ServerConsole
{
    public class Program
    {
        // Main Method
        static void Main(string[] args)
        {
            ExecuteServer();
        }

        public static void ExecuteServer()
        {
            // #1: We establish the local endpoint for the socket
            // Dns.GetHostName returns the name of the host running the application
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress iPAddress = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress, 11111); 

            // #2: We create a TCP/IP Socket using the Socket Class Constructor
            Socket listener = new Socket(iPAddress.AddressFamily, 
                SocketType.Stream, ProtocolType.Tcp);
            
            

        }
        
    }
}