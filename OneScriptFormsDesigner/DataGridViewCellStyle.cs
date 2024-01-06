using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Windows.Forms;

namespace osfDesigner
{
    public class DataGridViewCellStyle : System.Windows.Forms.DataGridViewCellStyle
    {

        [Browsable(false)]
        public System.Windows.Forms.DataGridViewCellStyle OriginalObj { get; set; }

        public DataGridViewCellStyle(System.Windows.Forms.DataGridViewCellStyle p1) : base(p1)
        {
            OriginalObj = p1;
        }
        
        public DataGridViewCellStyle()
        {
        }

        public DataGridViewCellStyle(string nameStyle, osfDesigner.DataGridView dataGridView)
        {
            NameStyle = nameStyle;
            Alignment = osfDesigner.DataGridViewContentAlignment.СерединаЛево;
            ForeColor = Color.FromName(System.Drawing.KnownColor.ControlText.ToString());
            SelectionForeColor = Color.FromName(System.Drawing.KnownColor.HighlightText.ToString());
            BackColor = Color.FromName(System.Drawing.KnownColor.Window.ToString());
            SelectionBackColor = Color.FromName(System.Drawing.KnownColor.Highlight.ToString());
            Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            WrapMode = DataGridViewTriState.Ложь;
            Font = new Font(dataGridView.Font, dataGridView.Font.Style);
        }

        [DisplayName("Выравнивание")]
        [Description("Возвращает или задает значение, показывающее положение содержимого в ячейке объекта Таблица (DataGridView).")]
        [Category("Макет")]
        [Browsable(true)]
        public new DataGridViewContentAlignment Alignment
        {
            get { return (DataGridViewContentAlignment)base.Alignment; }
            set { base.Alignment = (System.Windows.Forms.DataGridViewContentAlignment)value; }
        }

        [DisplayName("Заполнение")]
        [Description("Возвращает или задает расстояние между краем ячейки и ее содержимым.")]
        [Category("Макет")]
        [Browsable(true)]
        [TypeConverter(typeof(MyPaddingConverter))]
        [Editor(typeof(MyPaddingEditor), typeof(UITypeEditor))]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        [DisplayName("ОсновнойЦвет")]
        [Description("Возвращает или задает основной цвет ячейки объекта Таблица (DataGridView).")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        [DisplayName("ОсновнойЦветВыделенного")]
        [Description("Возвращает или задает основной цвет, используемый ячейкой объекта Таблица (DataGridView), когда она выбрана.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color SelectionForeColor
        {
            get { return base.SelectionForeColor; }
            set { base.SelectionForeColor = value; }
        }

        [DisplayName("Перенос")]
        [Description("Возвращает или задает значение, показывающее, переносится ли текстовое содержимое ячейки  объекта Таблица (DataGridView) на последующие строки или обрезается, когда оно слишком длинное и не помещается на одной строке.")]
        [Category("Макет")]
        [Browsable(true)]
        public new DataGridViewTriState WrapMode
        {
            get { return (DataGridViewTriState)base.WrapMode; }
            set { base.WrapMode = (System.Windows.Forms.DataGridViewTriState)value; }
        }

        [DisplayName("ЦветФона")]
        [Description("Возвращает или задает цвет фона ячейки объекта Таблица (DataGridView).")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        [DisplayName("ЦветФонаВыделенного")]
        [Description("Возвращает или задает цвет фона, используемый ячейкой объекта Таблица (DataGridView), когда она выбрана.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color SelectionBackColor
        {
            get { return base.SelectionBackColor; }
            set { base.SelectionBackColor = value; }
        }

        [DisplayName("Шрифт")]
        [Description("Возвращает или задает шрифт, применимый к текстовому содержимому ячейки объекта Таблица (DataGridView).")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyFontConverter))]
        public new Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        [DisplayName("ИмяСтиля")]
        [Category("Прочее")]
        [Browsable(true)]
        [ReadOnly(true)]
        public string NameStyle { get; set; }

        [Browsable(false)]
        public new dynamic DataSourceNullValue { get; set; }
        
        [Browsable(false)]
        public new dynamic Format { get; set; }
        
        [Browsable(false)]
        public new dynamic FormatProvider { get; set; }
        
        [Browsable(false)]
        public new dynamic IsDataSourceNullValueDefault { get; set; }
        
        [Browsable(false)]
        public new dynamic IsFormatProviderDefault { get; set; }
        
        [Browsable(false)]
        public new dynamic IsNullValueDefault { get; set; }
        
        [Browsable(false)]
        public new dynamic NullValue { get; set; }
		
        [Browsable(false)]
        public new dynamic Tag { get; set; }
		
        [Browsable(false)]
        public string DefaultValues
        {
            get
            {
                return @"
ОсновнойЦвет == ТекстЭлемента
ОсновнойЦветВыделенного == ТекстВыбранных
ЦветФона == Окно
ЦветФонаВыделенного == ФонВыбранных
Шрифт == Microsoft Sans Serif; 7,8pt
Выравнивание == СерединаЛево
Заполнение == 0; 0; 0; 0
Перенос == Ложь
ИмяСтиля == 
";
            }
            set { }
        }
		
        [Browsable(false)]
        public string RequiredValues
        {
            get
            {
                return @"
ИмяСтиля ==
";
            }
        }
    }
}
