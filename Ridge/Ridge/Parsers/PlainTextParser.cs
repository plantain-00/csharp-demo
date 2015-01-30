using System.Collections.Generic;
using System.Text;

using Ridge.Nodes;

namespace Ridge.Parsers
{
    internal class PlainTextParser : ParserBase
    {
        private readonly int _depth;

        internal PlainTextParser(IReadOnlyList<string> strings, int index, int depth, int endIndex) : base(strings, index, endIndex)
        {
            _depth = depth;
        }

        internal PlainText PlainText { get; private set; }

        internal override void Parse()
        {
            var builder = new StringBuilder(string.Empty);

            do
            {
                builder.Append(Strings[Index]);

                Index++;
            }
            while (Index < EndIndex
                   && Strings[Index] != STRING.LESS_THAN);

            var text = builder.ToString();

            if (text.Trim(' ', '\r', '\n', '\t') != string.Empty)
            {
                PlainText = new PlainText
                            {
                                Text = text,
                                Depth = _depth
                            };
            }
        }
    }
}