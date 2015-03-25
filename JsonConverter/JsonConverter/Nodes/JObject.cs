using System.Collections.Generic;

namespace JsonConverter.Nodes
{
    public class JObject : JToken
    {
        public IList<JProperty> Properties { get; set; }
    }
}