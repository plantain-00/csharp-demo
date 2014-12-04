using System.Windows.Controls;

namespace MineSweeper.Interfaces
{
    internal interface IContext
    {
        IResult ResultInstance { get; }
        void ClickRandomly();
        void Reset();
        void Replay();
        void InitButtons(Grid grid);
    }
}