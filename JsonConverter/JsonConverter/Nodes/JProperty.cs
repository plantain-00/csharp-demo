namespace JsonConverter.Nodes
{
    public class JProperty : JToken
    {
        public JProperty()
        {
        }

        internal JProperty(Source source, int depth) : base(depth)
        {
            if (source.IsNot('"'))
            {
                throw new ParseException(source);
            }

            Key = new JKey(source, Depth + 1);
            source.SkipWhiteSpace();
            source.Is(':');
            source.MoveForward();
            source.SkipWhiteSpace();

            if (source.Is('['))
            {
                Value = new JArray(source, Depth + 1);
            }
            else if (source.Is('{'))
            {
                Value = new JObject(source, Depth + 1);
            }
            else if (source.Is("true")
                     || source.Is("false"))
            {
                Value = new JBool(source, Depth + 1);
            }
            else if (source.Is("null"))
            {
                Value = new JNull(source, Depth + 1);
            }
            else if (source.Is('"'))
            {
                Value = new JString(source, Depth + 1);
            }
            else
            {
                Value = new JNumber(source, Depth + 1);
            }
        }

        public JKey Key { get; set; }
        public JToken Value { get; set; }
    }
}