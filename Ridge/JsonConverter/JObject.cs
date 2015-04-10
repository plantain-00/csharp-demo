using ParseLibrary;

namespace JsonConverter
{
    public abstract class JObject : FormattingBase
    {
        internal static JObject CreateObject(Source source, int depth)
        {
            if (source.Is('['))
            {
                return JArray.Create(source, depth + 1);
            }
            if (source.Is('{'))
            {
                return JClass.Create(source, depth + 1);
            }
            if (source.Is("true")
                || source.Is("false"))
            {
                return JBool.Create(source);
            }
            if (source.Is("null"))
            {
                return JNull.Create(source);
            }
            if (source.Is('"'))
            {
                return JString.Create(source);
            }
            return JNumber.Create(source);
        }

        public static JObject Create(string s)
        {
            var source = new Source(s);

            source.SkipBlankSpaces();

            var result = CreateObject(source, 0);

            source.SkipBlankSpaces();
            source.ExpectEnd();

            return result;
        }
    }
}