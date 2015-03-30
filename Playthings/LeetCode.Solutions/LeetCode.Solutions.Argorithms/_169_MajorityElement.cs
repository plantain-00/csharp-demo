namespace LeetCode.Solutions.Argorithms
{
    public class _169_MajorityElement
    {
        public int MajorityElement(int[] num)
        {
            var count = 0;
            var result = 0;
            foreach (var n in num)
            {
                if (count == 0)
                {
                    result = n;
                }
                if (result == n)
                {
                    ++count;
                }
                else
                {
                    --count;
                }
            }
            return result;
        }
    }
}