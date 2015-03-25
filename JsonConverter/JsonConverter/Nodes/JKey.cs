namespace JsonConverter.Nodes
{
    public class JKey : JToken
    {
        public JKey()
        {
        }

        internal JKey(Source source, int depth) : base(depth)
        {
            if (source.IsNot('"'))
            {
                throw new ParseException(source);
            }
            source.MoveForward();

            var startIndex = source.Index;
            source.MoveUntil(c => c == '"');
            Key = source.Substring(startIndex, source.Index - startIndex);

            source.MoveForward();
        }

        public string Key { get; set; }
    }
}