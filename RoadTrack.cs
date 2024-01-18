using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jeepney_Rush
{
    internal class RoadTrack
    {
        public PictureBox SetRoadTrack1()
        {
            PictureBox roadTrack1 = new PictureBox();
            roadTrack1.Name = "roadtrack1";
            roadTrack1.Location = new Point(0, -579);
            roadTrack1.Image = Resources.bloodyBlackRoad;
            roadTrack1.Size = new Size(613, 578);
            roadTrack1.Tag = "roadTrack1";

            return roadTrack1;
        }

        public PictureBox SetRoadTrack2()
        {
            PictureBox roadTrack2 = new PictureBox();
            roadTrack2.Name = "roadtrack2";
            roadTrack2.Location = new Point(0, 0);
            roadTrack2.Image = Resources.bloodyBlackRoad;
            roadTrack2.Size = new Size(613, 578);
            roadTrack2.Tag = "roadTrack2";
            return roadTrack2;
        }
    }
}
