using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _204_CountPrimesTests
    {
        [TestMethod]
        public void CountPrimesTest()
        {
            var x = new _204_CountPrimes();
            Assert.IsTrue(x.CountPrimes(2) == 0);
            Assert.IsTrue(x.CountPrimes(3) == 1);
            Assert.IsTrue(x.CountPrimes(4) == 2);
            Assert.IsTrue(x.CountPrimes(5) == 2);
            Assert.IsTrue(x.CountPrimes(6) == 3);
        }
    }
}