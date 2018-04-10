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
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace shilka2
{

    class Shell
    {
        const int SHELL_LENGTH = 3;
        const int SHELL_SPEED = 25;
        const int SHELL_DELAY = 2;
        const int FRAGMENTATION = 15;
        const int VOLLEY = 3;
        const int FLASH_SIZE = 4;

        public const int FIRE_WIDTH_CORRECTION = 140;
        public const int FIRE_HEIGHT_CORRECTION = 30;
        public const int FIRE_HEIGHT_POINT_CORRECTION = 70;

        public double x { get; set; }
        public double y { get; set; }
        public double sin { get; set; }
        public double cos { get; set; }
        public bool fly { get; set; }
        public bool flash { get; set; }
        public int delay { get; set; }
        public static double ptX { get; set; }
        public static double ptY { get; set; }

        public static double currentHeight = -1;
        public static double currentWidth = -1;

        public static bool Fire = false;
        public static bool AnimationStop = false;

        public static double LastSin = 0;
        public static double LastCos = 1;

        static int FireMutex = 0;
        
        static Random rand;

        static List<Shell> shells = new List<Shell>();
        public static List<Line> allLines = new List<Line>();

        static Shell()
        {
            rand = new Random();
        }

        public static void ShellsFly(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;

                if (AnimationStop) return;

                foreach (var line in allLines)
                    main.firePlace.Children.Remove(line);
       
                allLines.Clear();
                Shilka.DrawGuns(main);

                FireMutex++;

                foreach (var shell in shells)
                {
                    Line shellTrace = new Line();

                    shellTrace.X1 = shell.x + shell.cos;
                    shellTrace.Y1 = shell.y - shell.sin;
                    shellTrace.X2 = shell.x + SHELL_LENGTH * shell.cos;
                    shellTrace.Y2 = shell.y - SHELL_LENGTH * shell.sin;

                    shellTrace.Stroke = Brushes.Black;

                    shell.x = (shell.x + SHELL_SPEED * shell.cos);
                    shell.y = (shell.y - SHELL_SPEED * shell.sin);

                    foreach (var aircraft in Aircraft.aircrafts)
                        if (
                            shell.fly &&
                            (shell.y < (aircraft.aircraftImage.Margin.Top + aircraft.aircraftImage.Height) ) &&
                            (shell.y > (aircraft.aircraftImage.Margin.Top) ) &&
                            (shell.x > (aircraft.aircraftImage.Margin.Left) ) &&
                            (shell.x < (aircraft.aircraftImage.Margin.Left + aircraft.aircraftImage.Width) )
                        ) {
                            if (aircraft.cloud) continue;

                            shell.flash = true;
                            shellTrace.Stroke = Brushes.Red;
                            shellTrace.StrokeThickness = FLASH_SIZE;

                            aircraft.hitpoint -= 1;

                            Shilka.staticticInTarget++;

                            if (aircraft.hitpoint <= 0)
                            {
                                if (aircraft.dead == false)
                                {
                                    if (!aircraft.friend)
                                    {
                                        Shilka.staticticAircraftShutdown++;
                                        Shilka.statisticAmountOfDamage += aircraft.price;
                                        Shilka.statisticLastDamage = " ( +" + aircraft.price + " млн $ сбит " + 
                                            aircraft.aircraftType + " )";
                                    }
                                    else
                                        main.EndGame("Вы подбили своего!\nИгра окончена.\nСохранить статистику?", "#FF7E1C25");
                                }
                                aircraft.dead = true;
                            }
                        }
                        else if (shell.flash) shell.fly = false;
                            

                    if ((shell.y < 0) || (shell.x > currentWidth))
                        shell.fly = false;
                    else if (shell.delay < SHELL_DELAY)
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

                FireMutex--;
            }));
        }

        public static void ShellsFire(object obj, ElapsedEventArgs e)
        {
            if (Fire && !Shilka.reheatingGunBurrels)
            {
                FireMutex++;
                if (FireMutex > 1)
                {
                    FireMutex--;
                    return;
                }
 
                for (int a = 0; a < VOLLEY; a++)
                {
                    Shell newShell = new Shell();
                    newShell.fly = true;
                    newShell.delay = 0;

                    newShell.x = rand.Next( (-1 * FRAGMENTATION) , FRAGMENTATION) + FIRE_WIDTH_CORRECTION;
                    newShell.y = currentHeight + rand.Next( (-1 * FRAGMENTATION), FRAGMENTATION) - FIRE_HEIGHT_CORRECTION;

                    double e1 = Math.Sqrt((ptX * ptX) + (ptY * ptY));
                    newShell.cos = ptX / e1;
                    newShell.sin = ptY / e1;

                    LastCos = newShell.cos;
                    LastSin = newShell.sin;

                    Shilka.statisticShellsFired++;

                    shells.Add(newShell);

                    Case.CaseExtractor();
                }
                FireMutex--;

                Shilka.HeatingOfGuns(true);
            }
            else
                Shilka.HeatingOfGuns(false);
        }
    }
}
