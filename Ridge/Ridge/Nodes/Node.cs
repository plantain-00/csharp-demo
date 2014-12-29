using System.Collections.Generic;

namespace Ridge.Nodes
{
    public class Node
    {
        public List<Node> Children { get; set; }
        internal int Depth { get; set; }
    }
}