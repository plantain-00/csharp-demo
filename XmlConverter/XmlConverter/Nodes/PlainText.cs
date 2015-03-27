namespace XmlConverter.Nodes
{
    public class PlainText : Node
    {
        public string Text { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            if (formatting == Formatting.None)
            {
                return Text;
            }
            var spaces = new string(' ', Depth * spaceNumber);
            return spaces + Text + "\n";
        }

        internal static PlainText Create(Source source, int depth)
        {
            source.ExpectNot('<');

            var startIndex = source.Index;
            source.MoveUntil(c => c == '<');
            var result = new PlainText
                         {
                             Text = source.Substring(startIndex, source.Index - startIndex),
                             Depth = depth
                         };
            return result;
        }
    }
}