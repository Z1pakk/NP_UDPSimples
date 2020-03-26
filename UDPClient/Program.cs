using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            StartSender();
        }

        private static void StartSender()
        {
            using(Socket s =new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
                IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 1098);

                Console.Write("Enter message=>");
                byte[] buffer = Encoding.UTF8.GetBytes(Console.ReadLine());
                s.SendTo(buffer, iPEndPoint);
                Console.WriteLine("Message sent to remote adress.");
            }
        }
    }
}
