using System.Windows.Media;

using MineSweeper.Interfaces;

using NUnit.Framework;

namespace MineSweeper.Test
{
    [TestFixture]
    internal class ResultTest
    {
        [Test]
        public void PropertyChangedCorrectly()
        {
            var a = new Result();
            var times = 0;
            a.PropertyChanged += delegate
                                 {
                                     times++;
                                 };
            a.Foreground = Brushes.Blue;
            Assert.IsTrue(times == 1,
                          times.ToString());
            a.IsOn = false;
            Assert.IsTrue(times == 2,
                          times.ToString());
            a.MineNumber--;
            Assert.IsTrue(times == 3,
                          times.ToString());
            a.TimePast = 1.ToString();
            Assert.IsTrue(times == 4,
                          times.ToString());
            a.UndiggedNumber--;
            Assert.IsTrue(times == 5,
                          times.ToString());
        }
        [Test]
        public void ResetTest()
        {
            var a = new Result
                    {
                        Foreground = Brushes.Blue,
                        IsOn = false,
                        MineNumber = 2,
                        TimePast = "3",
                        UndiggedNumber = 4
                    };
            a.Reset();
            Assert.IsTrue(a.IsOn);
            Assert.IsTrue(Equals(a.Foreground,
                                 Brushes.Black),
                          a.Foreground.ToString());
            Assert.IsTrue(a.MineNumber == Constants.TOTAL,
                          a.MineNumber.ToString());
            Assert.IsTrue(a.TimePast == string.Empty,
                          a.TimePast);
            Assert.IsTrue(a.UndiggedNumber == Constants.TOTAL_NUMBER,
                          a.UndiggedNumber.ToString());
        }
        [Test]
        public void TheResultIsConstructedCorrectly()
        {
            var a = new Result();
            Assert.IsTrue(a.IsOn);
            Assert.IsTrue(Equals(a.Foreground,
                                 Brushes.Black),
                          a.Foreground.ToString());
            Assert.IsTrue(a.MineNumber == Constants.TOTAL,
                          a.MineNumber.ToString());
            Assert.IsTrue(a.TimePast == string.Empty,
                          a.TimePast);
            Assert.IsTrue(a.UndiggedNumber == Constants.TOTAL_NUMBER,
                          a.UndiggedNumber.ToString());
        }
    }
}