using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSpace
{
    class ServerExecutor
    {
        static void Main (string[] args)
        {
            new ServerExecutor();
        }

        TcpListener listener;
        private List<ServerLogicsHandler> clients = new List<ServerLogicsHandler>();

        ServerExecutor()
        {
            Console.WriteLine("ServerLogHandler started...");
            listener = new TcpListener (IPAddress.Any, 80);
            listener.Start();
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
            Console.ReadKey();
        }

        private void OnConnect (IAsyncResult ar)
        {
            var newTcpClient = listener.EndAcceptTcpClient(ar);
            Console.WriteLine("New client connected");
            clients.Add(new ServerLogicsHandler(newTcpClient, this)); // accepts a new client input, and adds to the list of clients

            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }
    }
}