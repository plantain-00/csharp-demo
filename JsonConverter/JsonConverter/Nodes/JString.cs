namespace JsonConverter.Nodes
{
    public class JString : JToken
    {
        public JString()
        {
        }

        internal JString(Source source, int depth) : base(depth)
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
    }
}