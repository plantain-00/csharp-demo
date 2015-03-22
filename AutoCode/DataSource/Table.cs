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

        [Category("����")]
        [DisplayName("���ݱ�����")]
        public string Name { get; set; }
        [Category("����������")]
        [DisplayName("�����м���")]
        public List<Column> Columns { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}