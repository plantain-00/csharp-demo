namespace XmlConverter.Nodes
{
    public class Document : XmlBase
    {
        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            throw new System.NotImplementedException();
        }

        public static XmlBase Create(string xml)
        {
            var source = new Source(xml);

            source.SkipWhiteSpace();

            //var result = Attribute.Create(source);
            var result = Comment.Create(source);

            source.SkipWhiteSpace();
            if (!source.IsTail)
            {
                throw new ParseException(source);
            }

            return result;
        }
    }
}