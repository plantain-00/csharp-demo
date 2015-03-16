using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SevenQueens
{
    internal class Program
    {
        /// <summary>
        ///     if queens is {2, 0, 3, 1},
        ///     then it means {(0,2), (1,0), (2,3), (3,1)}.
        ///     of course it is a valid result.
        /// </summary>
        /// <param name="queens"></param>
        /// <returns></returns>
        public static bool IsValid(int[] queens)
        {
            // the last point
            var px = queens.Length - 1;
            var py = queens[px];

            for (var i = 0; i < px; i++)
            {
                // |
                if (px == i)
                {
                    return false;
                }
                // -
                if (py == queens[i])
                {
                    return false;
                }
                var deltaX = px - i;
                var deltaY = py - queens[i];
                // \
                if (deltaX == deltaY)
                {
                    return false;
                }
                // /
                if (deltaX + deltaY == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private static void Xxx(ref List<int[]> result, int maxDepth, int depth, IList<int> queens = null)
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
                int[] newArray;
                if (queens == null)
                {
                    newArray = new[]
                               {
                                   i
                               };
                }
                else
                {
                    newArray = new int[queens.Count + 1];
                    for (var j = 0; j < queens.Count; j++)
                    {
                        newArray[j] = queens[j];
                    }
                    newArray[queens.Count] = i;
                }
                if (depth == 1)
                {
                    Xxx(ref result, maxDepth, depth + 1, newArray);
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
                    Xxx(ref result, maxDepth, depth + 1, newArray);
                }
            }
        }

        private static void Main(string[] args)
        {
            var counts = new List<int>();
            var watch = new Stopwatch();
            watch.Start();
            for (var i = 0; i < 12; i++)
            {
                var result = new List<int[]>();
                Xxx(ref result, i + 1, 1);
                counts.Add(result.Count);
            }
            watch.Stop();
            foreach (var count in counts)
            {
                Console.WriteLine(count);
            }
            Console.WriteLine(watch.ElapsedMilliseconds); //3.9-4.1
            Console.Read();
        }
    }
}