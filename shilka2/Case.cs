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

namespace shilka2
{
    class Case
    {
        const int CASE_LENGTH = 5;

        public double x { get; set; }
        public double y { get; set; }
        public double sin { get; set; }
        public double cos { get; set; }
        public bool fly { get; set; }
        public int speed { get; set; }

        public static List<Case> cases = new List<Case>();
        public static List<Line> allLines = new List<Line>();

        static int CaseMutex = 0;

        static Random rand;

        static Case()
        {
            rand = new Random();
        }

        public static void CaseExtractor()
        {
            Case newCase = new Case();
            newCase.x = Shell.FIRE_WIDTH_CORRECTION / 2;
            newCase.y = Shell.currentHeight - 18;
            newCase.sin = rand.NextDouble() * (0.4 - 0.2) + 0.2;
            newCase.cos = rand.NextDouble() * (0.8 - 0.4) + 0.4;
            newCase.speed = rand.Next(5, 10);
            newCase.fly = true;

            Case.cases.Add(newCase);
        }

        public static void CasesFly(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;

                foreach (var line in allLines)
                    main.firePlace.Children.Remove(line);

                allLines.Clear();

                CaseMutex++;

                foreach (var c in cases)
                {
                    Line shellTrace = new Line();

                    shellTrace.X1 = c.x + c.cos;
                    shellTrace.Y1 = c.y - c.sin;
                    shellTrace.X2 = c.x + CASE_LENGTH * c.cos;
                    shellTrace.Y2 = c.y - CASE_LENGTH * c.sin;

                    shellTrace.Stroke = Brushes.Black;

                    c.x = (c.x - c.speed * c.cos);
                    c.y = (c.y - c.speed * c.sin);

                    if (c.x < 0)
                        c.fly = false;
                    else
                    {
                        main.firePlace.Children.Add(shellTrace);
                        Canvas.SetZIndex(shellTrace, 150);
                        allLines.Add(shellTrace);
                    }
                }

                CaseMutex--;

                for (int x = 0; x < cases.Count; x++)
                    if ((CaseMutex <= 0) && (cases[x].fly == false))
                        cases.RemoveAt(x);

            }));
        }
    }
}
