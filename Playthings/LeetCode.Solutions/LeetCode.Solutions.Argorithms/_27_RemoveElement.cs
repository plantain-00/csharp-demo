namespace LeetCode.Solutions.Argorithms
{
    public class _27_RemoveElement
    {
        public int RemoveElement(int[] nums, int val)
        {
            var result = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                if (nums[i] != val)
                {
                    nums[result] = nums[i];
                    result++;
                }
            }

            return result;
        }
    }
}