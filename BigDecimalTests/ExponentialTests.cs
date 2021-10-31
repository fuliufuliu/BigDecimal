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
            // Assert.AreEqual(new BigDecimal("2.71828 18284 59045 23536 02874 713527"), BigDecimal.Exp(1, 31));
            // Assert.AreEqual(new BigDecimal("2.7182818284590452353602874713527"), 
                // BigDecimal.Exp(1, 33));
        }
    }
}
