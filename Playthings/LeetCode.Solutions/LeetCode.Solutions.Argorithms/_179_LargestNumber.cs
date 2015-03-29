using System;

namespace LeetCode.Solutions.Argorithms
{
    public class _179_LargestNumber
    {
        public string LargestNumber(int[] num)
        {
            if (Array.TrueForAll(num, x => x == 0))
            {
                return "0";
            }

            Array.Sort(num, Comparer);
            return string.Concat(num);
        }

        private static int Comparer(int a, int b)
        {
            var sa = b.ToString();
            var sb = a.ToString();
            return (sa + sb).CompareTo(sb + sa);
        }
    }
}