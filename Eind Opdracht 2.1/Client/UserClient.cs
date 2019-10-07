using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LogHandler;

namespace Client
{
    class UserClient
    {
        private bool clientRunning = true;
        private NetworkStream stream;
        private TcpClient client;
        private byte[] buffer = new byte[1024];
        static string totalBuffer = "";
        private string userName = "";
        private dynamic steamData;
        private int id = 0;

        private LogWriter logWriterClient;

        static void Main (string[] args)
        {
            UserClient userClient = new UserClient();
            string userName = "LFG-Waylon194";

            userClient.RunClient();
                        
            userClient.SendUserName(userName);
            userClient.SendSteamID(730);
            userClient.SendSteamID(230);

            //userClient.Write("bye\r\nbye\r\n\r\n");

            while (userClient.clientRunning)
            {
                
            }
        }

        private void RunClient()
        {
            this.logWriterClient = new LogWriter("Client.log");
            this.logWriterClient.WriteTextToFile(logWriterClient.GetLogPath(), "Client started");
            this.client = new TcpClient();
            client.Connect("localhost", 80);
            stream = client.GetStream();
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(this.OnRead), null);
        }

        private void Write (string text)
        {
            stream.Write(Encoding.ASCII.GetBytes(text), 0, text.Length);
            stream.Flush();
        }

        private void OnRead (IAsyncResult ar)
        {
            try
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
            catch (IOException ioEX)
            {
                this.logWriterClient.WriteTextToFile(logWriterClient.GetLogPath(), "Client IOException, something went wrong with the data gathering");
                Console.WriteLine(ioEX.Message);
            }
        }

        private void HandlePacket(string[] data)
        {
            switch (data[0])
            {
                case "username":
                    this.logWriterClient.WriteTextToFile(logWriterClient.GetLogPath(), $"Server accepted and logged-in user: {data[1]}");
                    Console.WriteLine($"Je bent ingelogd: {data[1]}");
                    break;

                case "goodbye":

                    string userName = "LFG-Waylon194";
                    Console.WriteLine($"Server says goodbye {userName}");
                    this.logWriterClient.WriteTextToFile(logWriterClient.GetLogPath(), "Client shutting down...");
                    this.client.Close();
                    this.clientRunning = false;
                    break;

                case "data":
                    string steamDataJson = data[1];
                    JObject jObject;
                    try
                    {
                        jObject = JObject.Parse(steamDataJson);
                        dynamic steamDataConvert = jObject[data[2]];
                        Console.WriteLine(steamDataConvert);
                        this.logWriterClient.WriteTextToFile(logWriterClient.GetLogPath(), $"Client succesfully handled the ID-data {this.id} conversion to JSON");
                    }
                    catch (JsonReaderException e)
                    {
                        this.logWriterClient.WriteTextToFile(logWriterClient.GetLogPath(), $"JSON Reader exception caught, this id {this.id} is unknown or steam server connection is lost...");
                        Console.WriteLine("JSON reader failed, the given id doesn't exist");
                    }
                    // prints the name for now
                    break;

                default:
                    this.logWriterClient.WriteTextToFile(logWriterClient.GetLogPath(), "Unknown packet received, the client can't handle this packet");
                    Console.WriteLine("Unknown packet");
                    break;
            }
        }

        public void SendUserName (string userName)
        {
            this.userName = userName;
            Write($"username\r\n{userName}\r\n\r\n");
        }

        public void SendSteamID (int id)
        {
            this.id = id;
            Write($"get-id\r\n{id}\r\n\r\n");
        }
    }
}