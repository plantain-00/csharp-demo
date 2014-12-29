using System;
using System.Collections.Generic;
using System.Linq;

namespace Ridge
{
    public class Parser
    {
        public List<Node> Parse(string html)
        {
            var result = new List<Node>();

            var strings = new LexicalAnalysis().Analyse(html);
            var doctypeParser = new DocTypeParser(strings, 0);
            result.Add(doctypeParser.DocType);

            for (var index = doctypeParser.Index; index < strings.Count;)
            {
                while (strings[index] == STRING.SPACE)
                {
                    index++;
                }
                if (strings[index] == STRING.LESS_THAN)
                {
                    var tagParser = new TagParser(strings, index, 0);
                    result.Add(tagParser.Tag);
                    index = tagParser.Index;
                }
                else
                {
                    var plainTextParser = new PlainTextParser(strings, index, 0);
                    result.Add(plainTextParser.PlainText);
                    index = plainTextParser.Index;
                }
                while (index < strings.Count
                       && strings[index] == STRING.SPACE)
                {
                    index++;
                }
            }

            return result;
        }
    }

    public class ParserBase
    {
        protected internal int Index { get; protected set; }
    }

    public class CommentParser : ParserBase
    {
        public CommentParser(IReadOnlyList<string> strings, int index, int depth)
        {
            if (strings[index] != STRING.LESS_THAN
                || !strings[index + 1].StartsWith(STRING.COMMENT_START))
            {
                throw new Exception();
            }
            index++;
            Comment = new Comment
                      {
                          Text = string.Empty,
                          Depth = depth
                      };
            while (!strings[index].EndsWith(STRING.COMMENT_END)
                   || strings[index + 1] != STRING.LARGER_THAN)
            {
                Comment.Text += strings[index];
                index++;
            }
            Comment.Text += strings[index];
            index += 2;
            Index = index;
        }

        public Comment Comment { get; private set; }
    }

    public class StringParser : ParserBase
    {
        public StringParser(IReadOnlyList<string> strings, int index)
        {
            if (strings[index] != STRING.SINGLE_QUOTE
                && strings[index] != STRING.DOUBLE_QUOTE)
            {
                throw new Exception();
            }
            var startIndex = index;
            switch (strings[index])
            {
                case STRING.SINGLE_QUOTE:
                    do
                    {
                        index++;
                    }
                    while ((strings[index] != STRING.SINGLE_QUOTE || strings[index - 1].EndsWith(STRING.ESCAPE))
                           && index < strings.Count);
                    break;
                case STRING.DOUBLE_QUOTE:
                    do
                    {
                        index++;
                    }
                    while ((strings[index] != STRING.DOUBLE_QUOTE || strings[index - 1].EndsWith(STRING.ESCAPE))
                           && index < strings.Count);
                    break;
            }
            index++;
            String = String.Empty;
            for (var i = startIndex + 1; i < index - 1; i++)
            {
                String += strings[i];
            }
            Index = index;
        }

        public string String { get; private set; }
    }

    public class AttributeParser : ParserBase
    {
        public AttributeParser(IReadOnlyList<string> strings, int index)
        {
            if (strings[index] == STRING.SPACE
                || strings[index] == STRING.LESS_THAN
                || strings[index] == STRING.LARGER_THAN
                || strings[index] == STRING.SLASH)
            {
                throw new Exception();
            }
            Attribute = new Attribute
                        {
                            Name = strings[index]
                        };
            index++;
            bool willContinue;
            do
            {
                willContinue = false;
                switch (strings[index])
                {
                    case STRING.SPACE:
                        willContinue = true;
                        index++;
                        Index = index;
                        break;
                    case STRING.EQUAL:
                        index++;
                        while (index < strings.Count
                               && strings[index] == STRING.SPACE)
                        {
                            index++;
                        }
                        var stringParser = new StringParser(strings, index);
                        Index = stringParser.Index;
                        Attribute.Value = stringParser.String;
                        return;
                    case STRING.LARGER_THAN:
                        Index = index;
                        break;
                    case STRING.SLASH:
                        if (strings[index + 1] == STRING.LARGER_THAN)
                        {
                            Index = index;
                        }
                        break;
                }
            }
            while (willContinue && index < strings.Count);
        }

