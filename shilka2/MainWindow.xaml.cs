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
        public System.Timers.Timer Game;
        public System.Timers.Timer Aircrafts;
        bool Pause = false;
        bool endGameAlready = false;

        public MainWindow()
        {
            InitializeComponent();

            Game = new System.Timers.Timer(30);
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

            this.WindowState = System.Windows.WindowState.Maximized;
            this.WindowStyle = System.Windows.WindowStyle.None;

            double heightForShilka =
                System.Windows.SystemParameters.PrimaryScreenHeight - ShilkaImg.Height;

            Aircraft.minFlightHeight = (int)(heightForShilka - ShilkaImg.Height);

            EndMenu.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            EndMenu.Margin = new Thickness(System.Windows.SystemParameters.PrimaryScreenWidth, 0, 0, 0);

            ShilkaImg.Margin = new Thickness(0, heightForShilka, 0, 0);
        }

        public void EndGame(string endText, string bgColor)
        {
            endGameAlready = true;

            Game.Stop();
            Aircrafts.Stop();

            double l = EndMenu.Margin.Left - EndMenu.ActualWidth;
            double t = EndMenu.Margin.Top;
            double r = EndMenu.Margin.Right;
            double b = EndMenu.Margin.Bottom;

            EndText.Content = endText;

            var converter = new BrushConverter();
            EndMenu.Background = (Brush)converter.ConvertFrom(bgColor);

            ThicknessAnimation endMenuShow = new ThicknessAnimation();
            endMenuShow.Duration = TimeSpan.FromSeconds(0.2);
            endMenuShow.From = EndMenu.Margin;
            endMenuShow.To = new Thickness(l, t, r, b);
            EndMenu.BeginAnimation(Border.MarginProperty, endMenuShow);
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
            if (!endGameAlready) EndGame("Выход из игры.\nСохранить статистику?", "#FF0F0570");
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
    }
}
