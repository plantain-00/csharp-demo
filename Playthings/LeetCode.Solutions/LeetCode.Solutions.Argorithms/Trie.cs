namespace LeetCode.Solutions.Argorithms
{
    public class Trie
    {
        private readonly TrieNode _root;

        public Trie()
        {
            _root = new TrieNode();
        }

        public void Insert(string word)
        {
            _root.Insert(word, 0);
        }

        public bool Search(string word)
        {
            return _root.Search(word, 0);
        }

        public bool StartsWith(string word)
        {
            return _root.StartsWith(word, 0);
        }
    }
}