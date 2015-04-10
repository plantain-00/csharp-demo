using System.Linq;

using ParseLibrary;

namespace Ridge.Nodes
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
            if (" \r\n</>".Any(c => source.Is(c)))
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
                source.MoveForward();

                source.SkipWhiteSpace();
                if ("</>".Any(c => source.Is(c)))
                {
                    throw new ParseException(source);
                }
                if (source.Is('\"'))
                {
                    source.MoveForward();
                    result.Value = source.TakeUntil(c => c == '\"');
                    source.MoveForward();
                }
                else if (source.Is('\''))
                {
                    source.MoveForward();
                    result.Value = source.TakeUntil(c => c == '\'');
                    source.MoveForward();
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