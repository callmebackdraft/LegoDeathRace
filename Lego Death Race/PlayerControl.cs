using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using SharpDX.XInput;
using EV3MessengerLib;

namespace Lego_Death_Race
{
    public partial class PlayerControl : UserControl
    {
        private List<int> mLapTimes = new List<int>();
        private List<int> mPowerUpCount = new List<int>();
        public bool mGameRunning = true;      // This is set to false when this control is destroyed or by the parent. This is the condition in the read out controller thread.
        private int mPlayerId;
        private Controller mController;
        private EV3Messenger mEV3Messenger;
        private string mComPort;

        // Constructor
        public PlayerControl()
        {
            InitializeComponent();
        }

        // Destructor
        ~PlayerControl()
        {
            // Set mControlAlive to false, so running threads get destroyed
            mGameRunning = false;
        }

        public void InitPlayer(int playerId, string comPort, string playerName)
        {
            mPlayerId = playerId;
            mComPort = comPort;
            // Reset this form
            ResetPlayer();
            // Set the name of the player
            tboxName.Text = playerName;
            // Init the controller
            InitController();
            connectToBrick();
        }

        #region Controller
        private void InitController()
        {
            // Create controller instance
            switch (mPlayerId)
            {
                case 0:
                    mController = new Controller(SharpDX.XInput.UserIndex.One);
                    break;
                case 1:
                    mController = new Controller(SharpDX.XInput.UserIndex.Two);
                    break;
                case 2:
                    mController = new Controller(SharpDX.XInput.UserIndex.Three);
                    break;
                case 3:
                    mController = new Controller(SharpDX.XInput.UserIndex.Four);
                    break;
            }
            // Start the controller handler thread
            new Thread(new ThreadStart(Xbox360ControllerThread)).Start();
        }

        private void Xbox360ControllerThread()
        {
            while (mGameRunning)
            {
                SetControllerConnected(mController.IsConnected);
                if(mController.IsConnected)
                    ReadOutController();
                Thread.Sleep(10);
            }
        }

        private void ReadOutController()
        {
                State stateNew = mController.GetState();
                string btnPressed = stateNew.Gamepad.Buttons.ToString();
                if (btnPressed.Contains("A"))
                {
                    Console.WriteLine("YAAAAY A PRESSED");
                    sendEV3Message("","");
                }
                else if (btnPressed.Contains("B"))
                {
                    Console.WriteLine("YAAAAY B PRESSED");
                    sendEV3Message("", "");
                }
                else if (btnPressed.Contains("X"))
                {
                    Console.WriteLine("YAAAAY X PRESSED");
                    sendEV3Message("", "");
                }
                else if (btnPressed.Contains("Y"))
                {
                    Console.WriteLine("YAAAAY Y PRESSED");
                    sendEV3Message("", "");
                }
                else if (btnPressed.Contains("RightShoulder"))
                {
                    Console.WriteLine("YAAAAY RightShoulder PRESSED");
                    sendEV3Message("", "");
                }
                else if (btnPressed.Contains("LeftShoulder"))
                {
                    Console.WriteLine("YAAAAY LeftShoulder PRESSED");
                    sendEV3Message("", "");
                }
                if (stateNew.Gamepad.LeftTrigger >= 100)
                {
                    Console.WriteLine("REVERSE!!!!!!");
                    sendEV3Message("Move", "Backward");
                }
                else if (stateNew.Gamepad.RightTrigger >= 100)
                {
                    Console.WriteLine("FORWARD!!!!!!");
                    sendEV3Message("Move", "Forward");
                }
                else if (stateNew.Gamepad.RightTrigger <= 100 && stateNew.Gamepad.LeftTrigger <= 100)
                {
                    //Console.WriteLine("STOP!!!!!");
                    sendEV3Message("Move", "Stop");
                }


                if (btnPressed.Contains("DPadUp"))
                {
                    Console.WriteLine("UP!!!!");
                    sendEV3Message("", "");
                }
                else if (btnPressed.Contains("DPadDown"))
                {
                    Console.WriteLine("DOWN!!!!");
                    sendEV3Message("", "");
                }
                else if (btnPressed.Contains("DPadLeft"))
                {
                    Console.WriteLine("LEFT!!!!");
                    sendEV3Message("Turn", "Left");
                }
                else if (btnPressed.Contains("DPadRight"))
                {
                    Console.WriteLine("RIGHT!!!!");
                    sendEV3Message("Turn", "Right");  
                }
                else
                {
                    sendEV3Message("Turn", "Stop");
                }
            //Vibration v;
            //v.LeftMotorSpeed = ushort.MaxValue;
            //v.RightMotorSpeed = ushort.MaxValue;
            //mController.SetVibration(v);
        }
        #endregion
        #region EV3

        private void connectToBrick()
        {
            mEV3Messenger = new EV3Messenger();
            mEV3Messenger.Connect(mComPort);
        }
        
        private void sendEV3Message(string header, string message)
        {
            if (mEV3Messenger.IsConnected)
            {
                mEV3Messenger.SendMessage(header, message);
            }
        }

        private void receiveEV3Message()
        {
            
            if (mEV3Messenger.IsConnected)
            {
                EV3Message message = mEV3Messenger.ReadMessage();
                if (message != null)
                {
                    if (message.MailboxTitle == "Message")
                    {
                        //custom messages;
                    }
                    else if (message.MailboxTitle == "Speed")
                    {
                        SetCurrentSpeed(Convert.ToInt16(message.ValueAsNumber));
                    }
                    else if (message.MailboxTitle == "Tag")
                    {
                        if (message.ToString() == "Finish")
                        {
                            //finish sequence
                        }
                        else SetPowerUp(Convert.ToInt16(message.ValueAsNumber));
                    }
                }
            }
        }






