using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace shilka2
{
    class Wrecks : FlyObject
    {
        double fall = 0;

        int speed { get; set; }
        public Image wreckImage;

        public static List<Wrecks> wrecks = new List<Wrecks>();

        static int wreckMutex = 0;

        public static void WreckBreackOffFromAircraft(double startX, double startY, Aircraft.FlightDirectionType direction)
        {
            wreckMutex++;
            if (wreckMutex > 1)
            {
                wreckMutex--;
                return;
            }

            Wrecks newWreck = new Wrecks();

            newWreck.x = startX;
            newWreck.y = startY;

            newWreck.sin = 0;
            newWreck.cos = (direction == Aircraft.FlightDirectionType.Left ? 1 : -1);
            newWreck.speed = rand.Next(Constants.MIN_SPEED, Constants.MAX_SPEED);

            newWreck.fly = true;

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                Image newImage = new Image();

                newImage.Width = Constants.CASE_LENGTH;
                newImage.Height = Constants.CASE_LENGTH;

                newImage.Source = Aircraft.ImageFromResources("case", Aircraft.ImageType.Other);
                newImage.Margin = new Thickness(newWreck.x, newWreck.y, 0, 0);

                newWreck.wreckImage = newImage;

                main.firePlace.Children.Add(newImage);
                Wrecks.wrecks.Add(newWreck);
            }));

            wreckMutex--;
        }

        public static void WreckFly(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                wreckMutex++;
                if (wreckMutex > 1)
                {
                    wreckMutex--;
                    return;
                }

                foreach (var c in wrecks)
                {
                    c.fall += Constants.FREE_FALL_SPEED;

                    c.x = (c.x - c.speed * c.cos);
                    c.y = (c.y - c.speed * c.sin) + c.fall;
                    c.wreckImage.Margin = new Thickness(c.x, c.y, 0, 0);

                    if (c.x < 0)
                        c.fly = false;
                }

                for (int x = 0; x < wrecks.Count; x++)
                    if ((wrecks[x].fly == false) && (wreckMutex == 1))
                    {
                        main.firePlace.Children.Remove(wrecks[x].wreckImage);
                        wrecks.RemoveAt(x);
                    }

                wreckMutex--;
            }));
        }
    }
}
