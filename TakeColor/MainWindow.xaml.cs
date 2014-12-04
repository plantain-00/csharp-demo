using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;
using Control = System.Windows.Forms.Control;
using Size = System.Drawing.Size;

namespace TakeColor
{
    public partial class MainWindow
    {
        private const int ROW = 4;
        private const int COLUMN = 4;
        private static readonly Bitmap cache = new Bitmap(1, 1);
        private static readonly Graphics tempGraphics = Graphics.FromImage(cache);
        private readonly Model[,] _models;
        public MainWindow()
        {
            InitializeComponent();
            for (var i = 0; i < 2 * ROW + 1; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (var i = 0; i < 2 * COLUMN + 1; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            _models = new Model[2 * ROW + 1, 2 * COLUMN + 1];
            for (var i = 0; i < 2 * ROW + 1; i++)
            {
                for (var j = 0; j < 2 * COLUMN + 1; j++)
                {
                    _models[i, j] = new Model();
                    var textblock = new TextBlock();
                    textblock.SetBinding(TextBlock.BackgroundProperty,
                                         new Binding("Background")
                                         {
                                             Source = _models[i, j]
                                         });
                    textblock.SetBinding(TextBlock.ForegroundProperty,
                                         new Binding("Foreground")
                                         {
                                             Source = _models[i, j]
                                         });
                    textblock.SetBinding(TextBlock.TextProperty,
                                         new Binding("Text")
                                         {
                                             Source = _models[i, j]
                                         });
                    var border = new Border();
                    if (i == ROW
                        && j == COLUMN)
                    {
                        border.BorderBrush = Brushes.Red;
                        border.BorderThickness = new Thickness(2, 2, 2, 2);
                    }
                    else
                    {
                        border.BorderThickness = new Thickness(1, 1, 1, 1);
                    }
                    border.Child = textblock;
                    grid.Children.Add(border);
                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                }
            }
            KeyDown += (sender, e) =>
                       {
                           if (e.Key == Key.F1)
                           {
                               var x = Control.MousePosition.X;
                               var y = Control.MousePosition.Y;
                               for (var i = 0; i < 2 * ROW + 1; i++)
                               {
                                   for (var j = 0; j < 2 * COLUMN + 1; j++)
                                   {
                                       GetColor(x - ROW + i, y - COLUMN + j, i, j);
                                   }
                               }
                           }
                       };
        }
        private void GetColor(int x, int y, int i, int j)
        {
            tempGraphics.CopyFromScreen(x - ROW + i, y - COLUMN + j, 0, 0, new Size(1, 1));
            var color = cache.GetPixel(0, 0);
            _models[i, j].Background = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
            _models[i, j].Text = string.Format("{0}\n({1}, {2})", ColorTranslator.ToHtml(color), x, y);
        }
    }
}