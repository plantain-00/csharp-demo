using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonConverter.Nodes
{
    public class JClass : JObject
    {
        public JClass()
        {
        }

        internal JClass(Source source, int depth)
        {
            Depth = depth;
            Properties = new List<JProperty>();

            if (source.IsNot('{'))
            {
                throw new ParseException(source);
            }
            source.MoveForward();
            source.SkipWhiteSpace();

            if (source.Is('}'))
            {
                source.MoveForward();
                return;
            }

            Properties.Add(new JProperty(source, depth));
            source.SkipWhiteSpace();

            while (source.Is(','))
            {
                source.MoveForward();
                source.SkipWhiteSpace();
                if (source.IsNot('"'))
                {
                    throw new ParseException(source);
                }

                Properties.Add(new JProperty(source, depth));
                source.SkipWhiteSpace();
            }

            if (source.IsNot('}'))
            {
                throw new ParseException(source);
            }
            source.MoveForward();
        }

        public IList<JProperty> Properties { get; set; }
        public int Depth { get; set; }

        public JObject this[int index]
        {
            get
            {
                return Properties[index].Value;
            }
            set
            {
                Properties[index].Value = value;
            }
        }

        public JObject this[string key]
        {
            get
            {
                return Properties.Single(p => p.Key == key).Value;
            }
            set
            {
                Properties.Single(p => p.Key == key).Value = value;
            }
        }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            if (formatting == Formatting.None)
            {
                var builder = new StringBuilder("{");

                for (var i = 0; i < Properties.Count; i++)
                {
                    builder.Append(Properties[i].ToString(formatting));
                    if (i != Properties.Count - 1)
                    {
                        builder.Append(",");
                    }
                }

                builder.Append("}");

                return builder.ToString();
            }
            else
            {
                var space = new string(' ', spaceNumber * (Depth - 1));
                var propertiesSpace = new string(' ', spaceNumber * Depth);

                var builder = new StringBuilder("{\n");
                builder.Append(propertiesSpace);

                for (var i = 0; i < Properties.Count; i++)
                {
                    builder.Append(Properties[i].ToString(formatting));
                    if (i != Properties.Count - 1)
                    {
                        builder.Append(",\n");
                        builder.Append(propertiesSpace);
                    }
                }

                builder.Append('\n');
                builder.Append(space);
                builder.Append("}");

                return builder.ToString();
            }
        }
    }
}