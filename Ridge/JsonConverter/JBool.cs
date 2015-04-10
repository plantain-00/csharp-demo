using ParseLibrary;

namespace JsonConverter
{
    public sealed class JBool : JObject
    {
        private const string TRUE_STRING = "true";
        private const string FALSE_STRING = "false";

        public static readonly JBool True = new JBool
                                            {
                                                Value = true
                                            };
        public static readonly JBool False = new JBool
                                             {
                                                 Value = false
                                             };

        private JBool()
        {
        }

        public bool Value { get; private set; }

        internal static JBool Create(Source source)
        {
            if (source.Is(TRUE_STRING))
            {
                source.Skip(TRUE_STRING);

                return True;
            }
            if (source.Is(FALSE_STRING))
            {
                source.Skip(FALSE_STRING);

                return False;
            }
            throw new ParseException(source);
        }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            return Value ? TRUE_STRING : FALSE_STRING;
        }
    }
}