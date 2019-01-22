using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Timers;
using System.Windows.Media;

namespace shilka2
{
    class Aircraft : FlyObject
    {
        const int MAX_FLIGHT_HEIGHT = 75;
        public const int AIRCRAFT_AVERAGE_PRICE = 61;
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
        public double price;
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

        public Label aircraftSchoolName;

        public List<DynamicElement> dynamicElemets = new List<DynamicElement>(); 

        public static List<Aircraft> aircrafts = new List<Aircraft>();

        public static void AircraftFly(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

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

                            if (aircraft.y > aircraft.minAltitude)
                            aircraft.y = aircraft.minAltitude;
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
                            Statistic.statisticHasGone++;

                            if (aircraft.hitpoint < aircraft.hitpointMax)
                            {
                                Statistic.statisticDamaged++;

                                double residualValue =  aircraft.price * (double)aircraft.hitpoint / (double)aircraft.hitpointMax;
                                double priceOfDamage = aircraft.price - residualValue;
                                Statistic.statisticAmountOfDamage += priceOfDamage;

                                Statistic.statisticShutdownFlag = false;
                                Statistic.statisticLastDamagePrice = priceOfDamage;
                                Statistic.statisticLastDamageType = aircraft.aircraftName;
                            }
                        } 
                        else if (aircraft.hitpoint < aircraft.hitpointMax)
                            if (aircraft.friend)
                                Statistic.statisticFriendDamage++;
                            else if (aircraft.airliner)
                                Statistic.statisticAirlinerDamage++;
                    }

                    aircraft.aircraftImage.Margin = new Thickness(aircraft.x, aircraft.y, 0, 0);

                    foreach (DynamicElement d in aircraft.dynamicElemets)
                    {
                        double xDirection = (aircraft.flightDirection == FlightDirectionType.Left ? d.x_left : d.x_right);
                        d.element.Margin = new Thickness(aircraft.x + xDirection, aircraft.y + d.y, 0, 0);

                        if (d.movingType == DynamicElement.MovingType.zRotate)
                        {
                            int direction = (aircraft.flightDirection == FlightDirectionType.Left ? 1 : -1);

                            d.rotateDegreeCurrent += (25 * direction);

                            if (d.rotateDegreeCurrent < -180 || d.rotateDegreeCurrent > 180)
                                d.rotateDegreeCurrent = 0;
                                
                            d.element.RenderTransform = new RotateTransform(d.rotateDegreeCurrent, (d.element.ActualWidth / 2), (d.element.ActualHeight / 2));
                        }

                        if (d.movingType == DynamicElement.MovingType.xRotate || d.movingType == DynamicElement.MovingType.yRotate)
                        {
                            d.rotateDegreeCurrent -= 0.2;

                            if (d.rotateDegreeCurrent < 0.2)
                                d.rotateDegreeCurrent = 1;

                            if (d.movingType == DynamicElement.MovingType.xRotate)
                                d.element.RenderTransform = new ScaleTransform(d.rotateDegreeCurrent, 1, (d.element.ActualWidth / 2), 0);
                            else
                                d.element.RenderTransform = new ScaleTransform(1, d.rotateDegreeCurrent, 0, (d.element.ActualHeight / 2));
                        }
                    }

