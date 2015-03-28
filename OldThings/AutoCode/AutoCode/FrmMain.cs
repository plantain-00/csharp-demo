using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using AutoCode.Services;
using AutoCode.Templates;

using NPOI.HSSF.UserModel;

using Timer = System.Windows.Forms.Timer;

namespace AutoCode
{
    public partial class FrmMain : Form
    {
        private readonly Timer timer = new Timer
                                       {
                                           Interval = 30000
                                       };

        public FrmMain()
        {
            InitializeComponent();
            BLLService.Deserialize(DataRepository.DefaultFileName);
            propertyGrid1.SelectedObject = DataRepository.software;
            Closing += delegate
                       {
                           BLLService.Serialize(DataRepository.DefaultFileName);
                       };
            tsbExport.Click += delegate
                               {
                                   var dialog = new SaveFileDialog();
                                   if (DialogResult.OK == dialog.ShowDialog())
                                   {
                                       BLLService.Serialize(dialog.FileName);
                                   }
                               };
            tsbImport.Click += delegate
                               {
                                   var dialog = new OpenFileDialog();
                                   if (DialogResult.OK != dialog.ShowDialog())
                                   {
                                       return;
                                   }
                                   BLLService.Deserialize(dialog.FileName);
                               };
            tsbSave.Click += delegate
                             {
                                 BLLService.Serialize(DataRepository.DefaultFileName);
                             };
            tsbProduceCode.Click += delegate
                                    {
                                        BLLService.Serialize(DataRepository.DefaultFileName);
                                        BLLService.Deserialize(DataRepository.DefaultFileName);
                                        if (DataRepository.IsNotCorrect)
                                        {
                                            return;
                                        }
                                        File.WriteAllText("Solution.sln",
                                                          new Solution().TransformText());
                                        if (Directory.Exists(DataRepository.software.DALNameSpace))
                                        {
                                            Directory.Delete(DataRepository.software.DALNameSpace,
                                                             true);
                                        }
                                        do
                                        {
                                            Directory.CreateDirectory(DataRepository.software.DALNameSpace);
                                        }
                                        while (!Directory.Exists(DataRepository.software.DALNameSpace));
                                        File.WriteAllText(Path.Combine(DataRepository.software.DALNameSpace,
                                                                       "DataSourceRepository.cs"),
                                                          new DataSourceRepository().TransformText());
                                        File.WriteAllText(Path.Combine(DataRepository.software.DALNameSpace,
                                                                       DataRepository.software.DALNameSpace + ".csproj"),
                                                          new DALProject().TransformText());
                                        var dalText = new DAL().TransformText()
                                                               .Split(new[]
                                                                      {
                                                                          "DALRemark20130426"
                                                                      },
                                                                      StringSplitOptions.RemoveEmptyEntries);
                                        var dalIndex = 0;
                                        Directory.CreateDirectory(Path.Combine(DataRepository.software.DALNameSpace,
                                                                               "Services"));
                                        foreach (var database in DataRepository.software.Databases)
                                        {
                                            var dir = database.Name;
                                            Directory.CreateDirectory(Path.Combine(DataRepository.software.DALNameSpace,
                                                                                   "Services",
                                                                                   dir));
                                            foreach (var table in database.Tables)
                                            {
                                                File.WriteAllText(Path.Combine(DataRepository.software.DALNameSpace,
                                                                               "Services",
                                                                               dir,
                                                                               "DAL" + table.Name + "Service.cs"),
                                                                  dalText[dalIndex]);
                                                dalIndex++;
                                            }
                                        }
                                        File.WriteAllText(Path.Combine(DataRepository.software.DALNameSpace,
                                                                       "Services\\DbHelperSQLite.cs"),
                                                          new DbHelperSQLite().TransformText());
                                        File.WriteAllText(Path.Combine(DataRepository.software.DALNameSpace,
                                                                       "Services\\DALService.cs"),
                                                          new DatabaseService().TransformText());
                                        var dalListText = new DALModelList().TransformText()
                                                                            .Split(new[]
                                                                                   {
                                                                                       "DALModelListRemark20130515"
                                                                                   },
                                                                                   StringSplitOptions.RemoveEmptyEntries);
                                        var dalListIndex = 0;
                                        Directory.CreateDirectory(Path.Combine(DataRepository.software.DALNameSpace,
                                                                               "Models"));
                                        foreach (var database in DataRepository.software.Databases)
                                        {
                                            var dir = database.Name;
                                            Directory.CreateDirectory(Path.Combine(DataRepository.software.DALNameSpace,
                                                                                   "Models",
                                                                                   dir));
                                            foreach (var table in database.Tables)
                                            {
                                                File.WriteAllText(Path.Combine(DataRepository.software.DALNameSpace,
                                                                               "Models",
                                                                               dir,
                                                                               "DAL" + table.Name + ".cs"),
                                                                  dalListText[dalListIndex]);
                                                dalListIndex++;
                                            }
                                        }
                                        File.WriteAllText(Path.Combine(DataRepository.software.DALNameSpace,
                                                                       "Models\\DatabaseInfo.cs"),
                                                          new DatabaseInfo().TransformText());
                                        Directory.CreateDirectory(Path.Combine(DataRepository.software.DALNameSpace,
                                                                               "Properties"));
                                        File.WriteAllText(Path.Combine(DataRepository.software.DALNameSpace,
                                                                       "Properties\\AssemblyInfo.cs"),
                                                          new DALAssemblyInfo().TransformText());
                                        if (Directory.Exists(DataRepository.software.ModelNameSpace))
                                        {
                                            Directory.Delete(DataRepository.software.ModelNameSpace,
                                                             true);
                                        }
                                        do
                                        {
                                            Directory.CreateDirectory(DataRepository.software.ModelNameSpace);
                                        }
                                        while (!Directory.Exists(DataRepository.software.ModelNameSpace));
                                        File.WriteAllText(Path.Combine(DataRepository.software.ModelNameSpace,
                                                                       DataRepository.software.ModelNameSpace + ".csproj"),
                                                          new ModelProject().TransformText());
                                        var modelText = new Model().TransformText()
                                                                   .Split(new[]
                                                                          {
                                                                              "ModelRemark20130426"
                                                                          },
                                                                          StringSplitOptions.RemoveEmptyEntries);
                                        var modelIndex = 0;
                                        foreach (var database in DataRepository.software.Databases)
                                        {
                                            var dir = database.Name;
                                            Directory.CreateDirectory(Path.Combine(DataRepository.software.ModelNameSpace,
                                                                                   dir));
                                            foreach (var table in database.Tables)
                                            {
                                                File.WriteAllText(Path.Combine(DataRepository.software.ModelNameSpace,
                                                                               dir,
                                                                               "Model" + table.Name + ".cs"),
                                                                  modelText[modelIndex]);
                                                modelIndex++;
                                            }
                                        }
                                        Directory.CreateDirectory(Path.Combine(DataRepository.software.ModelNameSpace,
                                                                               "Properties"));
                                        File.WriteAllText(Path.Combine(DataRepository.software.ModelNameSpace,
                                                                       "Properties\\AssemblyInfo.cs"),
                                                          new ModelAssemblyInfo().TransformText());
                                        var directory = Path.Combine(DataRepository.software.DALNameSpace,
                                                                     DataRepository.software.OutputDirectory);
                                        if (Directory.Exists(directory))
                                        {
                                            Directory.Delete(directory,
                                                             true);
                                        }
                                        Directory.CreateDirectory(DataRepository.software.DALNameSpace);
                                        Directory.CreateDirectory(directory);
                                        File.Copy("System.Data.SQLite.DLL",
                                                  Path.Combine(directory,
                                                               "System.Data.SQLite.DLL"));
                                        if (Directory.Exists("Excels"))
                                        {
                                            Directory.Delete("Excels",
                                                             true);
                                        }
                                        Thread.Sleep(200);
                                        Directory.CreateDirectory("Excels");
                                        foreach (var database in DataRepository.software.Databases)
                                        {
                                            DataRepository.Workbook = new HSSFWorkbook();
                                            foreach (var table in database.Tables)
                                            {
                                                var sheet = DataRepository.Workbook.CreateSheet(table.Name);
                                                var headRow = sheet.CreateRow(0);
                                                sheet.SetColumnWidth(0,
                                                                     5000);
                                                sheet.SetColumnWidth(1,
                                                                     5000);
                                                sheet.SetColumnWidth(2,
                                                                     5000);
                                                sheet.SetColumnWidth(3,
                                                                     5000);
                                                sheet.SetColumnWidth(4,
                                                                     5000);
                                                sheet.SetColumnWidth(5,
                                                                     5000);
                                                sheet.SetColumnWidth(6,
                                                                     5000);
                                                headRow.CreateCell(0)
                                                       .SetCellValue("名称");
                                                headRow.CreateCell(1)
                                                       .SetCellValue("类型");
                                                headRow.CreateCell(2)
                                                       .SetCellValue("是否为主键");
                                                headRow.CreateCell(3)
                                                       .SetCellValue("是否非空");
                                                headRow.CreateCell(4)
                                                       .SetCellValue("是否为索引");
                                                headRow.CreateCell(5)
                                                       .SetCellValue("是否为自动增长字段");
                                                headRow.CreateCell(6)
                                                       .SetCellValue("注释");
                                                for (var i = 0;
                                                     i < table.Columns.Count;
                                                     i++)
                                                {
                                                    var column = table.Columns[i];
                                                    var row = sheet.CreateRow(i + 1);
                                                    row.CreateCell(0)
                                                       .SetCellValue(column.Name);
                                                    row.CreateCell(1)
                                                       .SetCellValue(column.Type.ToString()
                                                                           .TrimStart('@'));
                                                    row.CreateCell(2)
                                                       .SetCellValue(column.IsMainKey);
                                                    row.CreateCell(3)
                                                       .SetCellValue(column.NotNull);
                                                    row.CreateCell(4)
                                                       .SetCellValue(column.IsIndex);
                                                    row.CreateCell(5)
                                                       .SetCellValue(column.IsAutoIncrement);
                                                    row.CreateCell(6)
                                                       .SetCellValue(column.Comment);
                                                }
                                            }
                                            using (var file = new FileStream("Excels\\" + database.Name + ".xls",
                                                                             FileMode.Create))
                                            {
                                                DataRepository.Workbook.Write(file);
                                            }
                                        }
                                        foreach (var database in DataRepository.software.Databases)
                                        {
                                            File.Delete(database.Name);
                                            SQLiteConnection.CreateFile(database.Name);
                                            var dataSource = (string.IsNullOrEmpty(database.Password)
                                                                  ? "Data Source=" + database.Name
                                                                  : "Data Source=" + database.Name + ";Password=" + database.Password);
                                            foreach (var table in database.Tables)
                                            {
                                                var tmp = table.Columns.Skip(1)
                                                               .Aggregate(string.Empty,
                                                                          (s,
                                                                           a) => s + ",'" + a.Name + "'");
                                                BLLService.ExecuteSql(dataSource,
                                                                      string.Format("create table '{0}'('{1}' Integer Primary Key Autoincrement{2});",
                                                                                    table.Name,
                                                                                    table.Columns[0].Name,
                                                                                    tmp));
                                                var indexColumns = table.Columns.Where(column => column.IsIndex)
                                                                        .Aggregate(string.Empty,
                                                                                   (s,
                                                                                    c) => s + (c.Name + " asc,"));
                                                if (indexColumns.Length == 0)
                                                {
                                                    continue;
                                                }
                                                indexColumns = indexColumns.Trim(',');
                                                BLLService.ExecuteSql(dataSource,
                                                                      string.Format("create index {0}Index on {0}({1});",
                                                                                    table.Name,
                                                                                    indexColumns));
                                            }
                                            try
                                            {
                                                if (!string.IsNullOrEmpty(database.InitializeCommand))
                                                {
                                                    BLLService.ExecuteSql(dataSource,
                                                                          database.InitializeCommand);
                                                }
                                            }
                                            catch (Exception exception)
                                            {
                                                MessageBox.Show(exception.Message);
                                            }
                                            File.Delete(database.Name + ".bak");
                                            File.Copy(database.Name,
                                                      database.Name + ".bak");
                                        }
                                        MessageBox.Show("生成成功！");
                                        //Process.Start("explorer.exe", Directory.GetCurrentDirectory());
                                    };
            timer.Tick += delegate
                          {
                              BLLService.Serialize(DataRepository.DefaultFileName);
                              Text = "自动保存于：" + DateTime.Now;
                          };
            timer.Start();
        }
    }
}