using System.Collections.Generic;
using System.Text;

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

            Properties.Add(new JProperty(source, Depth + 1));
            source.SkipWhiteSpace();

            while (source.Is(','))
            {
                source.MoveForward();
                source.SkipWhiteSpace();
                if (source.IsNot('"'))
                {
                    throw new ParseException(source);
                }

                Properties.Add(new JProperty(source, Depth + 1));
                source.SkipWhiteSpace();
            }

            if (source.IsNot('}'))
            {
                throw new ParseException(source);
            }
            source.MoveForward();
        }

        public IList<JProperty> Properties { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder("{");

            var count = Properties.Count;
            for (var i = 0; i < Properties.Count; i++)
            {
                builder.Append(Properties[i]);
                if (i != count - 1)
                {
                    builder.Append(",");
                }
            }

            builder.Append("}");

            return builder.ToString();
        }
    }
}