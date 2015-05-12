using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Solutions.Argorithms
{
    public class _207_CourseSchedule
    {
        public bool CanFinish(List<Edge<int>> prerequisites)
        {
            var indexes = new List<int>();


            while (prerequisites.Count > 0)
            {
                for (var i = 0; i < prerequisites.Count; i++)
                {
                    var edge = prerequisites[i];
                    if (prerequisites.All(p => p.To != edge.From))
                    {
                        indexes.Add(i);
                    }
                }
                if (indexes.Count == 0)
                {
                    return false;
                }
                for (var i = indexes.Count - 1; i >= 0; i--)
                {
                    prerequisites.RemoveAt(i);
                }
                indexes.Clear();
            }

            return true;
        }
    }

    public class Edge<T>
    {
        public Edge(T from, T to)
        {
            From = from;
            To = to;
        }

        public T From { get; set; }
        public T To { get; set; }
    }
}