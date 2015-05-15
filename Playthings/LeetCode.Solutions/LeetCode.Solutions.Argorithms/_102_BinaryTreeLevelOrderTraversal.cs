using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _102_BinaryTreeLevelOrderTraversal
    {
        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            var result = new List<IList<int>>();
            LevelBuilder(result, root, 0);
            return result;
        }

        private static void LevelBuilder(IList<IList<int>> list, TreeNode root, int level)
        {
            if (root == null)
            {
                return;
            }
            if (level >= list.Count)
            {
                list.Add(new List<int>());
            }
            LevelBuilder(list, root.left, level + 1);
            LevelBuilder(list, root.right, level + 1);
            list[level].Add(root.val);
        }
    }
}