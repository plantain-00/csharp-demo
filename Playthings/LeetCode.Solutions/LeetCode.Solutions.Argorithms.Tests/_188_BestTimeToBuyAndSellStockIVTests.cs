using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _188_BestTimeToBuyAndSellStockIVTests
    {
        [TestMethod]
        public void MaxProfitTest()
        {
            var array = new[]
                        {
                            1,
                            6,
                            2,
                            8,
                            3,
                            9
                        };

            Assert.IsTrue(new _188_BestTimeToBuyAndSellStockIV().MaxProfit(1, array) == 8);
            Assert.IsTrue(new _188_BestTimeToBuyAndSellStockIV().MaxProfit(2, array) == 13);
            Assert.IsTrue(new _188_BestTimeToBuyAndSellStockIV().MaxProfit(3, array) == 17); 
        }
    }
}