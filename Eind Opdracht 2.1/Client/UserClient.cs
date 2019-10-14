using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using LogHandler;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    public class UserClient
    {
        private bool connectionEstablished = false;
        private bool clientRunning = false;
        private NetworkStream stream;
        private TcpClient client;
        private byte[] buffer = new byte[1024];
        private string totalBuffer = "";
        private string userName = "";
        private dynamic steamDataJson;
        private int id = 0;
        private LogWriter logWriterClient;

        static void Main (string[] args)
        {
            UserClient client = new UserClient();
            Thread clientThread = new Thread(client.RunClient);
            clientThread.Start();

            while (!client.connectionEstablished)
            {

            } 
            
            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            LoginWindow loginWindow = new LoginWindow(client);
            Application.Run(loginWindow);
        }

        public UserClient() 
        {
           
        }

        public dynamic SteamDataJSON
        {
            get
            {
                return this.steamDataJson;
            }
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }

        public void RunClient()
        {
            this.client = new TcpClient();
            InitLogWriter();

            while (!connectionEstablished)
            {
                try
                {
                    client.Connect("localhost", 80);
                    Thread.Sleep(500);
                    this.connectionEstablished = true;
                }
                catch (SocketException)
                {
                    logWriterClient.WriteTextToFile(logWriterClient.GetLogPath(), "Connection error, server offline");
                    Console.WriteLine("Connection error, server offline");
                }
            }
            Console.WriteLine("Connection established");
            stream = client.GetStream();
            this.clientRunning = true;
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(this.OnRead), null);
        }

        public void InitLogWriter() 
        { 
            this.logWriterClient = new LogWriter("Client.log");
            this.logWriterClient.WriteTextToFile(logWriterClient.GetLogPath(), "Client started");
        }

        private void Write (string text)
        {
            try
            {
                stream.Write(Encoding.ASCII.GetBytes(text), 0, text.Length);
                stream.Flush();
            }
            catch (NullReferenceException)
            {
                logWriterClient.WriteTextToFile(logWriterClient.GetLogPath(), "Writing error");
                Console.WriteLine();
            }
            
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
                if (clientRunning)
                {
                    stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
                }
                else
                {
                    this.stream.Close();
                    this.client.Close();
                }
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
                    this.clientRunning = bool.Parse(data[1]);
                    Console.WriteLine($"Server says goodbye {this.userName}");
                    this.logWriterClient.WriteTextToFile(logWriterClient.GetLogPath(), "Client shutting down...");
                    break;

                case "data":
                    this.steamDataJson = data[1];
                    JObject jObject;
                    try
                    {
                        jObject = JObject.Parse(steamDataJson);
                        dynamic steamDataConvert = jObject[data[2]];
                        this.steamDataJson = steamDataConvert;
                        Console.WriteLine(steamDataConvert);
                        this.logWriterClient.WriteTextToFile(logWriterClient.GetLogPath(), $"Client succesfully handled the ID-data {this.id} conversion to JSON");
                    }
                    catch (JsonReaderException)
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

        public void SendGoodbye()
        {
            Write($"bye\r\nbye\r\n\r\n");
        } 
    }
}