using System;
using System.Collections.Generic;

namespace MineSweeper.Interfaces
{
    internal class AroundService
    {
        internal static IEnumerable<T> AsEnumerable<T>(int i,
                                                       int j,
                                                       Func<int, int, T> func)
        {
            if (i - 1 >= 0)
            {
                if (j - 1 >= 0)
                {
                    yield return func(i - 1,
                                      j - 1);
                }
                yield return func(i - 1,
                                  j);
                if (j + 1 < Constants.COLUMN_NUMBER)
                {
                    yield return func(i - 1,
                                      j + 1);
                }
            }
            if (i + 1 < Constants.ROW_NUMBER)
            {
                if (j - 1 >= 0)
                {
                    yield return func(i + 1,
                                      j - 1);
                }
                yield return func(i + 1,
                                  j);
                if (j + 1 < Constants.COLUMN_NUMBER)
                {
                    yield return func(i + 1,
                                      j + 1);
                }
            }
            if (j - 1 >= 0)
            {
                yield return func(i,
                                  j - 1);
            }
            if (j + 1 < Constants.COLUMN_NUMBER)
            {
                yield return func(i,
                                  j + 1);
            }
        }
        internal static void Act(int i,
                                 int j,
                                 Action<int, int> action,
                                 Directions direction = Directions.None)
        {
            if (i - 1 >= 0)
            {
                if (j - 1 >= 0
                    && direction != Directions.LeftUp)
                {
                    action(i - 1,
                           j - 1);
                }
                if (direction != Directions.LeftUp
                    && direction != Directions.RightUp)
                {
                    action(i - 1,
                           j);
                }
                if (j + 1 < Constants.COLUMN_NUMBER
                    && direction != Directions.RightUp)
                {
                    action(i - 1,
                           j + 1);
                }
            }
            if (i + 1 < Constants.ROW_NUMBER)
            {
                if (j - 1 >= 0
                    && direction != Directions.LeftDown)
                {
                    action(i + 1,
                           j - 1);
                }
                if (direction != Directions.LeftDown
                    && direction != Directions.RightDown)
                {
                    action(i + 1,
                           j);
                }
                if (j + 1 < Constants.COLUMN_NUMBER
                    && direction != Directions.RightDown)
                {
                    action(i + 1,
                           j + 1);
                }
            }
            if (j - 1 >= 0
                && direction != Directions.LeftDown
                && direction != Directions.LeftUp)
            {
                action(i,
                       j - 1);
            }
            if (j + 1 < Constants.COLUMN_NUMBER
                && direction != Directions.RightDown
                && direction != Directions.RightUp)
            {
                action(i,
                       j + 1);
            }
        }
    }
}