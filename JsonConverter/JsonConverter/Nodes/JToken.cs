namespace JsonConverter.Nodes
{
    public abstract class JToken
    {
        public abstract string ToString(Formatting formatting, int spaceNumber = 4);

        public override string ToString()
        {
            return ToString(Formatting.None);
        }

        public static JToken Convert(string s)
        {
            var source = new Source(s);

            source.SkipWhiteSpace();
            if (source.Is('{'))
            {
                return new JObject(source, 0);
            }
            throw new ParseException(source);
        }
    }
}