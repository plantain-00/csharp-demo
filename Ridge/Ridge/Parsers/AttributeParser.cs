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
            if (Strings[Index] == STRING.SPACE
                || Strings[Index] == STRING.RETURN
                || Strings[Index] == STRING.NEW_LINE
                || Strings[Index] == STRING.LESS_THAN
                || Strings[Index] == STRING.LARGER_THAN
                || Strings[Index] == STRING.SLASH)
            {
                throw new Exception();
            }
            Attribute = new Attribute
                        {
                            Name = Strings[Index]
                        };
            Index++;

            bool willContinue;
            do
            {
                willContinue = false;
                switch (Strings[Index])
                {
                    case STRING.SPACE:
                    case STRING.RETURN:
                    case STRING.NEW_LINE:
                        willContinue = true;
                        Index++;
                        break;
                    case STRING.EQUAL:
                        Index++;
                        while (Index < Strings.Count
                               && (Strings[Index] == STRING.SPACE || Strings[Index] == STRING.RETURN || Strings[Index] == STRING.NEW_LINE))
                        {
                            Index++;
                        }
                        var stringParser = new StringParser(Strings, Index);
                        stringParser.Parse();

                        Index = stringParser.Index;
                        Attribute.Value = stringParser.String;
                        return;
                }
            }
            while (willContinue && Index < Strings.Count);
        }
    }
}