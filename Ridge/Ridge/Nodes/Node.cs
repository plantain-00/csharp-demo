using System.Collections.Generic;

namespace Ridge.Nodes
{
    public class Node
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
    }
}