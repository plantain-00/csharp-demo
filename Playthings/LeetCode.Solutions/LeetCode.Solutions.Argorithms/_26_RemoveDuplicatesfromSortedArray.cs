namespace LeetCode.Solutions.Argorithms
{
    public class _26_RemoveDuplicatesfromSortedArray
    {
        public int RemoveDuplicates(int[] nums)
        {
            if (nums.Length == 0)
            {
                return 0;
            }
            var j = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                if (nums[i] != nums[j])
                {
                    nums[++j] = nums[i];
                }
            }
            return ++j;
        }
    }
}