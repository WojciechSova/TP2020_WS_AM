using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Class1 calc = new Class1();
            Random rand = new Random();
            int a = rand.Next();
            int b = calc.Add(4, a);
            Assert.AreEqual(a + 4, b);
        }
    }
}
