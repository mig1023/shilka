using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace shilka2
{
    class Wrecks : FlyObject
    {
        double fall = 0;

        int speed { get; set; }
        int direction { get; set; }
        int rotateSpeed { get; set; }

        double rotateDegreeCurrent = 0;

        public Image wreckImage;

        public static List<Wrecks> wrecks = new List<Wrecks>();

        static int wreckMutex = 0;

        private static int MinMaxSpeed(ref int maxSpeed)
        {
            if (maxSpeed < 1)
                return 0;

            if (maxSpeed < 2)
            {
                maxSpeed = 2;
                return 0;
            }

            return 1;
        }

        public static void WreckBreackOffFromAircraft(double startX, double startY,
            Aircraft.FlightDirectionType direction, int maxSpeed, int wrecksMaxSize, int wrecksNumber)
        {
            if (rand.Next(Constants.WRECKS_RAND_RANGE) > wrecksNumber)
                return;

            wreckMutex++;
            if (wreckMutex > 1)
            {
                wreckMutex--;
                return;
            }

            int minSpeed = MinMaxSpeed(ref maxSpeed);

            Wrecks newWreck = new Wrecks();

            newWreck.x = startX;
            newWreck.y = startY;

            newWreck.sin = 0;
            newWreck.cos = (direction == Aircraft.FlightDirectionType.Left ? 1 : -1);
            newWreck.speed = (maxSpeed == 0 ? 0 : rand.Next(minSpeed, maxSpeed));
            newWreck.rotateSpeed = rand.Next(Constants.WRECKS_MIN_ROTATE_SPEED, Constants.WRECKS_MAX_ROTATE_SPEED);
            newWreck.direction = (rand.Next(2) == 0 ? 1 : -1);

            newWreck.fly = true;

            int wr_rand_num = Constants.WRECKS_TYPE_NUM + 1;

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                Image newImage = new Image();

                newImage.Width = rand.Next(wrecksMaxSize) + Constants.WRECKS_MIN_SIZE;
                newImage.Height = rand.Next(wrecksMaxSize) + Constants.WRECKS_MIN_SIZE;

                newImage.Source = Aircraft.ImageFromResources("wrecks" + (Aircraft.rand.Next(1, wr_rand_num)), Aircraft.ImageType.Other);
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

                    c.rotateDegreeCurrent += (c.rotateSpeed * c.direction);

                    if (c.rotateDegreeCurrent < -180 || c.rotateDegreeCurrent > 180)
                        c.rotateDegreeCurrent = 0;

                    c.wreckImage.RenderTransform = new RotateTransform(c.rotateDegreeCurrent, (c.wreckImage.ActualWidth / 2), (c.wreckImage.ActualHeight / 2));

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
