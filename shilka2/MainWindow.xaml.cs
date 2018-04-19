using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace shilka2
{
    public partial class MainWindow : Window
    {
        public System.Timers.Timer Game = new System.Timers.Timer(30);
        public System.Timers.Timer Aircrafts;
        bool Pause = false;
        bool endGameAlready = false;

        public MainWindow()
        {
            InitializeComponent();

            this.WindowState = System.Windows.WindowState.Maximized;
            this.WindowStyle = System.Windows.WindowStyle.None;

            double heightForShilka =
                System.Windows.SystemParameters.PrimaryScreenHeight - ShilkaImg.Height;

            Aircraft.minFlightHeight = (int)(heightForShilka - ShilkaImg.Height);

            EndMenu.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            EndMenu.Margin = new Thickness(System.Windows.SystemParameters.PrimaryScreenWidth, 0, 0, 0);

            StatisticMenu.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            StatisticMenu.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            StatisticMenu.Margin = new Thickness(0, System.Windows.SystemParameters.PrimaryScreenHeight, 0, 0);

            var converter = new BrushConverter();
            StatisticMenu.Background = (Brush)converter.ConvertFrom("#FF001B36");

            StatisticGrid.ItemsSource = Shilka.LoadStatistic();

            StatisticGrid.Height = StatisticMenu.Height - Shilka.statisticGridMargins;
            StatisticGrid.Width = StatisticMenu.Width - Shilka.statisticGridMargins;

            StartMenu.Height = StatisticMenu.Height;
            StartMenu.Width = StatisticMenu.Width;
            StartMenu.Margin = new Thickness(0, 0, 0, 0);

            Thickness buttonMargin = new Thickness(
                (StartMenu.Width / 2 - startButton.Width), (StartMenu.Height / 2 - (startButton.Height + shilkaArt.Height ) / 2), 0, 0
            );

            startButton.Margin = buttonMargin;
            resultButton.Margin = buttonMargin;
            exitButton.Margin = buttonMargin;
            shilkaArt.Margin = buttonMargin;

            StartMenu.Background = (Brush)converter.ConvertFrom("#FF343333");

            ShilkaImg.Margin = new Thickness(0, heightForShilka, 0, 0);
        }

        public void StartGame()
        {
            MoveCanvas(
                StartMenu,
                StartMenu.Margin.Left - StartMenu.ActualWidth,
                StartMenu.Margin.Top,
                StartMenu.Margin.Right,
                StartMenu.Margin.Bottom,
                0.6
            );

            Game.Enabled = true;
            Game.Elapsed += new ElapsedEventHandler(Shell.ShellsFire);
            Game.Elapsed += new ElapsedEventHandler(Shell.ShellsFly);
            Game.Elapsed += new ElapsedEventHandler(Case.CasesFly);
            Game.Elapsed += new ElapsedEventHandler(Aircraft.AircraftFly);
            Game.Elapsed += new ElapsedEventHandler(Shilka.StatisticShow);
            Game.Start();

            Aircrafts = new System.Timers.Timer(2000);
            Aircrafts.Enabled = true;
            Aircrafts.Elapsed += new ElapsedEventHandler(Aircraft.AircraftStart);
            Aircrafts.Start();
        }

        public void EndGame(string endText, string bgColor)
        {
            endGameAlready = true;

            Game.Stop();
            Aircrafts.Stop();

            EndText.Content = endText;

            var converter = new BrushConverter();
            EndMenu.Background = (Brush)converter.ConvertFrom(bgColor);

            MoveCanvas(
                EndMenu,
                EndMenu.Margin.Left - EndMenu.ActualWidth,
                EndMenu.Margin.Top,
                EndMenu.Margin.Right,
                EndMenu.Margin.Bottom,
                0.2
            );
        }

        public void GameStatisticShow()
        {
            if (Game.Enabled)
            {
                Game.Stop();
                Aircrafts.Stop();
            }

            MoveCanvas(
                StatisticMenu,
                StatisticMenu.Margin.Left,
                StatisticMenu.Margin.Top - StatisticMenu.ActualHeight,
                StatisticMenu.Margin.Right,
                StatisticMenu.Margin.Bottom,
                0.5
            );
        }

        public void ReturnStatisticShow()
        {
            MoveCanvas(
                StatisticMenu,
                StatisticMenu.Margin.Left,
                StatisticMenu.Margin.Top + StatisticMenu.ActualHeight,
                StatisticMenu.Margin.Right,
                StatisticMenu.Margin.Bottom,
                0.5
            );

            if (StartMenu.Margin.Left < 0 && !Pause)
            {
                Game.Start();
                Aircrafts.Start();
            }
        }

        public void MoveCanvas(Canvas moveCanvas, double l, double t, double r, double b, double speed)
        {
            ThicknessAnimation move = new ThicknessAnimation();
            move.Duration = TimeSpan.FromSeconds(speed);
            move.From = moveCanvas.Margin;
            move.To = new Thickness(l, t, r, b);
            moveCanvas.BeginAnimation(Border.MarginProperty, move);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Shilka.SetNewTergetPoint(e.GetPosition((Window)sender), sender);

            if (!Shilka.reheatingGunBurrels) Shell.Fire = true;
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Shell.Fire = false;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Shell.Fire)
                Shilka.SetNewTergetPoint(e.GetPosition((Window)sender), sender);
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!endGameAlready)
                EndGame("Выход из игры.\nСохранить статистику?", "#FF0F0570");
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (endGameAlready) return;

            if (Pause)
            {
                Shell.AnimationStop = false;
                Game.Start();
                Aircrafts.Start();
                Pause = false;
            }
            else
            {
                Shell.AnimationStop = true;
                Game.Stop();
                Aircrafts.Stop();
                Pause = true;
            }
        }

        private void GameOverWithoutSave_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GameOverWithSave_Click(object sender, RoutedEventArgs e)
        {
            Shilka.StatisticSave(PlayerName.Text);
            this.Close();
        }

        private void PlayerName_KeyUp(object sender, KeyEventArgs e)
        {
            if (PlayerName.Text == "")
                GameOverWithSave.IsEnabled = false;
            else
                GameOverWithSave.IsEnabled = true;
        }

        private void statisticButton_Click(object sender, RoutedEventArgs e)
        {
            GameStatisticShow();
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnStatisticShow();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
    }
}
