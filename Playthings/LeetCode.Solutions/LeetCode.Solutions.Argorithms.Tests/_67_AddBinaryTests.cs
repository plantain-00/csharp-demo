using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _67_AddBinaryTests
    {
        [TestMethod]
        public void AddBinaryTest()
        {
            var tmp = new _67_AddBinary();
            Assert.IsTrue(tmp.AddBinary("11", "1") == "100");
            Assert.IsTrue(tmp.AddBinary("10", "1") == "11");
        }
    }
}