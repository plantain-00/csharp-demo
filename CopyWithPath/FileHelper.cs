using System.IO;

namespace CopyWithPath
{
    public static class FileHelper
    {
        public static void CopyThoughDirectoryNotFound(string sourceFileName, string destFileName)
        {
            var directory = Path.GetDirectoryName(destFileName);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            File.Copy(sourceFileName, destFileName);
        }
    }
}