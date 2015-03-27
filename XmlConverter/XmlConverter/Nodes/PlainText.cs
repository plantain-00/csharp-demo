namespace XmlConverter.Nodes
{
    public class PlainText : Node
    {
        public string Text { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            return Text;
        }

        internal static PlainText Create(Source source)
        {
            if (source.Is('<'))
            {
                throw new ParseException(source);
            }

            var startIndex = source.Index;
            source.MoveUntil(c => c == '<');
            var result = new PlainText
                         {
                             Text = source.Substring(startIndex, source.Index - startIndex)
                         };
            return result;
        }
    }
}