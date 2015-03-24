namespace JsonConverter.Nodes
{
    public class JProperty : JToken
    {
        public JKey Key { get; set; }
        public JToken Value { get; set; }
    }
}