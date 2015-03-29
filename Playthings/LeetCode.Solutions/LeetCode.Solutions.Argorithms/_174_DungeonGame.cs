using System;

namespace LeetCode.Solutions.Argorithms
{
    public class _174_DungeonGame
    {
        public int CalculateMinimumHP(int[,] dungeon)
        {
            var x = dungeon.GetLength(0) - 1;
            var y = dungeon.GetLength(1) - 1;
            var dp = new int[x + 1, y + 1];

            dp[x, y] = Math.Max(1, 1 - dungeon[x, y]);
            for (var i = x - 1; i >= 0; i--)
            {
                dp[i, y] = Math.Max(1, dp[i + 1, y] - dungeon[i, y]);
            }

            for (var j = y - 1; j >= 0; j--)
            {
                for (var i = x; i >= 0; i--)
                {
                    if (i == x)
                    {
                        dp[i, j] = Math.Max(1, dp[i, j + 1] - dungeon[i, j]);
                    }
                    else
                    {
                        var min = Math.Min(dp[i, j + 1] - dungeon[i, j], dp[i + 1, j] - dungeon[i, j]);
                        dp[i, j] = Math.Max(1, min);
                    }
                }
            }

            return dp[0, 0];
        }
    }
}