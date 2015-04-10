using ParseLibrary;

namespace XmlConverter
{
    public abstract class Node : FormattingBase
    {
        public int Depth { get; set; }
    }
}