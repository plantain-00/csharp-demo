using System.Collections.Generic;
using System.Linq;
using System.Text;

using ParseLibrary;

using Ridge.Nodes;

namespace Ridge
{
    public class Document
    {
        public Document()
        {
            Nodes = new List<Node>();
        }

        public Document(string html) : this()
        {
            var source = new Source(html);
            source.SkipWhiteSpace();
            if (source.Is(DocType.NAME, true))
            {
                Nodes.Add(DocType.Create(source, 0));
            }

            source.SkipWhiteSpace();
            while (!source.IsTail)
            {
                Nodes.Add(Node.CreateNode(source, 0));

                source.SkipWhiteSpace();
            }
        }

        public List<Node> Nodes { get; set; }

        public Node this[string param]
        {
            get
            {
                if (param.StartsWith("#")
                    && param.Length > 1)
                {
                    var id = param.Substring(1);
                    return GetElementById(id);
                }
                return this[param, 0];
            }
        }

        public Node this[string tagName, int index]
        {
            get
            {
                var nodes = Nodes.Where(c => c is Tag && (c as Tag).Name.Is(tagName, true));
                return nodes.ElementAt(index);
            }
        }

        public Node this[int index]
        {
            get
            {
                return Nodes[index];
            }
        }

        public Node GetElementById(string id)
        {
            foreach (var node in Nodes)
            {
                var result = node.GetElementById(id);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        public string ToString(Formatting formatting, int spaceNumber = 4)
        {
            var builder = new StringBuilder();
            foreach (var node in Nodes)
            {
                builder.Append(node.ToString(formatting, spaceNumber));
            }
            return builder.ToString();
        }

        public override string ToString()
        {
            return ToString(Formatting.Indented);
        }
    }
}