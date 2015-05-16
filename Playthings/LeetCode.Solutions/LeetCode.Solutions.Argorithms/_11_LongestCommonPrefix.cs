using System.Linq;

namespace LeetCode.Solutions.Argorithms
{
    public class _11_LongestCommonPrefix
    {
        public string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 0)
            {
                return "";
            }

            var length = strs.Min(s => s.Length);

            for (var i = 0; i < length; i++)
            {
                for (var j = 1; j < strs.Length; j++)
                {
                    if (strs[j][i] != strs[0][i])
                    {
                        return strs[0].Substring(0, i);
                    }
                }
            }

            return strs[0].Substring(0, length);
        }
    }
}