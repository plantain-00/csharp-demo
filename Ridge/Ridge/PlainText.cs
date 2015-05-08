using ParseLibrary;

namespace Ridge
{
    public class PlainText : Node
    {
        public string Text { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            if (formatting == Formatting.None)
            {
                return Text.Trim(' ', '\n', '\r', '\t');
            }
            return string.Format("{0}{1}\n", new string(' ', Depth * spaceNumber), Text.Trim(' ', '\n', '\r', '\t'));
        }

        internal static PlainText Create(Source source, int depth)
        {
            source.ExpectNot('<');
            var result = new PlainText
                         {
                             Depth = depth,
                             Text = source.TakeUntil('<').TrimEnd()
                         };
            return result;
        }
    }
}