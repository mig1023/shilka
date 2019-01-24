using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace shilka2
{
    class Case : FlyObject
    {
        double fall = 0;

        int speed { get; set; }

        public static List<Case> cases = new List<Case>();
        public static List<Line> allLines = new List<Line>();

        static int caseMutex = 0;
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

            caseMutex++;
            if (caseMutex > 1)
            {
                caseMutex--;
                return;
            }

            Case newCase = new Case();
            newCase.x = Constants.FIRE_WIDTH_CORRECTION / 2;
            newCase.y = Shell.currentHeight - Constants.EXTR_HEIGHT_CORRECTION;
            newCase.sin = rand.NextDouble() * (Constants.MAX_FRAGM_SIN - Constants.MIN_FRAGM_SIN) + Constants.MIN_FRAGM_SIN;
            newCase.cos = rand.NextDouble() * (Constants.MAX_FRAGM_COS - Constants.MIN_FRAGM_COS) + Constants.MIN_FRAGM_COS;
            newCase.speed = rand.Next(Constants.MIN_SPEED, Constants.MAX_SPEED);
            newCase.fly = true;

            Case.cases.Add(newCase);

            caseMutex--;
        }

        public static void CasesFly(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                foreach (var line in allLines)
                    main.firePlace.Children.Remove(line);

                allLines.Clear();

                caseMutex++;

                foreach (var c in cases)
                {
                    Line shellTrace = new Line();

                    shellTrace.X1 = c.x + c.cos;
                    shellTrace.Y1 = c.y - c.sin;
                    shellTrace.X2 = c.x + Constants.CASE_LENGTH * c.cos;
                    shellTrace.Y2 = c.y - Constants.CASE_LENGTH * c.sin;

                    shellTrace.Stroke = Brushes.Black;

                    c.fall += Constants.FREE_FALL_SPEED;

                    c.x = (c.x - c.speed * c.cos);
                    c.y = (c.y - c.speed * c.sin) + c.fall;

                    if (c.x < 0)
                        c.fly = false;
                    else
                    {
                        main.firePlace.Children.Add(shellTrace);
                        Canvas.SetZIndex(shellTrace, 150);
                        allLines.Add(shellTrace);
                    }
                }

                caseMutex--;

                for (int x = 0; x < cases.Count; x++)
                    if ((caseMutex <= 0) && (cases[x].fly == false))
                        cases.RemoveAt(x);
            }));
        }
    }
}
