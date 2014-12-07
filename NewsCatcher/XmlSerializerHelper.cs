using System.IO;
using System.Xml.Serialization;

namespace NewsCatcher
{
    public static class XmlSerializerHelper
    {
        public static T Deserialize<T>(this string path)
        {
            return (T) new XmlSerializer(typeof (T)).Deserialize(new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite));
        }
        public static void Serialize(this object obj, string path)
        {
            new XmlSerializer(obj.GetType()).Serialize(new FileStream(path, FileMode.Create, FileAccess.ReadWrite), obj);
        }
    }
}