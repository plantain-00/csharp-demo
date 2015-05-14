using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _119_PascalsTriangleII
    {
        public IList<int> GetRow(int rowIndex)
        {
            if (rowIndex < 0)
            {
                return new int[0];
            }
            var result = new int[rowIndex + 1];
            result[0] = 1;

            for (var i = 1; i <= rowIndex; i++)
            {
                for (var j = i; j >= 0; j--)
                {
                    if (j == i)
                    {
                        result[j] = 1;
                    }
                    else if (j != 0)
                    {
                        result[j] += result[j - 1];
                    }
                }
            }
            return result;
        }
    }
}