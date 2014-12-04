using System;
using System.Collections.Generic;
using System.Drawing;

using BPM.Image.net.ArrowStyleClass;
using BPM.Image.net.NodeStyleClass;

namespace BPM.Image.net
{
    /// <summary>
    ///     ���̽ڵ�
    /// </summary>
    public class ProcessNode
    {
        private readonly ProcessModel _process;
        /// <summary>
        ///     ��ʽ
        /// </summary>
        public NodeStyle Style = new NodeStyle();
        internal ProcessNode(string name, int xIndex, float yIndex, ProcessModel process)
        {
            Name = name;
            Next = new List<ProcessNode>();
            XIndex = xIndex;
            YIndex = yIndex;
            _process = process;
        }
        /// <summary>
        ///     ���̽ڵ�����
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        ///     ����Ľڵ㼯��
        /// </summary>
        public List<ProcessNode> Next { get; private set; }
        internal int XIndex { get; private set; }
        internal float YIndex { get; private set; }
        private bool _isDrawn;
        internal void Reset()
        {
            _isDrawn = false;
            foreach (var node in Next)
            {
                node.Reset();
            }
        }
        /// <summary>
        ///     ����һ���½ڵ�
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public ProcessNode PointTo(ProcessNode node)
        {
            _process.Arrows.Add(new ArrowModel
                                {
                                    From = this,
                                    To = node,
                                    Style = new ArrowStyle()
                                });
            Next.Add(node);
            return node;
        }
        /// <summary>
        ///     ����һ���½ڵ�
        /// </summary>
        /// <param name="name"></param>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        /// <returns></returns>
        public ProcessNode PointTo(string name, int xIndex, float yIndex)
        {
            return PointTo(new ProcessNode(name, xIndex, yIndex, _process));
        }
        /// <summary>
        ///     ���ӵ�һ���Ѵ��ڵĽڵ�
        /// </summary>
        /// <param name="node"></param>
        public void AttachTo(ProcessNode node)
        {
            _process.Arrows.Add(new ArrowModel
                                {
                                    From = this,
                                    To = node,
                                    Style = new ArrowStyle()
                                });
            Next.Add(node);
        }
        /// <summary>
        ///     ���ӵ�һ���Ѵ��ڵĽڵ�
        /// </summary>
        /// <param name="name"></param>
        public void AttachTo(string name)
        {
            AttachTo(_process.StartNode.Search(name));
        }
        internal WordsNode Draw(Graphics graphics)
        {
#if !NET30 && !NET20
            if (OnWordsDrawing != null)
            {
                OnWordsDrawing();
            }
#endif
            WordsNode wordsNode;
            if (!_isDrawn)
            {
                wordsNode = ProcessStatic.DrawWords(graphics, Name, XIndex, YIndex, Style);
                _isDrawn = true;
            }
            else
            {
                wordsNode = ProcessStatic.GetWordNode(XIndex, YIndex);
            }
#if !NET30 && !NET20
            if (OnWordsDrawn != null)
            {
                OnWordsDrawn();
            }
#endif
            foreach (var node in Next)
            {
                var to = node.Draw(graphics);
#if !NET30 && !NET20
                if (OnFromPointing != null)
                {
                    OnFromPointing(this, node);
                }
#endif
                var arrowModel = _process[Name, node.Name];
                if (!arrowModel.IsDrawn)
                {
                    ProcessStatic.PointTo(graphics, wordsNode, to, arrowModel.Style);
                    arrowModel.IsDrawn = true;
                }
#if !NET30 && !NET20
                if (OnFromPointed != null)
                {
                    OnFromPointed(this, node);
                }
#endif
            }
            return wordsNode;
        }
        internal ProcessNode Search(string name)
        {
            if (name == Name)
            {
                return this;
            }
            foreach (var node in Next)
            {
                var result = node.Search(name);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
#if !NET30 && !NET20
        /// <summary>
        ///     ��������֮ǰ���¼�
        /// </summary>
        public event Action OnWordsDrawing;
        /// <summary>
        ///     ��������֮����¼�
        /// </summary>
        public event Action OnWordsDrawn;
        /// <summary>
        ///     ��������ļ�ͷ֮ǰ���¼�
        /// </summary>
        public event Action<ProcessNode, ProcessNode> OnFromPointing;
        /// <summary>
        ///     ��������ļ�ͷ֮����¼�
        /// </summary>
        public event Action<ProcessNode, ProcessNode> OnFromPointed;
#endif
    }
}