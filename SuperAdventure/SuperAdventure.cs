using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Engine;
namespace SuperAdventure
{
    public partial class SuperAdventure : Form
    {
        private Player player;
        public SuperAdventure()
        {
            InitializeComponent();

            player = new Player(10, 10, 20, 0, 1);

            Location location = new Location(1, "Home", "This is your house.");

            lblHitPoints.Text = player.m_currentHitPoints.ToString();
            lblGold.Text = player.m_gold.ToString();
            lblExperience.Text = player.m_experiencePoints.ToString();
            lblLevel.Text = player.m_level.ToString();
        }

        private void SuperAdventure_Load(object sender, EventArgs e)
        {

        }

        private void HitPoints_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
