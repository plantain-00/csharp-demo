using System.Collections.Generic;

namespace JsonConverter.Nodes
{
    public class JObject : JToken
    {
        public JObject()
        {
        }

        internal JObject(Source source, int depth) : base(depth)
        {
            if (source.IsNot('{'))
            {
                throw new ParseException(source);
            }
            source.MoveForward();

            source.SkipWhiteSpace();

            Properties = new List<JProperty>();

            if (source.Is('}'))
            {
                source.MoveForward();
                return;
            }
            var property = new JProperty(source, Depth + 1);

            Properties.Add(property);
            source.SkipWhiteSpace();

            while (source.Is(','))
            {
                source.MoveForward();
                source.SkipWhiteSpace();
                if (source.IsNot('"'))
                {
                    throw new ParseException(source);
                }

                var newProperty = new JProperty(source, Depth + 1);
                Properties.Add(newProperty);
                source.SkipWhiteSpace();
            }

            if (source.IsNot('}'))
            {
                throw new ParseException(source);
            }
            source.MoveForward();
        }

        public IList<JProperty> Properties { get; set; }
    }
}