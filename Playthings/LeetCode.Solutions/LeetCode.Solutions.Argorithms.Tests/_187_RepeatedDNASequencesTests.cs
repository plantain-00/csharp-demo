using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _187_RepeatedDNASequencesTests
    {
        [TestMethod]
        public void FindRepeatedDnaSequencesTest()
        {
            var result = new _187_RepeatedDNASequences().FindRepeatedDnaSequences("AAAAACCCCCAAAAACCCCCCAAAAAGGGTTT").ToArray();
            Assert.IsTrue(result.Length == 2);
            Assert.IsTrue(result[0] == "AAAAACCCCC");
            Assert.IsTrue(result[1] == "CCCCCAAAAA");

            result = new _187_RepeatedDNASequences().FindRepeatedDnaSequences("AAAAAAAAAAA").ToArray();
            Assert.IsTrue(result.Length == 1);
            Assert.IsTrue(result[0] == "AAAAAAAAAA");
        }
    }
}