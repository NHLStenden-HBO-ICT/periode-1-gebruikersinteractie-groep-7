using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace Road_Racers_1._0
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer GameTimer = new DispatcherTimer();
        private bool UpKeyPressed, DownKeyPressed, LeftKeyPressed, RightKeyPressed;
        private float SpeedX, SpeedY, Friction = 0.60f, Speed = 2;
        private bool UpArrowPressed, DownArrowPressed, LeftArrowPressed, RightArrowPressed;
        private float PlayerSpeedX, PlayerSpeedY;

        private void KeyboardDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                UpKeyPressed = true;
            }
            if (e.Key == Key.A)
            {
                LeftKeyPressed = true;
            }

            if (e.Key == Key.D)
            {
                RightKeyPressed = true;
            }

            if (e.Key == Key.S)
            {
                DownKeyPressed = true;
            }

            if (e.Key == Key.Up)
            {
                UpArrowPressed = true;
            }
            if (e.Key == Key.Left)
            {
                LeftArrowPressed = true;
            }

            if (e.Key == Key.Right)
            {
                RightArrowPressed = true;
            }

            if (e.Key == Key.Down)
            {
                DownArrowPressed = true;
            }
        }

        private void KeyboardUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                UpKeyPressed = false;
            }
            if (e.Key == Key.A)
            {
                LeftKeyPressed = false;
            }

            if (e.Key == Key.D)
            {
                RightKeyPressed = false;
            }

            if (e.Key == Key.S)
            {
                DownKeyPressed = false;
            }

            if (e.Key == Key.Up)
            {
                UpArrowPressed = false;
            }
            if (e.Key == Key.Left)
            {
                LeftArrowPressed = false;
            }

            if (e.Key == Key.Right)
            {
                RightArrowPressed = false;
            }

            if (e.Key == Key.Down)
            {
                DownArrowPressed = false;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            GameScreen.Focus();

            GameTimer.Interval = TimeSpan.FromMilliseconds(16);
            GameTimer.Tick += GameTick;
            GameTimer.Start();
        }

        private void GameTick(object sender, EventArgs e)
        {
            // Motor movement
            if (UpKeyPressed)
            {
                SpeedY -= Speed; // Change direction for moving up
            }

            if (RightKeyPressed)
            {
                SpeedX -= Speed; // Change direction for moving right
            }

            if (DownKeyPressed)
            {
                SpeedY += Speed; // Change direction for moving down
            }

            if (LeftKeyPressed)
            {
                SpeedX += Speed; // Change direction for moving left
            }

            // Player movement
            if (UpArrowPressed)
            {
                PlayerSpeedY -= Speed;
            }

            if (RightArrowPressed)
            {
                PlayerSpeedX += Speed;
            }

            if (DownArrowPressed)
            {
                PlayerSpeedY += Speed;
            }

            if (LeftArrowPressed)
            {
                PlayerSpeedX -= Speed;
            }

            // Apply friction to both the motor and the player
            SpeedX = SpeedX * Friction;
            SpeedY = SpeedY * Friction;
            PlayerSpeedX = PlayerSpeedX * Friction;
            PlayerSpeedY = PlayerSpeedY * Friction;

            // Update the position of the motor and the player
            Canvas.SetLeft(Motor, Canvas.GetLeft(Motor) + SpeedX);
            Canvas.SetTop(Motor, Canvas.GetTop(Motor) + SpeedY);

            Canvas.SetLeft(Player, Canvas.GetLeft(Player) + PlayerSpeedX);
            Canvas.SetTop(Player, Canvas.GetTop(Player) + PlayerSpeedY);
        }


    }
}