using System;
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
        public enum weatherTypes
        {
            goodWeather,
            rain,
            snow,
        };

        int speed { get; set; }
        public Image weatherImage;

        public static List<Weather> weather = new List<Weather>();
        static int weatherCycle = 0;
        public static weatherTypes currentWeather = weatherTypes.goodWeather;
        static int weatherMutex = 0;

        public static void NewWeather(object obj, ElapsedEventArgs e)
        {
            if (weatherCycle < Constants.WEATHER_CYCLE)
                weatherCycle += 1;
            else
            {
                weatherCycle = 0;

                Array weathers = Enum.GetValues(typeof(weatherTypes));
                currentWeather = (weatherTypes)weathers.GetValue(rand.Next(weathers.Length));
            }

            if (currentWeather == weatherTypes.goodWeather)
                return;

            Weather newWeather = new Weather();
            newWeather.x = rand.Next(0, (int)SystemParameters.PrimaryScreenWidth);
            newWeather.y = 0;
            newWeather.speed = rand.Next(Constants.MIN_SPEED, Constants.MAX_SPEED);
            newWeather.fly = true;

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                Image newImage = new Image();

                string imageName;

                if (currentWeather == weatherTypes.rain)
                {
                    newImage.Width = rand.Next(Constants.RAIN_MIN_WIDTH, Constants.RAIN_MAX_WIDTH);
                    newImage.Height = rand.Next(Constants.RAIN_MIN_HEIGHT, Constants.RAIN_MAX_HEIGHT);
                    imageName = "rain" + rand.Next(1, Constants.MAX_RAIN_TYPE).ToString();
                }
                else
                {
                    newImage.Width = rand.Next(Constants.SNOW_MIN_SIZE, Constants.SNOW_MAX_SIZE);
                    newImage.Height = rand.Next(Constants.SNOW_MIN_SIZE, Constants.SNOW_MAX_SIZE);
                    imageName = "snow" + rand.Next(1, Constants.MAX_SNOW_TYPE).ToString();
                }

                newImage.Source = Aircraft.ImageFromResources(imageName);
                newImage.Margin = new Thickness(newWeather.x, newWeather.y, 0, 0);

                newWeather.weatherImage = newImage;

                FirePlace main = (FirePlace)Application.Current.MainWindow;
                Canvas.SetZIndex(newImage, 120);
                main.firePlace.Children.Add(newImage);
                Weather.weather.Add(newWeather);
            }));
        }

        public static void WeatherElementsFly(object obj, ElapsedEventArgs e)
        {
            weatherMutex += 1;
            if (weatherMutex > 1)
            {
                weatherMutex -= 1;
                return;
            }

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                foreach (Weather w in weather)
                {

                    w.x = w.x;
                    w.y = (w.y + w.speed);
                    w.weatherImage.Margin = new Thickness(w.x, w.y, 0, 0);

                    if (w.y > SystemParameters.PrimaryScreenHeight + 100)
                        w.fly = false;
                }

                for (int x = 0; x < weather.Count; x++)
                    if ((weather[x].fly == false) && (weatherMutex == 1))
                    {
                        main.firePlace.Children.Remove(weather[x].weatherImage);
                        weather.RemoveAt(x);
                    }
            }));

            weatherMutex = 0;
        }
    }
}
