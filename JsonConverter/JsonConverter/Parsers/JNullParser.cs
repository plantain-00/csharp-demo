using JsonConverter.Nodes;

namespace JsonConverter.Parsers
{
    internal class JNullParser : ParserBase
    {
        internal JNullParser(Source source, int depth) : base(source, depth)
        {
        }

        internal override void Parse()
        {
            if (Source.IsNot("null"))
            {
                throw new ParseException(Source);
            }
            Source.MoveForward("null".Length);
            Result = new JNull
                     {
                         Depth = Depth
                     };
        }
    }
}