using System.Text;

namespace LeetCode.Solutions.Argorithms
{
    public class _38_CountandSay
    {
        public string CountAndSay(int n)
        {
            var curr = new StringBuilder("1");
            for (var i = 1; i < n; i++)
            {
                var prev = curr;
                curr = new StringBuilder();
                var count = 1;
                var say = prev[0];

                for (int j = 1, len = prev.Length; j < len; j++)
                {
                    if (prev[j] != say)
                    {
                        curr.Append(count).Append(say);
                        count = 1;
                        say = prev[j];
                    }
                    else
                    {
                        count++;
                    }
                }
                curr.Append(count).Append(say);
            }
            return curr.ToString();
        }
    }
}