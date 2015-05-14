using System;

namespace LeetCode.Solutions.Argorithms
{
    public class _110_BalancedBinaryTree
    {
        public bool IsBalanced(TreeNode root)
        {
            var depth = GetDepth(root);
            return depth.Item1;
        }

        private static Tuple<bool, int> GetDepth(TreeNode node)
        {
            if (node == null)
            {
                return Tuple.Create(true, 0);
            }

            if (node.left == null
                && node.right == null)
            {
                return Tuple.Create(true, 1);
            }
            var left = GetDepth(node.left);
            if (!left.Item1)
            {
                return Tuple.Create(false, 0);
            }

            var right = GetDepth(node.right);
            if (!right.Item1)
            {
                return Tuple.Create(false, 0);
            }

            if (left.Item2 - right.Item2 <= 1
                && left.Item2 - right.Item2 >= -1)
            {
                return Tuple.Create(true, Math.Max(left.Item2, right.Item2) + 1);
            }

            return Tuple.Create(false, 0);
        }
    }
}