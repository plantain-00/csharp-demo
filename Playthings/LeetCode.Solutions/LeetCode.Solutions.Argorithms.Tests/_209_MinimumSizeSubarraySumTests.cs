using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _209_MinimumSizeSubarraySumTests
    {
        [TestMethod]
        public void MinSubArrayLenTest()
        {
            var tmp = new _209_MinimumSizeSubarraySum();
            Assert.IsTrue(tmp.MinSubArrayLen(7,
                                             new[]
                                             {
                                                 2,
                                                 3,
                                                 1,
                                                 2,
                                                 4,
                                                 3
                                             }) == 2);
        }
    }
}