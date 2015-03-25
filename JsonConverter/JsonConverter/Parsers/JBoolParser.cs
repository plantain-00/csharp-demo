using JsonConverter.Nodes;

namespace JsonConverter.Parsers
{
    internal class JBoolParser : ParserBase
    {
        internal JBoolParser(Source source, int depth) : base(source, depth)
        {
        }

        internal override void Parse()
        {
            if (Source.Is("true"))
            {
                Source.MoveForward("true".Length);

                Result = new JBool
                         {
                             Value = true,
                             Depth = Depth
                         };
            }
            else if (Source.Is("false"))
            {
                Source.MoveForward("false".Length);

                Result = new JBool
                         {
                             Value = false,
                             Depth = Depth
                         };
            }
            else
            {
                throw new ParseException(Source);
            }
        }
    }
}