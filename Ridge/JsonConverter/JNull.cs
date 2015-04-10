using ParseLibrary;

namespace JsonConverter
{
    public sealed class JNull : JObject
    {
        private const string NULL_STRING = "null";

        public static readonly JNull Null = new JNull();

        private JNull()
        {
        }

        internal static JNull Create(Source source)
        {
            source.Expect(NULL_STRING);
            source.Skip(NULL_STRING);
            return Null;
        }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            return NULL_STRING;
        }
    }
}