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
    public class Weather : FlyObject
    {
        public enum weatherTypes
        {
            good,
            rain,
            storm,
            snow,
            sand,
        };

        int speed { get; set; }
        int direction = 0;

        public Image weatherImage;
        
        weatherTypes type;

        public static List<Weather> weather = new List<Weather>();
        static int weatherCycle = 0;
        public static weatherTypes currentWeather = weatherTypes.good;
        static int weatherMutex = 0;

        static int thundar = 0;
        public static Image thunderCurrentImage;

        private static void Lightning(bool thunderclap = false, bool thunderimage = false)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                if (thunderimage)
                {
                    Image thuderImage = new Image();

                    thuderImage.Height = rand.Next(200, (int)SystemParameters.PrimaryScreenHeight);
                    thuderImage.Source = Aircraft.ImageFromResources("thunder" + (rand.Next(1, 7)));
                    thuderImage.Margin = new Thickness(rand.Next(0, (int)SystemParameters.PrimaryScreenWidth), -10, 0, 0);

                    thunderCurrentImage = thuderImage;
                    main.firePlace.Children.Add(thuderImage);
                }
                else
                {
                    if (thunderCurrentImage != null)
                        main.firePlace.Children.Remove(thunderCurrentImage);
                }
                    
                main.thunderPlace.Visibility = (thunderclap ? Visibility.Visible : Visibility.Hidden);
            }));
        }

        private static void Thunder()
        {
            switch(thundar)
            {
                case 2:
                    thundar = 0;
                    Lightning();
                    break;
                case 1:
                    thundar = 2;
                    Lightning(thunderimage: true);
                    break;
                case 0:
                    if (rand.Next(30) == 1)
                    {
                        thundar = 1;
                        Lightning(thunderclap: true);
                    }
                    break;
            }
        }

        public static void NewWeather(object obj, ElapsedEventArgs e)
        {
            if ((thundar != 0) || (currentWeather == weatherTypes.storm))
                Thunder();

            if (weatherCycle < Constants.WEATHER_CYCLE)
                weatherCycle += 1;
            else
            {
                weatherCycle = 0;

                Array weathers = Enum.GetValues(typeof(weatherTypes));
                weatherTypes currentWeatherForScripts = (weatherTypes)weathers.GetValue(rand.Next(weathers.Length));
                currentWeather = Scripts.ScriptsWeather(Shilka.currentScript, currentWeatherForScripts);
            }

            if (currentWeather == weatherTypes.good)
                return;
            
            Weather newWeather = new Weather();

            if (currentWeather == weatherTypes.sand)
            {
                newWeather.x = 0;
                newWeather.y = rand.Next(0, (int)SystemParameters.PrimaryScreenHeight);
            }
            else
            {
                newWeather.x = rand.Next(0, (int)SystemParameters.PrimaryScreenWidth);
                newWeather.y = 0;
            }
                
            newWeather.speed = rand.Next(Constants.MIN_SPEED, Constants.MAX_SPEED);
            newWeather.fly = true;
            newWeather.type = currentWeather;

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                Image newImage = new Image();

                string imageName;

                if ((currentWeather == weatherTypes.rain) || (currentWeather == weatherTypes.storm))
                {
                    newImage.Width = rand.Next(Constants.RAIN_MIN_WIDTH, Constants.RAIN_MAX_WIDTH);
                    newImage.Height = rand.Next(Constants.RAIN_MIN_HEIGHT, Constants.RAIN_MAX_HEIGHT);
                    imageName = "rain" + rand.Next(1, Constants.MAX_RAIN_TYPE + 1).ToString();
                }
                else if (currentWeather == weatherTypes.sand)
                {
                    newImage.Width = Constants.CASE_LENGTH;
                    newImage.Height = Constants.CASE_LENGTH;

                    imageName = "case";
                }
                else
                {
                    newImage.Width = rand.Next(Constants.SNOW_MIN_SIZE, Constants.SNOW_MAX_SIZE);
                    newImage.Height = rand.Next(Constants.SNOW_MIN_SIZE, Constants.SNOW_MAX_SIZE);
                    imageName = "snow" + rand.Next(1, Constants.MAX_SNOW_TYPE + 1).ToString();
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
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                weatherMutex += 1;
                if (weatherMutex > 1)
                {
                    weatherMutex -= 1;
                    return;
                }

                FirePlace main = (FirePlace)Application.Current.MainWindow;

                foreach (Weather w in weather)
                {
                    if (w.type == weatherTypes.snow)
                    {
                        if (rand.Next(Constants.SNOW_DIRECTION_CHANGE_CHANCE) == 1)
                            w.direction = rand.Next(
                                (Constants.SNOW_DIRECTION_FLY_SPEED * -1), Constants.SNOW_DIRECTION_FLY_SPEED + 1
                            );

                        w.x = w.x + w.direction;
                    }

                    if (w.type == weatherTypes.sand)
                    {
                        if (rand.Next(Constants.SNOW_DIRECTION_CHANGE_CHANCE) == 1)
                            w.direction = rand.Next(
                                (Constants.SNOW_DIRECTION_FLY_SPEED * -1), Constants.SNOW_DIRECTION_FLY_SPEED + 1
                            );

                        w.y = w.y + w.direction;
                        w.x = (w.x + w.speed);
                    }
                    else
                        w.y = (w.y + w.speed);

                    w.weatherImage.Margin = new Thickness(w.x, w.y, 0, 0);

                    if ((w.y > SystemParameters.PrimaryScreenHeight + 100) || (w.x > SystemParameters.PrimaryScreenWidth + 10))
                        w.fly = false;
                }

                for (int x = weather.Count-1; x >= 0; x--)
                    if ((weather[x].fly == false) && (weatherMutex == 1))
                    {
                        main.firePlace.Children.Remove(weather[x].weatherImage);
                        weather.RemoveAt(x);
                    }

                weatherMutex = 0;
            }));
        }
    }
}
