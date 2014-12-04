using MineSweeper.Interfaces;

using NUnit.Framework;

namespace MineSweeper.Test
{
    [TestFixture]
    internal class IocContainerTest
    {
        [Test]
        public void ResolveCorrectly()
        {
            Assert.IsTrue(IocContainer.Resolve<IResult>() != null);
            Assert.IsTrue(IocContainer.Resolve<IPosition>() != null);
            Assert.IsTrue(IocContainer.Resolve<IContext>() != null);
        }
    }
}