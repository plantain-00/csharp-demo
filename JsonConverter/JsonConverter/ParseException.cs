using System;

namespace JsonConverter
{
    public class ParseException : Exception
    {
        public ParseException(Source source)
        {
            Index = source.Index;

            Context = source.Context;
        }

        public int Index { get; private set; }
        public string Context { get; private set; }
    }
}