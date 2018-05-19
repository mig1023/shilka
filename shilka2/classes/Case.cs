using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using shilka2.classes;

namespace shilka2
{
    class Case : FlyObject
    {
        const int CASE_LENGTH = 5;
        const double MIN_FRAGM_SIN = 0.2;
        const double MAX_FRAGM_SIN = 0.4;
        const double MIN_FRAGM_COS = 0.4;
        const double MAX_FRAGM_COS = 0.8;
        const int MIN_SPEED = 3;
        const int MAX_SPEED = 12;
        const int EXTR_HEIGHT_CORRECTION = 18; 

        public int speed { get; set; }

        public static List<Case> cases = new List<Case>();
        public static List<Line> allLines = new List<Line>();

        static int CaseMutex = 0;
        static bool caseLimiter = false;

        public static void CaseExtractor()
        {
            if (caseLimiter)
            {
                caseLimiter = false;
                return;
            }
            else
                caseLimiter = true;

            CaseMutex++;
            if (CaseMutex > 1)
            {
                CaseMutex--;
                return;
            }

            Case newCase = new Case();
            newCase.x = Shell.FIRE_WIDTH_CORRECTION / 2;
            newCase.y = Shell.currentHeight - EXTR_HEIGHT_CORRECTION;
            newCase.sin = rand.NextDouble() * (MAX_FRAGM_SIN - MIN_FRAGM_SIN) + MIN_FRAGM_SIN;
            newCase.cos = rand.NextDouble() * (MAX_FRAGM_COS - MIN_FRAGM_COS) + MIN_FRAGM_COS;
            newCase.speed = rand.Next(MIN_SPEED, MAX_SPEED);
            newCase.fly = true;

            Case.cases.Add(newCase);

            CaseMutex--;
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
