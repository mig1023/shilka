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
        static int maxAltitudeGlobal = Constants.MAX_FLIGHT_HEIGHT;
        enum FlightDirectionType { Left, Right };
        enum zIndexType { inFront, Behind };

        double tangage { get; set; }
        int tangageDelay = 0;
        double angleOfAttack = 0;

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
        public bool doesNotFlyInBadWeather = false;

        FlightDirectionType flightDirection;

        private static bool schoolEnemyAlready = false;
        private static bool schoolFriendAlready = false;
        private static bool schoolAirlinerAlready = false;

        public static int allAircraftsInGame = 0;

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
                        escapeFromFireCoefficient = Constants.ESCAPE_COEFFICIENT;

                    if (aircraft.flightDirection == FlightDirectionType.Left)
                        escapeFromFireCoefficient *= -1;

                    aircraft.x += aircraft.speed * escapeFromFireCoefficient;

                    if (aircraft.dead)
                    {
                        aircraft.y += Constants.TANGAGE_DEAD_SPEED * (rand.NextDouble() * 2 - 1) + 4;

                        if (aircraft.dynamicElemets.Count == 0)
                        {
                            if (aircraft.angleOfAttack > 0)
                                aircraft.angleOfAttack += Constants.ANGLE_OF_ATTACK_CHANGE_SPEED;
                            else if (aircraft.angleOfAttack < 0)
                                aircraft.angleOfAttack -= Constants.ANGLE_OF_ATTACK_CHANGE_SPEED;
                            else
                                aircraft.angleOfAttack += (rand.NextDouble() - 0.5) * 0.2;

                            aircraft.aircraftImage.RenderTransform = new RotateTransform(
                                aircraft.angleOfAttack, (aircraft.aircraftImage.ActualWidth / 2), (aircraft.aircraftImage.ActualHeight / 2)
                            );
                        }
                        else
                            foreach (DynamicElement d in aircraft.dynamicElemets)
                                if ((d.movingType != DynamicElement.MovingType.xRotate) && (rand.Next(2) == 1))
                                    d.slowRotation = true;
                    }
                    else if (!aircraft.cloud)
                    {
                        aircraft.tangageDelay++;

                        if (aircraft.tangageDelay > Constants.TANGAGE_DELAY)
                        {
                            aircraft.tangageDelay = 0;
                            aircraft.tangage = Constants.TANGAGE_SPEED * (rand.NextDouble() * 2 - 1);
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
                        (((aircraft.x + aircraft.aircraftImage.Width) < 0) && (aircraft.flightDirection == FlightDirectionType.Left))
                        ||
                        ((aircraft.x > main.Width) && (aircraft.flightDirection == FlightDirectionType.Right))
                    ) {
                        aircraft.fly = false;

                        if ((!aircraft.dead) && (!aircraft.friend) && (!aircraft.airliner))
                        {
                            Statistic.statisticHasGone += 1;
                            Statistic.statisticLastHasGone = aircraft.aircraftName;

                            if (aircraft.hitpoint < aircraft.hitpointMax)
                            {
                                Statistic.statisticDamaged++;

                                double residualValue = aircraft.price * (double)aircraft.hitpoint / (double)aircraft.hitpointMax;
                                double priceOfDamage = aircraft.price - residualValue;
                                Statistic.statisticAmountOfDamage += priceOfDamage;

                                Statistic.statisticShutdownFlag = false;
                                Statistic.statisticLastDamagePrice = priceOfDamage;
                                Statistic.statisticLastDamageType = aircraft.aircraftName;
                                Statistic.seriousDamage = (aircraft.hitpoint < (aircraft.hitpointMax / 2) ? true : false);

                                Statistic.AircraftToStatistic(aircraft.aircraftName, Statistic.statisticAircraftType.damaged);
                            }
                        } 
                        else if (aircraft.hitpoint < aircraft.hitpointMax)
                            if (aircraft.friend)
                            {
                                Statistic.statisticFriendDamage += 1;
                                Statistic.statisticLastDamageFriend = aircraft.aircraftName;
                            }
                            else if (aircraft.airliner)
                            {
                                Statistic.statisticAirlinerDamage += 1;
                                Statistic.statisticLastDamageAirliner = aircraft.aircraftName;
                            }
                    }

                    aircraft.aircraftImage.Margin = new Thickness(aircraft.x, aircraft.y, 0, 0);

                    foreach (DynamicElement d in aircraft.dynamicElemets)
                    {
                        double xDirection = (aircraft.flightDirection == FlightDirectionType.Left ? d.x_left : d.x_right);
                        d.element.Margin = new Thickness(aircraft.x + xDirection, aircraft.y + d.y, 0, 0);

                        if (d.movingType == DynamicElement.MovingType.zRotate)
                        {
                            int direction = (aircraft.flightDirection == FlightDirectionType.Left ? 1 : -1);

                            d.rotateDegreeCurrent += (Constants.ROTATE_STEP * direction);

                            if (d.rotateDegreeCurrent < -180 || d.rotateDegreeCurrent > 180)
                                d.rotateDegreeCurrent = 0;
                                
                            d.element.RenderTransform = new RotateTransform(d.rotateDegreeCurrent, (d.element.ActualWidth / 2), (d.element.ActualHeight / 2));
                        }

                        if (d.movingType == DynamicElement.MovingType.xRotate || d.movingType == DynamicElement.MovingType.yRotate)
                        {
                            d.rotateDegreeCurrent -= (d.slowRotation ? Constants.SLOW_ROTATION : Constants.FAST_ROTATION);

                            if (d.rotateDegreeCurrent < Constants.FAST_ROTATION)
                            {
                                d.rotateDegreeCurrent = 1;

                                if (d.backSide && d.currentSide)
                                    d.element.Source = ImageFromResources(d.elementName);
                                else if (d.backSide)
                                    d.element.Source = ImageFromResources(d.elementName + "_back");

                                d.currentSide = !d.currentSide;
                            }
                               

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

        static void CreateNewAircraft(AircraftsType aircraft)
        {
            allAircraftsInGame += 1;

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                zIndexType? zIndex = null;

                Image newAircraftImage = new Image();

                newAircraftImage.Width = aircraft.size[0];
                newAircraftImage.Height = aircraft.size[1];

                Aircraft newAircraft = Aircraft.Clone(aircraft);

                newAircraft.y = rand.Next(maxAltitudeGlobal, Aircrafts.minAltitudeGlobal);

                bool flightDirectionRight = rand.Next(2) == 1;

                if ((Shilka.currentScript == Scripts.scriptsNames.Belgrad) && !aircraft.cloud)
                    if (aircraft.friend)
                        flightDirectionRight = true;
                    else
                        flightDirectionRight = false;

                if (flightDirectionRight)
                {
                    newAircraft.flightDirection = FlightDirectionType.Right;
                    newAircraft.x = -1 * newAircraftImage.Width;
                }
                else
                {
                    newAircraft.flightDirection = FlightDirectionType.Left;
                    newAircraft.x = Application.Current.MainWindow.Width;
                }

                newAircraftImage.Source = ImageFromResources(aircraft.aircraftType);

                bool flightLeftAndNotCloud = (newAircraft.flightDirection == FlightDirectionType.Left) && !aircraft.cloud;
                bool flightRightAndCloud = (rand.Next(2) == 1) && aircraft.cloud;

                if (flightLeftAndNotCloud || flightRightAndCloud)
                    newAircraftImage.FlowDirection = FlowDirection.RightToLeft;

                newAircraftImage.Margin = new Thickness(newAircraft.x, newAircraft.y, 0, 0);

                if ((aircraft.elements != null) && (aircraft.elements.Count > 0))
                {
                    zIndex = (rand.Next(2) > 0 ? zIndexType.inFront : zIndexType.Behind);

                    foreach (DynamicElement d in aircraft.elements)
                    {
                        DynamicElement tmp = DynamicElement.Clone(d);
                        tmp.element = new Image();

                        tmp.element.Margin = new Thickness(newAircraft.x, newAircraft.y, 0, 0);
                        tmp.element.Source = ImageFromResources(d.elementName);
                        tmp.rotateDegreeCurrent = d.startDegree;

                        if ((newAircraft.flightDirection == FlightDirectionType.Right) && !d.mirror)
                            tmp.element.FlowDirection = FlowDirection.RightToLeft;
                        else if ((newAircraft.flightDirection == FlightDirectionType.Left) && d.mirror)
                            tmp.element.FlowDirection = FlowDirection.RightToLeft;

                        newAircraft.dynamicElemets.Add(tmp);

                        bool flightRightAndYRotate = (
                            (newAircraft.flightDirection == FlightDirectionType.Right)
                            &&
                            (d.movingType != DynamicElement.MovingType.yRotate)
                        );

                        if (flightRightAndYRotate || d.background)
                            Canvas.SetZIndex(tmp.element, (zIndex == zIndexType.inFront ? 60 : 20));
                        else
                            Canvas.SetZIndex(tmp.element, (zIndex == zIndexType.inFront ? 80 : 40));

                        main.firePlace.Children.Add(tmp.element);
                    }
                }

                int randomSpeed = (aircraft.cloud ? 0 : rand.Next(3));

                newAircraft.speed = aircraft.speed + randomSpeed;

                if (newAircraft.minAltitude == -1)
                    newAircraft.minAltitude = Aircrafts.minAltitudeGlobal;

                if (!aircraft.friend && !aircraft.airliner)
                {
                    Statistic.statisticAllAircraft++;
                    Statistic.statisticPriceOfAllAircrafts += aircraft.price;
                }

                if (zIndex != null)
                    Canvas.SetZIndex(newAircraftImage, (zIndex == zIndexType.inFront ? 70 : 30));
                else
                    Canvas.SetZIndex(newAircraftImage, (aircraft.cloud ? 100 : 50));

                if (Shilka.school)
                {
                    Label aircraftLabelName = new Label();
                    aircraftLabelName.Content = newAircraft.aircraftName;
                    newAircraft.aircraftSchoolName = aircraftLabelName;

                    if (aircraft.airliner)
                        newAircraft.aircraftSchoolName.Foreground = Brushes.Blue;
                    else if (aircraft.friend)
                        newAircraft.aircraftSchoolName.Foreground = Brushes.Green;
                    else
                        newAircraft.aircraftSchoolName.Foreground = Brushes.Red;

                    Canvas.SetZIndex(aircraftLabelName, Canvas.GetZIndex(newAircraftImage));
                    main.firePlace.Children.Add(aircraftLabelName);
                }

                if (Shilka.school && !newAircraft.cloud)
                    aircraftMessagesForSchool(newAircraft, main);

                newAircraft.aircraftImage = newAircraftImage;
                main.firePlace.Children.Add(newAircraftImage);
                aircrafts.Add(newAircraft);
            }));
        }

        public static Aircraft Clone(AircraftsType aircraft)
        {
            Aircraft newAircraft = new Aircraft();

            newAircraft.aircraftType = aircraft.aircraftType;
            newAircraft.aircraftName = aircraft.aircraftName;
            newAircraft.hitpoint = aircraft.hitPoint;
            newAircraft.hitpointMax = aircraft.hitPoint;
            newAircraft.price = aircraft.price;
            newAircraft.minAltitude = aircraft.minAltitude;
            newAircraft.maxAltitude = aircraft.maxAltitude;
            newAircraft.friend = aircraft.friend;
            newAircraft.airliner = aircraft.airliner;
            newAircraft.cloud = aircraft.cloud;
            newAircraft.cantEscape = aircraft.cantEscape;
            newAircraft.fly = true;

            return newAircraft;
        }

        public static int FindAircraftByName(string aircraftName)
        {
            int aircraftIndex = 0;

            foreach(AircraftsType currentAircraft in Aircrafts.aircraft)
            {
                if (currentAircraft.aircraftName == aircraftName)
                    return aircraftIndex;
            }

            return -1;
        }

        public static void Shutdown(Aircraft aircraft, FirePlace main)
        {
            if (aircraft.friend)
                main.EndGame("Вы сбили свой " + aircraft.aircraftName +
                    "!\nИгра окончена.\nСохранить статистику?", Constants.END_COLOR);

            else if (aircraft.airliner)
                main.EndGame("Вы сбили пассажирский самолёт" +
                    "!\nИгра окончена.\nСохранить статистику?", Constants.END_COLOR);

            else
            {
                Statistic.staticticAircraftShutdown++;
                Statistic.statisticAmountOfDamage += aircraft.price;

                Statistic.statisticShutdownFlag = true;
                Statistic.statisticLastDamagePrice = aircraft.price;
                Statistic.statisticLastDamageType = aircraft.aircraftName;

                Statistic.AircraftToStatistic(aircraft.aircraftName, Statistic.statisticAircraftType.downed);
            }
        }

        private static int aircraftCategoryForSchool(int currentAircraftCategory, int allAircraftsInGame)
        {
            if (allAircraftsInGame < Constants.SCHOOL_CLOUD_AT_THE_START)
                return 1;
            else if (allAircraftsInGame < Constants.SCHOOL_ENEMY_AT_THE_START)
                return rand.Next(5, 13);
            else if (allAircraftsInGame < Constants.SCHOOL_FRIEND_AT_THE_START)
                return rand.Next(13, 15);
            else if (allAircraftsInGame < Constants.SCHOOL_AIRLINER_AT_THE_START)
                return 15;

            return currentAircraftCategory;
        }

        private static void aircraftMessagesForSchool(Aircraft aircraft, FirePlace main)
        {
            if (aircraft.airliner && !schoolAirlinerAlready)
            {
                schoolAirlinerAlready = true;
                main.SchoolMessage(Constants.AIRLINER_INFORMATION, Brushes.Blue);
            }
            else if (aircraft.friend && !schoolFriendAlready)
            {
                schoolFriendAlready = true;
                main.SchoolMessage(Constants.FRIEND_INFORMATION, Brushes.Green);
            }
            else if (!schoolEnemyAlready)
            {
                schoolEnemyAlready = true;
                main.SchoolMessage(Constants.ENEMY_INFORMATION, Brushes.Red);
            }
        }

        public static void Start(object obj, ElapsedEventArgs e)
        {
            int aircraftCategory = rand.Next(1, 16);

            if (Shilka.school)
                aircraftCategory = aircraftCategoryForSchool(aircraftCategory, allAircraftsInGame);

            int dice;

            AircraftsType newAircraft;

            switch (aircraftCategory)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                default:

                    newAircraft = new AircraftsType
                    {
                        aircraftType = "cloud" + (rand.Next(1, 8)),
                        size = new int[] { rand.Next(200, 501), rand.Next(70, 171) },
                        speed = Constants.CLOUD_SPEED,
                        friend = true,
                        cloud = true
                    };
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
                        dice = rand.Next(Aircrafts.aircraft.Count);
                    }
                    while (!AircraftInList(Scripts.scriptAircraft, dice));

                    if ((Shilka.currentScript == Scripts.scriptsNames.F117Hunt) && (dice != FindAircraftByName("f117")))
                        goto case 1;

                    if (Weather.currentWeather != Weather.weatherTypes.good && Aircrafts.aircraft[dice].doesNotFlyInBadWeather)
                        goto case 9;

                    if (Shilka.currentScript == Scripts.scriptsNames.Khmeimim)
                        goto case 10;

                    newAircraft = Aircrafts.aircraft[dice];

                    break;

                case 10:
                case 11:
                case 12:

                    if (Scripts.scriptHelicopters == null)
                        goto case 5;

                    do
                    {
                        dice = rand.Next(Aircrafts.helicopters.Count);
                    }
                    while (!AircraftInList(Scripts.scriptHelicopters, dice));

                    newAircraft = Aircrafts.helicopters[dice];
                    
                    break;

                case 13:

                    if (Scripts.scriptAircraftFriend == null)
                        goto case 1;

                    do
                    {
                        dice = rand.Next(Aircrafts.aircraftFriend.Count);
                    }
                    while (!AircraftInList(Scripts.scriptAircraftFriend, dice));

                    newAircraft = Aircrafts.aircraftFriend[dice];
                    
                    break;

                case 14:

                    if (Scripts.scriptHelicoptersFriend == null)
                        goto case 13;

                    do
                    {
                        dice = rand.Next(Aircrafts.helicoptersFriend.Count);
                    }
                    while (!AircraftInList(Scripts.scriptHelicoptersFriend, dice));

                    newAircraft = Aircrafts.helicoptersFriend[dice];
                    
                    break;

                case 15:

                    if (Scripts.scriptAirliners == null)
                        goto case 1;

                    do
                    {
                        dice = rand.Next(Aircrafts.airliners.Count);
                    }
                    while (!AircraftInList(Scripts.scriptAirliners, dice));

                    newAircraft = Aircrafts.airliners[dice];
                    
                    break;
            }

            CreateNewAircraft(newAircraft);
        }
    }
}
