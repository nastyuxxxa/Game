using System;
using System.Windows.Forms;
using WMPLib;

namespace game
{
    public partial class Form1 : Form
    {

        int roadSpeed;
        int trafficSpeed;
        int playerSpeed;
        int score;
        int carImage;

        Random rnd = new Random();
        Random otherCarsPosition = new Random();

        bool moveLeft, moveRight;

        WindowsMediaPlayer crash;
        WindowsMediaPlayer gameSong;


        public Form1()
        {
            InitializeComponent();
            if (buttonStart.Enabled == false)
                ResetGame();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) moveLeft = true;

            if (e.KeyCode == Keys.Right) moveRight = true;
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) moveLeft = false;

            if (e.KeyCode == Keys.Right) moveRight = false;
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            gameSong.controls.play();
            scoreLabel.Text = "Score: " + score;
            score++;

            if (moveLeft && playerCar.Left > 5)
                playerCar.Left -= playerSpeed;
            if (moveRight && playerCar.Left < 258)
                playerCar.Left += playerSpeed;

            road1.Top += roadSpeed;
            road2.Top += roadSpeed;

            if (road2.Top > 326)
                road2.Top = -326;
            if (road1.Top > 326)
                road1.Top = -326;

            redCar.Top += trafficSpeed;
            greenCar.Top += trafficSpeed;

            if (redCar.Top > 460)
                ChangeOtherCars(redCar);
            if (greenCar.Top > 460)
                ChangeOtherCars(greenCar);

            if (playerCar.Bounds.IntersectsWith(greenCar.Bounds) || playerCar.Bounds.IntersectsWith(redCar.Bounds))
            {
                gameSong.controls.stop();
                crash.controls.play();
                GameOver();
            }

            if (score > 100 && score < 200)
            {
                trafficSpeed = 9;
                roadSpeed = 16;
            }

            if (score > 200 && score < 500)
            {
                trafficSpeed = 12;
                roadSpeed = 18;
            }

            if (score > 500 && score < 800)
            {
                trafficSpeed = 16;
                roadSpeed = 21;
            }

            if (score > 800)
            {
                trafficSpeed = 21;
                roadSpeed = 25;
            }


        }

        private void ChangeOtherCars(PictureBox car)
        {
            carImage = rnd.Next(1, 8);
            switch (carImage)
            {
                case 1:
                    car.Image = Properties.Resources.ambulance;
                    break;
                case 2:
                    car.Image = Properties.Resources.carGreen;
                    break;
                case 3:
                    car.Image = Properties.Resources.carOrange;
                    break;
                case 4:
                    car.Image = Properties.Resources.carPink;
                    break;
                case 5:
                    car.Image = Properties.Resources.carYellow;
                    break;
                case 6:
                    car.Image = Properties.Resources.CarRed;
                    break;
                case 7:
                    car.Image = Properties.Resources.TruckBlue;
                    break;
                case 8:
                    car.Image = Properties.Resources.TruckWhite;
                    break;
            }

            car.Top = otherCarsPosition.Next(100, 400) * -1;

            if ((string)car.Tag == "carLeft")
                car.Left = otherCarsPosition.Next(5, 200);
            if ((string)car.Tag == "carRight")
                car.Left = otherCarsPosition.Next(245, 422);
        }

        private void GameOver()
        {
            gameTimer.Stop();
            buttonStart.Enabled = true;
        }

        private void RestartGame()
        {
            ResetGame();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            RestartGame();
            buttonStart.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            playerSpeed = 15;

            crash = new WindowsMediaPlayer();
            crash.URL = "song\\crash.mp3";
            crash.settings.volume = 15;

            gameSong = new WindowsMediaPlayer();
            gameSong.URL = "song\\gameSong.mp3";
            gameSong.settings.setMode("loop", true);
            gameSong.settings.volume = 15;
        }

        private void ResetGame()
        {
            moveLeft = false;
            moveRight = false;
            score = 0;
            roadSpeed = 12;
            trafficSpeed = 7;

            //gameSong.controls.play();

            redCar.Top = otherCarsPosition.Next(200, 500) * -1;
            redCar.Left = otherCarsPosition.Next(5, 200);

            greenCar.Top = otherCarsPosition.Next(200, 500) * -1;
            greenCar.Left = otherCarsPosition.Next(245, 422);

            gameTimer.Start();
        }
    }
}
