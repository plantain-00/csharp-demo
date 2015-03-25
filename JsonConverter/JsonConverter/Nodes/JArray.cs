using System.Collections.Generic;

namespace JsonConverter.Nodes
{
    public class JArray : JObject
    {
        public JArray()
        {
        }

        internal JArray(Source source, int depth)
        {
            if (source.IsNot('['))
            {
                throw new ParseException(source);
            }
            source.MoveForward();
        }

        public IList<JToken> Items { get; set; }
        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            throw new System.NotImplementedException();
        }
    }
}