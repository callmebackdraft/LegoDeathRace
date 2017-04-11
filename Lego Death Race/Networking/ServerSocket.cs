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
        public int PlayerId { get; set; }
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
        private Thread mReceiver = null;

        //public List<ConnectionToBrick> mConnectedBricks = new List<ConnectionToBrick>();
        private int mPlayerCount;

        private List<RegisteredClient> mRegisteredClients = new List<RegisteredClient>();

        public List<RegisteredClient> RegisteredClients { get { return mRegisteredClients; } }

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

            mReceiver = new Thread(new ThreadStart(ReceivePackets));
            mReceiver.Start();
        }

        public void Close()
        {
            if(mServerSocket != null)
                mServerSocket.Close();
            mServerSocket = null;
            // Strap dinamite to the thread, might be blocked because 'mServerSocket.ReceiveFrom(buffer, ref remote);'
            if (mReceiver != null)
                mReceiver.Abort();
        }

        private void ReceivePackets()
        {
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remote = (EndPoint)(sender);
            byte[] buffer = new byte[256];
            while (mServerSocket != null)
            {
                int recv = mServerSocket.ReceiveFrom(buffer, ref remote);
                RegisteredClient c = GetRegisteredClientByEndPoint(remote);
                // Add client to list if it's not already
                if (c == null)
                {

                    c = new RegisteredClient(remote);
                    RegisteredClient c2 = GetRegisteredClientByPlayerId(c.PlayerId);
                    if (c2 != null)
                        mRegisteredClients.Add(c);
                    else
                        c = c2;
                }
                c.ResetLastTimeAccessed();
                OnPacketRecieved(new PacketEventArgs() { PlayerId = c.PlayerId, Packet = new Packet(buffer) });
            }
            Console.Write("Stopped receiving packets");
        }

        public void SendPacket(int playerId, Packet p)
        {
            RegisteredClient c = GetRegisteredClientByPlayerId(playerId);
            if (c != null && mServerSocket != null)
                mServerSocket.SendTo(p.Data, c.EndPoint);
        }

        public bool IsPlayerConnected(int playerId)
        {
            RegisteredClient c = GetRegisteredClientByPlayerId(playerId);
            if (c == null)
                return false;
            if(!c.Connected)    // If car ain't connected anymore, remove it from the list
            {
                int index = GetRegisteredClientIndexByPlayerId(c.PlayerId);
                if (index >= 0)
                    mRegisteredClients.RemoveAt(index);
            }
            return c.Connected;
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

        private int GetRegisteredClientIndexByPlayerId(int playerId)
        {
            for (int i = 0; i < mRegisteredClients.Count; i++)
                if (mRegisteredClients[i].PlayerId == playerId)
                    return i;
            return -1;
        }

        protected virtual void OnPacketRecieved(PacketEventArgs e)
        {
            // Trigger event for listeners on the ServerSocket class
            if (PacketRecieved != null)
                PacketRecieved(this, e);
        }
    }
}
