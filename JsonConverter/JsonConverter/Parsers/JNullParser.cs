namespace JsonConverter.Parsers
{
    internal class JNullParser : ParserBase
    {
        internal JNullParser(Source source) : base(source)
        {
        }

        internal override void Parse()
        {
            if (Source.IsNot("null"))
            {
                throw new ParseException(Source);
            }
            Source.MoveForward("null".Length);
        }
    }
}