using ParseLibrary;

namespace XmlConverter
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
            source.Expect(COMMENT_START);
            source.Skip(COMMENT_START);

            source.SkipWhiteSpace();

            var result = new Comment
                         {
                             Value = source.TakeUntil(c => source.Is(COMMENT_END)).Trim(),
                             Depth = depth
                         };
            source.Skip(COMMENT_END);

            return result;
        }
    }
}