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
using shilka2.classes;

namespace shilka2
{
    class Aircraft : FlyObject
    {
        public const int MAX_FLIGHT_HEIGHT = 75;
        public const int AIRCRAFT_AVERAGE_PRICE = 81;
        const double ESCAPE_COEFFICIENT = 1.6;
        const int TANGAGE_DELAY = 12;
        const int TANGAGE_SPEED = 4;
        const int TANGAGE_DEAD_SPEED = 10;

        public static int maxAltitudeGlobal = MAX_FLIGHT_HEIGHT;
        public static int minAltitudeGlobal { get; set; }
        public static int minAltitudeForLargeAircraft = (int)System.Windows.SystemParameters.PrimaryScreenHeight / 2;
        enum FlightDirectionType { Left, Right };

        public double tangage { get; set; }
        int tangage_delay = 0;

        public string aircraftType;
        public int price;
        public int hitpoint;
        public int hitpointMax;
        public int speed;
        public int minAltitude;

        public Boolean dead = false;
        public Boolean friend = false;
        public Boolean cloud = false;
        public Boolean cantEscape = false;

        FlightDirectionType flightDirection;

        public Image aircraftImage;

        public static List<Aircraft> aircrafts = new List<Aircraft>();

        public static void AircraftFly(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;

                foreach (var aircraft in Aircraft.aircrafts)
                {
                    double escapeFromFireCoefficient = 1;

                    if ((aircraft.hitpoint < aircraft.hitpointMax) && !aircraft.cantEscape)
                        escapeFromFireCoefficient = ESCAPE_COEFFICIENT;

                    if (aircraft.flightDirection == FlightDirectionType.Left)
                        aircraft.x -= aircraft.speed * escapeFromFireCoefficient;
                    else
                        aircraft.x += aircraft.speed * escapeFromFireCoefficient;

                    if (aircraft.dead)
                        aircraft.y += TANGAGE_DEAD_SPEED * (rand.NextDouble() * 2 - 1) + 4;
                    else
                        if (!aircraft.cloud)
                        {
                            aircraft.tangage_delay++;
                            if (aircraft.tangage_delay > TANGAGE_DELAY)
                            {
                                aircraft.tangage_delay = 0;
                                aircraft.tangage = TANGAGE_SPEED * (rand.NextDouble() * 2 - 1);
                            }
                            aircraft.y += aircraft.tangage;

                            if (aircraft.y > aircraft.minAltitude) aircraft.y = aircraft.minAltitude;
                        }

                    if (aircraft.y < maxAltitudeGlobal) aircraft.y = maxAltitudeGlobal;

                    if (
                        ((aircraft.x + aircraft.aircraftImage.Width) < 0) && (aircraft.flightDirection == FlightDirectionType.Left)
                        ||
                        (aircraft.x > main.Width) && (aircraft.flightDirection == FlightDirectionType.Right)
                    ) {
                        aircraft.fly = false;

                        if ((!aircraft.dead) && (!aircraft.friend))
                        {
                            Shilka.statisticHasGone++;

                            if (aircraft.hitpoint < aircraft.hitpointMax)
                            {
                                Shilka.statisticDamaged++;

                                double residualValue =  aircraft.price * (double)aircraft.hitpoint / (double)aircraft.hitpointMax;
                                int priceOfDamage = aircraft.price - (int)residualValue;
                                Shilka.statisticAmountOfDamage += priceOfDamage;

                                Shilka.statisticShutdownFlag = false;
                                Shilka.statisticLastDamagePrice = priceOfDamage;
                                Shilka.statisticLastDamageType = aircraft.aircraftType;
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
            int newAircraft = rand.Next(10)+1;

            switch (newAircraft)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    createNewAircraft(
                        aircraftName: "cloud" + (int)(rand.Next(7) + 1),
                        hitPoint: 10,
                        aircraftWidth: (int)(rand.Next(300) + 200),
                        aircraftHeight: (int)(rand.Next(100) + 70),
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
                    switch ((int)(rand.Next(20) + 1))
                    {
                        case 1:
                            createNewAircraft(
                                aircraftName: "a10",
                                hitPoint: 200,
                                aircraftWidth: 270,
                                aircraftHeight: 68,
                                price: 12,
                                speed: 5,
                                cantEscape: true
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
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true
                            ); break;
                        case 4:
                            createNewAircraft(
                                aircraftName: "f117",
                                hitPoint: 50,
                                aircraftWidth: 270,
                                aircraftHeight: 47,
                                price: 111
                            ); break;
                        case 5:
                            createNewAircraft(
                                aircraftName: "f14",
                                hitPoint: 120,
                                aircraftWidth: 275,
                                aircraftHeight: 67,
                                price: 38
                            ); break;
                        case 6:
                            createNewAircraft(
                                aircraftName: "f18",
                                hitPoint: 120,
                                aircraftWidth: 270,
                                aircraftHeight: 61,
                                price: 29
                            ); break;
                        case 7:
                            createNewAircraft(
                                aircraftName: "f16",
                                hitPoint: 120,
                                aircraftWidth: 270,
                                aircraftHeight: 89,
                                price: 34
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
                                price: 29
                            ); break;
                        case 10:
                            createNewAircraft(
                                aircraftName: "f4",
                                hitPoint: 150,
                                aircraftWidth: 270,
                                aircraftHeight: 64,
                                price: 3,
                                speed: 8
                            ); break;
                        case 11:
                            createNewAircraft(
                                aircraftName: "tornado",
                                hitPoint: 100,
                                aircraftWidth: 270,
                                aircraftHeight: 72,
                                price: 111
                            ); break;
                        case 12:
                            createNewAircraft(
                                aircraftName: "predator",
                                hitPoint: 30,
                                aircraftWidth: 140,
                                aircraftHeight: 44,
                                price: 4,
                                speed: 5,
                                cantEscape: true
                            ); break;
                        case 13:
                            createNewAircraft(
                                aircraftName: "reaper",
                                hitPoint: 50,
                                aircraftWidth: 161,
                                aircraftHeight: 52,
                                price: 16,
                                speed: 5,
                                cantEscape: true
                            ); break;
                        case 14:
                            createNewAircraft(
                                aircraftName: "f35",
                                hitPoint: 90,
                                aircraftWidth: 270,
                                aircraftHeight: 76,
                                price: 83
                            ); break;
                        case 15:
                            createNewAircraft(
                                aircraftName: "e3",
                                hitPoint: 150,
                                aircraftWidth: 581,
                                aircraftHeight: 164,
                                price: 270,
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft
                            ); break;
                        case 16:
                            createNewAircraft(
                                aircraftName: "eurofighter",
                                hitPoint: 100,
                                aircraftWidth: 270,
                                aircraftHeight: 77,
                                price: 123
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
                                aircraftWidth: 332,
                                aircraftHeight: 76,
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
                                speed: 7,
                                cantEscape: true
                            ); break;
                        case 20:
                            createNewAircraft(
                                aircraftName: "tomahawk",
                                hitPoint: 20,
                                aircraftWidth: 125,
                                aircraftHeight: 29,
                                price: 2,
                                speed: 5,
                                cantEscape: true
                            ); break;
                    }
                    break;

                case 10:
                    switch ((int)(rand.Next(10) + 1))
                    {
                        case 1:
                            createNewAircraft(
                                aircraftName: "mig23",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 71,
                                friend: true
                            ); break;
                        case 2:
                            createNewAircraft(
                                aircraftName: "mig29",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 65,
                                friend: true
                            ); break;
                        case 3:
                            createNewAircraft(
                                aircraftName: "mig31",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 63,
                                speed: 14,
                                friend: true
                            ); break;
                        case 4:
                            createNewAircraft(
                                aircraftName: "su17",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 61,
                                speed: 5,
                                friend: true
                            ); break;
                        case 5:
                            createNewAircraft(
                                aircraftName: "su24",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 67,
                                speed: 8,
                                friend: true
                            ); break;
                        case 6:
                            createNewAircraft(
                                aircraftName: "su25",
                                hitPoint: 180,
                                aircraftWidth: 270,
                                aircraftHeight: 81,
                                speed: 5,
                                friend: true,
                                cantEscape: true
                            ); break;
                        case 7:
                            createNewAircraft(
                                aircraftName: "su27",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 77,
                                friend: true
                            ); break;
                        case 8:
                            createNewAircraft(
                                aircraftName: "su34",
                                hitPoint: 100,
                                aircraftWidth: 275,
                                aircraftHeight: 56,
                                friend: true
                            ); break;
                        case 9:
                            createNewAircraft(
                                aircraftName: "pakfa",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 57,
                                speed: 12,
                                friend: true
                            ); break;
                        case 10:
                            createNewAircraft(
                                aircraftName: "tu160",
                                hitPoint: 120,
                                aircraftWidth: 510,
                                aircraftHeight: 108,
                                speed: 18,
                                minAltitude: minAltitudeForLargeAircraft,
                                friend: true
                            ); break;
                    }
                    break;
            }
        }

        static void createNewAircraft(string aircraftName, int hitPoint, int aircraftWidth, int aircraftHeight, 
            int speed = 10, int minAltitude = -1, Boolean friend = false, Boolean cloud = false, Boolean cantEscape = false,
            int price = 0)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;

                Image newAircraftImage = new Image();

                newAircraftImage.Width = aircraftWidth;
                newAircraftImage.Height = aircraftHeight;

                Aircraft newAircraft = new Aircraft();

                newAircraft.y = rand.Next(Aircraft.maxAltitudeGlobal, Aircraft.minAltitudeGlobal);

                if (rand.Next(2) == 1)
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

                if ( ( (newAircraft.flightDirection == FlightDirectionType.Left) && !cloud ) || (rand.Next(2) == 1) && cloud )
                    newAircraftImage.FlowDirection = System.Windows.FlowDirection.RightToLeft;

                newAircraftImage.Margin = new Thickness(newAircraft.x, newAircraft.y, 0, 0);

                newAircraft.aircraftType = aircraftName;
                newAircraft.hitpoint = hitPoint;
                newAircraft.hitpointMax = hitPoint;
                newAircraft.price = price;
                newAircraft.speed = speed;
                newAircraft.minAltitude = minAltitude;
                newAircraft.friend = friend;
                newAircraft.cloud = cloud;
                newAircraft.cantEscape = cantEscape;
                newAircraft.fly = true;

                if (newAircraft.minAltitude == -1) newAircraft.minAltitude = minAltitudeGlobal;

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
