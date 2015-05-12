using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _207_CourseScheduleTests
    {
        [TestMethod]
        public void CanFinishTest()
        {
            var tmp = new _207_CourseSchedule();
            var graph = new List<Edge<int>>
                        {
                            new Edge<int>(1, 0)
                        };
            Assert.IsTrue(tmp.CanFinish(graph));

            graph = new List<Edge<int>>
                    {
                        new Edge<int>(1, 0),
                        new Edge<int>(0, 1)
                    };
            Assert.IsFalse(tmp.CanFinish(graph));

            graph = new List<Edge<int>>
                    {
                        new Edge<int>(0, 1),
                        new Edge<int>(1, 2),
                        new Edge<int>(2, 3),
                        new Edge<int>(3, 4),
                        new Edge<int>(2, 4),
                        new Edge<int>(2, 5)
                    };
            Assert.IsTrue(tmp.CanFinish(graph));

            graph = new List<Edge<int>>
                    {
                        new Edge<int>(0, 1),
                        new Edge<int>(1, 2),
                        new Edge<int>(2, 3),
                        new Edge<int>(3, 4),
                        new Edge<int>(2, 4),
                        new Edge<int>(4, 1)
                    };
            Assert.IsFalse(tmp.CanFinish(graph));
        }
    }
}