using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static NetworkStream stream;
        private static byte[] buffer = new byte[1024];
        static string totalBuffer = "";
        private string userName = "LFG-Waylon194";

        static void Main (string[] args)
        {
            string userName = "LFG-Waylon194";
            TcpClient client = new TcpClient();
            client.Connect("localhost", 80);

            stream = client.GetStream();
            stream.BeginRead (buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);

            Write($"username\r\n{userName}\r\n\r\n");
            Write("bye\r\nbye\r\n\r\n");

            while (true)
            {
                Console.WriteLine("enter a message");
                string line = Console.ReadLine();
                //Write($"broadcast\r\n{line}\r\n\r\n");
            }
        }

        private static void Write (string text)
        {
            stream.Write(Encoding.ASCII.GetBytes(text), 0, text.Length);
            stream.Flush();
        }

        private static void OnRead (IAsyncResult ar)
        {
            Console.WriteLine("got data");
            int receivedBytes = stream.EndRead(ar);
            totalBuffer += Encoding.ASCII.GetString(buffer, 0, receivedBytes);

            while (totalBuffer.Contains("\r\n\r\n"))
            {
                string packet = totalBuffer.Substring(0, totalBuffer.IndexOf("\r\n\r\n"));
                totalBuffer = totalBuffer.Substring(totalBuffer.IndexOf("\r\n\r\n") + 4);
                string[] data = Regex.Split(packet, "\r\n");
                HandlePacket(data);
            }
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
        }

        private static void HandlePacket(string[] data)
        {
            
            switch (data[0])
            {
                case "username":
                    Console.WriteLine($"Je bent ingelogd: {data[1]}");
                    break;
                case "goodbye":
                    string userName = "LFG-Waylon194";
                    Console.WriteLine($"Server says goodbye {userName}");
                    break;
                default:
                    Console.WriteLine("Unknown packet");
                    break;
            }
        }
    }
}