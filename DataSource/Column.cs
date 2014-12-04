using System;
using System.ComponentModel;

namespace DataSource
{
    [Serializable]
    [DefaultProperty("Name")]
    public class Column
    {
        public Column()
        {
            Name = "NewColumn";
            Type = SupportedType.Int32;
            Comment = string.Empty;
            NotNull = true;
        }

        [DisplayName("列名")]
        [Category("属性")]
        public string Name { get; set; }
        [DisplayName("类型")]
        [Category("属性")]
        public SupportedType Type { get; set; }
        [DisplayName("是否是主键")]
        [DefaultValue(false)]
        [Category("属性")]
        public bool IsMainKey { get; set; }
        [DisplayName("是否非空")]
        [DefaultValue(true)]
        [Category("属性")]
        public bool NotNull { get; set; }
        [DisplayName("是否自动增长")]
        [DefaultValue(false)]
        [Category("属性")]
        public bool IsAutoIncrement { get; set; }
        [DisplayName("是否是索引")]
        [DefaultValue(false)]
        [Category("属性")]
        public bool IsIndex { get; set; }
        [DisplayName("注释")]
        [Category("属性")]
        public string Comment { get; set; }

        public override string ToString()
        {
            return string.Format("{0}({1})",
                                 Name,
                                 Type);
        }
    }
}