using System.Collections.Generic;

using Ridge.Nodes;

namespace Ridge.Parsers
{
    internal class AttributesParser : ParserBase
    {
        internal AttributesParser(IReadOnlyList<string> strings, int index, int endIndex) : base(strings, index, endIndex)
        {
        }

        internal List<Attribute> Attributes { get; private set; }
        internal bool HasSlash { get; private set; }

        internal override void Parse()
        {
            var meetEndOfAllAttributes = false;
            do
            {
                SkipSpaces();
                if (Strings[Index] == STRING.SLASH
                    && Strings[Index + 1] == STRING.LARGER_THAN)
                {
                    meetEndOfAllAttributes = true;
                    Index += 2;
                    HasSlash = true;
                }
                else if (Strings[Index] == STRING.LARGER_THAN)
                {
                    meetEndOfAllAttributes = true;
                    Index++;
                }
                else
                {
                    ParseNextAttribute();
                }
            }
            while (!meetEndOfAllAttributes
                   && Index < EndIndex);
        }

        private void ParseNextAttribute()
        {
            var attributeParser = new AttributeParser(Strings, Index, EndIndex);
            attributeParser.Parse();
            if (Attributes == null)
            {
                Attributes = new List<Attribute>();
            }
            Attributes.Add(attributeParser.Attribute);
            Index = attributeParser.Index;
        }
    }
}