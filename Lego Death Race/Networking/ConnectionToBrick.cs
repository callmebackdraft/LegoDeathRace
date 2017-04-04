using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace Lego_Death_Race.Networking
{
    public class ConnectedEventArgs : System.EventArgs
    {
        public bool Connected { get; set; }
    }
    public class PacketEventArgs : EventArgs
    {
        public Packet Packet { get; set; }
    }

    public class ConnectionToBrick
    {
        public delegate void PacketRecievedEventHandler(object sender, PacketEventArgs e);
        public event PacketRecievedEventHandler PacketRecieved;

        public delegate void ConnectionStatusChangedEventHandler(object sender, ConnectedEventArgs e);
        public event ConnectionStatusChangedEventHandler ConnectionStatusChanged;

        public int PlayerIndex { get; set; }

        //set vars
        private const int port = 8965;               //masterserver port

        //private IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        private Socket mSocket = null;

        private Thread mTReceiver = null, mTRequester = null;

        private byte[] _buffer = null;

       

        //public volatile bool mKeepUpdating = true;


        //constructor
        public ConnectionToBrick(Socket s)
        {
            mSocket = s;
            OnConnectionStatusChanged(s.Connected);
            string ip = ((IPEndPoint)mSocket.RemoteEndPoint).Address.ToString();
            //PlayerIndex = Convert.ToInt16(ip.Substring(ip.Length - 3, 3));
            switch (ip.Substring(ip.Length - 3, 3))
            {
                case "118":
                    PlayerIndex = 0;
                    break;
                default:
                    PlayerIndex = -1;
                    break;
            }
            StartRequestingUpdates();
            //mTMonitorSocketConnected = new Thread(Thread_UpdateSocketConnected);
            //mTMonitorSocketConnected.Start();
        }

        //deconstrutor
        ~ConnectionToBrick()
        {
            Close();
        }

        public void StartRequestingUpdates()
        {
            mTRequester = new Thread(Thread_SendDataRequests);
            mTReceiver = new Thread(Thread_ReceiveInfo);
            mTRequester.Start();
            mTReceiver.Start();
        }

        public void SendData(Packet p)
        {
            try
            {
                //if(mKeepUpdating || (!mKeepUpdating && p.Type != Packet.PT_INFOREQUEST))
                mSocket.Send(p.Data, 0, p.Length, 0);
                foreach (byte b in p.Data)
                    Console.Write((int)b + " ");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                if (!mSocket.Connected)
                    OnConnectionStatusChanged(mSocket.Connected);
            }
            //OnConnectionStatusChanged(socket.Connected);
        }

        private void Thread_SendDataRequests()
        {
            while (mSocket.Connected)
            {
            }
        }

        //retrieves info from server
        private void Thread_ReceiveInfo()
        {
            while (mSocket.Connected)
            {
                //Thread.Sleep(500);
                try
                {
                    byte[] Buffer = new byte[255];
                    int rec = mSocket.Receive(Buffer, 0, Buffer.Length, 0);
                    Array.Resize(ref Buffer, rec);
                    handleInfo(Buffer);
                }
                catch (Exception e)
                {
                    if (!mSocket.Connected)
                        OnConnectionStatusChanged(mSocket.Connected);
                }
            }
        }

        //handles retrieved info
        private void handleInfo(byte[] buffer)
        {
            string str = "[ ";
            foreach (byte b in buffer)
                str += b.ToString("X2") + " ";
            str += "]";
            //MessageBox.Show("Buffer recieved;" + Environment.NewLine + "Buffer Length: " + buffer.Length.ToString() + Environment.NewLine + str);
            Debug.WriteLine("Buffer recieved;" + Environment.NewLine + "Buffer Length: " + buffer.Length.ToString() + Environment.NewLine + str);
            if (_buffer == null)
            {
                _buffer = buffer;
            }
            else
            {
                byte[] array = new byte[_buffer.Length + buffer.Length];
                Buffer.BlockCopy(_buffer, 0, array, 0, _buffer.Length);
                Buffer.BlockCopy(buffer, 0, array, _buffer.Length, buffer.Length);
                _buffer = array;
            }

            if (_buffer.Length >= Packet.HEADERLENGTH)
            {
                Debug.WriteLine("_buffer.Length >= Packet.HEADERLENGTH");
                Packet p = new Packet(_buffer);
                //ushort length = BitConverter.ToUInt16(_buffer, 2);
                //ushort type = BitConverter.ToUInt16(_buffer, 4);
                Debug.WriteLine("Packet.length: " + p.Length);
                if (_buffer.Length >= p.Length)
                {
                    byte[] packet = new byte[p.Length];
                    if (_buffer.Length > p.Length)
                    {
                        Buffer.BlockCopy(_buffer, 0, packet, 0, packet.Length);
                        byte[] rest = new byte[_buffer.Length - packet.Length];
                        Buffer.BlockCopy(_buffer, packet.Length, rest, 0, rest.Length);
                        _buffer = rest;
                    }
                    else
                    {
                        packet = _buffer;
                        _buffer = null;
                    }
                    //MessageBox.Show("Package has fully arrived!" + Environment.NewLine + "Type: " + type + Environment.NewLine + "Length: " + length);
                    Debug.WriteLine("Packet fully arrived with type: " + new Packet(packet).Type.ToString());
                    OnPacketRecieved(new Packet(packet));


                }
            }




        }

        protected virtual void OnPacketRecieved(Packet packet)
        {
            if (PacketRecieved != null)
                PacketRecieved(this, new PacketEventArgs() { Packet = packet });
        }
        protected virtual void OnConnectionStatusChanged(bool connected)
        {
            if (!connected)
            {
                if (mTRequester != null)
                {
                    mTRequester.Abort();
                    mTRequester = null;
                }
                if (mTReceiver != null)
                {
                    mTReceiver.Abort();
                    mTReceiver = null;
                }
                //Thread.Sleep(1000);
                //Connect();
            }

            if (ConnectionStatusChanged != null)
                ConnectionStatusChanged(this, new ConnectedEventArgs() { Connected = connected });
        }

        public void Close()
        {
            //MessageBox.Show("closing");
            ConnectionStatusChanged = null; // Remove the eventhandlers, otherwise when the server is offline you'll get an error when it's trying to update the main connection panel
            if (mTRequester != null)
                mTRequester.Abort();
            if (mTReceiver != null)
                mTReceiver.Abort();
            if (mSocket != null)
            {
                //MessageBox.Show("closing socket");
                mSocket.Close();
                //mSocket = null;
            }
        }
    }
}
