using JsonConverter.Nodes;

namespace JsonConverter.Parsers
{
    internal class JKeyParser : ParserBase
    {
        public JKeyParser(Source source) : base(source)
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
            Result = new JKey
                     {
                         Key = Source.Substring(startIndex, Source.Index - startIndex)
                     };

            Source.MoveForward();
        }
    }
}