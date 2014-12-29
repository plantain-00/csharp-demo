using System;
using System.Collections.Generic;
using System.Text;

using Ridge.Nodes;

namespace Ridge.Parsers
{
    internal class TagParser : ParserBase
    {
        private readonly int _depth;

        internal TagParser(IReadOnlyList<string> strings, int index, int depth) : base(strings, index)
        {
            _depth = depth;
        }

        internal Tag Tag { get; private set; }

        internal override void Parse()
        {
            if (Strings[Index] != STRING.LESS_THAN
                || Strings[Index + 1] == STRING.SLASH)
            {
                throw new Exception();
            }
            Index++;
            Tag = new Tag
                  {
                      Name = Strings[Index],
                      Depth = _depth
                  };

            Index++;
            ParseAttributes();

            if (!IsSingleLineTag())
            {
                var endIndex = FindEndMarkOfCurrentTag();

                SkipSpaces();

                if (Tag.Name.Equals("script", StringComparison.CurrentCultureIgnoreCase))
                {
                    var builder = new StringBuilder(string.Empty);
                    for (var i = Index; i < endIndex; i++)
                    {
                        builder.Append(Strings[i]);
                    }

                    Tag.Children.Add(new ScriptText
                                     {
                                         Text = builder.ToString()
                                     });
                    Index = endIndex + 4;
                }
                else
                {
                    while (Index < endIndex)
                    {
                        SkipSpaces();

                        if (Strings[Index] == STRING.LESS_THAN
                            && Strings[Index + 1].StartsWith(STRING.COMMENT_START))
                        {
                            ParseComment();
                        }
                        else if (Strings[Index] == STRING.LESS_THAN
                                 && Strings[Index + 1] != STRING.SLASH)
                        {
                            ParseTag();
                        }
                        else
                        {
                            ParsePlainText();
                        }

                        SkipSpaces();
                    }

                    Index = endIndex + 4;
                }
            }
        }

        private void ParsePlainText()
        {
            var plainTextParser = new PlainTextParser(Strings, Index, _depth + 1);
            plainTextParser.Parse();

            Tag.Children.Add(plainTextParser.PlainText);
            Index = plainTextParser.Index;
        }

        private void ParseTag()
        {
            var tagParser = new TagParser(Strings, Index, _depth + 1);
            tagParser.Parse();

            Tag.Children.Add(tagParser.Tag);
            Index = tagParser.Index;
        }

        private void ParseComment()
        {
            var commentParser = new CommentParser(Strings, Index, _depth + 1);
            commentParser.Parse();

            Tag.Children.Add(commentParser.Comment);
            Index = commentParser.Index;
        }

        private int FindEndMarkOfCurrentTag()
        {
            var endIndex = -1;
            var stackDepth = 0;
            for (var i = Index; i + 3 < Strings.Count; i++)
            {
                if (Strings[i] == STRING.LESS_THAN
                    && Strings[i + 1] == STRING.SLASH
                    && Strings[i + 2] == Tag.Name
                    && Strings[i + 3] == STRING.LARGER_THAN)
                {
                    if (stackDepth == 0)
                    {
                        endIndex = i;
                        break;
                    }
                    stackDepth--;
                }
                else if (Strings[i] == STRING.LESS_THAN
                         && Strings[i + 1] == Tag.Name)
                {
                    stackDepth++;
                }
            }

            if (endIndex != -1)
            {
                Tag.Children = new List<Node>();
            }
            else
            {
                throw new Exception();
            }
            return endIndex;
        }

        private bool IsSingleLineTag()
        {
            return Tag.Name.Equals("input", StringComparison.CurrentCultureIgnoreCase) || Tag.Name.Equals("meta", StringComparison.CurrentCultureIgnoreCase) || Tag.Name.Equals("link", StringComparison.CurrentCultureIgnoreCase) || Tag.Name.Equals("br", StringComparison.CurrentCultureIgnoreCase) || Tag.Name.Equals("hr", StringComparison.CurrentCultureIgnoreCase) || Tag.Name.Equals("img", StringComparison.CurrentCultureIgnoreCase);
        }

        private void ParseAttributes()
        {
            var attributesParser = new AttributesParser(Strings, Index);
            attributesParser.Parse();

            Tag.Attributes = attributesParser.Attributes;
            Tag.HasSlash = attributesParser.HasSlash;
            Index = attributesParser.Index;
        }
    }
}