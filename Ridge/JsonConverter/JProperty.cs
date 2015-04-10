using ParseLibrary;

namespace JsonConverter
{
    public sealed class JProperty : FormattingBase
    {
        public string Key { get; set; }
        public JObject Value { get; set; }
        public int Depth { get; set; }

        internal static JProperty Create(Source source, int depth)
        {
            source.Expect('"');
            source.SkipIt();

            var result = new JProperty
                         {
                             Key = source.TakeUntil(c => c == '"')
                         };

            source.SkipIt();

            source.SkipWhiteSpace();
            source.Expect(':');
            source.SkipIt();
            source.SkipWhiteSpace();

            result.Value = JObject.CreateObject(source, depth);

            return result;
        }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            if (formatting == Formatting.None)
            {
                return string.Format("\"{0}\":{1}", Key, Value.ToString(formatting));
            }
            return string.Format("\"{0}\" : {1}", Key, Value.ToString(formatting));
        }
    }
}