using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataSource
{
    [Serializable]
    [DefaultProperty("Tables")]
    public class Database
    {
        public Database()
        {
            Name = "NewDatabase";
            Password = string.Empty;
            InitializeCommand = string.Empty;
            if (Tables == null)
            {
                Tables = new List<Table>();
            }
        }

        [Category("属性")]
        [DisplayName("数据库名称")]
        public string Name { get; set; }
        [Category("属性")]
        [DisplayName("数据库密码")]
        public string Password { get; set; }
        [Category("属性")]
        [DisplayName("初始化语句")]
        public string InitializeCommand { get; set; }
        [Category("数据表设置")]
        [DisplayName("数据表集合")]
        public List<Table> Tables { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}