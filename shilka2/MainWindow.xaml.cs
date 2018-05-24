using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Data;

namespace shilka2
{
    public partial class MainWindow : Window
    {
        public System.Timers.Timer Game = new System.Timers.Timer(30);
        public System.Timers.Timer HandMove = new System.Timers.Timer(600);
        public System.Timers.Timer Aircrafts = new System.Timers.Timer(2000);
        bool Pause = false;
        bool endGameAlready = false;
        bool startMenuShowYet = true;

        public MainWindow()
        {
            InitializeComponent();

            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;

            double heightForShilka = SystemParameters.PrimaryScreenHeight - ShilkaImg.Height;

            Aircraft.minAltitudeGlobal = (int)(heightForShilka - ShilkaImg.Height);

            EndMenu.Height = SystemParameters.PrimaryScreenHeight;
            EndMenu.Margin = new Thickness(SystemParameters.PrimaryScreenWidth, 0, 0, 0);
            firePlaceDock.Margin = new Thickness(SystemParameters.PrimaryScreenWidth, 0, 0, 0);

            StatisticMenu.Height = SystemParameters.PrimaryScreenHeight;
            StatisticMenu.Width = SystemParameters.PrimaryScreenWidth;
            StatisticMenu.Margin = new Thickness(0, SystemParameters.PrimaryScreenHeight, 0, 0);

            var converter = new BrushConverter();
            StatisticMenu.Background = (Brush)converter.ConvertFrom("#FF001B36");

            StatisticGrid.ItemsSource = Shilka.LoadStatistic();

            StatisticGrid.Height = StatisticMenu.Height - Shilka.statisticGridMargins;
            StatisticGrid.Width = StatisticMenu.Width - Shilka.statisticGridMargins;

            StartMenu.Height = StatisticMenu.Height;
            StartMenu.Width = StatisticMenu.Width;
            StartMenu.Margin = new Thickness(0, 0, 0, 0);

            Thickness buttonMargin = new Thickness(
                (StartMenu.Width / 2 - startSimple.Width), (StartMenu.Height / 2 - (startSimple.Height + shilkaArt.Height ) / 2), 0, 0
            );

            startSimple.Margin = buttonMargin;
            resultButton.Margin = buttonMargin;
            exitButton.Margin = buttonMargin;
            shilkaArt.Margin = buttonMargin;
            startVietnam.Margin = buttonMargin;
            startDesertStorm.Margin = buttonMargin;

            StartMenu.Background = (Brush)converter.ConvertFrom("#FF343333");

            ShilkaImg.Margin = new Thickness(0, heightForShilka, 0, 0);
            HandImg.Margin = new Thickness(65, (heightForShilka - 120), 0, 0);
        }

        public void StartGame(int[] scriptAircraft, int[] scriptAircraftFriend)
        {
            startMenuShowYet = false;

            Aircraft.scriptAircraft = scriptAircraft;
            Aircraft.scriptAircraftFriend = scriptAircraftFriend;

            MoveCanvas(
                moveCanvas: StartMenu,
                prevCanvas: firePlaceDock,
                left: StartMenu.Margin.Left - StartMenu.ActualWidth,
                speed: 0.6
            );

            HandMove.Enabled = true;
            HandMove.Elapsed += new ElapsedEventHandler(HideHand);
            HandMove.Start();

            Game.Enabled = true;
            Game.Elapsed += new ElapsedEventHandler(Shell.ShellsFire);
            Game.Elapsed += new ElapsedEventHandler(Shell.ShellsFly);
            Game.Elapsed += new ElapsedEventHandler(Case.CasesFly);
            Game.Elapsed += new ElapsedEventHandler(Aircraft.AircraftFly);
            Game.Elapsed += new ElapsedEventHandler(Shilka.StatisticShow);
            Game.Start();

            Aircrafts.Enabled = true;
            Aircrafts.Elapsed += new ElapsedEventHandler(Aircraft.AircraftStart);
            Aircrafts.Start();
        }

