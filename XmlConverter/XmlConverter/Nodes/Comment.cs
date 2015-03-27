namespace XmlConverter.Nodes
{
    public class Comment : Node
    {
        public const string COMMENT_START = "<!--";
        private const string COMMENT_END = "-->";

        public string Value { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            if (formatting == Formatting.None)
            {
                return string.Format("{0}{1}{2}", COMMENT_START, Value, COMMENT_END);
            }
            var spaces = new string(' ', Depth * spaceNumber);
            return string.Format("{3}{0}{1}{2}\n", COMMENT_START, Value, COMMENT_END, spaces);
        }

        internal static Comment Create(Source source, int depth)
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
                             Value = source.Substring(startIndex, source.Index - startIndex).Trim(),
                             Depth = depth
                         };
            source.MoveForward(COMMENT_END.Length);

            return result;
        }
    }
}