using System;

namespace JsonConverter
{
    public class ParseException : Exception
    {
        public ParseException(Source source)
        {
            Index = source.Index;

            var startIndex = Math.Max(0, source.Index - 5);
            var endIndex = Math.Min(source.Length - 1, source.Index + 5);
            Context = source.Substring(startIndex, endIndex - startIndex + 1);
        }

        public int Index { get; private set; }
        public string Context { get; private set; }
    }
}