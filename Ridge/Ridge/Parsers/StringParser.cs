using System;
using System.Collections.Generic;

namespace Ridge.Parsers
{
    internal class StringParser : ParserBase
    {
        internal StringParser(IReadOnlyList<string> strings, int index) : base(strings, index)
        {
        }

        internal string String { get; private set; }

        internal override void Parse()
        {
            var startIndex = Index;

            if (Strings[Index] == STRING.SINGLE_QUOTE
                || Strings[Index] == STRING.DOUBLE_QUOTE)
            {
                FindNextQuote(Strings[Index]);
            }
            else
            {
                throw new Exception();
            }

            GetString(startIndex);

            Index++;
        }

        private void GetString(int startIndex)
        {
            String = String.Empty;
            for (var i = startIndex + 1; i < Index; i++)
            {
                String += Strings[i];
            }
        }

        private void FindNextQuote(string quote)
        {
            do
            {
                Index++;
            }
            while (Strings[Index] != quote
                   && Index < Strings.Count);
        }
    }
}