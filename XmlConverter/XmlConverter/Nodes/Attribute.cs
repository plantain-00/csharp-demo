using System.Linq;

namespace XmlConverter.Nodes
{
    public class Attribute : XmlBase
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            if (formatting == Formatting.None)
            {
                return string.Format("{0}:\"{1}\"", Key, Value);
            }
            return string.Format("{0} : \"{1}\"", Key, Value);
        }

        internal static Attribute Create(Source source)
        {
            if ("/>=?".Any(c => source.Is(c)))
            {
                throw new ParseException(source);
            }

            var startIndex = source.Index;
            source.MoveUntil(c => c == '=');

            var result = new Attribute
                         {
                             Key = source.Substring(startIndex, source.Index - startIndex).Trim()
                         };
            source.MoveForward();

            source.SkipWhiteSpace();

            if (source.Is('\''))
            {
                source.MoveForward();
                startIndex = source.Index;
                source.MoveUntil(c => c == '\'');
                result.Value = source.Substring(startIndex, source.Index - startIndex);
                source.MoveForward();
            }
            else if (source.Is('"'))
            {
                source.MoveForward();
                startIndex = source.Index;
                source.MoveUntil(c => c == '"');
                result.Value = source.Substring(startIndex, source.Index - startIndex);
                source.MoveForward();
            }
            else
            {
                throw new ParseException(source);
            }

            return result;
        }
    }
}