using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Windows.Forms;

namespace osfDesigner
{
    public class DataGridView : System.Windows.Forms.DataGridView
    {

        private bool _AutoNumberingRows_osf;
        private string _DoubleClick_osf;
        private string _CellDoubleClick_osf;
        private string _CellValueChanged_osf;
        private string _KeyUp_osf;
        private string _KeyDown_osf;
        private string _KeyPress_osf;
        private string _MouseEnter_osf;
        private string _CellMouseEnter_osf;
        private string _MouseLeave_osf;
        private string _CellMouseLeave_osf;
        private string _Click_osf;
        private string _CellContentClick_osf;
        private string _CellClick_osf;
        private string _LocationChanged_osf;
        private string _Enter_osf;
        private string _RowEnter_osf;
        private string _CellEnter_osf;
        private string _MouseHover_osf;
        private string _ColumnHeaderMouseClick_osf;
        private string _RowHeaderMouseClick_osf;
        private string _MouseDown_osf;
        private string _CellMouseDown_osf;
        private string _MouseUp_osf;
        private string _CellMouseUp_osf;
        private string _Move_osf;
        private string _MouseMove_osf;
        private string _CellMouseMove_osf;
        private string _Paint_osf;
        private string _LostFocus_osf;
        private string _Leave_osf;
        private string _RowLeave_osf;
        private string _CellLeave_osf;
        private string _SizeChanged_osf;
        private string _CellEndEdit_osf;
        private string _CellBeginEdit_osf;
        public DataGridViewCellStyleHeaders columnHeadersDefaultCellStyle;
        public DataGridViewCellStyleHeaders rowHeadersDefaultCellStyle;
        private bool _ArrowRowHeaders_osf;
        private string _TextChanged_osf;
        private string _CurrentCellChanged_osf;
        private string _ControlAdded_osf;
        private string _ControlRemoved_osf;

        public DataGridView()
        {
            columnHeadersDefaultCellStyle = new DataGridViewCellStyleHeaders(base.ColumnHeadersDefaultCellStyle);
            rowHeadersDefaultCellStyle = new DataGridViewCellStyleHeaders(base.RowHeadersDefaultCellStyle);

            columnHeadersDefaultCellStyle.NameStyle = "";
            rowHeadersDefaultCellStyle.NameStyle = "";

            columnHeadersDefaultCellStyle.DefaultValues = @"
ОсновнойЦвет == ТекстОкна
ОсновнойЦветВыделенного == ТекстВыбранных
ЦветФона == ЛицеваяЭлемента
ЦветФонаВыделенного == ФонВыбранных
Шрифт == Microsoft Sans Serif; 7,8pt
Выравнивание == СерединаЛево
Заполнение == 0; 0; 0; 0
Перенос == Истина
ИмяСтиля == Таблица1СтильЯчейки
";
            rowHeadersDefaultCellStyle.DefaultValues = @"
ОсновнойЦвет == ТекстОкна
ОсновнойЦветВыделенного == ТекстВыбранных
ЦветФона == ЛицеваяЭлемента
ЦветФонаВыделенного == ФонВыбранных
Шрифт == Microsoft Sans Serif; 7,8pt
Выравнивание == СерединаЛево
Заполнение == 0; 0; 0; 0
Перенос == Истина
ИмяСтиля == Таблица1СтильЯчейки
";
            Enabled_osf = base.Enabled;
            Visible_osf = base.Visible;
        }

