using System.Drawing;

using BPM.Image.net.ArrowStyleClass;
using BPM.Image.net.NodeStyleClass;

namespace BPM.Image.net
{
    /// <summary>
    ///     流程图静态设置
    /// </summary>
    public static class ProcessStatic
    {
        /// <summary>
        ///     节点之间的空隙宽度，默认为100px
        /// </summary>
        public static int SpacingWidth = 100;
        /// <summary>
        ///     节点之间的空隙高度，默认为40px
        /// </summary>
        public static int SpacingHeight = 40;
        /// <summary>
        ///     图片Padding，默认为10px
        /// </summary>
        public static int Padding = 10;
        /// <summary>
        ///     图片边框Pen，默认为红色实线
        /// </summary>
        public static Pen BorderPen = new Pen(Color.Red);
        /// <summary>
        ///     图片背景色，默认为(240, 240, 240)
        /// </summary>
        public static Brush Background = new SolidBrush(Color.FromArgb(240, 240, 240));
        /// <summary>
        ///     节点的宽度，默认为50px
        /// </summary>
        public static int Width = 50;
        /// <summary>
        ///     节点的高度，默认为30px
        /// </summary>
        public static int Height = 30;
        internal static int GetImageWidth(int depth)
        {
            return 2 * Padding + depth * (Width + SpacingWidth) - SpacingWidth;
        }
        internal static int GetImageHeight(int branchCount)
        {
            return 2 * Padding + branchCount * (Height + SpacingHeight) - SpacingHeight;
        }
        internal static WordsNode DrawWords(Graphics graphics, string words, int xIndex, float yIndex, NodeStyle nodeStyle)
        {
            return DrawWords(graphics, words, X(xIndex), Y(yIndex), nodeStyle);
        }
        private static WordsNode DrawWords(Graphics graphics, string words, float x, float y, NodeStyle nodeStyle)
        {
            switch (nodeStyle.Shape)
            {
                case ProcessNodeShape.Ellipse:
                    graphics.DrawEllipse(nodeStyle.BorderPen, x, y, Width, Height);
                    break;
                case ProcessNodeShape.Rectangle:
                    graphics.DrawRectangle(nodeStyle.BorderPen, x, y, Width, Height);
                    break;
                case ProcessNodeShape.EllipseBlock:
                    graphics.FillEllipse(nodeStyle.WordsStyle.BackgroundBrush, x, y, Width, Height);
                    break;
                case ProcessNodeShape.RectangleBlock:
                    graphics.FillRectangle(nodeStyle.WordsStyle.BackgroundBrush, x, y, Width, Height);
                    break;
            }
            graphics.DrawString(words,
                                nodeStyle.WordsStyle.Font,
                                nodeStyle.WordsStyle.Brush,
                                new RectangleF(x, y, Width, Height),
                                new StringFormat
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                });
            return new WordsNode
                   {
                       Entrance = new PointF(x, y + Height / 2),
                       Exit = new PointF(x + Width, y + Height / 2)
                   };
        }
        internal static WordsNode GetWordNode(int xIndex, float yIndex)
        {
            var x = X(xIndex);
            var y = Y(yIndex);
            return new WordsNode
                   {
                       Entrance = new PointF(x, y + Height / 2),
                       Exit = new PointF(x + Width, y + Height / 2)
                   };
        }
        private static void DrawArrow(Graphics graphics, float x1, float y1, float x2, float y2, ArrowStyle style)
        {
            var length = (x2 - x1) * style.CurveStyle.ControlPointsRate;
            graphics.DrawBezier(style.CurveStyle.Pen, x1, y1, x1 + length, y1, x2 - length, y2, x2, y2);
            graphics.FillPolygon(style.HeadStyle.Brush,
                                 new[]
                                 {
                                     new PointF(x2, y2),
                                     new PointF(x2 - style.HeadStyle.Width, y2 - style.HeadStyle.Height),
                                     new PointF(x2 - style.HeadStyle.Width, y2 + style.HeadStyle.Height)
                                 });
        }
        internal static void PointTo(Graphics graphics, WordsNode from, WordsNode to, ArrowStyle arrowStyle)
        {
            DrawArrow(graphics, from.Exit.X, from.Exit.Y, to.Entrance.X, to.Entrance.Y, arrowStyle);
        }
        private static float Y(float j)
        {
            return Padding + (Height + SpacingHeight) * j;
        }
        private static float X(int i)
        {
            return Padding + (Width + SpacingWidth) * i;
        }
    }
}