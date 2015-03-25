namespace JsonConverter.Parsers
{
    internal class JArrayParser : ParserBase
    {
        internal JArrayParser(Source source, int depth) : base(source, depth)
        {
        }

        internal override void Parse()
        {
            if (Source.IsNot('['))
            {
                throw new ParseException(Source);
            }
            Source.MoveForward();
        }
    }
}