        public Attribute Attribute { get; private set; }
    }

    public class AttributesParser : ParserBase
    {
        public AttributesParser(IReadOnlyList<string> strings, int index)
        {
            bool willContinue;
            do
            {
                while (strings[index] == STRING.SPACE)
                {
                    index++;
                }
                if (strings[index] == STRING.SLASH
                    && strings[index + 1] == STRING.LARGER_THAN)
                {
                    willContinue = false;
                    index += 2;
                    HasSlash = true;
                }
                else if (strings[index] == STRING.LARGER_THAN)
                {
                    willContinue = false;
                    index++;
                }
                else
                {
                    willContinue = true;
                    var attributeParser = new AttributeParser(strings, index);
                    if (Attributes == null)
                    {
                        Attributes = new List<Attribute>();
                    }
                    Attributes.Add(attributeParser.Attribute);
                    index = attributeParser.Index;
                }
            }
            while (willContinue && index < strings.Count);
            Index = index;
        }

        public List<Attribute> Attributes { get; private set; }
        internal bool HasSlash { get; private set; }
    }

    public class PlainTextParser : ParserBase
    {
        public PlainTextParser(IReadOnlyList<string> strings, int index, int depth)
        {
            PlainText = new PlainText
                        {
                            Text = string.Empty
                        };
            do
            {
                PlainText.Text += strings[index];
                index++;
            }
            while (index < strings.Count
                   && strings[index] != STRING.LESS_THAN);
            Index = index;
            PlainText.Depth = depth;
        }

        public PlainText PlainText { get; private set; }
    }

    public class DocTypeParser : ParserBase
    {
        public DocTypeParser(IReadOnlyList<string> strings, int index)
        {
            if (strings[index] != STRING.LESS_THAN
                || strings[index + 1] != STRING.DOCTYPE)
            {
                throw new Exception();
            }
            DocType = new DocType
                      {
                          Name = STRING.DOCTYPE,
                          Depth = 0,
                          Declaration = string.Empty
                      };
            index += 2;
            while (strings[index] != STRING.LARGER_THAN)
            {
                DocType.Declaration += strings[index];
                index++;
            }
            index++;
            Index = index;
        }

        public DocType DocType { get; private set; }
    }

    public class TagParser : ParserBase
    {
        public TagParser(IReadOnlyList<string> strings, int index, int depth)
        {
            if (strings[index] != STRING.LESS_THAN
                || strings[index + 1] == STRING.SLASH)
            {
                throw new Exception();
            }
            index++;
            Tag = new Tag
                  {
                      Name = strings[index],
                      Depth = depth
                  };

            index++;
            var attributesParser = new AttributesParser(strings, index);
            Tag.Attributes = attributesParser.Attributes;
            Tag.HasSlash = attributesParser.HasSlash;

            if (Tag.Name.Equals("input", StringComparison.CurrentCultureIgnoreCase)
                || Tag.Name.Equals("meta", StringComparison.CurrentCultureIgnoreCase)
                || Tag.Name.Equals("link", StringComparison.CurrentCultureIgnoreCase)
                || Tag.Name.Equals("br", StringComparison.CurrentCultureIgnoreCase)
                || Tag.Name.Equals("hr", StringComparison.CurrentCultureIgnoreCase)
                || Tag.Name.Equals("img", StringComparison.CurrentCultureIgnoreCase))
            {
                Index = attributesParser.Index;
            }
            else
            {
                index = attributesParser.Index;
                var endIndex = -1;
                var stackDepth = 0;
                for (var i = index; i + 3 < strings.Count; i++)
                {
                    if (strings[i] == STRING.LESS_THAN
                        && strings[i + 1] == STRING.SLASH
                        && strings[i + 2] == Tag.Name
                        && strings[i + 3] == STRING.LARGER_THAN)
                    {
                        if (stackDepth == 0)
                        {
                            endIndex = i;
                            break;
                        }
                        stackDepth--;
                    }
                    else if (strings[i] == STRING.LESS_THAN
                             && strings[i + 1] == Tag.Name
                             && strings[i + 2] == STRING.LARGER_THAN)
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

                while (index < endIndex)
                {
                    while (strings[index] == STRING.SPACE)
                    {
                        index++;
                    }
                    if (strings[index] == STRING.LESS_THAN
                        && strings[index + 1].StartsWith(STRING.COMMENT_START))
                    {
                        var commentParser = new CommentParser(strings, index, depth + 1);
                        Tag.Children.Add(commentParser.Comment);
                        index = commentParser.Index;
                    }
                    else if (strings[index] == STRING.LESS_THAN
                             && strings[index + 1] != STRING.SLASH)
                    {
                        var tagParser = new TagParser(strings, index, depth + 1);
                        Tag.Children.Add(tagParser.Tag);
                        index = tagParser.Index;
                    }
                    else
                    {
                        var plainTextParser = new PlainTextParser(strings, index, depth + 1);
                        Tag.Children.Add(plainTextParser.PlainText);
                        index = plainTextParser.Index;
                    }
                    while (strings[index] == STRING.SPACE)
                    {
                        index++;
                    }
                }

                Index = endIndex + 4;
            }
        }

        public Tag Tag { get; private set; }
    }

