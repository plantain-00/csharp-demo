using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _51_NQueensTests
    {
        [TestMethod]
        public void SolveNQueensTest()
        {
            var tmp = new _51_NQueens();
            var queens = tmp.SolveNQueens(4);
        }
    }
}