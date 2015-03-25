using System.Collections.Generic;

namespace JsonConverter.Nodes
{
    public class JArray : JToken
    {
        public JArray()
        {
        }

        internal JArray(Source source, int depth) : base(depth)
        {
            if (source.IsNot('['))
            {
                throw new ParseException(source);
            }
            source.MoveForward();
        }

        public IList<JToken> Items { get; set; }
    }
}