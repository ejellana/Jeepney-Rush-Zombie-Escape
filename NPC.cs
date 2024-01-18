using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jeepney_Rush
{
    internal class NPC
    {
        public PictureBox SetNPC1()
        {
            PictureBox npc1 = new PictureBox();
            npc1.Name = "NPC1";
            npc1.BackColor = Color.Transparent;
            npc1.Size = new Size(64, 115);
            npc1.Image = Resources.car1;
            npc1.Location = new Point(114, 78);
            npc1.SizeMode = PictureBoxSizeMode.StretchImage;
            npc1.Tag = "carLeft";

            return npc1;
        }

        public PictureBox SetNPC2()
        {
            PictureBox npc2 = new PictureBox();
            npc2.Name = "NPC1";
            npc2.BackColor = Color.Transparent;
            npc2.Size = new Size(64, 115);
            npc2.Image = Resources.car2;
            npc2.Location = new Point(449, 78);
            npc2.SizeMode = PictureBoxSizeMode.StretchImage;
            npc2.Tag = "carRight";

            return npc2;
        }

        public PictureBox SetZombie()
        {
            PictureBox zombie = new PictureBox();
            zombie.Tag = "zombie";
            zombie.BackColor = Color.Transparent;
            zombie.Size = new Size(61, 64);
            zombie.Image = Resources.zombie1;
            zombie.Location = new Point(276, 78);
            zombie.SizeMode = PictureBoxSizeMode.StretchImage;

            return zombie;
        }
    }
}
