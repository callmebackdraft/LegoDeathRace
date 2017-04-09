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

namespace Lego_Death_Race
{
    public partial class PlayerControl : UserControl
    {
        private List<DateTime> mLapTimes = new List<DateTime>();
        //private List<int> mPowerUpCount = new List<int>();
        public bool mGameRunning = true;      // This is set to false when this control is destroyed or by the parent. This is the condition in the read out controller thread.
        //private int mPlayerId;
        public int PlayerId { get; set; }
        private Controller mController;
        public bool ControllerConnected { get { return mController.IsConnected; } }
        public State ControllerState { get { return mController.GetState(); } }
        public int LapCount { get { return mLapTimes.Count - 1; } } // We do minus one because the first added DateTime is the start time.
        public TimeSpan CurrentLapTime { get { return mLapTimes.Count > 0 ? DateTime.Now.Subtract(mLapTimes[mLapTimes.Count - 1]) : new TimeSpan(); } }
        private float mTopSpeed = 0;
        public bool mFinishLineVarOnCar = false;

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

        public void InitPlayer(int playerId, string playerName)
        {
            //mPlayerId = playerId;
            PlayerId = playerId;
            // Reset this form
            ResetPlayer();
            // Set the name of the player
            tboxName.Text = playerName;
            // Init the controller
            InitController();
        }

        

        #region Controller
        private void InitController()
        {
            // Create controller instance
            switch (PlayerId)
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
                Thread.Sleep(10);
            }
        }
        #endregion
        #region GUI related stuff
        #region Lap Times and lapnumber
        public void AddLapTime(DateTime dt)
        {
            if (lboxLapTimes.InvokeRequired)
            {
                lboxLapTimes.Invoke((MethodInvoker)delegate
                {
                    AddLapTime(dt);
                });
            }
            else
            {
                mLapTimes.Add(dt);
                if (mLapTimes.Count > 1)
                {
                    lboxLapTimes.Items.Add("Lap " + (mLapTimes.Count - 1) + ": " + mLapTimes[mLapTimes.Count - 1].Subtract(mLapTimes[mLapTimes.Count - 2]).ToString(@"mm\:ss\.ff"));
                    SetFastestLapTime(GetFastestLapTime());
                    SetCurrentLapNumber(LapCount + 1);  // Here we do plus one because otherwise we would be showing the lapnumber of the last lap and not the current one.
                }
            }
        }

        public void SetCurrentLapTime()
        {
            if (lblCurrentLapTime.InvokeRequired)
            {
                lblCurrentLapTime.Invoke((MethodInvoker)delegate
                {
                    SetCurrentLapTime();
                });
            }
            else
            {
                lblCurrentLapTime.Text = CurrentLapTime.ToString(@"mm\:ss\.ff");
                if (mLapTimes.Count < 2) // First lap, wwe want the fastest laptime to be the same as to current laptime
                    SetFastestLapTime(CurrentLapTime);
            }
        }

        private TimeSpan GetFastestLapTime()    // This method may only be called when there are at least two items in the mLapTimes List
        {
            TimeSpan fastestTime = new TimeSpan(), temp;
            for (int i = 1; i < mLapTimes.Count; i++)
            {
                if (i == 1)
                    fastestTime = mLapTimes[i].Subtract(mLapTimes[i - 1]);
                else
                {
                    temp = mLapTimes[i].Subtract(mLapTimes[i - 1]);
                    if (fastestTime > temp)
                        fastestTime = temp;
                }
            }
            return fastestTime;
        }

        private void SetFastestLapTime(TimeSpan ts)
        {
            if (lblFastestLapTime.InvokeRequired)
            {
                lblFastestLapTime.Invoke((MethodInvoker)delegate
                {
                    SetFastestLapTime(ts);
                });
            }
            else
                lblFastestLapTime.Text = ts.ToString(@"mm\:ss\.ff");
        }

        private void SetCurrentLapNumber(int lapNumber)
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
        #endregion
        #region Max and current speed
        public void SetCurrentSpeed(float speed)
        {
            if (lblCurrentSpeed.InvokeRequired)
            {
                lblCurrentSpeed.Invoke((MethodInvoker)delegate
                {
                    SetCurrentSpeed(speed);
                });
            }
            else
            {
                lblCurrentSpeed.Text = speed.ToString("n2") + " Km/U";
                if (speed > mTopSpeed)
                {
                    mTopSpeed = speed;
                    SetFastestSpeed(speed);
                }
            }
        }
        private void SetFastestSpeed(float speed)
        {
            if (lblFastestSpeed.InvokeRequired)
            {
                lblFastestSpeed.Invoke((MethodInvoker)delegate
                {
                    SetFastestSpeed(speed);
                });
            }
            else
                lblFastestSpeed.Text = speed.ToString("n2") + " Km/U";
        }
        #endregion
        #region Power ups
        public void SetPowerUp(int powerUp)
        {
            if (pboxPowerup.InvokeRequired)
            {
                pboxPowerup.Invoke((MethodInvoker)delegate
                {
                    SetPowerUp(powerUp);
                });
            }
            else
            {
                Image img = null;
                switch (powerUp)
                {
                    case 0:
                        img = null;
                        break;
                    case 1:
                        img = Properties.Resources.icon_powerup_minigun;
                        break;
                    case 2:
                        img = Properties.Resources.icon_powerup_speedup;
                        break;
                    case 3:
                        img = Properties.Resources.icon_slowdown;
                        break;
                }
                pboxPowerup.Image = img;
            }
        }

        public void SetPowerUpCount(int powerUp, int count)
        {
            Label l = GetLabelByName("lblPowerUpCount" + powerUp);
            if (l.InvokeRequired)
            {
                l.Invoke((MethodInvoker)delegate
                {
                    SetPowerUpCount(powerUp, count);
                });
            }
            else
            {
                l.Text = count.ToString();
            }
        }

        public void SetPowerUpAmmo(int value)
        {
            if(pbarPowerUpAmmo.InvokeRequired)
            {
                pbarPowerUpAmmo.Invoke((MethodInvoker)delegate
                {
                    SetPowerUpAmmo(value);
                });
            }
            else
            {
                if(value < 1)
                {
                    pbarPowerUpAmmo.Visible = false;
                }
                else
                {
                    pbarPowerUpAmmo.Visible = true;
                    pbarPowerUpAmmo.Value = value;
                }
            }
        }
        #endregion
        #region REST
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
            //SetCurrentLapTime(0);
            //SetFastestLapTime(0);
            // Reset current and fastest speed
            SetFastestSpeed(0);
            SetCurrentSpeed(0);
            // Reset powerups
            for (int i = 0; i < 3; i++)
            {
                SetPowerUpCount(i, 0);
            }
            // Reset connection statusses
            SetCarConnected(false);
            SetControllerConnected(false);
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
        public void SetCarConnected(bool connected)
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
                    pboxCarConnected.Image = Properties.Resources.icon_car_connected;
                else
                    pboxCarConnected.Image = Properties.Resources.icon_car_disconnected;
            }
        }
        #endregion
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            AddLapTime(DateTime.Now);
        }
    }
}
