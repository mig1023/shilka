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
        bool startGameAlready = false;
        bool startMenuShowYet = true;

        string StatisticColor = "#FF5B5B5B";
        string StartColor = "#FF343333";
        string EndColor = "#FF0F0570";

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
            StatisticMenu.Background = (Brush)converter.ConvertFrom(StatisticColor);

            StatisticGrid.ItemsSource = Statistic.Load();

            StatisticGrid.Height = StatisticMenu.Height - Statistic.statisticGridMargins;
            StatisticGrid.Width = StatisticMenu.Width - Statistic.statisticGridMargins;

            StartMenu.Height = StatisticMenu.Height;
            StartMenu.Width = StatisticMenu.Width;
            StartMenu.Margin = new Thickness(0, 0, 0, 0);

            StartMenuButtons.Margin = new Thickness(
                (StartMenu.Width / 2 - StartMenuButtons.Width / 2), (StartMenu.Height / 2 - StartMenuButtons.Height / 2), 0, 0
            );

            StartMenu.Background = (Brush)converter.ConvertFrom(StartColor);

            ShilkaImg.Margin = new Thickness(0, heightForShilka, 0, 0);
            RadarImg.Margin = new Thickness(62, heightForShilka, 0, 0);
            HandImg.Margin = new Thickness(65, (heightForShilka - 120), 0, 0);

            note.Margin = new Thickness(SystemParameters.PrimaryScreenWidth - 600, (heightForShilka - 120), 0, 0);
        }

        public void StartGame(int?[] scriptAircraft, int?[] scriptHelicopters,
            int?[] scriptAircraftFriend, int?[] scriptHelicoptersFriend)
        {
            startMenuShowYet = false;

            if (startGameAlready)
            {
                Shilka.endGameCleaning();
                Pause = false;
                pauseButton.IsChecked = false;
                Shell.AnimationStop = false;
            }

            Scripts.scriptAircraft = scriptAircraft;
            Scripts.scriptHelicopters = scriptHelicopters;
            Scripts.scriptAircraftFriend = scriptAircraftFriend;
            Scripts.scriptHelicoptersFriend = scriptHelicoptersFriend;

            MoveCanvas(
                moveCanvas: StartMenu,
                prevCanvas: firePlaceDock,
                left: StartMenu.Margin.Left - StartMenu.ActualWidth,
                speed: 0.6
            );

            if (!startGameAlready)
            {
                HandMove.Elapsed += new ElapsedEventHandler(HideHand);

                Game.Elapsed += new ElapsedEventHandler(Shell.ShellsFire);
                Game.Elapsed += new ElapsedEventHandler(Shell.ShellsFly);
                Game.Elapsed += new ElapsedEventHandler(Case.CasesFly);
                Game.Elapsed += new ElapsedEventHandler(Aircraft.AircraftFly);
                Game.Elapsed += new ElapsedEventHandler(Statistic.Show);

                Aircrafts.Elapsed += new ElapsedEventHandler(Aircraft.AircraftStart);
            }
            HandMove.Enabled = true;
            HandMove.Start();

            Game.Enabled = true;
            Game.Start();

            Aircrafts.Enabled = true;
            Aircrafts.Start();

            startGameAlready = true;
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
                main.HandImg.BeginAnimation(MarginProperty, move);
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

        public void MoveCanvas(Canvas moveCanvas, Canvas prevCanvas, double left = -1, double top = -1,
            double speed = 1, EventHandler secondAnimation = null)
        {
            left = (left == -1 ? moveCanvas.Margin.Left : left);
            top = (top == -1 ? moveCanvas.Margin.Top : top);

            ThicknessAnimation move = new ThicknessAnimation();
            move.Duration = TimeSpan.FromSeconds(speed);
            move.From = moveCanvas.Margin;
            move.To = new Thickness(left, top, moveCanvas.Margin.Right, moveCanvas.Margin.Bottom);
            moveCanvas.BeginAnimation(MarginProperty, move);

            left = prevCanvas.Margin.Left - (moveCanvas.Margin.Left - left);
            top = prevCanvas.Margin.Top - (moveCanvas.Margin.Top - top);

            move.From = prevCanvas.Margin;
            move.To = new Thickness(left, top, moveCanvas.Margin.Right, moveCanvas.Margin.Bottom);

            if (secondAnimation != null) move.Completed += secondAnimation;

            prevCanvas.BeginAnimation(MarginProperty, move);
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
            Shilka.SetNewTergetPoint(e.GetPosition((Window)sender), sender);

            if (!Pause) RadarImg.RenderTransform = new RotateTransform(Shilka.lastDegree, 4, 20);
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!endGameAlready) EndGame("Выход из игры.\nСохранить статистику?", EndColor);
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

        private void endGameSecAnimation(object Sender, EventArgs e)
        {
            MoveCanvas(
                moveCanvas: StartMenu,
                prevCanvas: firePlaceDock,
                left: StartMenu.Margin.Left + StartMenu.ActualWidth,
                speed: 0.6
            );
        }

        private void GameOver(string playerName = "")
        {
            if (playerName != "") Statistic.Save(playerName);

            MoveCanvas(
                moveCanvas: firePlaceDock, 
                prevCanvas: EndMenu,
                left: firePlaceDock.Margin.Left + EndMenu.ActualWidth,
                speed: 0.2,
                secondAnimation: new EventHandler(endGameSecAnimation)
            );

            startMenuShowYet = true;
            endGameAlready = false;
        }

        private void GameOverWithoutSave_Click(object sender, RoutedEventArgs e)
        {
            GameOver();
        }

        private void GameOverWithSave_Click(object sender, RoutedEventArgs e)
        {
            GameOver(PlayerName.Text);
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

        public void startScript(Scripts.scriptsNames script)
        {
            Shilka.currentScript = script;

            string flagName = Scripts.scriptFlagName(script);

            if (flagName != null)
                scenarioFlag.Source = Aircraft.imageFromResources(flagName);
            else
                scenarioFlag.Source = null;

            StartGame(
                Scripts.scriptEnemyAircrafts(script),
                Scripts.scriptEnemyHelicopters(script),
                Scripts.scriptFriendAircrafts(script),
                Scripts.scriptFriendHelicopterss(script)
            );
        }

        private void startScript_Click(object sender, RoutedEventArgs e)
        {
            Button startButton = sender as Button;

            switch (startButton.Name)
            {
                case "noScript":
                    startScript(Scripts.scriptsNames.noScript);
                    break;
                case "Vietnam":
                    startScript(Scripts.scriptsNames.Vietnam);
                    break;
                case "DesertStorm":
                    startScript(Scripts.scriptsNames.DesertStorm);
                    break;
                case "Yugoslavia":
                    startScript(Scripts.scriptsNames.Yugoslavia);
                    break;
                case "IranIraq":
                    startScript(Scripts.scriptsNames.IranIraq);
                    break;
                case "Syria":
                    startScript(Scripts.scriptsNames.Syria);
                    break;
                case "KoreanBoeing":
                    startScript(Scripts.scriptsNames.KoreanBoeing);
                    break;
                case "Libya":
                    startScript(Scripts.scriptsNames.Libya);
                    break;
                case "Yemen":
                    startScript(Scripts.scriptsNames.Yemen);
                    break;
                case "Rust":
                    startScript(Scripts.scriptsNames.Rust);
                    break;
                case "F117Hunt":
                    startScript(Scripts.scriptsNames.F117Hunt);
                    break;
                case "Khmeimim":
                    startScript(Scripts.scriptsNames.Khmeimim);
                    break;
            }
        }

        private void StatisticGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()+1).ToString();
        }
    }
}
