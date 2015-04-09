using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _199_BinaryTreeRightSideView
    {
        private int _current;

        public IList<int> RightSideView(TreeNode root)
        {
            var result = new List<int>();
            Gen(root, 1, result);
            return result;
        }

        private void Gen(TreeNode root, int level, ICollection<int> result)
        {
            while (true)
            {
                if (root == null)
                {
                    return;
                }
                if (level > _current)
                {
                    result.Add(root.val);
                    _current = level;
                }
                Gen(root.right, level + 1, result);
                root = root.left;
                level = level + 1;
            }
        }
    }
}