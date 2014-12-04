using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json.Linq;

namespace JsonAnalyser
{
    public class JsonHeaderLogic
    {
        private const string NULL_TEXT = "<null>";
        private const string ARRAY = "[{0}]";
        private const string OBJECT = "[{0}]";
        private const string PROPERTY = "{0}";
        private JsonHeaderLogic(JToken token, string header, IEnumerable<JsonHeaderLogic> children)
        {
            Token = token;
            Header = header;
            Children = children;
        }
        public string Header { get; private set; }
        public IEnumerable<JsonHeaderLogic> Children { get; private set; }
        public JToken Token { get; private set; }
        public static JsonHeaderLogic FromJToken(JToken jtoken)
        {
            if (jtoken == null)
            {
                throw new ArgumentNullException("jtoken");
            }
            var type = jtoken.GetType();
            if (typeof (JValue).IsAssignableFrom(type))
            {
                var jvalue = (JValue) jtoken;
                var value = jvalue.Value;
                if (value == null)
                {
                    value = NULL_TEXT;
                }
                return new JsonHeaderLogic(jvalue, value.ToString(), null);
            }
            if (typeof (JContainer).IsAssignableFrom(type))
            {
                var jcontainer = (JContainer) jtoken;
                var children = jcontainer.Children().Select(FromJToken);
                string header;
                if (typeof (JProperty).IsAssignableFrom(type))
                {
                    header = string.Format(PROPERTY, ((JProperty) jcontainer).Name);
                }
                else if (typeof (JArray).IsAssignableFrom(type))
                {
                    header = string.Format(ARRAY, children.Count());
                }
                else if (typeof (JObject).IsAssignableFrom(type))
                {
                    header = string.Format(OBJECT, children.Count());
                }
                else
                {
                    throw new Exception("不支持的JContainer类型");
                }
                return new JsonHeaderLogic(jcontainer, header, children);
            }
            throw new Exception("不支持的JToken类型");
        }
    }
}