using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer
{
    class User
    {
        public IPAddress Address { get; set; }

        public int Count { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new List<User>();

            //using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            //{
            //}

            UdpClient udpClient = new UdpClient(1098);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 1098);
            try
            {
                while(true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    for (int i = 0; i < users.Count; i++)
                    {
                        Console.WriteLine($" {users[i].Address} - {users[i].Count}");
                    }

                    Console.ResetColor();
                    
                    Console.WriteLine("Waiting for message");
                    byte[] buffer = udpClient.Receive(ref iPEndPoint);
                    string msg = Encoding.UTF8.GetString(buffer);

                    Console.WriteLine($"Received msg from {iPEndPoint.Address.ToString()} at {DateTime.Now.ToString()}");

                    if (users.Count > 0 && users.Count(s => s.Address.ToString() == iPEndPoint.Address.ToString()) > 0)
                        users.First(a => a.Address.ToString() == iPEndPoint.Address.ToString()).Count += 1;
                    else
                        users.Add(new User { Address = iPEndPoint.Address, Count = 1 });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
