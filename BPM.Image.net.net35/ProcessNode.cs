using System;
using System.Collections.Generic;
using System.Drawing;

using BPM.Image.net.ArrowStyleClass;
using BPM.Image.net.NodeStyleClass;

namespace BPM.Image.net
{
    /// <summary>
    ///     流程节点
    /// </summary>
    public class ProcessNode
    {
        private readonly ProcessModel _process;
        /// <summary>
        ///     样式
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
        ///     流程节点名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        ///     紧随的节点集合
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
        ///     紧随一个新节点
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
        ///     紧随一个新节点
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
        ///     附加到一个已存在的节点
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
        ///     附加到一个已存在的节点
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
        ///     绘制文字之前的事件
        /// </summary>
        public event Action OnWordsDrawing;
        /// <summary>
        ///     绘制文字之后的事件
        /// </summary>
        public event Action OnWordsDrawn;
        /// <summary>
        ///     绘制射出的箭头之前的事件
        /// </summary>
        public event Action<ProcessNode, ProcessNode> OnFromPointing;
        /// <summary>
        ///     绘制射出的箭头之后的事件
        /// </summary>
        public event Action<ProcessNode, ProcessNode> OnFromPointed;
#endif
    }
}