using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _217_ContainsDuplicate
    {
        public bool ContainsDuplicate(int[] nums)
        {
            var sets = new HashSet<int>();
            foreach (var num in nums)
            {
                if (sets.Contains(num))
                {
                    return true;
                }
                sets.Add(num);
            }
            return false;
        }
    }
}