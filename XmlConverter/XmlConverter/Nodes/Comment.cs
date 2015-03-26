namespace XmlConverter.Nodes
{
    public class Comment : XmlBase
    {
        private const string COMMENT_START = "<!--";
        private const string COMMENT_END = "-->";

        public string Value { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            return string.Format("{0}{1}{2}", COMMENT_START, Value, COMMENT_END);
        }

        internal static Comment Create(Source source)
        {
            if (source.IsNot(COMMENT_START))
            {
                throw new ParseException(source);
            }
            source.MoveForward(COMMENT_START.Length);

            source.SkipWhiteSpace();

            var startIndex = source.Index;
            source.MoveUntil(c => source.Is(COMMENT_END));
            var result = new Comment
                         {
                             Value = source.Substring(startIndex, source.Index - startIndex).Trim()
                         };
            source.MoveForward(COMMENT_END.Length);

            return result;
        }
    }
}