using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;

namespace shilka2
{
    class Shilka
    {
        const double LAST_DEGREE_CORRECTION = 10;
        const int GUNS_LENGTH = 30;

        static int flashСount = 0;

        static string statisticFileName = "statistic.dat";
        public static int statisticGridMargins = 120;

        public static int statisticShellsFired = 0;
        public static int staticticInTarget = 0;
        public static int statisticAllAircraft = 0;
        public static double statisticPriceOfAllAircrafts = 0;
        public static int staticticAircraftShutdown = 0;
        public static int statisticHasGone = 0;
        public static int statisticDamaged = 0;
        public static int statisticFriendDamage = 0;
        public static double statisticAmountOfDamage = 0;

        public static double statisticLastDamagePrice;
        public static bool statisticShutdownFlag = false;
        public static string statisticLastDamageType;
        public static Scripts.scriptsNames currentScript;

        public static int degreeOfHeatingGunBurrels = 30;
        public static bool reheatingGunBurrels = false;

        public static double lastDegree = 0;

        static Random rand;

        static Shilka()
        {
            rand = new Random();
        }

        public static void SetNewTergetPoint(Point pt, object sender)
        {
            Shell.ptX = pt.X - Shell.FIRE_WIDTH_CORRECTION;
            Shell.ptY = (sender as Window).Height - pt.Y - Shell.FIRE_HEIGHT_POINT_CORRECTION;
            Shell.currentHeight = (sender as Window).Height;
            Shell.currentWidth = (sender as Window).Width + Shell.FIRE_WIDTH_CORRECTION;

            if (Shell.ptX < 0) Shell.ptX = 0;
            if (Shell.ptY < 0) Shell.ptY = 0;

            double LastSin = Shell.ptY / Math.Sqrt((Shell.ptX * Shell.ptX) + (Shell.ptY * Shell.ptY));
            lastDegree = Math.Asin(LastSin) * (180 / Math.PI) * -1;

            lastDegree += LAST_DEGREE_CORRECTION;
            if (lastDegree > 0) lastDegree = 0;
        }

        public static void HeatingOfGuns(bool shooting)
        {
            if (rand.Next(2) == 1) return;

            if (shooting)
                degreeOfHeatingGunBurrels++;
            else
                degreeOfHeatingGunBurrels--;

            if (degreeOfHeatingGunBurrels < 30)
                degreeOfHeatingGunBurrels = 30;
            else if (degreeOfHeatingGunBurrels > 350)
            {
                reheatingGunBurrels = true;
                Shell.Fire = false;
            }
            else if(degreeOfHeatingGunBurrels < 300)
                reheatingGunBurrels = false;
        }

        public static void DrawGuns(MainWindow main)
        {

            double currentHeight = Shell.currentHeight;
            if (currentHeight < 0) currentHeight = main.ActualHeight;

            for (int numGuns = 0; numGuns <= 1; numGuns++)
            {
                Line gun = new Line();
                gun.X1 = Shell.FIRE_WIDTH_CORRECTION - 3 - (12 * numGuns);
                gun.Y1 = currentHeight - Shell.FIRE_HEIGHT_CORRECTION + 5 - (9 * numGuns);
                gun.X2 = gun.X1 + GUNS_LENGTH * Shell.LastCos;
                gun.Y2 = gun.Y1 - GUNS_LENGTH * Shell.LastSin;
                gun.Stroke = Brushes.Black;
                gun.StrokeThickness = 3;
                main.firePlace.Children.Add(gun);
                Canvas.SetZIndex(gun, 200);
                Shell.allLines.Add(gun);

                if (Shell.Fire)
                {
                    flashСount++;
                    if (flashСount >= 10) flashСount = 0;

                    if (
                        ( (flashСount >= 2) && (flashСount < 5) && (numGuns == 0) )
                        ||
                        ( (flashСount >= 7) && (numGuns == 1) )
                    ) {
                        Line flash = new Line();
                        flash.X1 = gun.X2;
                        flash.Y1 = gun.Y2;

                        int flashSize = rand.Next(3) + 5;

                        flash.X2 = gun.X2 + flashSize * Shell.LastCos;
                        flash.Y2 = gun.Y2 - flashSize * Shell.LastSin;

                        int flashColor = rand.Next(4);

                        if (flashColor == 0)
                            flash.Stroke = Brushes.Red;
                        else if (flashColor == 1)
                            flash.Stroke = Brushes.DarkRed;
                        else if (flashColor == 2)
                            flash.Stroke = Brushes.Firebrick;
                        else if (flashColor == 3)
                            flash.Stroke = Brushes.Maroon;

                        flash.StrokeThickness = rand.Next(2) + 4;
                        main.firePlace.Children.Add(flash);
                        Canvas.SetZIndex(flash, 210);
                        Shell.allLines.Add(flash);
                    }
                }
            }
        }

        static void Statistic(out int baseForPercent, out int shutdownPercent, out int damagedPercent,
            out int statisticWithoutDamage, out double chance, out int inTargetPercent)
        {
            baseForPercent = (
                (statisticHasGone > 0 || staticticAircraftShutdown > 0) ? (100 / (statisticHasGone + staticticAircraftShutdown)) : 100
            );

            shutdownPercent = (staticticAircraftShutdown * baseForPercent) | 0;
            damagedPercent = (statisticDamaged * baseForPercent);
            statisticWithoutDamage = ((statisticHasGone - statisticDamaged) * baseForPercent);
            inTargetPercent = ( (statisticShellsFired > 0) ? staticticInTarget * 100 / statisticShellsFired : 0 );

            chance = (double)statisticPriceOfAllAircrafts / (statisticAllAircraft * (double)Aircraft.AIRCRAFT_AVERAGE_PRICE);
            if (double.IsNaN(chance)) chance = 0;
        }

