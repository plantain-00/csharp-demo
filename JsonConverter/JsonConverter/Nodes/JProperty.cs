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

            Key = new JKey(source);
            source.SkipWhiteSpace();
            source.Is(':');
            source.MoveForward();
            source.SkipWhiteSpace();

            if (source.Is('['))
            {
                Value = new JArray(source, depth + 1);
            }
            else if (source.Is('{'))
            {
                Value = new JObject(source, depth + 1);
            }
            else if (source.Is("true")
                     || source.Is("false"))
            {
                Value = new JBool(source);
            }
            else if (source.Is("null"))
            {
                Value = new JNull(source);
            }
            else if (source.Is('"'))
            {
                Value = new JString(source);
            }
            else
            {
                Value = new JNumber(source);
            }
        }

        public JKey Key { get; set; }
        public JToken Value { get; set; }
        public int Depth { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            if (formatting == Formatting.None)
            {
                return string.Format("{0}:{1}", Key.ToString(formatting), Value.ToString(formatting));
            }
            return string.Format("{0} : {1}", Key.ToString(formatting), Value.ToString(formatting));
        }
    }
}