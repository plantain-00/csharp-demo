namespace JsonConverter.Nodes
{
    public abstract class JObject : JToken
    {
        public new T As<T>() where T : JObject
        {
            return this as T;
        }

        internal static JObject Convert(Source source, int depth)
        {
            if (source.Is('['))
            {
                return new JArray(source, depth + 1);
            }
            if (source.Is('{'))
            {
                return new JClass(source, depth + 1);
            }
            if (source.Is("true")
                || source.Is("false"))
            {
                return new JBool(source);
            }
            if (source.Is("null"))
            {
                return new JNull(source);
            }
            if (source.Is('"'))
            {
                return new JString(source);
            }
            return new JNumber(source);
        }

        public static JObject Convert(string s)
        {
            var source = new Source(s);

            source.SkipWhiteSpace();

            return Convert(source, 0);
        }
    }
}