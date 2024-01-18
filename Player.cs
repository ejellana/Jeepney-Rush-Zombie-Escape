using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jeepney_Rush
{
    internal class Player 
    {
        bool goLeft, goRight; // Boolean variables to track player movement direction
        public Player() // Constructor to initialize player state
        {
            goLeft = false;
            goRight = false;
        }

        public PictureBox Avatar = new PictureBox
        {
            Name = "player",
            BackColor = Color.Transparent,
            Size = new Size(64, 115),
            Location = new Point(250, 447),
            SizeMode = PictureBoxSizeMode.StretchImage,
            Image = Resources.jeep1
        };

        public bool PlayerRight
        {
            get { return goRight; }
            set { goRight = value; }
        }

        public bool PlayerLeft
        {
            get { return goLeft; }
            set { goLeft = value; }
        }

        public PictureBox Explosion = new PictureBox
        {
            Name = "Boom",
            BackColor = Color.Transparent,
            Image = Resources.explosion,
            SizeMode = PictureBoxSizeMode.AutoSize,
        };

        public PictureBox Blood = new PictureBox
        {
            Name = "blood",
            BackColor = Color.Transparent,
            Image = Resources.blood,
            SizeMode = PictureBoxSizeMode.AutoSize,
        };

        public void PlayerCrash()
        {
            Avatar.Controls.Add(Explosion);
        }
        public void ResetCrash()
        {
            Avatar.Controls.Remove(Explosion);
        }
        public void zombieCrash()
        {
            Avatar.Controls.Add(Blood);
        }
        public void ResetZombie()
        {
            Avatar.Controls.Remove(Blood);
        }
    }

    // Movement class handling keyboard input for player movement
    class Movement
    {
        // Property to get the player object from the GamePanel
        public Player GetPlayer { get { return GamePanel.player; } }

        public void keyisdown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.A)
            {
                GetPlayer.PlayerLeft = true;
            }
            if (e.KeyCode == Keys.D)
            {
                GetPlayer.PlayerRight = true;
            }
        }

        public void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                GetPlayer.PlayerLeft = false;
            }
            if (e.KeyCode == Keys.D)
            {
                GetPlayer.PlayerRight = false;
            }

        }
    }
}
