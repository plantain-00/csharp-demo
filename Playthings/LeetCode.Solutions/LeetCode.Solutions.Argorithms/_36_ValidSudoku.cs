using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _36_ValidSudoku
    {
        public bool IsValidSudoku(char[,] board)
        {
            for (var i = 0; i < 9; i++)
            {
                var list = new HashSet<char>();
                for (var j = 0; j < 9; j++)
                {
                    if (board[i, j] != '.')
                    {
                        if (list.Contains(board[i, j]))
                        {
                            return false;
                        }
                        list.Add(board[i, j]);
                    }
                }
            }

            for (var j = 0; j < 9; j++)
            {
                var list = new HashSet<char>();
                for (var i = 0; i < 9; i++)
                {
                    if (board[i, j] != '.')
                    {
                        if (list.Contains(board[i, j]))
                        {
                            return false;
                        }
                        list.Add(board[i, j]);
                    }
                }
            }

            for (var m = 0; m < 3; m++)
            {
                for (var n = 0; n < 3; n++)
                {
                    var list = new HashSet<char>();
                    for (var i = m * 3; i < m * 3 + 3; i++)
                    {
                        for (var j = n * 3; j < n * 3 + 3; j++)
                        {
                            if (board[i, j] != '.')
                            {
                                if (list.Contains(board[i, j]))
                                {
                                    return false;
                                }
                                list.Add(board[i, j]);
                            }
                        }
                    }
                }
            }

            return true;
        }
    }
}