                    if (Shilka.school)
                    {
                        int hitpoint = (aircraft.hitpoint >= 0 ? aircraft.hitpoint/2 : 0);

                        if (!aircraft.cloud)
                            aircraft.aircraftSchoolName.Content = aircraft.aircraftName + " " + new string('|', hitpoint);

                        aircraft.aircraftSchoolName.Margin = new Thickness(aircraft.x, aircraft.y + aircraft.aircraftImage.Height, 0, 0);
                    }
                }

                for (int x = 0; x < aircrafts.Count; x++)
                    if (aircrafts[x].fly == false)
                    {
                        main.firePlace.Children.Remove(aircrafts[x].aircraftImage);

                        foreach (DynamicElement d in aircrafts[x].dynamicElemets)
                            main.firePlace.Children.Remove(d.element);

                        if (Shilka.school)
                            main.firePlace.Children.Remove(aircrafts[x].aircraftSchoolName);

                        aircrafts.RemoveAt(x);
                    }
            }));
        }

        private static bool AircraftInList(int?[] scriptAircraft, int aircraft)
        {
            if (scriptAircraft == null)
                return false;

            if (scriptAircraft.Length == 0)
                return true;

            bool inList = false;

            foreach (int aircraftInList in scriptAircraft)
                if (aircraftInList == aircraft)
                    inList = true;

            return inList;
        }

        public static ImageSource ImageFromResources(string imageName)
        {
            return new BitmapImage(new Uri("images/" + imageName + ".png", UriKind.Relative)) { };
        }

        static void CreateNewAircraft(string aircraftType, int hitPoint, int[] size,
            string aircraftName = "", int speed = 10, int minAltitude = -1, int maxAltitude = -1, bool friend = false,
            bool airliner = false, bool cloud = false, bool cantEscape = false, double price = 0)
        {
            List<DynamicElement> elements = new List<DynamicElement>();

            CreateNewAircraft(aircraftType, hitPoint, size, elements, aircraftName,
                speed, minAltitude, maxAltitude, friend, airliner, cloud, cantEscape, price);
        }

        static void CreateNewAircraft(string aircraftType, int hitPoint, int[] size,
            List<DynamicElement> elements, string aircraftName = "", int speed = 10, int minAltitude = -1,
            int maxAltitude = -1, bool friend = false, bool airliner = false, bool cloud = false,
            bool cantEscape = false, double price = 0)
        {

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                zIndexType? zIndex = null;

                Image newAircraftImage = new Image();

                newAircraftImage.Width = size[0];
                newAircraftImage.Height = size[1];

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

                newAircraftImage.Source = ImageFromResources(aircraftType);

                if (((newAircraft.flightDirection == FlightDirectionType.Left) && !cloud) || (rand.Next(2) == 1) && cloud)
                    newAircraftImage.FlowDirection = FlowDirection.RightToLeft;

                newAircraftImage.Margin = new Thickness(newAircraft.x, newAircraft.y, 0, 0);

                if (elements.Count > 0)
                    zIndex = (rand.Next(2) > 0 ? zIndexType.inFront : zIndexType.Behind);

                foreach (DynamicElement d in elements)
                {
                    d.element = new Image();
                    d.element.Source = ImageFromResources(d.elementName);
                    d.rotateDegreeCurrent = d.startDegree;

                    if ((newAircraft.flightDirection == FlightDirectionType.Right) && !d.mirror)
                        d.element.FlowDirection = FlowDirection.RightToLeft;
                    else if ((newAircraft.flightDirection == FlightDirectionType.Left) && d.mirror)
                        d.element.FlowDirection = FlowDirection.RightToLeft;

                    newAircraft.dynamicElemets.Add(d);

                    if (
                            ((newAircraft.flightDirection == FlightDirectionType.Right) && d.movingType != DynamicElement.MovingType.yRotate)
                            ||
                            d.background
                    )
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
                newAircraft.minAltitude = minAltitude;
                newAircraft.maxAltitude = maxAltitude;
                newAircraft.friend = friend;
                newAircraft.airliner = airliner;
                newAircraft.cloud = cloud;
                newAircraft.cantEscape = cantEscape;
                newAircraft.fly = true;

                int randomSpeed = (cloud ? 0 : rand.Next(3));

                newAircraft.speed = speed + randomSpeed;

                if (newAircraft.minAltitude == -1)
                    newAircraft.minAltitude = minAltitudeGlobal;

                if (!friend && !airliner)
                {
                    Statistic.statisticAllAircraft++;
                    Statistic.statisticPriceOfAllAircrafts += price;
                }

                if (zIndex != null)
                    Canvas.SetZIndex(newAircraftImage, (zIndex == zIndexType.inFront ? 70 : 30));
                else
                    Canvas.SetZIndex(newAircraftImage, (cloud ? 100 : 50));

                if (Shilka.school)
                {
                    Label aircraftLabelName = new Label();
                    aircraftLabelName.Content = newAircraft.aircraftName;
                    newAircraft.aircraftSchoolName = aircraftLabelName;
                    newAircraft.aircraftSchoolName.Foreground = ((friend || airliner) ? Brushes.Green : Brushes.Red);

                    Canvas.SetZIndex(aircraftLabelName, Canvas.GetZIndex(newAircraftImage));
                    main.firePlace.Children.Add(aircraftLabelName);
                } 

                newAircraft.aircraftImage = newAircraftImage;
                main.firePlace.Children.Add(newAircraftImage);
                aircrafts.Add(newAircraft);
            }));
        }

        public static void AircraftStart(object obj, ElapsedEventArgs e)
        {
            int newAircraft = rand.Next(15) + 1;

            int dice;

            switch (newAircraft)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    CreateNewAircraft(
                        aircraftType: "cloud" + (rand.Next(7) + 1),
                        hitPoint: 10,
                        size: new int[] { rand.Next(300) + 200, rand.Next(100) + 70 },
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

                    if (Scripts.scriptAircraft == null)
                        goto case 1;

                    do
                    {
                        dice = rand.Next(38) + 1;
                    }
                    while (!AircraftInList(Scripts.scriptAircraft, dice));

                    if ((Shilka.currentScript == Scripts.scriptsNames.F117Hunt) && (dice != 4))
                        goto case 1;

                    if (Shilka.currentScript == Scripts.scriptsNames.Khmeimim)
                        goto case 10;

                    switch (dice)
                    {
                        case 1:
                            CreateNewAircraft(
                                aircraftType: "a10",
                                aircraftName: "A-10 Thunderbolt",
                                hitPoint: 200,
                                size: new int[] { 270, 68 },
                                price: 12,
                                speed: 5,
                                cantEscape: true
                            );
                            break;

                        case 2:
                            CreateNewAircraft(
                                aircraftType: "b1",
                                aircraftName: "B-1 Lancer",
                                hitPoint: 90,
                                size: new int[] { 510, 108 },
                                price: 283,
                                speed: 12
                            );
                            break;

                        case 3:
                            CreateNewAircraft(
                                aircraftType: "b52",
                                aircraftName: "B-52 Stratofortress",
                                hitPoint: 120,
                                size: new int[] { 565, 155 },
                                price: 53,
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true
                            );
                            break;

                        case 4:
                            CreateNewAircraft(
                                aircraftType: "f117",
                                aircraftName: "F-117 Nighthawk",
                                hitPoint: 50,
                                size: new int[] { 270, 47 },
                                price: 112
                            );
                            break;

                        case 5:
                            CreateNewAircraft(
                                aircraftType: "f14",
                                aircraftName: "F-14 Tomcat",
                                hitPoint: 120,
                                size: new int[] { 275, 67 },
                                price: 38
                            );
                            break;

                        case 6:
                            CreateNewAircraft(
                                aircraftType: "f18",
                                aircraftName: "F-18 Hornet",
                                hitPoint: 120,
                                size: new int[] { 270, 61 },
                                price: 57
                            );
                            break;

                        case 7:
                            CreateNewAircraft(
                                aircraftType: "f16",
                                aircraftName: "F-16 Fighting Falcon",
                                hitPoint: 120,
                                size: new int[] { 270, 89 },
                                price: 34
                            );
                            break;

                        case 8:
                            CreateNewAircraft(
                                aircraftType: "f22",
                                aircraftName: "F-22 Raptor",
                                hitPoint: 90,
                                size: new int[] { 270, 73 },
                                price: 146,
                                speed: 14
                            );
                            break;

                        case 9:
                            CreateNewAircraft(
                                aircraftType: "f15",
                                aircraftName: "F-15 Eagle",
                                hitPoint: 120,
                                size: new int[] { 270, 62 },
                                price: 29
                            );
                            break;

                        case 10:
                            CreateNewAircraft(
                                aircraftType: "f4",
                                aircraftName: "F-4 Fantom",
                                hitPoint: 150,
                                size: new int[] { 270, 64 },
                                price: 3,
                                speed: 8
                            );
                            break;

                        case 11:
                            CreateNewAircraft(
                                aircraftType: "tornado",
                                aircraftName: "Panavia Tornado",
                                hitPoint: 100,
                                size: new int[] { 270, 72 },
                                price: 111
                            );
                            break;

                        case 12:
                            CreateNewAircraft(
                                aircraftType: "predator",
                                aircraftName: "MQ-1 Predator",
                                hitPoint: 30,
                                size: new int[] { 140, 44 },
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
                            );
                            break;

                        case 13:
                            CreateNewAircraft(
                                aircraftType: "reaper",
                                aircraftName: "MQ-9 Reaper",
                                hitPoint: 50,
                                size: new int[] { 161, 52 },
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
                            );
                            break;

                        case 14:
                            CreateNewAircraft(
                                aircraftType: "f35",
                                aircraftName: "F-35 Lightning II",
                                hitPoint: 90,
                                size: new int[] { 270, 76 },
                                price: 108
                            );
                            break;

                        case 15:
                            CreateNewAircraft(
                                aircraftType: "e3",
                                aircraftName: "E-3 Centry",
                                hitPoint: 150,
                                size: new int[] { 581, 164 },
                                price: 270,
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft
                            );
                            break;

                        case 16:
                            CreateNewAircraft(
                                aircraftType: "eurofighter",
                                aircraftName: "Eurofighter Typhoon",
                                hitPoint: 100,
                                size: new int[] { 270, 77 },
                                price: 123
                            );
                            break;

                        case 17:
                            CreateNewAircraft(
                                aircraftType: "rafale",
                                aircraftName: "Rafale",
                                hitPoint: 90,
                                size: new int[] { 270, 86 },
                                price: 85,
                                speed: 11
                            );
                            break;

                        case 18:
                            CreateNewAircraft(
                                aircraftType: "b2",
                                aircraftName: "B-2 Spirit",
                                hitPoint: 125,
                                size: new int[] { 332, 76 },
                                price: 2100,
                                speed: 18
                            );
                            break;

                        case 19:
                            CreateNewAircraft(
                                aircraftType: "globalhawk",
                                aircraftName: "RQ-4 Global Hawk",
                                hitPoint: 125,
                                size: new int[] { 265, 85 },
                                price: 70,
                                speed: 7,
                                cantEscape: true
                            );
                            break;

                        case 20:
                            CreateNewAircraft(
                                aircraftType: "tomahawk",
                                aircraftName: "Tomahawk",
                                hitPoint: 20,
                                size: new int[] { 125, 29 },
                                price: 2,
                                speed: 5,
                                cantEscape: true
                            );
                            break;

                        case 21:
                            CreateNewAircraft(
                                aircraftType: "f8",
                                aircraftName: "F-8 Crusader",
                                hitPoint: 80,
                                size: new int[] { 270, 93 },
                                price: 6,
                                speed: 8
                            );
                            break;

                        case 22:
                            CreateNewAircraft(
                                aircraftType: "ac130",
                                aircraftName: "AC-130 Spectre",
                                hitPoint: 120,
                                size: new int[] { 400, 154 },
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
                            );
                            break;

                        case 23:
                            CreateNewAircraft(
                                aircraftType: "a6",
                                aircraftName: "A-6 Intruder",
                                hitPoint: 80,
                                size: new int[] { 270, 78 },
                                price: 43,
                                speed: 7
                            );
                            break;

                        case 24:
                            CreateNewAircraft(
                                aircraftType: "f111",
                                aircraftName: "F-111",
                                hitPoint: 80,
                                size: new int[] { 285, 59 },
                                price: 72
                            );
                            break;

                        case 25:
                            CreateNewAircraft(
                                aircraftType: "f5",
                                aircraftName: "F-5 Tiger",
                                hitPoint: 80,
                                size: new int[] { 270, 58 },
                                price: 2,
                                speed: 10
                            );
                            break;

                        case 26:
                            CreateNewAircraft(
                                aircraftType: "scalp",
                                aircraftName: "SCALP",
                                hitPoint: 20,
                                size: new int[] { 115, 23 },
                                price: 2,
                                speed: 5,
                                cantEscape: true
                            );
                            break;

                        case 27:
                            CreateNewAircraft(
                                aircraftType: "ea6",
                                aircraftName: "EA-6 Prowler",
                                hitPoint: 80,
                                size: new int[] { 285, 66 },
                                price: 52,
                                speed: 7
                            );
                            break;

                        case 28:
                            CreateNewAircraft(
                                aircraftType: "hawkeye",
                                aircraftName: "E-2 Hawkeye",
                                hitPoint: 100,
                                size: new int[] { 324, 96 },
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
                            );
                            break;

                        case 29:
                            CreateNewAircraft(
                                aircraftType: "rc135",
                                aircraftName: "RC-135",
                                hitPoint: 120,
                                size: new int[] { 528, 185 },
                                price: 90,
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true
                            );
                            break;

                        case 30:
                            CreateNewAircraft(
                                aircraftType: "u2",
                                aircraftName: "U-2",
                                hitPoint: 80,
                                size: new int[] { 355, 103 },
                                price: 6,
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true
                            );
                            break;

                        case 31:
                            CreateNewAircraft(
                                aircraftType: "sr71",
                                aircraftName: "SR-71 Blackbird",
                                hitPoint: 80,
                                size: new int[] { 450, 71 },
                                price: 34,
                                speed: 18,
                                minAltitude: minAltitudeForLargeAircraft
                            );
                            break;

                        case 32:
                            CreateNewAircraft(
                                aircraftType: "harrier",
                                aircraftName: "BAE Sea Harrier",
                                hitPoint: 80,
                                size: new int[] { 275, 81 },
                                price: 24,
                                speed: 7
                            );
                            break;

                        case 33:
                            CreateNewAircraft(
                                aircraftType: "cessna",
                                aircraftName: "Cessna 172",
                                hitPoint: 50,
                                size: new int[] { 170, 61 },
                                speed: 6,
                                price: 0.3,
                                cantEscape: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "ltl_prop",
                                        y = 2,
                                        x_left = -6,
                                        x_right = 164,
                                        movingType = DynamicElement.MovingType.yRotate,
                                        mirror = true
                                    }
                                }
                            );
                            break;

                        case 34:
                            CreateNewAircraft(
                                aircraftType: "hunter",
                                aircraftName: "RQ-5 Hunter",
                                hitPoint: 30,
                                size: new int[] { 172, 49 },
                                speed: 5,
                                price: 2,
                                cantEscape: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "ltl_prop",
                                        y = -10,
                                        x_left = 105,
                                        x_right = 53,
                                        movingType = DynamicElement.MovingType.yRotate,
                                        background = true
                                    }
                                }
                            );
                            break;

                        case 35:
                            CreateNewAircraft(
                                aircraftType: "r99",
                                aircraftName: "Embraer R-99",
                                hitPoint: 100,
                                size: new int[] { 350, 88 },
                                price: 80,
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true
                            );
                            break;


                        case 36:
                            CreateNewAircraft(
                                aircraftType: "m2000",
                                aircraftName: "Mirage 2000",
                                hitPoint: 120,
                                size: new int[] { 270, 79 },
                                price: 25
                            );
                            break;

                        case 37:
                            CreateNewAircraft(
                                aircraftType: "m2000ed",
                                aircraftName: "Mirage 2000ED",
                                hitPoint: 120,
                                size: new int[] { 270, 75 },
                                price: 35
                            );
                            break;

                        case 38:
                            CreateNewAircraft(
                                aircraftType: "jassm",
                                aircraftName: "JASSM",
                                hitPoint: 20,
                                size: new int[] { 108, 25 },
                                price: 0.85,
                                speed: 5,
                                cantEscape: true
                            );
                            break;
                    }
                    break;

                case 10:
                case 11:
                case 12:

                    if (Scripts.scriptHelicopters == null)
                        goto case 5;

                    do
                    {
                        dice = rand.Next(17) + 1;
                    }
                    while (!AircraftInList(Scripts.scriptHelicopters, dice));

                    switch (dice)
                    {

                        case 1:
                            CreateNewAircraft(
                                aircraftType: "ah64",
                                aircraftName: "AH-64 Apache",
                                hitPoint: 120,
                                size: new int[] { 209, 63 },
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
                            );
                            break;

                        case 2:
                            CreateNewAircraft(
                                aircraftType: "ah1",
                                aircraftName: "AH-1 Cobra",
                                hitPoint: 100,
                                size: new int[] { 209, 54 },
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
                            );
                            break;

                        case 3:
                            CreateNewAircraft(
                                aircraftType: "uh60",
                                aircraftName: "UH-60 Black Hawk",
                                hitPoint: 80,
                                size: new int[] { 210, 65 },
                                speed: 5,
                                maxAltitude: maxAltitudeForHelicopters,
                                price: 25,
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
                            );
                            break;

                        case 4:
                            CreateNewAircraft(
                                aircraftType: "uh1",
                                aircraftName: "UH-1 Iroquois",
                                hitPoint: 80,
                                size: new int[] { 210, 65 },
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
                            );
                            break;

                        case 5:
                            CreateNewAircraft(
                                aircraftType: "ch47",
                                aircraftName: "CH-47 Chinook",
                                hitPoint: 80,
                                size: new int[] { 270, 101 },
                                speed: 5,
                                maxAltitude: maxAltitudeForHelicopters,
                                price: 30,
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
                            );
                            break;

                        case 6:
                            CreateNewAircraft(
                                aircraftType: "v22",
                                aircraftName: "V-22 Ospray",
                                hitPoint: 80,
                                size: new int[] { 282, 103 },
                                speed: 7,
                                maxAltitude: maxAltitudeForHelicopters,
                                price: 116,
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
                            );
                            break;

                        case 7:
                            CreateNewAircraft(
                               aircraftType: "tiger",
                               aircraftName: "Eurocopter Tiger",
                               hitPoint: 80,
                               size: new int[] { 209, 76 },
                               speed: 5,
                               maxAltitude: maxAltitudeForHelicopters,
                               price: 39,
                               elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -3,
                                        x_left = -35,
                                        x_right = 20,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                    new DynamicElement {
                                        elementName = "y_suppl",
                                        y = 5,
                                        x_left = 170,
                                        x_right = -7,
                                        movingType = DynamicElement.MovingType.zRotate
                                    }
                               }
                           );
                            break;

                        case 8:
                            CreateNewAircraft(
                                aircraftType: "drone",
                                aircraftName: "дрон-разведчик",
                                hitPoint: 1,
                                size: new int[] { 26, 9 },
                                speed: 3,
                                maxAltitude: maxAltitudeForHelicopters,
                                price: 0.01,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "micro_prop",
                                        y = -5,
                                        x_left = -6,
                                        x_right = -6,
                                        movingType = DynamicElement.MovingType.xRotate,
                                        startDegree = 0.5,
                                    },
                                    new DynamicElement {
                                        elementName = "micro_prop",
                                        y = -5,
                                        x_left = 15,
                                        x_right = 15,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                }
                            );
                            break;

                        case 9:
                            CreateNewAircraft(
                                 aircraftType: "gazelle",
                                 aircraftName: "Aerospatiale Gazelle",
                                 hitPoint: 60,
                                 size: new int[] { 185, 64 },
                                 speed: 5,
                                 maxAltitude: maxAltitudeForHelicopters,
                                 price: 0.5,
                                 elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -3,
                                        x_left = -53,
                                        x_right = 12,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                    new DynamicElement {
                                        elementName = "f_suppl",
                                        y = 19,
                                        x_left = 155,
                                        x_right = 7,
                                        movingType = DynamicElement.MovingType.zRotate
                                    }
                                 }
                             );
                            break;

                        case 10:
                            CreateNewAircraft(
                                aircraftType: "comanche",
                                aircraftName: "RAH-66 Comanche",
                                hitPoint: 80,
                                size: new int[] { 210, 61 },
                                speed: 6,
                                maxAltitude: maxAltitudeForHelicopters,
                                price: 100,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -10,
                                        x_left = -29,
                                        x_right = 12,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                    new DynamicElement {
                                        elementName = "f_suppl",
                                        y = 26,
                                        x_left = 175,
                                        x_right = 12,
                                        movingType = DynamicElement.MovingType.zRotate
                                    }
                                }
                            );
                            break;

                        case 11:
                            CreateNewAircraft(
                                aircraftType: "oh1",
                                aircraftName: "OH-1 Ninja",
                                hitPoint: 100,
                                size: new int[] { 205, 69 },
                                speed: 5,
                                maxAltitude: maxAltitudeForHelicopters,
                                price: 24,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -10,
                                        x_left = -29,
                                        x_right = 12,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                    new DynamicElement {
                                        elementName = "f_suppl",
                                        y = 34,
                                        x_left = 172,
                                        x_right = 10,
                                        movingType = DynamicElement.MovingType.zRotate
                                    }
                                }
                            );
                            break;

                        case 12:
                            CreateNewAircraft(
                                aircraftType: "mangusta",
                                aircraftName: "T-129 Mangusta",
                                hitPoint: 100,
                                size: new int[] { 215, 66 },
                                speed: 5,
                                maxAltitude: maxAltitudeForHelicopters,
                                price: 52,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -14,
                                        x_left = -35,
                                        x_right = 22,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                    new DynamicElement {
                                        elementName = "i_suppl",
                                        y = -14,
                                        x_left = 179,
                                        x_right = -4,
                                        movingType = DynamicElement.MovingType.zRotate
                                    }
                                }
                            );
                            break;

                        case 13:
                            CreateNewAircraft(
                                aircraftType: "puma",
                                aircraftName: "Aerospatiale Puma",
                                hitPoint: 80,
                                size: new int[] { 215, 58 },
                                price: 15,
                                speed: 5,
                                minAltitude: maxAltitudeForHelicopters,
                                cantEscape: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -19,
                                        x_left = -52,
                                        x_right = 40,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                    new DynamicElement {
                                        elementName = "t_suppl",
                                        y = -16,
                                        x_left = 177,
                                        x_right = -11,
                                        movingType = DynamicElement.MovingType.zRotate
                                    }
                                }
                            );
                            break;

                        case 14:
                            CreateNewAircraft(
                                aircraftType: "mh53",
                                aircraftName: "Sikorsky MH-53",
                                hitPoint: 100,
                                size: new int[] { 375, 84 },
                                price: 53,
                                speed: 5,
                                minAltitude: maxAltitudeForHelicopters,
                                cantEscape: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main_big",
                                        y = 5,
                                        x_left = 40,
                                        x_right = 69,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                    new DynamicElement {
                                        elementName = "t_suppl",
                                        y = -16,
                                        x_left = 327,
                                        x_right = -11,
                                        movingType = DynamicElement.MovingType.zRotate
                                    }
                                }
                            );
                            break;

                        case 15:
                            CreateNewAircraft(
                                aircraftType: "as565",
                                aircraftName: "Eurocopter AS565",
                                hitPoint: 100,
                                size: new int[] { 199, 70 },
                                speed: 5,
                                maxAltitude: maxAltitudeForHelicopters,
                                price: 10,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = -4,
                                        x_left = -33,
                                        x_right = 3,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                    new DynamicElement {
                                        elementName = "f_suppl",
                                        y = 34,
                                        x_left = 167,
                                        x_right = 10,
                                        movingType = DynamicElement.MovingType.zRotate,
                                        background = true
                                    }
                                }
                            );
                            break;

                        case 16:
                            CreateNewAircraft(
                                aircraftType: "drone2",
                                aircraftName: "дрон-разведчик",
                                hitPoint: 1,
                                size: new int[] { 30, 17 },
                                speed: 3,
                                maxAltitude: maxAltitudeForHelicopters,
                                price: 0.01,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "micro_prop",
                                        y = -2,
                                        x_left = -6,
                                        x_right = -6,
                                        movingType = DynamicElement.MovingType.xRotate,
                                        startDegree = 0.5,
                                    },
                                    new DynamicElement {
                                        elementName = "micro_prop",
                                        y = -2,
                                        x_left = 19,
                                        x_right = 19,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                }
                            );
                            break;

                        case 17:
                            CreateNewAircraft(
                                aircraftType: "oh58d",
                                aircraftName: "Bell OH-58D",
                                hitPoint: 100,
                                size: new int[] { 209, 83 },
                                speed: 5,
                                maxAltitude: maxAltitudeForHelicopters,
                                price: 11,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main",
                                        y = 5,
                                        x_left = -43,
                                        x_right = 29,
                                        movingType = DynamicElement.MovingType.xRotate,
                                        background = true
                                    },
                                    new DynamicElement {
                                        elementName = "ltl_suppl",
                                        y = 24,
                                        x_left = 165,
                                        x_right = 14,
                                        movingType = DynamicElement.MovingType.zRotate
                                    }
                                }
                            );
                            break;
                    }
                    break;

                case 13:

                    if (Scripts.scriptAircraftFriend == null)
                        goto case 1;

                    do
                    {
                        dice = rand.Next(15) + 1;
                    }
                    while (!AircraftInList(Scripts.scriptAircraftFriend, dice));

                    switch (dice)
                    {
                        case 1:
                            CreateNewAircraft(
                                aircraftType: "mig23",
                                aircraftName: "МиГ-23",
                                hitPoint: 80,
                                size: new int[] { 270, 71 },
                                friend: true
                            );
                            break;

                        case 2:
                            CreateNewAircraft(
                                aircraftType: "mig29",
                                aircraftName: "МиГ-29",
                                hitPoint: 80,
                                size: new int[] { 270, 65 },
                                friend: true
                            );
                            break;

                        case 3:
                            CreateNewAircraft(
                                aircraftType: "mig31",
                                aircraftName: "МиГ-31",
                                hitPoint: 80,
                                size: new int[] { 270, 63 },
                                speed: 14,
                                friend: true
                            );
                            break;

                        case 4:
                            CreateNewAircraft(
                                aircraftType: "su17",
                                aircraftName: "Су-17",
                                hitPoint: 80,
                                size: new int[] { 270, 61 },
                                speed: 5,
                                friend: true
                            );
                            break;

                        case 5:
                            CreateNewAircraft(
                                aircraftType: "su24",
                                aircraftName: "Су-24",
                                hitPoint: 80,
                                size: new int[] { 270, 67 },
                                speed: 8,
                                friend: true
                            );
                            break;

                        case 6:
                            CreateNewAircraft(
                                aircraftType: "su25",
                                aircraftName: "Су-25",
                                hitPoint: 180,
                                size: new int[] { 270, 81 },
                                speed: 5,
                                friend: true,
                                cantEscape: true
                            );
                            break;

                        case 7:
                            CreateNewAircraft(
                                aircraftType: "su27",
                                aircraftName: "Су-27",
                                hitPoint: 80,
                                size: new int[] { 270, 77 },
                                friend: true
                            );
                            break;

                        case 8:
                            CreateNewAircraft(
                                aircraftType: "su34",
                                aircraftName: "Су-34",
                                hitPoint: 100,
                                size: new int[] { 275, 56 },
                                friend: true
                            );
                            break;

                        case 9:
                            CreateNewAircraft(
                                aircraftType: "pakfa",
                                aircraftName: "Су-57",
                                hitPoint: 80,
                                size: new int[] { 270, 57 },
                                speed: 12,
                                friend: true
                            );
                            break;

                        case 10:
                            CreateNewAircraft(
                                aircraftType: "tu160",
                                aircraftName: "Ту-160",
                                hitPoint: 120,
                                size: new int[] { 510, 108 },
                                speed: 18,
                                minAltitude: minAltitudeForLargeAircraft,
                                friend: true
                            );
                            break;

                        case 11:
                            CreateNewAircraft(
                                aircraftType: "mig19",
                                aircraftName: "МиГ-19",
                                hitPoint: 80,
                                size: new int[] { 270, 81 },
                                friend: true
                            );
                            break;

                        case 12:
                            CreateNewAircraft(
                                aircraftType: "mig21",
                                aircraftName: "МиГ-21",
                                hitPoint: 80,
                                size: new int[] { 270, 62 },
                                friend: true
                            );
                            break;

                        case 13:
                            CreateNewAircraft(
                                aircraftType: "mig25",
                                aircraftName: "МиГ-25",
                                hitPoint: 80,
                                size: new int[] { 270, 64 },
                                speed: 14,
                                friend: true
                            );
                            break;

                        case 14:
                            CreateNewAircraft(
                                aircraftType: "a50",
                                aircraftName: "А-50",
                                hitPoint: 150,
                                size: new int[] { 570, 175 },
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                friend: true
                            );
                            break;

                        case 15:
                            CreateNewAircraft(
                                aircraftType: "tu95",
                                aircraftName: "Ту-95",
                                hitPoint: 120,
                                size: new int[] { 510, 116 },
                                speed: 5,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                friend: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "ltl_prop",
                                        y = 68,
                                        x_left = 118,
                                        x_right = 340,
                                        movingType = DynamicElement.MovingType.yRotate,
                                        mirror = true
                                    },
                                    new DynamicElement {
                                        elementName = "ltl_prop",
                                        y = 68,
                                        x_left = 111,
                                        x_right = 347,
                                        startDegree = 0.5,
                                        movingType = DynamicElement.MovingType.yRotate,
                                        mirror = true
                                    },
                                    new DynamicElement {
                                        elementName = "ltl_prop",
                                        y = 68,
                                        x_left = 151,
                                        x_right = 380,
                                        movingType = DynamicElement.MovingType.yRotate,
                                        mirror = true
                                    },
                                    new DynamicElement {
                                        elementName = "ltl_prop",
                                        y = 68,
                                        x_left = 158,
                                        x_right = 387,
                                        startDegree = 0.5,
                                        movingType = DynamicElement.MovingType.yRotate,
                                        mirror = true
                                    },
                                }
                            );
                            break;
                    }
                    break;

                case 14:

                    if (Scripts.scriptHelicoptersFriend == null)
                        goto case 13;

                    do
                    {
                        dice = rand.Next(6) + 1;
                    }
                    while (!AircraftInList(Scripts.scriptHelicoptersFriend, dice));

                    switch (dice)
                    {

                        case 1:
                            CreateNewAircraft(
                                aircraftType: "mi28",
                                aircraftName: "Ми-28",
                                hitPoint: 120,
                                size: new int[] { 209, 62 },
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
                            );
                            break;

                        case 2:
                            CreateNewAircraft(
                                aircraftType: "mi24",
                                aircraftName: "Ми-24",
                                hitPoint: 120,
                                size: new int[] { 210, 57 },
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
                            );
                            break;

                        case 3:
                            CreateNewAircraft(
                                aircraftType: "mi8",
                                aircraftName: "Ми-8",
                                hitPoint: 80,
                                size: new int[] { 220, 62 },
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
                            );
                            break;

                        case 4:
                            CreateNewAircraft(
                                aircraftType: "ka52",
                                aircraftName: "Ка-52",
                                hitPoint: 120,
                                size: new int[] { 232, 70 },
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
                            );
                            break;

                        case 5:
                            CreateNewAircraft(
                                aircraftType: "ka27",
                                aircraftName: "Ка-27",
                                hitPoint: 80,
                                size: new int[] { 197, 63 },
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
                            );
                            break;

                        case 6:
                            CreateNewAircraft(
                                aircraftType: "mi10",
                                aircraftName: "Ми-10",
                                hitPoint: 80,
                                size: new int[] { 300, 77 },
                                speed: 5,
                                maxAltitude: maxAltitudeForHelicopters,
                                friend: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "prop_main_big",
                                        y = -20,
                                        x_left = -35,
                                        x_right = 70,
                                        movingType = DynamicElement.MovingType.xRotate,
                                    },
                                    new DynamicElement {
                                        elementName = "t_suppl",
                                        y = -15,
                                        x_left = 260,
                                        x_right = -15,
                                        movingType = DynamicElement.MovingType.zRotate
                                    }
                                }
                            );
                            break;
                    }
                    break;

                case 15:

                    if (Scripts.scriptAirliners == null)
                        goto case 1;

                    do
                    {
                        dice = rand.Next(12) + 1;
                    }
                    while (!AircraftInList(Scripts.scriptAirliners, dice));

                    switch (dice)
                    {
                        case 1:
                            CreateNewAircraft(
                                aircraftType: "a320",
                                aircraftName: "Аэробус А320",
                                hitPoint: 100,
                                size: new int[] { 565, 173 },
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                airliner: true
                            );
                            break;

                        case 2:
                            CreateNewAircraft(
                                aircraftType: "boeing747",
                                aircraftName: "Боинг 747",
                                hitPoint: 100,
                                size: new int[] { 565, 158 },
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                airliner: true
                            );
                            break;

                        case 3:
                            CreateNewAircraft(
                                aircraftType: "md11",
                                aircraftName: "MD-11",
                                hitPoint: 100,
                                size: new int[] { 560, 153 },
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                airliner: true
                            );
                            break;

                        case 4:
                            CreateNewAircraft(
                                aircraftType: "atr42",
                                aircraftName: "ATR 42",
                                hitPoint: 80,
                                size: new int[] { 320, 110 },
                                speed: 5,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                airliner: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "ltl_prop",
                                        y = 34,
                                        x_left = 95,
                                        x_right = 212,
                                        movingType = DynamicElement.MovingType.yRotate,
                                        mirror = true
                                    }
                                }
                            );
                            break;

                        case 5:
                            CreateNewAircraft(
                                aircraftType: "dhc8",
                                aircraftName: "Bombardier DHC-8",
                                hitPoint: 80,
                                size: new int[] { 370, 90 },
                                speed: 5,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                airliner: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "ltl_prop",
                                        y = 27,
                                        x_left = 122,
                                        x_right = 234,
                                        movingType = DynamicElement.MovingType.yRotate,
                                        mirror = true
                                    }
                                }
                            );
                            break;

                        case 6:
                            CreateNewAircraft(
                                aircraftType: "ssj100",
                                aircraftName: "Sukhoi Superjet 100",
                                hitPoint: 80,
                                size: new int[] { 355, 124 },
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                airliner: true
                            );
                            break;

                        case 7:
                            CreateNewAircraft(
                                aircraftType: "boeing707",
                                aircraftName: "Боинг 707",
                                hitPoint: 80,
                                size: new int[] { 470, 116 },
                                speed: 9,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                airliner: true
                            );
                            break;

                        case 8:
                            CreateNewAircraft(
                                aircraftType: "l1049",
                                aircraftName: "Локхид L-1049",
                                hitPoint: 60,
                                size: new int[] { 414, 119 },
                                speed: 6,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                airliner: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "ltl_prop",
                                        y = 43,
                                        x_left = 126,
                                        x_right = 275,
                                        movingType = DynamicElement.MovingType.yRotate,
                                        mirror = true
                                    },
                                    new DynamicElement {
                                        elementName = "ltl_prop",
                                        y = 48,
                                        x_left = 119,
                                        x_right = 282,
                                        movingType = DynamicElement.MovingType.yRotate,
                                        mirror = true,
                                        startDegree = 0.5,
                                    }
                                }
                            );
                            break;

                        case 9:
                            CreateNewAircraft(
                                aircraftType: "mc21",
                                aircraftName: "Иркут МС-21",
                                hitPoint: 80,
                                size: new int[] { 560, 154 },
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                airliner: true
                            );
                            break;

                        case 10:
                            CreateNewAircraft(
                                aircraftType: "a380",
                                aircraftName: "Аэробус А380",
                                hitPoint: 120,
                                size: new int[] { 621, 191 },
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                airliner: true
                            );
                            break;

                        case 11:
                            CreateNewAircraft(
                                aircraftType: "boeing777",
                                aircraftName: "Боинг 777",
                                hitPoint: 100,
                                size: new int[] { 585, 164 },
                                speed: 8,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                airliner: true
                            );
                            break;

                        case 12:
                            CreateNewAircraft(
                                aircraftType: "il114",
                                aircraftName: "Ил-114",
                                hitPoint: 80,
                                size: new int[] { 420, 135 },
                                speed: 5,
                                minAltitude: minAltitudeForLargeAircraft,
                                cantEscape: true,
                                airliner: true,
                                elements: new List<DynamicElement> {
                                    new DynamicElement {
                                        elementName = "ltl_prop",
                                        y = 78,
                                        x_left = 131,
                                        x_right = 275,
                                        movingType = DynamicElement.MovingType.yRotate,
                                        mirror = true
                                    }
                                }
                            );
                            break;
                    }
                    break;
            }
        }
    }
}
