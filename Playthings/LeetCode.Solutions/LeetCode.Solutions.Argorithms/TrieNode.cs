namespace LeetCode.Solutions.Argorithms
{
    public class TrieNode
    {
        public bool IsWord { get; set; }

        public TrieNode[] Children { get; set; }

        public void Insert(string word, int index)
        {
            if (index == word.Length)
            {
                IsWord = true;
                return;
            }

            var i = word[index] - 'a';

            if (Children == null)
            {
                Children = new TrieNode[26];
            }

            if (Children[i] == null)
            {
                Children[i] = new TrieNode();
            }

            Children[i].Insert(word, index + 1);
        }

        public bool Search(string word, int index)
        {
            if (index == word.Length)
            {
                return IsWord;
            }

            var i = word[index] - 'a';

            if (Children == null
                || Children[i] == null)
            {
                return false;
            }

            return Children[i].Search(word, index + 1);
        }

        public bool StartsWith(string word, int index)
        {
            if (index == word.Length)
            {
                return true;
            }

            var i = word[index] - 'a';

            if (Children == null
                || Children[i] == null)
            {
                return false;
            }

            return Children[i].StartsWith(word, index + 1);
        }
    }
}