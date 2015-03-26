using System;

using JsonConverter.Nodes;

namespace JsonConverter.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string JSON = "{\"name\":\"aaa\",\"age\":123,\"is_boy\":true,\"anything_else\":null,\"bbb\":{\"jjj\":\"ddd\",\"ere\":false,\"eee\":{\"fff\":\"sde\",\"ger\":null,\"yyy\":[1,2,3,{\"gtg\":[]}]}}}";
            var o = JObject.Create(JSON);
            var s = o.ToString(Formatting.Indented);
            var d = ((o as JClass)[1] as JNumber).Number;
        }
    }
}