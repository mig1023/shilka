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

        public string aircraftType;
        public int price;
        public int hitpoint;
        public int hitpointMax;
        public int speed;
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
                        aircraft.x -= aircraft.speed;
                    }
                    else
                    {
                        aircraft.x += aircraft.speed;
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
                        if (!aircraft.dead)
                        {
                            Shilka.statisticHasGone++;

                            if (aircraft.hitpoint < aircraft.hitpointMax)
                            {
                                Shilka.statisticDamaged++;

                                double priceOfDamage = aircraft.price / ((double)aircraft.hitpointMax / (double)aircraft.hitpoint);
                                Shilka.statisticAmountOfDamage += (int)priceOfDamage;
                                Shilka.statisticLastDamage = " ( +" + priceOfDamage.ToString() + " повреждён " + aircraft.aircraftType + " )";
                            }
                            
                        }    
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
            int newAircraft = Aircraft.rand.Next(12)+1;

            Shilka.statisticAllAircraft++;

            switch (newAircraft)
            {
                case 1: createNewAircraft("a10", 120, 204, 50, 12, 5); break;
                case 2: createNewAircraft("b1", 50, 406, 79, 283, 12); break;
                case 3: createNewAircraft("b52", 80, 406, 105, 53, 8); break;
                case 4: createNewAircraft("f117", 50, 204, 28, 111, 10); break;
                case 5: createNewAircraft("f14", 80, 204, 54, 38, 10); break;
                case 6: createNewAircraft("f18", 80, 204, 55, 29, 10); break;
                case 7: createNewAircraft("f16", 80, 204, 65, 34, 10); break;
                case 8: createNewAircraft("f22", 50, 204, 47, 142, 12); break;
                case 9: createNewAircraft("f15", 80, 204, 48, 29, 10); break;
                case 10: createNewAircraft("f4", 80, 204, 56, 3, 8); break;
                case 11: createNewAircraft("tornado", 50, 204, 72, 111, 10); break;
                case 12: createNewAircraft("predator", 30, 140, 42, 4, 5); break;
            }
        }

        static void createNewAircraft(string aircraftName, int hitPoint, int aircraftWidth, int aircraftHeight, int price, int speed)
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

                newAircraft.aircraftType = aircraftName;
                newAircraft.hitpoint = hitPoint;
                newAircraft.hitpointMax = hitPoint;
                newAircraft.price = price;
                newAircraft.speed = speed;

                newAircraft.aircraftImage = newAircraftImage;
                main.firePlace.Children.Add(newAircraftImage);
                Aircraft.aircrafts.Add(newAircraft);
            }));
        }
    }
}
