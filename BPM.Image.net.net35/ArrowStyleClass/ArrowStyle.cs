namespace BPM.Image.net.ArrowStyleClass
{
    /// <summary>
    ///     箭头样式
    /// </summary>
    public class ArrowStyle
    {
        /// <summary>
        ///     构造箭头样式
        /// </summary>
        public ArrowStyle()
        {
            HeadStyle = new HeadStyle();
            CurveStyle = new CurveStyle();
        }
        /// <summary>
        ///     头部样式
        /// </summary>
        public HeadStyle HeadStyle { get; set; }
        /// <summary>
        ///     曲线样式
        /// </summary>
        public CurveStyle CurveStyle { get; set; }
    }
}