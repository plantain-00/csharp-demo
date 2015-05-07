using System.Collections.Generic;
using System.Linq;

using ParseLibrary;

namespace Ridge
{
    public abstract class Node : FormattingBase
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
                var nodes = Children.Where(c => c is Tag && (c as Tag).Name.Is(tagName, true));
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

        public T As<T>() where T : Node
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

        internal static Node CreateNode(Source source, int depth)
        {
            if (source.IsTail)
            {
                return null;
            }
            if (source.Is('<'))
            {
                if (source.Is(Comment.COMMENT_START))
                {
                    return Comment.Create(source, depth);
                }

                if (source.Is("</"))
                {
                    source.MoveUntil('>');
                    return CreateNode(source, depth);
                }

                return Tag.Create(source, depth);
            }
            return PlainText.Create(source, depth);
        }
    }
}