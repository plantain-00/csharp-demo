using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataSource
{
    [Serializable]
    [DefaultProperty("Databases")]
    public class Software
    {
        public Software()
        {
            DALNameSpace = "CompanyName.SolutionName.DAL";
            ModelNameSpace = "CompanyName.SolutionName.Model";
            OutputDirectory = @"..\Library\";
            if (Databases == null)
            {
                Databases = new List<Database>();
            }
        }

        [Category("数据库设置")]
        [Description("设置数据库信息。")]
        [DisplayName("数据库集合")]
        public List<Database> Databases { get; set; }
        [Category("代码生成设置")]
        [Description("设置DAL项目的命名空间。")]
        [DisplayName("DAL项目命名空间")]
        public string DALNameSpace { get; set; }
        [Category("代码生成设置")]
        [Description("设置Model项目的命名空间。")]
        [DisplayName("Model项目命名空间")]
        public string ModelNameSpace { get; set; }
        [Category("代码生成设置")]
        [Description("设置DAL项目和Model项目输出目录。")]
        [DisplayName("输出目录")]
        public string OutputDirectory { get; set; }
    }
}