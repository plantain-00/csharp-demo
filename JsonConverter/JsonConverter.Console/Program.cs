using System;

using JsonConverter.Nodes;

namespace JsonConverter.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string JSON = "{\"name\":\"aaa\",\"age\":123,\"is_boy\":true,\"anything_else\":null,\"bbb\":{\"jjj\":\"ddd\",\"ere\":false,\"eee\":{\"fff\":\"sde\",\"ger\":null,\"yyy\":[1,2,3,{\"gtg\":[]}]}}}";
            var o = JObject.Convert(JSON);
            var s = o.ToString(Formatting.Indented);
            var d = o.As<JClass>()[1].As<JNumber>().IntNumber;
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public bool IsGood { get; set; }
        public double Price { get; set; }
        public DateTime Expiry { get; set; }
        public string[] Sizes { get; set; }
    }
}