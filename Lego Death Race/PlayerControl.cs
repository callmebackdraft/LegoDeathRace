using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lego_Death_Race
{
    public partial class PlayerControl : UserControl
    {
        List<int> mLapTimes = new List<int>();
        public PlayerControl()
        {
            InitializeComponent();
        }

        public void AddLapTime(int time)
        {
            mLapTimes.Add(time);
            if (mLapTimes.Count == 1)
                lblLaptimesTemplate.Text = "Lap 1: " + time;
            else
            {
                Label l = new Label();
                l.Font = lblLaptimesTemplate.Font;
                l.AutoSize = false;
                l.Size = lblLaptimesTemplate.Size;
                l.Location = new Point(lblLaptimesTemplate.Location.X, (lblLaptimesTemplate.Location.Y - lblLaptimesTemplate.Size.Height) + (lblLaptimesTemplate.Size.Height * mLapTimes.Count));
                l.Text = "Lap " + mLapTimes.Count + ": " + time;
                groupBox1.Controls.Add(l);
            }
        }
    }
}
