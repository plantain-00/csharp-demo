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
            if ("/>=?".Any(c => source.Is(c)))
            {
                throw new ParseException(source);
            }

            var result = new Attribute
                         {
                             Key = source.TakeUntil(c => " =".Any(a => a == c))
                         };
            source.Skip();

            source.SkipWhiteSpace();

            if (source.Is('\''))
            {
                source.Skip();
                result.Value = source.TakeUntil(c => c == '\'');
                source.Skip();
            }
            else if (source.Is('"'))
            {
                source.Skip();
                result.Value = source.TakeUntil(c => c == '"');
                source.Skip();
            }
            else
            {
                throw new ParseException(source);
            }

            return result;
        }
    }
}