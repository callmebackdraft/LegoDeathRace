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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            StartGameSettings sgs = new StartGameSettings();
            if(sgs.ShowDialog() == DialogResult.OK)
            {
                // Start the game
                Main main = new Main();
                main.Init(sgs.PlayerCount, sgs.PlayerNames);
                this.Visible = false;
                main.ShowDialog();

                // Game exited
                this.Visible = true;
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }
    }
}
