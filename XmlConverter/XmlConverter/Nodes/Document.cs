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

            return Attribute.Create(source);
        }
    }
}