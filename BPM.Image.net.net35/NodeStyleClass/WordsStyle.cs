using System.Drawing;

namespace BPM.Image.net.NodeStyleClass
{
    /// <summary>
    ///     文字样式
    /// </summary>
    public class WordsStyle
    {
        /// <summary>
        ///     默认节点文字字体，默认为系统默认字体
        /// </summary>
        public static Font DefaultFont = SystemFonts.DefaultFont;
        /// <summary>
        ///     默认节点文字颜色，默认为系统默认文本颜色
        /// </summary>
        public static Brush DefaultBrush = SystemBrushes.ControlText;
        /// <summary>
        ///     默认节点文字背景色，默认为(220, 220, 220)
        /// </summary>
        public static Brush DefaultBackgroundBrush = new SolidBrush(Color.FromArgb(220, 220, 220));
        /// <summary>
        ///     构造文字样式
        /// </summary>
        public WordsStyle()
        {
            Font = DefaultFont;
            Brush = DefaultBrush;
            BackgroundBrush = DefaultBackgroundBrush;
        }
        /// <summary>
        ///     节点文字字体，默认为系统默认字体
        /// </summary>
        public Font Font { get; set; }
        /// <summary>
        ///     节点文字颜色，默认为系统默认文本颜色
        /// </summary>
        public Brush Brush { get; set; }
        /// <summary>
        ///     节点文字背景色，默认为(220, 220, 220)
        /// </summary>
        public Brush BackgroundBrush { get; set; }
    }
}