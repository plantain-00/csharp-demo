using System.Collections.Generic;

using Ridge.Nodes;

namespace Ridge.Parsers
{
    internal class AttributesParser : ParserBase
    {
        internal AttributesParser(IReadOnlyList<string> strings, int index) : base(strings, index)
        {
        }

        internal List<Attribute> Attributes { get; private set; }
        internal bool HasSlash { get; private set; }

        internal override void Parse()
        {
            bool willContinue;
            do
            {
                while (Index < Strings.Count
                       && (Strings[Index] == STRING.SPACE || Strings[Index] == STRING.RETURN || Strings[Index] == STRING.NEW_LINE))
                {
                    Index++;
                }
                if (Strings[Index] == STRING.SLASH
                    && Strings[Index + 1] == STRING.LARGER_THAN)
                {
                    willContinue = false;
                    Index += 2;
                    HasSlash = true;
                }
                else if (Strings[Index] == STRING.LARGER_THAN)
                {
                    willContinue = false;
                    Index++;
                }
                else
                {
                    willContinue = true;
                    var attributeParser = new AttributeParser(Strings, Index);
                    attributeParser.Parse();
                    if (Attributes == null)
                    {
                        Attributes = new List<Attribute>();
                    }
                    Attributes.Add(attributeParser.Attribute);
                    Index = attributeParser.Index;
                }
            }
            while (willContinue && Index < Strings.Count);
        }
    }
}