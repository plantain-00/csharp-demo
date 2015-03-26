namespace XmlConverter.Nodes
{
    public abstract class XmlBase
    {
        public abstract string ToString(Formatting formatting, int spaceNumber = 4);

        public override string ToString()
        {
            return ToString(Formatting.None);
        }
    }
}