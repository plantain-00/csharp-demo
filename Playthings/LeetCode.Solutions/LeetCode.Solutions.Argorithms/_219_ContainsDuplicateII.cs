using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _219_ContainsDuplicateII
    {
        public bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            var set = new HashSet<int>();

            for (var i = 0; i < nums.Length && i <= k; i++)
            {
                if (!set.Add(nums[i]))
                {
                    return true;
                }
            }

            for (var i = k + 1; i < nums.Length; i++)
            {
                set.Remove(nums[i - k - 1]);
                if (!set.Add(nums[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}