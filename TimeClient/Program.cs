using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TimeClient
{
    class Program
    {
        static void Main(string[] args)
        {   
            Console.WriteLine("Enter any key for get datetime!!!");
            while (true)
            {
                Console.ReadLine();

                // ip and port of server
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1070);

                // ip and port of client
                IPEndPoint iPEndPointClient = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1080);
                UdpClient udpClient = new UdpClient(iPEndPointClient);

                byte[] buffer = Encoding.UTF8.GetBytes("GetDate");
                udpClient.Send(buffer, buffer.Length, iPEndPoint);

                buffer = new byte[1024];
                buffer = udpClient.Receive(ref iPEndPoint);
                string date = Encoding.UTF8.GetString(buffer);
                Console.WriteLine($"Current date and time: {date}");

                udpClient.Close();
            }
        }
    }
}
