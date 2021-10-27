using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ean13.Converters
{
    public static class SvgConverter
    {
        public static string Svg(this Ean13Barcode barcode, int scale = 2)
        {
            var line = new List<int>();

            for (int i = 1, count = scale; i < barcode.Sequence.Length; i++, count += scale)
            {
                if (barcode.Sequence[i] == barcode.Sequence[i - 1])
                    continue;

                line.Add(count);
                count = 0;
            }

            var width = scale * 95;

            return $@"<svg xmlns=""http://www.w3.org/2000/svg""><line x1=""0"" y1=""0"" x2=""{width}"" y2=""0"" stroke=""black"" stroke-width=""{width}"" stroke-dasharray=""{string.Join(' ', line)}"" /></svg>";
        }

        public static void SaveAsSvg(this Ean13Barcode barcode, string path, int scale = 2)
        {
            var svg = barcode.Svg(scale);

            File.WriteAllText(path, svg, Encoding.UTF8);
        }
    }
}