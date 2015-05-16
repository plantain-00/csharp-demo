using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _13_RomantoInteger
    {
        private static readonly Dictionary<char, int> _dictionary = new Dictionary<char, int>
                                                                    {
                                                                        {
                                                                            'I', 1
                                                                        },
                                                                        {
                                                                            'V', 5
                                                                        },
                                                                        {
                                                                            'X', 10
                                                                        },
                                                                        {
                                                                            'L', 50
                                                                        },
                                                                        {
                                                                            'C', 100
                                                                        },
                                                                        {
                                                                            'D', 500
                                                                        },
                                                                        {
                                                                            'M', 1000
                                                                        }
                                                                    };

        public int RomanToInt(string s)
        {
            var previousNum = _dictionary[s[0]];
            var result = _dictionary[s[0]];

            for (var i = 1; i < s.Length; i++)
            {
                if (previousNum < _dictionary[s[i]])
                {
                    result = result - (2 * previousNum);
                }

                previousNum = _dictionary[s[i]];
                result += previousNum;
            }
            return result;
        }
    }
}