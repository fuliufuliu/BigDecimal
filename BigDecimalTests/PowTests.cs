using System;
using BigDecimals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigDecimalTests
{
    [TestClass]
    public class PowTests
    {
        [TestMethod]
        public void PowersOf2Test()
        {
            BigDecimal bd = new BigDecimal(2);
            Assert.AreEqual(new BigDecimal(8), BigDecimal.Pow(bd, 3));
            Assert.AreEqual(new BigDecimal(16), BigDecimal.Pow(bd, 4));
            Assert.AreEqual(new BigDecimal(256), BigDecimal.Pow(BigDecimal.Pow(bd, 4), 2));
        }

        [TestMethod]
        public void DecimalPowerTest1()
        {
            BigDecimal bd = new BigDecimal(21, 1);
            Assert.AreEqual(new BigDecimal(441, 2), BigDecimal.Pow(bd, 2));
            Assert.AreEqual(new BigDecimal(9261, 3), BigDecimal.Pow(bd, 3));
            Assert.AreEqual(new BigDecimal(194481, 4), BigDecimal.Pow(bd, 4));
        }

        [TestMethod]
        public void DecimalPowerTest2()
        {
            BigDecimal bd = new BigDecimal(212, 2);
            Assert.AreEqual(new BigDecimal(44944, 4), BigDecimal.Pow(bd, 2));
            Assert.AreEqual(new BigDecimal(9528128, 6), BigDecimal.Pow(bd, 3));
            Assert.AreEqual(new BigDecimal(2019963136, 8), BigDecimal.Pow(bd, 4));
        }

        [TestMethod]
        public void DecimalPowerLimitPrecisionTest()
        {
            BigDecimal bd = new BigDecimal(22222, 4);
            Assert.AreEqual(new BigDecimal(29365096514548, 10), BigDecimal.Pow(bd, 10, 10));
        }
    }
}
