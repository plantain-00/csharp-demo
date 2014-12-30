using System.Collections.Generic;

namespace Ridge
{
    internal class LexicalAnalysis
    {
        internal List<string> Analyse(string s)
        {
            var result = new List<string>();
            var tmp = string.Empty;
            foreach (var c in s)
            {
                if (char.IsWhiteSpace(c)
                    || c == CHAR.SINGLE_QUOTE
                    || c == CHAR.DOUBLE_QUOTE
                    || c == CHAR.LARGER_THAN
                    || c == CHAR.LESS_THAN
                    || c == CHAR.EQUAL
                    || c == CHAR.SLASH
                    || c == CHAR.RETURN
                    || c == CHAR.NEW_LINE)
                {
                    if (tmp != string.Empty)
                    {
                        result.Add(tmp);
                        tmp = string.Empty;
                    }
                    if (c != CHAR.NEW_LINE
                        && c != CHAR.RETURN)
                    {
                        result.Add(new string(c, 1));
                    }
                }
                else
                {
                    tmp += c;
                }
            }
            if (!string.IsNullOrEmpty(tmp))
            {
                result.Add(tmp);
            }
            return result;
        }
    }
}