using System.IO;
using System.Text;
using System.Web;

namespace ImageServer
{
    /// <summary>
    ///     Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var fileName = context.Request.QueryString["file"];
            var filePath = context.Server.MapPath("Files/" + fileName);
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var bytes = new byte[(int) fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            context.Response.ContentType = "application/octet-stream";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));
            context.Response.BinaryWrite(bytes);
            context.Response.Flush();
            context.Response.End();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}