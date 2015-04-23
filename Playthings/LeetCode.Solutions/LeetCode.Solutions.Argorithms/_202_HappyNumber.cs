using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _202_HappyNumber
    {
        public bool IsHappy(int n)
        {
            var set = new List<int>
                      {
                          n
                      };
            while (n != 1)
            {
                var result = 0;
                while (n != 0)
                {
                    var tmp = n % 10;
                    result += tmp * tmp;
                    n /= 10;
                }
                if (set.Contains(result))
                {
                    return false;
                }
                set.Add(result);
                n = result;
            }
            return true;
        }
    }
}