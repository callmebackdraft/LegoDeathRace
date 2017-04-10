using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lego_Death_Race.Networking
{
    public class Packet
    {
        #region PacketTypes
        public const ushort PT_CONTROLLER_BUTTON_STATE = 0;
        public const ushort PT_CAR_DATA = 1;
        public const ushort PT_RESET_FINISHLINE_VAR = 2;
        public const ushort PT_RESET_GAME_LOGIC = 3;
        #endregion

        public const ushort HEADERLENGTH = 6;
        private byte[] _buffer;
        private bool mIsBigendian;

        public byte[] Data { get { return _buffer; } }
        public ushort Type { get { return ReadUShort(4); } }
        public ushort Length { get { return ReadUShort(2); } }

        public Packet(ushort length, ushort type)
        {
            _buffer = new byte[length];
            WriteUShort(1, 0);
            WriteUShort(length, 2);
            WriteUShort(type, 4);
        }

        public Packet(byte[] packet)
        {
            _buffer = packet;
            byte byte1 = _buffer[0];
            byte byte2 = _buffer[1];
            if (((byte1 << 8) + byte2) == 1)
                mIsBigendian = true;
            else
                mIsBigendian = false;
        }

        protected void WriteUShort(ushort value, int offset)
        {
            byte[] tempBuf = new byte[2];//ushort is 2 bytes long
            tempBuf = BitConverter.GetBytes(value);
            Buffer.BlockCopy(tempBuf, 0, _buffer, offset, 2);
        }

        protected ushort ReadUShort(int offset)
        {
            byte byte1 = _buffer[offset];
            byte byte2 = _buffer[offset + 1];
            if (mIsBigendian)
                return (ushort)((byte1 << 8) + byte2);
            else
                return (ushort)((byte2 << 8) + byte1);
        }

        protected void WriteUInt32(UInt32 value, int offset)
        {
            byte[] tempBuf = new byte[4];
            tempBuf = BitConverter.GetBytes(value);
            Buffer.BlockCopy(tempBuf, 0, _buffer, offset, 4);
        }

        protected void WriteShort(short value, int offset)
        {
            byte[] tempBuf = new byte[2];//ushort is 2 bytes long
            tempBuf = BitConverter.GetBytes(value);
            Buffer.BlockCopy(tempBuf, 0, _buffer, offset, 2);
        }

        protected float ReadFloat(int offset)
        {
            byte[] arr = new byte[4];
            if (mIsBigendian)
                for (int i = 0; i < 4; i++)
                    arr[i] = _buffer[offset + i];
            else
                for (int i = 0; i < 4; i++)
                    arr[i] = _buffer[(offset + 3) - i];

            return BitConverter.ToSingle(arr, 0);
        }
    }
}
