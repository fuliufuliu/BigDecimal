using BigDecimals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigDecimalTests
{
    [TestClass]
    public class RemainderTest
    {
        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual(new BigDecimal("485645123215448924.298854")% 100000, new BigDecimal("48924.298854"));
            Assert.AreEqual(new BigDecimal("4856451232154325435435448924.29885432543254")% 1000000000000, new BigDecimal("435435448924.29885432543254"));
            Assert.AreEqual(new BigDecimal("5.011") % new BigDecimal("0.25"), 
                new BigDecimal("0.011"));
            Assert.AreEqual(new BigDecimal("45.88") % new BigDecimal("0.7"),
                new BigDecimal("0.38"));
        }
    }
}
