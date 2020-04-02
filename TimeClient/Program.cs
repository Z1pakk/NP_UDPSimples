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
            Task.Run(() =>
            {
                IPEndPoint localpt = new IPEndPoint(IPAddress.Any, 8001);
                IPEndPoint iPEndPoint1 = new IPEndPoint(IPAddress.Parse("224.5.5.5"), 8001);

                UdpClient udpClient1 = new UdpClient();

                udpClient1.Client.SetSocketOption(
                SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                udpClient1.Client.Bind(localpt);


                udpClient1.JoinMulticastGroup(IPAddress.Parse("224.5.5.5"), 50);
                while (true)
                {
                    // ip and port of server
                    byte[] buffer = new byte[1024];
                    buffer = udpClient1.Receive(ref iPEndPoint1);
                    string date = Encoding.UTF8.GetString(buffer);
                    Console.WriteLine($"Current date and time: {date}");
                }
            });

            // ip and port of server


            while (true)
            {

                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("224.5.5.5"), 8001);

                UdpClient udpClient = new UdpClient();
                udpClient.JoinMulticastGroup(IPAddress.Parse("224.5.5.5"), 50);

                string text = Console.ReadLine();
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                udpClient.Send(buffer, buffer.Length, iPEndPoint);

                udpClient.Close();
            }
        }
    }
}
