using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TimeServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // ip and port server
            IPEndPoint iPEndPointServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1070);

            // ip and port client
            IPEndPoint iPEndPointClient = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1080);

            UdpClient udpServer = new UdpClient(iPEndPointServer);

            while(true)
            {
                Console.WriteLine("Wait for connection");

                byte[] buffer = new byte[1024];
                buffer = udpServer.Receive(ref iPEndPointClient);

                string str = Encoding.UTF8.GetString(buffer);
                if(str == "GetDate")
                {
                    string currentDate = DateTime.Now.ToString();
                    buffer = Encoding.UTF8.GetBytes(currentDate);
                    udpServer.Send(buffer, buffer.Length, iPEndPointClient);
                }

                Console.WriteLine($"Received msg from {iPEndPointClient.Address.ToString()} at {DateTime.Now.ToString()}");

            }

        }
    }
}
