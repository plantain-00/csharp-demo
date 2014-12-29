using System.Collections.Generic;

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
            PlainText = new PlainText
                        {
                            Text = string.Empty
                        };
            do
            {
                PlainText.Text += Strings[Index];
                if (Strings[Index] == STRING.SLASH
                    && Strings[Index + 1] == STRING.SLASH)
                {
                    Index++;
                    PlainText.Text += Strings[Index];
                    Index++;
                    while (Index < Strings.Count
                           && (Strings[Index] != STRING.NEW_LINE || Strings[Index] != STRING.RETURN))
                    {
                        PlainText.Text += Strings[Index];
                        Index++;
                    }
                    if (Index < Strings.Count)
                    {
                        PlainText.Text += Strings[Index];
                        Index++;
                    }
                }
                else
                {
                    Index++;
                }
            }
            while (Index < Strings.Count
                   && Strings[Index] != STRING.LESS_THAN);
            PlainText.Depth = _depth;
        }
    }
}