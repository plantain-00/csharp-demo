using System;

using JsonConverter.Nodes;

namespace JsonConverter
{
    internal abstract class ParserBase
    {
        protected internal ParserBase(Source source)
        {
            Source = source;
        }

        protected internal Source Source { get; private set; }
        protected internal JToken Result { get; protected set; }
        internal abstract void Parse();
    }
}