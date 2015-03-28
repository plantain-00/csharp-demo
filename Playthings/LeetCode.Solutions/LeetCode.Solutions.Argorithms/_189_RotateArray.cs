namespace LeetCode.Solutions.Argorithms
{
    public class _189_RotateArray
    {
        public void Rotate(int[] nums, int k)
        {
            if (nums == null
                || nums.Length == 0)
            {
                return;
            }
            if (k >= nums.Length)
            {
                k %= nums.Length;
            }
            if (k == 0)
            {
                return;
            }

            var nowIndex = 0;
            var tmp = nums[0];
            var loopIndex = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                nowIndex = (k + nowIndex) % nums.Length;

                //swap between tmp and nowIndex
                var tmp1 = tmp;
                tmp = nums[nowIndex];
                nums[nowIndex] = tmp1;

                if (nowIndex == loopIndex)
                {
                    loopIndex++;
                    nowIndex = loopIndex;
                    tmp = nums[nowIndex];
                }
            }
        }
    }
}