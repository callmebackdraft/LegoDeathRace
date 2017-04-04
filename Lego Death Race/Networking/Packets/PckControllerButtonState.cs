using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lego_Death_Race.Networking.Packets
{
    public class PckControllerButtonState : Packet
    {
        public PckControllerButtonState(State controllerState) : base(HEADERLENGTH + 6, PT_CONTROLLER_BUTTON_STATE)
        {
            WriteUInt32((UInt32)controllerState.Gamepad.Buttons, HEADERLENGTH);
            Data[HEADERLENGTH + 4] = controllerState.Gamepad.LeftTrigger;
            Data[HEADERLENGTH + 5] = controllerState.Gamepad.RightTrigger;
        }
    }
}
