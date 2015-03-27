using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlConverter.Nodes
{
    public class Declaration : XmlBase
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
            source.MoveForward();

            source.SkipWhiteSpace();
            source.Expect('?');
            source.MoveForward();

            source.SkipWhiteSpace();
            source.Expect("xml", true);
            source.MoveForward("xml".Length);

            var result = new Declaration();
            source.SkipWhiteSpace();
            while ("/>=?".All(c => source.IsNot(c)))
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
            source.MoveForward();

            source.SkipWhiteSpace();
            source.Expect('>');
            source.MoveForward();

            return result;
        }
    }
}