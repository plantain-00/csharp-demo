using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _118_PascalsTriangle
    {
        public IList<IList<int>> Generate(int numRows)
        {
            var result = new List<IList<int>>();

            for (var i = 0; i < numRows; i++)
            {
                var row = new int[i + 1];
                for (var j = 0; j < i + 1; j++)
                {
                    if (j == 0
                        || j == i)
                    {
                        row[j] = 1;
                    }
                    else
                    {
                        row[j] = result[result.Count - 1][j - 1] + result[result.Count - 1][j];
                    }
                }
                result.Add(row);
            }
            return result;
        }
    }
}