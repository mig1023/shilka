using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace shilka2
{
    class Shell : FlyObject
    {
        bool flash { get; set; }
        int delay { get; set; }
        public static double ptX { get; set; }
        public static double ptY { get; set; }

        public static double currentHeight = -1;
        public static double currentWidth = -1;

        public static bool animationStop = false;

        public static double lastSin = 0;
        public static double lastCos = 1;

        public Image shellImage;

        static int fireMutex = 0;
        
        public static List<Shell> shells = new List<Shell>();
        public static List<Line> allLines = new List<Line>();

        public static void ShellsFly(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                if (animationStop)
                    return;

                foreach (var line in allLines)
                    main.firePlace.Children.Remove(line);
       
                allLines.Clear();
                Shilka.DrawGuns(main);

                fireMutex++;

                foreach (var shell in shells)
                {
                    shell.x = (shell.x + Constants.SHELL_SPEED * shell.cos);
                    shell.y = (shell.y - Constants.SHELL_SPEED * shell.sin);

                    shell.shellImage.Margin = new Thickness(shell.x, shell.y, 0, 0);

                    foreach (Aircraft aircraft in Aircraft.aircrafts)
                        if (
                            shell.fly &&
                            (shell.y < (aircraft.aircraftImage.Margin.Top + aircraft.aircraftImage.Height) ) &&
                            (shell.y > (aircraft.aircraftImage.Margin.Top) ) &&
                            (shell.x > (aircraft.aircraftImage.Margin.Left) ) &&
                            (shell.x < (aircraft.aircraftImage.Margin.Left + aircraft.aircraftImage.Width) )
                        ) {
                            if (aircraft.cloud)
                                continue;

                            bool itsOnlyTargetPlane = false;

                            if (Shilka.training && (aircraft.aircraftType == "il28bm_77bm2"))
                                if (aircraft.targetTubHit(shell, ref itsOnlyTargetPlane))
                                    continue;

                            if (itsOnlyTargetPlane && aircraft.fly && (aircraft.tragetTugHitPoint <= 0))
                                aircraft.targetTugDisengaged();

                            Line shellTrace = new Line();

                            shellTrace.X1 = shell.x + shell.cos;
                            shellTrace.Y1 = shell.y - shell.sin;
                            shellTrace.X2 = shell.x + Constants.FLASH_SIZE;
                            shellTrace.Y2 = shell.y - Constants.FLASH_SIZE;
                            shell.flash = true;
                            shellTrace.Stroke = Brushes.Red;
                            shellTrace.StrokeThickness = Constants.FLASH_SIZE;

                            main.firePlace.Children.Add(shellTrace);
                            Canvas.SetZIndex(shellTrace, 20);
                            allLines.Add(shellTrace);

                            Statistic.staticticInTarget++;

                            if (itsOnlyTargetPlane)
                            {
                                shell.fly = false;
                                continue;
                            }

                            aircraft.hitpoint -= 1;

                            if (aircraft.hitpoint <= 0 && !aircraft.dead)
                                aircraft.Shutdown(main);

                            double planeMiddle = aircraft.aircraftImage.Margin.Left + aircraft.aircraftImage.Width / 2;
                            aircraft.placeOfDamage = (shell.x < planeMiddle ? 1 : -1);
                        }
                        else if (shell.flash)
                            shell.fly = false;

                    if ((shell.delay >= Constants.SHELL_DELAY) && (shell.shellImage.Visibility == Visibility.Hidden))
                        shell.shellImage.Visibility = Visibility.Visible;

                    if ((shell.y < 0) || (shell.x > currentWidth))
                        shell.fly = false;
                    else if (shell.delay < Constants.SHELL_DELAY)
                        shell.delay++;
                }

                for (int x = 0; x < shells.Count; x++)
                    if ((shells[x].fly == false) && (fireMutex == 1))
                    {
                        main.firePlace.Children.Remove(shells[x].shellImage);
                        shells.RemoveAt(x);
                    }

                fireMutex--;
            }));
        }

        public static void ShellsFire(object obj, ElapsedEventArgs e)
        {
            int currentFragmentation = Constants.FRAGMENTATION + ( ( Shilka.degreeOfHeatingGunBurrels - 30 ) / 25 );

            if ((Shilka.currentScript == Scripts.scriptsNames.Libya) && (rand.Next(Constants.GUN_DAMAGED_CHANCE) != 1))
                return;

            if (Shilka.fire && !Shilka.reheatingGunBurrels)
            {
                fireMutex++;
                if (fireMutex > 1)
                {
                    fireMutex--;
                    return;
                }
 
                for (int a = 0; a < Constants.VOLLEY; a++)
                {
                    Shell newShell = new Shell();
                    newShell.fly = true;
                    newShell.delay = 0;

                    newShell.x = rand.Next( (-1 * currentFragmentation), currentFragmentation) + Constants.FIRE_WIDTH_CORRECTION;
                    newShell.y = currentHeight + rand.Next( (-1 * currentFragmentation), currentFragmentation) - Constants.FIRE_HEIGHT_CORRECTION;

                    double e1 = Math.Sqrt((ptX * ptX) + (ptY * ptY));

                    double tryCos = ptX / e1;
                    double trySin = ptY / e1;

                    newShell.cos = (double.IsNaN(tryCos) ? lastCos : tryCos);
                    newShell.sin = (double.IsNaN(trySin) ? lastSin : trySin);

                    lastCos = newShell.cos;
                    lastSin = newShell.sin;

                    Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
                    {
                        FirePlace main = (FirePlace)Application.Current.MainWindow;

                        Image newImage = new Image();

                        newImage.Width = Constants.SHELL_LENGTH;
                        newImage.Height = Constants.SHELL_THICKNESS;

                        double angle = (Math.Asin(newShell.sin) * 180 / Math.PI) * -1;

                        newImage.RenderTransform = new RotateTransform(angle);
                        newImage.Source = Aircraft.ImageFromResources("shell", Aircraft.ImageType.Other);
                        newImage.Margin = new Thickness(newShell.x, newShell.y, 0, 0);
                        newImage.Visibility = Visibility.Hidden;

                        newShell.shellImage = newImage;

                        main.firePlace.Children.Add(newImage);
                        shells.Add(newShell);
                    }));

                    Statistic.statisticShellsFired++;

                    Case.CaseExtractor();
                }
                fireMutex--;

                Shilka.HeatingOfGuns(shooting: true);
            }
            else
                Shilka.HeatingOfGuns(shooting: false);
        }
    }
}
