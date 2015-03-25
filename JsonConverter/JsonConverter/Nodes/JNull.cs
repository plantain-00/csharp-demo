namespace JsonConverter.Nodes
{
    public class JNull : JToken
    {
        private const string NULL_STRING = "null";

        public JNull()
        {
        }

        internal JNull(Source source, int depth) : base(depth)
        {
            if (source.IsNot(NULL_STRING))
            {
                throw new ParseException(source);
            }
            source.MoveForward(NULL_STRING.Length);
        }

        public override string ToString()
        {
            return NULL_STRING;
        }
    }
}