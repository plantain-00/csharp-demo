﻿using System.Linq;

namespace JsonConverter.Nodes
{
    public class JNumber : JObject
    {
        public JNumber()
        {
        }

        internal JNumber(Source source)
        {
            if ("\":[{".Any(c => source.Is(c)))
            {
                throw new ParseException(source);
            }

            var startIndex = source.Index;
            source.MoveUntil(c => "\",}]".Any(a => a == c));
            var rawNumber = source.Substring(startIndex, source.Index - startIndex);
            Number = System.Convert.ToDouble(rawNumber);
        }

        public double Number { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            return Number.ToString();
        }
    }
}