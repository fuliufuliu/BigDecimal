using System;
using BigDecimals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigDecimalTests
{
    [TestClass]
    public class ExponentialTests
    {
        [TestMethod]
        public void Exponential1Test()
        {
            Assert.AreEqual(new BigDecimal("2.7182818284"), BigDecimal.Exp(1, 10));
        }
    }
}
