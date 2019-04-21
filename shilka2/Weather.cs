using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace shilka2
{
    class Weather : FlyObject
    {
        int speed { get; set; }
        public Image weatherImage;

        public static List<Weather> precipitation = new List<Weather>();

        public static void NewPrecipitation(object obj, ElapsedEventArgs e)
        {
            Weather newWeather = new Weather();
            newWeather.x = rand.Next(0, (int)SystemParameters.PrimaryScreenWidth);
            newWeather.y = 0;
            newWeather.speed = rand.Next(Constants.MIN_SPEED, Constants.MAX_SPEED);
            newWeather.fly = true;

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                Image newImage = new Image();

                newImage.Width = 8;
                newImage.Height = 17;

                newImage.Source = Aircraft.ImageFromResources("rain");
                newImage.Margin = new Thickness(newWeather.x, newWeather.y, 0, 0);

                newWeather.weatherImage = newImage;

                Canvas.SetZIndex(newImage, 120);
                main.firePlace.Children.Add(newImage);
                Weather.precipitation.Add(newWeather);
            }));
        }

        public static void PrecipitationFly(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                foreach (var c in precipitation)
                {
                    c.x = c.x;
                    c.y = (c.y + c.speed);
                    c.weatherImage.Margin = new Thickness(c.x, c.y, 0, 0);

                    if (c.y > SystemParameters.PrimaryScreenHeight + 100)
                        c.fly = false;
                }

                for (int x = 0; x < precipitation.Count; x++)
                    if (precipitation[x].fly == false)
                    {
                        precipitation.RemoveAt(x);
                        main.firePlace.Children.Remove(precipitation[x].weatherImage);
                    }
            }));
        }
    }
}
