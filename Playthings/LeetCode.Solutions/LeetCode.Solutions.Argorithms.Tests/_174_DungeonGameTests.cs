using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _174_DungeonGameTests
    {
        [TestMethod]
        public void CalculateMinimumHPTest()
        {
            var array = new[,]
                        {
                            {
                                -2,
                                -3,
                                3
                            },
                            {
                                -5,
                                -10,
                                1
                            },
                            {
                                10,
                                30,
                                -5
                            }
                        };
            Assert.IsTrue(new _174_DungeonGame().CalculateMinimumHP(array) == 7);
        }
    }
}