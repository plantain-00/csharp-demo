using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _19_RemoveNthNodeFromEndofListTests
    {
        [TestMethod]
        public void RemoveNthFromEndTest()
        {
            var node = new ListNode(1)
                       {
                           next = new ListNode(2)
                                  {
                                      next = new ListNode(3)
                                             {
                                                 next = new ListNode(4)
                                                        {
                                                            next = new ListNode(5)
                                                        }
                                             }
                                  }
                       };
            var a = new _19_RemoveNthNodeFromEndofList().RemoveNthFromEnd(node, 2);
        }
    }
}