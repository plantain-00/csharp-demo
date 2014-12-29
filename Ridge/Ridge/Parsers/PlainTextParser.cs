using System.Collections.Generic;
using System.Text;

using Ridge.Nodes;

namespace Ridge.Parsers
{
    internal class PlainTextParser : ParserBase
    {
        private readonly int _depth;

        internal PlainTextParser(IReadOnlyList<string> strings, int index, int depth) : base(strings, index)
        {
            _depth = depth;
        }

        internal PlainText PlainText { get; private set; }

        internal override void Parse()
        {
            PlainText = new PlainText();
            FindEndOfPlainText();

            PlainText.Depth = _depth;
        }

        private void FindEndOfPlainText()
        {
            var builder = new StringBuilder(string.Empty);

            builder.Append(Strings[Index]);

            if (Strings[Index] == STRING.SLASH
                && Strings[Index + 1] == STRING.SLASH)
            {
                ContainSingleLineComment(builder);
            }
            else
            {
                Index++;
            }

            while (Index < Strings.Count
                   && Strings[Index] != STRING.LESS_THAN)
            {
                builder.Append(Strings[Index]);

                if (Strings[Index] == STRING.SLASH
                    && Strings[Index + 1] == STRING.SLASH)
                {
                    ContainSingleLineComment(builder);
                }
                else
                {
                    Index++;
                }
            }

            PlainText.Text = builder.ToString();
        }

        private void ContainSingleLineComment(StringBuilder builder)
        {
            Index++;
            builder.Append(Strings[Index]);
            Index++;

            FindEndOfSingleLineComment(builder);

            if (Index < Strings.Count)
            {
                builder.Append(Strings[Index]);
                Index++;
            }
        }

        private void FindEndOfSingleLineComment(StringBuilder builder)
        {
            while (Index < Strings.Count
                   && (Strings[Index] != STRING.NEW_LINE && Strings[Index] != STRING.RETURN))
            {
                builder.Append(Strings[Index]);
                Index++;
            }
        }
    }
}