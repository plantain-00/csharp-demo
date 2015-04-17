using System;
using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _1_TwoSum
    {
        public Tuple<int, int> TwoSum(int[] numbers, int target)
        {
            var dictionary = new Dictionary<int, int>();

            for (var i = 0; i < numbers.Length; i++)
            {
                var number = numbers[i];
                if (dictionary.ContainsKey(number))
                {
                    return Tuple.Create(dictionary[number] + 1, i + 1);
                }
                var theOther = target - number;
                if (!dictionary.ContainsKey(theOther))
                {
                    dictionary.Add(theOther, i);
                }
            }
            throw new Exception();
        }
    }
}