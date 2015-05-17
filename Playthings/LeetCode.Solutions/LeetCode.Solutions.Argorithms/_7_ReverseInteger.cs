namespace LeetCode.Solutions.Argorithms
{
    public class _7_ReverseInteger
    {
        public int Reverse(int x)
        {
            long result = 0;
            do
            {
                result = x % 10 + result * 10;
                x = x / 10;
            }
            while (x != 0);

            if (result > int.MaxValue
                || result < int.MinValue)
            {
                result = 0;
            }
            return (int) result;
        }
    }
}