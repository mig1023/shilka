﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace shilka2
{
    class Shilka
    {
        public const int SHILKA_HEIGHT_CORRECTION = 100;
        static int flash_count = 0;

        public static void SetNewTergetPoint(Point pt, object sender)
        {
            Shell.ptX = pt.X - Shell.FIRE_WIDTH_CORRECTION;
            Shell.ptY = (sender as Window).Height - pt.Y - Shell.FIRE_HEIGHT_POINT_CORRECTION;
            Shell.currentHeight = (sender as Window).Height;
            Shell.currentWidth = (sender as Window).Width + Shell.FIRE_WIDTH_CORRECTION;

            if (Shell.ptX < 0) Shell.ptX = 0;
            if (Shell.ptY < 0) Shell.ptY = 0;
        }

        public static void DrawGuns(MainWindow main)
        {
            double currentHeight = Shell.currentHeight;
            if (currentHeight < 0) currentHeight = main.ActualHeight;

            for (int num_guns = 0; num_guns <= 1; num_guns++)
            {
                Line gun = new Line();
                gun.X1 = Shell.FIRE_WIDTH_CORRECTION - 3 - (12 * num_guns);
                gun.Y1 = currentHeight - Shell.FIRE_HEIGHT_CORRECTION + 5 - (7 * num_guns);
                gun.X2 = gun.X1 + 30 * Shell.LastCos;
                gun.Y2 = gun.Y1 - 30 * Shell.LastSin;
                gun.Stroke = Brushes.Black;
                gun.StrokeThickness = 3;
                main.firePlace.Children.Add(gun);
                Shell.allLines.Add(gun);

                if (Shell.Fire)
                {
                    flash_count++;
                    if (flash_count >= 20)
                        flash_count = 0;
                    else
                        flash_count++;

                    if ( ( (flash_count >= 5) && (flash_count < 10) && (num_guns == 0) ) ||
                        ((flash_count >= 15) && (num_guns == 1)) )
                    {
                        Line flash = new Line();
                        flash.X1 = gun.X2;
                        flash.Y1 = gun.Y2;
                        flash.X2 = gun.X2 + 5 * Shell.LastCos;
                        flash.Y2 = gun.Y2 - 5 * Shell.LastSin;
                        flash.Stroke = Brushes.Red;
                        flash.StrokeThickness = 5;
                        main.firePlace.Children.Add(flash);
                        Shell.allLines.Add(flash);
                    }
                }

                    
            }
        }
    }
}