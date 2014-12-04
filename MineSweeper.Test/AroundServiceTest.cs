using System.Linq;

using MineSweeper.Interfaces;

using NUnit.Framework;

namespace MineSweeper.Test
{
    [TestFixture]
    internal class AroundServiceTest
    {
        [Test]
        [TestCase(0, 0, Directions.None, 3)]
        [TestCase(0, Constants.COLUMN_NUMBER - 1, Directions.None, 3)]
        [TestCase(Constants.ROW_NUMBER - 1, 0, Directions.None, 3)]
        [TestCase(Constants.ROW_NUMBER - 1, Constants.COLUMN_NUMBER - 1, Directions.None, 3)]
        [TestCase(0, 4, Directions.None, 5)]
        [TestCase(4, 0, Directions.None, 5)]
        [TestCase(Constants.ROW_NUMBER - 1, 4, Directions.None, 5)]
        [TestCase(4, Constants.COLUMN_NUMBER - 1, Directions.None, 5)]
        [TestCase(4, 4, Directions.None, 8)]
        [TestCase(0, 0, Directions.LeftDown, 2)]
        [TestCase(0, Constants.COLUMN_NUMBER - 1, Directions.LeftDown, 0)]
        [TestCase(Constants.ROW_NUMBER - 1, 0, Directions.LeftDown, 3)]
        [TestCase(Constants.ROW_NUMBER - 1, Constants.COLUMN_NUMBER - 1, Directions.LeftDown, 2)]
        [TestCase(0, 4, Directions.LeftDown, 2)]
        [TestCase(4, 0, Directions.LeftDown, 4)]
        [TestCase(Constants.ROW_NUMBER - 1, 4, Directions.LeftDown, 4)]
        [TestCase(4, Constants.COLUMN_NUMBER - 1, Directions.LeftDown, 2)]
        [TestCase(4, 4, Directions.LeftDown, 5)]
        [TestCase(0, 0, Directions.LeftUp, 3)]
        [TestCase(0, Constants.COLUMN_NUMBER - 1, Directions.LeftUp, 2)]
        [TestCase(Constants.ROW_NUMBER - 1, 0, Directions.LeftUp, 2)]
        [TestCase(Constants.ROW_NUMBER - 1, Constants.COLUMN_NUMBER - 1, Directions.LeftUp, 0)]
        [TestCase(0, 4, Directions.LeftUp, 4)]
        [TestCase(4, 0, Directions.LeftUp, 4)]
        [TestCase(Constants.ROW_NUMBER - 1, 4, Directions.LeftUp, 2)]
        [TestCase(4, Constants.COLUMN_NUMBER - 1, Directions.LeftUp, 2)]
        [TestCase(4, 4, Directions.LeftUp, 5)]
        [TestCase(0, 0, Directions.RightDown, 0)]
        [TestCase(0, Constants.COLUMN_NUMBER - 1, Directions.RightDown, 2)]
        [TestCase(Constants.ROW_NUMBER - 1, 0, Directions.RightDown, 2)]
        [TestCase(Constants.ROW_NUMBER - 1, Constants.COLUMN_NUMBER - 1, Directions.RightDown, 3)]
        [TestCase(0, 4, Directions.RightDown, 2)]
        [TestCase(4, 0, Directions.RightDown, 2)]
        [TestCase(Constants.ROW_NUMBER - 1, 4, Directions.RightDown, 4)]
        [TestCase(4, Constants.COLUMN_NUMBER - 1, Directions.RightDown, 4)]
        [TestCase(4, 4, Directions.RightDown, 5)]
        [TestCase(0, 0, Directions.RightUp, 2)]
        [TestCase(0, Constants.COLUMN_NUMBER - 1, Directions.RightUp, 3)]
        [TestCase(Constants.ROW_NUMBER - 1, 0, Directions.RightUp, 0)]
        [TestCase(Constants.ROW_NUMBER - 1, Constants.COLUMN_NUMBER - 1, Directions.RightUp, 2)]
        [TestCase(0, 4, Directions.RightUp, 4)]
        [TestCase(4, 0, Directions.RightUp, 2)]
        [TestCase(Constants.ROW_NUMBER - 1, 4, Directions.RightUp, 2)]
        [TestCase(4, Constants.COLUMN_NUMBER - 1, Directions.RightUp, 4)]
        [TestCase(4, 4, Directions.RightUp, 5)]
        public void ActCorrectly(int i,
                                 int j,
                                 Directions direction,
                                 int count)
        {
            var times = 0;
            AroundService.Act(i,
                              j,
                              delegate
                              {
                                  times++;
                              },
                              direction);
            Assert.IsTrue(times == count,
                          times.ToString());
        }
        [Test]
        [TestCase(0, 0, 3)]
        [TestCase(0, Constants.COLUMN_NUMBER - 1, 3)]
        [TestCase(Constants.ROW_NUMBER - 1, 0, 3)]
        [TestCase(Constants.ROW_NUMBER - 1, Constants.COLUMN_NUMBER - 1, 3)]
        [TestCase(0, 4, 5)]
        [TestCase(4, 0, 5)]
        [TestCase(Constants.ROW_NUMBER - 1, 4, 5)]
        [TestCase(4, Constants.COLUMN_NUMBER - 1, 5)]
        [TestCase(4, 4, 8)]
        public void AsEnumerableCorrectly(int i,
                                          int j,
                                          int count)
        {
            var sum = AroundService.AsEnumerable(i,
                                                 j,
                                                 delegate
                                                 {
                                                     return 1;
                                                 })
                                   .Sum();
            Assert.IsTrue(sum == count,
                          sum.ToString());
        }
    }
}