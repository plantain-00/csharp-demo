using JsonConverter.Nodes;

namespace JsonConverter
{
    internal abstract class ParserBase
    {
        protected internal ParserBase(Source source, int depth)
        {
            Source = source;
            Depth = depth;
        }

        protected internal Source Source { get; private set; }
        protected internal JToken Result { get; protected set; }
        protected internal int Depth { get; protected set; }
        internal abstract void Parse();
    }
}