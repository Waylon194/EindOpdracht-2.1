using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ServerLogHandler;
using SteamSpaceStore;

namespace ServerSpace
{
    class ServerLogicsHandler
    {
        private TcpClient tcpClient;
        private ServerExecutor program;
        private NetworkStream stream;

        private LogWriter logWriter;

        private byte[] buffer;
        private string totalBuffer;

        private SteamStoreAPIHandler steamStore;
        private string userName { get; set; }

        public ServerLogicsHandler (TcpClient tcpClient, ServerExecutor program)
        {
            this.tcpClient = tcpClient;
            this.program = program;

            this.stream = tcpClient.GetStream();

            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
        }

        public ServerLogicsHandler ()
        {
            this.logWriter = new LogWriter();
            this.buffer = new byte[1024];
            this.totalBuffer = "";
            this.steamStore = new SteamStoreAPIHandler();
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
            catch (IOException)
            {
                Console.WriteLine("An error occurred, likely due to a client disconnecting");
            }
        }

        private void HandlePacket (string[] data)
        {
            switch(data[0])
            {
                case "username": // if id is username
                        Write($"username\r\n {data[1]}\r\n\r\n");
                        this.userName = data[1];
                        Console.WriteLine("Client connected: " + this.userName);
                    break;

                case "get-id": // if id is get-id, gets and send back the steam-API-Json
                    int id = 0;
                    Int32.TryParse(data[1], out id);
                    dynamic storeData = steamStore.GetSteamData(id, "nl");
                    SendSteamData(storeData, id);
                    break;

                case "bye": // if id is bye, closes the connection // TODO---
                        Write($"goodbye\r\n {userName} \r\n\r\n");
                        Console.WriteLine($"Client DC issued: {userName}");
                    break;

                default:
                        Write("unknown-id\r\n\r\n"); // if id is unknown catches with default switch state
                        Console.WriteLine("Unknown packet");
                    break;
            }
        }

        public void Write (string text)
        {
            stream.Write(Encoding.ASCII.GetBytes(text), 0, text.Length);
            stream.Flush();
        }

        public void SendSteamData (dynamic data, int idNumber)
        {
            Write($"data\r\n{data}\r\n{idNumber.ToString()}\r\n\r\n");
            //Console.WriteLine(data);
        }
    }
}