using System.ComponentModel;

namespace osfDesigner
{
    public class DataGridComboBoxColumnStyle : System.Windows.Forms.DataGridTextBoxColumn
    {
        private osfDesigner.HorizontalAlignment _Alignment;
        private osfDesigner.ComboBox _ComboBox = new osfDesigner.ComboBox();

        public DataGridComboBoxColumnStyle()
        {
            _Alignment = (osfDesigner.HorizontalAlignment)(base.Alignment);
        }

        [DisplayName("Выравнивание")]
        [Description("Возвращает или задает выравнивание текста в колонке.")]
        [Category("Показать")]
        [Browsable(true)]
        public new HorizontalAlignment Alignment
        {
            get { return _Alignment; }
            set
            {
                base.Alignment = (System.Windows.Forms.HorizontalAlignment)value;
                _Alignment = value;
            }
        }

        [DisplayName("ИмяСтиля")]
        [Category("Прочее")]
        [Browsable(true)]
        [ReadOnly(true)]
        public string NameStyle { get; set; }

        [DisplayName("ИмяОтображаемого")]
        [Description("Возвращает или задает имя элемента данных, на который отображается стиль столбца.")]
        [Category("Прочее")]
        [Browsable(true)]
        public new string MappingName
        {
            get { return base.MappingName; }
            set { base.MappingName = value; }
        }

        [DisplayName("ПолеВыбора")]
        [Description("Возвращает объект класса ПолеВыбора (ComboBox), используемый в стиле колонки сетки данных.")]
        [Category("Прочее")]
        [Browsable(true)]
        [ReadOnly(true)]
        [TypeConverter(typeof(MyConverter))]
        public osfDesigner.ComboBox ComboBox
        {
            get { return _ComboBox; }
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
        [Description("Возвращает или задает значение, указывающее, находится ли СтильКолонкиПолеВвода (DataGridTextBoxColumn) в состоянии 'только для чтения'.")]
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
        public new string NullText { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new string Format { get; set; }
	
        [Browsable(false)]
        public string RequiredValues
        {
            get
            {
                return @"
";
            }
        }

        [Browsable(false)]
        public string DefaultValues { get; set; }
    }
}
