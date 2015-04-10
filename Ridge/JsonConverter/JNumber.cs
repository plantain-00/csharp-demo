using System;
using System.Linq;

using ParseLibrary;

namespace JsonConverter
{
    public sealed class JNumber : JObject
    {
        internal static JNumber Create(Source source)
        {
            if ("\":[{".Any(c => source.Is(c)))
            {
                throw new ParseException(source);
            }

            var startIndex = source.Index;
            source.MoveUntil(c => "\",}]".Any(a => a == c));
            var rawNumber = source.Substring(startIndex, source.Index - startIndex);
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