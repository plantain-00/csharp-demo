using System.Drawing;

namespace BPM.Image.net.ArrowStyleClass
{
    /// <summary>
    ///     箭头头部样式
    /// </summary>
    public class HeadStyle
    {
        /// <summary>
        ///     默认箭头头部高度，默认为4px
        /// </summary>
        public static int DefaultHeight = 4;
        /// <summary>
        ///     默认箭头头部颜色，默认为黑色
        /// </summary>
        public static Brush DefaultBrush = Brushes.Black;
        /// <summary>
        ///     默认箭头头部宽度,默认为10px
        /// </summary>
        public static int DefaultWidth = 10;
        /// <summary>
        ///     构造头部样式
        /// </summary>
        public HeadStyle()
        {
            Height = DefaultHeight;
            Brush = DefaultBrush;
            Width = DefaultWidth;
        }
        /// <summary>
        ///     箭头头部高度，默认为4px
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        ///     箭头头部宽度,默认为10px
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        ///     箭头头部颜色，默认为黑色
        /// </summary>
        public Brush Brush { get; set; }
    }
}