        [DisplayName("АвтоНумерацияСтрок")]
        [Description("Возвращает или задает значение, указывающее, применять ли в заголовках строк таблицы автоматическую нумерацию.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public  bool AutoNumberingRows_osf
        {
            get { return _AutoNumberingRows_osf; }
            set { _AutoNumberingRows_osf = value; }
        }

        [DisplayName("АвтоШиринаЗаголовковСтрок")]
        [Description("Возвращает или задает значение, указывающее, может ли настраиваться ширина заголовков строк, а также может ли она настраиваться пользователем или же настройка  выполняется автоматически по содержимому заголовков.")]
        [Category("Прочее")]
        [Browsable(true)]
        public new DataGridViewRowHeadersWidthSizeMode AutoResizeRowHeadersWidth
        {
            get { return (DataGridViewRowHeadersWidthSizeMode)base.RowHeadersWidthSizeMode; }
            set { base.RowHeadersWidthSizeMode = (System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode)value; }
        }

        [DisplayName("ВысотаЗаголовковКолонок")]
        [Description("Возвращает или задает высоту (в пикселях) строки заголовков колонок.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new int ColumnHeadersHeight
        {
            get { return base.ColumnHeadersHeight; }
            set { base.ColumnHeadersHeight = value; }
        }

        [DisplayName("ДвойноеНажатие")]
        [Description("Возвращает или задает код, когда элемент управления дважды щелкнут.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string DoubleClick_osf
        {
            get { return _DoubleClick_osf; }
            set { _DoubleClick_osf = value; }
        }

        [DisplayName("ДвойноеНажатиеЯчейки")]
        [Description("Возвращает или задает код для выполнения при щелчке в любой части ячейки.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string CellDoubleClick_osf
        {
            get { return _CellDoubleClick_osf; }
            set { _CellDoubleClick_osf = value; }
        }

        [DisplayName("ДобавлятьСтроки")]
        [Description("Возвращает или задает значение, указывающее, разрешено ли пользователю добавление строк.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool AllowUserToAddRows
        {
            get { return base.AllowUserToAddRows; }
            set { base.AllowUserToAddRows = value; }
        }

        [DisplayName("Доступность")]
        [Description("Возвращает или задает значение, указывающее, может ли элемент управления реагировать на действия пользователя.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public bool Enabled_osf { get; set; }
				
        [Browsable(false)]
        public new bool Enabled { get; set; }

        [DisplayName("ЗначениеЯчейкиИзменено")]
        [Description("Возвращает или задает код для выполнения при изменении значения ячейки.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string CellValueChanged_osf
        {
            get { return _CellValueChanged_osf; }
            set { _CellValueChanged_osf = value; }
        }

        [DisplayName("ИзменятьРазмерКолонок")]
        [Description("Возвращает или задает значение, указывающее, может ли пользователь изменять размер колонок.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool AllowUserToResizeColumns
        {
            get { return base.AllowUserToResizeColumns; }
            set { base.AllowUserToResizeColumns = value; }
        }

        [DisplayName("ИзменятьРазмерСтрок")]
        [Description("Возвращает или задает значение, указывающее, может ли пользователь изменять размер строк.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool AllowUserToResizeRows
        {
            get { return base.AllowUserToResizeRows; }
            set { base.AllowUserToResizeRows = value; }
        }

        [DisplayName("ИспользоватьКурсорОжидания")]
        [Description("Возвращает или задает значение, указывающее, следует ли использовать курсор ожидания для текущего элемента управления и всех дочерних элементов управления.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool UseWaitCursor
        {
            get { return base.UseWaitCursor; }
            set { base.UseWaitCursor = value; }
        }

        [DisplayName("КлавишаВверх")]
        [Description("Возвращает или задает код для события, которое происходит, когда отпускается клавиша, если элемент управления имеет фокус.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string KeyUp_osf
        {
            get { return _KeyUp_osf; }
            set { _KeyUp_osf = value; }
        }

        [DisplayName("КлавишаВниз")]
        [Description("Возвращает или задает код, выполняемый при нажатии клавиши в то время как элемент управления имеет фокус.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string KeyDown_osf
        {
            get { return _KeyDown_osf; }
            set { _KeyDown_osf = value; }
        }

        [DisplayName("КлавишаНажата")]
        [Description("Возвращает или задает код, выполняемый при нажатии клавиши в то время как элемент управления имеет фокус.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string KeyPress_osf
        {
            get { return _KeyPress_osf; }
            set { _KeyPress_osf = value; }
        }

        [DisplayName("Колонки")]
        [Description("Возвращает коллекцию, содержащую все колонки элемента управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyCollectionConverter))]
        [Editor(typeof(MyDataGridViewColumnCollectionEditor), typeof(UITypeEditor))]
        public new DataGridViewColumnCollection Columns
        {
            get { return base.Columns; }
        }

        [DisplayName("Курсор")]
        [Description("Возвращает или задает курсор, который отображается, когда указатель мыши находится над элементом управления.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyCursorConverter))]
        [Editor(typeof(MyCursorEditor), typeof(UITypeEditor))]
        public new Cursor Cursor
        {
            get { return base.Cursor; }
            set { base.Cursor = value; }
        }

        [DisplayName("МышьНадЭлементом")]
        [Description("Возвращает или задает код для выполнения, когда указатель мыши находится над элементом управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseEnter_osf
        {
            get { return _MouseEnter_osf; }
            set { _MouseEnter_osf = value; }
        }

        [DisplayName("МышьНадЯчейкой")]
        [Description("Возвращает или задает код для выполнения при наведении указателя мыши на ячейку.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string CellMouseEnter_osf
        {
            get { return _CellMouseEnter_osf; }
            set { _CellMouseEnter_osf = value; }
        }

        [DisplayName("МышьПокинулаЭлемент")]
        [Description("Возвращает или задает код для выполнения, когда указатель мыши покидает пределы элемента управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseLeave_osf
        {
            get { return _MouseLeave_osf; }
            set { _MouseLeave_osf = value; }
        }

        [DisplayName("МышьПокинулаЯчейку")]
        [Description("Возвращает или задает код для выполнения когда указатель мыши выходит за пределы ячейки.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string CellMouseLeave_osf
        {
            get { return _CellMouseLeave_osf; }
            set { _CellMouseLeave_osf = value; }
        }

        [DisplayName("Нажатие")]
        [Description("Возвращает или задает код для выполнения при нажатии элемента управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Click_osf
        {
            get { return _Click_osf; }
            set { _Click_osf = value; }
        }

        [DisplayName("НажатиеСодержимогоЯчейки")]
        [Description("Возвращает или задает код для выполнения при щелчке по содержимому ячейки.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string CellContentClick_osf
        {
            get { return _CellContentClick_osf; }
            set { _CellContentClick_osf = value; }
        }

        [DisplayName("НажатиеЯчейки")]
        [Description("Возвращает или задает код для выполнения при щелчке по любой части ячейки.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string CellClick_osf
        {
            get { return _CellClick_osf; }
            set { _CellClick_osf = value; }
        }

        [DisplayName("Отображать")]
        [Description("Возвращает или задает значение, указывающее, отображаются ли элемент управления и все его дочерние элементы управления. Истина, если элемент управления отображается; в противном случае - Ложь.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public bool Visible_osf { get; set; }
				
        [Browsable(false)]
        public new bool Visible { get; set; }

        [DisplayName("ОтображатьЗаголовкиСтолбцов")]
        [Description("Возвращает или задает значение, указывающее, отображается ли колонка, содержащая заголовки строк.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool ColumnHeadersVisible
        {
            get { return base.ColumnHeadersVisible; }
            set { base.ColumnHeadersVisible = value; }
        }

        [DisplayName("ОтображатьЗаголовкиСтрок")]
        [Description("Возвращает или задает значение, указывающее, отображается ли строка заголовков колонок.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool RowHeadersVisible
        {
            get { return base.RowHeadersVisible; }
            set { base.RowHeadersVisible = value; }
        }

        [DisplayName("Положение")]
        [Description("Возвращает или задает координаты верхнего левого угла элемента управления относительно верхнего левого угла его контейнера.")]
        [Category("Макет")]
        [Browsable(true)]
        [TypeConverter(typeof(MyLocationConverter))]
        [Editor(typeof(MyLocationEditor), typeof(UITypeEditor))]
        public Point Location_osf
        {
            get { return base.Location; }
            set { base.Location = value; }
        }
				
        [Browsable(false)]
        public new Point Location { get; set; }

        [DisplayName("ПоложениеИзменено")]
        [Description("Возвращает или задает код для выполнения при изменении свойства Положение (Location).")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string LocationChanged_osf
        {
            get { return _LocationChanged_osf; }
            set { _LocationChanged_osf = value; }
        }

        [DisplayName("ПолосыПрокрутки")]
        [Description("Возвращает или задает тип полос прокрутки, отображающихся для элемента управления Таблица (DataGridView).")]
        [Category("Макет")]
        [Browsable(true)]
        public new ScrollBars ScrollBars
        {
            get { return (ScrollBars)base.ScrollBars; }
            set { base.ScrollBars = (System.Windows.Forms.ScrollBars)value; }
        }

        [DisplayName("ПорядокОбхода")]
        [Description("Возвращает или задает последовательность перехода по клавише TAB между элементами управления внутри контейнера.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new int TabIndex
        {
            get { return base.TabIndex; }
            set { base.TabIndex = value; }
        }

        [DisplayName("ПриВходе")]
        [Description("Возвращает или задает код для выполнения при входе в элемент управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Enter_osf
        {
            get { return _Enter_osf; }
            set { _Enter_osf = value; }
        }

        [DisplayName("ПриВходеВСтроку")]
        [Description("Возвращает или задает код для выполнения, когда строка получает фокус ввода, но перед этим становится текущей строкой.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string RowEnter_osf
        {
            get { return _RowEnter_osf; }
            set { _RowEnter_osf = value; }
        }

        [DisplayName("ПриВходеВЯчейку")]
        [Description("Возвращает или задает код для выполнения, когда текущая ячейка изменяется в элементе управления Таблица (DataGridView), или когда этот элемент управления получает фокус ввода.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string CellEnter_osf
        {
            get { return _CellEnter_osf; }
            set { _CellEnter_osf = value; }
        }

        [DisplayName("ПриЗадержкеМыши")]
        [Description("Возвращает или задает код для выполнения когда указатель мыши задерживается на элементе управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseHover_osf
        {
            get { return _MouseHover_osf; }
            set { _MouseHover_osf = value; }
        }

        [DisplayName("ПриНажатииЗаголовкаКолонки")]
        [Description("Возвращает или задает код для выполнения при щелчке заголовка колонки.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string ColumnHeaderMouseClick_osf
        {
            get { return _ColumnHeaderMouseClick_osf; }
            set { _ColumnHeaderMouseClick_osf = value; }
        }

        [DisplayName("ПриНажатииЗаголовкаСтроки")]
        [Description("Возвращает или задает код для выполнения при щелчке в пределах заголовка строки.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string RowHeaderMouseClick_osf
        {
            get { return _RowHeaderMouseClick_osf; }
            set { _RowHeaderMouseClick_osf = value; }
        }

        [DisplayName("ПриНажатииКнопкиМыши")]
        [Description("Возвращает или задает код для выполнения при нажатии кнопки мыши, если указатель мыши находится на элементе управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseDown_osf
        {
            get { return _MouseDown_osf; }
            set { _MouseDown_osf = value; }
        }

        [DisplayName("ПриНажатииКнопкиМышиВЯчейке")]
        [Description("Возвращает или задает код для выполнения при нажатии кнопки мыши, когда указатель мыши находится в пределах ячейки.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string CellMouseDown_osf
        {
            get { return _CellMouseDown_osf; }
            set { _CellMouseDown_osf = value; }
        }

        [DisplayName("ПриОтпусканииМыши")]
        [Description("Возвращает или задает код для выполнения при отпускании кнопки мыши, когда указатель мыши находится на элементе управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseUp_osf
        {
            get { return _MouseUp_osf; }
            set { _MouseUp_osf = value; }
        }

        [DisplayName("ПриОтпусканииМышиНадЯчейкой")]
        [Description("Возвращает или задает код для выполнения, если пользователь отпускает кнопку мыши, когда указатель мыши находится на ячейке.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string CellMouseUp_osf
        {
            get { return _CellMouseUp_osf; }
            set { _CellMouseUp_osf = value; }
        }

        [DisplayName("ПриПеремещении")]
        [Description("Возвращает или задает код для выполнения при перемещении элемента управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Move_osf
        {
            get { return _Move_osf; }
            set { _Move_osf = value; }
        }

        [DisplayName("ПриПеремещенииМыши")]
        [Description("Возвращает или задает код для выполнения, когда указатель мыши переместился над элементом управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseMove_osf
        {
            get { return _MouseMove_osf; }
            set { _MouseMove_osf = value; }
        }

        [DisplayName("ПриПеремещенииМышиНадЯчейкой")]
        [Description("Возвращает или задает код для выполнения, когда указатель мыши перемещается в пределы элемента управления Таблица (DataGridView).")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string CellMouseMove_osf
        {
            get { return _CellMouseMove_osf; }
            set { _CellMouseMove_osf = value; }
        }

        [DisplayName("ПриПерерисовке")]
        [Description("Возвращает или задает код для выполнения, когда элемент управления перерисован.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Paint_osf
        {
            get { return _Paint_osf; }
            set { _Paint_osf = value; }
        }

        [DisplayName("ПриПотереФокуса")]
        [Description("Возвращает или задает код для выполнения, когда элемент управления теряет фокус.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string LostFocus_osf
        {
            get { return _LostFocus_osf; }
            set { _LostFocus_osf = value; }
        }

        [DisplayName("ПриУходе")]
        [Description("Возвращает или задает код для выполнения, когда фокус ввода покидает элемент управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Leave_osf
        {
            get { return _Leave_osf; }
            set { _Leave_osf = value; }
        }

        [DisplayName("ПриУходеИзСтроки")]
        [Description("Возвращает или задает код для выполнения, когда строка теряет фокус (строка больше не является текущей).")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string RowLeave_osf
        {
            get { return _RowLeave_osf; }
            set { _RowLeave_osf = value; }
        }

        [DisplayName("ПриУходеИзЯчейки")]
        [Description("Возвращает или задает код для выполнения при перемещении фокуса с данной ячейки (ячейка больше не является текущей).")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string CellLeave_osf
        {
            get { return _CellLeave_osf; }
            set { _CellLeave_osf = value; }
        }

        [DisplayName("Размер")]
        [Description("Возвращает или задает высоту и ширину элемента управления.")]
        [Category("Макет")]
        [Browsable(true)]
        [TypeConverter(typeof(MySizeConverter))]
        [Editor(typeof(MySizeEditor), typeof(UITypeEditor))]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }

        [DisplayName("РазмерИзменен")]
        [Description("Возвращает или задает код для выполнения при изменении значения свойства Размер (Size).")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string SizeChanged_osf
        {
            get { return _SizeChanged_osf; }
            set { _SizeChanged_osf = value; }
        }

        [DisplayName("РедактированиеЯчейкиЗавершено")]
        [Description("Возвращает или задает код для выполнения при остановке режима правки для выбранной ячейки.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string CellEndEdit_osf
        {
            get { return _CellEndEdit_osf; }
            set { _CellEndEdit_osf = value; }
        }

        [DisplayName("РедактированиеЯчейкиНачато")]
        [Description("Возвращает или задает код для выполнения при запуске режима правки для выбранных ячеек.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string CellBeginEdit_osf
        {
            get { return _CellBeginEdit_osf; }
            set { _CellBeginEdit_osf = value; }
        }

        [DisplayName("РежимАвтоРазмераКолонок")]
        [Description("Возвращает или задает значение, указывающее, как определяется ширина колонки.")]
        [Category("Макет")]
        [Browsable(true)]
        public DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode1
        {
            get { return (DataGridViewAutoSizeColumnsMode)base.AutoSizeColumnsMode; }
            set { base.AutoSizeColumnsMode = (System.Windows.Forms.DataGridViewAutoSizeColumnsMode)value; }
        }

        [DisplayName("РежимАвтоРазмераСтрок")]
        [Description("Возвращает или задает значение, указывающее, как определяется высота строки.")]
        [Category("Макет")]
        [Browsable(true)]
        public new DataGridViewAutoSizeRowsMode AutoSizeRowsMode
        {
            get { return (DataGridViewAutoSizeRowsMode)base.AutoSizeRowsMode; }
            set { base.AutoSizeRowsMode = (System.Windows.Forms.DataGridViewAutoSizeRowsMode)value; }
        }

        [DisplayName("РежимВыбора")]
        [Description("Возвращает или задает значение, указывающее, каким образом могут быть выбраны ячейки объекта Таблица (DataGridView).")]
        [Category("Поведение")]
        [Browsable(true)]
        public new DataGridViewSelectionMode SelectionMode
        {
            get { return (DataGridViewSelectionMode)base.SelectionMode; }
            set { base.SelectionMode = (System.Windows.Forms.DataGridViewSelectionMode)value; }
        }

        [DisplayName("РежимВысотыЗаголовковКолонок")]
        [Description("Возвращает или задает варианты настройки высоты заголовков колонок.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode
        {
            get { return (DataGridViewColumnHeadersHeightSizeMode)base.ColumnHeadersHeightSizeMode; }
            set { base.ColumnHeadersHeightSizeMode = (System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode)value; }
        }

        [DisplayName("СтильЗаголовковКолонок")]
        [Description("Возвращает или задает стиль заголовка колонки по умолчанию.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyDataGridViewCellStyleConverter))]
        [Editor(typeof(MyColumnHeadersCellStyleEditor), typeof(UITypeEditor))]
        public new System.Windows.Forms.DataGridViewCellStyle ColumnHeadersDefaultCellStyle
        {
            get { return columnHeadersDefaultCellStyle; }
            set
            {
                string NameStyle1 = columnHeadersDefaultCellStyle.NameStyle;
                columnHeadersDefaultCellStyle = new DataGridViewCellStyleHeaders(value);
                columnHeadersDefaultCellStyle.NameStyle = NameStyle1;
                base.ColumnHeadersDefaultCellStyle = value;
            }
        }

        [DisplayName("СтильЗаголовковСтрок")]
        [Description("Возвращает или задает стиль по умолчанию, применяемый к ячейкам заголовков строк.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyDataGridViewCellStyleConverter))]
        [Editor(typeof(MyRowHeadersCellStyleEditor), typeof(UITypeEditor))]
        public new System.Windows.Forms.DataGridViewCellStyle RowHeadersDefaultCellStyle
        {
            get { return rowHeadersDefaultCellStyle; }
            set
            {
                string NameStyle1 = rowHeadersDefaultCellStyle.NameStyle;
                rowHeadersDefaultCellStyle = new DataGridViewCellStyleHeaders(value);
                rowHeadersDefaultCellStyle.NameStyle = NameStyle1;
                base.RowHeadersDefaultCellStyle = value;
            }
        }

        [DisplayName("СтрелкаЗаголовковСтрок")]
        [Description("Возвращает или задает значение, указывающее, отображать ли в заголовках строк таблицы изображение стрелки.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public  bool ArrowRowHeaders_osf
        {
            get { return _ArrowRowHeaders_osf; }
            set { _ArrowRowHeaders_osf = value; }
        }

        [DisplayName("Стыковка")]
        [Description("Возвращает или задает, к какому краю родительского контейнера прикреплен элемент управления.")]
        [Category("Макет")]
        [Browsable(true)]
        [Editor(typeof(MyDockEditor), typeof(UITypeEditor))]
        public new DockStyle Dock
        {
            get { return (DockStyle)base.Dock; }
            set { base.Dock = (System.Windows.Forms.DockStyle)value; }
        }

        [DisplayName("ТабФокус")]
        [Description("Возвращает или задает значение, указывающее, может ли пользователь перевести фокус на данный элемент управления при помощи клавиши TAB.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool TabStop
        {
            get { return base.TabStop; }
            set { base.TabStop = value; }
        }

        [DisplayName("ТекстИзменен")]
        [Description("Возвращает или задает код для выполнения, при изменении свойства Текст (Text).")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string TextChanged_osf
        {
            get { return _TextChanged_osf; }
            set { _TextChanged_osf = value; }
        }

        [DisplayName("ТекущаяЯчейкаИзменена")]
        [Description("Возвращает или задает код для выполнения при изменении значения свойства Таблица.ТекущаяЯчейка (DataGridView.CurrentCell).")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string CurrentCellChanged_osf
        {
            get { return _CurrentCellChanged_osf; }
            set { _CurrentCellChanged_osf = value; }
        }

        [DisplayName("ТолькоЧтение")]
        [Description("Возвращает или задает значение, указывающее, может ли пользователь изменять ячейки.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool ReadOnly
        {
            get { return base.ReadOnly; }
            set { base.ReadOnly = value; }
        }

        [DisplayName("ЦветФонаТаблицы")]
        [Description("Возвращает или задает цвет области, отличной от строки сетки.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color BackgroundColor
        {
            get { return base.BackgroundColor; }
            set { base.BackgroundColor = value; }
        }

        [DisplayName("ШиринаЗаголовковСтрок")]
        [Description("Возвращает или задает ширину (в пикселях) колонки, содержащей заголовки строк.")]
        [Category("Макет")]
        [Browsable(true)]
        public new int RowHeadersWidth
        {
            get { return base.RowHeadersWidth; }
            set { base.RowHeadersWidth = value; }
        }

        [DisplayName("ЭлементДобавлен")]
        [Description("Возвращает или задает код для выполнения при добавлении нового элемента управления в ЭлементыУправления (ControlCollection).")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string ControlAdded_osf
        {
            get { return _ControlAdded_osf; }
            set { _ControlAdded_osf = value; }
        }

        [DisplayName("ЭлементУдален")]
        [Description("Возвращает или задает код для выполнения при удалении элемента управления из ЭлементыУправления (ControlCollection).")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string ControlRemoved_osf
        {
            get { return _ControlRemoved_osf; }
            set { _ControlRemoved_osf = value; }
        }

        [DisplayName("Якорь")]
        [Description("Возвращает или задает границы контейнера, с которым связан элемент управления, и определяет способ изменения размеров элемента управления при изменении размеров его родительского элемента.")]
        [Category("Макет")]
        [Browsable(true)]
        [Editor(typeof(MyAnchorEditor), typeof(UITypeEditor))]
        public new AnchorStyles Anchor
        {
            get { return (AnchorStyles)base.Anchor; }
            set { base.Anchor = (System.Windows.Forms.AnchorStyles)value; }
        }

        [Browsable(false)]
        public new dynamic AutoSizeColumnsMode { get; set; }
		
        [DisplayName("Шрифт")]
        [Description("Возвращает или задает шрифт текста, отображаемый элементом управления.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyFontConverter))]
        public new Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        [Browsable(false)]
        public new dynamic AccessibilityObject { get; set; }
        
        [Browsable(false)]
        public new dynamic AccessibleDefaultActionDescription { get; set; }
        
        [Browsable(false)]
        public new dynamic AccessibleDescription { get; set; }
        
        [Browsable(false)]
        public new dynamic AccessibleName { get; set; }
        
        [Browsable(false)]
        public new dynamic AccessibleRole { get; set; }
        
        [Browsable(false)]
        public new dynamic AdjustedTopLeftHeaderBorderStyle { get; set; }
        
        [Browsable(false)]
        public new dynamic AdvancedCellBorderStyle { get; set; }
        
        [Browsable(false)]
        public new dynamic AdvancedColumnHeadersBorderStyle { get; set; }
        
        [Browsable(false)]
        public new dynamic AdvancedRowHeadersBorderStyle { get; set; }
        
        [Browsable(false)]
        public new dynamic AllowDrop { get; set; }
        
        [Browsable(false)]
        public new dynamic AllowUserToDeleteRows { get; set; }
        
        [Browsable(false)]
        public new dynamic AllowUserToOrderColumns { get; set; }
        
        [Browsable(false)]
        public new dynamic AlternatingRowsDefaultCellStyle { get; set; }
        
        [Browsable(false)]
        public new dynamic AutoScrollOffset { get; set; }
        
        [Browsable(false)]
        public new dynamic AutoSize { get; set; }
        
        [Browsable(false)]
        public new dynamic BackgroundImageLayout { get; set; }
        
        [Browsable(false)]
        public new dynamic BindingContext { get; set; }
        
        [Browsable(false)]
        public new dynamic BorderStyle { get; set; }
        
        [Browsable(false)]
        public new dynamic CanSelect { get; set; }
        
        [Browsable(false)]
        public new dynamic CausesValidation { get; set; }
        
        [Browsable(false)]
        public new dynamic CellBorderStyle { get; set; }
        
        [Browsable(false)]
        public new dynamic ClipboardCopyMode { get; set; }
        
        [Browsable(false)]
        public new dynamic ColumnHeadersBorderStyle { get; set; }
        
        [Browsable(false)]
        public new dynamic CompanyName { get; set; }
        
        [Browsable(false)]
        public new dynamic Container { get; set; }
        
        [Browsable(false)]
        public new dynamic ContainsFocus { get; set; }
        
        [Browsable(false)]
        public new dynamic ContextMenuStrip { get; set; }
        
        [Browsable(false)]
        public new dynamic Created { get; set; }
        
        [Browsable(false)]
        public new dynamic CurrentCellAddress { get; set; }
        
        [Browsable(false)]
        public new dynamic DataBindings { get; set; }

        [Browsable(false)]
        public new string DataMember { get; set; }
        
        [Browsable(false)]
        public new dynamic DataSource { get; set; }
        
        [Browsable(false)]
        public new dynamic DefaultCellStyle { get; set; }
        
        [Browsable(false)]
        public dynamic DeviceDpi { get; set; }
        
        [Browsable(false)]
        public new dynamic DisplayRectangle { get; set; }
        
        [Browsable(false)]
        public new dynamic Disposing { get; set; }
        
        [Browsable(false)]
        public new dynamic EditingControl { get; set; }
        
        [Browsable(false)]
        public new dynamic EditingPanel { get; set; }
        
        [Browsable(false)]
        public new dynamic EditMode { get; set; }
        
        [Browsable(false)]
        public new dynamic EnableHeadersVisualStyles { get; set; }
        
        [Browsable(false)]
        public new dynamic FirstDisplayedCell { get; set; }
        
        [Browsable(false)]
        public new dynamic FirstDisplayedScrollingColumnHiddenWidth { get; set; }
        
        [Browsable(false)]
        public new dynamic FirstDisplayedScrollingColumnIndex { get; set; }
        
        [Browsable(false)]
        public new dynamic FirstDisplayedScrollingRowIndex { get; set; }
        
        [Browsable(false)]
        public new dynamic GridColor { get; set; }
        
        [Browsable(false)]
        public new dynamic Handle { get; set; }
        
        [Browsable(false)]
        public new dynamic HasChildren { get; set; }
        
        [Browsable(false)]
        public new dynamic HorizontalScrollingOffset { get; set; }
        
        [Browsable(false)]
        public new dynamic ImeMode { get; set; }
        
        [Browsable(false)]
        public new dynamic InvokeRequired { get; set; }
        
        [Browsable(false)]
        public new dynamic IsAccessible { get; set; }
        
        [Browsable(false)]
        public new dynamic IsCurrentCellDirty { get; set; }
        
        [Browsable(false)]
        public new dynamic IsCurrentCellInEditMode { get; set; }
        
        [Browsable(false)]
        public new dynamic IsCurrentRowDirty { get; set; }
        
        [Browsable(false)]
        public new dynamic IsDisposed { get; set; }
        
        [Browsable(false)]
        public new dynamic IsHandleCreated { get; set; }
        
        [Browsable(false)]
        public new dynamic IsMirrored { get; set; }
        
        [Browsable(false)]
        public dynamic Item { get; set; }
        
        [Browsable(false)]
        public new dynamic LayoutEngine { get; set; }
        
        [Browsable(false)]
        public new dynamic Margin { get; set; }
        
        [Browsable(false)]
        public new dynamic MaximumSize { get; set; }
        
        [Browsable(false)]
        public new dynamic MinimumSize { get; set; }
        
        [Browsable(false)]
        public new dynamic MultiSelect { get; set; }
        
        [Browsable(false)]
        public new dynamic NewRowIndex { get; set; }
        
        [Browsable(false)]
        public new dynamic Padding { get; set; }
        
        [Browsable(false)]
        public new dynamic PreferredSize { get; set; }
        
        [Browsable(false)]
        public new dynamic RecreatingHandle { get; set; }
        
        [Browsable(false)]
        public new dynamic Region { get; set; }
        
        [Browsable(false)]
        public new dynamic RightToLeft { get; set; }
        
        [Browsable(false)]
        public new dynamic RowHeadersBorderStyle { get; set; }
        
        [Browsable(false)]
        public new dynamic RowHeadersWidthSizeMode { get; set; }
        
        [Browsable(false)]
        public new dynamic RowsDefaultCellStyle { get; set; }
        
        [Browsable(false)]
        public new dynamic RowTemplate { get; set; }
        
        [Browsable(false)]
        public new dynamic SelectedCells { get; set; }
        
        [Browsable(false)]
        public new dynamic SelectedColumns { get; set; }
        
        [Browsable(false)]
        public new dynamic SelectedRows { get; set; }
        
        [Browsable(false)]
        public new dynamic ShowCellErrors { get; set; }
        
        [Browsable(false)]
        public new dynamic ShowCellToolTips { get; set; }
        
        [Browsable(false)]
        public new dynamic ShowEditingIcon { get; set; }
        
        [Browsable(false)]
        public new dynamic ShowRowErrors { get; set; }
        
        [Browsable(false)]
        public new dynamic Site { get; set; }
        
        [Browsable(false)]
        public new dynamic SortedColumn { get; set; }
        
        [Browsable(false)]
        public new dynamic SortOrder { get; set; }
        
        [Browsable(false)]
        public new dynamic StandardTab { get; set; }
        
        [Browsable(false)]
        public new dynamic Tag { get; set; }
        
        [Browsable(false)]
        public new dynamic TopLeftHeaderCell { get; set; }
        
        [Browsable(false)]
        public new dynamic UserSetCursor { get; set; }
        
        [Browsable(false)]
        public new dynamic VerticalScrollingOffset { get; set; }
        
        [Browsable(false)]
        public new dynamic VirtualMode { get; set; }
        
        [Browsable(false)]
        public new dynamic WindowTarget { get; set; }

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
Положение ==
ПорядокОбхода ==
Размер ==
Колонки ==
РежимВысотыЗаголовковКолонок ==
СтильЗаголовковКолонок ==
СтильЗаголовковСтрок ==
";
            }
        }
    }
}
