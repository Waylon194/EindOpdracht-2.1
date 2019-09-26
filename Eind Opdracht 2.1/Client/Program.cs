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

        static void Main (string[] args)
        {
            TcpClient client = new TcpClient();
            client.Connect("localhost", 80);

            stream = client.GetStream();
            stream.BeginRead (buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);

            Write("login\r\njohan\r\njohan\r\n\r\n");

            while (true)
            {
                Console.WriteLine("enter a message");
                string line = Console.ReadLine();
                Write($"broadcast\r\n{line}\r\n\r\n");
            }
        }

        private static void Write (string v)
        {
            stream.Write(System.Text.Encoding.ASCII.GetBytes(v), 0, v.Length);
            stream.Flush();
        }

        private static void OnRead (IAsyncResult ar)
        {
            Console.WriteLine("got data");
            int receivedBytes = stream.EndRead(ar);
            totalBuffer += System.Text.Encoding.ASCII.GetString(buffer, 0, receivedBytes);

            while (totalBuffer.Contains("\r\n\r\n"))
            {
                string packet = totalBuffer.Substring(0, totalBuffer.IndexOf("\r\n\r\n"));
                totalBuffer = totalBuffer.Substring(totalBuffer.IndexOf("\r\n\r\n") + 4);
                string[] data = Regex.Split(packet, "\r\n");
                handlePacket(data);
            }
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
        }

        private static void handlePacket(string[] data)
        {
            switch (data[0])
            {
                case "username":
                    Console.WriteLine($"Je bent ingelogd: {data[1]}");
                    break;
                case "message":
                    Console.WriteLine($"Bericht van {data[1]}: {data[2]}");
                    break;
                default:
                    Console.WriteLine("Unknown packet");
                    break;
            }
        }
    }
}