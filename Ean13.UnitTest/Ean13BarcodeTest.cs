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

            var barcode = new Ean13Barcode("7891150027794");
            var actual = string.Join(string.Empty, barcode.Sequence);

            Assert.AreEqual(expected, actual);
        }
    }
}