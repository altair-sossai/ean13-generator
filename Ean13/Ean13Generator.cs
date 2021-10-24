using System.Collections.Generic;

namespace Ean13
{
    public static class Ean13Generator
    {
        private static readonly byte[,] Sequences =
        {
            {0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2},
            {0, 0, 1, 0, 1, 1, 2, 2, 2, 2, 2, 2},
            {0, 0, 1, 1, 0, 1, 2, 2, 2, 2, 2, 2},
            {0, 0, 1, 1, 1, 0, 2, 2, 2, 2, 2, 2},
            {0, 1, 0, 0, 1, 1, 2, 2, 2, 2, 2, 2},
            {0, 1, 1, 0, 0, 1, 2, 2, 2, 2, 2, 2},
            {0, 1, 1, 1, 0, 0, 2, 2, 2, 2, 2, 2},
            {0, 1, 0, 1, 0, 1, 2, 2, 2, 2, 2, 2},
            {0, 1, 0, 1, 1, 0, 2, 2, 2, 2, 2, 2},
            {0, 1, 1, 0, 1, 0, 2, 2, 2, 2, 2, 2}
        };

        private static readonly byte[,,] Digits =
        {
            {{0, 0, 0, 1, 1, 0, 1}, {0, 1, 0, 0, 1, 1, 1}, {1, 1, 1, 0, 0, 1, 0}},
            {{0, 0, 1, 1, 0, 0, 1}, {0, 1, 1, 0, 0, 1, 1}, {1, 1, 0, 0, 1, 1, 0}},
            {{0, 0, 1, 0, 0, 1, 1}, {0, 0, 1, 1, 0, 1, 1}, {1, 1, 0, 1, 1, 0, 0}},
            {{0, 1, 1, 1, 1, 0, 1}, {0, 1, 0, 0, 0, 0, 1}, {1, 0, 0, 0, 0, 1, 0}},
            {{0, 1, 0, 0, 0, 1, 1}, {0, 0, 1, 1, 1, 0, 1}, {1, 0, 1, 1, 1, 0, 0}},
            {{0, 1, 1, 0, 0, 0, 1}, {0, 1, 1, 1, 0, 0, 1}, {1, 0, 0, 1, 1, 1, 0}},
            {{0, 1, 0, 1, 1, 1, 1}, {0, 0, 0, 0, 1, 0, 1}, {1, 0, 1, 0, 0, 0, 0}},
            {{0, 1, 1, 1, 0, 1, 1}, {0, 0, 1, 0, 0, 0, 1}, {1, 0, 0, 0, 1, 0, 0}},
            {{0, 1, 1, 0, 1, 1, 1}, {0, 0, 0, 1, 0, 0, 1}, {1, 0, 0, 1, 0, 0, 0}},
            {{0, 0, 0, 1, 0, 1, 1}, {0, 0, 1, 0, 1, 1, 1}, {1, 1, 1, 0, 1, 0, 0}}
        };

        public static string Svg(string numbers, int scale = 2)
        {
            var barcode = Barcode(numbers);
            var line = new List<int>();

            for (int i = 1, count = scale; i < barcode.Length; i++, count += scale)
            {
                if (barcode[i] == barcode[i - 1])
                    continue;

                line.Add(count);
                count = 0;
            }

            var width = scale * 95;

            return $@"<svg xmlns=""http://www.w3.org/2000/svg""><line x1=""0"" y1=""0"" x2=""{width}"" y2=""0"" stroke=""black"" stroke-width=""{width}"" stroke-dasharray=""{string.Join(' ', line)}"" /></svg>";
        }

        public static byte[] Barcode(string numbers)
        {
            var barcode = new byte[95];
            var first = numbers[0] - 48;
            var i = 0;

            for (byte j = 0, flag = 1; j < 3; j++, flag = (byte) (flag == 1 ? 0 : 1))
                barcode[i++] = flag;

            for (byte j = 0; j < 6; j++)
            {
                var digit = numbers[j + 1] - 48;
                var sequence = Sequences[first, j];

                for (byte k = 0; k < 7; k++)
                    barcode[i++] = Digits[digit, sequence, k];
            }

            for (byte j = 0, flag = 0; j < 5; j++, flag = (byte) (flag == 1 ? 0 : 1))
                barcode[i++] = flag;

            for (byte j = 6; j < 12; j++)
            {
                var digit = numbers[j + 1] - 48;
                var sequence = Sequences[first, j];

                for (byte k = 0; k < 7; k++)
                    barcode[i++] = Digits[digit, sequence, k];
            }

            for (byte j = 0, flag = 1; j < 3; j++, flag = (byte) (flag == 1 ? 0 : 1))
                barcode[i++] = flag;

            return barcode;
        }
    }
}