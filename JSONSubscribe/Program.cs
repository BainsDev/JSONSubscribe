using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using WebSocketSharp;

namespace JSONSubscribe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SetConnection();
            Subscribe();
        }

        private static WebSocket socket;

        private static String clientPort;
        private static string clientPassword;

        public static void SetConnection()
        {
            var line = "";

            using (var stream = File.Open(@"C:/Riot Games/League of Legends/lockfile", FileMode.Open, FileAccess.Read,
                FileShare.ReadWrite))
            {
                using (var streamReader = new StreamReader(stream, Encoding.UTF8, true, 4096))
                {
                    while (!streamReader.EndOfStream) line = streamReader.ReadLine();
                }
            }

            clientPort = line.Split(':', ':')[2];
            clientPassword = line.Split(':', ':')[3];
        }

        public static void Subscribe()
        {
            socket = new WebSocket("wss://127.0.0.1:" + clientPort + "/", "wamp");
            socket.SetCredentials("riot", clientPassword, true);
            socket.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            socket.SslConfiguration.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            socket.OnMessage += OnMessage;
            socket.Connect();
            if (socket.IsAlive)
                Console.WriteLine("Socket connection successfully established with the LCU...");
            socket.Send("[5,\"OnJsonApiEvent\"]");
            Console.ReadLine();
        }

        private static void OnMessage(object sender, MessageEventArgs originalMessage)
        {
            if (!originalMessage.IsText) return;
            var body = SimpleJson.DeserializeObject<JsonArray>(originalMessage.Data);

            dynamic typeID = body[0];

            if (typeID != 8)
                return;

            dynamic message = body[2];
            var messageData = message["data"];

            String finalMessage = "[" + message["uri"] + "\", " + message["eventType"] + ", " + messageData + "]";
            Console.WriteLine(finalMessage);
        }

        protected void OnClose(CloseEventArgs e)
        {
            if (socket != null)
            {
                socket.Close();
                socket = null;
            }
        }
    }

}

