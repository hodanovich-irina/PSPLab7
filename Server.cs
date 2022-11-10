using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using SuperWebSocket;

namespace Lab7
{
    class Server
    {
        private static WebSocketServer wsServer;
        static string ans = "";
        static void Main(string[] args)
        {
            wsServer = new WebSocketServer();
            int port = 8080;
            int switcher = 1;
            wsServer.Setup(port);
            wsServer.NewSessionConnected += WsServer_NewSessionConnected;
            wsServer.NewMessageReceived += WsServer_NewMessageReceived;
            wsServer.NewDataReceived += WsServer_NewDataReceived;
            wsServer.SessionClosed += WsServer_SessionClosed;
            wsServer.Start();
            Console.WriteLine("Server is running on port " + port);

            while (switcher != 0)
            {
                Console.Clear();

                Console.WriteLine();

                switcher = Convert.ToInt32(Console.ReadLine());

                switch (switcher)
                {
                    case 1:
                        {
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }

        private static void WsServer_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {
            Console.WriteLine("Session closed");
        }

        private static void WsServer_NewDataReceived(WebSocketSession session, byte[] value)
        {
            Console.WriteLine("Updated");
        }

        private static void WsServer_NewMessageReceived(WebSocketSession session, string value)
        {
            var passw = JsonConvert.DeserializeObject<UserMessag>(value).Password;
            if (passw == "1")
            {
                Console.WriteLine("New message received: " + value);
                ans = value;
                SendExchangeRates(ans);
            }
        }

        private static void WsServer_NewSessionConnected(WebSocketSession session)
        {
            SendExchangeRates(ans);
        }

        private static void SendExchangeRates(string currency)
        {
            foreach (WebSocketSession wss in wsServer.GetAllSessions())
            {
                wss.Send(currency);
            }
            ans = "";
        }
    }
}


class UserMessag
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Message { get; set; }
}