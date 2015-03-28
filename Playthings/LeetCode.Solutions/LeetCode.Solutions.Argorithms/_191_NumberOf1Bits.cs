namespace LeetCode.Solutions.Argorithms
{
    public class _191_NumberOf1Bits
    {
        public int HammingWeight(uint n)
        {
            var result = 0;
            while (n != 0)
            {
                n = n & (n - 1);
                result++;
            }
            return result;
        }
    }
}