using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Solutions.Argorithms
{
    public class _199_BinaryTreeRightSideView
    {
        public IList<int> RightSideView(TreeNode root)
        {
            if (root == null)
            {
                return new List<int>();
            }
            var left = RightSideView(root.left);
            var right = RightSideView(root.right);
            var result = new List<int>
                         {
                             root.val
                         };
            result.AddRange(right);
            if (left.Count > right.Count)
            {
                result.AddRange(left.Skip(right.Count));
            }
            return result;
        }
    }
}