        public static void StatisticSave(string player)
        {
            int baseForPercent, shutdownPercent, damagedPercent, statisticWithoutDamage, inTargetPercent;

            double chance;
            string currentScriptName = "";

            Statistic(out baseForPercent, out shutdownPercent, out damagedPercent, out statisticWithoutDamage,
                out chance, out inTargetPercent);

            if (currentScript == Scripts.scriptsNames.noScript)
                currentScriptName = "быстрая игра";
            else if (currentScript == Scripts.scriptsNames.Vietnam)
                currentScriptName = "Вьетнам";
            else if (currentScript == Scripts.scriptsNames.IranIraq)
                currentScriptName = "Ирано-Иракская";
            else if (currentScript == Scripts.scriptsNames.DesertStorm)
                currentScriptName = "Буря в пустыни";
            else if (currentScript == Scripts.scriptsNames.Syria)
                currentScriptName = "Сирия";
            else if (currentScript == Scripts.scriptsNames.Yugoslavia)
                currentScriptName = "Югославия";
            else if (currentScript == Scripts.scriptsNames.KoreanBoeing)
                currentScriptName = "корейский Боинг";
            else if (currentScript == Scripts.scriptsNames.Libya)
                currentScriptName = "Ливия";

            string stat = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}\n",
                  player, currentScriptName, statisticShellsFired, staticticInTarget, staticticAircraftShutdown,
                  inTargetPercent, shutdownPercent, statisticDamaged, damagedPercent, statisticHasGone,
                  statisticWithoutDamage, statisticAmountOfDamage, statisticFriendDamage, chance
            );

            File.AppendAllText("statistic.dat", stat);
        }

        public static List<StatTable> LoadStatistic()
        {
            if (!File.Exists(statisticFileName))
            {
                List<StatTable> emptyResult = new List<StatTable>(0);
                return emptyResult;
            }

            string statisticText = File.ReadAllText(statisticFileName);
            string[] statisticLines = statisticText.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            List<StatTable> result = new List<StatTable>(statisticLines.Length);

            foreach (string statLine in statisticLines)
            {
                string[] stat = statLine.Split('|');

                result.Add(new StatTable(
                    stat[0], stat[1], stat[2], stat[3], stat[4], stat[5], stat[6],
                    stat[7], stat[8], stat[9], stat[10], stat[11], stat[12], stat[13]
                ));
            }

            return result;
        }

        public static void StatisticShow(object obj, ElapsedEventArgs e)
        {
            string stat = "";
            int baseForPercent, shutdownPercent, damagedPercent, statisticWithoutDamage, inTargetPercent;
            double chance;

            Statistic(out baseForPercent, out shutdownPercent, out damagedPercent, out statisticWithoutDamage,
                out chance, out inTargetPercent);

            if (statisticShellsFired > 0) stat += "Выстрелов: " + statisticShellsFired + "\n";

            if (staticticInTarget > 0) 
                stat += "Попаданий: " + staticticInTarget + " ( " + inTargetPercent + "% )\n";

            if (staticticAircraftShutdown > 0)
                stat += "Сбито: " + staticticAircraftShutdown + " ( " + shutdownPercent + "% )";

            if (statisticDamaged > 0)
                stat += (staticticAircraftShutdown == 0 ? "П" : " +п") + "овреждено: " + statisticDamaged + " ( " + damagedPercent + "% )\n";
            else if (staticticAircraftShutdown > 0)
                stat += "\n";
                
            if (statisticHasGone > 0)
            {
                stat += "Упущено: " + statisticHasGone + " ( " + (statisticHasGone * baseForPercent) + "% )";

                if (statisticDamaged < statisticHasGone)
                    stat += ", в том числе неповредённых: " + (statisticHasGone - statisticDamaged) + " ( " + statisticWithoutDamage + "% )";

                stat += "\n";
            }

            if (statisticAmountOfDamage > 0)
            {
                stat += "Нанесён ущерб: ";

                if (statisticAmountOfDamage < 1000)
                    stat += string.Format("{0:f2}", statisticAmountOfDamage) + " млн $";
                else if (statisticAmountOfDamage < 1000000)
                    stat += string.Format("{0:f2}", (double)statisticAmountOfDamage / 1000) + " млрд $";
                else
                    stat += string.Format("{0:f2}", (double)statisticAmountOfDamage / 1000000) + " трлн $";

                if (statisticShutdownFlag)
                    stat += " ( +" + statisticLastDamagePrice + " млн $ сбит " + statisticLastDamageType + " )";
                else
                    stat += string.Format(" ( +{0:f2} млн $ повреждён ", statisticLastDamagePrice) + statisticLastDamageType + " )";

                stat += "\n";
            }
                   
            if (statisticFriendDamage > 0) stat += "Повреждено своих: " + statisticFriendDamage + "\n";

            if (statisticAllAircraft > 0) stat += string.Format("Удача: {0:f2}", chance ) + "\n";

            if (statisticShellsFired > 0)
            {
                stat += "Температура стволов: " + degreeOfHeatingGunBurrels + "°C";

                if (reheatingGunBurrels) stat += " - перегрев стволов!\n";

                stat += "\n";
            }

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                main.statShells.Content = stat;
            }));
        }
    }
}
