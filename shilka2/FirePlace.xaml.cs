using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Data;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace shilka2
{
    public partial class FirePlace : Window
    {
        public System.Timers.Timer Game = new System.Timers.Timer(30);
        public System.Timers.Timer HandMove = new System.Timers.Timer(600);
        public System.Timers.Timer AircraftsStart = new System.Timers.Timer(2000);
        public System.Timers.Timer School = new System.Timers.Timer(800);
        public System.Timers.Timer GameTimer = new System.Timers.Timer(1000);

        bool pause = false;
        bool endGameAlready = false;
        bool startGameAlready = false;
        bool startMenuShowYet = true;

        string statisticColor = "#FF5B5B5B";
        string startColor = "#FF343333";
        string endColor = "#FF0F0570";

        static bool SchoolTicTac = false;

        public FirePlace()
        {
            InitializeComponent();

            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;

            Statistic.aircraftAveragePrice = Statistic.GetAveragePrice();

            double heightForShilka = SystemParameters.PrimaryScreenHeight - ShilkaImg.Height;

            Aircrafts.minAltitudeGlobal = (int)(heightForShilka - ShilkaImg.Height);

            foreach(Canvas Menu in new List<Canvas>() { EndMenu, RestartTrainingMenu, StatisticMenu })
                Menu.Height = SystemParameters.PrimaryScreenHeight;

            foreach (Canvas Menu in new List<Canvas>() { EndMenu, RestartTrainingMenu, firePlaceDock })
                Menu.Margin = new Thickness(SystemParameters.PrimaryScreenWidth, 0, 0, 0);

            foreach (Canvas Menu in new List<Canvas>() { EndMenu, RestartTrainingMenu, firePlaceDock })
                Menu.Margin = new Thickness(SystemParameters.PrimaryScreenWidth, 0, 0, 0);

            StatisticMenu.Width = SystemParameters.PrimaryScreenWidth;
            StatisticMenu.Margin = new Thickness(0, SystemParameters.PrimaryScreenHeight, 0, 0);

            var converter = new BrushConverter();
            StatisticMenu.Background = (Brush)converter.ConvertFrom(statisticColor);
            StatisticGrid.ItemsSource = Statistic.Load();
            StatisticGrid.Margin = new Thickness(0, 50, 0, 0);
            StatisticGrid.Height = StatisticMenu.Height - Statistic.statisticGridMargins - 50;

            foreach (DataGrid Menu in new List<DataGrid>() { StatisticGrid, StatBoxTable, StatBoxDown, StatBoxDamag })
                Menu.Width = StatisticMenu.Width - Statistic.statisticGridMargins;

            StatBoxTable.Margin = new Thickness(0, 50, 0, 0);
            StatBoxTable.Height = 320;

            StatBoxTable.Background = StatisticMenu.Background;
            StatBoxTable.RowBackground = StatisticMenu.Background;
            StatBoxTable.BorderBrush = StatisticMenu.Background;

            StatNotSelected.Margin = StatBoxTable.Margin;

            double widthStatBox = (SystemParameters.PrimaryScreenWidth / 2) - 100;
            StatBoxDownLabel.Margin = new Thickness(0, 290, 0, 0);
            StatBoxDown.Margin = new Thickness(0, 300, 0, 0);
            StatBoxDown.Height = 150;
            StatBoxDown.Width = widthStatBox;

            double leftPadding = (SystemParameters.PrimaryScreenWidth / 2) - 60;
            StatBoxDamaglabel.Margin = new Thickness(leftPadding, 290, 0, 0);
            StatBoxDamag.Margin = new Thickness(leftPadding, 300, 0, 0);
            StatBoxDamag.Height = StatBoxDown.Height;
            StatBoxDamag.Width = StatBoxTable.Width - widthStatBox - 40;

            statShells.Width = StatisticGrid.Width;

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

            ToolTipService.ShowDurationProperty.OverrideMetadata(
                typeof(DependencyObject), new FrameworkPropertyMetadata(Int32.MaxValue));
        }

        public void StartGame(int?[] scriptAircraft, int?[] scriptHelicopters, int?[] scriptAircraftFriend,
            int?[] scriptHelicoptersFriend, int?[] scriptAirliners)
        {
            startMenuShowYet = false;

            if (startGameAlready)
            {
                Shilka.EndGameCleaning();
                pause = false;
                pauseButton.IsChecked = false;
                Shell.animationStop = false;
            }

            double trainingAdditions = ((Shilka.school || Shilka.training) ? 25 : 0);
            statShells.Margin = new Thickness(Constants.STAT_TEXT_TOP, Constants.STAT_TEXT_LEFT + trainingAdditions, 0, 0);

            Scripts.scriptAircraft = scriptAircraft;
            Scripts.scriptHelicopters = scriptHelicopters;
            Scripts.scriptAircraftFriend = scriptAircraftFriend;
            Scripts.scriptHelicoptersFriend = scriptHelicoptersFriend;
            Scripts.scriptAirliners = scriptAirliners;

            MoveCanvas(
                moveCanvas: StartMenu,
                prevCanvas: firePlaceDock,
                left: StartMenu.Margin.Left - StartMenu.ActualWidth,
                speed: 0.6
            );

            if (!startGameAlready)
            {
                Game.Elapsed += new ElapsedEventHandler(Shell.ShellsFire);
                Game.Elapsed += new ElapsedEventHandler(Shell.ShellsFly);
                Game.Elapsed += new ElapsedEventHandler(Case.CasesFly);
                Game.Elapsed += new ElapsedEventHandler(Weather.NewWeather);
                Game.Elapsed += new ElapsedEventHandler(Weather.WeatherElementsFly);
                Game.Elapsed += new ElapsedEventHandler(Aircraft.AircraftFly);
                Game.Elapsed += new ElapsedEventHandler(Statistic.Show);
                Game.Elapsed += new ElapsedEventHandler(Shilka.RadarmMalfunction);

                HandMove.Elapsed += new ElapsedEventHandler(HideHand);
                AircraftsStart.Elapsed += new ElapsedEventHandler(Aircraft.Start);
                School.Elapsed += new ElapsedEventHandler(SchoolShow);
                GameTimer.Elapsed += new ElapsedEventHandler(GameTimeTicTac);
            }
            HandMove.Enabled = true;
            HandMove.Start();

            Game.Enabled = true;
            Game.Start();

            AircraftsStart.Enabled = true;
            AircraftsStart.Start();

            Statistic.gameTimeSec = 0;
            GameTimer.Enabled = true;
            GameTimer.Start();

            if (Shilka.school || Shilka.training)
            {
                schoolLabel.Content = (Shilka.school ? "обучающий режим" : "тренировка");
                schoolLabel.Visibility = Visibility.Visible;
                School.Enabled = true;
                School.Start();
            }
            else
            {
                schoolLabel.Visibility = Visibility.Hidden;
                School.Enabled = false;
                School.Stop();
            }

            startGameAlready = true;
        }

        public static void HideHand(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;
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

        public static void GameTimeTicTac(object obj, ElapsedEventArgs e)
        {
            Statistic.gameTimeSec += 1;

            if (Weather.currentWeather != Weather.weatherTypes.good)
                Statistic.gameBadWeatherSec += 1;

            if (Shilka.fire)
                Statistic.shootingTimeSec += 1;
        }

        public static void SchoolShow(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;
                
                if (SchoolTicTac)
                {
                    main.schoolLabel.Background = Brushes.Black;
                    main.schoolLabel.Foreground = Brushes.White;
                    SchoolTicTac = false;
                }
                else
                {
                    main.schoolLabel.Background = Brushes.Transparent;
                    main.schoolLabel.Foreground = Brushes.Black;
                    SchoolTicTac = true;
                }
            }));
        }

        public void EndGame(string endText, string bgColor)
        {
            endGameAlready = true;

            Game.Stop();
            AircraftsStart.Stop();
            School.Stop();
            GameTimer.Stop();

            thunderPlace.Visibility = Visibility.Hidden;

            if (Weather.thunderCurrentImage != null)
                firePlace.Children.Remove(Weather.thunderCurrentImage);

            Canvas newMenu = (Shilka.school || Shilka.training ? RestartTrainingMenu : EndMenu);
                            
            var converter = new BrushConverter();
            newMenu.Background = (Brush)converter.ConvertFrom(bgColor);

            GameOverWithSave.IsEnabled = false;

            if (Shilka.school || Shilka.training)
                RestartText.Text = endText;
            else
            {
                EndText.Text = endText;
                playerPlaceHolder.Visibility = Visibility.Visible;
            }

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
                AircraftsStart.Stop();
            }

            StatBoxTable.Items.Clear();
            StatNotSelected.Visibility = Visibility.Visible;

            StatisticGrid.Margin = new Thickness(0, 50, 0, 0);
            StatisticGrid.Height = StatisticMenu.Height - Statistic.statisticGridMargins - 50;

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
                AircraftsStart.Start();
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
            
            if (!Shilka.reheatingGunBurrels && !pause)
            {
                if (Shilka.fire == false)
                    Statistic.shootingNumber += 1;

                Shilka.fire = true;
            }
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Shilka.fire = false;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Shilka.SetNewTergetPoint(e.GetPosition((Window)sender), sender);

            if (!pause)
            {
                if (Shilka.currentScript == Scripts.scriptsNames.Libya)
                    RadarImg.RenderTransform = new RotateTransform(Constants.RADAR_DAMAGED, 4, 20);
                else if (Shilka.currentScript != Scripts.scriptsNames.IranIraq)
                    RadarImg.RenderTransform = new RotateTransform(Shilka.lastDegree, 4, 20);
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            string endText;

            if (Shilka.school)
                endText = "Выход из обучения";
            else if (Shilka.training)
                endText = "Выход из тренировки";
            else
                endText = "Выход из игры.\nСохранить статистику?";

            if (!endGameAlready)
                EndGame(endText, endColor); 
        }

        private void RestartTraining_Click(object sender, RoutedEventArgs e)
        {
            Shilka.EndGameCleaning();

            if (Shilka.training)
            {
                Aircraft.TrainingStartCleaning();
                Aircraft.StartSuspendedTarget();
            }
            
            if (Shilka.school)
                Aircraft.SchoolStartCleaning();

            MoveCanvas(
                moveCanvas: firePlaceDock,
                prevCanvas: RestartTrainingMenu,
                left: 0,
                speed: 0.2
            );

            endGameAlready = false;

            Pause(stop: false);

            School.Start();
        }

        private void Pause(bool stop = true)
        {
            if (stop)
            {
                Shell.animationStop = true;
                Game.Stop();
                AircraftsStart.Stop();
                pause = true;
            }
            else
            {
                Shell.animationStop = false;
                Game.Start();
                AircraftsStart.Start();
                pause = false;
            }
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (endGameAlready)
                return;

            if (pause)
                Pause(stop: false);
            else
                Pause(stop: true);
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
            if (!String.IsNullOrEmpty(playerName))
                Statistic.Save(playerName.Replace("|", String.Empty));

            Canvas prevMenu = ( Shilka.school || Shilka.training ? RestartTrainingMenu : EndMenu);

            MoveCanvas(
                moveCanvas: firePlaceDock, 
                prevCanvas: prevMenu,
                left: firePlaceDock.Margin.Left + EndMenu.ActualWidth,
                speed: 0.2,
                secondAnimation: new EventHandler(EndGameSecAnimation)
            );

            Shilka.night = false;
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
            StatisticGrid.ItemsSource = Statistic.Load();
        }

        private void PlayerName_KeyUp(object sender, KeyEventArgs e)
        {
            GameOverWithSave.IsEnabled = (String.IsNullOrEmpty(PlayerName.Text) ? false : true);

            if (String.IsNullOrEmpty(PlayerName.Text))
                playerPlaceHolder.Visibility = Visibility.Visible;
            else
                playerPlaceHolder.Visibility = Visibility.Hidden;

            if (e.Key == Key.Enter)
                GameOver(PlayerName.Text);
        }

        private void playerPlaceHolder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PlayerName.Focus();
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

            Shilka.degreeOfHeatingGunBurrels = Scripts.ScriptTemperature(script);
            Shilka.degreeOfHeatingGunBurrelsMin = Shilka.degreeOfHeatingGunBurrels;

            Shilka.night = false;

            if (flagName != null)
                scenarioFlag.Source = Aircraft.ImageFromResources(flagName, Aircraft.ImageType.Interface);
            else
                scenarioFlag.Source = null;

            if (script == Scripts.scriptsNames.KoreanBoeing)
                Weather.RestartCycle(Weather.weatherTypes.snow);
            else if (script == Scripts.scriptsNames.Yugoslavia)
            {
                statShells.Foreground = Brushes.White;
                Shilka.night = true;
            }
            else
                statShells.Foreground = Brushes.Black;

            notebookBackground.Source = Aircraft.ImageFromResources("background", Aircraft.ImageType.Interface);
            ShilkaImg.Source = Aircraft.ImageFromResources("shilka", Aircraft.ImageType.Interface);

            StartGame(
                Scripts.ScriptEnemyAircrafts(script),
                Scripts.ScriptEnemyHelicopters(script),
                Scripts.ScriptFriendAircrafts(script),
                Scripts.ScriptFriendHelicopters(script),
                Scripts.ScriptAirliners(script)
            );
        }

        private void startScript_Click(object sender, RoutedEventArgs e)
        {
            startScript_Click(sender, e, school: false, training: false);
        }

        private void startScript_Click(object sender, RoutedEventArgs e,
            bool school = false, bool training = false)
        {
            string scriptName = "noScript";

            pauseButton.Focus();

            Shilka.school = school;
            Shilka.training = training;

            if (training)
            {
                Aircraft.TrainingStartCleaning();
                Aircraft.StartSuspendedTarget();

                Game.Elapsed += new ElapsedEventHandler(Shilka.CraneTruckGoOut);

                TruckCraneImg.Visibility = Visibility.Visible;
            }
            else
                TruckCraneImg.Visibility = Visibility.Hidden;

            if (!school && !training)
            {
                Button startButton = sender as Button;
                scriptName = startButton.Name;
            }

            Scripts.scriptsNames script;

            bool scriptParsed = Enum.TryParse(scriptName, out script);

            if (scriptParsed)
                StartScript(script);
            else
                StartScript(Scripts.scriptsNames.noScript);
        }

        private void schoolButton_Click(object sender, RoutedEventArgs e)
        {
            startScript_Click(null, null, school: true);
        }

        private void trainingButton_Click(object sender, RoutedEventArgs e)
        {
            startScript_Click(null, null, training: true);
        }

        public void SchoolMessage(string msg, Brush brush)
        {
            Pause(stop: true);

            schoolInfoText.Text = msg;

            schoolInfoBox.Width = SystemParameters.PrimaryScreenWidth;

            schoolInfoBox.Background = brush;
            schoolInfoText.Background = brush;

            schoolInfoBox.Visibility = Visibility.Visible;

            Canvas.SetZIndex(schoolInfoBox, 102);
        }

        private void schoolInfoBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double screenCenterPosition = (SystemParameters.PrimaryScreenHeight / 2) - (schoolInfoBox.ActualHeight / 2);
            schoolInfoBox.Margin = new Thickness(0, screenCenterPosition, 0, 0);
        }

        private void schoolInfo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            schoolInfoBox.Visibility = Visibility.Hidden;
            Shilka.fire = false;
            Pause(stop: false);
        }

        private void StatisticGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()+1).ToString();
        }

        private void StatBoxAddRow(string[] column)
        {
            string line = ' ' + new string('.', 100);

            StatBox data = new StatBox { Column1 = column[0] + line, Column2 = " " + column[1], Column3 = column[2] + line, Column4 = " " + column[3] };
            StatBoxTable.Items.Add(data);
        }

        private void StatisticGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid stat = (DataGrid)sender;
            StatTable statRow = (StatTable)stat.SelectedItem;

            StatBoxTable.Items.Clear();

            if (statRow == null)
            {
                foreach(DataGrid StatBox in new List<DataGrid>() { StatBoxDown, StatBoxDamag })
                {
                    StatBox.Items.Clear();
                    StatBox.Columns.Clear();
                }

                StatNotSelected.Visibility = Visibility.Visible;
                return;
            }
            else
                StatNotSelected.Visibility = Visibility.Hidden;

            StatisticGrid.Margin = new Thickness(0, 540, 0, 0);
            StatisticGrid.Height = StatisticMenu.Height - Statistic.statisticGridMargins - 590;

            int shellsForShutdown = (statRow.shutdown > 0 ? (int)statRow.shellsFired / statRow.shutdown : 0);
            string scriptName = Statistic.statisticScripts[stat.SelectedIndex];
            int shellInQueue = (statRow.shootNumber > 0 ? (int)statRow.shellsFired / statRow.shootNumber : 0);
            double timeForQueue = (statRow.shootNumber > 0 ? (double)statRow.shootTime / (double)statRow.shootNumber : 0);

            StatBoxAddRow(new String[] {
                "зенитчик", statRow.name,
                "сценарий", scriptName
            });
            StatBoxAddRow(new String[] {
                "сбито", String.Format("{0} ( {1}% )", statRow.shutdown, statRow.shutdownPercent), 
                "повреждено", String.Format("{0} ( {1}% )", statRow.damaged, statRow.damagedPercent) });
            StatBoxAddRow(new String[] {
                "настрел", String.Format("{0} снарядов", statRow.shellsFired),
                "упущенных", statRow.hasGone.ToString()
            });
            StatBoxAddRow(new String[] {
                "\u2014 из них в цель", String.Format("{0} ( {1}% )", statRow.inTarget, statRow.inTargetPercent),
                "\u2014 из них без повреждений", String.Format("{0}%", statRow.withoutDamage)
            });
            StatBoxAddRow(new String[] {
                "\u2014 из них на сбитый", String.Format("{0} выстр./сбитый", shellsForShutdown),
                "нанесён ущерб", Statistic.HumanReadableSumm(statRow.amountOfDamage)
            });
            StatBoxAddRow(new String[] {
                "повреждено своих", statRow.friendDamage.ToString(),
                "повреждено гражданских", statRow.airlinerDamage.ToString()
            });
            StatBoxAddRow(new String[] {
                "общее время боя", statRow.time.ToString(),
                "средняя очередь", String.Format("{0} снарядов", shellInQueue)
            });
            StatBoxAddRow(new String[] {
                "\u2014 из них непогоды", statRow.badTime.ToString(),
                "время средней очереди", string.Format("{0:f2} сек", timeForQueue)
            });
            StatBoxAddRow(new String[] {
                "удача", statRow.chance.ToString(),
                "лучший трофей", StatMostValuableTrophy(statRow.aircrafts)
            });

            StatBoxValues(StatBoxDown, statRow.aircrafts, statRow.shutdown, statRow.amountOfDamage);
            StatBoxValues(StatBoxDamag, statRow.aircraftsDamaged, statRow.damaged, statRow.amountOfDamage, inaccurate: true);
        }

        private string StatMostValuableTrophy(string statData)
        {
            string[] aircraftsData = statData.Split(',');

            string trophy = "не было";
            double trophyPrice = 0;

            foreach (string aircraftData in aircraftsData)
            {
                Aircraft aircraft = Aircrafts.FindEnemyAircraft(aircraftData.Split('=')[0]);

                if (aircraft == null)
                    continue;

                if (trophyPrice < aircraft.price)
                {
                    trophy = aircraft.aircraftName;
                    trophyPrice = aircraft.price;
                }
            }

            return trophy;
        }

        private void StatBoxValues(DataGrid StatBox, string statData, int percentBaseCount,
            double percentBasePrice, bool inaccurate = false)
        {
            StatBox.Items.Clear();
            StatBox.Columns.Clear();

            if (String.IsNullOrWhiteSpace(statData))
                return;

            StatBox.AutoGenerateColumns = false;
            StatBox.Columns.Add(new DataGridTextColumn
            {
                Header = "тип",
                Binding = new Binding("aircraft")
            });
            StatBox.Columns.Add(new DataGridTextColumn
            {
                Header = "количество",
                Binding = new Binding("count"),
                Width = new DataGridLength(100)
            });
            StatBox.Columns.Add(new DataGridTextColumn
            {
                Header = "от общего кол-ва",
                Binding = new Binding("percent"),
                Width = new DataGridLength(120)
            });
            StatBox.Columns.Add(new DataGridTextColumn
            {
                Header = "от ущерба" + (inaccurate ? " (примерно)" : String.Empty),
                Binding = new Binding("pricePercent"),
                Width = new DataGridLength(120)
            });

            string[] aircraftsData = statData.Split(',');
            string inacc = (inaccurate ? "до " : String.Empty);

            foreach (string aircraftData in aircraftsData)
            {
                dynamic newRow = new ExpandoObject();
                string[] data = aircraftData.Split('=');

                int count = Int32.Parse(data[1]);
                int percentCount = (count * 100) / percentBaseCount;

                Aircraft aircraft = Aircrafts.FindEnemyAircraft(data[0]);
                double price = aircraft.price * count;
                int percentPrice = (int)((price * 100) / percentBasePrice);
                
                ((IDictionary<string, object>)newRow)["aircraft"] = data[0];
                ((IDictionary<string, object>)newRow)["count"] = data[1];
                ((IDictionary<string, object>)newRow)["percent"] = String.Format("{0}%", percentCount);
                ((IDictionary<string, object>)newRow)["pricePercent"] = String.Format("{0}{1}%", inacc, percentPrice);
                StatBox.Items.Add(newRow);
            }
        }
    }
}
