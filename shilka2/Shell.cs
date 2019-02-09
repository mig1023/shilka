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
        static string endColor = "#FF7E1C25";

        bool flash { get; set; }
        int delay { get; set; }
        public static double ptX { get; set; }
        public static double ptY { get; set; }

        public static double currentHeight = -1;
        public static double currentWidth = -1;

        public static bool fire = false;
        public static bool animationStop = false;

        public static double lastSin = 0;
        public static double lastCos = 1;

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
                    Line shellTrace = new Line();

                    shellTrace.X1 = shell.x + shell.cos;
                    shellTrace.Y1 = shell.y - shell.sin;
                    shellTrace.X2 = shell.x + Constants.SHELL_LENGTH * shell.cos;
                    shellTrace.Y2 = shell.y - Constants.SHELL_LENGTH * shell.sin;

                    shellTrace.Stroke = Brushes.Black;

                    shell.x = (shell.x + Constants.SHELL_SPEED * shell.cos);
                    shell.y = (shell.y - Constants.SHELL_SPEED * shell.sin);

                    foreach (var aircraft in Aircraft.aircrafts)
                        if (
                            shell.fly &&
                            (shell.y < (aircraft.aircraftImage.Margin.Top + aircraft.aircraftImage.Height) ) &&
                            (shell.y > (aircraft.aircraftImage.Margin.Top) ) &&
                            (shell.x > (aircraft.aircraftImage.Margin.Left) ) &&
                            (shell.x < (aircraft.aircraftImage.Margin.Left + aircraft.aircraftImage.Width) )
                        ) {
                            if (aircraft.cloud)
                                continue;

                            shell.flash = true;
                            shellTrace.Stroke = Brushes.Red;
                            shellTrace.StrokeThickness = Constants.FLASH_SIZE;

                            aircraft.hitpoint -= 1;

                            Statistic.staticticInTarget++;

                            if (aircraft.hitpoint <= 0)
                            {
                                if (aircraft.dead == false)
                                {
                                    if (aircraft.friend)
                                        main.EndGame("Вы сбили свой "+aircraft.aircraftName+
                                            "!\nИгра окончена.\nСохранить статистику?", endColor);

                                    else if (aircraft.airliner)
                                        main.EndGame("Вы сбили пассажирский самолёт"+
                                            "!\nИгра окончена.\nСохранить статистику?", endColor);

                                    else
                                    {
                                        Statistic.staticticAircraftShutdown++;
                                        Statistic.statisticAmountOfDamage += aircraft.price;

                                        Statistic.statisticShutdownFlag = true;
                                        Statistic.statisticLastDamagePrice = aircraft.price;
                                        Statistic.statisticLastDamageType = aircraft.aircraftName;

                                        //if ((rand.NextDouble() > 0.5) && (aircraft.dynamicElemets.Count > 0))
                                        if (aircraft.dynamicElemets.Count > 0)
                                            aircraft.fullDestruction = 1;
                                    }
                                }
                                aircraft.dead = true;
                            }
                        }
                        else if (shell.flash)
                            shell.fly = false;
                            

                    if ((shell.y < 0) || (shell.x > currentWidth))
                        shell.fly = false;
                    else if (shell.delay < Constants.SHELL_DELAY)
                        shell.delay++;
                    else
                    {
                        main.firePlace.Children.Add(shellTrace);
                        Canvas.SetZIndex(shellTrace, 20);
                        allLines.Add(shellTrace);
                    }
                }

                for (int x = 0; x < shells.Count; x++)
                    if (shells[x].fly == false)
                        shells.RemoveAt(x);

                fireMutex--;
            }));
        }

        public static void ShellsFire(object obj, ElapsedEventArgs e)
        {
            int currentFragmentation = Constants.FRAGMENTATION + ( ( Shilka.degreeOfHeatingGunBurrels - 30 ) / 25 );

            if (fire && !Shilka.reheatingGunBurrels)
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

                    Statistic.statisticShellsFired++;

                    shells.Add(newShell);

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
