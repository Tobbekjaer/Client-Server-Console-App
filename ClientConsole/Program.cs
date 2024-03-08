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

                    // Loop logic to send and receive messages
                    string data;
                    do
                    {
                        // Send a message to the server
                        Console.Write("Client: ");
                        string message = Console.ReadLine();
                        byte[] messageSent = Encoding.ASCII.GetBytes(message);
                        int byteSent = sender.Send(messageSent);

                        // Break out of the loop if "quit" command is sent
                        if (message.ToLower() == "quit")
                            break;

                        // Receive server's response
                        byte[] messageReceived = new byte[1024];
                        int bytesReceived = sender.Receive(messageReceived);
                        data = Encoding.ASCII.GetString(messageReceived, 0, bytesReceived);

                        // Display server's response
                        Console.WriteLine($"Server: {data}");

                    } while (data.ToLower() != "quit");

                    // Close Socket gracefully after conversation ends
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