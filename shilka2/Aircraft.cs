using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Resources;
using System.Timers;

namespace shilka2
{
    class Aircraft
    {
        enum FlightDirectionType { Left, Right };

        public double x { get; set; }
        public double y { get; set; }

        FlightDirectionType flightDirection;

        Image aircraftImage = new Image();
        public static List<Aircraft> aircrafts = new List<Aircraft>();

        public static void AircraftFly(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;

                foreach (var aircraft in Aircraft.aircrafts)
                {
                    if (aircraft.flightDirection == FlightDirectionType.Left)
                    {
                        aircraft.x -= 1;
                    }
                    else
                    {
                        aircraft.x += 1;
                    }
                    aircraft.aircraftImage.Margin = new Thickness(aircraft.x, 100, 0, 0);
                }
            }));
        }

        public static void AircraftStart()
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;

                Aircraft newAircraft = new Aircraft();

                newAircraft.x = Application.Current.MainWindow.Width + 400;
                newAircraft.y = 100;
                newAircraft.flightDirection = FlightDirectionType.Right;
                
                Image newAircraftImage = new Image();

                string direction = (newAircraft.flightDirection == FlightDirectionType.Left ? "left" : "right");

                newAircraftImage.Source = new BitmapImage(new Uri("images/f-117-"+direction+".png", UriKind.Relative)) {};
                newAircraftImage.Margin = new Thickness(newAircraft.x, newAircraft.y, 0, 0);
                newAircraftImage.Width = 204;
                newAircraftImage.Height = 50;

                newAircraft.aircraftImage = newAircraftImage;
                main.firePlace.Children.Add(newAircraftImage);
                Aircraft.aircrafts.Add(newAircraft);
            }));
        }


    }
}
