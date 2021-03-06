﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Navigation;

using NewsCatcher.Models;

using Newtonsoft.Json;

namespace NewsCatcher
{
    public partial class MainWindow
    {
        private const string NEWSHISTORY_JSON = "NewsHistory.json";
        internal static List<HistoryItem> History;
        private readonly List<ShowItem> _itemsSource;
        private Task _deserialization;

        public MainWindow()
        {
            InitializeComponent();
            _itemsSource = new List<ShowItem>();
            listView.ItemsSource = _itemsSource;

            Topmost = ConfigurationManager.AppSettings["is_debug"] != true.ToString();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == SingleInstanceAppliction.WM_SHOWME)
            {
                if (WindowState == WindowState.Minimized)
                {
                    WindowState = WindowState.Normal;
                }
                var top = Topmost;
                Topmost = true;
                Topmost = top;
            }
            return IntPtr.Zero;
        }

        private async void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            (sender as Hyperlink).Foreground = Brushes.Red;
            switch (e.Uri.ToString())
            {
                case "title":
                    break;
                case CnBeta.FAILS_MESSAGE:
                    _itemsSource.RemoveAll(i => i.Url == CnBeta.FAILS_MESSAGE);
                    await CnBetaTask();
                    break;
                case Cnblogs.FAILS_MESSAGE:
                    _itemsSource.RemoveAll(i => i.Url == Cnblogs.FAILS_MESSAGE);
                    await CnblogsTask();
                    break;
                case V2ex.FAILS_MESSAGE:
                    _itemsSource.RemoveAll(i => i.Url == V2ex.FAILS_MESSAGE);
                    await V2exTask();
                    break;
                case TV.FAILS_MESSAGE:
                    _itemsSource.RemoveAll(i => i.Url == TV.FAILS_MESSAGE);
                    await TVTask();
                    break;
                case CzechMassage.FAILS_MESSAGE:
                    _itemsSource.RemoveAll(i => i.Url == CzechMassage.FAILS_MESSAGE);
                    await CzechMassageTask();
                    break;
                case GithubTrending.FAILS_MESSAGE:
                    _itemsSource.RemoveAll(i => i.Url == GithubTrending.FAILS_MESSAGE);
                    await GithubTrendingTask();
                    break;
                default:
                {
                    var uri = e.Uri.AbsoluteUri;
                    History.Add(new HistoryItem
                                {
                                    Url = uri,
                                    Time = DateTime.Now.ToInt32()
                                });
                    if (!Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                    {
                        Process.Start(new ProcessStartInfo(WindowsHelper.GetDefaultBrowser(), uri));
                    }
                }
                    break;
            }
            e.Handled = true;
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            try
            {
                var json = JsonConvert.SerializeObject(History, Formatting.Indented);
                using (var writer = new StreamWriter(NEWSHISTORY_JSON, false))
                {
                    writer.Write(json);
                    writer.Flush();
                }
            }
            catch (Exception)
            {
            }
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _deserialization = new Task(delegate
                                        {
                                            try
                                            {
                                                using (var reader = new StreamReader(NEWSHISTORY_JSON))
                                                {
                                                    History = JsonConvert.DeserializeObject<List<HistoryItem>>(reader.ReadToEnd());
                                                }
                                                History.RemoveAll(h => h.Time < DateTime.Now.AddDays(-30).ToInt32());
                                            }
                                            catch (Exception)
                                            {
                                                History = new List<HistoryItem>();
                                            }
                                        });
            _deserialization.Start();
            CnblogsTask();
            CnBetaTask();
            V2exTask();
            TVTask();
            CzechMassageTask();
            GithubTrendingTask();
        }

        private void Refactor()
        {
            var isEmpty = true;
            for (var i = 0; i < _itemsSource.Count; i++)
            {
                if (isEmpty && string.IsNullOrEmpty(_itemsSource[i].Text))
                {
                    _itemsSource.RemoveAt(i);
                    i--;
                    continue;
                }
                isEmpty = string.IsNullOrEmpty(_itemsSource[i].Text);
            }
        }

        private async Task GithubTrendingTask()
        {
            var result = await GithubTrending.DoAsync();
            await _deserialization;
            _itemsSource.AddRange(result);
            Refactor();
            listView.Items.Refresh();
        }

        private async Task V2exTask()
        {
            var result = await V2ex.DoAsync();
            await _deserialization;
            _itemsSource.AddRange(result.Where(i => History.All(h => h.Url != i.Url)));
            Refactor();
            listView.Items.Refresh();
        }

        private async Task CnBetaTask()
        {
            var result = await CnBeta.DoAsync();
            await _deserialization;
            _itemsSource.AddRange(result.Where(i => History.All(h => h.Url != i.Url)));
            Refactor();
            listView.Items.Refresh();
        }

        private async Task CnblogsTask()
        {
            var result = await Cnblogs.DoAsync();
            await _deserialization;
            _itemsSource.AddRange(result.Where(i => History.All(h => h.Url != i.Url)));
            Refactor();
            listView.Items.Refresh();
        }

        private async Task TVTask()
        {
            var result = await TV.DoAsync();
            await _deserialization;
            _itemsSource.AddRange(result.Where(i => History.All(h => h.Url != i.Url)));
            Refactor();
            listView.Items.Refresh();
        }

        private async Task CzechMassageTask()
        {
            var result = await CzechMassage.DoAsync();
            await _deserialization;
            _itemsSource.AddRange(result.Where(i => History.All(h => h.Url != i.Url)));
            Refactor();
            listView.Items.Refresh();
        }
    }
}