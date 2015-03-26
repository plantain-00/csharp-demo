namespace JsonConverter.Nodes
{
    public sealed class JString : JObject
    {
        public string Value { get; set; }

        internal static JString Create(Source source)
        {
            if (source.IsNot('"'))
            {
                throw new ParseException(source);
            }
            source.MoveForward();

            var startIndex = source.Index;
            source.MoveUntil(c => c == '"');
            var result = new JString
                         {
                             Value = source.Substring(startIndex, source.Index - startIndex)
                         };

            source.MoveForward();

            return result;
        }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            return string.Format("\"{0}\"", Value);
        }
    }
}