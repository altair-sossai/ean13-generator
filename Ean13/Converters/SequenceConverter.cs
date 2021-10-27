using System.Collections.Generic;

namespace Ean13.Converters
{
    public static class SequenceConverter
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

        public static IEnumerable<byte> Sequence(this Ean13Barcode ean13)
        {
            var barcode = ean13.ToString();
            var first = barcode[0] - 48;

            for (byte j = 0, flag = 1; j < 3; j++, flag = (byte) (flag == 1 ? 0 : 1))
                yield return flag;

            for (byte j = 0; j < 6; j++)
            {
                var digit = barcode[j + 1] - 48;
                var sequence = Sequences[first, j];

                for (byte k = 0; k < 7; k++)
                    yield return Digits[digit, sequence, k];
            }

            for (byte j = 0, flag = 0; j < 5; j++, flag = (byte) (flag == 1 ? 0 : 1))
                yield return flag;

            for (byte j = 6; j < 12; j++)
            {
                var digit = barcode[j + 1] - 48;
                var sequence = Sequences[first, j];

                for (byte k = 0; k < 7; k++)
                    yield return Digits[digit, sequence, k];
            }

            for (byte j = 0, flag = 1; j < 3; j++, flag = (byte) (flag == 1 ? 0 : 1))
                yield return flag;
        }
    }
}