using ParseLibrary;

namespace XmlConverter
{
    public class Document : FormattingBase
    {
        public Declaration Declaration { get; set; }
        public Element Body { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            if (formatting == Formatting.None)
            {
                return Declaration.ToString(formatting) + Body.ToString(formatting);
            }
            return Declaration.ToString(formatting) + "\n" + Body.ToString(formatting);
        }

        public static Document Create(string xml)
        {
            var source = new Source(xml);

            source.SkipBlankSpaces();

            var result = new Document
                         {
                             Declaration = Declaration.Create(source)
                         };
            source.SkipBlankSpaces();
            result.Body = Element.Create(source, 0);

            source.SkipBlankSpaces();
            source.ExpectEnd();

            return result;
        }
    }
}