using System;
using System.Windows.Forms;
using WMPLib;

namespace game
{
    /// <summary>
    /// Форма игры
    /// </summary>
    public partial class Form1 : Form
    {

        //ооооооооо
        int roadSpeed; // скорость движения дороги
        int trafficSpeed; // скорость движения других машин
        int playerSpeed; // скорость движения игрока
        int score; // текущий счет игры
        int carImage; // номер текущего изображения машины

        Random rnd = new Random();
        // генератор случайных позиций для других машин
        Random otherCarsPosition = new Random();

        // флаги движения игрока влево и вправо
        bool moveLeft, moveRight;

        WindowsMediaPlayer crash; // звук столкновения
        WindowsMediaPlayer gameSong; // звук на фоне игры


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public Form1()
        {
            // Инициализирует поля класса и вызывает метод ResetGame()
            InitializeComponent();
            if (buttonStart.Enabled == false)
                ResetGame();
        }

        /// <summary>
        /// Обрабатывает событие нажатия клавиши на клавиатуре
        /// </summary>
        /// <param name="e">События клавиатуры</param>
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) moveLeft = true;

            if (e.KeyCode == Keys.Right) moveRight = true;
        }

        /// <summary>
        /// Обрабатывает событие освобождения клавиши на клавиатуре
        /// </summary>
        /// <param name="e">События клавиатуры</param>
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) moveLeft = false;

            if (e.KeyCode == Keys.Right) moveRight = false;
        }

        /// <summary>
        /// Обрабатывает события таймера игры
        /// </summary>
        private void GameTimerEvent(object sender, EventArgs e)
        {
            gameSong.controls.play(); // воспроизводит фоновый звук игры
            // Обновляет счет игры
            scoreLabel.Text = "Score: " + score;
            score++;

            // Перемещение игрока влево-вправо, учитывая границы формы
            if (moveLeft && playerCar.Left > 5)
                playerCar.Left -= playerSpeed;
            if (moveRight && playerCar.Left < 258)
                playerCar.Left += playerSpeed;

            // Перемещает дорогу вниз
            road1.Top += roadSpeed;
            road2.Top += roadSpeed;

            // Если дорога находится ниже края формы, перемещает ее на верхнюю границу формы
            if (road2.Top > 326)
                road2.Top = -326;
            if (road1.Top > 326)
                road1.Top = -326;

            // Перемещает другие машины вниз
            leftCar.Top += trafficSpeed;
            rightCar.Top += trafficSpeed;

            // Если другие машины находятся ниже края формы, изменяет их изображения и перемещает на случайную координату сверху формы
            if (leftCar.Top > 460)
                ChangeOtherCars(leftCar);
            if (rightCar.Top > 460)
                ChangeOtherCars(rightCar);

            // Если происходит столкновение игрока с другими машинами, останавливает главную музыку,
            // воспроизводит звук столкновения и вызывает метод GameOver()
            if (playerCar.Bounds.IntersectsWith(rightCar.Bounds) || playerCar.Bounds.IntersectsWith(leftCar.Bounds))
            {
                gameSong.controls.stop();
                crash.controls.play();
                GameOver();
            }

            // Изменяет скорость движения дороги и других машин в зависимости от текущего счета игры
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

        /// <summary>
        /// Метод изменения изображения и позиции других машин на случайную координату сверху формы
        /// </summary>
        /// <param name="car">Поле для изображения машины</param>
        private void ChangeOtherCars(PictureBox car)
        {
            // Выбирает случайное изображение машины, изменяет изображение переданной машины
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

            // Устанавливает ее вертикальную позицию вверху формы, а горизонтальную -
            // на случайную позицию между левой (правой) границей формы и серединой формы, в зависимости от заданного тега ее положения

            car.Top = otherCarsPosition.Next(100, 400) * -1;

            if ((string)car.Tag == "carLeft")
                car.Left = otherCarsPosition.Next(5, 200);
            if ((string)car.Tag == "carRight")
                car.Left = otherCarsPosition.Next(245, 422);
        }

        /// <summary>
        /// Окончание игры (столкновение с другой машиной)
        /// </summary>
        private void GameOver()
        {
            gameTimer.Stop(); // останавливает таймер игры
            buttonStart.Enabled = true; // разблокирует кнопку старта
        }

        /// <summary>
        /// Перезапускает игру
        /// </summary>
        private void RestartGame()
        {
            ResetGame();
        }

        /// <summary>
        /// Запускает игру
        /// </summary>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            RestartGame();
            buttonStart.Enabled = false; // блокирует кнопку старта
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            playerSpeed = 15;

            // устанавливает параметры звуков
            
            crash = new WindowsMediaPlayer();
            crash.URL = "song\\crash.mp3";
            crash.settings.volume = 15;

            gameSong = new WindowsMediaPlayer();
            gameSong.URL = "song\\gameSong.mp3";
            gameSong.settings.setMode("loop", true);
            gameSong.settings.volume = 15;
        }

        /// <summary>
        /// Сбрасывает игровые параметры на стандартные значения и запускает игру
        /// </summary>
        private void ResetGame()
        {
            moveLeft = false;
            moveRight = false;
            score = 0;
            roadSpeed = 12;
            trafficSpeed = 7;
            
            leftCar.Top = otherCarsPosition.Next(200, 500) * -1;
            leftCar.Left = otherCarsPosition.Next(5, 200);

            rightCar.Top = otherCarsPosition.Next(200, 500) * -1;
            rightCar.Left = otherCarsPosition.Next(245, 422);

            gameTimer.Start();
        }
    }
}
