using System;
using BigDecimals;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigDecimalTests
{
    [TestClass]
    public class ConstructorTests
    {

        [TestMethod]
        public void StringConstructorTest()
        {
            BigDecimal bd = new BigDecimal("1.");
            Assert.AreEqual("1.", bd.ToString());

            bd = new BigDecimal("1.23");
            Assert.AreEqual("1.23", bd.ToString());

            bd = new BigDecimal("123");
            Assert.AreEqual("123.", bd.ToString());
        }
    }
}
