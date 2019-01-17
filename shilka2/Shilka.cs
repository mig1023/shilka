﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Media.Imaging;

namespace shilka2
{
    class Shilka
    {
        const double LAST_DEGREE_CORRECTION = 10;
        const int GUNS_LENGTH = 30;
        const int GUN_NOUNT_LENGTH = 10;

        static int flashСount = 0;

        public static Scripts.scriptsNames currentScript;

        public static int degreeOfHeatingGunBurrels = 30;
        public static bool reheatingGunBurrels = false;

        static int gunReturn = 0; 

        public static double lastDegree = 0;
        public static bool school = false;

        static Random rand;

        static Shilka()
        {
            rand = new Random();
        }

        public static void EndGameCleaning()
        {
            Statistic.Clean();

            degreeOfHeatingGunBurrels = 30;
            reheatingGunBurrels = false;
            lastDegree = 0;

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;

                main.firePlace.Children.Clear();
                main.PlayerName.Text = "";

                Aircraft.aircrafts.Clear();
                Case.cases.Clear();
                Shell.shells.Clear();
            }));
        }

        public static void SetNewTergetPoint(Point pt, object sender)
        {
            Shell.ptX = pt.X - Shell.FIRE_WIDTH_CORRECTION;
            Shell.ptY = (sender as Window).Height - pt.Y - Shell.FIRE_HEIGHT_POINT_CORRECTION;
            Shell.currentHeight = (sender as Window).Height;
            Shell.currentWidth = (sender as Window).Width + Shell.FIRE_WIDTH_CORRECTION;

            if (Shell.ptX < 0)
                Shell.ptX = 0;

            if (Shell.ptY < 0)
                Shell.ptY = 0;

            double LastSin = Shell.ptY / Math.Sqrt((Shell.ptX * Shell.ptX) + (Shell.ptY * Shell.ptY));
            double newLastDegree = Math.Asin(LastSin) * (180 / Math.PI) * -1;

            newLastDegree += LAST_DEGREE_CORRECTION;
            if (newLastDegree > 0)
                newLastDegree = 0;

            lastDegree = ( double.IsNaN(newLastDegree) ? lastDegree : newLastDegree );
        }

        public static void HeatingOfGuns(bool shooting)
        {
            if (rand.Next(2) == 1)
                return;

            degreeOfHeatingGunBurrels += (shooting ? 1 : -1);

            if (degreeOfHeatingGunBurrels < 30)
                degreeOfHeatingGunBurrels = 30;
            else if (degreeOfHeatingGunBurrels > 350)
            {
                reheatingGunBurrels = true;
                Shell.fire = false;
            }
            else if (degreeOfHeatingGunBurrels < 300)
                reheatingGunBurrels = false;
        }

        public static Brush FlashesColor()
        {
            switch (rand.Next(4))
            {
                case 1:  return Brushes.DarkRed;
                case 2:  return Brushes.Firebrick;
                case 3:  return Brushes.Maroon;
                default: return Brushes.Red;
            }
        }

        public static void DrawGansFlashs(FirePlace main, Line gun, int numGuns)
        {
            flashСount++;

            if (flashСount >= 10)
                flashСount = 0;

            if (
                ((flashСount >= 2) && (flashСount < 5) && (numGuns == 0))
                ||
                ((flashСount >= 7) && (numGuns == 1))
            )
            {
                Line flash = new Line();
                flash.X1 = gun.X2;
                flash.Y1 = gun.Y2;

                int flashSize = rand.Next(3) + 1;

                flash.X2 = gun.X2 + flashSize * Shell.lastCos;
                flash.Y2 = gun.Y2 - flashSize * Shell.lastSin;

                flash.Stroke = FlashesColor();

                flash.StrokeThickness = rand.Next(2) + 4;
                main.firePlace.Children.Add(flash);
                Canvas.SetZIndex(flash, 210);
                Shell.allLines.Add(flash);
            }
        }

        public static void DrawGuns(FirePlace main)
        {

            double currentHeight = Shell.currentHeight;
            if (currentHeight < 0)
                currentHeight = main.ActualHeight;

            if (Shell.fire)
                gunReturn++;

            if (gunReturn > 3)
                gunReturn = 0;

            double[,] mountXY = new double[2, 2] { { 0, 0 }, { 0, 0 } };

            for (int numGuns = 0; numGuns <= 1; numGuns++)
            {
                Line gun = new Line();
                gun.X1 = Shell.FIRE_WIDTH_CORRECTION - 3 - (12 * numGuns);
                gun.Y1 = currentHeight - Shell.FIRE_HEIGHT_CORRECTION + 5 - (9 * numGuns);

                int gunReturnLen = 0;
                if (Shell.fire && ((gunReturn < 2 && numGuns == 0) || (gunReturn >= 2 && numGuns == 1)))
                    gunReturnLen = 5;

                gun.X2 = gun.X1 + (GUNS_LENGTH - gunReturnLen) * Shell.lastCos;
                gun.Y2 = gun.Y1 - (GUNS_LENGTH - gunReturnLen) * Shell.lastSin;

                mountXY[numGuns, 0] = gun.X1 + (GUNS_LENGTH - gunReturnLen - GUN_NOUNT_LENGTH) * Shell.lastCos;
                mountXY[numGuns, 1] = gun.Y1 - (GUNS_LENGTH - gunReturnLen - GUN_NOUNT_LENGTH) * Shell.lastSin;

                byte colorOfGuns = (degreeOfHeatingGunBurrels > 200 ? (byte)((degreeOfHeatingGunBurrels - 200) / 2) : (byte)0);

                if (colorOfGuns == 0)
                    gun.Stroke = Brushes.Black;
                else
                    gun.Stroke = new SolidColorBrush(Color.FromRgb((byte)(colorOfGuns - 200), 0, 0));

                gun.StrokeThickness = 3;
                main.firePlace.Children.Add(gun);
                Canvas.SetZIndex(gun, 200);
                Shell.allLines.Add(gun);

                if (Shell.fire)
                    DrawGansFlashs(main, gun, numGuns);
            }

            Line gunMount = new Line();

            gunMount.X1 = mountXY[0, 0];
            gunMount.Y1 = mountXY[0, 1];
            gunMount.X2 = mountXY[1, 0];
            gunMount.Y2 = mountXY[1, 1];

            gunMount.StrokeThickness = 1;
            gunMount.Stroke = Brushes.Black;

            main.firePlace.Children.Add(gunMount);
            Canvas.SetZIndex(gunMount, 200);
            Shell.allLines.Add(gunMount);
        }
    }
}
