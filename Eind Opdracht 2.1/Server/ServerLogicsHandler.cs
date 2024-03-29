﻿using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using LogHandler;
using SteamSpaceStore;

namespace ServerSpace
{
    class Server
    {
        private TcpClient tcpClient;
        private ServerExecutor program;
        private NetworkStream stream;
        private bool faultyDisconnect = true;
        private LogWriter logWriterServer;
        private byte[] buffer;
        private string totalBuffer;
        private SteamStoreAPIHandler steamStore;
        private string userName { get; set; }

        public Server (TcpClient tcpClient, ServerExecutor program)
        {
            ServerLogicsHandlerInit();
            this.tcpClient = tcpClient;
            this.program = program;
            this.stream = tcpClient.GetStream();
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
        }

        public void ServerLogicsHandlerInit()
        {
            this.logWriterServer = new LogWriter("Server.log");
            this.buffer = new byte[1024];
            this.totalBuffer = "";
            this.steamStore = new SteamStoreAPIHandler();
        }

        private void OnRead (IAsyncResult ar)
        {
            try
            {
                int receivedBytes = stream.EndRead(ar);
                if (receivedBytes > 0)
                {
                    Console.WriteLine("got data");
                    totalBuffer += Encoding.ASCII.GetString(buffer, 0, receivedBytes);

                    while (totalBuffer.Contains("\r\n\r\n"))
                    {
                        string packet = totalBuffer.Substring(0, totalBuffer.IndexOf("\r\n\r\n"));
                        totalBuffer = totalBuffer.Substring(totalBuffer.IndexOf("\r\n\r\n") + 4);

                        string[] data = Regex.Split(packet, "\r\n");
                        HandlePacket(data);
                    }
                }
                stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);

            }
            catch (IOException IOex)
            {
                if (faultyDisconnect)
                {
                    Console.WriteLine("An error occurred, likely due to a client disconnecting");
                    this.logWriterServer.WriteTextToFile(logWriterServer.GetLogPath(), $"ServerExeception: {IOex.Message}");
                }
                else
                {
                    Console.WriteLine("Client disconnected without errors");
                }
            }
        }

        private void HandlePacket (string[] data)
        {
            this.logWriterServer.WriteTextToFile(logWriterServer.GetLogPath(), $"Server got data:");
            switch (data[0])
            {
                case "username": // if id is username
                        Write($"username\r\n {data[1]}\r\n\r\n");
                        this.userName = data[1];
                        Console.WriteLine("Client connected: " + this.userName);
                        this.logWriterServer.WriteTextToFile(logWriterServer.GetLogPath(), $"Server got username: {this.userName}");
                    break;

                case "get-id": // if id is get-id, gets and send back the steam-API-Json
                    int id = 0;
                    Int32.TryParse(data[1], out id);
                    dynamic storeData = steamStore.GetSteamData(id, "nl");
                    SendSteamData(storeData, id);
                    this.logWriterServer.WriteTextToFile(logWriterServer.GetLogPath(), $"Server got id-request: {data[1]}");
                    break;

                case "bye": // if id is bye, closes the connection
                        Write($"goodbye\r\ntrue\r\n {userName} \r\n\r\n");
                        Console.WriteLine($"Client DC issued: {userName}");
                        this.faultyDisconnect = false;
                        this.logWriterServer.WriteTextToFile(logWriterServer.GetLogPath(), $"Server got client bye message from: {this.userName}");
                    break;

                default:
                        Write("unknown-id\r\n\r\n"); // if id is unknown catches with default switch state
                        Console.WriteLine("Unknown packet");
                        this.logWriterServer.WriteTextToFile(logWriterServer.GetLogPath(), $"Server got unknown message id from: {this.userName}");
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
        }
    }
}