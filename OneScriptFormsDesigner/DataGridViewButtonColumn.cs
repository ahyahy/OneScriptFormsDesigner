using System.ComponentModel;
using System.Drawing.Design;
using System;

namespace osfDesigner
{
    public class DataGridViewButtonColumn : System.Windows.Forms.DataGridViewButtonColumn
    {

        public DataGridViewButtonColumn()
        {
            Visible_osf = base.Visible;
        }

        [DisplayName("ВесЗаполнения")]
        [Description("Возвращает или задает значение, представляющее ширину колонки, для которой свойство РежимАвтоРазмера (AutoSizeMode) установлено в  значение Заполнение (Fill), относительно ширины других колонок элемента управления, находящихся в этом режиме.")]
        [Category("Макет")]
        [Browsable(true)]
        public new int FillWeight
        {
            get { return Convert.ToInt32(base.FillWeight); }
            set { base.FillWeight = Convert.ToSingle(value); }
        }

        [DisplayName("Закреплено")]
        [Description("Возвращает или задает значение, указывающее, перемещается ли колонка, когда пользователь выполняет горизонтальную прокрутку элемента управления Таблица (DataGridView).")]
        [Category("Макет")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool Frozen
        {
            get { return base.Frozen; }
            set { base.Frozen = value; }
        }

        [DisplayName("ИзменяемыйРазмер")]
        [Description("Возвращает или задает значение, указывающее, возможно ли изменение размера колонки.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new DataGridViewTriState Resizable
        {
            get { return (DataGridViewTriState)base.Resizable; }
            set { base.Resizable = (System.Windows.Forms.DataGridViewTriState)value; }
        }

        [DisplayName("ИмяСвойстваДанных")]
        [Description("Возвращает или задает имя того свойства данных или колонки базы данных в источнике данных, с которым связана колонка КолонкаТаблицы (DataGridViewColumn).")]
        [Category("Данные")]
        [Browsable(true)]
        public new string DataPropertyName
        {
            get { return base.DataPropertyName; }
            set { base.DataPropertyName = value; }
        }

        [DisplayName("ИспользоватьТекстКакЗначение")]
        [Description("Возвращает или задает значение, указывающее, отображается ли значение свойства КолонкаКнопка.Текст (DataGridViewButtonColumn.Text) как текст на кнопках, которые содержат ячейки в данной колонке.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool UseColumnTextForButtonValue
        {
            get { return base.UseColumnTextForButtonValue; }
            set { base.UseColumnTextForButtonValue = value; }
        }

        [DisplayName("МинимальнаяШирина")]
        [Description("Возвращает или задает наименьшую ширину колонки (в пикселях).")]
        [Category("Макет")]
        [Browsable(true)]
        public new int MinimumWidth
        {
            get { return base.MinimumWidth; }
            set { base.MinimumWidth = value; }
        }

        [DisplayName("Отображать")]
        [Description("Возвращает или задает значение, показывающее, видима ли колонка.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public bool Visible_osf { get; set; }
				
        [Browsable(false)]
        public new bool Visible { get; set; }

        [DisplayName("ПлоскийСтиль")]
        [Description("Возвращает или задает плоский внешний вид для ячеек кнопок в колонке.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new FlatStyle FlatStyle
        {
            get { return (FlatStyle)base.FlatStyle; }
            set { base.FlatStyle = (System.Windows.Forms.FlatStyle)value; }
        }

        [DisplayName("РежимАвтоРазмера")]
        [Description("Возвращает или задает режим, в котором автоматически изменяется ширина колонки.")]
        [Category("Макет")]
        [Browsable(true)]
        public new DataGridViewAutoSizeColumnMode AutoSizeMode
        {
            get { return (DataGridViewAutoSizeColumnMode)base.AutoSizeMode; }
            set { base.AutoSizeMode = (System.Windows.Forms.DataGridViewAutoSizeColumnMode)value; }
        }

        [DisplayName("РежимСортировки")]
        [Description("Возвращает или задает режим сортировки для колонки.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new DataGridViewColumnSortMode SortMode
        {
            get { return (DataGridViewColumnSortMode)base.SortMode; }
            set { base.SortMode = (System.Windows.Forms.DataGridViewColumnSortMode)value; }
        }

        [DisplayName("СтильЯчейки")]
        [Description("Возвращает или задает стиль по умолчанию для ячеек колонки.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyDataGridViewCellStyleConverter))]
        [Editor(typeof(MyDataGridViewCellStyleEditor), typeof(UITypeEditor))]
        public new System.Windows.Forms.DataGridViewCellStyle DefaultCellStyle
        {
            get { return base.DefaultCellStyle; }
            set { base.DefaultCellStyle = value; }
        }

        [DisplayName("Текст")]
        [Description("Возвращает или задает текст, отображаемый на ячейке кнопки по умолчанию.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        [DisplayName("ТекстЗаголовка")]
        [Description("Возвращает или задает текст заголовка колонки для ячейки.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new string HeaderText
        {
            get { return base.HeaderText; }
            set { base.HeaderText = value; }
        }

        [DisplayName("ТекстПодсказки")]
        [Description("Возвращает или задает текст, используемый для подсказок.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new string ToolTipText
        {
            get { return base.ToolTipText; }
            set { base.ToolTipText = value; }
        }

        [DisplayName("ТолькоЧтение")]
        [Description("Возвращает или задает значение, указывающее, может ли пользователь изменять ячейки колонки.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool ReadOnly
        {
            get { return base.ReadOnly; }
            set { base.ReadOnly = value; }
        }

        [DisplayName("Ширина")]
        [Description("Возвращает или задает текущую ширину колонки.")]
        [Category("Макет")]
        [Browsable(true)]
        public new int Width
        {
            get { return base.Width; }
            set { base.Width = value; }
        }

        [DisplayName("ШиринаРазделителя")]
        [Description("Возвращает или задает ширину (в пикселях) разделителя колонки.")]
        [Category("Макет")]
        [Browsable(true)]
        public new int DividerWidth
        {
            get { return base.DividerWidth; }
            set { base.DividerWidth = value; }
        }

        [DisplayName("ИмяКолонки")]
        [Category("Прочее")]
        [Browsable(true)]
        [ReadOnly(true)]
        public string NameColumn { get; set; }
		
        [Browsable(false)]
        public new dynamic CellTemplate { get; set; }
        
        [Browsable(false)]
        public new dynamic CellType { get; set; }
        
        [Browsable(false)]
        public new dynamic ContextMenuStrip { get; set; }
        
        [Browsable(false)]
        public new dynamic DefaultHeaderCellType { get; set; }
        
        [Browsable(false)]
        public new dynamic HasDefaultCellStyle { get; set; }
        
        [Browsable(false)]
        public new dynamic InheritedAutoSizeMode { get; set; }
        
        [Browsable(false)]
        public new dynamic InheritedStyle { get; set; }
        
        [Browsable(false)]
        public new dynamic IsDataBound { get; set; }
        
        [Browsable(false)]
        public new dynamic Site { get; set; }
        
        [Browsable(false)]
        public new dynamic State { get; set; }
        
        [Browsable(false)]
        public new dynamic Tag { get; set; }
        
        [Browsable(false)]
        public new dynamic ValueType { get; set; }

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
ТекстЗаголовка ==
СтильЯчейки ==
";
            }
        }
    }
}
