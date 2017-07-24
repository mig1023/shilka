using System;
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

            Line gun = new Line();
            gun.X1 = Shell.FIRE_WIDTH_CORRECTION - 3;
            gun.Y1 = currentHeight - Shell.FIRE_HEIGHT_CORRECTION + 5;
            gun.X2 = gun.X1 + 30 * Shell.LastCos;
            gun.Y2 = gun.Y1 - 30 * Shell.LastSin;
            gun.Stroke = Brushes.Black;
            gun.StrokeThickness = 3;
            main.firePlace.Children.Add(gun);
            Shell.allLines.Add(gun);

            gun = new Line();
            gun.X1 = Shell.FIRE_WIDTH_CORRECTION - 12;
            gun.Y1 = currentHeight - Shell.FIRE_HEIGHT_CORRECTION - 2;
            gun.X2 = gun.X1 + 30 * Shell.LastCos;
            gun.Y2 = gun.Y1 - 30 * Shell.LastSin;
            gun.Stroke = Brushes.Black;
            gun.StrokeThickness = 3;
            main.firePlace.Children.Add(gun);
            Shell.allLines.Add(gun);
        }
    }
}
