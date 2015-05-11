using System;

namespace LeetCode.Solutions.Argorithms
{
    public class _204_CountPrimes
    {
        public int CountPrimes(int n)
        {
            if (n <= 2)
            {
                return 0;
            }
            var passed = new bool[n];
            var sum = 1;
            var upper = Math.Sqrt(n);
            for (var i = 3; i < n; i += 2)
            {
                if (!passed[i])
                {
                    sum++;
                    if (i > upper)
                    {
                        continue;
                    }
                    for (var j = i * i; j < n; j += i)
                    {
                        passed[j] = true;
                    }
                }
            }
            return sum;
        }
    }
}