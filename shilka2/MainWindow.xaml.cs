﻿using System;
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
        bool pause = false;
        bool endGameAlready = false;
        bool startGameAlready = false;
        bool startMenuShowYet = true;

        string statisticColor = "#FF5B5B5B";
        string startColor = "#FF343333";
        string endColor = "#FF0F0570";

        public MainWindow()
        {
            InitializeComponent();

            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;

            double heightForShilka = SystemParameters.PrimaryScreenHeight - ShilkaImg.Height;

            Aircraft.minAltitudeGlobal = (int)(heightForShilka - ShilkaImg.Height);

            EndMenu.Height = SystemParameters.PrimaryScreenHeight;
            EndMenu.Margin = new Thickness(SystemParameters.PrimaryScreenWidth, 0, 0, 0);

            RestartTrainingMenu.Height = SystemParameters.PrimaryScreenHeight;
            RestartTrainingMenu.Margin = new Thickness(SystemParameters.PrimaryScreenWidth, 0, 0, 0);

            firePlaceDock.Margin = new Thickness(SystemParameters.PrimaryScreenWidth, 0, 0, 0);

            StatisticMenu.Height = SystemParameters.PrimaryScreenHeight;
            StatisticMenu.Width = SystemParameters.PrimaryScreenWidth;
            StatisticMenu.Margin = new Thickness(0, SystemParameters.PrimaryScreenHeight, 0, 0);

            var converter = new BrushConverter();
            StatisticMenu.Background = (Brush)converter.ConvertFrom(statisticColor);

            StatisticGrid.ItemsSource = Statistic.Load();

            StatisticGrid.Height = StatisticMenu.Height - Statistic.statisticGridMargins;
            StatisticGrid.Width = StatisticMenu.Width - Statistic.statisticGridMargins;

            StartMenu.Height = StatisticMenu.Height;
            StartMenu.Width = StatisticMenu.Width;
            StartMenu.Margin = new Thickness(0, 0, 0, 0);

            StartMenuButtons.Margin = new Thickness(
                (StartMenu.Width / 2 - StartMenuButtons.Width / 2), (StartMenu.Height / 2 - StartMenuButtons.Height / 2), 0, 0
            );

            StartMenu.Background = (Brush)converter.ConvertFrom(startColor);

            ShilkaImg.Margin = new Thickness(0, heightForShilka, 0, 0);
            RadarImg.Margin = new Thickness(62, heightForShilka, 0, 0);
            HandImg.Margin = new Thickness(65, (heightForShilka - 120), 0, 0);
        }

        public void StartGame(int?[] scriptAircraft, int?[] scriptHelicopters,
            int?[] scriptAircraftFriend, int?[] scriptHelicoptersFriend)
        {
            startMenuShowYet = false;

            if (startGameAlready)
            {
                Shilka.EndGameCleaning();
                pause = false;
                pauseButton.IsChecked = false;
                Shell.animationStop = false;
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

            Canvas newMenu = (Shilka.school ? RestartTrainingMenu : EndMenu);
                            
            var converter = new BrushConverter();
            newMenu.Background = (Brush)converter.ConvertFrom(bgColor);

            if (Shilka.school)
                RestartText.Content = endText;
            else
                EndText.Content = endText;

            MoveCanvas(
                moveCanvas: newMenu,
                prevCanvas: firePlaceDock,
                left: newMenu.Margin.Left - newMenu.ActualWidth,
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

            if (StartMenu.Margin.Left < 0 && !pause && !endGameAlready)
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

            if (secondAnimation != null)
                move.Completed += secondAnimation;

            prevCanvas.BeginAnimation(MarginProperty, move);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Shilka.SetNewTergetPoint(e.GetPosition((Window)sender), sender);

            if (!Shilka.reheatingGunBurrels)
                Shell.fire = true;
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Shell.fire = false;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Shilka.SetNewTergetPoint(e.GetPosition((Window)sender), sender);

            if (!pause)
                RadarImg.RenderTransform = new RotateTransform(Shilka.lastDegree, 4, 20);
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            string endText = (Shilka.school ? "Выход из обучения" : "Выход из игры.\nСохранить статистику?");

            if (!endGameAlready)
                EndGame(endText, endColor); 
        }

        private void GameOverTraining_Click(object sender, RoutedEventArgs e)
        {
            Shilka.EndGameCleaning();

            MoveCanvas(
                moveCanvas: firePlaceDock,
                prevCanvas: RestartTrainingMenu,
                left: 0,
                speed: 0.2
            );

            endGameAlready = false;

            Shell.animationStop = false;
            Game.Start();
            Aircrafts.Start();
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (endGameAlready)
                return;

            if (pause)
            {
                Shell.animationStop = false;
                Game.Start();
                Aircrafts.Start();
                pause = false;
            }
            else
            {
                Shell.animationStop = true;
                Game.Stop();
                Aircrafts.Stop();
                pause = true;
            }
        }

        private void EndGameSecAnimation(object Sender, EventArgs e)
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
            if (playerName != "")
                Statistic.Save(playerName);

            Canvas prevMenu = ( Shilka.school ? RestartTrainingMenu : EndMenu);

            MoveCanvas(
                moveCanvas: firePlaceDock, 
                prevCanvas: prevMenu,
                left: firePlaceDock.Margin.Left + EndMenu.ActualWidth,
                speed: 0.2,
                secondAnimation: new EventHandler(EndGameSecAnimation)
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

        public void StartScript(Scripts.scriptsNames script)
        {
            Shilka.currentScript = script;

            string flagName = Scripts.ScriptFlagName(script);

            if (flagName != null)
                scenarioFlag.Source = Aircraft.ImageFromResources(flagName);
            else
                scenarioFlag.Source = null;

            StartGame(
                Scripts.ScriptEnemyAircrafts(script),
                Scripts.ScriptEnemyHelicopters(script),
                Scripts.ScriptFriendAircrafts(script),
                Scripts.ScriptFriendHelicopterss(script)
            );
        }

        private void startScript_Click(object sender, RoutedEventArgs e)
        {
            string scriptName = "noScript";

            if (sender == null)
                Shilka.school = true;
            else
            {
                Button startButton = sender as Button;
                scriptName = startButton.Name;
                Shilka.school = false;
            }

            switch (scriptName)
            {
                case "Vietnam":
                    StartScript(Scripts.scriptsNames.Vietnam);
                    break;
                case "DesertStorm":
                    StartScript(Scripts.scriptsNames.DesertStorm);
                    break;
                case "Yugoslavia":
                    StartScript(Scripts.scriptsNames.Yugoslavia);
                    break;
                case "IranIraq":
                    StartScript(Scripts.scriptsNames.IranIraq);
                    break;
                case "Syria":
                    StartScript(Scripts.scriptsNames.Syria);
                    break;
                case "KoreanBoeing":
                    StartScript(Scripts.scriptsNames.KoreanBoeing);
                    break;
                case "Libya":
                    StartScript(Scripts.scriptsNames.Libya);
                    break;
                case "Yemen":
                    StartScript(Scripts.scriptsNames.Yemen);
                    break;
                case "Rust":
                    StartScript(Scripts.scriptsNames.Rust);
                    break;
                case "F117Hunt":
                    StartScript(Scripts.scriptsNames.F117Hunt);
                    break;
                case "Khmeimim":
                    StartScript(Scripts.scriptsNames.Khmeimim);
                    break;
                default:
                    StartScript(Scripts.scriptsNames.noScript);
                    break;
            }
        }

        private void schoolButton_Click(object sender, RoutedEventArgs e)
        {
            startScript_Click(null, null);
        }

        private void StatisticGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()+1).ToString();
        }
    }
}
