﻿using System.Collections.Generic;
using System.Linq;

using ParseLibrary;

namespace Ridge.Nodes
{
    public class Tag : Node
    {
        public string Name { get; set; }
        public List<Attribute> Attributes { get; set; }
        public bool HasSlash { get; set; }

        public new string this[string name]
        {
            get
            {
                if (Attributes == null)
                {
                    return null;
                }
                var attribute = Attributes.FirstOrDefault(a => a.Name.Is(name, true));
                return attribute == null ? null : attribute.Value;
            }
        }
        public string Text
        {
            get
            {
                if (Children == null
                    || Children.Count == 0)
                {
                    return string.Empty;
                }
                var children = string.Empty;
                foreach (var child in Children)
                {
                    if (child is PlainText)
                    {
                        children += (child as PlainText).Text;
                    }
                    else if (child is Tag)
                    {
                        children += (child as Tag).Text;
                    }
                }
                return children;
            }
        }

        internal override Node GetElementById(string id)
        {
            if (Attributes != null)
            {
                var attribute = Attributes.FirstOrDefault(a => a.Name.Is("id", true));
                if (attribute != null
                    && attribute.Value == id)
                {
                    return this;
                }
            }
            return base.GetElementById(id);
        }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            var attributes = string.Empty;
            if (Attributes != null)
            {
                foreach (var attribute in Attributes)
                {
                    attributes += " " + attribute;
                }
            }

            var slash = GetSlash();

            if (formatting == Formatting.None)
            {
                if (Children == null)
                {
                    return string.Format("<{0}{1}{2}>", Name, attributes, slash);
                }
                if (Children.Count == 0)
                {
                    return string.Format("<{0}{1}{2}></{0}>", Name, attributes, slash);
                }
                var children = string.Empty;
                foreach (var child in Children)
                {
                    children += child.ToString(Formatting.None);
                }
                return string.Format("<{0}{1}{3}>{2}</{0}>", Name, attributes, children, slash);
            }
            else
            {
                if (Children == null)
                {
                    return string.Format("{2}<{0}{1}{3}>\n", Name, attributes, new string(' ', Depth * spaceNumber), slash);
                }
                if (Children.Count == 0)
                {
                    return string.Format("{2}<{0}{1}{3}></{0}>\n", Name, attributes, new string(' ', Depth * spaceNumber), slash);
                }
                if (Children.Count == 1
                    && Children[0] is PlainText)
                {
                    var plainText = Children[0] as PlainText;
                    return string.Format("{3}<{0}{1}{4}>{2}</{0}>\n", Name, attributes, plainText.ToString(Formatting.None), new string(' ', Depth * spaceNumber), slash);
                }

                var children = string.Empty;
                foreach (var child in Children)
                {
                    children += child.ToString(formatting, spaceNumber);
                }

                return string.Format("{3}<{0}{1}{4}>\n{2}{3}</{0}>\n", Name, attributes, children, new string(' ', Depth * spaceNumber), slash);
            }
        }

        private string GetSlash()
        {
            return HasSlash ? " /" : "";
        }

        internal static Tag Create(Source source, int depth)
        {
            source.Expect('<');
            source.MoveForward();

            var result = new Tag
                         {
                             Name = source.TakeUntil(c => " \r\n</>".Contains(c)),
                             Attributes = new List<Attribute>(),
                             Depth = depth
                         };
            source.SkipWhiteSpace();

            result.Name.ExpectNot(string.Empty, source);

            if (result.Name.Is("input", true)
                || result.Name.Is("meta", true)
                || result.Name.Is("link", true)
                || result.Name.Is("br", true)
                || result.Name.Is("hr", true)
                || result.Name.Is("img", true))
            {
                while (source.IsNot('/')
                       && source.IsNot('>'))
                {
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
                else
                {
                    source.Expect('>');
                    source.MoveForward();
                }
            }
            else
            {
                while (source.IsNot('>'))
                {
                    result.Attributes.Add(Attribute.Create(source));
                    source.SkipWhiteSpace();
                }
                source.MoveForward();
                source.SkipWhiteSpace();

                result.Children = new List<Node>();

                while (source.IsNot("</" + result.Name + ">"))
                {
                    result.Children.Add(CreateNode(source, depth + 1));
                    source.SkipWhiteSpace();
                }
                source.MoveForward(3 + result.Name.Length);
            }

            return result;
        }
    }
}