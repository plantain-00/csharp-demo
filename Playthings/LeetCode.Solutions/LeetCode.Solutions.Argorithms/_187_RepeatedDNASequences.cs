using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _187_RepeatedDNASequences
    {
        public IList<string> FindRepeatedDnaSequences(string s)
        {
            if (s.Length <= 10)
            {
                return new string[0];
            }
            var result = new List<string>();
            var dictionary = new Dictionary<int, bool>();
            for (var i = 0; i < s.Length - 9; i++)
            {
                var tmp = ToInt(s, i);
                if (dictionary.ContainsKey(tmp))
                {
                    if (dictionary[tmp] == false)
                    {
                        dictionary[tmp] = true;
                        result.Add(s.Substring(i, 10));
                    }
                }
                else
                {
                    dictionary.Add(tmp, false);
                }
            }
            return result;
        }

        private static int ToInt(string s, int index)
        {
            var result = 0;
            for (var i = index; i < index + 10; i++)
            {
                int bit;
                switch (s[i])
                {
                    case 'A':
                        bit = 0;
                        break;
                    case 'C':
                        bit = 1;
                        break;
                    case 'G':
                        bit = 2;
                        break;
                    default:
                        bit = 3;
                        break;
                }
                result <<= 2;
                result |= bit;
            }
            return result;
        }
    }
}