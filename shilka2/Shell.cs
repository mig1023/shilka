using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace shilka2
{
    class Shell
    {
        const int SHELL_LENGTH = 3;
        const int SHELL_SPEED = 15;
        const int FRAGMENTATION = 9;
        const int VOLLEY = 3;

        public double x { get; set; }
        public double y { get; set; }
        public double sin { get; set; }
        public double cos { get; set; }
        public bool fly { get; set; }
        public int delay { get; set; }
        public static double ptX { get; set; }
        public static double ptY { get; set; }
        public static double currentHeight { get; set; }
        public static double currentWidth { get; set; }

        public static bool Fire = false;

        static int FireMutex = 0;
        static Random rand;

        static List<Shell> shells = new List<Shell>();
        static List<Line> allLines = new List<Line>();

        static Shell()
        {
            rand = new Random();
        }

        public static void ShellsFly(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;

                foreach (var line in allLines)
                    main.canvas.Children.Remove(line);
               
                allLines.Clear();

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

                    if ((shell.y < 0) || (shell.x > currentWidth))
                        shell.fly = false;
                    else
                    {
                        main.canvas.Children.Add(shellTrace);
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
            if (Fire)
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

                    newShell.x = rand.Next( (-1 * FRAGMENTATION) , FRAGMENTATION);
                    newShell.y = currentHeight + rand.Next( (-1 * FRAGMENTATION), FRAGMENTATION);

                    double e1 = Math.Sqrt((ptX * ptX) + (ptY * ptY));
                    newShell.cos = ptX / e1;
                    newShell.sin = ptY / e1;

                    shells.Add(newShell);
                }
                FireMutex--;
            }
        }
    }
}
