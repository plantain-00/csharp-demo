using System;
using System.IO;

using ICSharpCode.SharpZipLib.Zip;

namespace SharpZipLib.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var filenames = Directory.GetFiles("../../");
            using (var s = new ZipOutputStream(File.Create("result.zip")))
            {
                s.SetLevel(9);
                var buffer = new byte[4096];
                foreach (var file in filenames)
                {
                    var entry = new ZipEntry(Path.GetFileName(file))
                                {
                                    DateTime = DateTime.Now
                                };
                    s.PutNextEntry(entry);
                    using (var fs = File.OpenRead(file))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        }
                        while (sourceBytes > 0);
                    }
                }
            }
        }
    }
}