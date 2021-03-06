using System.Collections.Generic;
using System.Linq;
using System.Text;

using ParseLibrary;

namespace JsonConverter
{
    public sealed class JClass : JObject
    {
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

        internal static JClass Create(Source source, int depth)
        {
            var result = new JClass
                         {
                             Depth = depth,
                             Properties = new List<JProperty>()
                         };

            source.Expect('{');
            source.SkipIt();
            source.SkipBlankSpaces();

            if (source.Is('}'))
            {
                source.SkipIt();
                return result;
            }

            result.Properties.Add(JProperty.Create(source, depth));
            source.SkipBlankSpaces();

            while (source.Is(','))
            {
                source.SkipIt();
                source.SkipBlankSpaces();
                source.Expect('"');

                result.Properties.Add(JProperty.Create(source, depth));
                source.SkipBlankSpaces();
            }

            source.Expect('}');
            source.SkipIt();

            return result;
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