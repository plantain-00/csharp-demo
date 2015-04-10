using ParseLibrary;

namespace JsonConverter
{
    public sealed class JString : JObject
    {
        public string Value { get; set; }

        internal static JString Create(Source source)
        {
            source.Expect('"');
            source.SkipIt();

            var result = new JString
                         {
                             Value = source.TakeUntil(c => c == '"')
                         };

            source.SkipIt();

            return result;
        }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            return string.Format("\"{0}\"", Value);
        }
    }
}