using System.Collections;
using System.Collections.Generic;

namespace SevenQueens
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var result = new List<Queen[]>();
            foreach (var queen0 in new ChessBoard(0, 0))
            {
                foreach (var queen1 in new ChessBoard(queen0.NextValidQueen()))
                {
                    if (!queen1.IsEnabled(queen0))
                    {
                        continue;
                    }
                    foreach (var queen2 in new ChessBoard(queen1.NextValidQueen()))
                    {
                        if (!queen2.IsEnabled(queen0, queen1))
                        {
                            continue;
                        }
                        foreach (var queen3 in new ChessBoard(queen2.NextValidQueen()))
                        {
                            if (!queen3.IsEnabled(queen0, queen1, queen2))
                            {
                                continue;
                            }
                            foreach (var queen4 in new ChessBoard(queen3.NextValidQueen()))
                            {
                                if (!queen4.IsEnabled(queen0, queen1, queen2, queen3))
                                {
                                    continue;
                                }
                                foreach (var queen5 in new ChessBoard(queen4.NextValidQueen()))
                                {
                                    if (!queen5.IsEnabled(queen0, queen1, queen2, queen3, queen4))
                                    {
                                        continue;
                                    }
                                    foreach (var queen6 in new ChessBoard(queen5.NextValidQueen()))
                                    {
                                        if (!queen6.IsEnabled(queen0, queen1, queen2, queen3, queen4, queen5))
                                        {
                                            continue;
                                        }
                                        foreach (var queen7 in new ChessBoard(queen6.NextValidQueen()))
                                        {
                                            if (!queen7.IsEnabled(queen0, queen1, queen2, queen3, queen4, queen5, queen6))
                                            {
                                                continue;
                                            }
                                            result.Add(new[]
                                                       {
                                                           queen0,
                                                           queen1,
                                                           queen2,
                                                           queen3,
                                                           queen4,
                                                           queen5,
                                                           queen6,
                                                           queen7
                                                       });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public class ChessBoard : IEnumerable<Queen>
    {
        private readonly bool _isValid;
        private readonly int _x;
        private readonly int _y;

        public ChessBoard(int x, int y)
        {
            _x = x;
            _y = y;
            _isValid = true;
        }

        public ChessBoard(Queen queen)
        {
            if (queen == null)
            {
                _isValid = false;
            }
            else
            {
                _x = queen.X;
                _y = queen.Y;
                _isValid = true;
            }
        }

        public IEnumerator<Queen> GetEnumerator()
        {
            if (_isValid)
            {
                for (var x = _x; x < 8; x++)
                {
                    yield return new Queen(x, _y);
                }
                for (var y = _y + 1; y < 8; y++)
                {
                    for (var x = 0; x < 8; x++)
                    {
                        yield return new Queen(x, y);
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Queen
    {
        public Queen(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public Queen NextValidQueen()
        {
            return Y != 7 ? new Queen(0, Y + 1) : null;
        }

        public bool IsEnabled(Queen queen)
        {
            if (X == queen.X)
            {
                return false;
            }
            if (Y == queen.Y)
            {
                return false;
            }
            if (X - queen.X == Y - queen.Y)
            {
                return false;
            }
            return X - queen.X + Y - queen.Y != 0;
        }

        public bool IsEnabled(params Queen[] queens)
        {
            foreach (var queen in queens)
            {
                if (!IsEnabled(queen))
                {
                    return false;
                }
            }
            return true;
        }
    }
}