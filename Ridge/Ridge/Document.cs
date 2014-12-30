using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ridge.Nodes;
using Ridge.Parsers;

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
            var strings = new LexicalAnalysis().Analyse(html);

            var i = SkipSpaces(strings, 0);

            if (strings[i] == STRING.LESS_THAN
                && strings[i + 1].Equals(STRING.DOCTYPE, StringComparison.CurrentCultureIgnoreCase))
            {
                var doctypeParser = new DocTypeParser(strings, i, strings.Count);
                doctypeParser.Parse();

                Nodes.Add(doctypeParser.DocType);

                i = doctypeParser.Index;
            }

            while (i < strings.Count)
            {
                i = SkipSpaces(strings, i);

                if (strings[i] == STRING.LESS_THAN)
                {
                    var tagParser = new TagParser(strings, i, 0, strings.Count);
                    tagParser.Parse();

                    Nodes.Add(tagParser.Tag);
                    i = tagParser.Index;
                }
                else
                {
                    var plainTextParser = new PlainTextParser(strings, i, 0, strings.Count);
                    plainTextParser.Parse();

                    if (plainTextParser.PlainText != null)
                    {
                        Nodes.Add(plainTextParser.PlainText);
                    }
                    
                    i = plainTextParser.Index;
                }

                i = SkipSpaces(strings, i);
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
                var nodes = Nodes.Where(c => c is Tag && (c as Tag).Name.Equals(tagName, StringComparison.CurrentCultureIgnoreCase));
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

        private int SkipSpaces(IReadOnlyList<string> strings, int i)
        {
            while (i < strings.Count
                   && (strings[i] == STRING.SPACE || strings[i] == STRING.RETURN || strings[i] == STRING.NEW_LINE))
            {
                i++;
            }
            return i;
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