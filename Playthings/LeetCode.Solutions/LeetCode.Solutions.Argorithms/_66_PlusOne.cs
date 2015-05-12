namespace LeetCode.Solutions.Argorithms
{
    public class _66_PlusOne
    {
        public int[] PlusOne(int[] digits)
        {
            for (var i = digits.Length - 1; i >= 0; i--)
            {
                if (digits[i] != 9)
                {
                    digits[i]++;
                    break;
                }
                digits[i] = 0;
            }

            if (digits[0] != 0)
            {
                return digits;
            }
            var result = new int[digits.Length + 1];
            result[0] = 1;
            for (var i = 0; i < digits.Length; i++)
            {
                result[i + 1] = digits[i];
            }
            return result;
        }
    }
}