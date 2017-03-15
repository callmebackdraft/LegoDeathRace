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
    public partial class Main : Form
    {
        List<PlayerControl> mPlayers = new List<PlayerControl>();
        public Main()
        {
            InitializeComponent();
        }

        public void Init(int playerCount, List<string> PlayerNames)
        {
            // Create PlayerControls and add them to the form               - Assuming a max of 4 players
            for (int i = 0; i < playerCount; i++)
            {
                PlayerControl p = new PlayerControl();
                int yOffset = pnlSeperator.Location.Y + pnlSeperator.Size.Height;
                p.Location = new Point(i * p.Size.Width, yOffset);
                p.SetName(PlayerNames[i]);
                /*switch (i)
                {
                    case 0:
                        p.Location = new Point(0, yOffset);
                        break;
                    case 1:
                        p.Location = new Point(p.Size.Width, yOffset);
                        break;
                    case 2:
                        p.Location = new Point(0, p.Size.Height);
                        break;
                    case 3:
                        p.Location = new Point(p.Size.Width, p.Size.Height);
                        break;
                }*/
                mPlayers.Add(p);
                this.Controls.Add(p);
            }
            // Resize the window so the PlayerControls fits in perfectly    - Assuming a max of 4 playres

            this.ClientSize = new Size(playerCount * mPlayers[0].Size.Width, mPlayers[0].Location.Y + mPlayers[0].Size.Height);
            // Span the seperator over the entire width of this form
            pnlSeperator.Size = new Size(this.ClientSize.Width, pnlSeperator.Height);
        }
    }
}
