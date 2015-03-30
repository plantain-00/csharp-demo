using System;

namespace LeetCode.Solutions.Argorithms
{
    public class _165_CompareVersionNumbers
    {
        public int CompareVersion(string version1, string version2)
        {
            var v1 = version1.Split('.');
            var v2 = version2.Split('.');

            var longest = v1.Length > v2.Length ? v1.Length : v2.Length;

            for (var i = 0; i < longest; i++)
            {
                var ver1 = i < v1.Length ? Convert.ToInt32(v1[i]) : 0;
                var ver2 = i < v2.Length ? Convert.ToInt32(v2[i]) : 0;

                if (ver1 > ver2)
                {
                    return 1;
                }
                if (ver1 < ver2)
                {
                    return -1;
                }
            }
            return 0;
        }
    }
}