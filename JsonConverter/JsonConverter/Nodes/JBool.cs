namespace JsonConverter.Nodes
{
    public class JBool : JToken
    {
        public JBool()
        {
        }

        internal JBool(Source source, int depth) : base(depth)
        {
            if (source.Is("true"))
            {
                source.MoveForward("true".Length);

                Value = true;
            }
            else if (source.Is("false"))
            {
                source.MoveForward("false".Length);

                Value = false;
            }
            else
            {
                throw new ParseException(source);
            }
        }

        public bool Value { get; set; }
    }
}