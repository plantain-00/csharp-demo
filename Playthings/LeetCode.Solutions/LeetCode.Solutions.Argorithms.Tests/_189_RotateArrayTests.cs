using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _189_RotateArrayTests
    {
        [TestMethod]
        public void RotateTest()
        {
            var array = new[]
                        {
                            1,
                            2,
                            3,
                            4,
                            5,
                            6,
                            7
                        };
            new _189_RotateArray().Rotate(array, 3);
            Assert.IsTrue(array[0] == 5);
            Assert.IsTrue(array[1] == 6);
            Assert.IsTrue(array[2] == 7);

            Assert.IsTrue(array[3] == 1);
            Assert.IsTrue(array[4] == 2);
            Assert.IsTrue(array[5] == 3);
            Assert.IsTrue(array[6] == 4);

            array = new[]
                    {
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7,
                        8
                    };
            new _189_RotateArray().Rotate(array, 2);
            Assert.IsTrue(array[0] == 7);
            Assert.IsTrue(array[1] == 8);

            Assert.IsTrue(array[2] == 1);
            Assert.IsTrue(array[3] == 2);
            Assert.IsTrue(array[4] == 3);
            Assert.IsTrue(array[5] == 4);
            Assert.IsTrue(array[6] == 5);
            Assert.IsTrue(array[7] == 6);

            array = new[]
                    {
                        1
                    };
            new _189_RotateArray().Rotate(array, 0);
            Assert.IsTrue(array[0] == 1);

            array = new[]
                    {
                        1,
                        2
                    };
            new _189_RotateArray().Rotate(array, 3);
            Assert.IsTrue(array[0] == 2);
            Assert.IsTrue(array[1] == 1);

            array = new[]
                    {
                        1,
                        2,
                        3,
                        4,
                        5,
                        6
                    };
            new _189_RotateArray().Rotate(array, 4);
            Assert.IsTrue(array[0] == 3);
            Assert.IsTrue(array[1] == 4);
            Assert.IsTrue(array[2] == 5);
            Assert.IsTrue(array[3] == 6);
            Assert.IsTrue(array[4] == 1);
            Assert.IsTrue(array[5] == 2);
        }
    }
}