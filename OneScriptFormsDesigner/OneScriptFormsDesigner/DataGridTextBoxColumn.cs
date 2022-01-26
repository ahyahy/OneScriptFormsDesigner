using System.ComponentModel;

namespace osfDesigner
{
    public class DataGridTextBoxColumn : System.Windows.Forms.DataGridTextBoxColumn
    {

        private string _DoubleClick_osf;

        public DataGridTextBoxColumn()
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
		
        [DisplayName("ДвойноеНажатие")]
        [Description("Возвращает или задает код для выполнения, когда ячейка колонки дважды щелкнута.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string DoubleClick_osf
        {
            get { return _DoubleClick_osf; }
            set { _DoubleClick_osf = value; }
        }
		
        [DisplayName("ИмяСтиля")]
        [Category("Прочее")]
        [Browsable(true)]
        [ReadOnly(true)]
        public string NameStyle { get; set; }

        [Browsable(false)]
        public string DefaultValues { get; set; }
			
        [Browsable(false)]
        public string RequiredValues
        {
            get
            {
                return @"";
            }
        }
    }
}
