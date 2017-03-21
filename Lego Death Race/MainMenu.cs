using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
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
            if (sgs.ShowDialog() == DialogResult.OK)
            {
                // Get the EV3 COM ports
                List<string> ev3Ports = GetEv3ComPorts();
                // If we cannot find enough EV3 ports, do not start the game and give an error
                if (ev3Ports.Count < sgs.PlayerCount)
                {
                    // Start the game
                    MessageBox.Show("Er zijn niet genoeg EV3 bricks met de computer verbonden. Verbind op zijn minst 1 EV3 brick per player." + Environment.NewLine + "Er zijn momenteel " + ev3Ports.Count + " EV3 bricks verbonden.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Start the game
                    Main main = new Main();
                    main.Init(sgs.PlayerCount, sgs.PlayerNames, ev3Ports);
                    this.Visible = false;
                    main.ShowDialog();
                }

                // Game exited
                this.Visible = true;
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }

        private List<string> GetEv3ComPorts()
        {
            List<string> serialPorts = new List<string>();
            serialPorts.Add("");
            /*ManagementObjectSearcher serialSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_SerialPort");

            var query = from ManagementObject s in serialSearcher.Get()
                        select new { Name = s["Name"], DeviceID = s["DeviceID"], PNPDeviceID = s["PNPDeviceID"] }; // DeviceID -- > PNPDeviceID

            foreach (var port in query)
            {
                Console.WriteLine("{0} - {1}", port.DeviceID, port.Name);
                var pnpDeviceId = port.PNPDeviceID.ToString();

                if (pnpDeviceId.Contains("BTHENUM"))
                {
                    var bluetoothDeviceAddress = pnpDeviceId.Split('&')[4].Split('_')[0];
                    Console.WriteLine(bluetoothDeviceAddress);
                    if (bluetoothDeviceAddress.Length == 12 && bluetoothDeviceAddress != "000000000000")
                    {
                        //gets called when a com port with a BT address is found
                        serialPorts.Add(port.DeviceID.ToString());
                    }
                }
            }*/
            return serialPorts;
        }
    }
}
