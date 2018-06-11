using System;
using BigDecimals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigDecimalTests
{
    [TestClass]
    public class SqrtTests
    {
        [TestMethod]
        public void Sqrt1Test()
        {
            BigDecimal x = new BigDecimal(1);
            Assert.AreEqual("1.", x.ToString());
            string s = BigDecimal.Sqrt(x).ToString();
            Assert.AreEqual("1.", s);
        }

        [TestMethod]
        public void Sqrt2Test()
        {
            BigDecimal x = new BigDecimal(2, 0, 10);
            Assert.AreEqual("2.", x.ToString());
            string s = BigDecimal.Sqrt(x).ToString();
            Assert.AreEqual("1.41421356237", s);
        }

        [TestMethod]
        public void SqrtZeroTest()
        {
            BigDecimal x = new BigDecimal(0, 0);
            Assert.AreEqual("0.", x.ToString());
            string s = BigDecimal.Sqrt(x).ToString();
            Assert.AreEqual("0.", s);
        }

        [TestMethod]
        public void SqrtNegativeTest()
        {
            BigDecimal x = new BigDecimal(-1, 0);
            try
            {
                BigDecimal.Sqrt(x);
                Assert.Fail();
            }
            catch(ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }

    }
}
