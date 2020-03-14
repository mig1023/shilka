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
    public class Aircraft : FlyObject
    {
        static int maxAltitudeGlobal = Constants.MAX_FLIGHT_HEIGHT;
        public enum FlightDirectionType { Left, Right };
        enum zIndexType { inFront, Behind };
        public enum WeightType { Light, Middle, Heavy };
        public enum ImageType { Aircraft, DeadSprite, DynamicElement, Interface, Other };

        double tangage { get; set; }
        int tangageDelay = 0;
        double angleOfAttack = 0;

        public int placeOfDamage = 0;
        public double fallAcceleration = 0;
        public bool canPlaneForALongTime = false;
        public bool fallLikeAStone = false;

        public string aircraftType;
        public string aircraftName;
        public int hitpoint;
        public int hitpointMax;

        public int[] size;
        public List<DynamicElement> elements;

        public int hitPoint = 80;
        public int tragetTugHitPoint = 50;
        public double speed = 10;
        public int minAltitude = -1;
        public int maxAltitude = -1;
        public double price = 0;

        public bool dead = false;
        public bool friend = false;
        public bool airliner = false;
        public bool cloud = false;
        public bool trainingTug = false;
        public bool cantEscape = false;
        public bool deadSprite = false;
        public bool doesNotFlyInBadWeather = false;
        public bool aerostat = false;

        public int wrecksMaxSize = 10;
        public int wrecksNumber = 1;

        public WeightType weight = WeightType.Heavy;

        public FlightDirectionType flightDirection;

        private static bool schoolEnemyAlready = false;
        private static bool schoolFriendAlready = false;
        private static bool schoolAirlinerAlready = false;
        private static bool schoolMixAlready = false;

        private static bool trainingTurgetTug = false;
        private static bool trainingTurgetPlane = false;
        private static bool trainingTurgetDrone = false;
        private static bool trainingSuspendedTarget = false;

        private bool suspendedTarget = false;

        private static double suspendedTargetAngle = 0;
        public static bool suspendedTargetDowned = false;
        private static int suspendedTargetTimerCount = 0;

        public static int allAircraftsInGame = 0;

        public Image aircraftImage;

        public Label aircraftSchoolName;
        public Label aircraftSchoolPrice;

        public List<DynamicElement> dynamicElemets = new List<DynamicElement>(); 

        public static List<Aircraft> aircrafts = new List<Aircraft>();

        public static void SchoolStartCleaning()
        {
            schoolEnemyAlready = false;
            schoolFriendAlready = false;
            schoolAirlinerAlready = false;
            schoolMixAlready = false;
        }

        public static void TrainingStartCleaning()
        {
            trainingTurgetTug = false;
            trainingTurgetPlane = false;
            trainingTurgetDrone = false;
            trainingSuspendedTarget = false;
            suspendedTargetDowned = false;

            suspendedTargetTimerCount = 0;
            suspendedTargetAngle = 0;
        }

        public static void AircraftFly(object obj, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                foreach (Aircraft aircraft in aircrafts)
                {
                    if (!aircraft.suspendedTarget)
                    {
                        if ((aircraft.cloud || aircraft.aerostat) && (Weather.currentWeather == Weather.weatherTypes.storm))
                        {
                            if ((aircraft.flightDirection != Weather.stormDirection) && (aircraft.speed > 0))
                                aircraft.speed -= rand.Next(4) * 0.1;
                            else if ((aircraft.flightDirection != Weather.stormDirection) && (aircraft.speed <= 0))
                                aircraft.flightDirection = Weather.stormDirection;
                            else if ((aircraft.flightDirection == Weather.stormDirection) && (aircraft.speed < Constants.CLOUD_SPEED))
                                aircraft.speed += rand.Next(4) * 0.1;
                        }

                        double escape = (((aircraft.hitpoint < aircraft.hitpointMax) && !aircraft.cantEscape) ? Constants.ESCAPE_COEFFICIENT : 1);
                        double direction = (aircraft.flightDirection == FlightDirectionType.Left ? -1 : 1);
                        double tailWind = 1;

                        if (Weather.currentWeather == Weather.weatherTypes.storm)
                            tailWind = (aircraft.flightDirection == Weather.stormDirection ? 1.3 : 0.7);

                        aircraft.x += aircraft.speed * escape * tailWind * direction;
                    }

                    if (aircraft.dead)
                    {
                        aircraft.y += aircraft.AircraftDeadFallSpeed();

                        if ((aircraft.dynamicElemets.Count == 0 || aircraft.deadSprite) && !aircraft.aerostat)
                        {
                            double angle = aircraft.AircraftFlyAngle();

                            aircraft.angleOfAttack += (angle * aircraft.placeOfDamage);

                            aircraft.aircraftImage.RenderTransform = new RotateTransform(
                                aircraft.angleOfAttack, (aircraft.aircraftImage.ActualWidth / 2), (aircraft.aircraftImage.ActualHeight / 2)
                            );
                        }
                    }
                    else if (aircraft.suspendedTarget)
                    {
                        double angleDirection = 0;

                        if (suspendedTargetAngle > Constants.TRAINING_CRANE_ANGLE_MAX)
                            angleDirection = -1;
                        else if (suspendedTargetAngle < (Constants.TRAINING_CRANE_ANGLE_MAX * -1))
                            angleDirection = 1;
                        else
                            angleDirection = rand.NextDouble() * 2 - 1;

                        aircraft.angleOfAttack += Constants.TRAINING_CRANE_ANGLE_CHANGE_SPEED * angleDirection;

                        aircraft.aircraftImage.RenderTransform = new RotateTransform(
                            aircraft.angleOfAttack, (aircraft.aircraftImage.ActualWidth / 2), (aircraft.aircraftImage.ActualHeight / 2)
                        );
                    }
                    else if (!aircraft.aerostat && !aircraft.cloud && !aircraft.suspendedTarget)
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
                            int direction = (d.positiveDirection ? 1 : -1);

                            d.rotateDegreeCurrent += ((d.slowRotation ? Constants.SLOW_ROTATION : Constants.FAST_ROTATION) * direction);

                            if (d.rotateDegreeCurrent < Constants.ROTATION_REVERT)
                            {
                                if (d.backSide && d.currentSide)
                                    d.element.Source = ImageFromResources(d.elementName, ImageType.DynamicElement);
                                else if (d.backSide)
                                    d.element.Source = ImageFromResources(d.elementName + "_back", ImageType.DynamicElement);

                                d.currentSide = !d.currentSide;
                                d.positiveDirection = true;
                            }
                            else if (d.rotateDegreeCurrent >= 1)
                            {
                                d.positiveDirection = false;

                                if (d.oneWay)
                                {
                                    d.element.Source = ImageFromResources(d.elementName, ImageType.DynamicElement);
                                    d.currentSide = !d.currentSide;
                                }
                            }

                            if (d.movingType == DynamicElement.MovingType.xRotate)
                                d.element.RenderTransform = new ScaleTransform(d.rotateDegreeCurrent, 1, (d.element.ActualWidth / 2), 0);
                            else
                                d.element.RenderTransform = new ScaleTransform(1, d.rotateDegreeCurrent, 0, (d.element.ActualHeight / 2));
                        }
                    }

                    if (Shilka.school && !aircraft.cloud)
                    {
                        aircraft.aircraftSchoolName.Content = aircraft.GetAircraftFullName();
                        aircraft.aircraftSchoolName.Margin = new Thickness(aircraft.x, aircraft.y + aircraft.aircraftImage.Height, 0, 0);
                    }

                    if (Shilka.school && !aircraft.cloud && !aircraft.friend && !aircraft.airliner)
                    {
                        aircraft.aircraftSchoolPrice.Content = aircraft.GetAircraftPriceLine();
                        aircraft.aircraftSchoolPrice.Margin = new Thickness(aircraft.x, aircraft.y + aircraft.aircraftImage.Height + 16, 0, 0);
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

        private double AircraftFlyAngle()
        {
            switch (weight)
            {
                case WeightType.Light:
                    return Constants.ANGLE_OF_ATTACK_CHANGE_LIGHT;
                case WeightType.Middle:
                    return Constants.ANGLE_OF_ATTACK_CHANGE_MIDDLE;
                default:
                    return Constants.ANGLE_OF_ATTACK_CHANGE_HEAVY;
            }
        }

        private double AircraftDeadFallSpeed()
        {
            if (canPlaneForALongTime)
                return Constants.TANGAGE_DEAD_SPEED / 2;

            if (fallLikeAStone)
                return Constants.TANGAGE_DEAD_SPEED;

            fallAcceleration += Constants.FREE_FALL_SPEED_FOR_AIRCRAFT;

            double fallSpeed = (weight == WeightType.Light ? speed : 0);

            return Constants.TANGAGE_DEAD_SPEED * (rand.NextDouble() * 2 - 1) + fallSpeed + fallAcceleration;
        }

        private static bool AircraftInList(int?[] scriptAircraft, int aircraft)
        {
            if (scriptAircraft == null)
                return true;

            if (scriptAircraft.Length == 0)
                return false;

            bool inList = false;

            foreach (int aircraftInList in scriptAircraft)
                if (aircraftInList == aircraft)
                    inList = true;

            return inList;
        }

        public static ImageSource ImageFromResources(string imageName, ImageType type = ImageType.Aircraft)
        {
            BitmapImage image = null;

            string typeFolder = type.ToString();

            try
            {
                image = new BitmapImage(new Uri(String.Format("pack://application:,,,/images/{0}/{1}.png", typeFolder, imageName))) { };
            }
            catch
            {
                return null;
            }

            if (Shilka.night)
                return Invert(image);

            return image;
        }

        public static BitmapSource Invert(BitmapSource originalSource)
        {
            int stride = (originalSource.PixelWidth * originalSource.Format.BitsPerPixel + 7) / 8;

            int length = stride * originalSource.PixelHeight;
            byte[] data = new byte[length];

            originalSource.CopyPixels(data, stride, 0);

            int a = 0;

            for (int i = 0; i < length; i += 1)
            {
                if (a > 2)
                {
                    a = 0;
                    continue;
                }
                else
                    a += 1;

                data[i] = (byte)(255 - data[i]);
            }

            List<Color> colors = new List<Color>();
            colors.Add(Colors.Black);
            BitmapPalette palette = new BitmapPalette(colors);

            return BitmapSource.Create(originalSource.PixelWidth, originalSource.PixelHeight,
                originalSource.DpiX, originalSource.DpiY, originalSource.Format, palette, data, stride);
        }

        public void CreateNewAircraft(double? startX = null, double? startY = null, FlightDirectionType? startDirection = null,
            bool dead = false, bool transformation = false, bool suspended = false)
        {
            if (!transformation && (!Shilka.training || !cloud))
                allAircraftsInGame += 1;

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                zIndexType? zIndex = null;

                Image newAircraftImage = new Image();

                newAircraftImage.Width = size[0];
                newAircraftImage.Height = size[1];

                Aircraft newAircraft = Clone();

                newAircraft.dead = dead;

                newAircraft.suspendedTarget = suspended;

                newAircraft.y = startY ?? rand.Next(maxAltitudeGlobal, Aircrafts.minAltitudeGlobal);

                if ((newAircraft.minAltitude != -1) && (newAircraft.y > newAircraft.minAltitude))
                    newAircraft.y = newAircraft.minAltitude;

                FlightDirectionType newDirection = (rand.Next(2) == 1 ? FlightDirectionType.Right : FlightDirectionType.Left);

                if ((Weather.currentWeather == Weather.weatherTypes.storm) && cloud)
                    newDirection = Weather.stormDirection;

                if ((Shilka.currentScript == Scripts.scriptsNames.Belgrad) && !cloud)
                    if (friend)
                        newDirection = FlightDirectionType.Right;
                    else
                        newDirection = FlightDirectionType.Left;

                newAircraft.flightDirection = newDirection;

                if (newDirection == FlightDirectionType.Right)
                    newAircraft.x = -1 * newAircraftImage.Width;
                else
                    newAircraft.x = Application.Current.MainWindow.Width;

                newAircraft.x = startX ?? newAircraft.x;
                newAircraft.flightDirection = startDirection ?? newAircraft.flightDirection;

                newAircraftImage.Source = ImageFromResources(
                    imageName: aircraftType,
                    type: (newAircraft.cloud ? Aircraft.ImageType.Other : Aircraft.ImageType.Aircraft)
                );

                bool flightLeftAndNotCloud = (newAircraft.flightDirection == FlightDirectionType.Left) && !cloud;
                bool flightRightAndCloud = (rand.Next(2) == 1) && cloud;

                if (flightLeftAndNotCloud || flightRightAndCloud)
                    newAircraftImage.FlowDirection = FlowDirection.RightToLeft;

                newAircraftImage.Margin = new Thickness(newAircraft.x, newAircraft.y, 0, 0);

                if ((elements != null) && (elements.Count > 0))
                {
                    zIndex = (rand.Next(2) > 0 ? zIndexType.inFront : zIndexType.Behind);

                    foreach (DynamicElement d in elements)
                    {
                        DynamicElement tmp = DynamicElement.Clone(d);
                        tmp.element = new Image();

                        tmp.element.Margin = new Thickness(newAircraft.x, newAircraft.y, 0, 0);
                        tmp.element.Source = ImageFromResources(d.elementName, ImageType.DynamicElement);
                        tmp.rotateDegreeCurrent = d.startDegree;

                        if ((newAircraft.flightDirection == FlightDirectionType.Right) && !d.mirror)
                            tmp.element.FlowDirection = FlowDirection.RightToLeft;
                        else if ((newAircraft.flightDirection == FlightDirectionType.Left) && d.mirror)
                            tmp.element.FlowDirection = FlowDirection.RightToLeft;

                        newAircraft.dynamicElemets.Add(tmp);

                        bool flightRightAndZRotate = (
                            (newAircraft.flightDirection == FlightDirectionType.Right)
                            &&
                            (d.movingType == DynamicElement.MovingType.zRotate)
                        );

                        if (d.background || flightRightAndZRotate)
                            Canvas.SetZIndex(tmp.element, (zIndex == zIndexType.inFront ? 65 : 25));
                        else if (newAircraft.flightDirection == FlightDirectionType.Right)
                            Canvas.SetZIndex(tmp.element, (zIndex == zIndexType.inFront ? 85 : 45));
                        else
                            Canvas.SetZIndex(tmp.element, (zIndex == zIndexType.inFront ? 75 : 35));

                        main.firePlace.Children.Add(tmp.element);
                    }
                }

                int randomSpeed = (cloud ? 0 : rand.Next(3));

                newAircraft.speed = speed + randomSpeed;

                if (newAircraft.minAltitude == -1)
                    newAircraft.minAltitude = Aircrafts.minAltitudeGlobal;

                if (!friend && !airliner)
                {
                    Statistic.statisticAllAircraft++;
                    Statistic.statisticPriceOfAllAircrafts += price;
                }

                if (suspended)
                    Canvas.SetZIndex(newAircraftImage, 101);
                else if (zIndex != null)
                {
                    if (newAircraft.flightDirection == FlightDirectionType.Right)
                        Canvas.SetZIndex(newAircraftImage, (zIndex == zIndexType.inFront ? 80 : 40));
                    else
                        Canvas.SetZIndex(newAircraftImage, (zIndex == zIndexType.inFront ? 70 : 30));
                }
                else
                    Canvas.SetZIndex(newAircraftImage, (cloud ? 100 : 50));

                if (Shilka.school || !cloud)
                {
                    Label aircraftLabelName = new Label();
                    aircraftLabelName.Content = newAircraft.GetAircraftFullName();
                    newAircraft.aircraftSchoolName = aircraftLabelName;

                    if (airliner)
                        newAircraft.aircraftSchoolName.Foreground = Brushes.Blue;
                    else if (friend)
                        newAircraft.aircraftSchoolName.Foreground = Brushes.Green;
                    else
                        newAircraft.aircraftSchoolName.Foreground = Brushes.Red;

                    Canvas.SetZIndex(aircraftLabelName, Canvas.GetZIndex(newAircraftImage));
                    main.firePlace.Children.Add(aircraftLabelName);

                    if (!airliner && !friend)
                    {
                        Label aircraftLabelPrice = new Label();
                        aircraftLabelPrice.Content = GetAircraftPriceLine();
                        newAircraft.aircraftSchoolPrice = aircraftLabelPrice;
                        newAircraft.aircraftSchoolPrice.Foreground = Brushes.Gray;
                        Canvas.SetZIndex(aircraftLabelPrice, Canvas.GetZIndex(newAircraftImage));
                        main.firePlace.Children.Add(aircraftLabelPrice);
                    }
                }

                if (Shilka.school && !newAircraft.cloud)
                    newAircraft.AircraftMessagesForSchool(main);

                if (Shilka.training)
                    newAircraft.AircraftMessageForTraining(main);

                newAircraft.aircraftImage = newAircraftImage;
                main.firePlace.Children.Add(newAircraftImage);
                aircrafts.Add(newAircraft);
            }));
        }

        private string GetAircraftFullName()
        {
            return String.Format("{0} {1}", aircraftName, (hitpoint > 0 ? new string('|', hitpoint) : String.Empty));
        }

        private string GetAircraftPriceLine()
        {
            int tmpPrice = (int)(price / 10);
            string tmpPriceLine = new string('|', (tmpPrice > 0 ? tmpPrice : 1));

            return (price > 0 ? String.Format("Price: {0}m$ {1}", price, tmpPriceLine) : String.Empty);
        }

        public Aircraft Clone()
        {
            Aircraft newAircraft = new Aircraft();

            newAircraft.aircraftType = aircraftType;
            newAircraft.aircraftName = aircraftName;
            newAircraft.hitpoint = hitPoint;
            newAircraft.tragetTugHitPoint = tragetTugHitPoint;
            newAircraft.hitpointMax = hitPoint;
            newAircraft.price = price;
            newAircraft.minAltitude = minAltitude;
            newAircraft.maxAltitude = maxAltitude;
            newAircraft.friend = friend;
            newAircraft.airliner = airliner;
            newAircraft.cloud = cloud;
            newAircraft.trainingTug = trainingTug;
            newAircraft.cantEscape = cantEscape;
            newAircraft.deadSprite = deadSprite;
            newAircraft.weight = weight;

            newAircraft.fly = true;
            newAircraft.placeOfDamage = 0;
            newAircraft.canPlaneForALongTime = canPlaneForALongTime;
            newAircraft.fallLikeAStone = fallLikeAStone;
            newAircraft.doesNotFlyInBadWeather = doesNotFlyInBadWeather;
            newAircraft.aerostat = aerostat;

            newAircraft.wrecksMaxSize = wrecksMaxSize;
            newAircraft.wrecksNumber = wrecksNumber;

            return newAircraft;
        }

        public void Shutdown(FirePlace main)
        {
            if (friend || airliner)
            {
                string type = (friend ? "свой" : "пассажирский") + " ";

                if (trainingTug)
                    type = String.Empty;

                main.EndGame(
                    String.Format("Вы сбили {0}{1}!\nИгра окончена.\nСохранить статистику?", type, aircraftName),
                    Constants.END_COLOR
                );
            }
            else
            {
                dead = true;

                if (suspendedTarget)
                    suspendedTargetDowned = true;

                if (deadSprite)
                {
                    foreach (DynamicElement d in dynamicElemets)
                        main.firePlace.Children.Remove(d.element);

                    ImageSource sprite = ImageFromResources(aircraftType + "_dead", ImageType.DeadSprite);

                    if (sprite != null)
                        aircraftImage.Source = sprite;
                }

                Statistic.staticticAircraftShutdown += 1;
                Statistic.statisticAmountOfDamage += price;

                Statistic.statisticShutdownFlag = true;
                Statistic.statisticLastDamagePrice = price;
                Statistic.statisticLastDamageType = aircraftName;

                Statistic.AircraftToStatistic(aircraftName, Statistic.statisticAircraftType.downed);
            }
        }

        public bool TargetTubHit(Shell shell, ref bool itsOnlyTargetPlane)
        {
            if (flightDirection == FlightDirectionType.Right)
            {
                double targetPosition = aircraftImage.Margin.Left + Constants.TRAINING_IL28_TARGET_LEN;
                double aircraftPosition = targetPosition + Constants.TRAINING_IL28_TOW_LEN;

                if ((shell.x > targetPosition) && (shell.x < aircraftPosition))
                    return true;

                if (shell.x <= targetPosition)
                {
                    tragetTugHitPoint -= 1;
                    itsOnlyTargetPlane = true;
                }
            }
            else
            {
                double aircraftPosition = aircraftImage.Margin.Left + Constants.TRAINING_IL28_AIRCRAFT_LEN;
                double targetPosition = aircraftPosition + Constants.TRAINING_IL28_TOW_LEN;

                if ((shell.x > aircraftPosition) && (shell.x < targetPosition))
                    return true;

                if (shell.x >= targetPosition)
                {
                    tragetTugHitPoint -= 1;
                    itsOnlyTargetPlane = true;
                }
            }

            return false;
        }

        public void TargetTugDisengaged()
        {
            bool flyDirectRight = flightDirection == FlightDirectionType.Right;

            Statistic.staticticAircraftShutdown += 1;
            Statistic.statisticShutdownFlag = true;

            fly = false;

            int il28with77bm2 = Constants.TRAINING_IL28_INDEX;
            int il28 = Constants.TRAINING_IL28_WITHOUT_77bm2_INDEX;
            int target77bm2 = Constants.TRAINING_77bm2_INDEX;

            Aircraft newAircraft = Aircrafts.targetTugs[il28];
            double newX = (flyDirectRight ? x + (Aircrafts.targetTugs[il28with77bm2].size[0] - Aircrafts.targetTugs[il28].size[0]) : x);
            newAircraft.CreateNewAircraft(newX, y, flightDirection, transformation: true);

            newAircraft = Aircrafts.targetTugs[target77bm2];
            newX = (flyDirectRight ? x : x + (Aircrafts.targetTugs[il28with77bm2].size[0] - Aircrafts.targetTugs[target77bm2].size[0]));
            double newY = y + (Aircrafts.targetTugs[il28with77bm2].size[1] - Aircrafts.targetTugs[target77bm2].size[1]);

            newAircraft.CreateNewAircraft(newX, newY, flightDirection, dead: true, transformation: true);
        }

        private static int AircraftCategoryForSchool(int currentAircraftCategory, int allAircraftsInGame)
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

        private static Aircraft NewAircraftForTraining()
        {
            if (!suspendedTargetDowned)
                return Aircrafts.Cloud();
            else if (rand.Next(Constants.TRAINING_LAUNCH_PROBABILITTY) != 1)
                return Aircrafts.Cloud();
            else if (allAircraftsInGame < Constants.TRAINING_IL28_AT_THE_START)
                return Aircrafts.targetTugs[Constants.TRAINING_IL28_INDEX];
            else if (allAircraftsInGame < Constants.TRAINING_M16K_AT_THE_START)
                return Aircrafts.targetPlane[Constants.TRAINING_M16K_INDEX];
            else
                return Aircrafts.targetDrones[rand.Next(Aircrafts.targetDrones.Count)];
        }

        private void AircraftMessageForTraining(FirePlace main)
        {
            if (!trainingSuspendedTarget)
            {
                suspendedTargetTimerCount += 1;

                if (suspendedTargetTimerCount > Constants.TRAINING_TIMEOUT_BEFORE_FIRST_INFO)
                {
                    trainingSuspendedTarget = true;
                    main.SchoolMessage(Constants.SUSPENDED_TARGET_INFORMATION, Brushes.SeaGreen);
                }
            }

            if (!trainingTurgetTug && suspendedTargetDowned)
            {
                trainingTurgetTug = true;
                main.SchoolMessage(Constants.TRAINING_TUG_INFORMATION, Brushes.Green);
            }

            if (!trainingTurgetPlane && (allAircraftsInGame > Constants.TRAINING_IL28_AT_THE_START))
            {
                trainingTurgetPlane = true;
                main.SchoolMessage(Constants.TRAINING_PLANE_INFORMATION, Brushes.Crimson);
            }

            if (!trainingTurgetDrone && (allAircraftsInGame > Constants.TRAINING_M16K_AT_THE_START))
            {
                trainingTurgetDrone = true;
                main.SchoolMessage(Constants.TRAINING_DRONE_INFORMATION, Brushes.LightSeaGreen);
            }
        }

        private void AircraftMessagesForSchool(FirePlace main)
        {
            if ((allAircraftsInGame > Constants.SCHOOL_AIRLINER_AT_THE_START) && !schoolMixAlready)
            {
                schoolMixAlready = true;
                main.SchoolMessage(Constants.MIX_INFORMATION, Brushes.Gray);
            }
            else if (airliner && !schoolAirlinerAlready)
            {
                schoolAirlinerAlready = true;
                main.SchoolMessage(Constants.AIRLINER_INFORMATION, Brushes.Blue);
            }
            else if (friend && !schoolFriendAlready)
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

        public static void StartSuspendedTarget()
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                main.TruckCraneImg.Margin = new Thickness(
                    (SystemParameters.PrimaryScreenWidth - main.TruckCraneImg.Width - Constants.TRAINING_CRANE_LEFT_CORRECTTION),
                    (SystemParameters.PrimaryScreenHeight - main.TruckCraneImg.Height - Constants.TRAINING_CRANE_TOP_CORRECTTION),
                    0, 0
                );

                main.TruckCraneImg.Visibility = Visibility.Visible;

                int target;
                double suspendedTargetX, suspendedTargetY;

                if (rand.Next(2) == 1)
                {
                    target = Constants.TRAINING_OLD_MIG9_INDEX;

                    suspendedTargetX = main.TruckCraneImg.Margin.Left - Constants.TRAINING_CRANE_MIG9_LEFT_CORRECTTION;
                    suspendedTargetY = main.TruckCraneImg.Margin.Top + Constants.TRAINING_CRANE_MIG9_TOP_CORRECTTION;
                }
                else
                {
                    target = Constants.TRAINING_OLD_MIG15_INDEX;

                    suspendedTargetX = main.TruckCraneImg.Margin.Left - Constants.TRAINING_CRANE_MIG15_LEFT_CORRECTTION;
                    suspendedTargetY = main.TruckCraneImg.Margin.Top + Constants.TRAINING_CRANE_MIG15_TOP_CORRECTTION;
                }

                Aircraft newAircraft = Aircrafts.targetTugs[target];
                newAircraft.CreateNewAircraft(startX: suspendedTargetX, startY: suspendedTargetY, suspended: true);
            }));
        }

        public static void Start(object obj, ElapsedEventArgs e)
        {
            int aircraftCategory = rand.Next(1, 17);

            if (Shilka.school)
                aircraftCategory = AircraftCategoryForSchool(aircraftCategory, allAircraftsInGame);
            
            int dice;

            Aircraft newAircraft;

            if (Shilka.training)
                newAircraft = NewAircraftForTraining();
            else
                switch (aircraftCategory)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    default:

                        if (Shilka.currentScript == Scripts.scriptsNames.Vietnam)
                            goto case 5;

                        newAircraft = Aircrafts.Cloud();
                        break;

                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:

                        if ((Scripts.scriptAircraft != null) && (Scripts.scriptAircraft.Length == 0))
                            goto case 1;

                        do
                        {
                            dice = rand.Next(Aircrafts.aircraft.Count);
                        }
                        while (!AircraftInList(Scripts.scriptAircraft, dice));

                        string aircraftType = Aircrafts.aircraft[dice].aircraftType;

                        if ((Shilka.currentScript == Scripts.scriptsNames.F117Hunt) && (aircraftType != "f117"))
                            goto case 1;

                        if (Weather.currentWeather != Weather.weatherTypes.good && Aircrafts.aircraft[dice].doesNotFlyInBadWeather)
                            goto case 9;

                        if (Shilka.currentScript == Scripts.scriptsNames.Khmeimim)
                            goto case 10;

                        if ((Shilka.currentScript != Scripts.scriptsNames.Rust) && (aircraftType == "cessna"))
                            goto case 5;

                        if ((Shilka.currentScript == Scripts.scriptsNames.Rust) && (aircraftType != "cessna"))
                            goto case 15;

                        newAircraft = Aircrafts.aircraft[dice];

                        break;

                    case 10:
                    case 11:
                    case 12:

                        if ((Scripts.scriptHelicopters != null) && (Scripts.scriptHelicopters.Length == 0))
                            goto case 5;

                        do
                        {
                            dice = rand.Next(Aircrafts.helicopters.Count);
                        }
                        while (!AircraftInList(Scripts.scriptHelicopters, dice));

                        newAircraft = Aircrafts.helicopters[dice];

                        break;

                    case 13:

                        if ((Scripts.scriptAircraftFriend != null) && (Scripts.scriptAircraftFriend.Length == 0))
                            goto case 1;

                        do
                        {
                            dice = rand.Next(Aircrafts.aircraftFriend.Count);
                        }
                        while (!AircraftInList(Scripts.scriptAircraftFriend, dice));

                        newAircraft = Aircrafts.aircraftFriend[dice];

                        break;

                    case 14:

                        if ((Scripts.scriptHelicoptersFriend != null) && (Scripts.scriptHelicoptersFriend.Length == 0))
                            goto case 13;

                        do
                        {
                            dice = rand.Next(Aircrafts.helicoptersFriend.Count);
                        }
                        while (!AircraftInList(Scripts.scriptHelicoptersFriend, dice));

                        newAircraft = Aircrafts.helicoptersFriend[dice];

                        break;

                    case 15:

                        if (Shilka.currentScript == Scripts.scriptsNames.Vietnam)
                            goto case 5;

                        if ((Scripts.scriptAirliners != null) && (Scripts.scriptAirliners.Length == 0))
                            goto case 1;

                        do
                        {
                            dice = rand.Next(Aircrafts.airliners.Count);
                        }
                        while (!AircraftInList(Scripts.scriptAirliners, dice));

                        newAircraft = Aircrafts.airliners[dice];

                        break;

                    case 16:

                        if (
                                Shilka.currentScript != Scripts.scriptsNames.noScript
                                &&
                                Shilka.currentScript != Scripts.scriptsNames.KoreanBoeing
                            )
                            goto case 10;

                        dice = rand.Next(Aircrafts.aerostat.Count);

                        newAircraft = Aircrafts.aerostat[dice];

                        break;
                }

            newAircraft.CreateNewAircraft();
        }
    }
}
