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
            switch (Strings[Index])
            {
                case STRING.SINGLE_QUOTE:
                    do
                    {
                        Index++;
                    }
                    while (Strings[Index] != STRING.SINGLE_QUOTE
                           && Index < Strings.Count);
                    break;
                case STRING.DOUBLE_QUOTE:
                    do
                    {
                        Index++;
                    }
                    while (Strings[Index] != STRING.DOUBLE_QUOTE
                           && Index < Strings.Count);
                    break;
                default:
                    throw new Exception();
            }
            Index++;
            String = String.Empty;
            for (var i = startIndex + 1; i < Index - 1; i++)
            {
                String += Strings[i];
            }
        }
    }
}