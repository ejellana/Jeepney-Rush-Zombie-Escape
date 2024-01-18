using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jeepney_Rush
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainForm game = new MainForm();
            Application.Run(game);
            Console.ReadKey();
        }
    }
}
