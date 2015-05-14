using System;

namespace LeetCode.Solutions.Argorithms
{
    public class _111_MinimumDepthofBinaryTree
    {
        public int MinDepth(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            if (root.left == null)
            {
                if (root.right == null)
                {
                    return 1;
                }
                return MinDepth(root.right) + 1;
            }

            if (root.right == null)
            {
                return MinDepth(root.left) + 1;
            }

            return Math.Min(MinDepth(root.left), MinDepth(root.right)) + 1;
        }
    }
}