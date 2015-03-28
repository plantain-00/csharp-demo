﻿namespace LeetCode.Solutions.Argorithms
{
    public class _191_NumberOf1Bits
    {
        public int HammingWeight(uint n)
        {
            var result = 0;
            while (n != 0)
            {
                if ((n & 1) == 1)
                {
                    result++;
                }
                n >>= 1;
            }
            return result;
        }
    }
}