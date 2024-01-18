using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jeepney_Rush
{
    internal class GameLogic
    {
        // Variables to manage game state
        int score;
        int multiplier = 8;
        private int bonusPointsTimer, levelValue, immunityTimer, zomCounter;
        PictureBox boom = new PictureBox();
        PictureBox Blood = new PictureBox();
        public Panel ThisPanel = GamePanel.game_Panel;
        public Player GetPlayer { get { return GamePanel.player; } }
        int playerSpeed = 12, roadSpeed = 12, trafficSpeed = 15, carImage, zombImage;
        bool isImmune;
        Random carPosition = new Random();
        Random rand = new Random();
        public Timer gameTimer = new Timer();

        // Method to start the game
        public void StartGame()
        {
            MainForm.bttnStart.Enabled = false;
            gameTimer.Enabled = true;
            gameTimer.Interval = 20;
            gameTimer.Tick += GameTimerEvent;
            gameTimer.Start();
        }

        // Event handler for the game timer
        public void GameTimerEvent(object sender, EventArgs e)
        {

            // Game logic goes here!

            // Check and update game level
            levelValue++;          
            MainForm.txtScore.Text = "LEVEL 1: " + score; // Display current score and level in the UI
            if (bonusPointsTimer > 0) // Update score based on game conditions
            {
                score += multiplier;
                levelValue++;
            }
            else
            {
                score++;
                levelValue++;
            }
            if (score > MainForm.highestScore) // Update highest score if applicable
            {
                MainForm.highestScore = score+1;                
                MainForm.txtHighestScore.Text = "Highest\nScore: " + MainForm.highestScore;
            }

            // Player movement logic...

            if (GetPlayer.PlayerLeft && GetPlayer.Avatar.Left > 76)
            {
                GetPlayer.Avatar.Left -= playerSpeed;
            }

            if (GetPlayer.PlayerRight && GetPlayer.Avatar.Right < 530)
            {
                GetPlayer.Avatar.Left += playerSpeed;
            }

            // Iterating through game elements in the panel
            foreach (PictureBox item in ThisPanel.Controls)
            {
                if (item is PictureBox)
                {
                    // Road and NPC movement logic...
                    if ((string)item.Tag == "roadTrack1")
                    {
                        item.Top += roadSpeed;
                        if (item.Top > 525)
                        {
                            item.Top = -525;
                        }
                    }
                    if ((string)item.Tag == "roadTrack2")
                    {
                        item.Top += roadSpeed;
                        if (item.Top > 525)
                        {
                            item.Top = -525;
                        }
                    }

                    // Collision detection logic for player and obstacles...
                    if ((string)item.Name == "NPC1")
                    {
                        item.Top += trafficSpeed;
                        if (item.Top > 525)
                        {
                            changeNPCcars(item);
                        }
                        if (GetPlayer.Avatar.Bounds.IntersectsWith(item.Bounds) && !isImmune)
                        {
                            gameOver();
                        }
                    }
                    if ((string)item.Name == "NPC2")
                    {
                        item.Top += trafficSpeed;
                        if (item.Top > 525)
                        {
                            changeNPCcars(item);
                        }
                        if (GetPlayer.Avatar.Bounds.IntersectsWith(item.Bounds) && !isImmune)
                        {
                            gameOver();
                        }
                    }
                    // Zombie logic...
                    if ((string)item.Tag == "zombie")
                    {
                        item.Top += trafficSpeed;
                        if (item.Top > 525)
                        {
                            changeZombie(item);
                        }
                        if (GetPlayer.Avatar.Bounds.IntersectsWith(item.Bounds))
                        {
                            GetPlayer.zombieCrash();                                                     
                            zombSound();
                            zomCounter++;
                            // Bonus points logic...
                            if (zomCounter == 40)
                            {
                                zomCounter = 0;
                                Random random = new Random();
                                int randomNumber = random.Next(1, 3);
                                if (randomNumber == 1)
                                {
                                    isImmune = true;
                                    immunityTimer = 350;
                                    GamePanel.shielD.Image = Resources.shield;
                                    GamePanel.shielD.Visible = true;
                                    GamePanel.shielD.BringToFront();
                                }
                                else
                                {
                                    bonusPointsTimer = 350;
                                    GamePanel.multiPlier.Image = Resources.multiplier;
                                    GamePanel.multiPlier.Visible = true;
                                    GamePanel.multiPlier.BringToFront();
                                }
                            }
                            

                        }
                        if (isImmune)
                        {
                            immunityTimer--;

                            if (immunityTimer <= 0)
                            {
                                isImmune = false;
                                GamePanel.shielD.Visible = false;
                            }
                        }
                        if (bonusPointsTimer > 0)
                        {
                            bonusPointsTimer--;

                            if (bonusPointsTimer <= 0)
                            {
                                GamePanel.multiPlier.Visible = false;
                            }
                        }
                    }

                }
            }

            // Level progression logic...
            immunityTimer--;
            bonusPointsTimer--;

            if (immunityTimer <= 0)
            {
                isImmune = false;
            }
            if (levelValue > 0 && levelValue < 1000)
            {
                score++;
                MainForm.txtScore.Text = "LEVEL 1: " + score;
                
                GamePanel.award.Image = Resources.Bronze;
                roadSpeed = 15;
                trafficSpeed = 17;
            }

            if (levelValue > 1000 && levelValue < 2000)
            {
                score++;
                MainForm.txtScore.Text = "LEVEL 2: " + score;
                
                GamePanel.award.Image = Resources.Bronze;
                roadSpeed = 17;
                trafficSpeed = 19;
            }

            if (levelValue > 2000 && levelValue < 3999)
            {
                score++;
                MainForm.txtScore.Text = "LEVEL 3: " + score;
                
                GamePanel.award.Image = Resources.Silver;
                trafficSpeed = 23;
                roadSpeed = 21;
            }

            if (levelValue > 4000 && levelValue < 6000)
            {
                score++;
                MainForm.txtScore.Text = "LEVEL 4: " + score;
                
                GamePanel.award.Image = Resources.Silver;
                trafficSpeed = 27;
                roadSpeed = 25;
            }

            if (levelValue > 6001 && levelValue < 10000)
            {
                score++;
                MainForm.txtScore.Text = "LEVEL 5: " + score;               
                GamePanel.award.Image = Resources.Gold;
                trafficSpeed = 30;
                roadSpeed = 27;
            }
        }

        // Method to reset the game state
        public void resetGame()
        {
            // Reset game elements and UI for a new game...
            ThisPanel = GamePanel.game_Panel;
            gameTimer.Stop();
            gameTimer.Tick -= GameTimerEvent;
            MainForm.bttnStart.Enabled = true;
            GetPlayer.ResetCrash();
            GetPlayer.PlayerLeft = false;
            GetPlayer.PlayerRight = false;
            boom.Visible = false;
            Blood.Visible = false;
            GamePanel.award.Visible = false;
            GamePanel.shielD.Visible = false;
            GamePanel.multiPlier.Visible = false;

            GetPlayer.Avatar.Location = new Point(276, 447);
            roadSpeed = 12;
            trafficSpeed = 15;

            foreach (PictureBox item in ThisPanel.Controls)
            {
                if (item.Name == "NPC1")
                {
                    item.Top = carPosition.Next(200, 500) * -1;
                }
                if (item.Name == "NPC2")
                {
                    item.Top = carPosition.Next(200, 500) * -1;
                }
                if ((string)item.Tag == "zombie")
                {
                    item.Top = carPosition.Next(200, 500) * -1;
                }
            }
            

            isImmune = false;
            immunityTimer = 0;
            bonusPointsTimer = 0;
            zomCounter = 0;
            score = 0;
            levelValue = 0;
            
            StartGame(); // Restart the game...

        }

        private List<int> carPositions = new List<int>();

        // Method to change NPC cars based on a random selection
        private void changeNPCcars(PictureBox tempCar)
        {
            carImage = rand.Next(1, 17); // Logic to randomly change NPC car appearance and position...


            switch (carImage)
            {
                case 1: tempCar.Image = Resources.car1; break;
                case 2: tempCar.Image = Resources.car2; break;
                case 3: tempCar.Image = Resources.car3; break;
                case 4: tempCar.Image = Resources.car4; break;
                case 5: tempCar.Image = Resources.car5; break;
                case 6: tempCar.Image = Resources.car6; break;
                case 7: tempCar.Image = Resources.car7; break;
                case 8: tempCar.Image = Resources.ambulance; break;
                case 9: tempCar.Image = Resources.TruckBlue; break;
                case 10: tempCar.Image = Resources.TruckWhite; break;
                case 11: tempCar.Image = Resources.carGrey; break;
                case 12: tempCar.Image = Resources.CarRed; break;
                case 13: tempCar.Image = Resources.carOrange; break;
                case 14: tempCar.Image = Resources.carPink; break;
                case 15: tempCar.Image = Resources.carYellow; break;
                case 16: tempCar.Image = Resources.jeep_removebg_preview; break;

            }
            tempCar.Top = carPosition.Next(100, 400) * -1;

            if ((string)tempCar.Tag == "carLeft")
            {
                tempCar.Left = carPosition.Next(76, 270);
            }
            if ((string)tempCar.Tag == "carRight")
            {
                tempCar.Left = carPosition.Next(330, 500);
            }
            carPositions.Add(tempCar.Top);
        }

        // Method to change zombie appearance and position
        private void changeZombie(PictureBox tempZombie)
        {
            zombImage = rand.Next(1, 4); // Logic to randomly change zombie appearance and position without overlapping with other elements...


            switch (zombImage)
            {
                case 1: tempZombie.Image = Resources.zombie1; break;
                case 2: tempZombie.Image = Resources.zombie2; break;
                case 3: tempZombie.Image = Resources.zombie3; break;
            }
            int zombieTop = carPosition.Next(100, 400) * -1;
            int zombieLeft = carPosition.Next(80, 490);

            while (carPositions.Contains(zombieTop))
            {

                zombieTop = carPosition.Next(100, 400) * -1;
                zombieLeft = carPosition.Next(80, 490);
            }

            tempZombie.Top = zombieTop;
            tempZombie.Left = zombieLeft;
            carPositions.Clear();
        }

        // Method to handle game over conditions
        private void gameOver()
        {
            hitSound();
            boom.Visible = false;
            Blood.Visible = false;
            GamePanel.award.Visible = true;
            GamePanel.award.BringToFront();
            GetPlayer.PlayerCrash();
            gameTimer.Stop();
            MainForm.bttnStart.Enabled = true;
            GetPlayer.ResetZombie();
            recordHighScore();
        }

        private void hitSound()
        {
            System.Media.SoundPlayer playCrash = new System.Media.SoundPlayer(Resources.hit);
            playCrash.Play();
        }

        private void zombSound()
        {
            System.Media.SoundPlayer playCrash = new System.Media.SoundPlayer(Resources.zombHit);
            playCrash.Play();
        }

        // Method to record and update high scores
        List<int> scoreLeaders = new List<int>();
        private void recordHighScore()
        {
            string filePath = "HighScore.txt";
            if (!File.Exists(filePath)) // File handling for high scores...
            {
                using (FileStream fs = File.Create(filePath)) 
                    Console.WriteLine("File created successfully."); // Add the current score to the list...

            }

            scoreLeaders.Clear();
            StreamReader reader = new StreamReader("HighScore.txt");
            while (!reader.EndOfStream)
            {
                int score = int.Parse(reader.ReadLine());
                scoreLeaders.Add(score);
            }

            // Sort and update the high scores...
            scoreLeaders.Add(MainForm.highestScore+1); 
            scoreLeaders.Sort();
            scoreLeaders.Reverse();
            reader.Close();
            StreamWriter writer = new StreamWriter("HighScore.txt");
            foreach (int score in scoreLeaders)
            {
                writer.WriteLine(score); // Write the updated scores back to the file...
            }
            writer.Close();
        }       
    }
}
