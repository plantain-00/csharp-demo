using JsonConverter.Nodes;
using JsonConverter.Parsers;

namespace JsonConverter
{
    public class Json
    {
        private JToken _token;
        public Json(string s)
        {
            var source = new Source(s);

            source.SkipWhiteSpace();
            if (source.Is('{'))
            {
                source.MoveForward();

                var parser = new JObjectParser(source);
                parser.Parse();

                _token = parser.Result;
            }
        }
    }
}