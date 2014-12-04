using System.Collections.Generic;
using System.Drawing;

namespace BPM.Image.net
{
    /// <summary>
    ///     流程
    /// </summary>
    public class ProcessModel
    {
        internal readonly List<ArrowModel> Arrows = new List<ArrowModel>();
        private readonly int _branchCount;
        private readonly int _depth;
        /// <summary>
        ///     流程
        /// </summary>
        /// <param name="name"></param>
        /// <param name="branchCount"></param>
        /// <param name="depth"></param>
        /// <param name="startNode"></param>
        public ProcessModel(string name, int branchCount, int depth, string startNode)
        {
            Name = name;
            _branchCount = branchCount;
            _depth = depth;
            StartNode = new ProcessNode(startNode, 0, _branchCount / 2f - 0.5f, this);
        }
        /// <summary>
        ///     流程名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     流程开始节点
        /// </summary>
        public ProcessNode StartNode { get; private set; }
        /// <summary>
        ///     按节点名索引节点
        /// </summary>
        /// <param name="name"></param>
        public ProcessNode this[string name]
        {
            get
            {
                return StartNode.Search(name);
            }
        }
        /// <summary>
        ///     由射入节点和射出节点索引箭头样式
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public ArrowModel this[string from, string to]
        {
            get
            {
                return Arrows.Find(a => a.From.Name == from && a.To.Name == to);
            }
        }
        /// <summary>
        ///     转换为Bitmap图片
        /// </summary>
        /// <returns></returns>
        public Bitmap ToBitmap()
        {
            var width = ProcessStatic.GetImageWidth(_depth);
            var height = ProcessStatic.GetImageHeight(_branchCount);
            var image = new Bitmap(width, height);
            foreach (var arrow in Arrows)
            {
                arrow.IsDrawn = false;
            }
            StartNode.Reset();
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.FillRectangle(ProcessStatic.Background, 0, 0, width - 1, height - 1);
                graphics.DrawRectangle(ProcessStatic.BorderPen, 0, 0, width - 1, height - 1);
                StartNode.Draw(graphics);
            }
            return image;
        }
    }
}