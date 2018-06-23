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
            Assert.AreEqual(new BigDecimal(".6931471799"), BigDecimal.Ln(2, 10));
        }
    }
}
