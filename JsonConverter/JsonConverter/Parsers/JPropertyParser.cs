using JsonConverter.Nodes;

namespace JsonConverter.Parsers
{
    internal class JPropertyParser : ParserBase
    {
        public JPropertyParser(Source source) : base(source)
        {
        }

        internal override void Parse()
        {
            if (Source.IsNot('"'))
            {
                throw new ParseException(Source);
            }

            var keyParser = new JKeyParser(Source);
            keyParser.Parse();
            Source.SkipWhiteSpace();
            Source.Is(':');
            Source.MoveForward();
            Source.SkipWhiteSpace();
            if (Source.Is('['))
            {
                var arrayParser = new JArrayParser(Source);
                arrayParser.Parse();

                Result = new JProperty
                         {
                             Key = keyParser.Result as JKey,
                             Value = arrayParser.Result
                         };
            }
            else if (Source.Is('{'))
            {
                var objectParser = new JObjectParser(Source);
                objectParser.Parse();

                Result = new JProperty
                         {
                             Key = keyParser.Result as JKey,
                             Value = objectParser.Result
                         };
            }
            else if (Source.Is("true")
                     || Source.Is("false"))
            {
                var boolParser = new JBoolParser(Source);
                boolParser.Parse();

                Result = new JProperty
                         {
                             Key = keyParser.Result as JKey,
                             Value = boolParser.Result
                         };
            }
            else if (Source.Is("null"))
            {
                var nullParser = new JNullParser(Source);
                nullParser.Parse();

                Result = new JProperty
                         {
                             Key = keyParser.Result as JKey,
                             Value = nullParser.Result
                         };
            }
            else if (Source.Is('"'))
            {
                var stringParser = new JStringParser(Source);
                stringParser.Parse();

                Result = new JProperty
                         {
                             Key = keyParser.Result as JKey,
                             Value = stringParser.Result
                         };
            }
            else
            {
                var numberParser = new JNumberParser(Source);
                numberParser.Parse();

                Result = new JProperty
                         {
                             Key = keyParser.Result as JKey,
                             Value = numberParser.Result
                         };
            }
        }
    }
}