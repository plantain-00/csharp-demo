namespace LeetCode.Solutions.Argorithms
{
    public class _101_SymmetricTree
    {
        public bool IsSymmetric(TreeNode root)
        {
            if (root == null)
            {
                return true;
            }
            return IsSymmetricPrivately(root.left, root.right);
        }

        private static bool IsSymmetricPrivately(TreeNode node1, TreeNode node2)
        {
            if (node1 == null
                && node2 == null)
            {
                return true;
            }
            if (node1 == null
                || node2 == null)
            {
                return false;
            }
            if (node1.val != node2.val)
            {
                return false;
            }
            return IsSymmetricPrivately(node1.left, node2.right) && IsSymmetricPrivately(node1.right, node2.left);
        }
    }
}