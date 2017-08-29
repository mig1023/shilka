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
        public const int MAX_FLIGHT_HEIGHT = 75;
        public static int maxFlightHeight { get; set; }
        public static int minFlightHeight { get; set; }
        enum FlightDirectionType { Left, Right };
        static Random rand;

        public double x { get; set; }
        public double y { get; set; }
        public double tangage { get; set; }
        int tangage_delay = 0;

        public int hitpoint;
        public Boolean dead = false;
        public Boolean fly = true;

        FlightDirectionType flightDirection;

       public Image aircraftImage;

        public static List<Aircraft> aircrafts = new List<Aircraft>();

        static Aircraft()
        {
            rand = new Random();
            maxFlightHeight = MAX_FLIGHT_HEIGHT;
        }

        public static void AircraftFly(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;

                foreach (var aircraft in Aircraft.aircrafts)
                {
                    if (aircraft.flightDirection == FlightDirectionType.Left)
                    {
                        aircraft.x -= 8;
                    }
                    else
                    {
                        aircraft.x += 8;
                    }

                    if (aircraft.dead)
                    {
                        aircraft.y += 10 * (Aircraft.rand.NextDouble() * 2 - 1);
                    }
                    else
                    {
                        aircraft.tangage_delay++;
                        if (aircraft.tangage_delay > 12)
                        {
                            aircraft.tangage_delay = 0;
                            aircraft.tangage = 4 * (Aircraft.rand.NextDouble() * 2 - 1);
                        }
                        aircraft.y += aircraft.tangage;
                        if (aircraft.y > Aircraft.minFlightHeight) aircraft.y = Aircraft.minFlightHeight;
                    }

                    if (aircraft.y < maxFlightHeight) aircraft.y = maxFlightHeight;

                    if (
                        ((aircraft.x + aircraft.aircraftImage.ActualWidth) < 0) && (aircraft.flightDirection == FlightDirectionType.Left) ||
                        (aircraft.x > main.ActualWidth) && (aircraft.flightDirection == FlightDirectionType.Right)
                    ) {
                        aircraft.fly = false;
                    }

                    aircraft.aircraftImage.Margin = new Thickness(aircraft.x, aircraft.y, 0, 0);
                }

                for (int x = 0; x < Aircraft.aircrafts.Count; x++)
                    if (Aircraft.aircrafts[x].fly == false)
                        Aircraft.aircrafts.RemoveAt(x);
            }));
        }

        public static void AircraftStart(object obj, ElapsedEventArgs e)
        {
            int newAircraft = Aircraft.rand.Next(4)+1;

            Shilka.statisticAllAircraft++;

            switch (newAircraft)
            {
                case 1: createNewAircraft("a10", 50, 204, 50); break;
                case 2: createNewAircraft("b1", 50, 406, 79); break;
                case 3: createNewAircraft("b52", 50, 406, 105); break;
                case 4: createNewAircraft("f117", 50, 204, 28); break;
            }
        }

        static void createNewAircraft(string aircraftName, int hitPoint, int aircraftWidth, int aircraftHeight)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;

                Image newAircraftImage = new Image();

                newAircraftImage.Width = aircraftWidth;
                newAircraftImage.Height = aircraftHeight;

                Aircraft newAircraft = new Aircraft();

                newAircraft.y = Aircraft.rand.Next(Aircraft.maxFlightHeight, Aircraft.minFlightHeight);

                if (Aircraft.rand.Next(2) == 1)
                {
                    newAircraft.flightDirection = FlightDirectionType.Right;
                    newAircraft.x = -1 * newAircraftImage.Width;
                }
                else
                {
                    newAircraft.flightDirection = FlightDirectionType.Left;
                    newAircraft.x = Application.Current.MainWindow.Width;
                }

                newAircraftImage.Source = new BitmapImage(new Uri("images/"+aircraftName+".png", UriKind.Relative)) { };

                if (newAircraft.flightDirection == FlightDirectionType.Left)
                {
                    newAircraftImage.FlowDirection = System.Windows.FlowDirection.RightToLeft;
                }

                newAircraftImage.Margin = new Thickness(newAircraft.x, newAircraft.y, 0, 0);

                newAircraft.hitpoint = hitPoint;

                newAircraft.aircraftImage = newAircraftImage;
                main.firePlace.Children.Add(newAircraftImage);
                Aircraft.aircrafts.Add(newAircraft);
            }));
        }
    }
}
