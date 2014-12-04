using System;
using System.IO;
using System.Net;
using System.Text;

namespace HttpPostRequestSender
{
    public class UploadFileHelper
    {
        private readonly string _file;
        private readonly HttpWebRequest _request;
        public UploadFileHelper(string file, HttpWebRequest request)
        {
            _file = file;
            _request = request;
            FileName = Path.GetFileName(_file);
            Timeout = 300000;
            BufferLength = 4096;
        }
        public string FileName { get; set; }
        public int Timeout { get; set; }
        public int BufferLength { get; set; }
        public void Start()
        {
            using (var fileStream = new FileStream(_file, FileMode.Open, FileAccess.Read))
            {
                using (var binaryReader = new BinaryReader(fileStream))
                {
                    var boundary = "----------" + DateTime.Now.Ticks.ToString("x");
                    var boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                    var headerBytes = GetHead(boundary);
                    _request.Method = "POST";
                    _request.AllowWriteStreamBuffering = false;
                    _request.Timeout = Timeout;
                    _request.ContentType = "multipart/form-data; boundary=" + boundary;
                    var length = fileStream.Length + headerBytes.Length + boundaryBytes.Length;
                    _request.ContentLength = length;
                    var buffer = new byte[BufferLength];
                    var size = binaryReader.Read(buffer, 0, BufferLength);
                    using (var stream = _request.GetRequestStream())
                    {
                        stream.Write(headerBytes, 0, headerBytes.Length);
                        while (size > 0)
                        {
                            stream.Write(buffer, 0, size);
                            size = binaryReader.Read(buffer, 0, BufferLength);
                        }
                        stream.Write(boundaryBytes, 0, boundaryBytes.Length);
                    }
                }
            }
        }
        private byte[] GetHead(string boundary)
        {
            var sb = new StringBuilder();
            sb.Append("--");
            sb.Append(boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append("file");
            sb.Append("\"; filename=\"");
            sb.Append(FileName);
            sb.Append("\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append("application/octet-stream");
            sb.Append("\r\n");
            sb.Append("\r\n");
            var strPostHeader = sb.ToString();
            return Encoding.UTF8.GetBytes(strPostHeader);
        }
    }
}