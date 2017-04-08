using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lego_Death_Race.Networking.Packets
{
    public class PckCarData : Packet
    {
        public int CurrentPowerup { get { return Data[HEADERLENGTH]; } }
        public float CurrentSpeed { get { return ReadFloat(HEADERLENGTH + 1); } }
        public bool PassedFinishLine { get { return Convert.ToBoolean(Data[HEADERLENGTH + 5]); } }

        public PckCarData(byte[] data) : base(data) { }
    }
}
