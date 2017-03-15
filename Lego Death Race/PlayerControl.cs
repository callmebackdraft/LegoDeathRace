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
        private List<int> mLapTimes = new List<int>();
        public PlayerControl()
        {
            InitializeComponent();
        }

        public void SetName(string name)
        {
            tboxName.Text = name;
        }

        public void AddLapTime(int time)
        {
            mLapTimes.Add(time);
            lboxLapTimes.Items.Add("Lap " + mLapTimes.Count + ": " + time);
        }

        public void SetRank(int rank)
        {
            lblRank.Text = rank.ToString();
        }
    }
}
