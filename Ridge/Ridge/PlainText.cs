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
            return $"{new string(' ', Depth * spaceNumber)}{Text.Trim(' ', '\n', '\r', '\t')}\n";
        }

        internal static PlainText Create(Source source, Node parent, int depth)
        {
            source.ExpectNot('<');
            var result = new PlainText
                         {
                             Depth = depth,
                             Text = source.TakeUntil('<').TrimEnd(),
                             Parent = parent
                         };
            return result;
        }
    }
}