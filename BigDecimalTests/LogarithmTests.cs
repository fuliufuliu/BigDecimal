using System;
using BigDecimals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigDecimalTests
{
    [TestClass]
    public class LogarithmTests
    {
        [TestMethod]
        public void Ln2Test()
        {
            Assert.AreEqual(new BigDecimal(".6931471805"), BigDecimal.Ln(2, 10));
        }

        [TestMethod]
        public void Ln10Test()
        {
            Assert.AreEqual(new BigDecimal("2.3025"), BigDecimal.Ln(10, 4));
        }

        [TestMethod]
        public void LnETest()
        {
            Assert.AreEqual(new BigDecimal("1."), BigDecimal.Exp(BigDecimal.Ln(1, 2), 2));
        }
    }
}
