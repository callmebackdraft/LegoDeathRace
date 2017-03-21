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
        public List<string> PlayerNames { get; set; }
        public StartGameSettings()
        {
            InitializeComponent();
            changeDisplay();
        }

        private void changeDisplay()
        {
            PlayerCount = Convert.ToSByte(nudPlayerCount.Value);
            if (PlayerCount == 2)
            {
                tboxPlayerName0.Enabled = true;
                tboxPlayerName1.Enabled = false;
                tboxPlayerName2.Enabled = false;
                tboxPlayerName3.Enabled = false;
            }
            else if (PlayerCount == 3)
            {
                tboxPlayerName0.Enabled = true;
                tboxPlayerName1.Enabled = true;
                tboxPlayerName2.Enabled = true;
                tboxPlayerName3.Enabled = false;
            }
            else if (PlayerCount == 4)
            {
                tboxPlayerName0.Enabled = true;
                tboxPlayerName1.Enabled = true;
                tboxPlayerName2.Enabled = true;
                tboxPlayerName3.Enabled = true;
            }
            else
            {
                tboxPlayerName1.Enabled = false;
                tboxPlayerName2.Enabled = false;
                tboxPlayerName3.Enabled = false;
            }
        }

        private void nudPlayerAmount_ValueChanged(object sender, EventArgs e)
        {
            changeDisplay();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            PlayerNames = new List<string>();
            PlayerNames.Add(tboxPlayerName0.Text);
            PlayerNames.Add(tboxPlayerName1.Text);
            if(PlayerCount > 2)
                PlayerNames.Add(tboxPlayerName2.Text);
            if(PlayerCount > 3)
                PlayerNames.Add(tboxPlayerName3.Text);
            this.DialogResult = DialogResult.OK;
        }
    }
}
