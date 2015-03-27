namespace XmlConverter.Nodes
{
    public class Document : XmlBase
    {
        public Declaration Declaration { get; set; }
        public Element Body { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            return string.Format("{0}{1}", Declaration, Body);
        }

        public static Document Create(string xml)
        {
            var source = new Source(xml);

            source.SkipWhiteSpace();

            var result = new Document
                         {
                             Declaration = Declaration.Create(source)
                         };
            source.SkipWhiteSpace();
            result.Body = Element.Create(source);

            source.SkipWhiteSpace();
            if (!source.IsTail)
            {
                throw new ParseException(source);
            }

            return result;
        }
    }
}