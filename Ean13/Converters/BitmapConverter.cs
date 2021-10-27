using System.Drawing;
using System.Drawing.Imaging;

namespace Ean13.Converters
{
    public static class BitmapConverter
    {
        public static Bitmap Bitmap(this Ean13Barcode barcode, int scale = 2)
        {
            var width = 95 * scale;
            var height = width / 2;
            var bitmap = new Bitmap(width, height);
            var i = 0;

            foreach (var point in barcode.Sequence())
            {
                for (var j = 0; j < scale; j++, i++)
                {
                    if (point == 0)
                        continue;

                    for (var k = 0; k < height; k++)
                        bitmap.SetPixel(i, k, Color.Black);
                }
            }

            return bitmap;
        }

        public static void SaveAsPng(this Ean13Barcode barcode, string path, int scale = 2)
        {
            var bitmap = barcode.Bitmap(scale);

            bitmap.Save(path, ImageFormat.Png);
        }
    }
}