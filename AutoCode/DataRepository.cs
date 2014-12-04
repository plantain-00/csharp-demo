using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using DataSource;

using NPOI.HSSF.UserModel;

namespace AutoCode
{
    internal static class DataRepository
    {
        internal const string DefaultFileName = "Last.xml";
        internal const string NewDatabasePath = "NewDatabase.db";
        internal const string NewTableName = "NewTable";
        internal const string NewColumnName = "NewColumn";
        internal const string DALEnding = ".DAL";
        internal const string ModelEnding = ".Model";

        static DataRepository()
        {
            software = new Software();
        }

        internal static Software software { get; set; }
        internal static bool IsNotCorrect
        {
            get
            {
                if (string.IsNullOrEmpty(software.DALNameSpace)
                    || string.IsNullOrEmpty(software.ModelNameSpace))
                {
                    MessageBox.Show("命名空间不能为空！");
                    return true;
                }
                if (string.IsNullOrEmpty(software.OutputDirectory))
                {
                    MessageBox.Show("输出目录不能为空！");
                    return true;
                }
                if (software.Databases == null
                    || software.Databases.Count <= 0)
                {
                    MessageBox.Show("数据库不能为空！");
                    return true;
                }
                if (software.Databases.Any(d => string.IsNullOrEmpty(d.Name)))
                {
                    MessageBox.Show("数据库路径不能为空！");
                    return true;
                }
                if (software.Databases.Any(d => d.Tables == null || d.Tables.Count <= 0))
                {
                    MessageBox.Show("数据表不能为空！");
                    return true;
                }
                if (software.Databases.Any(d => d.Tables.Any(t => string.IsNullOrEmpty(t.Name))))
                {
                    MessageBox.Show("数据表名不能为空！");
                    return true;
                }
                if (software.Databases.Any(d => d.Tables.Any(t => !Regex.IsMatch(t.Name,
                                                                                 @"^[_a-zA-Z][a-zA-Z0-9]*$"))))
                {
                    MessageBox.Show("数据表名不合法！");
                    return true;
                }
                if (software.Databases.Any(d => d.Tables.Any(t => t.Columns == null || t.Columns.Count <= 0)))
                {
                    MessageBox.Show("数据列不能为空！");
                    return true;
                }
                if (software.Databases.Any(d => d.Tables.Any(t => t.Columns.Any(c => string.IsNullOrEmpty(c.Name)))))
                {
                    MessageBox.Show("数据列名不能为空！");
                    return true;
                }
                if (software.Databases.Any(d => d.Tables.Any(t => t.Columns.Any(c => !Regex.IsMatch(c.Name,
                                                                                                    @"^[_a-zA-Z][a-zA-Z0-9]*$")))))
                {
                    MessageBox.Show("数据列名不合法！");
                    return true;
                }
                if (!software.Databases.Any(d => d.Tables.Any(t => t.Columns.Any(c => string.IsNullOrEmpty(c.Name)))))
                {
                    return false;
                }
                MessageBox.Show("数据列类型不能为空！");
                return true;
            }
        }
        internal static HSSFWorkbook Workbook { get; set; }

        internal static bool DatabasesHaveNoElement()
        {
            return software == null || software.Databases == null || software.Databases.Count <= 0;
        }

        internal static bool TablesHaveNoElement(int databaseIndex)
        {
            if (DatabasesHaveNoElement())
            {
                return true;
            }
            return databaseIndex < 0 || databaseIndex >= software.Databases.Count || software.Databases[databaseIndex].Tables == null || software.Databases[databaseIndex].Tables.Count <= 0;
        }

        internal static bool ColumnsHaveNoElement(int databaseIndex,
                                                  int tableIndex)
        {
            if (TablesHaveNoElement(databaseIndex))
            {
                return true;
            }
            return tableIndex < 0 || tableIndex >= software.Databases[databaseIndex].Tables.Count || software.Databases[databaseIndex].Tables[tableIndex].Columns == null
                   || software.Databases[databaseIndex].Tables[tableIndex].Columns.Count <= 0;
        }
    }
}