using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _205_IsomorphicStrings
    {
        public bool IsIsomorphic(string s, string t)
        {
            var a = new Dictionary<char, char>();
            for (var i = 0; i < s.Length; i++)
            {
                if (a.ContainsKey(s[i]))
                {
                    if (a[s[i]] != t[i])
                    {
                        return false;
                    }
                }
                else
                {
                    if (a.ContainsValue(t[i]))
                    {
                        return false;
                    }
                    a.Add(s[i], t[i]);
                }
            }
            return true;
        }
    }
}