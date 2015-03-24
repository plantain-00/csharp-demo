using System.Collections.Generic;

namespace JsonConverter.Nodes
{
    public class JArray : JToken
    {
        public IList<JToken> Items { get; set; }
    }
}