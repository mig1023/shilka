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

        int rotateDirection { get; set; }
        int rotateSpeed { get; set; }

        double rotateDegreeCurrent = 0;

        public Image wreckImage;

        public static List<Wrecks> wrecks = new List<Wrecks>();

        static int wreckMutex = 0;

        private static int RandomSpeed(int maxSpeed)
        {
            int minSpeed = 1;

            if (maxSpeed < 1)
            {
                minSpeed = 0;
                maxSpeed = 0;
            }
            else  if (maxSpeed < 2)
            {
                minSpeed = 0;
                maxSpeed = 2;
            }

            return (maxSpeed == 0 ? 0 : rand.Next(minSpeed, maxSpeed));
        }

        public static void WreckBreackOffFromAircraft(double startX, double startY,
            Aircraft.FlightDirectionType direction, int maxSpeed, int maxSize,
            int number, int randomDistance = 0)
        {
            if (rand.Next(Constants.WRECKS_RAND_RANGE) > number)
                return;

            wreckMutex++;
            if (wreckMutex > 1)
            {
                wreckMutex--;
                return;
            }

            Wrecks newWreck = new Wrecks();

            newWreck.x = startX + (randomDistance > 0 ? rand.Next(randomDistance / 2) : 0);
            newWreck.y = startY;

            newWreck.sin = 0;
            newWreck.cos = (direction == Aircraft.FlightDirectionType.Left ? 1 : -1);
            newWreck.flightDirection = direction;
            newWreck.speed = RandomSpeed(maxSpeed);
            newWreck.rotateSpeed = rand.Next(Constants.WRECKS_MIN_ROTATE_SPEED, Constants.WRECKS_MAX_ROTATE_SPEED);
            newWreck.rotateDirection = (rand.Next(2) == 0 ? 1 : -1);

            newWreck.fly = true;

            int wr_rand_num = Constants.WRECKS_TYPE_NUM + 1;

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                Image newImage = new Image();

                newImage.Width = rand.Next(maxSize) + Constants.WRECKS_MIN_SIZE;
                newImage.Height = rand.Next(maxSize) + Constants.WRECKS_MIN_SIZE;

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

                    if (Weather.currentWeather == Weather.weatherTypes.storm)
                    {
                        c.speed = SpeedInStorm(c.speed, ref c.flightDirection);
                        c.cos = (c.flightDirection == Aircraft.FlightDirectionType.Left ? 1 : -1);
                    }

                    c.x = (c.x - c.speed * c.cos);
                    c.y = (c.y - c.speed * c.sin) + c.fall;
                    c.wreckImage.Margin = new Thickness(c.x, c.y, 0, 0);

                    c.rotateDegreeCurrent += (c.rotateSpeed * c.rotateDirection);

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
