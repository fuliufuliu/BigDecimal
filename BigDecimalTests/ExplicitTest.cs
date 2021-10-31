using BigDecimals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigDecimalTests
{
    [TestClass]
    public class ExplicitTest
    {
        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual((float)new BigDecimal("432432.4322"), 432432.4322f);
            Assert.AreEqual((double)new BigDecimal("43243200000.4322"), 43243200000.4322);
        }
    }
}
