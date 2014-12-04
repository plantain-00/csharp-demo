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

        [DisplayName("����")]
        [Category("����")]
        public string Name { get; set; }
        [DisplayName("����")]
        [Category("����")]
        public SupportedType Type { get; set; }
        [DisplayName("�Ƿ�������")]
        [DefaultValue(false)]
        [Category("����")]
        public bool IsMainKey { get; set; }
        [DisplayName("�Ƿ�ǿ�")]
        [DefaultValue(true)]
        [Category("����")]
        public bool NotNull { get; set; }
        [DisplayName("�Ƿ��Զ�����")]
        [DefaultValue(false)]
        [Category("����")]
        public bool IsAutoIncrement { get; set; }
        [DisplayName("�Ƿ�������")]
        [DefaultValue(false)]
        [Category("����")]
        public bool IsIndex { get; set; }
        [DisplayName("ע��")]
        [Category("����")]
        public string Comment { get; set; }

        public override string ToString()
        {
            return string.Format("{0}({1})",
                                 Name,
                                 Type);
        }
    }
}