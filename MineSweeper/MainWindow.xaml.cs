using System.Windows.Data;

using MineSweeper.Interfaces;

namespace MineSweeper
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    internal partial class MainWindow
    {
        private static readonly IContext _context = IocContainer.Resolve<IContext>();
        public MainWindow()
        {
            InitializeComponent();
            lblMines.SetBinding(ForegroundProperty,
                                new Binding("Foreground")
                                {
                                    Source = _context.ResultInstance
                                });
            lblTime.SetBinding(ForegroundProperty,
                               new Binding("Foreground")
                               {
                                   Source = _context.ResultInstance
                               });
            lblUndigged.SetBinding(ForegroundProperty,
                                   new Binding("Foreground")
                                   {
                                       Source = _context.ResultInstance
                                   });
            lblMines.SetBinding(ContentProperty,
                                new Binding("MineNumber")
                                {
                                    Source = _context.ResultInstance
                                });
            lblTime.SetBinding(ContentProperty,
                               new Binding("TimePast")
                               {
                                   Source = _context.ResultInstance
                               });
            lblUndigged.SetBinding(ContentProperty,
                                   new Binding("UndiggedNumber")
                                   {
                                       Source = _context.ResultInstance
                                   });
            btnClickRandomly.SetBinding(IsEnabledProperty,
                                        new Binding("IsOn")
                                        {
                                            Source = _context.ResultInstance
                                        });
            btnClickRandomly.Click += delegate
                                      {
                                          _context.ClickRandomly();
                                      };
            btnReplay.Click += delegate
                               {
                                   _context.Replay();
                               };
            btnReset.Click += delegate
                              {
                                  _context.Reset();
                              };
            _context.InitButtons(grid);
        }
    }
}