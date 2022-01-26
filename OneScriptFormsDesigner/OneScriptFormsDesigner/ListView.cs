using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing.Design; 
using System.Drawing;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    [Docking(DockingBehavior.Never)]
    public class ListView : System.Windows.Forms.ListView
    {
        private int tic1 = 0; // счетчик для правильной работы смарт-тэгов
        private string _DoubleClick_osf;
        private string _SelectedIndexChanged_osf;
        private string _KeyUp_osf;
        private string _KeyDown_osf;
        private string _KeyPress_osf;
        private string _ColumnClick_osf;
        private string _MouseEnter_osf;
        private string _MouseLeave_osf;
        private string _Click_osf;
        private string _BeforeLabelEdit_osf;
        private string _LocationChanged_osf;
        private string _AfterLabelEdit_osf;
        private string _ItemActivate_osf;
        private string _Enter_osf;
        private string _MouseHover_osf;
        private string _MouseDown_osf;
        private string _MouseUp_osf;
        private string _Move_osf;
        private string _MouseMove_osf;
        private string _Paint_osf;
        private string _LostFocus_osf;
        private string _Leave_osf;
        private string _SizeChanged_osf;
        private bool _AllowSorting_osf;
        private osfDesigner.View _View;
        private string _TextChanged_osf;
        private string _ControlAdded_osf;
        private string _ItemCheck_osf;
        private string _ControlRemoved_osf;

        public ListView()
        {
            Enabled_osf = base.Enabled;
            Visible_osf = base.Visible;
        }

        [DisplayName("АвтоУпорядочивание")]
        [Description("Возвращает или задает значение, определяющее автоматическое упорядочение значков.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool AutoArrange
        {
            get { return base.AutoArrange; }
            set { base.AutoArrange = value; }
        }

        [DisplayName("Активация")]
        [Description("Возвращает или задает тип действия, которое пользователь должен выполнить для активции элемента.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new ItemActivation Activation
        {
            get { return (ItemActivation)base.Activation; }
            set { base.Activation = (System.Windows.Forms.ItemActivation)value; }
        }

        [DisplayName("ВыбиратьПодэлементы")]
        [Description("Возвращает или задает значение, указывающее, выбираются ли при щелчке элемента все его подэлементы.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool FullRowSelect
        {
            get { return base.FullRowSelect; }
            set { base.FullRowSelect = value; }
        }

        [DisplayName("ВыборПриНаведении")]
        [Description("Возвращает или задает значение, указывающее, будет ли элемент автоматически выбираться, если указатель мыши задержится на нем нескольких секунд.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool HoverSelection
        {
            get { return base.HoverSelection; }
            set { base.HoverSelection = value; }
        }

        [DisplayName("Выравнивание")]
        [Description("Возвращает или задает выравнивание элементов в элементе управления.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new ListViewAlignment Alignment
        {
            get { return (ListViewAlignment)base.Alignment; }
            set { base.Alignment = (System.Windows.Forms.ListViewAlignment)value; }
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

        [DisplayName("Доступность")]
        [Description("Возвращает или задает значение, указывающее, может ли элемент управления реагировать на действия пользователя.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public bool Enabled_osf { get; set; }
				
        //скроем унаследованное свойство, для того чтобы оно не мешало нашему замещающему свойству использовать свой эдитор и конвертер.
        [Browsable(false)]
        public new bool Enabled { get; set; }

        [DisplayName("ИндексВыбранногоИзменен")]
        [Description("Возвращает или задает код для выполнения, когда изменились индексы выбранных в элементе управления СписокЭлементов (ListView).")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string SelectedIndexChanged_osf
        {
            get { return _SelectedIndexChanged_osf; }
            set { _SelectedIndexChanged_osf = value; }
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

        [DisplayName("КолонкаНажатие")]
        [Description("Возвращает или задает код для выполнения, когда пользователь нажимает кнопку заголовка колонки в элементе управления СписокЭлементов (ListView).")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string ColumnClick_osf
        {
            get { return _ColumnClick_osf; }
            set { _ColumnClick_osf = value; }
        }

        [DisplayName("Колонки")]
        [Description("Возвращает коллекцию всех заголовков колонок, которые отображаются в элементе управления.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyCollectionConverter))]
        [Editor(typeof(MyColumnHeaderCollectionEditor), typeof(UITypeEditor))]
        public new ColumnHeaderCollection Columns
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

        [DisplayName("МножественныйВыбор")]
        [Description("Возвращает или задает значение, указывающее, могут ли выбираться несколько элементов.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool MultiSelect
        {
            get { return base.MultiSelect; }
            set { base.MultiSelect = value; }
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

        [DisplayName("МышьПокинулаЭлемент")]
        [Description("Возвращает или задает код для выполнения, когда указатель мыши покидает пределы элемента управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseLeave_osf
        {
            get { return _MouseLeave_osf; }
            set { _MouseLeave_osf = value; }
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

        [DisplayName("ОсновнойЦвет")]
        [Description("Возвращает или задает цвет переднего плана элемента управления.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        [DisplayName("Отображать")]
        [Description("Возвращает или задает значение, указывающее, отображаются ли элемент управления и все его дочерние элементы управления. Истина, если элемент управления отображается; в противном случае - Ложь.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public bool Visible_osf { get; set; }
				
        //скроем унаследованное свойство, для того чтобы оно не мешало нашему замещающему свойству использовать свой эдитор и конвертер.
        [Browsable(false)]
        public new bool Visible { get; set; }

        [DisplayName("ПередРедактированиемНадписи")]
        [Description("Возвращает или задает код, выполняемый при начале изменения пользователем надписи элемента.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string BeforeLabelEdit_osf
        {
            get { return _BeforeLabelEdit_osf; }
            set { _BeforeLabelEdit_osf = value; }
        }

        [DisplayName("ПереносНадписи")]
        [Description("Возвращает или задает значение, указывающее, будут ли метки элементов переноситься на другую строку, когда они отображаются в элементе управления в виде значков.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool LabelWrap
        {
            get { return base.LabelWrap; }
            set { base.LabelWrap = value; }
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
				
        //скроем унаследованное свойство, для того чтобы оно не мешало нашему замещающему свойству использовать свой эдитор и конвертер.
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

        [DisplayName("ПорядокОбхода")]
        [Description("Возвращает или задает последовательность перехода по клавише TAB между элементами управления внутри контейнера.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new int TabIndex
        {
            get { return base.TabIndex; }
            set { base.TabIndex = value; }
        }

        [DisplayName("ПослеРедактированияНадписи")]
        [Description("Возвращает или задает код для выполнения, если надпись элемента была изменена пользователем.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string AfterLabelEdit_osf
        {
            get { return _AfterLabelEdit_osf; }
            set { _AfterLabelEdit_osf = value; }
        }

        [DisplayName("ПриАктивизацииЭлемента")]
        [Description("Возвращает или задает код для выполнения, когда элемент активируется.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string ItemActivate_osf
        {
            get { return _ItemActivate_osf; }
            set { _ItemActivate_osf = value; }
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

        [DisplayName("ПриЗадержкеМыши")]
        [Description("Возвращает или задает код для выполнения когда указатель мыши задерживается на элементе управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseHover_osf
        {
            get { return _MouseHover_osf; }
            set { _MouseHover_osf = value; }
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

        [DisplayName("ПриОтпусканииМыши")]
        [Description("Возвращает или задает код для выполнения при отпускании кнопки мыши, когда указатель мыши находится на элементе управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseUp_osf
        {
            get { return _MouseUp_osf; }
            set { _MouseUp_osf = value; }
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

        [DisplayName("Прокручиваемый")]
        [Description("Возвращает или задает значение, указывающее, добавляется ли к этому элементу управления полоса прокрутки, если для отображения всех составляющих элементов не хватает места.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool Scrollable
        {
            get { return base.Scrollable; }
            set { base.Scrollable = value; }
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

        [DisplayName("РазрешитьПеретаскиваниеКолонок")]
        [Description("Возвращает или задает значение, указывающее, может ли пользователь перетаскивать заголовки колонок для изменения порядка колонок.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool AllowColumnReorder
        {
            get { return base.AllowColumnReorder; }
            set { base.AllowColumnReorder = value; }
        }

        [DisplayName("РазрешитьСортировку")]
        [Description("Возвращает или задает значение, указывающее, можно ли выполнить повторную сортировку списка, щелкнув заголовок колонки.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public  bool AllowSorting_osf
        {
            get { return _AllowSorting_osf; }
            set { _AllowSorting_osf = value; }
        }

        [DisplayName("РедактированиеНадписи")]
        [Description("Возвращает или задает значение, указывающее, может ли пользователь редактировать надписи элементов.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool LabelEdit
        {
            get { return base.LabelEdit; }
            set { base.LabelEdit = value; }
        }

        [DisplayName("РежимОтображения")]
        [Description("Возвращает или задает способ отображения элементов в элементе управления.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public View View_osf
        {
            get { return _View; }
            set
            {
                base.View = (System.Windows.Forms.View)value;
                _View = value;
            }
        }

        [DisplayName("Сетка")]
        [Description("Возвращает или задает значение, показывающее, отображаются ли линии сетки между строками и колонками, содержащими элементы и подэлементы.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool GridLines
        {
            get { return base.GridLines; }
            set { base.GridLines = value; }
        }

        [DisplayName("СкрытьВыделение")]
        [Description("Возвращает или задает значение, указывающее, остается ли выделенным выбранный элемент, когда элемент управления теряет фокус.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool HideSelection
        {
            get { return base.HideSelection; }
            set { base.HideSelection = value; }
        }

        [DisplayName("Сортировка")]
        [Description("Возвращает или задает порядок сортировки составляющих элементов в данном элементе управления.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new SortOrder Sorting
        {
            get { return (SortOrder)base.Sorting; }
            set { base.Sorting = (System.Windows.Forms.SortOrder)value; }
        }

        [DisplayName("СписокБольшихИзображений")]
        [Description("Возвращает или задает СписокИзображений (ImageList), используемый при отображении элементов как большие значки.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyImageListConverter))]
        public new System.Windows.Forms.ImageList LargeImageList
        {
            get { return base.LargeImageList; }
            set { base.LargeImageList = value; }
        }

        [DisplayName("СписокМаленькихИзображений")]
        [Description("Возвращает или задает СписокИзображений (ImageList), используемый при отображении элементов как маленькие значки.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyImageListConverter))]
        public new System.Windows.Forms.ImageList SmallImageList
        {
            get { return base.SmallImageList; }
            set { base.SmallImageList = value; }
        }

        [DisplayName("СтильГраницы")]
        [Description("Возвращает или задает стиль рамки в элементе управления СписокЭлементов (ListView).")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new BorderStyle BorderStyle
        {
            get { return (BorderStyle)base.BorderStyle; }
            set { base.BorderStyle = (System.Windows.Forms.BorderStyle)value; }
        }

        [DisplayName("СтильЗаголовка")]
        [Description("Возвращает или задает стиль заголовка колонки.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new ColumnHeaderStyle HeaderStyle
        {
            get { return (ColumnHeaderStyle)base.HeaderStyle; }
            set { base.HeaderStyle = (System.Windows.Forms.ColumnHeaderStyle)value; }
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

        [DisplayName("Флажки")]
        [Description("Возвращает или задает значение, указывающее, будет ли отображаться флажок рядом с каждым элементом.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool CheckBoxes
        {
            get { return base.CheckBoxes; }
            set { base.CheckBoxes = value; }
        }

        [DisplayName("ФоновоеИзображение")]
        [Description("Возвращает или задает фоновое изображение, отображаемое в элементе управления.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyImageConverter))]
        [Editor(typeof(MyImageFileNameEditor), typeof(UITypeEditor))]
        [DefaultValue(null)]
        public new Bitmap BackgroundImage
        {
            get { return (System.Drawing.Bitmap)base.BackgroundImage; }
            set { base.BackgroundImage = value; }
        }

        [DisplayName("ЦветФона")]
        [Description("Возвращает или задает цвет фона для элемента управления.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

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

        [DisplayName("ЭлементДобавлен")]
        [Description("Возвращает или задает код для выполнения при добавлении нового элемента управления в ЭлементыУправления (ControlCollection).")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string ControlAdded_osf
        {
            get { return _ControlAdded_osf; }
            set { _ControlAdded_osf = value; }
        }

        [DisplayName("ЭлементПомечен")]
        [Description("Возвращает или задает код для выполнения, когда состояние флажка изменяется.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string ItemCheck_osf
        {
            get { return _ItemCheck_osf; }
            set { _ItemCheck_osf = value; }
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

        [DisplayName("Элементы")]
        [Description("Возвращает коллекцию, содержащую все элементы элемента управления.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyCollectionConverter))]
        [Editor(typeof(MyListViewItemCollectionEditor), typeof(UITypeEditor))]
        public new ListViewItemCollection Items
        {
            get { return base.Items; }
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
        public new dynamic AllowDrop { get; set; }
        
        [Browsable(false)]
        public new dynamic AutoScrollOffset { get; set; }
        
        [Browsable(false)]
        public new dynamic AutoSize { get; set; }
        
        [Browsable(false)]
        public new dynamic BackgroundImageLayout { get; set; }
        
        [Browsable(false)]
        public new dynamic BackgroundImageTiled { get; set; }
        
        [Browsable(false)]
        public new dynamic BindingContext { get; set; }
        
        [Browsable(false)]
        public new dynamic CanSelect { get; set; }
        
        [Browsable(false)]
        public new dynamic CausesValidation { get; set; }
        
        [Browsable(false)]
        public new dynamic CheckedIndices { get; set; }
        
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
        public new dynamic DataBindings { get; set; }
        
        [Browsable(false)]
        public dynamic DeviceDpi { get; set; }
        
        [Browsable(false)]
        public new dynamic DisplayRectangle { get; set; }
        
        [Browsable(false)]
        public new dynamic Disposing { get; set; }
        
        [Browsable(false)]
        public new dynamic Groups { get; set; }
        
        [Browsable(false)]
        public new dynamic Handle { get; set; }
        
        [Browsable(false)]
        public new dynamic HasChildren { get; set; }
        
        [Browsable(false)]
        public new dynamic HotTracking { get; set; }
        
        [Browsable(false)]
        public new dynamic ImeMode { get; set; }
        
        [Browsable(false)]
        public new dynamic InsertionMark { get; set; }
        
        [Browsable(false)]
        public new dynamic InvokeRequired { get; set; }
        
        [Browsable(false)]
        public new dynamic IsAccessible { get; set; }
        
        [Browsable(false)]
        public new dynamic IsDisposed { get; set; }
        
        [Browsable(false)]
        public new dynamic IsHandleCreated { get; set; }
        
        [Browsable(false)]
        public new dynamic IsMirrored { get; set; }
        
        [Browsable(false)]
        public new dynamic LayoutEngine { get; set; }
        
        [Browsable(false)]
        public new dynamic ListViewItemSorter { get; set; }
        
        [Browsable(false)]
        public new dynamic Margin { get; set; }
        
        [Browsable(false)]
        public new dynamic MaximumSize { get; set; }
        
        [Browsable(false)]
        public new dynamic MinimumSize { get; set; }
        
        [Browsable(false)]
        public new dynamic OwnerDraw { get; set; }
        
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
        public new dynamic RightToLeftLayout { get; set; }
        
        [Browsable(false)]
        public new dynamic SelectedIndices { get; set; }
        
        [Browsable(false)]
        public new dynamic ShowGroups { get; set; }
        
        [Browsable(false)]
        public new dynamic ShowItemToolTips { get; set; }
        
        [Browsable(false)]
        public new dynamic Site { get; set; }
        
        [Browsable(false)]
        public new dynamic StateImageList { get; set; }
        
        [Browsable(false)]
        public new dynamic Tag { get; set; }
        
        [Browsable(false)]
        public new dynamic TileSize { get; set; }
        
        [Browsable(false)]
        public new dynamic TopItem { get; set; }
        
        [Browsable(false)]
        public new dynamic UseCompatibleStateImageBehavior { get; set; }
        
        [Browsable(false)]
        public new dynamic VirtualListSize { get; set; }
        
        [Browsable(false)]
        public new dynamic VirtualMode { get; set; }
        
        [Browsable(false)]
        public new dynamic WindowTarget { get; set; }

        [Browsable(false)]
        public new dynamic View { get; set; }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (DesignMode)
            {
                IDesignerHost designerHost = pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost();
                if (designerHost != null)
                {
                    ControlDesigner designer = (ControlDesigner)designerHost.GetDesigner(this);
                    if (designer != null)
                    {
                        if (tic1 < 1)
                        {
                            designer.ActionLists.Clear();
                            designer.ActionLists.Add(new ListViewActionList(designer));
                            tic1 = tic1 + 1;
                        }
                    }
                }
            }
        }

        public class ListViewActionList : DesignerActionList
        {
            private ListView _control;
            private DesignerActionUIService designerActionUISvc = null;

            public ListViewActionList(ControlDesigner designer) : base(designer.Component)
            {
                _control = (ListView)designer.Component;
                this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
            }

            private PropertyDescriptor GetPropertyByName(String propName)
            {
                return TypeDescriptor.GetProperties(_control)[propName];
            }

            public View View_osf
            {
                get { return _control.View_osf; }
                set
                {
                    this.GetPropertyByName("View_osf").SetValue(_control, value);
                    this.designerActionUISvc.Refresh(this.Component);
                }
            }

            [TypeConverter(typeof(MyImageListConverter))]
            public System.Windows.Forms.ImageList SmallImageList
            {
                get { return _control.SmallImageList; }
                set
                {
                    this.GetPropertyByName("SmallImageList").SetValue(_control, value);
                    this.designerActionUISvc.Refresh(this.Component);
                }
            }

            [TypeConverter(typeof(MyImageListConverter))]
            public System.Windows.Forms.ImageList LargeImageList
            {
                get { return _control.LargeImageList; }
                set
                {
                    this.GetPropertyByName("LargeImageList").SetValue(_control, value);
                    this.designerActionUISvc.Refresh(this.Component);
                }
            }

            private void EditItems()
            {
                PropertyDescriptor pd = TypeDescriptor.GetProperties(_control)["Items"];
                UITypeEditor editor = (UITypeEditor)pd.GetEditor(typeof(UITypeEditor));
                MyRuntimeServiceProvider serviceProvider = new MyRuntimeServiceProvider(_control);
                object res1 = editor.EditValue(serviceProvider, serviceProvider, _control.Items);
                this.GetPropertyByName("Items").SetValue(_control, res1);
                this.designerActionUISvc.Refresh(this.Component);
            }

            private void EditColumns()
            {
                PropertyDescriptor pd = TypeDescriptor.GetProperties(_control)["Columns"];
                UITypeEditor editor = (UITypeEditor)pd.GetEditor(typeof(UITypeEditor));
                MyRuntimeServiceProvider serviceProvider = new MyRuntimeServiceProvider(_control);
                object res1 = editor.EditValue(serviceProvider, serviceProvider, _control.Columns);
                this.GetPropertyByName("Columns").SetValue(_control, res1);
                this.designerActionUISvc.Refresh(this.Component);
            }

            public override DesignerActionItemCollection GetSortedActionItems()
            {
                var items = new DesignerActionItemCollection();
                items.Add(new DesignerActionMethodItem(this, "EditItems", "Изменить элементы...", "", true));
                items.Add(new DesignerActionMethodItem(this, "EditColumns", "Изменить колонки...", "", true));

                items.Add(new DesignerActionPropertyItem("View_osf", "Режим отображения:"));
                items.Add(new DesignerActionPropertyItem("SmallImageList", "Список маленьких изображений:"));
                items.Add(new DesignerActionPropertyItem("LargeImageList", "Список больших изображений:"));

                return items;
            }
        }

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
Элементы ==
";
            }
        }
    }
}
