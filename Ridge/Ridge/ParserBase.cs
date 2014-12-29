using System.Collections.Generic;

namespace Ridge
{
    internal abstract class ParserBase
    {
        protected ParserBase(IReadOnlyList<string> strings, int index)
        {
            Strings = strings;
            Index = index;
        }

        protected internal int Index { get; protected set; }
        protected internal IReadOnlyList<string> Strings { get; protected set; }
        internal abstract void Parse();
    }
}