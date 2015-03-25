using System.Linq;

using JsonConverter.Nodes;

namespace JsonConverter.Parsers
{
    internal class JNumberParser : ParserBase
    {
        internal JNumberParser(Source source, int depth) : base(source, depth)
        {
        }

        internal override void Parse()
        {
            if ("\":[{".Any(c => Source.Is(c)))
            {
                throw new ParseException(Source);
            }

            var startIndex = Source.Index;
            Source.MoveUntil(c => "\",}]".Any(a => a == c));
            Result = new JNumber
                     {
                         RawNumber = Source.Substring(startIndex, Source.Index - startIndex),
                         Depth = Depth
                     };
        }
    }
}