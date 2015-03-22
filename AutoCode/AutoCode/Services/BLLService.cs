using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Xml.Serialization;

using DataSource;

namespace AutoCode.Services
{
    internal class BLLService
    {
        internal static void Serialize(string fileName)
        {
            new XmlSerializer(typeof(Software)).Serialize(new FileStream(fileName,
                                                                          FileMode.Create,
                                                                          FileAccess.ReadWrite,
                                                                          FileShare.ReadWrite),
                                                           DataRepository.software);
        }

        internal static void Deserialize(string fileName)
        {
            try
            {
                var tmp = new XmlSerializer(typeof(Software)).Deserialize(new FileStream(fileName,
                                                                                          FileMode.Open,
                                                                                          FileAccess.ReadWrite,
                                                                                          FileShare.ReadWrite)) as Software;
                DataRepository.software = tmp ?? new Software();
            }
            catch (Exception)
            {
                DataRepository.software = new Software();
            }
        }

        internal static void ExecuteSql(string dataSource,
                                        string sqlString,
                                        params SQLiteParameter[] cmdParms)
        {
            using (var connection = new SQLiteConnection(dataSource))
            {
                using (var cmd = new SQLiteCommand())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    cmd.Connection = connection;
                    cmd.CommandText = sqlString;
                    cmd.CommandType = CommandType.Text;
                    if (cmdParms != null)
                    {
                        foreach (var parm in cmdParms)
                        {
                            cmd.Parameters.Add(parm);
                        }
                    }
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
        }
    }
}