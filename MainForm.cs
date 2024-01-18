using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Jeepney_Rush
{
    public class MainForm : Form
        {
            GamePanel gamePanel = new GamePanel();
            MenuPanel menuPanel = new MenuPanel();
            GameLogic gameLogic = new GameLogic();
            public static Label txtScore;
            public static Label txtHighestScore;
            public static Button bttnStart;
            public static int highestScore;

        public MainForm()
            {
                DoubleBuffered = true;
                Text = "Jeepney Rush: Zombie Escape";
                ClientSize = new Size(640, 689);
                BackColor = Color.FromArgb(64, 64, 64);
                MaximizeBox = false;
                FormBorderStyle = FormBorderStyle.FixedSingle;

                Controls.Add(MenuPanel.mainMenu);
                MenuPanel.mainMenu.Visible = true;

                gamePanel.LoadGame();
                Controls.Add(GamePanel.game_Panel);
                GamePanel.game_Panel.Visible = false;
                
                bttnStart = new Button();
                bttnStart.Font = new Font("Stencil", 18);
                bttnStart.Location = new Point(25, 620);
                bttnStart.Text = "START";
                bttnStart.Size = new Size(111, 42);
                bttnStart.BackColor = Color.White;
                Controls.Add(bttnStart);
                bttnStart.Click += gamePanel.start_bttnClicked;

                txtScore = new Label();
                Controls.Add(txtScore);
                txtScore.Font = new Font("Stencil", 18);
                txtScore.ForeColor = SystemColors.ControlLightLight;
                txtScore.AutoSize = true;
                txtScore.Location = new Point(150, 627);
                txtScore.Text = "Level 1: 0";
                txtScore.Size = new Size(129, 29);

                txtHighestScore = new Label();
                Controls.Add(txtHighestScore);
                txtHighestScore.Font = new Font("Stencil", 14);
                txtHighestScore.ForeColor = SystemColors.ControlLightLight;
                txtHighestScore.AutoSize = true;
                txtHighestScore.Location = new Point(335, 620);
                txtHighestScore.Text = "Highest\nScore: ";
                txtHighestScore.Size = new Size(129, 29);

                string filePath = "HighScore.txt";
                if (File.Exists(filePath))
                {
                    StreamReader reader = new StreamReader(filePath);
                    

                    while (!reader.EndOfStream)
                    {
                        int score = int.Parse(reader.ReadLine());
                        if (score > highestScore)
                        {
                            highestScore = score;
                        }
                    }

                    reader.Close();

                    txtHighestScore.Text = "Highest\nScore: " + highestScore;
                }
        }      
    }
}
