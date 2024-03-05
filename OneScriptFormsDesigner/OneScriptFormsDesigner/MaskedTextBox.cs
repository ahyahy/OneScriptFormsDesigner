using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class MaskedTextBox : System.Windows.Forms.MaskedTextBox
    {
        private int tic = 0; // Счетчик для правильной работы смарт-тэгов.
        private string _MaskInputRejected_osf;
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
        private string _TextChanged_osf;
        private string _ControlAdded_osf;
        private string _ControlRemoved_osf;

        public MaskedTextBox()
        {
            Enabled_osf = base.Enabled;
            Visible_osf = base.Visible;
        }

        [DisplayName("ВводОтклонен")]
        [Description("Возвращает или задает код для выполнения в том случае, если назначенный или введенный пользователем знак не совпадает с соответствующим элементом формата маски ввода.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MaskInputRejected_osf
        {
            get { return _MaskInputRejected_osf; }
            set { _MaskInputRejected_osf = value; }
        }

        [DisplayName("ВыравниваниеТекста")]
        [Description("Возвращает или задает значение, определяющее способ выравнивания текста.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new HorizontalAlignment TextAlign
        {
            get { return (HorizontalAlignment)base.TextAlign; }
            set { base.TextAlign = (System.Windows.Forms.HorizontalAlignment)value; }
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

        [DisplayName("ИспользоватьСимволПриглашения")]
        [Description("Возвращает или задает значение, показывающее, может ли символ приглашения вводиться пользователем в качестве допустимых данных.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool AllowPromptAsInput
        {
            get { return base.AllowPromptAsInput; }
            set { base.AllowPromptAsInput = value; }
        }

        [DisplayName("ИспользоватьСистемныйСимволПароля")]
        [Description("Возвращает или задает значение, указывающее, должен ли использоваться символ пароля, предоставляемый операционной системой.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool UseSystemPasswordChar
        {
            get { return base.UseSystemPasswordChar; }
            set { base.UseSystemPasswordChar = value; }
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

        [DisplayName("КопироватьМаску")]
        [Description("Возвращает или задает значение, определяющее, копируются ли литералы и символы приглашения в буфер обмена.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new MaskFormat CutCopyMaskFormat
        {
            get { return (MaskFormat)base.CutCopyMaskFormat; }
            set { base.CutCopyMaskFormat = (System.Windows.Forms.MaskFormat)value; }
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

        [DisplayName("Маска")]
        [Description("Получает или задает маску ввода для использования во время выполнения.")]
        [Category("Поведение")]
        [Browsable(true)]
        [Editor(typeof(MyMaskPropertyEditor), typeof(UITypeEditor))]
        public new string Mask
        {
            get { return base.Mask; }
            set { base.Mask = value; }
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
				
        [Browsable(false)]
        public new bool Visible { get; set; }

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

        [DisplayName("ПропускЛитералов")]
        [Description("Возвращает или задает значение, указывающее, разрешено ли пользователю повторно вводить значения литералов.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool SkipLiterals
        {
            get { return base.SkipLiterals; }
            set { base.SkipLiterals = value; }
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

        [DisplayName("РежимВставки")]
        [Description("Возвращает или задает режим вставки текста.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new InsertKeyMode InsertKeyMode
        {
            get { return (InsertKeyMode)base.InsertKeyMode; }
            set { base.InsertKeyMode = (System.Windows.Forms.InsertKeyMode)value; }
        }

        [DisplayName("СбросНаПробел")]
        [Description("Возвращает или задает значение, определяющее способ обработки введенного знака пробела.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool ResetOnSpace
        {
            get { return base.ResetOnSpace; }
            set { base.ResetOnSpace = value; }
        }

        [DisplayName("СбросНаСимволПриглашения")]
        [Description("Возвращает или задает значение, определяющее способ обработки введенного символа, совпадающего с символом приглашения.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool ResetOnPrompt
        {
            get { return base.ResetOnPrompt; }
            set { base.ResetOnPrompt = value; }
        }

        [DisplayName("СигналОтклоненного")]
        [Description("Возвращает или задает значение, указывающее, подает ли элемент управления МаскаПоляВвода (MaskedTextBox) звуковой сигнал для каждого отклоненного нажатия клавиши.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool BeepOnError
        {
            get { return base.BeepOnError; }
            set { base.BeepOnError = value; }
        }

        [DisplayName("СимволПароля")]
        [Description("Возвращает или задает символ, отображаемый в качестве замены для данных, вводимых пользователем.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new Char PasswordChar
        {
            get { return base.PasswordChar; }
            set { base.PasswordChar = value; }
        }

        [DisplayName("СимволПриглашения")]
        [Description("Возвращает или задает символ, представляющий отсутствие данных, введенных пользователем в элемент управления МаскаПоляВвода (MaskedTextBox).")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new Char PromptChar
        {
            get { return base.PromptChar; }
            set { base.PromptChar = value; }
        }

        [DisplayName("СкрыватьМаскуПриУходе")]
        [Description("Возвращает или задает значение, указывающее, скрываются ли символы подсказки в маске ввода, когда фокус покидает элемент управления МаскаПоляВвода (MaskedTextBox).")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool HidePromptOnLeave
        {
            get { return base.HidePromptOnLeave; }
            set { base.HidePromptOnLeave = value; }
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
        [Description("Возвращает или задает текст, отображаемый в текущий момент.")]
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

        [DisplayName("ТолькоASCII")]
        [Description("Возвращает или задает значение, показывающее, принимает ли элемент управления МаскаПоляВвода (MaskedTextBox) символы, не принадлежащие кодировке ASCII.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool AsciiOnly
        {
            get { return base.AsciiOnly; }
            set { base.AsciiOnly = value; }
        }

        [DisplayName("ТолькоЧтение")]
        [Description("Возвращает  или задает значение, указывающее, является ли текст в текстовом поле доступным только для чтения.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool ReadOnly
        {
            get { return base.ReadOnly; }
            set { base.ReadOnly = value; }
        }

        [DisplayName("ФорматМаски")]
        [Description("Возвращает или задает значение, определяющее, включаются ли литералы и символы приглашения в форматированную строку.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new MaskFormat TextMaskFormat
        {
            get { return (MaskFormat)base.TextMaskFormat; }
            set { base.TextMaskFormat = (System.Windows.Forms.MaskFormat)value; }
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
        public new dynamic AcceptsTab { get; set; }
        
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
        public new dynamic BackgroundImage { get; set; }
        
        [Browsable(false)]
        public new dynamic BackgroundImageLayout { get; set; }
        
        [Browsable(false)]
        public new dynamic BindingContext { get; set; }
        
        [Browsable(false)]
        public new dynamic CanSelect { get; set; }
        
        [Browsable(false)]
        public new dynamic CanUndo { get; set; }
        
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
        public new dynamic Culture { get; set; }
        
        [Browsable(false)]
        public new dynamic DataBindings { get; set; }
        
        [Browsable(false)]
        public dynamic DeviceDpi { get; set; }
        
        [Browsable(false)]
        public new dynamic DisplayRectangle { get; set; }
        
        [Browsable(false)]
        public new dynamic Disposing { get; set; }
        
        [Browsable(false)]
        public new dynamic FormatProvider { get; set; }
        
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
        public new dynamic LayoutEngine { get; set; }
        
        [Browsable(false)]
        public new dynamic Lines { get; set; }
        
        [Browsable(false)]
        public new dynamic Margin { get; set; }
        
        [Browsable(false)]
        public new dynamic MaskedTextProvider { get; set; }
        
        [Browsable(false)]
        public new dynamic MaximumSize { get; set; }
        
        [Browsable(false)]
        public new dynamic MaxLength { get; set; }
        
        [Browsable(false)]
        public new dynamic MinimumSize { get; set; }
        
        [Browsable(false)]
        public new dynamic Multiline { get; set; }
        
        [Browsable(false)]
        public new dynamic Padding { get; set; }
        
        [Browsable(false)]
        public new dynamic PreferredSize { get; set; }
        
        [Browsable(false)]
        public new dynamic RecreatingHandle { get; set; }
        
        [Browsable(false)]
        public new dynamic Region { get; set; }
        
        [Browsable(false)]
        public new dynamic RejectInputOnFirstFailure { get; set; }
        
        [Browsable(false)]
        public new dynamic RightToLeft { get; set; }
        
        [Browsable(false)]
        public new dynamic ShortcutsEnabled { get; set; }
        
        [Browsable(false)]
        public new dynamic Site { get; set; }
        
        [Browsable(false)]
        public new dynamic Tag { get; set; }
        
        [Browsable(false)]
        public new dynamic ValidatingType { get; set; }
        
        [Browsable(false)]
        public new dynamic WindowTarget { get; set; }
        
        [Browsable(false)]
        public new dynamic WordWrap { get; set; }
        
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (DesignMode)
            {
                IDesignerHost designerHost = OneScriptFormsDesigner.DesignerHost;
                if (designerHost != null)
                {
                    ControlDesigner designer = (ControlDesigner)designerHost.GetDesigner(this);
                    if (designer != null)
                    {
                        if (tic < 1)
                        {
                            designer.ActionLists.Clear();
                            designer.ActionLists.Add(new MaskedTextBoxActionList(designer));
                            tic = tic + 1;
                        }
                    }
                }
            }
        }

        public class MaskedTextBoxActionList : DesignerActionList
        {
            private MaskedTextBox _control;
            private DesignerActionUIService designerActionUISvc = null;

            public MaskedTextBoxActionList(ControlDesigner designer) : base(designer.Component)
            {
                _control = (MaskedTextBox)designer.Component;
                designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
            }

            private PropertyDescriptor GetPropertyByName(String propName)
            {
                return TypeDescriptor.GetProperties(_control)[propName];
            }

            private void SetMask()
            {
                PropertyDescriptor pd = TypeDescriptor.GetProperties(_control)["Mask"];
                UITypeEditor editor = (UITypeEditor)pd.GetEditor(typeof(UITypeEditor));
                MyRuntimeServiceProvider serviceProvider = new MyRuntimeServiceProvider(_control);
                object fact = editor.EditValue(serviceProvider, serviceProvider, _control.Mask);
                GetPropertyByName("Mask").SetValue(_control, fact);
                designerActionUISvc.Refresh(Component);
            }

            public override DesignerActionItemCollection GetSortedActionItems()
            {
                var items = new DesignerActionItemCollection();
                items.Add(new DesignerActionMethodItem(this, "SetMask", "Установка маски...", "", true));
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
";
            }
        }
    }
}
