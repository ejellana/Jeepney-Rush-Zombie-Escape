using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jeepney_Rush
{
    // Custom Panel class that enables double buffering for smoother graphics rendering
    internal class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
        }
    }
    
    // GamePanel class responsible for managing the game panel and its elements
    internal class GamePanel
    {
        // Static property representing the game panel
        public static Panel game_Panel { get; set; }
        public GameLogic play = new GameLogic(); // GameLogic object for handling game logic
        public static Player player = new Player(); // Player object representing the player character

        // PictureBox objects for game elements such as awards, shields, and multipliers
        public static PictureBox award = new PictureBox();
        public static PictureBox shielD = new PictureBox();
        public static PictureBox multiPlier = new PictureBox();

        public Movement move = new Movement(); // Movement object for handling player movement

        // Constructor for initializing the game panel
        public GamePanel()
        {
            game_Panel = new Panel
            {
                BackgroundImage = Resources.blackBackground,
                Location = new Point(13, 13),
                Size = new Size(613, 577),
            };
        }

        // Method to load game elements onto the game panel
        public void LoadGame()
        {
            // Add player avatar to the game panel
            game_Panel.Controls.Add(player.Avatar);

            NPC npc = new NPC(); 
            PictureBox NPC1 = npc.SetNPC1();
            game_Panel.Controls.Add(NPC1);

            PictureBox NPC2 = npc.SetNPC2();
            game_Panel.Controls.Add(NPC2);

            PictureBox Zombie = npc.SetZombie();
            game_Panel.Controls.Add(Zombie);

            RoadTrack roadTrack = new RoadTrack();
            PictureBox roadTrack1 = roadTrack.SetRoadTrack1();
            game_Panel.Controls.Add(roadTrack1);

            PictureBox roadTrack2 = roadTrack.SetRoadTrack2();
            game_Panel.Controls.Add(roadTrack2);

            // Initialize and add game elements such as awards, shields, and multipliers
            award = InitializeAward();
            game_Panel.Controls.Add(award);

            shielD = InitializeShield();
            game_Panel.Controls.Add(shielD);

            multiPlier = InitializeMultiply();
            game_Panel.Controls.Add(multiPlier);

        }

        // Method to initialize and configure the award PictureBox
        private PictureBox InitializeAward()
        {
            PictureBox award = new PictureBox();
            award.BackColor = Color.Transparent;
            award.Location = new Point(185, 240);
            award.SizeMode = PictureBoxSizeMode.StretchImage;
            award.Size = new Size(250, 100);
            award.Visible = false;
            return award;
        }

        // Method to initialize and configure the shield PictureBox
        private PictureBox InitializeShield()
        {
            PictureBox shielD = new PictureBox();
            shielD.BackColor = Color.Transparent;
            shielD.BackgroundImage = Resources.blackBackground;
            shielD.Location = new Point(217, 210);
            shielD.SizeMode = PictureBoxSizeMode.AutoSize;
            shielD.Visible = false;

            return shielD;
        }

        // Method to initialize and configure the multiplier PictureBox
        private PictureBox InitializeMultiply()
        {
            PictureBox multiPlier = new PictureBox();
            multiPlier.BackColor = Color.Transparent;
            multiPlier.BackgroundImage = Resources.blackBackground;
            multiPlier.Location = new Point(210, 270);
            multiPlier.SizeMode = PictureBoxSizeMode.AutoSize;
            multiPlier.Visible = false;

            return multiPlier;
        }

        // Event handler for the start button click
        public void start_bttnClicked(object sender, EventArgs e)
        {
            // Reset the game and make the game panel visible
            play.resetGame();
            game_Panel.Visible = true;
            MenuPanel.mainMenu.Visible = false; // Hide the main menu

            // Set focus to the game panel and attach key event handlers
            game_Panel.Focus();
            game_Panel.KeyDown += new KeyEventHandler(move.keyisdown);
            game_Panel.KeyUp += new KeyEventHandler(move.keyisup);
        }
    }
}
