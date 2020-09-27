using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace shilka2
{
    public class Weather : FlyObject
    {
        public enum WeatherTypes { good, rain, storm, snow, sand };

        int direction = 0;

        public static Aircraft.FlightDirectionType stormDirection;

        public Image weatherImage;
        
        WeatherTypes type;

        public static List<Weather> weather = new List<Weather>();
        static int weatherCycle = 0;
        public static WeatherTypes currentWeather = WeatherTypes.good;
        static int weatherMutex = 0;

        static int thundar = 0;
        public static Image thunderCurrentImage;

        public static void Restart()
        {
            Array weathers = Enum.GetValues(typeof(WeatherTypes));
            WeatherTypes currentWeatherForScripts = (WeatherTypes)weathers.GetValue(RandForNewWeather(weathers.Length));
            currentWeather = Scripts.ScriptsWeather(Shilka.currentScript, currentWeatherForScripts);

            Restart(currentWeather, newWeatherCycle: 0);
        }

        public static void Restart(Weather.WeatherTypes newWeather, int? newWeatherCycle = null)
        {
            currentWeather = newWeather;
            weatherCycle = newWeatherCycle ?? Constants.WEATHER_CYCLE;
        }

        private static int RandForNewWeather(int maxWeatherNum)
        {
            int weatherCategory = rand.Next(1, 6);

            switch (weatherCategory)
            {
                case 1:
                case 2:
                case 3:
                    return 0;

                case 4:
                case 5:
                default:
                    return rand.Next(maxWeatherNum);
            };
        }

        private static void Lightning(bool thunderclap = false, bool thunderimage = false)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                if (thunderimage)
                {
                    Image thuderImage = new Image
                    {
                        Height = rand.Next(200, (int)SystemParameters.PrimaryScreenHeight),
                        Source = Functions.ImageFromResources("thunder" + (rand.Next(1, 7)), Aircraft.ImageType.Other),
                        Margin = new Thickness(rand.Next(0, (int)SystemParameters.PrimaryScreenWidth), -10, 0, 0)
                    };

                    thunderCurrentImage = thuderImage;
                    main.firePlace.Children.Add(thuderImage);
                }
                else
                {
                    if (thunderCurrentImage != null)
                        main.firePlace.Children.Remove(thunderCurrentImage);
                }

                main.thunderPlace.Background = (Shilka.currentScript == Scripts.ScriptsNames.Yugoslavia ? Brushes.White : Brushes.Black);
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

        public static void Change(object obj, ElapsedEventArgs e)
        {
            if (Shilka.training || Shilka.school)
                return;

            if ((thundar != 0) || (currentWeather == WeatherTypes.storm))
                Thunder();

            if (weatherCycle < Constants.WEATHER_CYCLE)
            {
                weatherCycle += 1;

                if (currentWeather == WeatherTypes.storm)
                    if (weatherCycle.ToString().Contains("00"))
                        stormDirection = (rand.Next(2) > 0 ? Aircraft.FlightDirectionType.Right : Aircraft.FlightDirectionType.Left);
            }
            else
                Restart();

            if (currentWeather == WeatherTypes.good)
                return;

            int newWeatherElementsCount = (currentWeather == WeatherTypes.storm ? 4 : 1);

            for (int iterator = 0; iterator < newWeatherElementsCount; iterator++)
            {
                Weather newWeather = new Weather();

                if (currentWeather == WeatherTypes.storm)
                {
                    newWeather.x = rand.Next((int)SystemParameters.PrimaryScreenWidth * -1, (int)SystemParameters.PrimaryScreenWidth * 2);
                    newWeather.y = 0;
                }
                else if (currentWeather == WeatherTypes.sand)
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

                    if ((currentWeather == WeatherTypes.rain) || (currentWeather == WeatherTypes.storm))
                    {
                        newImage.Width = rand.Next(Constants.RAIN_MIN_WIDTH, Constants.RAIN_MAX_WIDTH);
                        newImage.Height = rand.Next(Constants.RAIN_MIN_HEIGHT, Constants.RAIN_MAX_HEIGHT);
                        imageName = "rain" + rand.Next(1, Constants.MAX_RAIN_TYPE + 1).ToString();
                    }
                    else if (currentWeather == WeatherTypes.sand)
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

                    newImage.Source = Functions.ImageFromResources(imageName, Aircraft.ImageType.Other);
                    newImage.Margin = new Thickness(newWeather.x, newWeather.y, 0, 0);

                    newWeather.weatherImage = newImage;

                    FirePlace main = (FirePlace)Application.Current.MainWindow;
                    Canvas.SetZIndex(newImage, 120);
                    main.firePlace.Children.Add(newImage);
                    Weather.weather.Add(newWeather);
                }));
            }
        }

        public static void ElementsFly(object obj, ElapsedEventArgs e)
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
                    if (w.type == WeatherTypes.snow)
                    {
                        if (rand.Next(Constants.SNOW_DIRECTION_CHANGE_CHANCE) == 1)
                            w.direction = rand.Next(
                                (Constants.SNOW_DIRECTION_FLY_SPEED * -1), Constants.SNOW_DIRECTION_FLY_SPEED + 1
                            );

                        w.x += w.direction;
                    }

                    if (w.type == WeatherTypes.sand)
                    {
                        if (rand.Next(Constants.SNOW_DIRECTION_CHANGE_CHANCE) == 1)
                            w.direction = rand.Next(
                                (Constants.SNOW_DIRECTION_FLY_SPEED * -1), Constants.SNOW_DIRECTION_FLY_SPEED + 1
                            );

                        w.y += w.direction;
                        w.x = (w.x + w.speed);
                    }
                    else
                        w.y = (w.y + w.speed);

                    if (currentWeather == WeatherTypes.storm)
                        w.x += Constants.STORM_FLY_SPEED * (stormDirection == Aircraft.FlightDirectionType.Right ? 1 : -1);

                    w.weatherImage.Margin = new Thickness(w.x, w.y, 0, 0);

                    if (w.y > SystemParameters.PrimaryScreenHeight + 100)
                        w.fly = false;

                    if ((w.type == WeatherTypes.sand) && (w.x > SystemParameters.PrimaryScreenWidth + 10))
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
