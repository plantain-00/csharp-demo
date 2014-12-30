using System;
using System.Collections.Generic;

using Ridge.Nodes;

namespace Ridge.Parsers
{
    internal class CommentParser : ParserBase
    {
        private readonly int _depth;

        internal CommentParser(IReadOnlyList<string> strings, int index, int depth, int endIndex) : base(strings, index, endIndex)
        {
            _depth = depth;
        }

        internal Comment Comment { get; private set; }

        internal override void Parse()
        {
            if (Strings[Index] != STRING.LESS_THAN
                || !Strings[Index + 1].StartsWith(STRING.COMMENT_START))
            {
                throw new Exception();
            }
            Comment = new Comment
                      {
                          Text = string.Empty,
                          Depth = _depth
                      };
            Index++;

            FindEndOfComment_GetComment();
        }

        private void FindEndOfComment_GetComment()
        {
            while (Index < EndIndex
                   && (!Strings[Index].EndsWith(STRING.COMMENT_END) || Strings[Index + 1] != STRING.LARGER_THAN))
            {
                Comment.Text += Strings[Index];
                Index++;
            }

            Comment.Text += Strings[Index];
            Index += 2;
        }
    }
}