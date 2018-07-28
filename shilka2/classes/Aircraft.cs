using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Timers;
using shilka2.classes;
using System.Windows.Media;

namespace shilka2
{
    class Aircraft : FlyObject
    {
        const int MAX_FLIGHT_HEIGHT = 75;
        public const int AIRCRAFT_AVERAGE_PRICE = 72;
        const double ESCAPE_COEFFICIENT = 1.6;
        const int TANGAGE_DELAY = 12;
        const int TANGAGE_SPEED = 4;
        const int TANGAGE_DEAD_SPEED = 10;

        static int maxAltitudeGlobal = MAX_FLIGHT_HEIGHT;
        public static int minAltitudeGlobal { get; set; }
        static int minAltitudeForLargeAircraft = (int)SystemParameters.PrimaryScreenHeight / 2;
        static int maxAltitudeForHelicopters = minAltitudeForLargeAircraft;
        enum FlightDirectionType { Left, Right };
        enum zIndexType { inFront, Behind };

        double tangage { get; set; }
        int tangageDelay = 0;

        public string aircraftType;
        public string aircraftName;
        public int price;
        public int hitpoint;
        public int hitpointMax;
        public int speed;
        public int minAltitude;
        public int maxAltitude;

        public bool dead = false;
        public bool friend = false;
        public bool airliner = false;
        public bool cloud = false;
        public bool cantEscape = false;

        FlightDirectionType flightDirection;

        public Image aircraftImage;

        public List<DynamicElement> dynamicElemets = new List<DynamicElement>(); 

        public static List<Aircraft> aircrafts = new List<Aircraft>();

        public static void AircraftFly(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;

                foreach (var aircraft in aircrafts)
                {
                    double escapeFromFireCoefficient = 1;

                    if ((aircraft.hitpoint < aircraft.hitpointMax) && !aircraft.cantEscape)
                        escapeFromFireCoefficient = ESCAPE_COEFFICIENT;

                    if (aircraft.flightDirection == FlightDirectionType.Left)
                        escapeFromFireCoefficient *= -1;

                    aircraft.x += aircraft.speed * escapeFromFireCoefficient;

                    if (aircraft.dead)
                        aircraft.y += TANGAGE_DEAD_SPEED * (rand.NextDouble() * 2 - 1) + 4;
                    else
                        if (!aircraft.cloud)
                        {
                            aircraft.tangageDelay++;
                            if (aircraft.tangageDelay > TANGAGE_DELAY)
                            {
                                aircraft.tangageDelay = 0;
                                aircraft.tangage = TANGAGE_SPEED * (rand.NextDouble() * 2 - 1);
                            }
                            aircraft.y += aircraft.tangage;

                            if (aircraft.y > aircraft.minAltitude) aircraft.y = aircraft.minAltitude;
                        }

                    if ((aircraft.maxAltitude >= 0) && (aircraft.y < aircraft.maxAltitude))
                        aircraft.y = aircraft.maxAltitude;
                    else if (aircraft.y < maxAltitudeGlobal)
                        aircraft.y = maxAltitudeGlobal;

                    if (
                        ((aircraft.x + aircraft.aircraftImage.Width) < 0) && (aircraft.flightDirection == FlightDirectionType.Left)
                        ||
                        (aircraft.x > main.Width) && (aircraft.flightDirection == FlightDirectionType.Right)
                    ) {
                        aircraft.fly = false;

                        if ((!aircraft.dead) && (!aircraft.friend) && (!aircraft.airliner))
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
                                Shilka.statisticLastDamageType = aircraft.aircraftName;
                            }
                        } 
                        else if ( (aircraft.hitpoint < aircraft.hitpointMax) && aircraft.friend) Shilka.statisticFriendDamage++;
                    }

                    aircraft.aircraftImage.Margin = new Thickness(aircraft.x, aircraft.y, 0, 0);

