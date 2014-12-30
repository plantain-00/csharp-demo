using System;
using System.Collections.Generic;
using System.Text;

using Ridge.Nodes;

namespace Ridge.Parsers
{
    internal class TagParser : ParserBase
    {
        private readonly int _depth;

        internal TagParser(IReadOnlyList<string> strings, int index, int depth, int endIndex) : base(strings, index, endIndex)
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
            //Console.WriteLine(Index);
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

                    var text = builder.ToString();
                    if (text.Trim(' ', '\r', '\n', '\t') != string.Empty)
                    {
                        Tag.Children.Add(new ScriptText
                                         {
                                             Text = builder.ToString(),
                                             Depth = _depth + 1
                                         });
                    }

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
                            ParseComment(endIndex);
                        }
                        else if (Strings[Index] == STRING.LESS_THAN
                                 && Strings[Index + 1] != STRING.SLASH)
                        {
                            ParseTag(endIndex);
                        }
                        else
                        {
                            ParsePlainText(endIndex);
                        }

                        SkipSpaces();
                    }

                    Index = endIndex + 4;
                }
            }
        }

        private void ParsePlainText(int endIndex)
        {
            var plainTextParser = new PlainTextParser(Strings, Index, _depth + 1, endIndex);
            plainTextParser.Parse();

            if (plainTextParser.PlainText != null)
            {
                Tag.Children.Add(plainTextParser.PlainText);
            }
            
            Index = plainTextParser.Index;
        }

        private void ParseTag(int endIndex)
        {
            var tagParser = new TagParser(Strings, Index, _depth + 1, endIndex);
            tagParser.Parse();

            Tag.Children.Add(tagParser.Tag);
            Index = tagParser.Index;
        }

        private void ParseComment(int endIndex)
        {
            var commentParser = new CommentParser(Strings, Index, _depth + 1, endIndex);
            commentParser.Parse();

            Tag.Children.Add(commentParser.Comment);
            Index = commentParser.Index;
        }

        private int FindEndMarkOfCurrentTag()
        {
            var endIndex = EndIndex;
            var stackDepth = 0;
            for (var i = Index; i + 3 < EndIndex; i++)
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

            Tag.Children = new List<Node>();
            return endIndex;
        }

        private bool IsSingleLineTag()
        {
            return Tag.Name.Equals("input", StringComparison.CurrentCultureIgnoreCase) || Tag.Name.Equals("meta", StringComparison.CurrentCultureIgnoreCase) || Tag.Name.Equals("link", StringComparison.CurrentCultureIgnoreCase) || Tag.Name.Equals("br", StringComparison.CurrentCultureIgnoreCase) || Tag.Name.Equals("hr", StringComparison.CurrentCultureIgnoreCase) || Tag.Name.Equals("img", StringComparison.CurrentCultureIgnoreCase);
        }

        private void ParseAttributes()
        {
            var attributesParser = new AttributesParser(Strings, Index, EndIndex);
            attributesParser.Parse();

            Tag.Attributes = attributesParser.Attributes;
            Tag.HasSlash = attributesParser.HasSlash;
            Index = attributesParser.Index;
        }
    }
}