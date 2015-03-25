namespace SingleLinkedListReverse
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var node = new Node<int>
                       {
                           Value = 0,
                           NextNode = new Node<int>
                                      {
                                          Value = 1,
                                          NextNode = new Node<int>
                                                     {
                                                         Value = 2,
                                                         NextNode = new Node<int>
                                                                    {
                                                                        Value = 3
                                                                    }
                                                     }
                                      }
                       };
            var newNode = node.Reverse();
        }
    }

    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> NextNode { get; set; }

        public Node<T> Reverse()
        {
            var result = this;
            var current = this;
            while (NextNode != null)
            {
                result = NextNode;
                NextNode = result.NextNode;
                result.NextNode = current;
                current = result;
            }

            return result;
        }
    }
}