namespace JsonConverter.Nodes
{
    public class JNull : JObject
    {
        private const string NULL_STRING = "null";

        public JNull()
        {
        }

        internal JNull(Source source)
        {
            if (source.IsNot(NULL_STRING))
            {
                throw new ParseException(source);
            }
            source.MoveForward(NULL_STRING.Length);
        }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            return NULL_STRING;
        }
    }
}