using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _168_ExcelSheetColumnTitleTests
    {
        [TestMethod]
        public void ConvertToTitleTest()
        {
            Assert.IsTrue(new _168_ExcelSheetColumnTitle().ConvertToTitle(1) == "A");
        }
    }
}