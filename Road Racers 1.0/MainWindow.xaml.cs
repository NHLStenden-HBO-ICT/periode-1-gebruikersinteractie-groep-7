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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer GameTimer = new DispatcherTimer();
        private bool LeftArrowPressed, RightArrowPressed, LeftKeyPressed, RightKeyPressed;
        private bool Driveable = true, PlayerJump, MotorJump, PlayerCollidedWithJumpPad, MotorCollidedWithJumpPad, Falling;
        public float PlayerFuel = 100, MotorFuel;

        private double gravity = 0.5, JumpBoost = 0; // Adjust the gravity strength as needed & Initiate JumpBoost





        private void KeyboardDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                LeftArrowPressed = true;
            if (e.Key == Key.Right)
                RightArrowPressed = true;

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
            if (e.Key == Key.Left)
                LeftArrowPressed = false;
            if (e.Key == Key.Right)
                RightArrowPressed = false;

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

            Rect playerRect = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height);
            Rect motorRect = new Rect(Canvas.GetLeft(Motor), Canvas.GetTop(Motor), Motor.Width, Motor.Height);
            Rect jumpPadRect = new Rect(Canvas.GetLeft(JumpPad), Canvas.GetTop(JumpPad), JumpPad.Width, JumpPad.Height);
            Rect jumpPad_CopyRect = new Rect(Canvas.GetLeft(JumpPad_Copy), Canvas.GetTop(JumpPad_Copy), JumpPad_Copy.Width, JumpPad_Copy.Height);
            if (playerRect.IntersectsWith(jumpPadRect))
            {
                // Player collided with the JumpPad, trigger jumping
                PlayerJump = true;
            }

            if (motorRect.IntersectsWith(jumpPad_CopyRect))
            {
                // Motor collided with the JumpPad, trigger jumping
                MotorJump = true;
            }
            if (playerRect.IntersectsWith(jumpPadRect))
            {
                PlayerCollidedWithJumpPad = true;
            }

            if (motorRect.IntersectsWith(jumpPad_CopyRect))
            {
                MotorCollidedWithJumpPad = true;
            }

        }

        private double playerSpeedY = 0; // Initialize the vertical speed of the player

        private void MovePlayer()
        {
            double playerSpeedX = 0;


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

            if (PlayerCollidedWithJumpPad)
            {
                // Apply jumping force only when collided with JumpPad
                playerSpeedY = -25 + JumpBoost; // Adjust the jump force as needed
                PlayerCollidedWithJumpPad = false; // Reset the flag
            }

        }

        private double MotorSpeedY = 0; // Initialize the vertical speed of the motor

        private void MoveMotor()
        {
            double MotorSpeedX = 0;

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
                Canvas.SetTop(Motor, Canvas.GetTop(Barrier2) - 25);
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

            if (MotorCollidedWithJumpPad)
            {
                // Apply jumping force only when collided with JumpPad
                MotorSpeedY = -25 + JumpBoost; // Adjust the jump force as needed
                MotorCollidedWithJumpPad = false; // Reset the flag
            }
        }
    }

}
