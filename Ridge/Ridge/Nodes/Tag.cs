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
                var attribute = Attributes.FirstOrDefault(a => a.Name == name);
                return attribute == null ? null : attribute.Value;
            }
        }

        internal override Node GetElementById(string id)
        {
            if (Attributes != null)
            {
                var attribute = Attributes.FirstOrDefault(a => a.Name.Equals("id"));
                if (attribute != null
                    && attribute.Value == id)
                {
                    return this;
                }
            }
            return base.GetElementById(id);
        }

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
}