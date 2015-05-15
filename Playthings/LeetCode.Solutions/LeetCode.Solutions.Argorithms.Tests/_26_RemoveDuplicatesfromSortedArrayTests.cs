using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _26_RemoveDuplicatesfromSortedArrayTests
    {
        [TestMethod]
        public void RemoveDuplicatesTest()
        {
            var tmp = new[]
                      {
                          1,
                          1,
                          2
                      };
            var a = new _26_RemoveDuplicatesfromSortedArray().RemoveDuplicates(tmp);
        }
    }
}