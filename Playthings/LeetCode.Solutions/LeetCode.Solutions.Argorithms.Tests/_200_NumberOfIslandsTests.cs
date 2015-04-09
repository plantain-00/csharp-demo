using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _200_NumberOfIslandsTests
    {
        [TestMethod]
        public void NumIslandsTest()
        {
            var array = new char[4][];
            array[0] = new[]
                       {
                           '1',
                           '1',
                           '1',
                           '1',
                           '0'
                       };
            array[1] = new[]
                       {
                           '1',
                           '1',
                           '0',
                           '1',
                           '0'
                       };
            array[2] = new[]
                       {
                           '1',
                           '1',
                           '0',
                           '0',
                           '0'
                       };
            array[3] = new[]
                       {
                           '0',
                           '0',
                           '0',
                           '0',
                           '0'
                       };
            Assert.IsTrue(new _200_NumberOfIslands().NumIslands(array) == 1);
        }
    }
}