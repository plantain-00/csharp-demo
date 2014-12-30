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

            while (Index < Strings.Count
                   && Strings[Index] != STRING.LESS_THAN)
            {
                builder.Append(Strings[Index]);

                Index++;
            }

            PlainText.Text = builder.ToString();
        }
    }
}