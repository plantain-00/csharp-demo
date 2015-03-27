using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlConverter.Nodes
{
    public class Element : Node
    {
        public IList<Attribute> Attributes { get; set; }
        public IList<Node> ChildElements { get; set; }
        public string Name { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            var attributes = new StringBuilder();
            if (Attributes != null)
            {
                foreach (var attribute in Attributes)
                {
                    attributes.Append(" ");
                    attributes.Append(attribute.ToString(formatting));
                }
            }
            var children = new StringBuilder();

            if (formatting == Formatting.None)
            {
                if (ChildElements != null)
                {
                    foreach (var childElement in ChildElements)
                    {
                        children.Append(childElement.ToString(formatting));
                    }
                }
                if (ChildElements == null)
                {
                    return string.Format("<{0}{1}/>", Name, attributes);
                }
                return string.Format("<{0}{1}>{2}</{0}>", Name, attributes, children);
            }

            var spaces = new string(' ', Depth * spaceNumber);
            if (ChildElements != null)
            {
                foreach (var childElement in ChildElements)
                {
                    children.Append(childElement.ToString(formatting));
                }
            }
            if (ChildElements == null)
            {
                return string.Format("{2}<{0}{1}/>\n", Name, attributes, spaces);
            }
            var startLine = string.Format("{2}<{0}{1}>\n", Name, attributes, spaces);
            var endLine = string.Format("{1}</{0}>\n", Name, spaces);
            return startLine + children + endLine;
        }

        internal static Element Create(Source source, int depth)
        {
            source.Expect('<');
            source.MoveForward();

            source.SkipWhiteSpace();
            var startIndex = source.Index;
            source.MoveUntil(c => " >/".Any(a => a == c));
            var result = new Element
                         {
                             Name = source.Substring(startIndex, source.Index - startIndex),
                             Depth = depth
                         };

            source.SkipWhiteSpace();
            while (">/".All(c => source.IsNot(c)))
            {
                if (result.Attributes == null)
                {
                    result.Attributes = new List<Attribute>();
                }
                result.Attributes.Add(Attribute.Create(source));
                source.SkipWhiteSpace();
            }
            if (source.Is('/'))
            {
                source.MoveForward();

                source.SkipWhiteSpace();
                source.Expect('>');
                source.MoveForward();
            }
            else if (source.Is('>'))
            {
                source.MoveForward();

                source.SkipWhiteSpace();
                while (!IsEndTag(source, result.Name))
                {
                    if (result.ChildElements == null)
                    {
                        result.ChildElements = new List<Node>();
                    }
                    if (source.Is(Comment.COMMENT_START))
                    {
                        result.ChildElements.Add(Comment.Create(source, depth + 1));
                    }
                    else if (source.Is('<'))
                    {
                        result.ChildElements.Add(Create(source, depth + 1));
                    }
                    else
                    {
                        result.ChildElements.Add(PlainText.Create(source, depth + 1));
                    }

                    source.SkipWhiteSpace();
                }
            }
            else
            {
                throw new ParseException(source);
            }

            return result;
        }

        private static bool IsEndTag(Source source, string tagName)
        {
            var startIndex = source.Index;

            if (source.IsNot('<'))
            {
                return false;
            }
            source.MoveForward();

            source.SkipWhiteSpace();
            if (source.IsNot('/'))
            {
                source.Index = startIndex;
                return false;
            }
            source.MoveForward();

            source.SkipWhiteSpace();
            if (source.IsNot(tagName, true))
            {
                source.Index = startIndex;
                return false;
            }
            source.MoveForward(tagName.Length);

            source.SkipWhiteSpace();
            if (source.IsNot('>'))
            {
                source.Index = startIndex;
                return false;
            }
            source.MoveForward();
            return true;
        }
    }
}