using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace Lego_Death_Race
{
    public partial class Main : Form
    {
        private Thread mMainThread = null, mElapsedTimeUpdater = null;
        private DateTime mStartTime;
        List<PlayerControl> mPlayers = new List<PlayerControl>();
        private bool mGameRunning = false;
        public Main()
        {
            InitializeComponent();
        }

        public void Init(int playerCount, List<string> playerNames, List<string> ev3ComPorts)
        {
            // Create PlayerControls and add them to the form               - Assuming a max of 4 players
            for (int i = 0; i < playerCount; i++)
            {
                PlayerControl p = new PlayerControl();
                int yOffset = pnlSeperator.Location.Y + pnlSeperator.Size.Height;
                p.Location = new Point(i * p.Size.Width, yOffset);
                mPlayers.Add(p);
                this.Controls.Add(p);
                p.InitPlayer(i, ev3ComPorts[i], playerNames[i]);
            }
            // Resize the window so the PlayerControls fits in perfectly    - Assuming a max of 4 playres

            this.ClientSize = new Size(playerCount * mPlayers[0].Size.Width, mPlayers[0].Location.Y + mPlayers[0].Size.Height);
            // Span the seperator over the entire width of this form
            pnlSeperator.Size = new Size(this.ClientSize.Width, pnlSeperator.Height);
            BlackOutGui(true);
        }

        private void BlackOutGui(bool blackOut)
        {
            if (blackOut)
                pnlSeperator.Size = new Size(pnlSeperator.Size.Width, this.ClientSize.Height - pnlSeperator.Location.Y);
            else
                pnlSeperator.Size = new Size(pnlSeperator.Size.Width, 2);
        }

        private void btnStartRace_Click(object sender, EventArgs e)
        {
            mMainThread = new Thread(new ThreadStart(MainThread));
            mMainThread.Start();
        }

        private void btnQuitRace_Click(object sender, EventArgs e)
        {
            //BlackOutGui(false);
            if (mGameRunning)
            {
                mGameRunning = false;
                // Set this var to false in the playercontrols aswell. This will stop the threads for controllers, EV3s, ect.
                foreach (PlayerControl p in mPlayers)
                    p.mGameRunning = false;
            }
            else
                this.Close();
        }

        private void MainThread()
        {
            // Disable the start button
            CT_SetStartButtonEnabled(false);
            // Black out the GUI
            CT_BlackOutGui(true);
            // Create countdown label
            Label l = new Label();
            l.Location = new Point(0, 0);
            l.Size = pnlSeperator.Size;
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.ForeColor = Color.Yellow;
            l.Font = new Font("Terminator Two", 128);
            //pnlSeperator.Controls.Add(l);
            CT_AddCountDownLabel(l);
            // Start counting down
            CT_SetCountDownLabelText("5");
            SoundPlayer sp = new SoundPlayer(Properties.Resources.FIVE);
            sp.Play();
            Thread.Sleep(1000);
            CT_SetCountDownLabelText("4");
            sp = new SoundPlayer(Properties.Resources.FOUR);
            sp.Play();
            Thread.Sleep(1000);
            CT_SetCountDownLabelText("3");
            sp = new SoundPlayer(Properties.Resources.THREE);
            sp.Play();
            Thread.Sleep(1000);
            CT_SetCountDownLabelText("2");
            sp = new SoundPlayer(Properties.Resources.TWO);
            sp.Play();
            Thread.Sleep(1000);
            CT_SetCountDownLabelText("1");
            sp = new SoundPlayer(Properties.Resources.ONE);
            sp.Play();
            Thread.Sleep(1000);
            CT_SetCountDownLabelText("GO!");
            sp = new SoundPlayer(Properties.Resources.GO);
            sp.Play();
            Thread.Sleep(1000);
            // Remove countdown label and show the gui
            //pnlSeperator.Controls.RemoveAt(0);
            CT_RemoveCountDownLabel();
            //BlackOutGui(false);
            CT_BlackOutGui(false);

            // Race has started
            // Set game running var to true
            mGameRunning = true;
            foreach (PlayerControl i in mPlayers)
            {
                i.sendEV3Message("Race","Go");
            }
            // Save the start time
            mStartTime = DateTime.Now;
            mElapsedTimeUpdater = new Thread(new ThreadStart(UpdateElapsedTimeThread));
            mElapsedTimeUpdater.Start();
            // Enable quit button
            CT_SetQuitButtonEnabled(true);
        }

        #region Threads
        private void UpdateElapsedTimeThread()
        {
            while (mGameRunning)
            {
                TimeSpan ts = DateTime.Now.Subtract(mStartTime);
                CT_UpdateTimeElapsedLabel(ts.ToString(@"mm\:ss\:ff"));
                Thread.Sleep(10);
            }
        }
        #endregion
        #region Crossthread functions
        private void CT_SetStartButtonEnabled(bool enabled)
        {
            if (btnStartRace.InvokeRequired)
            {
                btnStartRace.Invoke((MethodInvoker)delegate
                {
                    CT_SetStartButtonEnabled(enabled);
                });
            }
            else
                btnStartRace.Enabled = enabled;
        }
        private void CT_BlackOutGui(bool enabled)
        {
            if (pnlSeperator.InvokeRequired)
            {
                pnlSeperator.Invoke((MethodInvoker)delegate
                {
                    CT_BlackOutGui(enabled);
                });
            }
            else
                BlackOutGui(enabled);
        }
        private void CT_AddCountDownLabel(Label l)
        {
            if (pnlSeperator.InvokeRequired)
            {
                pnlSeperator.Invoke((MethodInvoker)delegate
                {
                    CT_AddCountDownLabel(l);
                });
            }
            else
                pnlSeperator.Controls.Add(l);
        }
        private void CT_RemoveCountDownLabel()
        {
            if (pnlSeperator.InvokeRequired)
            {
                pnlSeperator.Invoke((MethodInvoker)delegate
                {
                    CT_RemoveCountDownLabel();
                });
            }
            else
                pnlSeperator.Controls.RemoveAt(0);
        }
        private void CT_SetCountDownLabelText(string text)
        {
            // Get Label control
            Label l = (Label)pnlSeperator.Controls[0];  // It's the first and only control on the panel
            if (l.InvokeRequired)
            {
                l.Invoke((MethodInvoker)delegate
                {
                    CT_SetCountDownLabelText(text);
                });
            }
            else
                l.Text = text;
        }
        private void CT_UpdateTimeElapsedLabel(string text)
        {
            if (lblTimeElapsed.InvokeRequired)
            {
                lblTimeElapsed.Invoke((MethodInvoker)delegate
                {
                    CT_UpdateTimeElapsedLabel(text);
                });
            }
            else
                lblTimeElapsed.Text = text;
        }
        private void CT_SetQuitButtonEnabled(bool enabled)
        {
            if (btnQuitRace.InvokeRequired)
            {
                btnQuitRace.Invoke((MethodInvoker)delegate
                {
                    CT_SetQuitButtonEnabled(enabled);
                });
            }
            else
                btnQuitRace.Enabled = enabled;
        }
        #endregion
    }
}
