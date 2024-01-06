using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Windows.Forms;

namespace osfDesigner
{
    public class DataGridTableStyle : System.Windows.Forms.DataGridTableStyle
    {
        public System.Windows.Forms.DataGridTableStyle M_DataGridTableStyle;

        [Browsable(false)]
        public System.Windows.Forms.DataGridTableStyle OriginalObj
        {
            get { return M_DataGridTableStyle; }
            set { M_DataGridTableStyle = value; }
        }
        
        public DataGridTableStyle()
        {
        }

        [DisplayName("ИмяОтображаемого")]
        [Description("Возвращает или задает имя, используемое для сопоставления этой таблицы с конкретным источником данных.")]
        [Category("Прочее")]
        [Browsable(true)]
        public new string MappingName
        {
            get { return M_DataGridTableStyle.MappingName; }
            set { M_DataGridTableStyle.MappingName = value; }
        }

        [DisplayName("ОсновнойЦвет")]
        [Description("Возвращает или задает основной цвет таблицы.")]
        [Category("Цвета")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color ForeColor
        {
            get { return M_DataGridTableStyle.ForeColor; }
            set { M_DataGridTableStyle.ForeColor = value; }
        }

        [DisplayName("ОсновнойЦветЗаголовков")]
        [Description("Возвращает или задает основной цвет заголовков.")]
        [Category("Цвета")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color HeaderForeColor
        {
            get { return M_DataGridTableStyle.HeaderForeColor; }
            set { M_DataGridTableStyle.HeaderForeColor = value; }
        }

        [DisplayName("ОтображатьЗаголовкиСтолбцов")]
        [Description("Возвращает или задает значение, показывающее, являются ли заголовки колонок видимыми.")]
        [Category("Показать")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool ColumnHeadersVisible
        {
            get { return M_DataGridTableStyle.ColumnHeadersVisible; }
            set { M_DataGridTableStyle.ColumnHeadersVisible = value; }
        }

        [DisplayName("ОтображатьЗаголовкиСтрок")]
        [Description("Возвращает или задает значение, показывающее, являются ли заголовки строк видимыми.")]
        [Category("Показать")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool RowHeadersVisible
        {
            get { return M_DataGridTableStyle.RowHeadersVisible; }
            set { M_DataGridTableStyle.RowHeadersVisible = value; }
        }

        [DisplayName("ПредпочтительнаяВысотаСтрок")]
        [Description("Возвращает или задает высоту, используемую для создания строк при отображении новой сетки.")]
        [Category("Макет")]
        [Browsable(true)]
        public new int PreferredRowHeight
        {
            get { return M_DataGridTableStyle.PreferredRowHeight; }
            set { M_DataGridTableStyle.PreferredRowHeight = value; }
        }

        [DisplayName("ПредпочтительнаяШиринаСтолбцов")]
        [Description("Возвращает или задает ширину, используемую для создания колонок при отображении новой сетки.")]
        [Category("Макет")]
        [Browsable(true)]
        public new int PreferredColumnWidth
        {
            get { return M_DataGridTableStyle.PreferredColumnWidth; }
            set { M_DataGridTableStyle.PreferredColumnWidth = value; }
        }

        [DisplayName("РазрешитьСортировку")]
        [Description("Возвращает или задает значение, показывающее, разрешена ли сортировка в таблице.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool AllowSorting
        {
            get { return M_DataGridTableStyle.AllowSorting; }
            set { M_DataGridTableStyle.AllowSorting = value; }
        }

        [DisplayName("СтилиКолонкиСеткиДанных")]
        [Description("Возвращает коллекцию колонок, нарисованных для этой таблицы.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyCollectionConverter))]
        [Editor(typeof(MyGridColumnStylesCollectionEditor), typeof(UITypeEditor))]
        public new GridColumnStylesCollection GridColumnStyles
        {
            get { return M_DataGridTableStyle.GridColumnStyles; }
        }

        [DisplayName("ТолькоЧтение")]
        [Description("Возвращает или задает значение, показывающее, разрешено ли редактирование колонок.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool ReadOnly
        {
            get { return M_DataGridTableStyle.ReadOnly; }
            set { M_DataGridTableStyle.ReadOnly = value; }
        }

        [DisplayName("ЦветСетки")]
        [Description("Возвращает или задает цвет линий сетки.")]
        [Category("Цвета")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color GridLineColor
        {
            get { return M_DataGridTableStyle.GridLineColor; }
            set { M_DataGridTableStyle.GridLineColor = value; }
        }

        [DisplayName("ЦветФона")]
        [Description("Возвращает или задает цвет фона строк сетки с четными номерами.")]
        [Category("Цвета")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color BackColor
        {
            get { return M_DataGridTableStyle.BackColor; }
            set { M_DataGridTableStyle.BackColor = value; }
        }

        [DisplayName("ЦветФонаЗаголовков")]
        [Description("Возвращает или задает цвет фона заголовков.")]
        [Category("Цвета")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color HeaderBackColor
        {
            get { return M_DataGridTableStyle.HeaderBackColor; }
            set { M_DataGridTableStyle.HeaderBackColor = value; }
        }

        [DisplayName("ЦветФонаНечетныхСтрок")]
        [Description("Возвращает или задает цвет фона строк сетки с нечетными номерами.")]
        [Category("Цвета")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color AlternatingBackColor
        {
            get { return M_DataGridTableStyle.AlternatingBackColor; }
            set { M_DataGridTableStyle.AlternatingBackColor = value; }
        }

        [DisplayName("ШиринаЗаголовковСтрок")]
        [Description("Возвращает или задает ширину заголовков строк.")]
        [Category("Макет")]
        [Browsable(true)]
        public new int RowHeaderWidth
        {
            get { return M_DataGridTableStyle.RowHeaderWidth; }
            set { M_DataGridTableStyle.RowHeaderWidth = value; }
        }

        [DisplayName("ШрифтЗаголовков")]
        [Description("Возвращает или задает шрифт, используемый для названий заголовков.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyFontConverter))]
        public new Font HeaderFont
        {
            get { return M_DataGridTableStyle.HeaderFont; }
            set { M_DataGridTableStyle.HeaderFont = value; }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.DataGridLineStyle GridLineStyle { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Drawing.Color LinkColor { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Drawing.Color SelectionBackColor { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Drawing.Color SelectionForeColor { get; set; }
		
        [DisplayName("ИмяСтиля")]
        [Category("Прочее")]
        [Browsable(true)]
        [ReadOnly(true)]
        public string NameStyle { get; set; }
		
        [DisplayName("Текст")]
        [Category("Прочее")]
        [Browsable(true)]
        public string Text { get; set; }

        [Browsable(false)]
        public string DefaultValues { get; set; }
			
        [Browsable(false)]
        public string RequiredValues
        {
            get
            {
                return @"
СтилиКолонкиСеткиДанных ==
Текст ==
";
            }
        }
    }
}
