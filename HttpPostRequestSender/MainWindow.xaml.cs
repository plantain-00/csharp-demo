using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;

using Microsoft.Win32;

namespace HttpPostRequestSender
{
    public partial class MainWindow
    {
        private OpenFileDialog _openFileDialog;
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void Send_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                result.Text = "准备发送请求...\n\n";
                var xml = postContent.Text.Trim();
                var localUrl = url.Text.Trim();
                var request = (HttpWebRequest) WebRequest.Create(localUrl);
                request.Method = method.Text;
                request.ContentType = contentType.Text;
                request.Timeout = 10000;
                if (!string.IsNullOrEmpty(xml))
                {
                    var requestStream = request.GetRequestStream();
                    using (var writer = new StreamWriter(requestStream, Encoding.GetEncoding(requestEncoding.Text)))
                    {
                        writer.Write(xml);
                        writer.Flush();
                    }
                }
                else if (!string.IsNullOrEmpty(file.Text)
                         && File.Exists(file.Text))
                {
                    var helper = new UploadFileHelper(file.Text, request);
                    helper.Start();
                }
                result.Text += "发送请求完成...\n\n";
                var response = (HttpWebResponse) await request.GetResponseAsync();
                result.Text += string.Format("已经接收到响应，响应码：{0}，准备输出结果...\n\n", response.StatusCode);
                using (var stream = response.GetResponseStream())
                {
                    using (var myStreamReader = new StreamReader(stream, Encoding.GetEncoding(responseEncoding.Text)))
                    {
                        result.Text += myStreamReader.ReadToEnd();
                    }
                }
                result.Text += "\n\n输出结果完成...";
            }
            catch (Exception exception)
            {
                result.Text += exception.Message;
            }
        }
        private void SelectDir_OnClick(object sender, RoutedEventArgs e)
        {
            if (_openFileDialog == null)
            {
                _openFileDialog = new OpenFileDialog
                                  {
                                      Filter = " 所有文件|*.*"
                                  };
            }
            if (_openFileDialog.ShowDialog() == true)
            {
                file.Text = _openFileDialog.FileName;
            }
        }
        private void Clear_OnClick(object sender, RoutedEventArgs e)
        {
            file.Clear();
        }
        private void ToString_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(file.Text))
            {
                result.Text = "没有选择文件！";
                return;
            }
            if (!File.Exists(file.Text))
            {
                result.Text = "文件不存在！";
                return;
            }
            try
            {
                using (var stream = new FileStream(file.Text, FileMode.Open, FileAccess.Read))
                {
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    result.Text = Convert.ToBase64String(bytes, 0, bytes.Length);
                }
            }
            catch (Exception exception)
            {
                result.Text = exception.Message;
            }
        }
    }
}