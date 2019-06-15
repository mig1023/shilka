using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace shilka2
{
    class PancirS1
    {
        static int flashСount = 0;

        public static Scripts.scriptsNames currentScript;

        public static int degreeOfHeatingGunBurrelsMin = 0;
        public static int degreeOfHeatingGunBurrels = degreeOfHeatingGunBurrelsMin;
        private static bool schoolHeating = false;

        public static bool reheatingGunBurrels = false;

        static int gunReturn = 0;

        public static bool fire = false;
        public static double lastDegree = 0;
        public static bool school = false;

        static Random rand;

        public static List<Line> gunLines = new List<Line>();

        static PancirS1()
        {
            rand = new Random();
        }

        public static void EndGameCleaning()
        {
            Statistic.Clean();

            fire = false;
            reheatingGunBurrels = false;
            lastDegree = 0;
            Weather.currentWeather = Weather.weatherTypes.good;

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

        public static void SetNewTergetPoint(Point pt, object sender)
        {
            Shell.ptX = pt.X - Constants.FIRE_WIDTH_CORRECTION;
            Shell.ptY = (sender as Window).Height - pt.Y - Constants.FIRE_HEIGHT_POINT_CORRECTION;
            Shell.currentHeight = (sender as Window).Height;
            Shell.currentWidth = (sender as Window).Width + Constants.FIRE_WIDTH_CORRECTION;

            if (Shell.ptX < 0)
                Shell.ptX = 0;

            if (Shell.ptY < 0)
                Shell.ptY = 0;

            double LastSin = Shell.ptY / Math.Sqrt((Shell.ptX * Shell.ptX) + (Shell.ptY * Shell.ptY));
            double newLastDegree = Math.Asin(LastSin) * (180 / Math.PI) * -1;

            newLastDegree += Constants.LAST_DEGREE_CORRECTION;

            if (newLastDegree > 0)
                newLastDegree = 0;

            lastDegree = (double.IsNaN(newLastDegree) ? lastDegree : newLastDegree);
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
                case 1: return Brushes.DarkRed;
                case 2: return Brushes.Firebrick;
                case 3: return Brushes.Maroon;
                default: return Brushes.Red;
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
            foreach (var line in gunLines)
                main.pancirGunsPlace.Children.Remove(line);

            gunLines.Clear();

            double currentHeight = Shell.currentHeight;

            if (currentHeight < 0)
                currentHeight = main.ActualHeight;

            if (fire)
                gunReturn++;

            if (gunReturn > Constants.GUN_RETURN_TIMEOUT)
                gunReturn = 0;

            double[] mountXY = new double[2] { 0, 0 };

            for (int numGuns = 0; numGuns <= 1; numGuns++)
            {
                Line gun = new Line();
                gun.X1 = Constants.PANCIR_FIRE_WIDTH_CORRECTION - 3 - (12);
                gun.Y1 = currentHeight - Constants.PANCIR_FIRE_HEIGHT_CORRECTION + 5 - (9);

                int gunReturnLen = 0;
                if (fire && gunReturn < Constants.GUN_MIDDLE_TIMEOUT)
                    gunReturnLen = Constants.GUN_RETURN_LEN;

                gun.X2 = gun.X1 + (Constants.PANCIR_GUNS_LENGTH - gunReturnLen) * Shell.lastCos;
                gun.Y2 = gun.Y1 - (Constants.PANCIR_GUNS_LENGTH - gunReturnLen) * Shell.lastSin;

                mountXY[0] = gun.X1 + (Constants.PANCIR_GUNS_LENGTH - gunReturnLen - Constants.GUN_NOUNT_LENGTH) * Shell.lastCos;
                mountXY[1] = gun.Y1 - (Constants.PANCIR_GUNS_LENGTH - gunReturnLen - Constants.GUN_NOUNT_LENGTH) * Shell.lastSin;

                byte colorOfGuns = (
                    degreeOfHeatingGunBurrels > Constants.HEATING_COLOR_BASE ?
                    (byte)((degreeOfHeatingGunBurrels - Constants.HEATING_COLOR_BASE) / 2) : (byte)0
                );

                if (colorOfGuns == 0)
                    gun.Stroke = Brushes.Black;
                else
                    gun.Stroke = new SolidColorBrush(Color.FromRgb((byte)(colorOfGuns - Constants.HEATING_COLOR_BASE), 0, 0));

                gun.StrokeThickness = Constants.PANCIR_GUN_THICKNESS;

                main.pancirGunsPlace.Children.Add(gun);

                Canvas.SetZIndex(gun, 200);
                gunLines.Add(gun);

                if (fire)
                    DrawGansFlashs(main, gun, numGuns, gun.StrokeThickness);
            }
        }
    }
}
