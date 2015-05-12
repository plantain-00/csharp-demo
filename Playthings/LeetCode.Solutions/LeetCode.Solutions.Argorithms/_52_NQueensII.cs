using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _52_NQueensII
    {
        public int TotalNQueens(int n)
        {
            return Xxx(0, n, 1, new int[n], 0);
        }

        private static bool IsValid(IList<int> queens, int queensLength)
        {
            // the last point
            var px = queensLength - 1;
            var py = queens[px];

            for (var i = 0; i < px; i++)
            {
                // |
                if (px == i)
                {
                    return false;
                }
                // -
                if (py == queens[i])
                {
                    return false;
                }
                var deltaX = px - i;
                var deltaY = py - queens[i];
                // \
                if (deltaX == deltaY)
                {
                    return false;
                }
                // /
                if (deltaX + deltaY == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private static int Xxx(int result, int maxDepth, int depth, IList<int> queens, int queensLength)
        {
            if (maxDepth == 1)
            {
                return result + 1;
            }
            for (var i = 0; i < maxDepth; i++)
            {
                queens[queensLength] = i;
                if (depth == 1)
                {
                    result = Xxx(result, maxDepth, depth + 1, queens, queensLength + 1);
                    continue;
                }
                if (!IsValid(queens, queensLength + 1))
                {
                    continue;
                }
                if (depth == maxDepth)
                {
                    result++;
                }
                else
                {
                    result = Xxx(result, maxDepth, depth + 1, queens, queensLength + 1);
                }
            }
            return result;
        }
    }
}