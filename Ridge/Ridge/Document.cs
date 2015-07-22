using System.Collections.Generic;
using System.Linq;
using System.Text;

using ParseLibrary;

namespace Ridge
{
    public class Document : FormattingBase
    {
        public Document()
        {
            Nodes = new List<Node>();
        }

        public Document(string html) : this()
        {
            var source = new Source(html);
            source.SkipBlankSpaces();
            if (source.Is(DocType.NAME, true))
            {
                Nodes.Add(DocType.Create(source, null, 0));
            }

            source.SkipBlankSpaces();
            while (!source.IsTail)
            {
                Nodes.Add(Node.CreateNode(source, null, 0));

                source.SkipBlankSpaces();
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
                var nodes = Nodes.Where(c => c is Tag && ((Tag) c).Name.Is(tagName, true));
                return nodes.ElementAt(index);
            }
        }

        public Node this[int index] => Nodes[index];

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

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            var builder = new StringBuilder();
            foreach (var node in Nodes)
            {
                builder.Append(node.ToString(formatting, spaceNumber));
            }
            return builder.ToString();
        }
    }
}