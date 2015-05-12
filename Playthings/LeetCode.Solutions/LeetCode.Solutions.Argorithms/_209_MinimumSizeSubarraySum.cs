using System;

namespace LeetCode.Solutions.Argorithms
{
    public class _209_MinimumSizeSubarraySum
    {
        public int MinSubArrayLen(int s, int[] nums)
        {
            if (nums.Length == 0)
            {
                return 0;
            }
            var first = 0;
            var second = 0;
            var min = nums.Length + 1;
            var sum = nums[0];
            while (first < nums.Length
                   && second <= first)
            {
                if (sum < s)
                {
                    first++;
                    if (first < nums.Length)
                    {
                        sum += nums[first];
                    }
                }
                else
                {
                    min = Math.Min(first - second + 1, min);
                    sum -= nums[second];
                    second++;
                }
            }

            if (min == nums.Length + 1)
            {
                return 0;
            }
            return min;
        }
    }
}