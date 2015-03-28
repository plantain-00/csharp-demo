using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _191_NumberOf1BitsTests
    {
        [TestMethod]
        public void HammingWeightTest()
        {
            Assert.IsTrue(new _191_NumberOf1Bits().HammingWeight(11) == 3);
        }
    }
}