namespace Ridge.Nodes
{
    public class DocType : Node
    {
        public string Name { get; set; }
        public string Declaration { get; set; }

        public override string ToString()
        {
            return string.Format("<{0}{1}>\n", Name, Declaration);
        }
    }
}