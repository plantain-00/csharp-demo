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
                             Key = source.TakeUntil('"')
                         };

            source.SkipIt();

            source.SkipBlankSpaces();
            source.Expect(':');
            source.SkipIt();
            source.SkipBlankSpaces();

            result.Value = JObject.CreateObject(source, depth);

            return result;
        }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            if (formatting == Formatting.None)
            {
                return $"\"{Key}\":{Value.ToString(formatting)}";
            }
            return $"\"{Key}\" : {Value.ToString(formatting)}";
        }
    }
}