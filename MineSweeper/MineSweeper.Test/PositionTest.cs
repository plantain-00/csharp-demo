using System.Windows.Media;

using MineSweeper.Interfaces;

using NUnit.Framework;

namespace MineSweeper.Test
{
    [TestFixture]
    public class PositionTest
    {
        [Test]
        public void PropertyChangedCorrectly()
        {
            var a = new Position();
            var times = 0;
            a.PropertyChanged += delegate
                                 {
                                     times++;
                                 };
            a.Foreground = Brushes.Blue;
            Assert.IsTrue(times == 1,
                          times.ToString());
            a.Text = "2";
            Assert.IsTrue(times == 2,
                          times.ToString());
            a.IsUndigged = false;
            Assert.IsTrue(times == 3,
                          times.ToString());
        }
        [Test]
        public void ThePositionConstructedCorrectly()
        {
            var a = new Position();
            Assert.IsTrue(a.Text == string.Empty,
                          a.Text);
            Assert.IsTrue(Equals(a.Foreground,
                                 Brushes.Black),
                          a.Foreground.ToString());
            Assert.IsTrue(a.IsUndigged);
        }
        [Test]
        [TestCase("", false)]
        [TestCase("1", true)]
        [TestCase(Constants.MINE_SYMBOL, false)]
        public void ThePositionIsANumber(string text,
                                         bool result)
        {
            var a = new Position
                    {
                        Text = text
                    };
            Assert.IsTrue(a.IsNumber() == result);
        }
    }
}