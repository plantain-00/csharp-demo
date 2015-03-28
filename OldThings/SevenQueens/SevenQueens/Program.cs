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
        /// <param name="queensLength"></param>
        /// <returns></returns>
        public static bool IsValid(int[] queens, int queensLength)
        {
            // the last point
            var px = queensLength - 1;
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

        private static int Xxx(int result, int maxDepth, int depth, int[] queens, int queensLength)
        {
            if (maxDepth == 1)
            {
                return result + 1;
            }
            for (var i = 0; i < maxDepth; i++)
            {
                queens[queensLength] = i;
                if (depth == 1)
                {
                    result = Xxx(result, maxDepth, depth + 1, queens, queensLength + 1);
                    continue;
                }
                if (!IsValid(queens, queensLength + 1))
                {
                    continue;
                }
                if (depth == maxDepth)
                {
                    result++;
                }
                else
                {
                    result = Xxx(result, maxDepth, depth + 1, queens, queensLength + 1);
                }
            }
            return result;
        }

        private static void Main(string[] args)
        {
            var counts = new List<int>();
            var watch = new Stopwatch();
            watch.Start();
            for (var i = 0; i < 12; i++)
            {
                var result = Xxx(0, i + 1, 1, new int[i + 1], 0);
                counts.Add(result);
            }
            watch.Stop();
            foreach (var count in counts)
            {
                Console.WriteLine(count);
            }
            Console.WriteLine(watch.ElapsedMilliseconds); //release:1.01-1.15
            Console.Read();
        }
    }
}