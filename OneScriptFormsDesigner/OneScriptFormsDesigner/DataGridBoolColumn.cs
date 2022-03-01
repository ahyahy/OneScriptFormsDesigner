﻿using System.ComponentModel;

namespace osfDesigner
{
    public class DataGridBoolColumn : System.Windows.Forms.DataGridBoolColumn
    {

        public DataGridBoolColumn()
        {
        }

        [DisplayName("Выравнивание")]
        [Description("Возвращает или задает выравнивание текста в колонке.")]
        [Category("Показать")]
        [Browsable(true)]
        public new HorizontalAlignment Alignment
        {
            get { return (HorizontalAlignment)base.Alignment; }
            set { base.Alignment = (System.Windows.Forms.HorizontalAlignment)value; }
        }

        [DisplayName("ИмяОтображаемого")]
        [Description("Возвращает или задает имя элемента данных, на который отображается стиль столбца.")]
        [Category("Прочее")]
        [Browsable(true)]
        public new string MappingName
        {
            get { return base.MappingName; }
            set { base.MappingName = value; }
        }

        [DisplayName("ТекстЗаголовка")]
        [Description("Возвращает или задает текст заголовка колонки.")]
        [Category("Показать")]
        [Browsable(true)]
        public new string HeaderText
        {
            get { return base.HeaderText; }
            set { base.HeaderText = value; }
        }

        [DisplayName("ТолькоЧтение")]
        [Description("Возвращает или задает значение, указывающее, находится ли СтильКолонкиБулево (DataGridBoolColumn) в состоянии 'только для чтения'.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool ReadOnly
        {
            get { return base.ReadOnly; }
            set { base.ReadOnly = value; }
        }

        [DisplayName("Ширина")]
        [Description("Возвращает или задает ширину колонки.")]
        [Category("Макет")]
        [Browsable(true)]
        public new int Width
        {
            get { return base.Width; }
            set { base.Width = value; }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool AllowNull { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new string NullText { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new object FalseValue { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new object NullValue { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new object TrueValue { get; set; }
		
        [DisplayName("ИмяСтиля")]
        [Category("Прочее")]
        [Browsable(true)]
        [ReadOnly(true)]
        public string NameStyle { get; set; }
		
        [DisplayName("Текст")]
        [Category("Прочее")]
        [Browsable(true)]
        public string Text { get; set; }

        private System.Collections.Hashtable toolTip = new System.Collections.Hashtable();
        [Browsable(false)]
        public System.Collections.Hashtable ToolTip
        {
            get { return toolTip; }
            set { toolTip = value; }
        }

        [Browsable(false)]
        public string DefaultValues { get; set; }
			
        [Browsable(false)]
        public string RequiredValues
        {
            get
            {
                return @"
Текст ==
";
            }
        }
    }
}
