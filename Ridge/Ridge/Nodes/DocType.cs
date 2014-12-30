namespace Ridge.Nodes
{
    public class DocType : Node
    {
        public string Name { get; set; }
        public string Declaration { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            if (formatting == Formatting.None)
            {
                return string.Format("<{0}{1}>", Name, Declaration);
            }
            return string.Format("<{0}{1}>\n", Name, Declaration);
        }

        public override string ToString()
        {
            return ToString(Formatting.Indented);
        }
    }
}