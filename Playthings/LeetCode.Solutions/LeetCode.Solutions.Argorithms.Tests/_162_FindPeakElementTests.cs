using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _162_FindPeakElementTests
    {
        [TestMethod]
        public void FindPeakElementTest()
        {
            var array = new[]
                        {
                            1,
                            2,
                            3,
                            1
                        };
            Assert.IsTrue(new _162_FindPeakElement().FindPeakElement(array) == 2);
        }
    }
}