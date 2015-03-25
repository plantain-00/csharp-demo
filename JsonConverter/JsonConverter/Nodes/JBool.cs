namespace JsonConverter.Nodes
{
    public class JBool : JObject
    {
        private const string TRUE_STRING = "true";
        private const string FALSE_STRING = "false";

        public JBool()
        {
        }

        internal JBool(Source source)
        {
            if (source.Is(TRUE_STRING))
            {
                source.MoveForward(TRUE_STRING.Length);

                Value = true;
            }
            else if (source.Is(FALSE_STRING))
            {
                source.MoveForward(FALSE_STRING.Length);

                Value = false;
            }
            else
            {
                throw new ParseException(source);
            }
        }

        public bool Value { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            return Value ? TRUE_STRING : FALSE_STRING;
        }
    }
}