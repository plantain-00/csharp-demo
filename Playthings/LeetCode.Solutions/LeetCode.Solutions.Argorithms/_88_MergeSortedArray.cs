namespace LeetCode.Solutions.Argorithms
{
    public class _88_MergeSortedArray
    {
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            while (n > 0)
            {
                if (m == 0
                    || nums2[n - 1] > nums1[m - 1])
                {
                    n--;
                    nums1[m + n] = nums2[n];
                }
                else
                {
                    m--;
                    nums1[m + n] = nums1[m];
                }
            }
        }
    }
}