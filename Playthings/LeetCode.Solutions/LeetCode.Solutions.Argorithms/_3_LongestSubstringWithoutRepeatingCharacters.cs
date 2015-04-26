using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _3_LongestSubstringWithoutRepeatingCharacters
    {
        public int LengthOfLongestSubstring(string s)
        {
            var dictionary = new Dictionary<char, int>();
            var max = 0;
            var availableFrom = 0;
            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];
                if (dictionary.ContainsKey(c))
                {
                    var last = dictionary[c] + 1;
                    if (last > availableFrom)
                    {
                        availableFrom = last;
                    }
                }
                var lastMax = i - availableFrom + 1;
                if (lastMax > max)
                {
                    max = lastMax;
                }
                dictionary[c] = i;
            }
            return max;
        }
    }
}