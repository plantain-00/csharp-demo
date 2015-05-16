namespace LeetCode.Solutions.Argorithms
{
    public class _9_PalindromeNumber
    {
        public bool IsPalindrome(int x)
        {
            if (x < 0
                || (x != 0 && x % 10 == 0))
            {
                return false;
            }
            var sum = 0;
            while (x > sum)
            {
                sum = sum * 10 + x % 10;
                x = x / 10;
            }
            return (x == sum) || (x == sum / 10);
        }
    }
}