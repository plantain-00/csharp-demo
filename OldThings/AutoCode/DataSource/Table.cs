using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataSource
{
    [Serializable]
    [DefaultProperty("Columns")]
    public class Table
    {
        public Table()
        {
            Name = "NewTable";
            if (Columns == null)
            {
                Columns = new List<Column>();
            }
        }

        [Category("属性")]
        [DisplayName("数据表名称")]
        public string Name { get; set; }
        [Category("数据列设置")]
        [DisplayName("数据列集合")]
        public List<Column> Columns { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}