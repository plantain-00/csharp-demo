using System.Collections.Generic;

using Ridge.Nodes;
using Ridge.Parsers;

namespace Ridge
{
    public class Parser
    {
        public List<Node> Parse(string html)
        {
            var result = new List<Node>();

            var strings = new LexicalAnalysis().Analyse(html);
            var doctypeParser = new DocTypeParser(strings, 0);
            doctypeParser.Parse();

            result.Add(doctypeParser.DocType);

            for (var index = doctypeParser.Index; index < strings.Count;)
            {
                while (index < strings.Count
                       && (strings[index] == STRING.SPACE || strings[index] == STRING.RETURN || strings[index] == STRING.NEW_LINE))
                {
                    index++;
                }
                if (strings[index] == STRING.LESS_THAN)
                {
                    var tagParser = new TagParser(strings, index, 0);
                    tagParser.Parse();

                    result.Add(tagParser.Tag);
                    index = tagParser.Index;
                }
                else
                {
                    var plainTextParser = new PlainTextParser(strings, index, 0);
                    plainTextParser.Parse();
                    
                    result.Add(plainTextParser.PlainText);
                    index = plainTextParser.Index;
                }
                while (index < strings.Count
                       && (strings[index] == STRING.SPACE || strings[index] == STRING.RETURN || strings[index] == STRING.NEW_LINE))
                {
                    index++;
                }
            }

            return result;
        }
    }
}