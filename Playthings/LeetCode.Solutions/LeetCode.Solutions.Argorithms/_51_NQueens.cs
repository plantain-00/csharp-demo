using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _51_NQueens
    {
        public IList<string[]> SolveNQueens(int n)
        {
            var result = new List<string[]>();
            Xxx(result, n, 1, new int[n], 0);
            return result;
        }

        private static string[] Format(int n, IList<int> ints)
        {
            var s = new string[n];
            for (var i = 0; i < ints.Count; i++)
            {
                s[i] = GetString(n, ints[i]);
            }
            return s;
        }

        private static string GetString(int n, int i)
        {
            var tmp = new char[n];
            for (var j = 0; j < n; j++)
            {
                if (j == i)
                {
                    tmp[j] = 'Q';
                }
                else
                {
                    tmp[j] = '.';
                }
            }
            return new string(tmp);
        }

        private static void Xxx(ICollection<string[]> result, int maxDepth, int depth, int[] queens, int queensLength)
        {
            if (maxDepth == 1)
            {
                result.Add(Format(maxDepth, queens));
                return;
            }
            for (var i = 0; i < maxDepth; i++)
            {
                queens[queensLength] = i;
                if (depth == 1)
                {
                    Xxx(result, maxDepth, depth + 1, queens, queensLength + 1);
                    continue;
                }
                if (!IsValid(queens, queensLength + 1))
                {
                    continue;
                }
                if (depth == maxDepth)
                {
                    result.Add(Format(maxDepth, queens));
                }
                else
                {
                    Xxx(result, maxDepth, depth + 1, queens, queensLength + 1);
                }
            }
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
    }
}