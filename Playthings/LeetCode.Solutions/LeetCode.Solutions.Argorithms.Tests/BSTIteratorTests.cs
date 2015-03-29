using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class BSTIteratorTests
    {
        [TestMethod]
        public void NextTest()
        {
            var tree = new TreeNode(4)
                       {
                           left = new TreeNode(2)
                                  {
                                      left = new TreeNode(1),
                                      right = new TreeNode(3)
                                  },
                           right = new TreeNode(6)
                                   {
                                       left = new TreeNode(5),
                                       right = new TreeNode(7)
                                   }
                       };

            var iterator = new BSTIterator(tree);

            var i = 0;

            while (iterator.HasNext())
            {
                i++;
                Assert.IsTrue(iterator.Next() == i);
            }
        }
    }
}