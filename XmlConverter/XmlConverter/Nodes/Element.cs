using System;
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
            if (ChildElements != null)
            {
                foreach (var childElement in ChildElements)
                {
                    children.Append(childElement.ToString(formatting));
                }
            }

            if (formatting == Formatting.None)
            {
                if (ChildElements == null)
                {
                    return string.Format("<{0}{1} />", Name, attributes);
                }
                return string.Format("<{0}{1} >{2}</{0}>", Name, attributes, children);
            }
            if (ChildElements == null)
            {
                return string.Format("<{0}{1} />", Name, attributes);
            }
            return string.Format("<{0}{1} >{2}</{0}>", Name, attributes, children);
        }

        internal static Element Create(Source source)
        {
            if (source.IsNot('<'))
            {
                throw new ParseException(source);
            }
            source.MoveForward();

            source.SkipWhiteSpace();
            var startIndex = source.Index;
            source.MoveUntil(c => " >/".Any(a => a == c));
            var result = new Element
                         {
                             Name = source.Substring(startIndex, source.Index - startIndex)
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
                if (source.IsNot('>'))
                {
                    throw new ParseException(source);
                }
                source.MoveForward();
            }
            else if (source.Is('>'))
            {
                source.MoveForward();

                source.SkipWhiteSpace();
                startIndex = source.Index;
                while (!IsEndTag(source, result.Name))
                {
                    source.Index = startIndex;

                    if (result.ChildElements == null)
                    {
                        result.ChildElements = new List<Node>();
                    }
                    if (source.Is('<'))
                    {
                        result.ChildElements.Add(Create(source));
                    }
                    else
                    {
                        result.ChildElements.Add(PlainText.Create(source));
                    }

                    source.SkipWhiteSpace();
                    startIndex = source.Index;
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
            if (source.IsNot('<'))
            {
                return false;
            }
            source.MoveForward();

            source.SkipWhiteSpace();
            if (source.IsNot('/'))
            {
                return false;
            }
            source.MoveForward();

            source.SkipWhiteSpace();
            if (source.IsNot(tagName, 0, true))
            {
                return false;
            }
            source.MoveForward(tagName.Length);

            source.SkipWhiteSpace();
            if (source.IsNot('>'))
            {
                return false;
            }
            source.MoveForward();
            return true;
        }
    }
}