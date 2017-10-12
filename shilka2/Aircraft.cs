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
        public const int AIRCRAFT_AVERAGE_PRICE = 81;
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
        public Boolean friend = false; 

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
                        if ((!aircraft.dead) && (!aircraft.friend))
                        {
                            Shilka.statisticHasGone++;

                            if (aircraft.hitpoint < aircraft.hitpointMax)
                            {
                                Shilka.statisticDamaged++;

                                double priceOfDamage = aircraft.price / ((double)aircraft.hitpointMax / (double)aircraft.hitpoint);
                                Shilka.statisticAmountOfDamage += (int)priceOfDamage;
                                Shilka.statisticLastDamage = String.Format(" ( +{0:f2} млн $ повреждён ", priceOfDamage) + 
                                    aircraft.aircraftType + " )";
                            }
                            
                        } 
                        else if ( (aircraft.hitpoint < aircraft.hitpointMax) && aircraft.friend) Shilka.statisticFriendDamage++;
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

            switch (newAircraft)
            {
                case 1:
                case 2:
                case 3:
                    int newEnemyAircraft = Aircraft.rand.Next(15) + 1;

                    switch (newEnemyAircraft)
                    {
                        case 1:
                            createNewAircraft("a10", 200, 204, 50, 12, 5, false); break;
                        case 2:
                            createNewAircraft("b1", 90, 406, 79, 283, 12, false); break;
                        case 3:
                            createNewAircraft("b52", 120, 406, 105, 53, 8, false); break;
                        case 4:
                            createNewAircraft("f117", 50, 204, 28, 111, 10, false); break;
                        case 5:
                            createNewAircraft("f14", 120, 204, 54, 38, 10, false); break;
                        case 6:
                            createNewAircraft("f18", 120, 204, 55, 29, 10, false); break;
                        case 7:
                            createNewAircraft("f16", 120, 204, 65, 34, 10, false); break;
                        case 8:
                            createNewAircraft("f22", 90, 204, 47, 142, 14, false); break;
                        case 9:
                            createNewAircraft("f15", 120, 204, 53, 29, 10, false); break;
                        case 10:
                            createNewAircraft("f4", 150, 204, 56, 3, 8, false); break;
                        case 11:
                            createNewAircraft("tornado", 100, 204, 72, 111, 10, false); break;
                        case 12:
                            createNewAircraft("predator", 30, 140, 38, 4, 5, false); break;
                        case 13:
                            createNewAircraft("reaper", 50, 140, 50, 16, 5, false); break;
                        case 14:
                            createNewAircraft("f35", 90, 204, 52, 83, 10, false); break;
                        case 15:
                            createNewAircraft("e3", 150, 406, 110, 270, 8, false); break;
                    }
                    break;

                case 4:
                    int newFriendAircraft = Aircraft.rand.Next(9) + 1;

                    switch (newFriendAircraft)
                    {
                        case 1:
                            createNewAircraft("mig23", 80, 204, 55, 0, 10, true); break;
                        case 2:
                            createNewAircraft("mig29", 80, 204, 51, 0, 10, true); break;
                        case 3:
                            createNewAircraft("mig31", 80, 204, 57, 0, 14, true); break;
                        case 4:
                            createNewAircraft("su17", 80, 204, 52, 0, 5, true); break;
                        case 5:
                            createNewAircraft("su24", 80, 204, 53, 0, 8, true); break;
                        case 6:
                            createNewAircraft("su25", 180, 204, 63, 0, 5, true); break;
                        case 7:
                            createNewAircraft("su27", 80, 204, 50, 0, 10, true); break;
                        case 8:
                            createNewAircraft("su34", 100, 204, 47, 0, 10, true); break;
                        case 9:
                            createNewAircraft("pakfa", 80, 204, 45, 0, 12, true); break;
                    }
                    break;
            }
        }

        static void createNewAircraft(string aircraftName, int hitPoint, int aircraftWidth, int aircraftHeight, int price, 
            int speed, Boolean friend)
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
                newAircraft.friend = friend;

                if (!friend)
                {
                    Shilka.statisticAllAircraft++;
                    Shilka.statisticPriceOfAllAircrafts += price;
                }

                newAircraft.aircraftImage = newAircraftImage;
                main.firePlace.Children.Add(newAircraftImage);
                Aircraft.aircrafts.Add(newAircraft);
            }));
        }
    }
}