    public class Node
    {
        public List<Node> Children { get; set; }
        internal int Depth { get; set; }
    }

    public class DocType : Node
    {
        public string Name { get; set; }
        public string Declaration { get; set; }

        public override string ToString()
        {
            return string.Format("<{0}{1}>\n", Name, Declaration);
        }
    }

    public class Tag : Node
    {
        public string Name { get; set; }
        public List<Attribute> Attributes { get; set; }
        public bool HasSlash { get; set; }

        public override string ToString()
        {
            var attributes = string.Empty;
            if (Attributes != null)
            {
                foreach (var attribute in Attributes)
                {
                    attributes += " " + attribute;
                }
            }
            if (Children == null)
            {
                return string.Format("{2}<{0}{1}{3}>\n", Name, attributes, new string(CHAR.SPACE, Depth * 4), HasSlash ? "/" : "");
            }
            if (Children.Count == 0)
            {
                return string.Format("{2}<{0}{1}></{0}>\n", Name, attributes, new string(CHAR.SPACE, Depth * 4));
            }
            if (Children.Count == 1
                && Children[0] is PlainText)
            {
                var plainText = Children[0] as PlainText;
                if (!Name.Equals("script", StringComparison.CurrentCultureIgnoreCase)
                    && !Name.Equals("html", StringComparison.CurrentCultureIgnoreCase)
                    && !Name.Equals("head", StringComparison.CurrentCultureIgnoreCase)
                    && !Name.Equals("body", StringComparison.CurrentCultureIgnoreCase)
                    && plainText.Text.All(t => t != '\n' && t != '\r'))
                {
                    return string.Format("{3}<{0}{1}>{2}</{0}>\n", Name, attributes, plainText.Text, new string(CHAR.SPACE, Depth * 4));
                }
            }
            var children = string.Empty;
            foreach (var child in Children)
            {
                children += child;
            }
            return string.Format("{3}<{0}{1}>\n{2}{3}</{0}>\n", Name, attributes, children, new string(CHAR.SPACE, Depth * 4));
        }
    }

    public class Comment : Node
    {
        public string Text { get; set; }

        public override string ToString()
        {
            return string.Format("{0}<{1}>\n", new string(CHAR.SPACE, Depth * 4), Text);
        }
    }

    public class PlainText : Node
    {
        public string Text { get; set; }

        public override string ToString()
        {
            return string.Format("{0}{1}\n", new string(CHAR.SPACE, Depth * 4), Text);
        }
    }

    public class Attribute
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            if (Value == null)
            {
                return Name;
            }
            return string.Format("{0}=\"{1}\"", Name, Value);
        }
    }
}