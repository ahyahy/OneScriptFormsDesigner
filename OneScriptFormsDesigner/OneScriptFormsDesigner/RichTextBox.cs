using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    [Docking(DockingBehavior.Never)]
    public class RichTextBox : System.Windows.Forms.RichTextBox
    {

        private string _SelectionChanged_osf;
        private string _DoubleClick_osf;
        private string _KeyUp_osf;
        private string _KeyDown_osf;
        private string _KeyPress_osf;
        private string _MouseEnter_osf;
        private string _MouseLeave_osf;
        private string _Click_osf;
        private string _LocationChanged_osf;
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
        private string _LinkClicked_osf;
        private string _TextChanged_osf;
        private string _ControlAdded_osf;
        private string _ControlRemoved_osf;

        public RichTextBox()
        {
            Enabled_osf = base.Enabled;
            Visible_osf = base.Visible;
        }

        [DisplayName("АвтоВыборСлов")]
        [Description("Возвращает или задает значение, указывающее, включен ли автоматический выбор слов.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool AutoWordSelection
        {
            get { return base.AutoWordSelection; }
            set { base.AutoWordSelection = value; }
        }

        [DisplayName("ВыделениеИзменено")]
        [Description("Возвращает или задает код для выполнения, при изменении выделения текста в элементе управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string SelectionChanged_osf
        {
            get { return _SelectionChanged_osf; }
            set { _SelectionChanged_osf = value; }
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

        [DisplayName("МаксимальнаяДлина")]
        [Description("Возвращает или задает максимальное число символов, которое разрешается вводить или вставлять в элемент управления ПолеВвода (TextBox).")]
        [Category("Поведение")]
        [Browsable(true)]
        public new int MaxLength
        {
            get { return base.MaxLength; }
            set { base.MaxLength = value; }
        }

        [DisplayName("Масштаб")]
        [Description("Возвращает или задает текущий масштаб ФорматированноеПолеВвода (RichTextBox).")]
        [Category("Поведение")]
        [Browsable(true)]
        public new decimal ZoomFactor
        {
            get { return Convert.ToDecimal(base.ZoomFactor); }
            set { base.ZoomFactor = Convert.ToSingle(value); }
        }

        [DisplayName("МногострочныйРежим")]
        [Description("Получает или задает значение, указывающее, является ли это текстовое поле многострочным.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool Multiline
        {
            get { return base.Multiline; }
            set { base.Multiline = value; }
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

        [DisplayName("ОбнаруживатьАдреса")]
        [Description("Возвращает или задает значение, указывающее форматировать ли автоматически в ФорматированноеПолеВвода (RichTextBox) унифицированный указатель ресурса (URL) при вводе его в элемент управления.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool DetectUrls
        {
            get { return base.DetectUrls; }
            set { base.DetectUrls = value; }
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

        [DisplayName("ОтступМаркера")]
        [Description("Возвращает или задает отступ, используемый в элементе управления ФорматированноеПолеВвода (RichTextBox) при применении маркированного стиля к тексту.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new int BulletIndent
        {
            get { return base.BulletIndent; }
            set { base.BulletIndent = value; }
        }

        [DisplayName("Перенос")]
        [Description("Указывает, будет ли элемент управления многострочного текстового поля автоматически переносить слова в начало следующей строки при необходимости.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool WordWrap
        {
            get { return base.WordWrap; }
            set { base.WordWrap = value; }
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

        [DisplayName("ПолосыПрокрутки")]
        [Description("Возвращает или задает тип полос прокрутки для отображения в элементе управления ФорматированноеПолеВвода (RichTextBox).")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new ScrollBars ScrollBars
        {
            get { return (ScrollBars)base.ScrollBars; }
            set { base.ScrollBars = (System.Windows.Forms.RichTextBoxScrollBars)value; }
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

        [DisplayName("ПравоеОграничение")]
        [Description("Возвращает или задает размер одной строки текста в элементе управления ФорматированноеПолеВвода (RichTextBox). По умолчанию используется значение ноль.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new int RightMargin
        {
            get { return base.RightMargin; }
            set { base.RightMargin = value; }
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

        [DisplayName("ПринятиеТаб")]
        [Description("Возвращает или задает значение, указывающее, что происходит при нажатии клавиши TAB в многострочном элементе управления: вводится знак табуляции  в текстовом поле или фокус ввода в форме перемещается к следующему элементу управления в последовательности переходов.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool AcceptsTab
        {
            get { return base.AcceptsTab; }
            set { base.AcceptsTab = value; }
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

        [DisplayName("СкрытьВыделение")]
        [Description("Возвращает или задает значение, указывающее, остается ли выделенный текст в ПолеВвода (TextBox) выделенным, когда фокус ввода в  форме переходит с данного элемента управления на другой.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool HideSelection
        {
            get { return base.HideSelection; }
            set { base.HideSelection = value; }
        }

        [DisplayName("СсылкаНажата")]
        [Description("Возвращает или задает код для выполнения, при щелчке пользователем ссылки, расположенной в тексте элемента управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string LinkClicked_osf
        {
            get { return _LinkClicked_osf; }
            set { _LinkClicked_osf = value; }
        }

        [DisplayName("СтильГраницы")]
        [Description("Возвращает или задает тип границы элемента управления ПолеВвода (TextBox).")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new BorderStyle BorderStyle
        {
            get { return (BorderStyle)base.BorderStyle; }
            set { base.BorderStyle = (System.Windows.Forms.BorderStyle)value; }
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

        [DisplayName("Текст")]
        [Description("Возвращает или задает текст, связанный с этим элементом управления.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
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

        [DisplayName("ТолькоЧтение")]
        [Description("Возвращает или задает значение, указывающее, является ли текст в текстовом поле доступным только для чтения.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool ReadOnly
        {
            get { return base.ReadOnly; }
            set { base.ReadOnly = value; }
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
        public new dynamic BackgroundImageLayout { get; set; }
        
        [Browsable(false)]
        public new dynamic BindingContext { get; set; }
        
        [Browsable(false)]
        public new dynamic CanSelect { get; set; }
        
        [Browsable(false)]
        public new dynamic CausesValidation { get; set; }
        
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
        public new dynamic EnableAutoDragDrop { get; set; }
        
        [Browsable(false)]
        public new dynamic Handle { get; set; }
        
        [Browsable(false)]
        public new dynamic HasChildren { get; set; }
        
        [Browsable(false)]
        public new dynamic ImeMode { get; set; }
        
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
        public new dynamic LanguageOption { get; set; }
        
        [Browsable(false)]
        public new dynamic LayoutEngine { get; set; }
        
        [Browsable(false)]
        public new dynamic Lines { get; set; }
        
        [Browsable(false)]
        public new dynamic Margin { get; set; }
        
        [Browsable(false)]
        public new dynamic MaximumSize { get; set; }
        
        [Browsable(false)]
        public new dynamic MinimumSize { get; set; }
        
        [Browsable(false)]
        public new dynamic Padding { get; set; }
        
        [Browsable(false)]
        public new dynamic PreferredSize { get; set; }
        
        [Browsable(false)]
        public new dynamic RecreatingHandle { get; set; }
        
        [Browsable(false)]
        public new dynamic RedoActionName { get; set; }
        
        [Browsable(false)]
        public new dynamic Region { get; set; }
        
        [Browsable(false)]
        public new dynamic RichTextShortcutsEnabled { get; set; }
        
        [Browsable(false)]
        public new dynamic RightToLeft { get; set; }
        
        [Browsable(false)]
        public new dynamic SelectedRtf { get; set; }
        
        [Browsable(false)]
        public new dynamic SelectionAlignment { get; set; }
        
        [Browsable(false)]
        public new dynamic SelectionBackColor { get; set; }
        
        [Browsable(false)]
        public new dynamic SelectionBullet { get; set; }
        
        [Browsable(false)]
        public new dynamic SelectionCharOffset { get; set; }
        
        [Browsable(false)]
        public new dynamic SelectionHangingIndent { get; set; }
        
        [Browsable(false)]
        public new dynamic SelectionProtected { get; set; }
        
        [Browsable(false)]
        public new dynamic SelectionRightIndent { get; set; }
        
        [Browsable(false)]
        public new dynamic SelectionTabs { get; set; }
        
        [Browsable(false)]
        public new dynamic SelectionType { get; set; }
        
        [Browsable(false)]
        public new dynamic ShortcutsEnabled { get; set; }
        
        [Browsable(false)]
        public new dynamic ShowSelectionMargin { get; set; }
        
        [Browsable(false)]
        public new dynamic Site { get; set; }
        
        [Browsable(false)]
        public new dynamic Tag { get; set; }
        
        [Browsable(false)]
        public new dynamic UndoActionName { get; set; }
        
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
";
            }
        }
    }
}
