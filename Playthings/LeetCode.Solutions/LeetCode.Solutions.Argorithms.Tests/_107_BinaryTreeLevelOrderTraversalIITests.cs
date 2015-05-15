using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _107_BinaryTreeLevelOrderTraversalIITests
    {
        [TestMethod]
        public void LevelOrderBottomTest()
        {
            var root = new TreeNode(3)
                       {
                           left = new TreeNode(9),
                           right = new TreeNode(20)
                                   {
                                       left = new TreeNode(15),
                                       right = new TreeNode(7)
                                   }
                       };
            var tmp = new _107_BinaryTreeLevelOrderTraversalII().LevelOrderBottom(root);
        }
    }
}