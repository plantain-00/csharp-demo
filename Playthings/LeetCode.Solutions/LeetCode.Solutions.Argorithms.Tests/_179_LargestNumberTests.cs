using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _179_LargestNumberTests
    {
        [TestMethod]
        public void LargestNumberTest()
        {
            var input = new[]
                        {
                            3,
                            30,
                            34,
                            5,
                            9
                        };
            Assert.IsTrue(new _179_LargestNumber().LargestNumber(input) == "9534330");
        }
    }
}