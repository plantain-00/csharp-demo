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
                var result = new JObject(source, 0);
                if (!source.IsTail)
                {
                    throw new ParseException(source);
                }
                return result;
            }
            throw new ParseException(source);
        }
    }
}