using System.Text;

namespace LeetCode.Solutions.Argorithms
{
    public class _6_ZigZagConversion
    {
        public string Convert(string s, int numRows)
        {
            var result = new StringBuilder();
            var len = s.Length;
            if (numRows == 1)
            {
                return s;
            }
            for (var i = 0; i < numRows; i++)
            {
                var j = 0;
                while (j < len + i)
                {
                    if (i < numRows - 1
                        && i > 0
                        && j > i)
                    {
                        result.Append(s[j - i]);
                    }
                    if ((i + j) < len)
                    {
                        result.Append(s[j + i]);
                    }
                    j += 2 * numRows - 2;
                }
            }
            return result.ToString();
        }
    }
}