namespace Ean13
{
    public class Ean13Barcode
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

        public Ean13Barcode(string numbers)
        {
            Numbers = numbers;
            BuildSequence();
        }

        public string Numbers { get; }
        public byte[] Sequence { get; } = new byte[95];

        private void BuildSequence()
        {
            var first = Numbers[0] - 48;
            var i = 0;

            for (byte j = 0, flag = 1; j < 3; j++, flag = (byte) (flag == 1 ? 0 : 1))
                Sequence[i++] = flag;

            for (byte j = 0; j < 6; j++)
            {
                var digit = Numbers[j + 1] - 48;
                var sequence = Sequences[first, j];

                for (byte k = 0; k < 7; k++)
                    Sequence[i++] = Digits[digit, sequence, k];
            }

            for (byte j = 0, flag = 0; j < 5; j++, flag = (byte) (flag == 1 ? 0 : 1))
                Sequence[i++] = flag;

            for (byte j = 6; j < 12; j++)
            {
                var digit = Numbers[j + 1] - 48;
                var sequence = Sequences[first, j];

                for (byte k = 0; k < 7; k++)
                    Sequence[i++] = Digits[digit, sequence, k];
            }

            for (byte j = 0, flag = 1; j < 3; j++, flag = (byte) (flag == 1 ? 0 : 1))
                Sequence[i++] = flag;
        }
    }
}