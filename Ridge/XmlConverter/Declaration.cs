using System.Collections.Generic;
using System.Linq;
using System.Text;

using ParseLibrary;

namespace XmlConverter
{
    public class Declaration : FormattingBase
    {
        public IList<Attribute> Attributes { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            var builder = new StringBuilder("<?xml");
            foreach (var attribute in Attributes)
            {
                builder.AppendFormat(" {0}", attribute);
            }
            builder.Append(" ?>");
            return builder.ToString();
        }

        internal static Declaration Create(Source source)
        {
            source.SkipWhiteSpace();
            source.Expect('<');
            source.SkipIt();

            source.SkipWhiteSpace();
            source.Expect('?');
            source.SkipIt();

            source.SkipWhiteSpace();
            source.Expect("xml", true);
            source.Skip("xml");

            var result = new Declaration();
            source.SkipWhiteSpace();
            while ("/>=?".All(source.IsNot))
            {
                if (result.Attributes == null)
                {
                    result.Attributes = new List<Attribute>();
                }
                result.Attributes.Add(Attribute.Create(source));
                source.SkipWhiteSpace();
            }

            source.SkipWhiteSpace();
            source.Expect('?');
            source.SkipIt();

            source.SkipWhiteSpace();
            source.Expect('>');
            source.SkipIt();

            return result;
        }
    }
}