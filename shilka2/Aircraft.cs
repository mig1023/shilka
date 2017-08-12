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

namespace shilka2
{
    class Aircraft
    {
        public double x { get; set; }
        public double y { get; set; }

        static List<Image> aircraftsImages = new List<Image>();
        public static List<Aircraft> aircrafts = new List<Aircraft>();

        public static void AircraftStart()
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                Image newAircraft = new Image();
                newAircraft.Source = new BitmapImage(new Uri("images/f-117-left.png", UriKind.Relative)) {};
                newAircraft.Margin = new Thickness( 100, 100, 0, 0);

// слишком большая картинка

                main.firePlace.Children.Add(newAircraft);
            }));
        }
    }
}
