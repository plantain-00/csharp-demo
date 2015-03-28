using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _190_ReverseBitsTests
    {
        [TestMethod]
        public void ReverseBitsTest()
        {
            Assert.IsTrue(new _190_ReverseBits().ReverseBits(43261596) == 964176192);
        }
    }
}