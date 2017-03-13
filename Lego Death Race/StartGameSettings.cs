using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lego_Death_Race
{
    public partial class StartGameSettings : Form
    {
        public int PlayerCount { get; set; }
        public StartGameSettings()
        {
            InitializeComponent();
            PlayerCount = Convert.ToSByte(nudPlayerCount.Value);
        }

        private void nudPlayerAmount_ValueChanged(object sender, EventArgs e)
        {
            PlayerCount = Convert.ToSByte(nudPlayerCount.Value);
            if (PlayerCount == 3)
            {
                tboxPlayerName2.Enabled = true;
                tboxPlayerName3.Enabled = false;
            }
            else if (PlayerCount == 4)
            {
                tboxPlayerName2.Enabled = true;
                tboxPlayerName3.Enabled = true;
            }
            else
            {
                tboxPlayerName2.Enabled = false;
                tboxPlayerName3.Enabled = false;
            }
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