                    foreach (DynamicElement d in aircraft.dynamicElemets)
                    {
                        if (aircraft.flightDirection == FlightDirectionType.Left)
                            d.element.Margin = new Thickness(aircraft.x + d.x_left, aircraft.y + d.y, 0, 0);
                        else
                            d.element.Margin = new Thickness(aircraft.x + d.x_right, aircraft.y + d.y, 0, 0);

                        if (d.movingType == DynamicElement.MovingType.zRotate)
                        {
                            int direction = (aircraft.flightDirection == FlightDirectionType.Left ? 1 : -1);

                            d.rotateDegreeCurrent += (25 * direction);
                            if (d.rotateDegreeCurrent < -180 || d.rotateDegreeCurrent > 180) d.rotateDegreeCurrent = 0;
                                
                            d.element.RenderTransform = new RotateTransform(d.rotateDegreeCurrent, (d.element.ActualWidth / 2), (d.element.ActualHeight / 2));
                        }

                        if (d.movingType == DynamicElement.MovingType.xRotate || d.movingType == DynamicElement.MovingType.yRotate)
                        {
                            d.rotateDegreeCurrent -= 0.2;
                            if (d.rotateDegreeCurrent < 0.2) d.rotateDegreeCurrent = 1;

                            if (d.movingType == DynamicElement.MovingType.xRotate)
                                d.element.RenderTransform = new ScaleTransform(d.rotateDegreeCurrent, 1, (d.element.ActualWidth/2), 0);
                            else
                                d.element.RenderTransform = new ScaleTransform(1, d.rotateDegreeCurrent, 0, (d.element.ActualHeight / 2));
                        }
                    }
                }

