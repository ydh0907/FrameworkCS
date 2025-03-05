using Do.Net;
using System.Net;
using System.Net.Sockets;

namespace TestServer
{
    public class Program
    {
        public static Listener Listener;
        public static ChatRoom Room;

        static void Main(string[] args)
        {
            Room = new ChatRoom();

            IPAddress ipAddress = IPAddress.Parse("172.31.2.209");
            IPEndPoint endPoint = new(ipAddress, 9070);

            Listener = new(endPoint);
            if (Listener.Listen())
                Listener.StartAccept(OnAccepted);

            while (true) { }
        }
        
        private static void OnAccepted(Socket socket)
        {
            ClientSession session = new();
            session.Open(socket);
            session.OnConnected(socket.RemoteEndPoint);
        }
    }
}
