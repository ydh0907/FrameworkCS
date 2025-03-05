using Do.Net;
using Packets;
using System.Net;

namespace TestServer
{
    public class ChatRoom
    {
        private List<ClientSession> sessions = new();

        private JobQueue jobQueue = new();

        public void Push(Action job) => jobQueue.Push(job);

        public void Broadcast(Packet packet)
        {
            ArraySegment<byte> buffer = packet.Serialize();
            sessions.ForEach(session => session.Send(buffer));
        }

        public void Enter(ClientSession session, EndPoint endPoint)
        {
            sessions.Add(session);
            session.Room = this;

            S_EnterPacket enterPacket = new();
            enterPacket.sender = endPoint.ToString();

            Broadcast(enterPacket);
        }
    }
}
