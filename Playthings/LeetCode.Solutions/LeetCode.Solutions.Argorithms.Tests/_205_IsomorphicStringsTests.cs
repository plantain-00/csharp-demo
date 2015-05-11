using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _205_IsomorphicStringsTests
    {
        [TestMethod]
        public void IsIsomorphicTest()
        {
            var tmp = new _205_IsomorphicStrings();
            Assert.IsTrue(tmp.IsIsomorphic("egg", "add"));
            Assert.IsFalse(tmp.IsIsomorphic("foo", "bar"));
            Assert.IsTrue(tmp.IsIsomorphic("paper", "title"));
            Assert.IsFalse(tmp.IsIsomorphic("ab", "aa"));
        }
    }
}