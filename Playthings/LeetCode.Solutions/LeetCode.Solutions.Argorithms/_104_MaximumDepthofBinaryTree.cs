using System;

namespace LeetCode.Solutions.Argorithms
{
    public class _104_MaximumDepthofBinaryTree
    {
        public int MaxDepth(TreeNode root)
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

                return MaxDepth(root.right) + 1;
            }

            if (root.right == null)
            {
                return MaxDepth(root.left) + 1;
            }

            return Math.Max(MaxDepth(root.left), MaxDepth(root.right)) + 1;
        }
    }
}