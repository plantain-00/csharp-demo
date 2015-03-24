namespace JsonConverter.Parsers
{
    internal class JObjectParser : ParserBase
    {
        internal JObjectParser(Source source) : base(source)
        {
        }

        internal override void Parse()
        {
            if (Source.IsNot('{'))
            {
                throw new ParseException(Source);
            }
        }
    }
}