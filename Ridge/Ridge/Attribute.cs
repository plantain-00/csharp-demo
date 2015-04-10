using System.Linq;

using ParseLibrary;

namespace Ridge
{
    public class Attribute
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            if (Value == null)
            {
                return Name;
            }
            return string.Format("{0}=\"{1}\"", Name, Value);
        }

        internal static Attribute Create(Source source)
        {
            if (" \r\n</>".Any(source.Is))
            {
                throw new ParseException(source);
            }

            var result = new Attribute
                         {
                             Name = source.TakeUntil(c => " =>/".Contains(c))
                         };
            source.SkipWhiteSpace();

            if (source.Is('='))
            {
                source.SkipIt();

                source.SkipWhiteSpace();
                if ("</>".Any(source.Is))
                {
                    throw new ParseException(source);
                }
                if (source.Is('\"'))
                {
                    source.SkipIt();
                    result.Value = source.TakeUntil(c => c == '\"');
                    source.SkipIt();
                }
                else if (source.Is('\''))
                {
                    source.SkipIt();
                    result.Value = source.TakeUntil(c => c == '\'');
                    source.SkipIt();
                }
                else
                {
                    result.Value = source.TakeUntil(c => " \r\n</>".Contains(c));
                }
            }

            return result;
        }
    }
}