        #endregion
        #region GUI related stuff       - Will clean this up later
        public void AddLapTime(int time)
        {
            if (lboxLapTimes.InvokeRequired)
            {
                lboxLapTimes.Invoke((MethodInvoker)delegate
                {
                    AddLapTime(time);
                });
            }
            else
            {
                mLapTimes.Add(time);
                lboxLapTimes.Items.Add("Lap " + mLapTimes.Count + ": " + time);
            }
        }

        public void SetRank(int rank)
        {
            if(lblRank.InvokeRequired)
            {
                lblRank.Invoke((MethodInvoker)delegate
                {
                    SetRank(rank);
                });
            }
            else
                lblRank.Text = rank.ToString();
        }

        public void SetPowerUp(int powerUp)
        {
            if(pboxPowerup.InvokeRequired)
            {
                pboxPowerup.Invoke((MethodInvoker)delegate
                {
                    SetPowerUp(powerUp);
                });
            }
            else
            {
                Image img = null;
                switch(powerUp)
                {
                    case 0:
                        img = Properties.Resources.icon_powerup_minigun;
                        break;
                    case 1:
                        img = Properties.Resources.icon_powerup_speedup;
                        break;
                    case 2:
                        img = Properties.Resources.icon_slowdown;
                        break;
                }
                pboxPowerup.Image = img;
            }
        }

        public void SetCurrentLapNumber(int lapNumber)
        {
            if (lblCurrentLap.InvokeRequired)
            {
                lblCurrentLap.Invoke((MethodInvoker)delegate
                {
                    SetCurrentLapNumber(lapNumber);
                });
            }
            else
                lblCurrentLap.Text = lapNumber.ToString();
        }

        public void SetFastestSpeed(int speed)
        {
            if (lblFastestSpeed.InvokeRequired)
            {
                lblFastestSpeed.Invoke((MethodInvoker)delegate
                {
                    SetFastestSpeed(speed);
                });
            }
            else
                lblFastestSpeed.Text = speed.ToString();
        }

        public void SetCurrentSpeed(int speed)
        {
            if (lblCurrentSpeed.InvokeRequired)
            {
                lblCurrentSpeed.Invoke((MethodInvoker)delegate
                {
                    SetCurrentSpeed(speed);
                });
            }
            else
                lblCurrentSpeed.Text = speed.ToString();
        }

        public void SetCurrentLapTime(int time)
        {
            if (lblCurrentLapTime.InvokeRequired)
            {
                lblCurrentLapTime.Invoke((MethodInvoker)delegate
                {
                    SetCurrentLapTime(time);
                });
            }
            else
                lblCurrentLapTime.Text = time.ToString();
        }

        public void SetFastestLapTime(int time)
        {
            if (lblFastestLapTime.InvokeRequired)
            {
                lblFastestLapTime.Invoke((MethodInvoker)delegate
                {
                    SetFastestLapTime(time);
                });
            }
            else
                lblFastestLapTime.Text = time.ToString();
        }

        public void IncrementPowerUpCount(int powerUp)
        {
            Label l = GetLabelByName("lblPowerUpCount" + powerUp);
            if(l.InvokeRequired)
            {
                l.Invoke((MethodInvoker)delegate
                {
                    IncrementPowerUpCount(powerUp);
                });
            }
            else
            {
                mPowerUpCount[powerUp]++;
                l.Text = mPowerUpCount[powerUp].ToString();
            }
        }

        /*private void CT_SetLabel(Label l, string text)
        {
            if (l.InvokeRequired)
            {
                l.Invoke((MethodInvoker)delegate
                {
                    CT_SetLabel(l, text);
                });
            }
            else
                l.Text = text;
        }*/

        private Label GetLabelByName(string name)
        {
            foreach (Control c in groupBox1.Controls)
                if (c is Label && c.Name == name)
                    return (Label)c;
            return null;
        }

        private void ResetPlayer()
        {
            // Reset rank
            SetRank(0);
            // Reset current lap and current and fastest lap time
            SetCurrentLapNumber(1);
            SetCurrentLapTime(0);
            SetFastestLapTime(0);
            // Reset current and fastest speed
            SetFastestSpeed(0);
            SetCurrentSpeed(0);
            // Reset powerups
            for (int i = 0; i < 3; i++)
            {
                mPowerUpCount.Add(-1);
                IncrementPowerUpCount(i);
            }
        }

        private void SetControllerConnected(bool connected)
        {
            if(pboxControllerConnected.InvokeRequired)
            {
                pboxControllerConnected.Invoke((MethodInvoker)delegate
                {
                    SetControllerConnected(connected);
                });
            }
            else
            {
                if (connected)
                    pboxControllerConnected.Image = Properties.Resources.icon_controller_connected;
                else
                    pboxControllerConnected.Image = Properties.Resources.icon_controller_disconnected;
            }
        }
        private void SetCarConnected(bool connected)
        {
            if (pboxCarConnected.InvokeRequired)
            {
                pboxCarConnected.Invoke((MethodInvoker)delegate
                {
                    SetCarConnected(connected);
                });
            }
            else
            {
                if (connected)
                    pboxCarConnected.Image = Properties.Resources.icon_controller_connected;
                else
                    pboxCarConnected.Image = Properties.Resources.icon_controller_disconnected;
            }
        }
        #endregion
    }
}