        public static void HideHand(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                main.HandMove.Enabled = false;
                main.HandMove.Stop();

                ThicknessAnimation move = new ThicknessAnimation();
                move.Duration = TimeSpan.FromSeconds(1.5);
                move.From = main.HandImg.Margin;
                move.To = new Thickness(
                    ( SystemParameters.PrimaryScreenHeight / 2 ),
                    SystemParameters.PrimaryScreenHeight,
                    0, 0
                );
                main.HandImg.BeginAnimation(Border.MarginProperty, move);
            }));
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
                moveCanvas: EndMenu,
                prevCanvas: firePlaceDock,
                left: EndMenu.Margin.Left - EndMenu.ActualWidth,
                speed: 0.2
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
                moveCanvas: StatisticMenu,
                prevCanvas: (startMenuShowYet ? StartMenu : firePlaceDock),
                top: StatisticMenu.Margin.Top - StatisticMenu.ActualHeight,
                speed: 0.5
            );
        }

        public void ReturnStatisticShow()
        {
            MoveCanvas(
                moveCanvas: StatisticMenu,
                prevCanvas: (startMenuShowYet ? StartMenu : firePlaceDock),
                top: StatisticMenu.Margin.Top + StatisticMenu.ActualHeight,
                speed: 0.5
            );

            if (StartMenu.Margin.Left < 0 && !Pause && !endGameAlready)
            {
                Game.Start();
                Aircrafts.Start();
            }
        }

        public void MoveCanvas(Canvas moveCanvas, Canvas prevCanvas,
            double left = -1, double top = -1, double right = -1, double bottom = -1, double speed = 1)
        {
            left = (left == -1 ? moveCanvas.Margin.Left : left);
            top = (top == -1 ? moveCanvas.Margin.Top : top);
            right = (right == -1 ? moveCanvas.Margin.Right : right);
            bottom = (bottom == -1 ? moveCanvas.Margin.Bottom : bottom);

            ThicknessAnimation move = new ThicknessAnimation();
            move.Duration = TimeSpan.FromSeconds(speed);
            move.From = moveCanvas.Margin;
            move.To = new Thickness(left, top, right, bottom);
            moveCanvas.BeginAnimation(Border.MarginProperty, move);

            left = prevCanvas.Margin.Left - (moveCanvas.Margin.Left - left);
            top = prevCanvas.Margin.Top - (moveCanvas.Margin.Top - top);
            right = prevCanvas.Margin.Right - (moveCanvas.Margin.Right - right);
            bottom = prevCanvas.Margin.Bottom - (moveCanvas.Margin.Bottom - bottom);

            move.From = prevCanvas.Margin;
            move.To = new Thickness(left, top, right, bottom);
            prevCanvas.BeginAnimation(Border.MarginProperty, move);
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
            if (Shell.Fire) Shilka.SetNewTergetPoint(e.GetPosition((Window)sender), sender);
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
            GameOverWithSave.IsEnabled = (PlayerName.Text == "" ? false : true);
        }

        private void statisticButton_Click(object sender, RoutedEventArgs e)
        {
            GameStatisticShow();
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnStatisticShow();
        }

        private void startSimple_Click(object sender, RoutedEventArgs e)
        {
            int[] aircraft = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
            int[] aircraftFriend = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

            StartGame(aircraft, aircraftFriend);
        }

        private void startVietnam_Click(object sender, RoutedEventArgs e)
        {
            int[] aircraft = new int[] { 3, 5, 10, 21, 22, 23 };
            int[] aircraftFriend = new int[] { 4, 11, 12 };

            StartGame(aircraft, aircraftFriend);
        }

        private void startDesertStorm_Click(object sender, RoutedEventArgs e)
        {
            int[] aircraft = new int[] { 1, 2, 3, 4, 5, 6, 7, 9, 10, 11, 15, 18, 20, 22, 23 };
            int[] aircraftFriend = new int[] { 1, 2, 6, 13 };

            StartGame(aircraft, aircraftFriend);
        }
    }
}
