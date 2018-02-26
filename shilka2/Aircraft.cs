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
        public Boolean cloud = false;

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
                        aircraft.y += 10 * (Aircraft.rand.NextDouble() * 2 - 1) + 4;
                    }
                    else
                    {
                        if (!aircraft.cloud)
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
                        
                    }

                    if (aircraft.y < maxFlightHeight) aircraft.y = maxFlightHeight;

                    if (
                        ((aircraft.x + aircraft.aircraftImage.Width) < 0) && (aircraft.flightDirection == FlightDirectionType.Left) ||
                        (aircraft.x > main.Width) && (aircraft.flightDirection == FlightDirectionType.Right)
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
            int newAircraft = Aircraft.rand.Next(10)+1;

            switch (newAircraft)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    createNewAircraft(
                        aircraftName: "cloud" + (int)(Aircraft.rand.Next(7) + 1),
                        hitPoint: 10,
                        aircraftWidth: (int)(Aircraft.rand.Next(300) + 200),
                        aircraftHeight: (int)(Aircraft.rand.Next(100) + 70),
                        price: 0,
                        speed: 5,
                        friend: true,
                        cloud: true
                    );
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    switch ((int)(Aircraft.rand.Next(19) + 1))
                    {
                        case 1:
                            createNewAircraft(
                                aircraftName: "a10",
                                hitPoint: 200,
                                aircraftWidth: 270,
                                aircraftHeight: 68,
                                price: 12,
                                speed: 5
                            ); break;
                        case 2:
                            createNewAircraft(
                                aircraftName: "b1",
                                hitPoint: 90,
                                aircraftWidth: 510,
                                aircraftHeight: 108,
                                price: 283,
                                speed: 12
                            ); break;
                        case 3:
                            createNewAircraft(
                                aircraftName: "b52",
                                hitPoint: 120,
                                aircraftWidth: 565,
                                aircraftHeight: 155,
                                price: 53,
                                speed: 8
                            ); break;
                        case 4:
                            createNewAircraft(
                                aircraftName: "f117",
                                hitPoint: 50,
                                aircraftWidth: 270,
                                aircraftHeight: 48,
                                price: 111,
                                speed: 10
                            ); break;
                        case 5:
                            createNewAircraft(
                                aircraftName: "f14",
                                hitPoint: 120,
                                aircraftWidth: 275,
                                aircraftHeight: 67,
                                price: 38,
                                speed: 10
                            ); break;
                        case 6:
                            createNewAircraft(
                                aircraftName: "f18",
                                hitPoint: 120,
                                aircraftWidth: 270,
                                aircraftHeight: 61,
                                price: 29,
                                speed: 10
                            ); break;
                        case 7:
                            createNewAircraft(
                                aircraftName: "f16",
                                hitPoint: 120,
                                aircraftWidth: 270,
                                aircraftHeight: 84,
                                price: 34,
                                speed: 10
                            ); break;
                        case 8:
                            createNewAircraft(
                                aircraftName: "f22",
                                hitPoint: 90,
                                aircraftWidth: 270,
                                aircraftHeight: 73,
                                price: 142,
                                speed: 14
                            ); break;
                        case 9:
                            createNewAircraft(
                                aircraftName: "f15",
                                hitPoint: 120,
                                aircraftWidth: 270,
                                aircraftHeight: 62,
                                price: 29,
                                speed: 10
                            ); break;
                        case 10:
                            createNewAircraft(
                                aircraftName: "f4",
                                hitPoint: 150,
                                aircraftWidth: 270,
                                aircraftHeight: 66,
                                price: 3,
                                speed: 8
                            ); break;
                        case 11:
                            createNewAircraft(
                                aircraftName: "tornado",
                                hitPoint: 100,
                                aircraftWidth: 270,
                                aircraftHeight: 72,
                                price: 111,
                                speed: 10
                            ); break;
                        case 12:
                            createNewAircraft(
                                aircraftName: "predator",
                                hitPoint: 30,
                                aircraftWidth: 140,
                                aircraftHeight: 44,
                                price: 4,
                                speed: 5
                            ); break;
                        case 13:
                            createNewAircraft(
                                aircraftName: "reaper",
                                hitPoint: 50,
                                aircraftWidth: 161,
                                aircraftHeight: 52,
                                price: 16,
                                speed: 5
                            ); break;
                        case 14:
                            createNewAircraft(
                                aircraftName: "f35",
                                hitPoint: 90,
                                aircraftWidth: 270,
                                aircraftHeight: 76,
                                price: 83,
                                speed: 10
                            ); break;
                        case 15:
                            createNewAircraft(
                                aircraftName: "e3",
                                hitPoint: 150,
                                aircraftWidth: 581,
                                aircraftHeight: 159,
                                price: 270,
                                speed: 8
                            ); break;
                        case 16:
                            createNewAircraft(
                                aircraftName: "eurofighter",
                                hitPoint: 100,
                                aircraftWidth: 270,
                                aircraftHeight: 77,
                                price: 123,
                                speed: 10
                            ); break;
                        case 17:
                            createNewAircraft(
                                aircraftName: "rafale",
                                hitPoint: 90,
                                aircraftWidth: 270,
                                aircraftHeight: 86,
                                price: 85,
                                speed: 11
                            ); break;
                        case 18:
                            createNewAircraft(
                                aircraftName: "b2",
                                hitPoint: 125,
                                aircraftWidth: 474,
                                aircraftHeight: 108,
                                price: 2100,
                                speed: 18
                            ); break;
                        case 19:
                            createNewAircraft(
                                aircraftName: "globalhawk",
                                hitPoint: 125,
                                aircraftWidth: 265,
                                aircraftHeight: 85,
                                price: 70,
                                speed: 7
                            ); break;
                    }
                    break;

                case 10:
                    switch ((int)(Aircraft.rand.Next(10) + 1))
                    {
                        case 1:
                            createNewAircraft(
                                aircraftName: "mig23",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 79,
                                price: 0,
                                speed: 10,
                                friend: true
                            ); break;
                        case 2:
                            createNewAircraft(
                                aircraftName: "mig29",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 65,
                                price: 0,
                                speed: 10,
                                friend: true
                            ); break;
                        case 3:
                            createNewAircraft(
                                aircraftName: "mig31",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 74,
                                price: 0,
                                speed: 14,
                                friend: true
                            ); break;
                        case 4:
                            createNewAircraft(
                                aircraftName: "su17",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 61,
                                price: 0,
                                speed: 5,
                                friend: true
                            ); break;
                        case 5:
                            createNewAircraft(
                                aircraftName: "su24",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 67,
                                price: 0,
                                speed: 8,
                                friend: true
                            ); break;
                        case 6:
                            createNewAircraft(
                                aircraftName: "su25",
                                hitPoint: 180,
                                aircraftWidth: 270,
                                aircraftHeight: 81,
                                price: 0,
                                speed: 5,
                                friend: true
                            ); break;
                        case 7:
                            createNewAircraft(
                                aircraftName: "su27",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 77,
                                price: 0,
                                speed: 10,
                                friend: true
                            ); break;
                        case 8:
                            createNewAircraft(
                                aircraftName: "su34",
                                hitPoint: 100,
                                aircraftWidth: 275,
                                aircraftHeight: 56,
                                price: 0,
                                speed: 10,
                                friend: true
                            ); break;
                        case 9:
                            createNewAircraft(
                                aircraftName: "pakfa",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 57,
                                price: 0,
                                speed: 12,
                                friend: true
                            ); break;
                        case 10:
                            createNewAircraft(
                                aircraftName: "tu160",
                                hitPoint: 120,
                                aircraftWidth: 510,
                                aircraftHeight: 108,
                                price: 0,
                                speed: 18,
                                friend: true
                            ); break;
                    }
                    break;
            }
        }

        static void createNewAircraft(string aircraftName, int hitPoint, int aircraftWidth, int aircraftHeight, int price, 
            int speed, Boolean friend = false, Boolean cloud = false)
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

                if ( ( (newAircraft.flightDirection == FlightDirectionType.Left) && !cloud ) || (Aircraft.rand.Next(2) == 1) && cloud )
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
                newAircraft.cloud = cloud;

                if (!friend)
                {
                    Shilka.statisticAllAircraft++;
                    Shilka.statisticPriceOfAllAircrafts += price;
                }

                Canvas.SetZIndex(newAircraftImage, ( cloud ? 100 : 50) );

                newAircraft.aircraftImage = newAircraftImage;
                main.firePlace.Children.Add(newAircraftImage);
                Aircraft.aircrafts.Add(newAircraft);
            }));
        }
    }
}
