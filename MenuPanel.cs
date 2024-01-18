using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jeepney_Rush
{
    internal class MenuPanel
    {
        public static Panel mainMenu { get; set; }
        public MenuPanel()
        {
            mainMenu = new Panel
            {
                BackgroundImage = Resources.zombMenu,
                Location = new Point(13, 5),
                Size = new Size(613, 577),
                AutoSize = true
            };
        }
    }
}
