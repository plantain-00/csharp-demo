namespace JsonConverter.Nodes
{
    public class JProperty : JToken
    {
        public JProperty()
        {
        }

        internal JProperty(Source source, int depth)
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

            source.SkipWhiteSpace();
            source.Is(':');
            source.MoveForward();
            source.SkipWhiteSpace();

            Value = JObject.Convert(source, depth);
        }

        public string Key { get; set; }
        public JObject Value { get; set; }
        public int Depth { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            if (formatting == Formatting.None)
            {
                return string.Format("\"{0}\":{1}", Key, Value.ToString(formatting));
            }
            return string.Format("\"{0}\" : {1}", Key, Value.ToString(formatting));
        }
    }
}