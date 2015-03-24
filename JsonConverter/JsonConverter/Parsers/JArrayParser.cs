namespace JsonConverter.Parsers
{
    internal class JArrayParser : ParserBase
    {
        internal JArrayParser(Source source) : base(source)
        {
        }

        internal override void Parse()
        {
            if (Source.IsNot('['))
            {
                throw new ParseException(Source);
            }
        }
    }
}