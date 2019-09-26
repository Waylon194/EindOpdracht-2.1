﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SteamSpaceStore;

namespace ClientServerDemo
{
    class ServerLogicsHandler
    {
        private TcpClient tcpClient;
        private ServerExecutor program;
        private NetworkStream stream;

        private byte[] buffer = new byte[1024];
        private string totalBuffer = "";

        private SteamStoreAPIHandler steamStore = new SteamStoreAPIHandler();

        private string userName { get; set; }

        public ServerLogicsHandler (TcpClient tcpClient, ServerExecutor program)
        {
            this.tcpClient = tcpClient;
            this.program = program;

            this.stream = tcpClient.GetStream();
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
        }

        private void OnRead (IAsyncResult ar)
        {
            Console.WriteLine("got data");
            int receivedBytes = stream.EndRead(ar);
            totalBuffer += Encoding.ASCII.GetString(buffer, 0, receivedBytes);

            while(totalBuffer.Contains("\r\n\r\n"))
            {
                string packet = totalBuffer.Substring(0, totalBuffer.IndexOf("\r\n\r\n"));
                totalBuffer = totalBuffer.Substring(totalBuffer.IndexOf("\r\n\r\n") + 4);

                string[] data = Regex.Split(packet, "\r\n");
                HandlePacket(data);
            }
            stream.BeginRead (buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
        }

        private void HandlePacket (string[] data)
        {
            switch(data[0])
            {
                case "username": // if id is username
                        Write($"username\r\n {data[1]}\r\n\r\n");
                        this.userName = data[1];
                    break;

                case "get-id": // if id is get-id, gets and send back the steam-API-Json
                    int id = 0;
                    Int32.TryParse(data[1], out id);
                    dynamic storeData = steamStore.GetSteamData(id, "nl");
                    SendSteamData(storeData, id);
                    break;

                case "bye": // if id is bye, closes the connection // TODO---
                        Write($"goodbye\r\n {userName} \r\n\r\n");
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