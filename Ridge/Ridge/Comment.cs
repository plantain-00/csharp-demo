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
                return string.Format("<!--{0}-->", Text);
            }
            return string.Format("{0}<!--{1}-->\n", new string(' ', Depth * spaceNumber), Text);
        }

        internal static Comment Create(Source source, int depth)
        {
            source.Expect(COMMENT_START);
            source.Skip(COMMENT_START);

            var result = new Comment
                         {
                             Depth = depth,
                             Text = source.TakeUntil(COMMENT_END)
                         };
            source.Skip(COMMENT_END);

            return result;
        }
    }
}