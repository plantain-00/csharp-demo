namespace LeetCode.Solutions.Argorithms
{
    public class _201_BitwiseANDofNumbersRange
    {
        public int RangeBitwiseAnd(int m, int n)
        {
            if (m == 0)
            {
                return 0;
            }
            var moveFactor = 1;
            while (m != n)
            {
                m >>= 1;
                n >>= 1;
                moveFactor <<= 1;
            }
            return m * moveFactor;
        }
    }
}