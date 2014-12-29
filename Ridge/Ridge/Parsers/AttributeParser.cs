using System;
using System.Collections.Generic;

using Attribute = Ridge.Nodes.Attribute;

namespace Ridge.Parsers
{
    internal class AttributeParser : ParserBase
    {
        internal AttributeParser(IReadOnlyList<string> strings, int index) : base(strings, index)
        {
        }

        internal Attribute Attribute { get; private set; }

        internal override void Parse()
        {
            if (IsNotAName())
            {
                throw new Exception();
            }
            Attribute = new Attribute
                        {
                            Name = Strings[Index]
                        };
            Index++;

            GetTagValue();
        }

        private void GetTagValue()
        {
            var meetEndOfAttribute = false;
            do
            {
                if (IsSpaces())
                {
                    Index++;
                }
                else if (Strings[Index] == STRING.EQUAL)
                {
                    Index++;

                    SkipSpaces();

                    var stringParser = new StringParser(Strings, Index);
                    stringParser.Parse();

                    Index = stringParser.Index;
                    Attribute.Value = stringParser.String;
                }
                else
                {
                    meetEndOfAttribute = true;
                }
            }
            while (Index < Strings.Count
                   && !meetEndOfAttribute);
        }

        private bool IsNotAName()
        {
            return Strings[Index] == STRING.SPACE || Strings[Index] == STRING.RETURN || Strings[Index] == STRING.NEW_LINE || Strings[Index] == STRING.LESS_THAN || Strings[Index] == STRING.LARGER_THAN || Strings[Index] == STRING.SLASH;
        }
    }
}