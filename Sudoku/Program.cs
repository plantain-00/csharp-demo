using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sudoku
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var board = new SudokuBoard();
            board.Set(0, 0, 0, 0, 0, 2, 0, 1, 0, 9);
            board.Set(1, 9, 0, 0, 0, 0, 4, 0, 2, 5);
            board.Set(2, 0, 0, 2, 0, 0, 0, 3, 0, 0);
            board.Set(3, 0, 0, 7, 5, 0, 8, 0, 6, 0);
            board.Set(4, 0, 9, 0, 0, 4, 6, 0, 0, 7);
            board.Set(5, 0, 6, 0, 0, 0, 0, 0, 3, 0);
            board.Set(6, 4, 0, 9, 1, 0, 0, 0, 0, 0);
            board.Set(7, 0, 3, 1, 0, 0, 2, 4, 0, 0);
            board.Set(8, 2, 0, 0, 0, 0, 0, 0, 0, 3);

            //board.Set(0, 4, 0, 0, 2, 7, 0, 3, 5, 9);
            //board.Set(1, 7, 0, 0, 0, 0, 8, 1, 0, 0);
            //board.Set(2, 0, 0, 0, 0, 9, 6, 0, 0, 0);
            //board.Set(3, 0, 9, 0, 1, 8, 3, 0, 4, 0);
            //board.Set(4, 8, 0, 5, 6, 0, 7, 0, 0, 2);
            //board.Set(5, 0, 0, 4, 0, 5, 0, 6, 0, 8);
            //board.Set(6, 3, 8, 1, 0, 2, 9, 0, 0, 4);
            //board.Set(7, 9, 0, 6, 0, 1, 5, 0, 7, 3);
            //board.Set(8, 5, 0, 7, 3, 6, 4, 8, 0, 1);

            Print(board.Positions);

            var result = board.GetResult();

            foreach (var positionse in result)
            {
                Console.WriteLine();
                Print(positionse);
            }

            Console.Read();
        }

        public static void Print(Position[,] positions)
        {
            for (var i = 0; i < positions.GetLength(0); i++)
            {
                for (var j = 0; j < positions.GetLength(1); j++)
                {
                    Console.Write(positions[i, j].Value);
                }
                Console.Write("\n");
            }
        }
    }

    public class SudokuBoard
    {
        public Position[,] Positions = new Position[9, 9];

        public SudokuBoard()
        {
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    Positions[i, j] = new Position();
                }
            }
        }

        public bool IsValid(int x, int y, int value)
        {
            for (var i = 0; i < x; i++)
            {
                if (Positions[i, y].Value == value)
                {
                    return false;
                }
            }

            for (var i = x + 1; i < 9; i++)
            {
                if (Positions[i, y].IsFixed
                    && Positions[i, y].Value == value)
                {
                    return false;
                }
            }

            for (var j = 0; j < y; j++)
            {
                if (Positions[x, j].Value == value)
                {
                    return false;
                }
            }

            for (var j = y + 1; j < 9; j++)
            {
                if (Positions[x, j].IsFixed
                    && Positions[x, j].Value == value)
                {
                    return false;
                }
            }

            for (var i = x / 3 * 3; i < x / 3 * 3 + 3; i++)
            {
                for (var j = y / 3 * 3; j < y / 3 * 3 + 3; j++)
                {
                    if (j > y
                        || (j == y && i > x))
                    {
                        if (Positions[i, j].IsFixed
                            && Positions[i, j].Value == value)
                        {
                            return false;
                        }
                    }
                    else if (i == x
                             && j == y)
                    {
                    }
                    else
                    {
                        if (Positions[i, j].Value == value)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public Position[,] Copy()
        {
            var result = new Position[9, 9];
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    result[i, j] = new Position(Positions[i, j].Value);
                }
            }
            return result;
        }

        public void Do(List<Position[,]> result, int x, int y)
        {
            if (Positions[x, y].IsFixed)
            {
                if (x == 8
                    && y == 8)
                {
                    result.Add(Copy());
                }
                else if (x == 8)
                {
                    Do(result, 0, y + 1);
                }
                else
                {
                    Do(result, x + 1, y);
                }
            }
            else
            {
                for (var i = 1; i < 10; i++)
                {
                    if (IsValid(x, y, i))
                    {
                        Positions[x, y].Value = i;

                        if (x == 8
                            && y == 8)
                        {
                            result.Add(Copy());
                        }
                        else if (x == 8)
                        {
                            Do(result, 0, y + 1);
                        }
                        else
                        {
                            Do(result, x + 1, y);
                        }
                    }
                }
            }
        }

        public List<Position[,]> GetResult()
        {
            var result = new List<Position[,]>();

            Do(result, 0, 0);

            return result;
        }

        public void Set(int x, int y, int value)
        {
            Positions[x, y].Set(value);
        }

        public void Set(int x, params int[] values)
        {
            for (var i = 0; i < values.Length; i++)
            {
                if (values[i] != 0)
                {
                    Set(x, i, values[i]);
                }
            }
        }
    }

    public class Position
    {
        public Position(int value)
        {
            Set(value);
        }

        public Position()
        {
            IsFixed = false;
        }

        public bool IsFixed { get; set; }
        public int Value { get; set; }

        public void Set(int value)
        {
            IsFixed = true;
            Value = value;
        }
    }
}