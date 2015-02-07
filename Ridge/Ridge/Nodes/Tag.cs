using System;
using System.Collections.Generic;
using System.Linq;

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
                var attribute = Attributes.FirstOrDefault(a => a.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
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
                var attribute = Attributes.FirstOrDefault(a => a.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase));
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
                    return string.Format("{2}<{0}{1}{3}>\n", Name, attributes, new string(CHAR.SPACE, Depth * spaceNumber), slash);
                }
                if (Children.Count == 0)
                {
                    return string.Format("{2}<{0}{1}{3}></{0}>\n", Name, attributes, new string(CHAR.SPACE, Depth * spaceNumber), slash);
                }
                if (Children.Count == 1
                    && Children[0] is PlainText)
                {
                    var plainText = Children[0] as PlainText;
                    return string.Format("{3}<{0}{1}{4}>{2}</{0}>\n", Name, attributes, plainText.ToString(Formatting.None), new string(CHAR.SPACE, Depth * spaceNumber), slash);
                }

                var children = string.Empty;
                foreach (var child in Children)
                {
                    children += child.ToString(formatting, spaceNumber);
                }

                return string.Format("{3}<{0}{1}{4}>\n{2}{3}</{0}>\n", Name, attributes, children, new string(CHAR.SPACE, Depth * spaceNumber), slash);
            }
        }

        private string GetSlash()
        {
            return HasSlash ? " /" : "";
        }

        public override string ToString()
        {
            return ToString(Formatting.Indented);
        }
    }
}