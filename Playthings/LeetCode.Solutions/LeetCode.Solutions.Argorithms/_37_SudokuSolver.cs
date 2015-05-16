namespace LeetCode.Solutions.Argorithms
{
    public class _37_SudokuSolver
    {
        public void SolveSudoku(char[,] board)
        {
            if (board == null
                || board.Length == 0)
            {
                return;
            }
            Solve(board);
        }

        private static bool Solve(char[,] board)
        {
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    if (board[i, j] == '.')
                    {
                        for (var c = '1'; c <= '9'; c++)
                        {
                            if (IsValid(board, i, j, c))
                            {
                                board[i, j] = c;

                                if (Solve(board))
                                {
                                    return true;
                                }
                                board[i, j] = '.';
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool IsValid(char[,] board, int i, int j, char c)
        {
            for (var m = 0; m < 9; m++)
            {
                if (board[m, j] == c)
                {
                    return false;
                }
            }

            for (var n = 0; n < 9; n++)
            {
                if (board[i, n] == c)
                {
                    return false;
                }
            }

            for (var m = (i / 3) * 3; m < (i / 3) * 3 + 3; m++)
            {
                for (var n = (j / 3) * 3; n < (j / 3) * 3 + 3; n++)
                {
                    if (board[m, n] == c)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}