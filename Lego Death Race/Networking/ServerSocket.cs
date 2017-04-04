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
        private const short mPort = 24309;
        // Server socket
        private Socket mServerSocket = null;

        private List<ConnectionToBrick> mConnectedBricks = new List<ConnectionToBrick>();

        public ServerSocket(int playerCount)
        {
            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, mPort);
            mServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mServerSocket.Bind(localEndPoint);
            mServerSocket.Listen(4);

            while(true)
            {
                if (mConnectedBricks.Count < playerCount)
                {
                    Socket s = mServerSocket.Accept();
                    ConnectionToBrick ctb = new ConnectionToBrick(s);
                    ctb.ConnectionStatusChanged += Ctb_ConnectionStatusChanged;
                    mConnectedBricks.Add(ctb);
                }
            }
        }

        private void Ctb_ConnectionStatusChanged(object sender, ConnectedEventArgs e)
        {
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
