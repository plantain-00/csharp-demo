using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _200_NumberOfIslands
    {
        public int NumIslands(char[][] grid)
        {
            if (grid == null
                || grid.Length == 0)
            {
                return 0;
            }
            var islands = 0;
            for (var i = 0; i < grid.Length; i++)
            {
                for (var j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        Explore(grid, i, j);
                        islands++;
                    }
                }
            }
            return islands;
        }

        private static bool IsValid(IList<char[]> grid, int i, int j)
        {
            return i < grid.Count && i >= 0 && j < grid[0].Length && j >= 0 && grid[i][j] == '1';
        }

        private static void Explore(IList<char[]> grid, int i, int j)
        {
            grid[i][j] = 'x';
            if (IsValid(grid, i, j - 1))
            {
                Explore(grid, i, j - 1);
            }
            if (IsValid(grid, i + 1, j))
            {
                Explore(grid, i + 1, j);
            }
            if (IsValid(grid, i - 1, j))
            {
                Explore(grid, i - 1, j);
            }
            if (IsValid(grid, i, j + 1))
            {
                Explore(grid, i, j + 1);
            }
        }
    }
}