using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace shilka2
{
    class Case : FlyObject
    {
        double fall = 0;

        int speed { get; set; }
        public Image caseImage;

        public static List<Case> cases = new List<Case>();

        static int caseMutex = 0;

        public static void CaseExtractor()
        {
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

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                Image newImage = new Image();

                newImage.Width = Constants.CASE_LENGTH;
                newImage.Height = Constants.CASE_LENGTH;

                newImage.Source = Aircraft.ImageFromResources("case");
                newImage.Margin = new Thickness(newCase.x, newCase.y, 0, 0);

                newCase.caseImage = newImage;

                main.firePlace.Children.Add(newImage);
                Case.cases.Add(newCase);
            }));

            caseMutex--;
        }

        public static void CasesFly(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                caseMutex++;
                if (caseMutex > 1)
                {
                    caseMutex--;
                    return;
                }

                foreach (var c in cases)
                {
                    c.fall += Constants.FREE_FALL_SPEED;

                    c.x = (c.x - c.speed * c.cos);
                    c.y = (c.y - c.speed * c.sin) + c.fall;
                    c.caseImage.Margin = new Thickness(c.x, c.y, 0, 0);

                    if (c.x < 0)
                        c.fly = false;
                }

                for (int x = 0; x < cases.Count; x++)
                    if ((cases[x].fly == false) && (caseMutex == 1))
                    {
                        main.firePlace.Children.Remove(cases[x].caseImage);
                        cases.RemoveAt(x);
                    }

                caseMutex--;
            }));
        }
    }
}
