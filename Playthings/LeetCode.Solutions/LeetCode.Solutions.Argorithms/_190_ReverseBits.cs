namespace LeetCode.Solutions.Argorithms
{
    public class _190_ReverseBits
    {
        public uint ReverseBits(uint n)
        {
            uint result = 0;
            for (var i = 0; i < 32; i++)
            {
                result <<= 1;
                if ((n & 1) == 1)
                {
                    result++;
                }
                n >>= 1;
            }
            return result;
        }
    }
}