                for (int x = 0; x < aircrafts.Count; x++)
                    if (aircrafts[x].fly == false)
                    {
                        main.firePlace.Children.Remove(aircrafts[x].aircraftImage);

                        foreach (DynamicElement d in aircrafts[x].dynamicElemets)
                            main.firePlace.Children.Remove(d.element);

                        aircrafts.RemoveAt(x);
                    }
            }));
        }

        private static bool aircraftInList(int?[] scriptAircraft, int aircraft)
        {
            if (scriptAircraft == null) return false;

            if (scriptAircraft.Length == 0) return true;

            bool inList = false;

            foreach (int aircraftInList in scriptAircraft)
                if (aircraftInList == aircraft) inList = true;

            return inList;
        }

        static ImageSource imageFromResources(string imageName)
        {
            return new BitmapImage(new Uri("images/" + imageName + ".png", UriKind.Relative)) { };
        }

        static void createNewAircraft(string aircraftType, int hitPoint, int aircraftWidth, int aircraftHeight,
            string aircraftName = "", int speed = 10, int minAltitude = -1, int maxAltitude = -1, bool friend = false,
            bool airliner = false, bool cloud = false, bool cantEscape = false, int price = 0)
        {
            List<DynamicElement> elements = new List<DynamicElement>();

            createNewAircraft(aircraftType, hitPoint, aircraftWidth, aircraftHeight, elements, aircraftName,
                speed, minAltitude, maxAltitude, friend, airliner, cloud, cantEscape, price);
        }

        static void createNewAircraft(string aircraftType, int hitPoint, int aircraftWidth, int aircraftHeight,
            List<DynamicElement> elements, string aircraftName = "", int speed = 10, int minAltitude = -1,
            int maxAltitude = -1, bool friend = false, bool airliner = false, bool cloud = false,
            bool cantEscape = false, int price = 0)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;

                zIndexType? zIndex = null;

                Image newAircraftImage = new Image();

                newAircraftImage.Width = aircraftWidth;
                newAircraftImage.Height = aircraftHeight;

                Aircraft newAircraft = new Aircraft();

                newAircraft.y = rand.Next(maxAltitudeGlobal, minAltitudeGlobal);

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

                newAircraftImage.Source = imageFromResources(aircraftType);

                if (((newAircraft.flightDirection == FlightDirectionType.Left) && !cloud) || (rand.Next(2) == 1) && cloud)
                    newAircraftImage.FlowDirection = FlowDirection.RightToLeft;

                newAircraftImage.Margin = new Thickness(newAircraft.x, newAircraft.y, 0, 0);

                if (elements.Count > 0)
                    zIndex = (rand.Next(2) > 0 ? zIndexType.inFront : zIndexType.Behind);

                foreach (DynamicElement d in elements)
                {
                    d.element = new Image();
                    d.element.Source = imageFromResources(d.elementName);

                    if (newAircraft.flightDirection == FlightDirectionType.Right)
                        d.element.FlowDirection = FlowDirection.RightToLeft;

                    if (d.movingType == DynamicElement.MovingType.xRotate)
                        d.rotateDegreeCurrent = d.startDegree;

                    newAircraft.dynamicElemets.Add(d);

                    if ((newAircraft.flightDirection == FlightDirectionType.Right) && d.movingType != DynamicElement.MovingType.yRotate)
                        Canvas.SetZIndex(d.element, (zIndex == zIndexType.inFront ? 60 : 20));
                    else
                        Canvas.SetZIndex(d.element, (zIndex == zIndexType.inFront ? 80 : 40));

                    main.firePlace.Children.Add(d.element);
                }

                newAircraft.aircraftType = aircraftType;
                newAircraft.aircraftName = aircraftName;
                newAircraft.hitpoint = hitPoint;
                newAircraft.hitpointMax = hitPoint;
                newAircraft.price = price;
                newAircraft.speed = speed;
                newAircraft.minAltitude = minAltitude;
                newAircraft.maxAltitude = maxAltitude;
                newAircraft.friend = friend;
                newAircraft.airliner = airliner;
                newAircraft.cloud = cloud;
                newAircraft.cantEscape = cantEscape;
                newAircraft.fly = true;

                if (newAircraft.minAltitude == -1) newAircraft.minAltitude = minAltitudeGlobal;

                if (!friend && !airliner)
                {
                    Shilka.statisticAllAircraft++;
                    Shilka.statisticPriceOfAllAircrafts += price;
                }

                if (zIndex != null)
                    Canvas.SetZIndex(newAircraftImage, (zIndex == zIndexType.inFront ? 70 : 30));
                else
                    Canvas.SetZIndex(newAircraftImage, (cloud ? 100 : 50));

                newAircraft.aircraftImage = newAircraftImage;
                main.firePlace.Children.Add(newAircraftImage);
                aircrafts.Add(newAircraft);
            }));
        }

        public static void AircraftStart(object obj, ElapsedEventArgs e)
        {
            int newAircraft = rand.Next(15)+1;

            int dice;

            switch (newAircraft)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    createNewAircraft(
                        aircraftType: "cloud" + (int)(rand.Next(7) + 1),
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

                    if (Scripts.scriptAircraft == null) goto case 1;

                    do
                    {
                        dice = (int)(rand.Next(31) + 1);
                    }
                    while (!aircraftInList(Scripts.scriptAircraft, dice));

                    switch (dice)
                    {
                        case 1:
                            createNewAircraft(
                                aircraftType: "a10",
                                aircraftName: "A-10 Thunderbolt",
                                hitPoint: 200,
                                aircraftWidth: 270,
                                aircraftHeight: 68,
                                price: 12,
                                speed: 5,
                                cantEscape: true
                            ); break;
                        case 2:
                            createNewAircraft(
                                aircraftType: "b1",
                                aircraftName: "B-1 Lancer",
                                hitPoint: 90,
                                aircraftWidth: 510,
                                aircraftHeight: 108,
                                price: 283,
                                speed: 12
                            ); break;
                        case 3:
                            createNewAircraft(
                                aircraftType: "b52",
                                aircraftName: "B-52 Stratofortress",
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
                                aircraftType: "f117",
                                aircraftName: "F-117 Nighthawk",
                                hitPoint: 50,
                                aircraftWidth: 270,
                                aircraftHeight: 47,
                                price: 111
                            ); break;
                        case 5:
                            createNewAircraft(
                                aircraftType: "f14",
                                aircraftName: "F-14 Tomcat",
                                hitPoint: 120,
                                aircraftWidth: 275,
                                aircraftHeight: 67,
                                price: 38
                            ); break;
                        case 6:
                            createNewAircraft(
                                aircraftType: "f18",
                                aircraftName: "F-18 Hornet",
                                hitPoint: 120,
                                aircraftWidth: 270,
                                aircraftHeight: 61,
                                price: 29
                            ); break;
                        case 7:
                            createNewAircraft(
                                aircraftType: "f16",
                                aircraftName: "F-16 Fighting Falcon",
                                hitPoint: 120,
                                aircraftWidth: 270,
                                aircraftHeight: 89,
                                price: 34
                            ); break;
                        case 8:
                            createNewAircraft(
                                aircraftType: "f22",
                                aircraftName: "F-22 Raptor",
                                hitPoint: 90,
                                aircraftWidth: 270,
                                aircraftHeight: 73,
                                price: 142,
                                speed: 14
                            ); break;
                        case 9:
                            createNewAircraft(
                                aircraftType: "f15",
                                aircraftName: "F-15 Eagle",
                                hitPoint: 120,
                                aircraftWidth: 270,
                                aircraftHeight: 62,
                                price: 29
                            ); break;
                        case 10:
                            createNewAircraft(
                                aircraftType: "f4",
                                aircraftName: "F-4 Fantom",
                                hitPoint: 150,
                                aircraftWidth: 270,
                                aircraftHeight: 64,
                                price: 3,
                                speed: 8
                            ); break;
                        case 11:
                            createNewAircraft(
                                aircraftType: "tornado",
                                aircraftName: "Panavia Tornado",
                                hitPoint: 100,
                                aircraftWidth: 270,
                                aircraftHeight: 72,
                                price: 111
                            ); break;
                        case 12:
                            createNewAircraft(
                                aircraftType: "predator",
                                aircraftName: "MQ-1 Predator",
                                hitPoint: 30,
                                aircraftWidth: 140,
                                aircraftHeight: 44,
                                price: 4,
                                speed: 5,
                                cantEscape: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "ltl_prop",
                                        y = -15,
                                        x_left = 130,
                                        x_right = -5,
                                        movingType = DynamicElement.MovingType.yRotate
                                    }
                                }
                            ); break;
                        case 13:
                            createNewAircraft(
                                aircraftType: "reaper",
                                aircraftName: "MQ-9 Reaper",
                                hitPoint: 50,
                                aircraftWidth: 161,
                                aircraftHeight: 52,
                                price: 16,
                                speed: 5,
                                cantEscape: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "ltl_prop",
                                        y = -3,
                                        x_left = 143,
                                        x_right = 5,
                                        movingType = DynamicElement.MovingType.yRotate
                                    }
                                }
                            ); break;
                        case 14:
                            createNewAircraft(
                                aircraftType: "f35",
                                aircraftName: "F-35 Lightning II",
                                hitPoint: 90,
                                aircraftWidth: 270,
                                aircraftHeight: 76,
                                price: 83
                            ); break;
                        case 15:
                            createNewAircraft(
                                aircraftType: "e3",
                                aircraftName: "E-3 Centry",
                                hitPoint: 150,
                                aircraftWidth: 581,
                                aircraftHeight: 164,
                                price: 270,
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft
                            ); break;
                        case 16:
                            createNewAircraft(
                                aircraftType: "eurofighter",
                                aircraftName: "Eurofighter Typhoon",
                                hitPoint: 100,
                                aircraftWidth: 270,
                                aircraftHeight: 77,
                                price: 123
                            ); break;
                        case 17:
                            createNewAircraft(
                                aircraftType: "rafale",
                                aircraftName: "Rafale",
                                hitPoint: 90,
                                aircraftWidth: 270,
                                aircraftHeight: 86,
                                price: 85,
                                speed: 11
                            ); break;
                        case 18:
                            createNewAircraft(
                                aircraftType: "b2",
                                aircraftName: "B-2 Spirit",
                                hitPoint: 125,
                                aircraftWidth: 332,
                                aircraftHeight: 76,
                                price: 2100,
                                speed: 18
                            ); break;
                        case 19:
                            createNewAircraft(
                                aircraftType: "globalhawk",
                                aircraftName: "RQ-4 Global Hawk",
                                hitPoint: 125,
                                aircraftWidth: 265,
                                aircraftHeight: 85,
                                price: 70,
                                speed: 7,
                                cantEscape: true
                            ); break;
                        case 20:
                            createNewAircraft(
                                aircraftType: "tomahawk",
                                aircraftName: "Tomahawk",
                                hitPoint: 20,
                                aircraftWidth: 125,
                                aircraftHeight: 29,
                                price: 2,
                                speed: 5,
                                cantEscape: true
                            ); break;
                        case 21:
                            createNewAircraft(
                                aircraftType: "f8",
                                aircraftName: "F-8 Crusader",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 81,
                                price: 6,
                                speed: 8
                            ); break;
                        case 22:
                            createNewAircraft(
                                aircraftType: "ac130",
                                aircraftName: "AC-130 Spectre",
                                hitPoint: 120,
                                aircraftWidth: 400,
                                aircraftHeight: 154,
                                price: 190,
                                speed: 7,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "air_prop",
                                        y = 50,
                                        x_left = 105,
                                        x_right = 275,
                                        movingType = DynamicElement.MovingType.yRotate
                                    }
                                }
                            ); break;
                        case 23:
                            createNewAircraft(
                                aircraftType: "a6",
                                aircraftName: "A-6 Intruder",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 78,
                                price: 43,
                                speed: 7
                            ); break;
                        case 24:
                            createNewAircraft(
                                aircraftType: "f111",
                                aircraftName: "F-111",
                                hitPoint: 80,
                                aircraftWidth: 285,
                                aircraftHeight: 59,
                                price: 72
                            ); break;
                        case 25:
                            createNewAircraft(
                                aircraftType: "f5",
                                aircraftName: "F-5 Tiger",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 58,
                                price: 2,
                                speed: 10
                            ); break;
                        case 26:
                            createNewAircraft(
                                aircraftType: "scalp",
                                aircraftName: "SCALP",
                                hitPoint: 20,
                                aircraftWidth: 115,
                                aircraftHeight: 23,
                                price: 2,
                                speed: 5,
                                cantEscape: true
                            ); break;
                        case 27:
                            createNewAircraft(
                                aircraftType: "ea6",
                                aircraftName: "EA-6 Prowler",
                                hitPoint: 80,
                                aircraftWidth: 285,
                                aircraftHeight: 66,
                                price: 52,
                                speed: 7
                            ); break;
                        case 28:
                            createNewAircraft(
                                aircraftType: "hawkeye",
                                aircraftName: "E-2 Hawkeye",
                                hitPoint: 100,
                                aircraftWidth: 324,
                                aircraftHeight: 96,
                                price: 80,
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "air_prop",
                                        y = 13,
                                        x_left = 75,
                                        x_right = 225,
                                        movingType = DynamicElement.MovingType.yRotate
                                    }
                                }
                            ); break;
                        case 29:
                            createNewAircraft(
                                aircraftType: "rc135",
                                aircraftName: "RC-135",
                                hitPoint: 120,
                                aircraftWidth: 528,
                                aircraftHeight: 185,
                                price: 90,
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true
                            ); break;
                        case 30:
                            createNewAircraft(
                                aircraftType: "u2",
                                aircraftName: "U-2",
                                hitPoint: 80,
                                aircraftWidth: 355,
                                aircraftHeight: 103,
                                price: 6,
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true
                            ); break;
                        case 31:
                            createNewAircraft(
                                aircraftType: "sr71",
                                aircraftName: "SR-71 Blackbird",
                                hitPoint: 80,
                                aircraftWidth: 450,
                                aircraftHeight: 71,
                                price: 34,
                                speed: 14,
                                minAltitude: minAltitudeForLargeAircraft
                            ); break;
                    }
                    break;

                case 10:
                case 11:
                case 12:

                    if (Scripts.scriptHelicopters == null) goto case 5;

                    do
                    {
                        dice = (int)(rand.Next(6) + 1);
                    }
                    while (!aircraftInList(Scripts.scriptHelicopters, dice));

                    switch (dice)
                    {

                        case 1:
                            createNewAircraft(
                                aircraftType: "ah64",
                                aircraftName: "AH-64 Apache",
                                hitPoint: 120,
                                aircraftWidth: 209,
                                aircraftHeight: 63,
                                speed: 5,
                                maxAltitude: maxAltitudeForHelicopters,
                                price: 61,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -8,
                                        x_left = -41,
                                        x_right = 27,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                    new DynamicElement {
                                        elementName = "x_suppl",
                                        y = -5,
                                        x_left = 170,
                                        x_right = -10,
                                        movingType = DynamicElement.MovingType.zRotate
                                    }
                                }
                            );  break;
                        case 2:
                            createNewAircraft(
                                aircraftType: "ah1",
                                aircraftName: "AH-1 Cobra",
                                hitPoint: 100,
                                aircraftWidth: 209,
                                aircraftHeight: 54,
                                speed: 5,
                                maxAltitude: maxAltitudeForHelicopters,
                                price: 11,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -22,
                                        x_left = -41,
                                        x_right = 27,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                    new DynamicElement {
                                        elementName = "i_suppl",
                                        y = -9,
                                        x_left = 175,
                                        x_right = -12,
                                        movingType = DynamicElement.MovingType.zRotate
                                    }
                                }
                            ); break;
                        case 3:
                            createNewAircraft(
                                aircraftType: "uh60",
                                aircraftName: "UH-60 Black Hawk",
                                hitPoint: 80,
                                aircraftWidth: 210,
                                aircraftHeight: 65,
                                speed: 5,
                                maxAltitude: maxAltitudeForHelicopters,
                                price: 21,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -5,
                                        x_left = -48,
                                        x_right = 36,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                    new DynamicElement {
                                        elementName = "t_suppl",
                                        y = -11,
                                        x_left = 175,
                                        x_right = -12,
                                        movingType = DynamicElement.MovingType.zRotate
                                    }
                                }
                            ); break;
                        case 4:
                            createNewAircraft(
                                aircraftType: "uh1",
                                aircraftName: "UH-1 Iroquois",
                                hitPoint: 80,
                                aircraftWidth: 210,
                                aircraftHeight: 65,
                                speed: 5,
                                maxAltitude: maxAltitudeForHelicopters,
                                price: 5,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -13,
                                        x_left = -39,
                                        x_right = 25,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                    new DynamicElement {
                                        elementName = "i_suppl",
                                        y = -11,
                                        x_left = 175,
                                        x_right = -12,
                                        movingType = DynamicElement.MovingType.zRotate
                                    }
                                }
                            ); break;
                        case 5:
                            createNewAircraft(
                                aircraftType: "ch47",
                                aircraftName: "CH-47 Chinook",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 101,
                                speed: 5,
                                maxAltitude: maxAltitudeForHelicopters,
                                price: 29,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = 6,
                                        x_left = -68,
                                        x_right = 110,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -20,
                                        x_left = 125,
                                        x_right = -78,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                }
                            ); break;
                        case 6:
                            createNewAircraft(
                                aircraftType: "v22",
                                aircraftName: "V-22 Ospray",
                                hitPoint: 80,
                                aircraftWidth: 282,
                                aircraftHeight: 103,
                                speed: 7,
                                maxAltitude: maxAltitudeForHelicopters,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -18,
                                        x_left = -5,
                                        x_right = 60,
                                        movingType = DynamicElement.MovingType.xRotate,
                                        startDegree = 0.5,
                                    },
                                }
                            ); break;
                    }
                    break;

                case 13:

                    if (Scripts.scriptAircraftFriend == null) goto case 1;

                    do
                    {
                        dice = (int)(rand.Next(14) + 1);
                    }
                    while (!aircraftInList(Scripts.scriptAircraftFriend, dice));

                    switch (dice)
                    {
                        case 1:
                            createNewAircraft(
                                aircraftType: "mig23",
                                aircraftName: "МиГ-23",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 71,
                                friend: true
                            ); break;
                        case 2:
                            createNewAircraft(
                                aircraftType: "mig29",
                                aircraftName: "МиГ-29",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 65,
                                friend: true
                            ); break;
                        case 3:
                            createNewAircraft(
                                aircraftType: "mig31",
                                aircraftName: "МиГ-31",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 63,
                                speed: 14,
                                friend: true
                            ); break;
                        case 4:
                            createNewAircraft(
                                aircraftType: "su17",
                                aircraftName: "Су-17",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 61,
                                speed: 5,
                                friend: true
                            ); break;
                        case 5:
                            createNewAircraft(
                                aircraftType: "su24",
                                aircraftName: "Су-24",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 67,
                                speed: 8,
                                friend: true
                            ); break;
                        case 6:
                            createNewAircraft(
                                aircraftType: "su25",
                                aircraftName: "Су-25",
                                hitPoint: 180,
                                aircraftWidth: 270,
                                aircraftHeight: 81,
                                speed: 5,
                                friend: true,
                                cantEscape: true
                            ); break;
                        case 7:
                            createNewAircraft(
                                aircraftType: "su27",
                                aircraftName: "Су-27",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 77,
                                friend: true
                            ); break;
                        case 8:
                            createNewAircraft(
                                aircraftType: "su34",
                                aircraftName: "Су-34",
                                hitPoint: 100,
                                aircraftWidth: 275,
                                aircraftHeight: 56,
                                friend: true
                            ); break;
                        case 9:
                            createNewAircraft(
                                aircraftType: "pakfa",
                                aircraftName: "Су-57",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 57,
                                speed: 12,
                                friend: true
                            ); break;
                        case 10:
                            createNewAircraft(
                                aircraftType: "tu160",
                                aircraftName: "Ту-160",
                                hitPoint: 120,
                                aircraftWidth: 510,
                                aircraftHeight: 108,
                                speed: 18,
                                minAltitude: minAltitudeForLargeAircraft,
                                friend: true
                            ); break;
                        case 11:
                            createNewAircraft(
                                aircraftType: "mig19",
                                aircraftName: "МиГ-19",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 81,
                                friend: true
                            ); break;
                        case 12:
                            createNewAircraft(
                                aircraftType: "mig21",
                                aircraftName: "МиГ-21",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 62,
                                friend: true
                            ); break;
                        case 13:
                            createNewAircraft(
                                aircraftType: "mig25",
                                aircraftName: "МиГ-25",
                                hitPoint: 80,
                                aircraftWidth: 270,
                                aircraftHeight: 64,
                                speed: 14,
                                friend: true
                            ); break;
                        case 14:
                            createNewAircraft(
                                aircraftType: "a50",
                                aircraftName: "А-50",
                                hitPoint: 150,
                                aircraftWidth: 570,
                                aircraftHeight: 175,
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                friend: true
                            ); break;
                    }
                    break;

                case 14:

                    if (Scripts.scriptHelicoptersFriend == null) goto case 13;

                    do
                    {
                        dice = (int)(rand.Next(5) + 1);
                    }
                    while (!aircraftInList(Scripts.scriptHelicoptersFriend, dice));

                    switch (dice)
                    {

                        case 1:
                            createNewAircraft(
                                aircraftType: "mi28",
                                aircraftName: "Ми-28",
                                hitPoint: 120,
                                aircraftWidth: 209,
                                aircraftHeight: 62,
                                speed: 5,
                                maxAltitude: maxAltitudeForHelicopters,
                                friend: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -7,
                                        x_left = -39,
                                        x_right = 25,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                    new DynamicElement {
                                        elementName = "x_suppl",
                                        y = -11,
                                        x_left = 165,
                                        x_right = -8,
                                        movingType = DynamicElement.MovingType.zRotate
                                    }
                                }
                            ); break;
                        case 2:
                            createNewAircraft(
                                aircraftType: "mi24",
                                aircraftName: "Ми-24",
                                hitPoint: 120,
                                aircraftWidth: 210,
                                aircraftHeight: 57,
                                speed: 5,
                                maxAltitude: maxAltitudeForHelicopters,
                                friend: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -11,
                                        x_left = -39,
                                        x_right = 25,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                    new DynamicElement {
                                        elementName = "y_suppl",
                                        y = -15,
                                        x_left = 180,
                                        x_right = -10,
                                        movingType = DynamicElement.MovingType.zRotate
                                    }
                                }
                            ); break;
                        case 3:
                            createNewAircraft(
                                aircraftType: "mi8",
                                aircraftName: "Ми-8",
                                hitPoint: 80,
                                aircraftWidth: 220,
                                aircraftHeight: 62,
                                speed: 5,
                                maxAltitude: maxAltitudeForHelicopters,
                                friend: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -11,
                                        x_left = -47,
                                        x_right = 40,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                    new DynamicElement {
                                        elementName = "y_suppl",
                                        y = -19,
                                        x_left = 190,
                                        x_right = -15,
                                        movingType = DynamicElement.MovingType.zRotate
                                    }
                                }
                            ); break;
                        case 4:
                            createNewAircraft(
                                aircraftType: "ka52",
                                aircraftName: "Ка-52",
                                hitPoint: 120,
                                aircraftWidth: 232,
                                aircraftHeight: 70,
                                speed: 5,
                                maxAltitude: maxAltitudeForHelicopters,
                                friend: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -13,
                                        x_left = -30,
                                        x_right = 30,
                                        movingType = DynamicElement.MovingType.xRotate,
                                        startDegree = 0.5,
                                    },
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -2,
                                        x_left = -30,
                                        x_right = 30,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                }
                            ); break;
                        case 5:
                            createNewAircraft(
                                aircraftType: "ka27",
                                aircraftName: "Ка-27",
                                hitPoint: 80,
                                aircraftWidth: 197,
                                aircraftHeight: 63,
                                speed: 5,
                                maxAltitude: maxAltitudeForHelicopters,
                                friend: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -32,
                                        x_left = -30,
                                        x_right = 0,
                                        movingType = DynamicElement.MovingType.xRotate,
                                        startDegree = 0.5,
                                    },
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -21,
                                        x_left = -30,
                                        x_right = 0,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                }
                            ); break;
                    }
                    break;

                case 15:

                    dice = (int)(rand.Next(2) + 1);

                    switch (dice)
                    {
                        case 1:
                            createNewAircraft(
                                aircraftType: "a320",
                                aircraftName: "Аэробус А320",
                                hitPoint: 100,
                                aircraftWidth: 565,
                                aircraftHeight: 173,
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                airliner: true
                            ); break;

                        case 2:
                            createNewAircraft(
                                aircraftType: "boeing747",
                                aircraftName: "Боинг 747",
                                hitPoint: 100,
                                aircraftWidth: 565,
                                aircraftHeight: 158,
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                airliner: true
                            ); break;
                    }
                    break;
            }
        }
    }
}
