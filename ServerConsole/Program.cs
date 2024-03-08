using System;
using System.Net;
using System.Net.Quic;
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
            
            try
            {
                // #3: We associate network address to the Server Socket by using Bind() method
                // All clients that want to connect to this Server Socket must know this network address 
                listener.Bind(localEndPoint);

                // #4: We create the client list that will want to connect to the server 
                // by using the Listen() method
                listener.Listen(10); 

                Console.WriteLine("Waiting connection ... ");

                // Suspend while waiting for incoming connections
                // #5: By using the Accept() method the server will accept the connection of the client
                Socket clienSocket = listener.Accept();

                Console.WriteLine("Connected ... ");

                // Data buffer
                byte[] bytes = new byte[1024];

                // Receive client's message and respond
                string data;
                do
                {
                    // Receive client's message
                    int numByte = clienSocket.Receive(bytes);
                    data = Encoding.ASCII.GetString(bytes, 0, numByte);
                    Console.WriteLine($"Client: {data}");

                    // Check if client wants to quit
                    if (data.ToLower() == "quit")
                        break;

                    // Get server's response
                    Console.Write("Server: ");
                    string message = Console.ReadLine();

                    // Send response to the client
                    byte[] messageSent = Encoding.ASCII.GetBytes(message);
                    clienSocket.Send(messageSent);

                } while (data.ToLower() != "quit");             
                // #9: Close client Socket using the Close() method 
                // After closing we can use the closed Socket for a new Client connection
                clienSocket.Shutdown(SocketShutdown.Both);
                clienSocket.Close();
            }
            catch (SocketException se) {
                Console.WriteLine(se.ToString());
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
