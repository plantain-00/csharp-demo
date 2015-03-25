namespace JsonConverter.Nodes
{
    public class JString : JToken
    {
        public JString()
        {
        }

        internal JString(Source source)
        {
            if (source.IsNot('"'))
            {
                throw new ParseException(source);
            }
            source.MoveForward();

            var startIndex = source.Index;
            source.MoveUntil(c => c == '"');
            Value = source.Substring(startIndex, source.Index - startIndex);

            source.MoveForward();
        }

        public string Value { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            return string.Format("\"{0}\"", Value);
        }
    }
}