namespace LeetCode.Solutions.Argorithms
{
    public class _171_ExcelSheetColumnNumber
    {
        public int TitleToNumber(string s)
        {
            var colNumber = 0;

            foreach (var c in s)
            {
                colNumber = colNumber * 26 + (c - 64);
            }

            return colNumber;
        }
    }
}