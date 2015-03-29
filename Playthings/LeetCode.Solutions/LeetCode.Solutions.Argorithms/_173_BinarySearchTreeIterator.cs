using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class BSTIterator
    {
        private readonly Stack<TreeNode> _stack;
        private TreeNode _current;

        public BSTIterator(TreeNode root)
        {
            _current = root;
            _stack = new Stack<TreeNode>();
        }

        public bool HasNext()
        {
            return _stack.Count != 0 || _current != null;
        }

        public int Next()
        {
            while (_current != null)
            {
                _stack.Push(_current);
                _current = _current.left;
            }
            var t = _stack.Pop();
            _current = t.right;
            return t.val;
        }
    }

    public class TreeNode
    {
        public TreeNode left;
        public TreeNode right;
        public int val;

        public TreeNode(int x)
        {
            val = x;
        }
    }
}