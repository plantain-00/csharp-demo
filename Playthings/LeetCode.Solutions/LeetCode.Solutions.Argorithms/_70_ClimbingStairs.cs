namespace LeetCode.Solutions.Argorithms
{
    public class _70_ClimbingStairs
    {
        public int ClimbStairs(int n)
        {
            if (n <= 0)
            {
                return 0;
            }

            if (n == 1)
            {
                return 1;
            }

            if (n == 2)
            {
                return 2;
            }

            var steps = new int[n];
            steps[0] = 1;
            steps[1] = 2;

            for (var i = 2; i < n; i++)
            {
                steps[i] = steps[i - 1] + steps[i - 2];
            }

            return steps[n - 1];
        }
    }
}