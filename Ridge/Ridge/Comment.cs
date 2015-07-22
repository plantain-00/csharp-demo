using ParseLibrary;

namespace Ridge
{
    public class Comment : Node
    {
        internal const string COMMENT_START = "<!--";
        internal const string COMMENT_END = "-->";
        public string Text { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            if (formatting == Formatting.None)
            {
                return $"<!--{Text}-->";
            }
            return $"{new string(' ', Depth * spaceNumber)}<!--{Text}-->\n";
        }

        internal static Comment Create(Source source, Node parent, int depth)
        {
            source.Expect(COMMENT_START);
            source.Skip(COMMENT_START);

            var result = new Comment
                         {
                             Depth = depth,
                             Text = source.TakeUntil(COMMENT_END),
                             Parent = parent
                         };
            source.Skip(COMMENT_END);

            return result;
        }
    }
}