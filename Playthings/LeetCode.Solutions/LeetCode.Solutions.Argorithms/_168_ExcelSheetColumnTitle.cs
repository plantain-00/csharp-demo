namespace LeetCode.Solutions.Argorithms
{
    public class _168_ExcelSheetColumnTitle
    {
        public string ConvertToTitle(int n)
        {
            var result = "";
            while (n != 0)
            {
                n--;
                result = (char)('A' + n % 26) + result;
                n /= 26;
            }
            return result;
        }
    }
}