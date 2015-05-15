namespace LeetCode.Solutions.Argorithms
{
    public class _58_LengthofLastWord
    {
        public int LengthOfLastWord(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }

            s = s.TrimEnd();

            return s.Length - 1 - s.LastIndexOf(' ');
        }
    }
}