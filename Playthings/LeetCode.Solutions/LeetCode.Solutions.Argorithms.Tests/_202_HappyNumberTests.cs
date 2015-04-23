using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _202_HappyNumberTests
    {
        [TestMethod]
        public void IsHappyTest()
        {
            Assert.IsTrue(new _202_HappyNumber().IsHappy(19));
            Assert.IsFalse(new _202_HappyNumber().IsHappy(18));
        }
    }
}