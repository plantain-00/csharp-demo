using System.Collections.Generic;
using System.Text;

using ParseLibrary;

namespace JsonConverter
{
    public sealed class JArray : JObject
    {
        public IList<JObject> Items { get; set; }
        public int Depth { get; set; }

        public JObject this[int index]
        {
            get
            {
                return Items[index];
            }
            set
            {
                Items[index] = value;
            }
        }

        internal static JArray Create(Source source, int depth)
        {
            var result = new JArray
                         {
                             Depth = depth,
                             Items = new List<JObject>()
                         };

            source.Expect('[');
            source.SkipIt();
            source.SkipBlankSpaces();

            if (source.Is(']'))
            {
                source.SkipIt();
                source.SkipBlankSpaces();
                return result;
            }

            result.Items.Add(CreateObject(source, depth));
            source.SkipBlankSpaces();

            while (source.Is(','))
            {
                source.SkipIt();
                source.SkipBlankSpaces();
                result.Items.Add(CreateObject(source, depth));
                source.SkipBlankSpaces();
            }

            source.Expect(']');
            source.SkipIt();

            return result;
        }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            if (formatting == Formatting.None)
            {
                var builder = new StringBuilder("[");

                for (var i = 0; i < Items.Count; i++)
                {
                    builder.Append(Items[i].ToString(formatting));
                    if (i != Items.Count - 1)
                    {
                        builder.Append(",");
                    }
                }

                builder.Append("]");

                return builder.ToString();
            }
            else
            {
                var space = new string(' ', spaceNumber * (Depth - 1));
                var itemsSpace = new string(' ', spaceNumber * Depth);

                var builder = new StringBuilder("[\n");
                builder.Append(itemsSpace);

                for (var i = 0; i < Items.Count; i++)
                {
                    builder.Append(Items[i].ToString(formatting));
                    if (i != Items.Count - 1)
                    {
                        builder.Append(",\n");
                        builder.Append(itemsSpace);
                    }
                }

                builder.Append('\n');
                builder.Append(space);
                builder.Append("]");

                return builder.ToString();
            }
        }
    }
}