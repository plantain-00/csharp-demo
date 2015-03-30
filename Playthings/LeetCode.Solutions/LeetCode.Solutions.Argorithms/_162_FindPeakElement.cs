namespace LeetCode.Solutions.Argorithms
{
    public class _162_FindPeakElement
    {
        public int FindPeakElement(int[] num)
        {
            if (num.Length <= 1)
            {
                return 0;
            }
            var low = 0;
            var high = num.Length - 1;

            while (low < high)
            {
                var mid = (low + high) / 2;
                if (num[mid] > num[mid + 1])
                {
                    high = mid;
                }
                else if (num[mid] < num[mid + 1])
                {
                    low = mid + 1;
                }
            }

            return low;
        }
    }
}