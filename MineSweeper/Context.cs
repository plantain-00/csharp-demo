using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;

using MineSweeper.Interfaces;

namespace MineSweeper
{
    internal class Context : IContext
    {
        private readonly IPosition[,] _positionArray;
        private readonly Random _randomInstance;
        private readonly DispatcherTimer _timerInstance;
        private DateTimeOffset? _startTime;
        public Context()
        {
            ResultInstance = IocContainer.Resolve<IResult>();
            _timerInstance = new DispatcherTimer
                             {
                                 Interval = new TimeSpan(0,
                                                         0,
                                                         0,
                                                         0,
                                                         1000),
                             };
            _timerInstance.Tick += OnTick;
            _timerInstance.Start();
            _positionArray = new IPosition[Constants.ROW_NUMBER, Constants.COLUMN_NUMBER];
            for (var i = 0;
                 i < Constants.ROW_NUMBER;
                 i++)
            {
                for (var j = 0;
                     j < Constants.COLUMN_NUMBER;
                     j++)
                {
                    _positionArray[i,
                                   j] = IocContainer.Resolve<IPosition>();
                }
            }
            _randomInstance = new Random();
            ResetValue();
        }
        public IResult ResultInstance { get; private set; }
        public void ClickRandomly()
        {
            var value = _randomInstance.Next(ResultInstance.UndiggedNumber);
            for (var i = 0;
                 i < Constants.ROW_NUMBER;
                 i++)
            {
                for (var j = 0;
                     j < Constants.COLUMN_NUMBER;
                     j++)
                {
                    if (_positionArray[i,
                                       j].IsUndigged)
                    {
                        if (value == 0)
                        {
                            ClickButtonLeft(i,
                                            j);
                            return;
                        }
                        value--;
                    }
                }
            }
        }
        public void Reset()
        {
            ResetValue();
            Replay();
        }
        public void Replay()
        {
            ResetPosition();
            _startTime = null;
            _timerInstance.Start();
            ResultInstance.Reset();
        }
        public void InitButtons(Grid grid)
        {
            for (var i = 0;
                 i < Constants.ROW_NUMBER;
                 i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (var j = 0;
                 j < Constants.COLUMN_NUMBER;
                 j++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (var i = 0;
                 i < Constants.ROW_NUMBER;
                 i++)
            {
                for (var j = 0;
                     j < Constants.COLUMN_NUMBER;
                     j++)
                {
                    var button = new Button
                                 {
                                     Background = Brushes.CornflowerBlue,
                                     FontSize = 16
                                 };
                    var locali = i;
                    var localj = j;
                    button.MouseRightButtonDown += delegate
                                                   {
                                                       ClickButtonRight(locali,
                                                                        localj);
                                                   };
                    button.Click += delegate
                                    {
                                        ClickButtonLeft(locali,
                                                        localj);
                                    };
                    button.SetBinding(UIElement.IsEnabledProperty,
                                      new Binding("IsUndigged")
                                      {
                                          Source = _positionArray[i,
                                                                  j]
                                      });
                    button.SetBinding(Control.ForegroundProperty,
                                      new Binding("Foreground")
                                      {
                                          Source = _positionArray[i,
                                                                  j]
                                      });
                    button.SetBinding(ContentControl.ContentProperty,
                                      new Binding("Text")
                                      {
                                          Source = _positionArray[i,
                                                                  j]
                                      });
                    grid.Children.Add(button);
                    Grid.SetRow(button,
                                i);
                    Grid.SetColumn(button,
                                   j);
                }
            }
        }
        private IPosition GetPosition(int i,
                                      int j)
        {
            return _positionArray[i,
                                  j];
        }
        private IEnumerable<IPosition> GetAroundPositon(int i,
                                                        int j)
        {
            return AroundService.AsEnumerable(i,
                                              j,
                                              GetPosition);
        }
        private int GetAroundUndigged(int i,
                                      int j)
        {
            return GetAroundPositon(i,
                                    j)
                .Count(position => position.IsUndigged);
        }
        private int GetAroundMine(int i,
                                  int j)
        {
            return GetAroundPositon(i,
                                    j)
                .Count(position => !position.IsUndigged && position.Text == Constants.MINE_SYMBOL);
        }
        private int GetAroundMineAndUndigged(int i,
                                             int j)
        {
            return GetAroundPositon(i,
                                    j)
                .Count(position => (position.IsUndigged || position.Text == Constants.MINE_SYMBOL));
        }
        private void ClickAllLeft()
        {
            for (var i = 0;
                 i < Constants.ROW_NUMBER;
                 i++)
            {
                for (var j = 0;
                     j < Constants.COLUMN_NUMBER;
                     j++)
                {
                    if (_positionArray[i,
                                       j].IsUndigged)
                    {
                        ClickButtonLeft(i,
                                        j);
                    }
                }
            }
        }
        private void ResetPosition()
        {
            for (var i = 0;
                 i < Constants.ROW_NUMBER;
                 i++)
            {
                for (var j = 0;
                     j < Constants.COLUMN_NUMBER;
                     j++)
                {
                    _positionArray[i,
                                   j].IsUndigged = true;
                    _positionArray[i,
                                   j].Foreground = Brushes.Black;
                    _positionArray[i,
                                   j].Text = String.Empty;
                }
            }
        }
        private void SetStartTime()
        {
            if (_startTime == null)
            {
                _startTime = DateTimeOffset.Now;
            }
        }
        private void OnTick(object sender,
                            EventArgs e)
        {
            ResultInstance.TimePast = (_startTime == null
                                           ? String.Empty
                                           : Math.Ceiling((DateTimeOffset.Now - _startTime.Value).TotalSeconds + 0.5)
                                                 .ToString());
            if (ResultInstance.MineNumber == 0)
            {
                ResultInstance.Foreground = Brushes.Green;
                ResultInstance.IsOn = false;
                ClickAllLeft();
                _timerInstance.Stop();
                return;
            }
            for (var i = 0;
                 i < Constants.ROW_NUMBER;
                 i++)
            {
                for (var j = 0;
                     j < Constants.COLUMN_NUMBER;
                     j++)
                {
                    var position = _positionArray[i,
                                                  j];
                    if (position.IsUndigged)
                    {
                        continue;
                    }
                    if (position.Text == null)
                    {
                        continue;
                    }
                    if (position.Text == Constants.MINE_SYMBOL)
                    {
                        continue;
                    }
                    if (GetAroundUndigged(i,
                                          j) == 0)
                    {
                        continue;
                    }
                    switch (position.Text)
                    {
                        case "":
                            AroundService.Act(i,
                                              j,
                                              ClickButtonLeft);
                            break;
                        case "1":
                            Check(i,
                                  j,
                                  1);
                            break;
                        case "2":
                            Check(i,
                                  j,
                                  2);
                            break;
                        case "3":
                            Check(i,
                                  j,
                                  3);
                            break;
                        case "4":
                            Check(i,
                                  j,
                                  4);
                            break;
                        case "5":
                            Check(i,
                                  j,
                                  5);
                            break;
                        case "6":
                            Check(i,
                                  j,
                                  6);
                            break;
                        case "7":
                            Check(i,
                                  j,
                                  7);
                            break;
                        case "8":
                            Check(i,
                                  j,
                                  8);
                            break;
                    }
                }
            }
            Check2();
        }
        private void ResetValue()
        {
            for (var i = 0;
                 i < Constants.ROW_NUMBER;
                 i++)
            {
                for (var j = 0;
                     j < Constants.COLUMN_NUMBER;
                     j++)
                {
                    _positionArray[i,
                                   j].Value = 0;
                }
            }
            var mines = Constants.TOTAL;
            while (mines > 0)
            {
                var r = _randomInstance.Next(Constants.TOTAL_NUMBER);
                var row = r / Constants.COLUMN_NUMBER;
                var column = r % Constants.COLUMN_NUMBER;
                var position = _positionArray[row,
                                              column];
                if (position.Value == 0)
                {
                    position.Value = Constants.MINE_VALUE;
                    mines--;
                }
            }
            for (var i = 0;
                 i < Constants.ROW_NUMBER;
                 i++)
            {
                for (var j = 0;
                     j < Constants.COLUMN_NUMBER;
                     j++)
                {
                    var position = _positionArray[i,
                                                  j];
                    if (position.Value != Constants.MINE_VALUE)
                    {
                        position.Value = AroundService.AsEnumerable(i,
                                                                    j,
                                                                    GetValue)
                                                      .Count(value => value == Constants.MINE_VALUE);
                    }
                }
            }
        }
        private int GetValue(int i,
                             int j)
        {
            return _positionArray[i,
                                  j].Value;
        }
        private void ClickButtonRight(int i,
                                      int j)
        {
            SetStartTime();
            var position = _positionArray[i,
                                          j];
            if (position.IsUndigged)
            {
                position.IsUndigged = false;
                ResultInstance.UndiggedNumber--;
                if (position.Value != Constants.MINE_VALUE)
                {
                    Fail();
                    return;
                }
                position.Foreground = Brushes.Red;
                position.Text = Constants.MINE_SYMBOL;
                ResultInstance.MineNumber--;
            }
        }
        private void ClickButtonLeft(int i,
                                     int j)
        {
            SetStartTime();
            Dig(i,
                j,
                Fail);
        }
        private void Fail()
        {
            _timerInstance.Stop();
            ResultInstance.Foreground = Brushes.Red;
            ResultInstance.IsOn = false;
            for (var i = 0;
                 i < Constants.ROW_NUMBER;
                 i++)
            {
                for (var j = 0;
                     j < Constants.COLUMN_NUMBER;
                     j++)
                {
                    Dig(i,
                        j,
                        null);
                }
            }
        }
        private void Dig(int i,
                         int j,
                         Action ifFailed)
        {
            var position = _positionArray[i,
                                          j];
            if (position.IsUndigged)
            {
                position.IsUndigged = false;
                ResultInstance.UndiggedNumber--;
                switch (position.Value)
                {
                    case Constants.MINE_VALUE:
                        position.Text = Constants.MINE_SYMBOL;
                        if (ifFailed != null)
                        {
                            ifFailed();
                        }
                        break;
                    case 0:
                        position.Text = String.Empty;
                        break;
                    default:
                        position.Text = position.Value.ToString();
                        break;
                }
                if (!ResultInstance.IsOn)
                {
                    position.Foreground = Brushes.Red;
                }
            }
        }
        private void ClickButtonLeftIfUndigged(int i,
                                               int j)
        {
            if (_positionArray[i,
                               j].IsUndigged)
            {
                ClickButtonLeft(i,
                                j);
            }
        }
        private void ClickButtonRightIfUndigged(int i,
                                                int j)
        {
            if (_positionArray[i,
                               j].IsUndigged)
            {
                ClickButtonRight(i,
                                 j);
            }
        }
        private void ClickButtonAroundLeft(int i,
                                           int j,
                                           Directions direction = Directions.None)
        {
            AroundService.Act(i,
                              j,
                              ClickButtonLeftIfUndigged,
                              direction);
        }
        private void ClickButtonAroundRight(int i,
                                            int j,
                                            Directions direction = Directions.None)
        {
            AroundService.Act(i,
                              j,
                              ClickButtonRightIfUndigged,
                              direction);
        }
        private void Check(int i,
                           int j,
                           int index)
        {
            var sum = GetAroundMineAndUndigged(i,
                                               j);
            if (sum == index)
            {
                ClickButtonAroundRight(i,
                                       j);
            }
            else if (sum > index
                     && GetAroundMine(i,
                                      j) >= index)
            {
                ClickButtonAroundLeft(i,
                                      j);
            }
        }
        private void Check2()
        {
            for (var i = 0;
                 i + 1 < Constants.ROW_NUMBER;
                 i++)
            {
                for (var j = 0;
                     j + 1 < Constants.COLUMN_NUMBER;
                     j++)
                {
                    var position1 = _positionArray[i,
                                                   j];
                    var position2 = _positionArray[i,
                                                   j + 1];
                    var position3 = _positionArray[i + 1,
                                                   j];
                    var position4 = _positionArray[i + 1,
                                                   j + 1];
                    if (position1.IsUndigged
                        && position2.IsUndigged
                        && !position3.IsUndigged
                        && !position4.IsUndigged)
                    {
                        Handle(position3,
                               position4,
                               i + 1,
                               j,
                               i + 1,
                               j + 1,
                               Directions.LeftUp,
                               Directions.RightUp);
                    }
                    else if (!position1.IsUndigged
                             && !position2.IsUndigged
                             && position3.IsUndigged
                             && position4.IsUndigged)
                    {
                        Handle(position1,
                               position2,
                               i,
                               j,
                               i,
                               j + 1,
                               Directions.LeftDown,
                               Directions.RightDown);
                    }
                    else if (!position1.IsUndigged
                             && position2.IsUndigged
                             && !position3.IsUndigged
                             && position4.IsUndigged)
                    {
                        Handle(position1,
                               position3,
                               i,
                               j,
                               i + 1,
                               j,
                               Directions.RightUp,
                               Directions.RightDown);
                    }
                    else if (position1.IsUndigged
                             && !position2.IsUndigged
                             && position3.IsUndigged
                             && !position4.IsUndigged)
                    {
                        Handle(position2,
                               position4,
                               i,
                               j + 1,
                               i + 1,
                               j + 1,
                               Directions.LeftUp,
                               Directions.LeftDown);
                    }
                }
            }
        }
        private void Handle(IPosition positionA,
                            IPosition positionB,
                            int iA,
                            int jA,
                            int iB,
                            int jB,
                            Directions directionA,
                            Directions directionB)
        {
            if (positionA.IsNumber()
                && positionB.IsNumber())
            {
                var nA = GetAroundUndigged(iA,
                                           jA);
                var nB = GetAroundUndigged(iB,
                                           jB);
                if (nA == 2
                    && nB > 2)
                {
                    Handle(positionA,
                           positionB,
                           iA,
                           jA,
                           iB,
                           jB,
                           nA,
                           nB,
                           directionA);
                }
                else if (nA > 2
                         && nB == 2)
                {
                    Handle(positionB,
                           positionA,
                           iB,
                           jB,
                           iA,
                           jA,
                           nB,
                           nA,
                           directionB);
                }
            }
        }
        private void Handle(IPosition positionA,
                            IPosition positionB,
                            int iA,
                            int jA,
                            int iB,
                            int jB,
                            int nA,
                            int nB,
                            Directions direction)
        {
            var vA = Convert.ToInt32(positionA.Text);
            var mA = GetAroundMine(iA,
                                   jA);
            var vB = Convert.ToInt32(positionB.Text);
            var mB = GetAroundMine(iB,
                                   jB);
            if (vA > mA)
            {
                if (vB - (vA - mA) - mB == 0)
                {
                    ClickButtonAroundLeft(iB,
                                          jB,
                                          direction);
                }
                else if (vB - (vA - mA) - mB == nB - nA)
                {
                    ClickButtonAroundRight(iB,
                                           jB,
                                           direction);
                }
            }
        }
    }
}