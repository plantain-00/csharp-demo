using System.Collections.Generic;

using JsonConverter.Nodes;

namespace JsonConverter.Parsers
{
    internal class JObjectParser : ParserBase
    {
        internal JObjectParser(Source source, int depth) : base(source, depth)
        {
        }

        internal override void Parse()
        {
            if (Source.IsNot('{'))
            {
                throw new ParseException(Source);
            }
            Source.MoveForward();

            Source.SkipWhiteSpace();

            var result = new JObject
                         {
                             Properties = new List<JProperty>(),
                             Depth = Depth
                         };
            Result = result;

            if (Source.Is('}'))
            {
                Source.MoveForward();
                return;
            }
            var propertyParser = new JPropertyParser(Source, Depth + 1);

            propertyParser.Parse();
            result.Properties.Add(propertyParser.Result as JProperty);
            Source.SkipWhiteSpace();

            while (Source.Is(','))
            {
                Source.MoveForward();
                Source.SkipWhiteSpace();
                if (Source.IsNot('"'))
                {
                    throw new ParseException(Source);
                }

                propertyParser.Parse();
                result.Properties.Add(propertyParser.Result as JProperty);
                Source.SkipWhiteSpace();
            }

            if (Source.IsNot('}'))
            {
                throw new ParseException(Source);
            }
            Source.MoveForward();
        }
    }
}