using System;

namespace ParseLibrary
{
    public class ParseException : Exception
    {
        public ParseException(Source source, Exception innerException = null) : base(null, innerException)
        {
            Index = source.Index;

            Context = source.Context;
        }

        public int Index { get; private set; }
        public string Context { get; private set; }
    }
}