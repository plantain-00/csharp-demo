namespace LeetCode.Solutions.Argorithms
{
    public class _172_TrailingZeroes
    {
        public int TrailingZeroes(int n)
        {
            if (n < 5)
            {
                return 0;
            }
            return n / 5 + TrailingZeroes(n / 5);
        }
    }
}