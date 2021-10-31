using System.Numerics;
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
            Assert.AreEqual(new BigDecimal("432432.4"), (BigDecimal)432432.4f); // float 的精度限制不能超过7位
            Assert.AreEqual((double)new BigDecimal("43243200000.4322"), 43243200000.4322);
            Assert.AreEqual((BigInteger)new BigDecimal("43243200000.4322"), 43243200000);
            Assert.AreEqual(new BigDecimal("43243200000.4322"), (BigDecimal)43243200000.4322);
            Assert.AreEqual((BigInteger)new BigDecimal("43243200000.4322"), new BigInteger(43243200000));
        }
    }
}
