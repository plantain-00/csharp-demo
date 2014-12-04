using System.Drawing;

namespace BPM.Image.net.NodeStyleClass
{
    /// <summary>
    ///     节点样式
    /// </summary>
    public class NodeStyle
    {
        /// <summary>
        ///     默认节点边框颜色，默认为黑色实线
        /// </summary>
        public static Pen DefaultBorderPen = new Pen(Brushes.Black);
        /// <summary>
        ///     默认节点形状，默认为矩形
        /// </summary>
        public static ProcessNodeShape DefaultShape = ProcessNodeShape.Rectangle;
        /// <summary>
        ///     构造节点样式
        /// </summary>
        public NodeStyle()
        {
            BorderPen = DefaultBorderPen;
            Shape = DefaultShape;
            WordsStyle = new WordsStyle();
        }
        /// <summary>
        ///     节点边框颜色，默认为黑色实线
        /// </summary>
        public Pen BorderPen { get; set; }
        /// <summary>
        ///     节点形状，默认为矩形
        /// </summary>
        public ProcessNodeShape Shape { get; set; }
        /// <summary>
        ///     文字样式
        /// </summary>
        public WordsStyle WordsStyle { get; set; }
    }
}