using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _82_RemoveDuplicatesfromSortedListIITests
    {
        [TestMethod]
        public void DeleteDuplicatesTest()
        {
            var tmp = new ListNode(1)
                      {
                          next = new ListNode(1)
                                 {
                                     next = new ListNode(2)
                                            {
                                                next = new ListNode(3)
                                                       {
                                                           next = new ListNode(3)
                                                       }
                                            }
                                 }
                      };
            var a = new _82_RemoveDuplicatesfromSortedListII().DeleteDuplicates(tmp);
        }
    }
}