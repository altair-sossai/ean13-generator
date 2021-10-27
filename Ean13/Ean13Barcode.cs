using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ean13
{
    public class Ean13Barcode
    {
        private const string Pattern = @"^\d{12}$";
        private readonly string _numbers;

        private string _barcode;
        private int _verifiedDigit = -1;

        public Ean13Barcode(string numbers)
        {
            if (!Regex.IsMatch(numbers, Pattern))
                throw new ArgumentException("Informe uma sequência numérica com doze (12) dígitos.", nameof(numbers));

            _numbers = numbers;
        }

        public int VerifiedDigit
        {
            get
            {
                if (_verifiedDigit != -1)
                    return _verifiedDigit;

                var sum = _numbers
                    .Select(number => number - 48)
                    .Select((number, i) => i % 2 == 0 ? number : number * 3)
                    .Sum();

                var verifiedDigit = (Math.Floor(sum / 10d) + 1) * 10 - sum;

                if (verifiedDigit % 10 == 0)
                    verifiedDigit = 0;

                return _verifiedDigit = (int) verifiedDigit;
            }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(_barcode) ? _barcode = $"{_numbers}{VerifiedDigit}" : _barcode;
        }
    }
}