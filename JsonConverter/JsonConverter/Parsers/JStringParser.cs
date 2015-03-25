using JsonConverter.Nodes;

namespace JsonConverter.Parsers
{
    internal class JStringParser : ParserBase
    {
        internal JStringParser(Source source, int depth) : base(source, depth)
        {
        }

        internal override void Parse()
        {
            if (Source.IsNot('"'))
            {
                throw new ParseException(Source);
            }
            Source.MoveForward();

            var startIndex = Source.Index;
            Source.MoveUntil(c => c == '"');
            Result = new JString
                     {
                         Value = Source.Substring(startIndex, Source.Index - startIndex),
                         Depth = Depth
                     };

            Source.MoveForward();
        }
    }
}