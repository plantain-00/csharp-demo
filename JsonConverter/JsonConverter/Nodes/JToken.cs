namespace JsonConverter.Nodes
{
    public class JToken
    {
        public JToken()
        {
        }

        protected internal JToken(int depth)
        {
            Depth = depth;
        }

        public int Depth { get; set; }

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