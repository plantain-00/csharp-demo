using System;

namespace LeetCode.Solutions.Argorithms
{
    public class _188_BestTimeToBuyAndSellStockIV
    {
        public int MaxProfit(int k, int[] prices)
        {
            if (k >= prices.Length / 2)
            {
                var result = 0;
                for (var i = 1; i < prices.Length; i++)
                {
                    if (prices[i] > prices[i - 1])
                    {
                        result += prices[i] - prices[i - 1];
                    }
                }
                return result;
            }

            var t = new int[k + 1, prices.Length];
            for (var i = 1; i <= k; i++)
            {
                var tmpMax = -prices[0];
                for (var j = 1; j < prices.Length; j++)
                {
                    t[i, j] = Math.Max(t[i, j - 1], prices[j] + tmpMax);
                    tmpMax = Math.Max(tmpMax, t[i - 1, j - 1] - prices[j]);
                }
            }
            return t[k, prices.Length - 1];
        }
    }
}