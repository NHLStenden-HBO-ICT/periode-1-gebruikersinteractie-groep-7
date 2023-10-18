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
        private bool UpKeyPressed, DownKeyPressed, LeftKeyPressed, RightKeyPressed, falling = true;
        private float MotorSpeedX, MotorSpeedY, Friction = 0f, Speed = 2;
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
                Canvas.SetTop(Motor, Canvas.GetTop(Motor) - 5);
                falling = true;
            }

            if (RightKeyPressed)
            {
                Canvas.SetLeft(Motor, Canvas.GetLeft(Motor) + 10);
            }
            
            if (LeftKeyPressed)
            {
                    Canvas.SetLeft(Motor, Canvas.GetLeft(Motor) - 10);
            }

            // Player movement
            if (UpArrowPressed)
            {
                Canvas.SetTop(Player, Canvas.GetTop(Player) - 5);
                falling = true;
            }

            if (RightArrowPressed)
            {
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) + 10);
            }

            if (LeftArrowPressed)
            {
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) - 10);
            }

            if (falling)
            {
                Canvas.SetTop(Player, Canvas.GetTop(Player) + 1);
                Canvas.SetTop(Motor, Canvas.GetTop(Motor) + 1);
            }

            Rect PlayerRect = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height);
            Rect BarrierRect = new Rect(Canvas.GetLeft(Barrier), Canvas.GetTop(Barrier), Barrier.Width, Barrier.Height);
            if (PlayerRect.IntersectsWith(BarrierRect))
            {
                falling = false;
            }

            Rect MotorRect = new Rect(Canvas.GetLeft(Motor), Canvas.GetTop(Motor), Motor.Width, Motor.Height);
            Rect Barrier2Rect = new Rect(Canvas.GetLeft(Barrier2), Canvas.GetTop(Barrier2), Barrier2.Width, Barrier2.Height);

            if (MotorRect.IntersectsWith(Barrier2Rect))
            {
                falling = false;
            }


            // Apply friction to both the motor and the player
            MotorSpeedX = MotorSpeedX * Friction;
            MotorSpeedY = MotorSpeedY * Friction;
            PlayerSpeedX = PlayerSpeedX * Friction;
            PlayerSpeedY = PlayerSpeedY * Friction;

            // Update the position of the motor and the player
            Canvas.SetLeft(Motor, Canvas.GetLeft(Motor) + MotorSpeedX);
            Canvas.SetTop(Motor, Canvas.GetTop(Motor) + MotorSpeedY);

            Canvas.SetLeft(Player, Canvas.GetLeft(Player) + PlayerSpeedX);
            Canvas.SetTop(Player, Canvas.GetTop(Player) + PlayerSpeedY);
        }


    }
}