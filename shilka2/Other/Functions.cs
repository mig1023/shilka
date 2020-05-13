using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace shilka2
{
    class Functions
    {
        public static ImageSource ImageFromResources(string imageName,
            Aircraft.ImageType type = Aircraft.ImageType.Aircraft, bool noInvert = false)
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

            if (Shilka.night && !noInvert)
                return Invert(image);

            return image;
        }

        private static bool InvertCount(ref int a)
        {
            if (a > 2)
            {
                a = 0;
                return true;
            }
            else
            {
                a += 1;
                return false;
            }
        }

        public static BitmapSource Invert(BitmapSource originalSource)
        {
            int stride = (originalSource.PixelWidth * originalSource.Format.BitsPerPixel + 7) / 8;

            int length = stride * originalSource.PixelHeight;
            byte[] data = new byte[length];

            originalSource.CopyPixels(data, stride, 0);

            int a = 0;

            for (int i = 0; i < length; i += 1)
                if (!InvertCount(ref a))
                    data[i] = (byte)(255 - data[i]);

            List<Color> colors = new List<Color>
            {
                Colors.Black
            };
            BitmapPalette palette = new BitmapPalette(colors);

            return BitmapSource.Create(originalSource.PixelWidth, originalSource.PixelHeight,
                originalSource.DpiX, originalSource.DpiY, originalSource.Format, palette, data, stride);
        }
    }
}
