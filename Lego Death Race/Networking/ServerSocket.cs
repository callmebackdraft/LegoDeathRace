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
    public class ServerSocket
    {
        public delegate void PacketRecievedEventHandler(object sender, PacketEventArgs e);
        public event PacketRecievedEventHandler BrickPacketRecieved;

        public delegate void ConnectionStatusChangedEventHandler(object sender, ConnectedEventArgs e);
        public event ConnectionStatusChangedEventHandler BrickConnectionStatusChanged;

        private const short mPort = 8965;
        // Server socket
        private Socket mServerSocket = null;

        public List<ConnectionToBrick> mConnectedBricks = new List<ConnectionToBrick>();
        private int mPlayerCount;

        public List<ConnectionToBrick> ConnectedBricks { get { return mConnectedBricks; } }

        public ServerSocket(int playerCount)
        {
            mPlayerCount = playerCount;
            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, mPort);
            mServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mServerSocket.Bind(localEndPoint);
            mServerSocket.Listen(4);

            new Thread(ListenForConnections).Start();
        }

        private void ListenForConnections()
        {
            while (true)
            {
                if (mConnectedBricks.Count < mPlayerCount)
                {
                    Socket s = mServerSocket.Accept();
                    ConnectionToBrick ctb = new ConnectionToBrick(s);
                    ctb.ConnectionStatusChanged += Ctb_ConnectionStatusChanged;
                    ctb.PacketRecieved += Ctb_PacketRecieved;
                    mConnectedBricks.Add(ctb);
                }
            }
        }

        private void Ctb_PacketRecieved(object sender, PacketEventArgs e)
        {
            // Trigger event for listeners on the ServerSocket class
            if (BrickPacketRecieved != null)
                BrickPacketRecieved(sender, e);
        }

        private void Ctb_ConnectionStatusChanged(object sender, ConnectedEventArgs e)
        {
            // Trigger event for listeners on the ServerSocket class
            if (BrickConnectionStatusChanged != null)
                BrickConnectionStatusChanged(sender, e);
            // If the socket is not connected anymore, destroy and remove it from the list, so the car can reconnect.
            if (!e.Connected)
            {
                ConnectionToBrick ctb = (ConnectionToBrick)sender;
                int iCCTB = GetIndexConnectionToBrickByPlayerIndex(ctb.PlayerIndex);
                if (iCCTB != -1)
                    mConnectedBricks.RemoveAt(iCCTB);
            }
        }

        private int GetIndexConnectionToBrickByPlayerIndex(int index)
        {
            for (int i = 0; i < mConnectedBricks.Count; i++)
                if (mConnectedBricks[i].PlayerIndex == index)
                    return i;
            return -1;
        }
    }
}
