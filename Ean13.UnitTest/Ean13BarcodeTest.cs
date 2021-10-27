using System;
using Ean13.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ean13.UnitTest
{
    [TestClass]
    public class Ean13BarcodeTest
    {
        [TestMethod]
        public void Sequence()
        {
            const string expected = "10101101110010111001100101100110110001010011101010111001011011001000100100010011101001011100101";

            var barcode = new Ean13Barcode("789115002779");
            var sequence = barcode.Sequence();
            var actual = string.Join(string.Empty, sequence);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void VerifiedDigit_789115002779()
        {
            var barcode = new Ean13Barcode("789115002779");

            Assert.AreEqual(4, barcode.VerifiedDigit);
        }

        [TestMethod]
        public void VerifiedDigit_789100031550()
        {
            var barcode = new Ean13Barcode("789100031550");

            Assert.AreEqual(7, barcode.VerifiedDigit);
        }

        [TestMethod]
        public void Barcode()
        {
            var barcode = new Ean13Barcode("789115002779");

            Assert.AreEqual("7891150027794", barcode.ToString());

            barcode.SaveAsPng(@"D:\Estudos\svg\01-inicio\img.png");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidBarcode()
        {
            new Ean13Barcode("7891150027794");
        }
    }
}