using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ean13.UnitTest
{
    [TestClass]
    public class Ean13GeneratorTest
    {
        [TestMethod]
        public void Barcode()
        {
            const string expected = "10101101110010111001100101100110110001010011101010111001011011001000100100010011101001011100101";

            var barcode = Ean13Generator.Barcode("7891150027794");
            var actual = string.Join(string.Empty, barcode);

            Assert.AreEqual(expected, actual);
        }
    }
}