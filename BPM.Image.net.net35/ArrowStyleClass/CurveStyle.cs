using System.Drawing;

namespace BPM.Image.net.ArrowStyleClass
{
    /// <summary>
    ///     箭头曲线样式
    /// </summary>
    public class CurveStyle
    {
        /// <summary>
        ///     默认曲线控制点比例，默认为0.4，值越大,弯曲越严重,参见"贝塞尔曲线"
        /// </summary>
        public static float DefaultControlPointsRate = 0.4f;
        /// <summary>
        ///     默认箭头曲线Pen，默认为黑色实线
        /// </summary>
        public static Pen DefaultPen = new Pen(Brushes.Black);
        /// <summary>
        ///     构造曲线样式
        /// </summary>
        public CurveStyle()
        {
            ControlPointsRate = DefaultControlPointsRate;
            Pen = DefaultPen;
        }
        /// <summary>
        ///     曲线控制点比例，默认为0.4，值越大,弯曲越严重,参见"贝塞尔曲线"
        /// </summary>
        public float ControlPointsRate { get; set; }
        /// <summary>
        ///     箭头曲线Pen，默认为黑色实线
        /// </summary>
        public Pen Pen { get; set; }
    }
}