using System.Linq;

namespace JsonConverter.Nodes
{
    public class JNumber : JToken
    {
        public JNumber()
        {
        }

        internal JNumber(Source source, int depth) : base(depth)
        {
            if ("\":[{".Any(c => source.Is(c)))
            {
                throw new ParseException(source);
            }

            var startIndex = source.Index;
            source.MoveUntil(c => "\",}]".Any(a => a == c));
            RawNumber = source.Substring(startIndex, source.Index - startIndex);
        }

        public string RawNumber { get; set; }

        public override string ToString()
        {
            return RawNumber;
        }
    }
}