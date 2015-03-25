namespace JsonConverter.Nodes
{
    public class JKey : JToken
    {
        public JKey()
        {
        }

        internal JKey(Source source)
        {
            if (source.IsNot('"'))
            {
                throw new ParseException(source);
            }
            source.MoveForward();

            var startIndex = source.Index;
            source.MoveUntil(c => c == '"');
            Key = source.Substring(startIndex, source.Index - startIndex);

            source.MoveForward();
        }

        public string Key { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            return string.Format("\"{0}\"", Key);
        }
    }
}