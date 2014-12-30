﻿using System;
using System.Collections.Generic;

using Ridge.Nodes;

namespace Ridge.Parsers
{
    internal class DocTypeParser : ParserBase
    {
        internal DocTypeParser(IReadOnlyList<string> strings, int index, int endIndex) : base(strings, index, endIndex)
        {
        }

        internal DocType DocType { get; private set; }

        internal override void Parse()
        {
            if (Strings[Index] != STRING.LESS_THAN
                || !Strings[Index + 1].Equals(STRING.DOCTYPE, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception();
            }
            DocType = new DocType
                      {
                          Name = STRING.DOCTYPE,
                          Depth = 0,
                          Declaration = string.Empty
                      };
            Index += 2;

            FindEndOfDocType_GetDeclaration();
        }

        private void FindEndOfDocType_GetDeclaration()
        {
            while (Index < EndIndex
                   && Strings[Index] != STRING.LARGER_THAN)
            {
                DocType.Declaration += Strings[Index];
                Index++;
            }

            if (Index < EndIndex)
            {
                Index++;
            }
        }
    }
}