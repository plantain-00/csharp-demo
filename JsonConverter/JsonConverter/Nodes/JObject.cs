using System.Collections.Generic;

namespace JsonConverter.Nodes
{
    public class JObject : JToken
    {
        public IDictionary<string, JToken> Properties { get; set; }
    }
}