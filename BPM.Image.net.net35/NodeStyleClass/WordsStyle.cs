using System.Drawing;

namespace BPM.Image.net.NodeStyleClass
{
    /// <summary>
    ///     ������ʽ
    /// </summary>
    public class WordsStyle
    {
        /// <summary>
        ///     Ĭ�Ͻڵ��������壬Ĭ��ΪϵͳĬ������
        /// </summary>
        public static Font DefaultFont = SystemFonts.DefaultFont;
        /// <summary>
        ///     Ĭ�Ͻڵ�������ɫ��Ĭ��ΪϵͳĬ���ı���ɫ
        /// </summary>
        public static Brush DefaultBrush = SystemBrushes.ControlText;
        /// <summary>
        ///     Ĭ�Ͻڵ����ֱ���ɫ��Ĭ��Ϊ(220, 220, 220)
        /// </summary>
        public static Brush DefaultBackgroundBrush = new SolidBrush(Color.FromArgb(220, 220, 220));
        /// <summary>
        ///     ����������ʽ
        /// </summary>
        public WordsStyle()
        {
            Font = DefaultFont;
            Brush = DefaultBrush;
            BackgroundBrush = DefaultBackgroundBrush;
        }
        /// <summary>
        ///     �ڵ��������壬Ĭ��ΪϵͳĬ������
        /// </summary>
        public Font Font { get; set; }
        /// <summary>
        ///     �ڵ�������ɫ��Ĭ��ΪϵͳĬ���ı���ɫ
        /// </summary>
        public Brush Brush { get; set; }
        /// <summary>
        ///     �ڵ����ֱ���ɫ��Ĭ��Ϊ(220, 220, 220)
        /// </summary>
        public Brush BackgroundBrush { get; set; }
    }
}