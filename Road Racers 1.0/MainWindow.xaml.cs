using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Road_Racers_1._0
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer GameTimer = new DispatcherTimer();
        private bool UpArrowPressed, LeftArrowPressed, RightArrowPressed, UpKeyPressed, LeftKeyPressed, RightKeyPressed;
        private bool Driveable = true ;
        public float PlayerFuel = 100, MotorFuel;

        private double gravity = 0.5; // Adjust the gravity strength as needed

       



        private void KeyboardDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                UpArrowPressed = true;
            if (e.Key == Key.Left)
                LeftArrowPressed = true;
            if (e.Key == Key.Right)
                RightArrowPressed = true;

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

        }

        private void KeyboardUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                UpArrowPressed = false;
            if (e.Key == Key.Left)
                LeftArrowPressed = false;
            if (e.Key == Key.Right)
                RightArrowPressed = false;

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
            MovePlayer();
            MoveMotor();
        }

        private double playerSpeedY = 0; // Initialize the vertical speed of the player

        private void MovePlayer()
        {
            double playerSpeedX = 0;

            if (UpArrowPressed)
                playerSpeedY -= 1; // Jumping
            if (RightArrowPressed)
                playerSpeedX = 5;
            if (LeftArrowPressed)
                playerSpeedX = -5;

            // Apply gravity to the player
            playerSpeedY += gravity;

            // Update the position of the player
            Canvas.SetLeft(Player, Canvas.GetLeft(Player) + playerSpeedX);
            Canvas.SetTop(Player, Canvas.GetTop(Player) + playerSpeedY);

            // Detect ground or floor collision (for example, with a barrier)
            if (Canvas.GetTop(Player) + Player.Height >= Canvas.GetTop(Barrier))
            {
                // Stop the player from falling through the ground
                Canvas.SetTop(Player, Canvas.GetTop(Barrier) - Player.Height);
                playerSpeedY = 0;
            }

            if (PlayerFuel == 0.0)
            {
                Driveable = false;
            }

            if (Driveable == false)
            {
                playerSpeedX = 0;
                playerSpeedY = 0;
            }

        
            
        }

        private double MotorSpeedY = 0; // Initialize the vertical speed of the motor

        private void MoveMotor()
        {
            double MotorSpeedX = 0;

            if (UpKeyPressed)
                MotorSpeedY -= 1; // Jumping
            if (RightKeyPressed)
                MotorSpeedX = 5;
            if (LeftKeyPressed)
                MotorSpeedX = -5;

            // Apply gravity to the motor
            MotorSpeedY += gravity;

            // Update the position of the motor
            Canvas.SetLeft(Motor, Canvas.GetLeft(Motor) + MotorSpeedX);
            Canvas.SetTop(Motor, Canvas.GetTop(Motor) + MotorSpeedY);

            // Detect ground or floor collision (for example, with Barrier2)
            if (Canvas.GetTop(Motor) + Motor.Height >= Canvas.GetTop(Barrier2))
            {
                // Stop the motor from falling through the ground
                Canvas.SetTop(Motor, Canvas.GetTop(Barrier2) - Motor.Height);
                MotorSpeedY = 0;
            }
      
            if (MotorFuel == 0.0)
            {
                Driveable = false;
            }

            if (Driveable == false)
            {
                MotorSpeedX = 0;
                MotorSpeedY = 0;
            }
        }
    }
}
