using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


// A C# program for Client
namespace ClientConsole
{
    public class Program
    {
        // Main Method
        static void Main(string[] args)
        {
            ExecuteClient();
        }

        // ExecuteClient() Method
        public static void ExecuteClient()
        {
            try
            {
                // #1: We establish the remote endpoint for the socket
                // This example uses port 11111 on the local computer
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress iPAddress = ipHost.AddressList[0];
                IPEndPoint localEndPoint  = new IPEndPoint(iPAddress, 11111); 
                
                // #2: We create a TCP/IP Socket using the Socket Class Constructor
                Socket sender = new Socket(iPAddress.AddressFamily, 
                    SocketType.Stream, ProtocolType.Tcp);
                
                try
                {
                    // #3: Connect Socket to the remote endpoint using method Connect()
                    sender.Connect(localEndPoint);

                    // We print EndPoint information to show that we are connected
                    Console.WriteLine($"Socket connected to -> {sender.RemoteEndPoint?.ToString()}");

                    // #4: Create a message that we will send to the server
                    Console.Write("Client: ");
                    string message = Console.ReadLine();

                    byte[] messageSent = Encoding.ASCII.GetBytes(message);
                    int byteSent = sender.Send(messageSent);

                    // Data buffer
                    byte[] messageReceived = new byte[1024];

                    // #5: We receive the message using the Receive() method
                    // This method returns number of bytes received, which we will use to convert to a string
                    int bytesReceived = sender.Receive(messageReceived);
                    System.Console.WriteLine($"Server: {Encoding.ASCII.GetString(messageReceived, 0, bytesReceived)}");

                    // #6: Close Socket using the Close() method
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                // Manage of Socket's Exceptions
                catch (ArgumentNullException ane) {
                    
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }              
                catch (SocketException se) {
                    
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }                
                catch (Exception e) {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            } 
        catch (Exception e) {           
            Console.WriteLine(e.ToString());
        }

        }
    }
}