using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Media.Imaging;

namespace shilka2
{
    class Shilka
    {
        static int flashСount = 0;

        public static Scripts.scriptsNames currentScript;
        public static bool night = false; 

        public static int degreeOfHeatingGunBurrelsMin = 0;
        public static int degreeOfHeatingGunBurrels = degreeOfHeatingGunBurrelsMin;
        private static bool schoolHeating = false;

        public static bool reheatingGunBurrels = false;

        static int gunReturn = 0;

        public static bool fire = false;
        public static double lastDegree = 0;
        public static bool school = false;
        public static bool training = false;
        
        public static int radarMalfunction = 0;
        public static int radarMalfunctionDirection = 0;
        public static int radarMalfunctionDelay = 0;

        public static int gunMalfunction = 0;

        private static bool craneTruckMove = false;

        static Random rand;

        static Shilka()
        {
            rand = new Random();
        }

        public static void EndGameCleaning()
        {
            Statistic.Clean();

            fire = false;
            reheatingGunBurrels = false;
            lastDegree = 0;
            Aircraft.allAircraftsInGame = 0;
            Weather.RestartCycle(Weather.weatherTypes.good);

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                main.firePlace.Children.Clear();
                main.PlayerName.Text = String.Empty;
                main.schoolInfoBox.Visibility = Visibility.Hidden;

                Aircraft.aircrafts.Clear();
                Case.cases.Clear();
                Shell.shells.Clear();
                Weather.weather.Clear();
            }));
        }

        public static void RadarmMalfunction(object obj, ElapsedEventArgs e)
        {
            if (currentScript == Scripts.scriptsNames.IranIraq)
            {
                Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
                {
                    FirePlace main = (FirePlace)Application.Current.MainWindow;
                    main.RadarImg.RenderTransform = new RotateTransform(RadarmMalfunction(), 4, 20);
                }));
            }
        }

        public static void CraneTruckGoOut(object obj, ElapsedEventArgs e)
        {
            if (!Aircraft.suspendedTargetDowned)
                return;

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                if (main.TruckCraneImg.Visibility == Visibility.Hidden)
                    return;

                craneTruckMove = !craneTruckMove;
                string craneTrackSource = "truck_crane" + (craneTruckMove ? "_move" : "");
                main.TruckCraneImg.Source = Aircraft.ImageFromResources(craneTrackSource, Aircraft.ImageType.Interface);

                if (main.TruckCraneImg.Margin.Left > SystemParameters.PrimaryScreenWidth)
                    main.TruckCraneImg.Visibility = Visibility.Hidden;
                else
                    main.TruckCraneImg.Margin = new Thickness(
                        main.TruckCraneImg.Margin.Left + 1,
                        main.TruckCraneImg.Margin.Top,
                        main.TruckCraneImg.Margin.Right,
                        main.TruckCraneImg.Margin.Bottom
                    );
            }));
        }

        public static double RadarmMalfunction()
        {
            if ((radarMalfunctionDelay > 10) && (rand.Next(2) == 1))
                radarMalfunctionDelay = 0;
            else if (radarMalfunctionDelay > 10)
                radarMalfunctionDirection = rand.Next(10) - 5;
            else
                radarMalfunctionDelay += 1;

            radarMalfunction -= radarMalfunctionDirection;

            if (radarMalfunction > 0)
            {
                radarMalfunction = 0;
                radarMalfunctionDirection = rand.Next(10) * -1;
            }
            else if (radarMalfunction < -130)
            {
                radarMalfunction = -130;
                radarMalfunctionDirection = rand.Next(10);
            }

            return radarMalfunction;
        }

        public static double GunMalfunction()
        {
            if (rand.Next(3) == 1)
                gunMalfunction = rand.Next(400) - 200;

            return gunMalfunction;
        }

        public static void SetNewTergetPoint(Point pt, object sender)
        {
            Shell.ptX = pt.X - Constants.FIRE_WIDTH_CORRECTION;
            Shell.ptY = (sender as Window).Height - pt.Y - Constants.FIRE_HEIGHT_POINT_CORRECTION;
            Shell.currentHeight = (sender as Window).Height;
            Shell.currentWidth = (sender as Window).Width + Constants.FIRE_WIDTH_CORRECTION;

            if (Shilka.currentScript == Scripts.scriptsNames.IranIraq)
                Shell.ptX -= GunMalfunction();

            if (Shell.ptX < 0)
                Shell.ptX = 0;

            if (Shell.ptY < 0)
                Shell.ptY = 0;

            double LastSin = Shell.ptY / Math.Sqrt((Shell.ptX * Shell.ptX) + (Shell.ptY * Shell.ptY));
            double newLastDegree = Math.Asin(LastSin) * (180 / Math.PI) * -1;

            newLastDegree += Constants.LAST_DEGREE_CORRECTION;

            if (newLastDegree > 0)
                newLastDegree = 0;

            lastDegree = ( double.IsNaN(newLastDegree) ? lastDegree : newLastDegree );
        }

        public static void HeatingOfGuns(bool shooting)
        {
            if (rand.Next(2) == 1)
                return;

            degreeOfHeatingGunBurrels += (shooting ? 1 : -1);

            int currentDegreeOfHeatinMin = degreeOfHeatingGunBurrelsMin;

            if (Weather.currentWeather == Weather.weatherTypes.rain)
                currentDegreeOfHeatinMin = currentDegreeOfHeatinMin + Constants.HEATING_IN_RAIN;
            else if (Weather.currentWeather == Weather.weatherTypes.snow)
                currentDegreeOfHeatinMin = currentDegreeOfHeatinMin + Constants.HEATING_UNDER_SNOW;

            if (degreeOfHeatingGunBurrels < currentDegreeOfHeatinMin)
                degreeOfHeatingGunBurrels = currentDegreeOfHeatinMin;
            else if (degreeOfHeatingGunBurrels > Constants.GUNS_OVERHEATING)
            {
                reheatingGunBurrels = true;
                fire = false;
            }
            else if (degreeOfHeatingGunBurrels < Constants.GUNS_HEAT_UP)
                reheatingGunBurrels = false;

            if (Shilka.school && !schoolHeating && (degreeOfHeatingGunBurrels > Constants.GUNS_HEAT_UP))
            {
                schoolHeating = true;

                Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
                {
                    FirePlace main = (FirePlace)Application.Current.MainWindow;
                    main.SchoolMessage(Constants.HEATING_INFORMATION, Brushes.Orange);
                }));
            }
        }

        public static Brush FlashesColor()
        {
            switch (rand.Next(4))
            {
                case 1:
                    return Brushes.DarkRed;
                case 2:
                    return Brushes.Firebrick;
                case 3:
                    return Brushes.Maroon;
                default:
                    return Brushes.Red;
            }
        }

        public static void DrawGansFlashs(FirePlace main, Line gun, int numGuns, double flashThickness)
        {
            flashСount++;

            if (flashСount >= 10)
                flashСount = 0;

            if (
                ((flashСount >= 2) && (flashСount < 5) && (numGuns == 0))
                ||
                ((flashСount >= 7) && (numGuns == 1))
            )
            {
                Line flash = new Line();

                flash.X1 = gun.X2;
                flash.Y1 = gun.Y2;

                int flashSize = rand.Next(1, 4);

                flash.X2 = gun.X2 + flashSize * Shell.lastCos;
                flash.Y2 = gun.Y2 - flashSize * Shell.lastSin;

                flash.Stroke = FlashesColor();

                flash.StrokeThickness = flashThickness;
                main.firePlace.Children.Add(flash);
                Canvas.SetZIndex(flash, 210);
                Shell.allLines.Add(flash);
            }
        }

        public static void DrawGuns(FirePlace main)
        {
            double currentHeight = Shell.currentHeight;

            if (currentHeight < 0)
                currentHeight = main.ActualHeight;

            if (fire)
                gunReturn++;

            if (gunReturn > Constants.GUN_RETURN_TIMEOUT)
                gunReturn = 0;

            double[,] mountXY = new double[2, 2] { { 0, 0 }, { 0, 0 } };

            for (int numGuns = 0; numGuns <= 1; numGuns++)
            {
                Line gun = new Line();
                gun.X1 = Constants.FIRE_WIDTH_CORRECTION - 3 - (12 * numGuns);
                gun.Y1 = currentHeight - Constants.FIRE_HEIGHT_CORRECTION + 5 - (9 * numGuns);

                int gunReturnLen = 0;
                if ( fire && (
                        (gunReturn < Constants.GUN_MIDDLE_TIMEOUT && numGuns == 0)
                        ||
                        (gunReturn >= Constants.GUN_MIDDLE_TIMEOUT && numGuns == 1)
                    ) )
                    gunReturnLen = Constants.GUN_RETURN_LEN;

                gun.X2 = gun.X1 + (Constants.GUNS_LENGTH - gunReturnLen) * Shell.lastCos;
                gun.Y2 = gun.Y1 - (Constants.GUNS_LENGTH - gunReturnLen) * Shell.lastSin;

                mountXY[numGuns, 0] = gun.X1 + (Constants.GUNS_LENGTH - gunReturnLen - Constants.GUN_NOUNT_LENGTH) * Shell.lastCos;
                mountXY[numGuns, 1] = gun.Y1 - (Constants.GUNS_LENGTH - gunReturnLen - Constants.GUN_NOUNT_LENGTH) * Shell.lastSin;

                byte colorOfGuns = (
                    degreeOfHeatingGunBurrels > Constants.HEATING_COLOR_BASE ?
                    (byte)((degreeOfHeatingGunBurrels - Constants.HEATING_COLOR_BASE) / 2) : (byte)0
                );


                if (Shilka.night)
                {
                    int color = (degreeOfHeatingGunBurrels < Constants.HEATING_COLOR_BASE ? 0 : 360 - degreeOfHeatingGunBurrels);
                    gun.Stroke = (color > 0 ? new SolidColorBrush(Color.FromRgb((byte)255, (byte)color, (byte)color)) : Brushes.White);
                }
                else if (colorOfGuns == 0)
                    gun.Stroke = (Shilka.night ? Brushes.White : Brushes.Black);
                else
                    gun.Stroke = new SolidColorBrush(Color.FromRgb((byte)(colorOfGuns - Constants.HEATING_COLOR_BASE), 0, 0));

                gun.StrokeThickness = Constants.GUN_THICKNESS;
                main.firePlace.Children.Add(gun);
                Canvas.SetZIndex(gun, 200);
                Shell.allLines.Add(gun);

                if (fire)
                    DrawGansFlashs(main, gun, numGuns, gun.StrokeThickness);
            }

            Line gunMount = new Line();

            gunMount.X1 = mountXY[0, 0];
            gunMount.Y1 = mountXY[0, 1];
            gunMount.X2 = mountXY[1, 0];
            gunMount.Y2 = mountXY[1, 1];

            gunMount.StrokeThickness = 1;
            gunMount.Stroke = (Shilka.night ? Brushes.White : Brushes.Black);

            main.firePlace.Children.Add(gunMount);
            Canvas.SetZIndex(gunMount, 200);
            Shell.allLines.Add(gunMount);
        }
    }
}
