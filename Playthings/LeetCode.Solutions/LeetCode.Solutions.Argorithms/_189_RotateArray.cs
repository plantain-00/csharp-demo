namespace LeetCode.Solutions.Argorithms
{
    public class _189_RotateArray
    {
        public void Rotate(int[] nums, int k)
        {
            if (k >= nums.Length)
            {
                k %= nums.Length;
            }
            if (k == 0)
            {
                return;
            }
            var common = GetGreatestCommonDivisor(nums.Length, k);

            var loop = nums.Length / common;
            for (var i = 0; i < common; i++)
            {
                var tmp = nums[i];
                var index = i;
                for (var j = 0; j < loop; j++)
                {
                    if (j == loop - 1)
                    {
                        nums[index] = tmp;
                    }
                    else
                    {
                        var newIndex = GetLastIndex(index, nums.Length, k);
                        nums[index] = nums[newIndex];
                        index = newIndex;
                    }
                }
            }
        }

        private static int GetLastIndex(int index, int n, int k)
        {
            if (index < k)
            {
                return index + n - k;
            }
            return index - k;
        }

        private static int GetGreatestCommonDivisor(int a, int b)
        {
            int larger;
            int smaller;
            if (a == b)
            {
                return a;
            }
            if (a > b)
            {
                larger = a;
                smaller = b;
            }
            else
            {
                larger = b;
                smaller = a;
            }
            var divider = b;
            while (divider != 0)
            {
                divider = larger % smaller;
                larger = smaller;
                smaller = divider;
            }
            return larger;
        }
    }
}