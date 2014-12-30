using System;
using System.Collections.Generic;
using System.Linq;

namespace Ridge.Nodes
{
    public abstract class Node
    {
        public List<Node> Children { get; set; }
        internal int Depth { get; set; }

        public Node this[int index]
        {
            get
            {
                return Children[index];
            }
        }

        public Node this[string tagName, int index]
        {
            get
            {
                var nodes = Children.Where(c => c is Tag && (c as Tag).Name.Equals(tagName, StringComparison.CurrentCultureIgnoreCase));
                return nodes.ElementAt(index);
            }
        }

        public Node this[string tagName]
        {
            get
            {
                return this[tagName, 0];
            }
        }

        public T As<T>() where T : class
        {
            return this as T;
        }

        internal virtual Node GetElementById(string id)
        {
            if (Children == null)
            {
                return null;
            }
            foreach (var child in Children)
            {
                var node = child.GetElementById(id);
                if (node != null)
                {
                    return node;
                }
            }
            return null;
        }

        public abstract string ToString(Formatting formatting, int spaceNumber = 4);
    }
}