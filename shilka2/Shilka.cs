using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        static int flash_count = 0;

        public static int statisticShellsFired = 0;
        public static int staticticInTarget = 0;
        public static int statisticAllAircraft = 0;
        public static int statisticPriceOfAllAircrafts = 0;
        public static int staticticAircraftShutdown = 0;
        public static int statisticHasGone = 0;
        public static int statisticDamaged = 0;
        public static int statisticFriendDamage = 0;
        public static int statisticAmountOfDamage = 0;
        public static string statisticLastDamage;

        public static void SetNewTergetPoint(Point pt, object sender)
        {
            Shell.ptX = pt.X - Shell.FIRE_WIDTH_CORRECTION;
            Shell.ptY = (sender as Window).Height - pt.Y - Shell.FIRE_HEIGHT_POINT_CORRECTION;
            Shell.currentHeight = (sender as Window).Height;
            Shell.currentWidth = (sender as Window).Width + Shell.FIRE_WIDTH_CORRECTION;

            if (Shell.ptX < 0) Shell.ptX = 0;
            if (Shell.ptY < 0) Shell.ptY = 0;
        }

        public static void DrawGuns(MainWindow main)
        {
            double currentHeight = Shell.currentHeight;
            if (currentHeight < 0) currentHeight = main.ActualHeight;

            for (int num_guns = 0; num_guns <= 1; num_guns++)
            {
                Line gun = new Line();
                gun.X1 = Shell.FIRE_WIDTH_CORRECTION - 3 - (12 * num_guns);
                gun.Y1 = currentHeight - Shell.FIRE_HEIGHT_CORRECTION + 5 - (9 * num_guns);
                gun.X2 = gun.X1 + 30 * Shell.LastCos;
                gun.Y2 = gun.Y1 - 30 * Shell.LastSin;
                gun.Stroke = Brushes.Black;
                gun.StrokeThickness = 3;
                main.firePlace.Children.Add(gun);
                Canvas.SetZIndex(gun, 200);
                Shell.allLines.Add(gun);

                if (Shell.Fire)
                {
                    flash_count++;
                    if (flash_count >= 20)
                        flash_count = 0;
                    else
                        flash_count++;

                    if (((flash_count >= 5) && (flash_count < 10) && (num_guns == 0)) ||
                        ((flash_count >= 15) && (num_guns == 1)))
                    {
                        Line flash = new Line();
                        flash.X1 = gun.X2;
                        flash.Y1 = gun.Y2;
                        flash.X2 = gun.X2 + 5 * Shell.LastCos;
                        flash.Y2 = gun.Y2 - 5 * Shell.LastSin;
                        flash.Stroke = Brushes.Red;
                        flash.StrokeThickness = 5;
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
            chance = (double)statisticPriceOfAllAircrafts / (statisticAllAircraft * (double)Aircraft.AIRCRAFT_AVERAGE_PRICE);
            inTargetPercent = ( (statisticShellsFired > 0) ? staticticInTarget * 100 / statisticShellsFired : 0 );
        } 

        public static void StatisticSave()
        {
            int baseForPercent, shutdownPercent, damagedPercent, statisticWithoutDamage, inTargetPercent;
            double chance;

            Statistic(out baseForPercent, out shutdownPercent, out damagedPercent, out statisticWithoutDamage,
                out chance, out inTargetPercent);

            string stat = String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}\n",
                  statisticShellsFired, staticticInTarget, staticticAircraftShutdown, inTargetPercent, staticticAircraftShutdown,
                  shutdownPercent, statisticDamaged, damagedPercent, statisticHasGone, statisticWithoutDamage, statisticAmountOfDamage,
                  statisticFriendDamage, chance
            );

            File.AppendAllText("statistic.dat", stat);
        }

        public static void StatisticShow(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;

                string stat = "";
                int baseForPercent, shutdownPercent, damagedPercent, statisticWithoutDamage, inTargetPercent;
                double chance;

                Statistic(out baseForPercent, out shutdownPercent, out damagedPercent, out statisticWithoutDamage,
                    out chance, out inTargetPercent);

                if (statisticShellsFired > 0) stat += "Выстрелов: " + statisticShellsFired;

                if (staticticInTarget > 0) 
                    stat += "\nПопаданий: " + staticticInTarget + " ( " + inTargetPercent + "% )";

                if (staticticAircraftShutdown > 0)
                {
                    stat += "\nСбито: " + staticticAircraftShutdown + " ( " + shutdownPercent + "% )";

                    if (statisticDamaged > 0)
                    {
                        stat += " + повреждён: " + statisticDamaged + " ( " + damagedPercent + "% )";
                    }
                }
                
                if (statisticHasGone > 0) stat += "\nУпущено: " + statisticHasGone + " ( " + (statisticHasGone * baseForPercent)  + "% )";

                if (statisticDamaged < statisticHasGone)
                {
                    stat += " в том числе неповредённых: " + (statisticHasGone - statisticDamaged) + " ( " + statisticWithoutDamage + "% )";
                }

                if (statisticAmountOfDamage > 0)
                {
                    string AmountOfDamage;

                    if (statisticAmountOfDamage < 1000)
                    {
                        AmountOfDamage = statisticAmountOfDamage + " млн $";
                    }
                    else if (statisticAmountOfDamage < 1000000)
                    {
                        AmountOfDamage = String.Format("{0:f2}", (double)statisticAmountOfDamage / 1000) + " млрд $";
                    }
                    else
                    {
                        AmountOfDamage = String.Format("{0:f2}", (double)statisticAmountOfDamage / 1000000) + " трлн $";
                    }

                    stat += "\nНанесён ущерб: " + AmountOfDamage + statisticLastDamage;
                }
                   
                if (statisticFriendDamage > 0) stat += "\nПовреждено своих: " + statisticFriendDamage;

                if (staticticAircraftShutdown > 0) stat += String.Format("\nУдача: {0:f2}", chance );   

                main.statShells.Content = stat;
            }));
        }
    }
}
