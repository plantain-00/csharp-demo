using System.Linq;

using ParseLibrary;

namespace XmlConverter
{
    public class Attribute : FormattingBase
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            return string.Format("{0}=\"{1}\"", Key, Value);
        }

        internal static Attribute Create(Source source)
        {
            if ("/>=?".Any(source.Is))
            {
                throw new ParseException(source);
            }

            var result = new Attribute
                         {
                             Key = source.TakeUntil(c => " =".Any(a => a == c))
                         };
            source.SkipIt();

            source.SkipWhiteSpace();

            if (source.Is('\''))
            {
                source.SkipIt();
                result.Value = source.TakeUntil(c => c == '\'');
                source.SkipIt();
            }
            else if (source.Is('"'))
            {
                source.SkipIt();
                result.Value = source.TakeUntil(c => c == '"');
                source.SkipIt();
            }
            else
            {
                throw new ParseException(source);
            }

            return result;
        }
    }
}