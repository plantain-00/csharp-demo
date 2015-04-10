using System;
using System.Linq;

using ParseLibrary;

namespace JsonConverter
{
    public sealed class JNumber : JObject
    {
        internal static JNumber Create(Source source)
        {
            if ("\":[{".Any(source.Is))
            {
                throw new ParseException(source);
            }

            var rawNumber = source.TakeUntilAny("\",}]");
            try
            {
                var result = new JNumber
                             {
                                 Number = Convert.ToDouble(rawNumber)
                             };
                return result;
            }
            catch (Exception exception)
            {
                throw new ParseException(source, exception);
            }
        }

        public double Number { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            return Number.ToString();
        }
    }
}