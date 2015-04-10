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
                             Key = source.TakeUntilAny(" =")
                         };
            source.SkipIt();

            source.SkipBlankSpaces();

            if (source.Is('\''))
            {
                source.SkipIt();
                result.Value = source.TakeUntil('\'');
                source.SkipIt();
            }
            else if (source.Is('"'))
            {
                source.SkipIt();
                result.Value = source.TakeUntil('"');
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