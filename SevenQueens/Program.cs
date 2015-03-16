using System;
using System.Collections.Generic;
using System.Linq;

namespace SevenQueens
{
    internal class Program
    {
        public static bool IsValid(params int[] queens)
        {
            var queen1X = queens.Length - 1;
            var queen1Y = queens[queens.Length - 1];
            for (var i = 0; i < queens.Length - 1; i++)
            {
                var queen2X = i;
                var queen2Y = queens[i];
                if (queen1X == queen2X)
                {
                    return false;
                }
                if (queen1Y == queen2Y)
                {
                    return false;
                }
                if (queen1X - queen2X == queen1Y - queen2Y)
                {
                    return false;
                }
                if (queen1X - queen2X + queen1Y - queen2Y == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private static void Xxx(ICollection<int[]> result, int maxDepth, int depth, params int[] queens)
        {
            if (maxDepth == 1)
            {
                result.Add(new[]
                           {
                               0
                           });
                return;
            }
            for (var i = 0; i < maxDepth; i++)
            {
                var newArray = queens.Concat(new[]
                                             {
                                                 i
                                             }).ToArray();
                if (depth == 1)
                {
                    Xxx(result, maxDepth, depth + 1, newArray);
                    continue;
                }
                if (!IsValid(newArray))
                {
                    continue;
                }
                if (depth == maxDepth)
                {
                    result.Add(newArray);
                }
                else
                {
                    Xxx(result, maxDepth, depth + 1, newArray);
                }
            }
        }

        private static void Main(string[] args)
        {
            for (var i = 0; i < 12; i++)
            {
                var result = new List<int[]>();
                Xxx(result, i + 1, 1);
                Console.WriteLine(result.Count);
            }
            Console.Read();
        }
    }
}