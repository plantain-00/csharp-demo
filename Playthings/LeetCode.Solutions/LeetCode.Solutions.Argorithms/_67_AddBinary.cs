using System;

namespace LeetCode.Solutions.Argorithms
{
    public class _67_AddBinary
    {
        public string AddBinary(string a, string b)
        {
            var length = Math.Max(a.Length, b.Length) + 1;
            var result = new char[length];
            var over = 0;

            for (var i = 0; i < length; i++)
            {
                var ai = a.Length - 1 - i;
                var bi = b.Length - 1 - i;
                var resulti = result.Length - 1 - i;

                var tmp = over + (ai >= 0 ? a[ai] - '0' : 0) + (bi >= 0 ? b[bi] - '0' : 0);
                switch (tmp)
                {
                    case 0:
                        result[resulti] = '0';
                        over = 0;
                        break;
                    case 1:
                        result[resulti] = '1';
                        over = 0;
                        break;
                    case 2:
                        result[resulti] = '0';
                        over = 1;
                        break;
                    case 3:
                        result[resulti] = '1';
                        over = 1;
                        break;
                }
            }
            if (result[0] == '0')
            {
                return new string(result, 1, result.Length - 1);
            }
            return new string(result);
        }
    }
}