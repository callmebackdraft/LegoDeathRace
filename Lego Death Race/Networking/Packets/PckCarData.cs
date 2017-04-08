using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lego_Death_Race.Networking.Packets
{
    public class PckCarData : Packet
    {
        public byte CurrentPowerUp { get { return Data[HEADERLENGTH]; } }
        public byte PowerUpAmmo { get { return Data[HEADERLENGTH + 1]; } }
        public float CurrentSpeed { get { return ReadFloat(HEADERLENGTH + 2); } }
        public bool PassedFinishLine { get { return Convert.ToBoolean(Data[HEADERLENGTH + 6]); } }
        public byte CollectedPowerUps_Minigun { get { return Data[HEADERLENGTH + 7]; } }
        public byte CollectedPowerUps_SpeedUp { get { return Data[HEADERLENGTH + 8]; } }
        public byte CollectedPowerUps_SlowDown { get { return Data[HEADERLENGTH + 9]; } }

        public PckCarData(byte[] data) : base(data) { }
    }
}
