namespace LeetCode.Solutions.Argorithms
{
    public class _8_StringtoIntegeratoi
    {
        public int MyAtoi(string str)
        {
            if (str.Length == 0)
            {
                return 0;
            }
            var num = 0;
            var index = 0;
            var sign = 1;
            var signCount = 0;
            while (str[index] == ' '
                   && index < str.Length)
            {
                index++;
            }
            if (str[index] == '+')
            {
                signCount++;
                index++;
            }
            if (str[index] == '-')
            {
                signCount++;
                sign = -1;
                index++;
            }
            if (signCount >= 2)
            {
                return 0;
            }
            while (index < str.Length)
            {
                var ch = str[index];
                if (ch < '0'
                    || ch > '9')
                {
                    break;
                }
                if (int.MaxValue / 10 < num
                    || int.MaxValue / 10 == num && int.MaxValue % 10 < (ch - '0'))
                {
                    return sign == -1 ? int.MinValue : int.MaxValue;
                }
                num = num * 10 + (ch - '0');
                index++;
            }
            return sign * num;
        }
    }
}