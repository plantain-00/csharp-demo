using System.Collections.Generic;

namespace Ridge
{
    public class LexicalAnalysis
    {
        public List<string> Analyse(string s)
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
                    || c == CHAR.SLASH)
                {
                    if (tmp != string.Empty)
                    {
                        result.Add(tmp);
                        tmp = string.Empty;
                    }
                    if (c != CHAR.RETURN
                        && c != CHAR.NEW_LINE)
                    {
                        result.Add(new string(c, 1));
                    }
                }
                else
                {
                    tmp += c;
                }
            }
            return result;
        }
    }

    internal static class CHAR
    {
        public const char RETURN = '\r';
        public const char NEW_LINE = '\n';
        public const char SINGLE_QUOTE = '\'';
        public const char DOUBLE_QUOTE = '\"';
        public const char EQUAL = '=';
        public const char LESS_THAN = '<';
        public const char LARGER_THAN = '>';
        public const char SLASH = '/';
        public const char SPACE = ' ';
        public const char ESCAPE = '\\';
    }

    internal static class STRING
    {
        public const string RETURN = "\r";
        public const string NEW_LINE = "\n";
        public const string SINGLE_QUOTE = "\'";
        public const string DOUBLE_QUOTE = "\"";
        public const string EQUAL = "=";
        public const string LESS_THAN = "<";
        public const string LARGER_THAN = ">";
        public const string SLASH = "/";
        public const string SPACE = " ";
        public const string ESCAPE = "\\";
    }
}