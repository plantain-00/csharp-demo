namespace NewsCatcher
{
    public static class ParagraphHelper
    {
        public static string Paragraph(this string s, int maxLength)
        {
            var result = s;
            for (var i = 0; i < s.Length / maxLength; i++)
            {
                result = result.Insert(maxLength * (i + 1), "\n");
            }
            return result;
        }
    }
}