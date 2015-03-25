namespace JsonConverter.Nodes
{
    public class JNull : JToken
    {
        public JNull()
        {
        }

        internal JNull(Source source, int depth) : base(depth)
        {
            if (source.IsNot("null"))
            {
                throw new ParseException(source);
            }
            source.MoveForward("null".Length);
        }
    }
}