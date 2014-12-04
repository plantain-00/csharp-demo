using BPM.Image.net.ArrowStyleClass;

namespace BPM.Image.net
{
    /// <summary>
    ///     箭头
    /// </summary>
    public class ArrowModel
    {
        /// <summary>
        ///     样式
        /// </summary>
        public ArrowStyle Style { get; set; }
        /// <summary>
        ///     发出节点
        /// </summary>
        public ProcessNode From { get; set; }
        /// <summary>
        ///     接收节点
        /// </summary>
        public ProcessNode To { get; set; }
        internal bool IsDrawn { get; set; }
    }
}