using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using BPM.Image.net.ArrowStyleClass;
using BPM.Image.net.NodeStyleClass;

namespace BPM.Image.net.Example.net35
{
    internal class Program
    {
        private static void Main()
        {
            CurveStyle.DefaultPen = new Pen(Color.Black, 1)
                                    {
                                        DashStyle = DashStyle.Custom,
                                        DashPattern = new float[]
                                                      {
                                                          5,
                                                          5
                                                      }
                                    };
            var process = new ProcessModel("Test流程", 3, 7, "开始");
            process.StartNode.PointTo("A", 1, 1).PointTo("B1", 2, 0.5f).PointTo("C", 3, 1).PointTo("D", 4, 2).PointTo("E", 5, 1).PointTo("结束", 6, 1);
            process["A"].PointTo("B2", 2, 1.5f).AttachTo("D");
            process["B1"].PointTo("C2", 3, 0).AttachTo("E");
            process.StartNode.Style.Shape = ProcessNodeShape.Ellipse;
            process["结束"].Style.Shape = ProcessNodeShape.Ellipse;
            ProcessStatic.SpacingWidth = 70;
            Action<string, string> arrowAction = (from, to) =>
                                                 {
                                                     var style = process[from, to].Style;
                                                     style.CurveStyle.Pen = new Pen(Brushes.Red);
                                                     style.HeadStyle.Brush = Brushes.Red;
                                                 };
            Action<string> nodeAction = s =>
                                        {
                                            var style = process[s].Style;
                                            style.WordsStyle.Brush = Brushes.Red;
                                        };
            nodeAction("开始");
            nodeAction("A");
            nodeAction("B1");
            nodeAction("B2");
            nodeAction("C");
            nodeAction("D");
            arrowAction("开始", "A");
            arrowAction("A", "B1");
            arrowAction("A", "B2");
            arrowAction("B1", "C");
            arrowAction("B2", "D");
            process.ToBitmap().Save("c.bmp");
        }
    }
}