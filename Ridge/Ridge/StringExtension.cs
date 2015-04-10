using System;

namespace Ridge
{
    public static class StringExtension
    {
        public static bool Is(this string current, string s, bool ignoreCase = false)
        {
            if (ignoreCase)
            {
                return String.Equals(current, s, StringComparison.InvariantCultureIgnoreCase);
            }

            return current == s;
        }

        public static bool IsNot(this string current, string s, bool ignoreCase = false)
        {
            return !current.Is(s, ignoreCase);
        }

        public static void Expect(this string current, string s, Source source, bool ignoreCase = false)
        {
            if (current.IsNot(s))
            {
                throw new ParseException(source);
            }
        }

        public static void ExpectNot(this string current, string s, Source source, bool ignoreCase = false)
        {
            if (current.Is(s))
            {
                throw new ParseException(source);
            }
        }
    }
}