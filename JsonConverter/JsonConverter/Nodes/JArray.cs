using System.Collections.Generic;
using System.Text;

namespace JsonConverter.Nodes
{
    public class JArray : JObject
    {
        public JArray()
        {
        }

        internal JArray(Source source, int depth)
        {
            Depth = depth;
            Items = new List<JToken>();

            if (source.IsNot('['))
            {
                throw new ParseException(source);
            }
            source.MoveForward();
            source.SkipWhiteSpace();

            Items.Add(Convert(source, depth));
            source.SkipWhiteSpace();

            while (source.Is(','))
            {
                source.MoveForward();
                source.SkipWhiteSpace();
                Items.Add(Convert(source, depth));
                source.SkipWhiteSpace();
            }

            if (source.IsNot(']'))
            {
                throw new ParseException(source);
            }
            source.MoveForward();
        }

        public IList<JToken> Items { get; set; }
        public int Depth { get; set; }

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