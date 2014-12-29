namespace Ridge.Nodes
{
    public class Attribute
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            if (Value == null)
            {
                return Name;
            }
            return string.Format("{0}=\"{1}\"", Name, Value);
        }
    }
}