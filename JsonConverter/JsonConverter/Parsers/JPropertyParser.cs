using JsonConverter.Nodes;

namespace JsonConverter.Parsers
{
    internal class JPropertyParser : ParserBase
    {
        public JPropertyParser(Source source, int depth) : base(source, depth)
        {
        }

        internal override void Parse()
        {
            if (Source.IsNot('"'))
            {
                throw new ParseException(Source);
            }

            var keyParser = new JKeyParser(Source, Depth + 1);
            keyParser.Parse();
            Source.SkipWhiteSpace();
            Source.Is(':');
            Source.MoveForward();
            Source.SkipWhiteSpace();

            var result = new JProperty
                         {
                             Key = keyParser.Result as JKey,
                             Depth = Depth
                         };
            Result = result;

            if (Source.Is('['))
            {
                var arrayParser = new JArrayParser(Source, Depth + 1);
                arrayParser.Parse();

                result.Value = arrayParser.Result;
            }
            else if (Source.Is('{'))
            {
                var objectParser = new JObjectParser(Source, Depth + 1);
                objectParser.Parse();

                result.Value = objectParser.Result;
            }
            else if (Source.Is("true")
                     || Source.Is("false"))
            {
                var boolParser = new JBoolParser(Source, Depth + 1);
                boolParser.Parse();

                result.Value = boolParser.Result;
            }
            else if (Source.Is("null"))
            {
                var nullParser = new JNullParser(Source, Depth + 1);
                nullParser.Parse();

                result.Value = nullParser.Result;
            }
            else if (Source.Is('"'))
            {
                var stringParser = new JStringParser(Source, Depth + 1);
                stringParser.Parse();

                result.Value = stringParser.Result;
            }
            else
            {
                var numberParser = new JNumberParser(Source, Depth + 1);
                numberParser.Parse();

                result.Value = numberParser.Result;
            }
        }
    }
}