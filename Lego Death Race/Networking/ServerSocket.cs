using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lego_Death_Race.Networking
{
    public class ConnectedEventArgs : EventArgs
    {
        public bool Connected { get; set; }
    }
    public class PacketEventArgs : EventArgs
    {
        public int PlayerIndex { get; set; }
        public Packet Packet { get; set; }
    }

    public class ServerSocket
    {
        public delegate void PacketRecievedEventHandler(object sender, PacketEventArgs e);
        public event PacketRecievedEventHandler PacketRecieved;

        /*public delegate void ConnectionStatusChangedEventHandler(object sender, ConnectedEventArgs e);
        public event ConnectionStatusChangedEventHandler BrickConnectionStatusChanged;*/

        private const int mPort = 45621;
        // Server socket
        private Socket mServerSocket = null;

        //public List<ConnectionToBrick> mConnectedBricks = new List<ConnectionToBrick>();
        private int mPlayerCount;

        private List<RegisteredClient> mRegisteredClients = new List<RegisteredClient>();

        //public List<ConnectionToBrick> ConnectedBricks { get { return mConnectedBricks; } }

        public ServerSocket(int playerCount)
        {
            mPlayerCount = playerCount;
            // Establish the local endpoint for the socket.  
            // The DNS name of the computer
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, mPort);
            mServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            mServerSocket.Bind(localEndPoint);

            new Thread(ReceivePackets).Start();
        }

        private void ReceivePackets()
        {
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remote = (EndPoint)(sender);
            byte[] buffer = new byte[256];
            while (true)
            {
                int recv = mServerSocket.ReceiveFrom(buffer, ref remote);
                RegisteredClient c = GetRegisteredClientByEndPoint(remote);
                // Add client to list if it's not already
                if (c == null)
                {
                    c = new RegisteredClient(remote);
                    mRegisteredClients.Add(c);
                }
                OnPacketRecieved(new PacketEventArgs() { PlayerIndex = c.PlayerId, Packet = new Packet(buffer) });
            }
        }

        public void SendPacket(int playerId, Packet p)
        {
            RegisteredClient c = GetRegisteredClientByPlayerId(playerId);
            if (c != null)
                mServerSocket.SendTo(p.Data, c.EndPoint);
        }

        private RegisteredClient GetRegisteredClientByPlayerId(int playerId)
        {
            foreach (RegisteredClient c in mRegisteredClients)
                if (c.PlayerId == playerId)
                    return c;
            return null;
        }

        private RegisteredClient GetRegisteredClientByEndPoint(EndPoint endPoint)
        {
            foreach (RegisteredClient c in mRegisteredClients)
                if (c.EndPoint == endPoint)
                    return c;
            return null;
        }

        protected virtual void OnPacketRecieved(PacketEventArgs e)
        {
            // Trigger event for listeners on the ServerSocket class
            if (PacketRecieved != null)
                PacketRecieved(this, e);
        }
    }
}
