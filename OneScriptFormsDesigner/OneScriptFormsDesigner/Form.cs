using System.ComponentModel;
using System.Collections;
using System.Drawing.Design;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class Form : System.Windows.Forms.Form
    {
        private ArrayList _ArrayListComponentsAddingOrder = new ArrayList();
        private string _DoubleClick_osf;
        private string _Closed_osf;
        private MyIcon _icon;
        private string _KeyUp_osf;
        private string _KeyDown_osf;
        private string _KeyPress_osf;
        private string _MouseEnter_osf;
        private string _MouseLeave_osf;
        private string _Click_osf;
        private string _LocationChanged_osf;
        private string _Activated_osf;
        private string _Enter_osf;
        private string _Deactivate_osf;
        private string _Load_osf;
        private string _MouseHover_osf;
        private string _FormClosing_osf;
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

        [Browsable(false)]
        public ArrayList ArrayListComponentsAddingOrder
        {
            get { return _ArrayListComponentsAddingOrder; }
            set { _ArrayListComponentsAddingOrder = value; }
        }
        
        public Form()
        {
            _ArrayListComponentsAddingOrder.Add(this);
            Enabled_osf = base.Enabled;
            Visible_osf = base.Visible;
        }

        [DisplayName("АвтоПрокрутка")]
        [Description("Возвращает или задает значение, указывающее, позволит ли контейнер пользователю прокручивать любые элементы управления, размещенные за пределами его видимых границ.")]
        [Category("Макет")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool AutoScroll
        {
            get { return base.AutoScroll; }
            set { base.AutoScroll = value; }
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

        [DisplayName("Закрыта")]
        [Description("Возвращает или задает код для выполнения, когда форма уже закрылась.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Closed_osf
        {
            get { return _Closed_osf; }
            set { _Closed_osf = value; }
        }

        [DisplayName("Значок")]
        [Description("Возвращает или задает значок формы.")]
        [Category("Стиль окна")]
        [Browsable(true)]
        [TypeConverter(typeof(MyIconConverter))]
        [Editor(typeof(MyIconEditor), typeof(UITypeEditor))]
        [DefaultValue(null)]
        public new MyIcon Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                try
                {
                    base.Icon = _icon.M_Icon;
                }
                catch
                {
                    base.Icon = null;
                }
            }
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

        [DisplayName("КлавишаПредпросмотр")]
        [Description("Возвращает или задает значение, указывающее, получит ли форма события клавиш перед передачей событий элементу управления, на который установлен фокус.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool KeyPreview
        {
            get { return base.KeyPreview; }
            set { base.KeyPreview = value; }
        }

        [DisplayName("КнопкаМаксимизации")]
        [Description("Возвращает или задает значение, указывающее, будет ли кнопка максимизации отображаться в строке заголовка формы.")]
        [Category("Стиль окна")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool MaximizeBox
        {
            get { return base.MaximizeBox; }
            set { base.MaximizeBox = value; }
        }

        [DisplayName("КнопкаМинимизации")]
        [Description("Возвращает или задает значение, указывающее, отображается ли кнопка минимизации в строке заголовка формы.")]
        [Category("Стиль окна")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool MinimizeBox
        {
            get { return base.MinimizeBox; }
            set { base.MinimizeBox = value; }
        }

        [DisplayName("КнопкаОтмена")]
        [Description("Возвращает или задает кнопку, которая срабатывает при нажатии клавиши ESC.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyButtonControlConverter))]
        public System.Windows.Forms.IButtonControl CancelButton_osf { get; set; }

        [DisplayName("КнопкаПринять")]
        [Description("Возвращает или задает кнопку в форме, которая срабатывает при нажатии клавиши ВВОД.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyButtonControlConverter))]
        public System.Windows.Forms.IButtonControl AcceptButton_osf { get; set; }

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

        [DisplayName("МаксимальныйРазмер")]
        [Description("Возвращает или задает максимальный размер, до которого можно изменить размер формы.")]
        [Category("Макет")]
        [Browsable(true)]
        [TypeConverter(typeof(MySizeConverter))]
        [Editor(typeof(MyMaximumSizeEditor), typeof(UITypeEditor))]
        public Size MaximumSize_osf
        {
            get { return base.MaximumSize; }
            set { base.MaximumSize = value; }
        }

        [DisplayName("Меню")]
        [Description("Возвращает или задает объект ГлавноеМеню (MainMenu), представляющий собой главное меню формы.")]
        [Category("Стиль окна")]
        [Browsable(true)]
        [TypeConverter(typeof(MyFormMenuConverter))]
        public System.Windows.Forms.MainMenu Menu_osf
        {
            get { return base.Menu; }
            set { base.Menu = value; }
        }

        [DisplayName("МинимальныйРазмер")]
        [Description("Возвращает или задает минимальный размер, до которого может быть уменьшена форма.")]
        [Category("Макет")]
        [Browsable(true)]
        [TypeConverter(typeof(MySizeConverter))]
        [Editor(typeof(MyMinimumSizeEditor), typeof(UITypeEditor))]
        public Size MinimumSize_osf
        {
            get { return base.MinimumSize; }
            set { base.MinimumSize = value; }
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

        [DisplayName("НачальноеПоложение")]
        [Description("Возвращает или задает начальное положение формы при запуске.")]
        [Category("Макет")]
        [Browsable(true)]
        public new FormStartPosition StartPosition
        {
            get { return (FormStartPosition)base.StartPosition; }
            set { base.StartPosition = (System.Windows.Forms.FormStartPosition)value; }
        }

        [DisplayName("ОконноеМеню")]
        [Description("Возвращает или задает значение, показывающее, отображается ли блок управления в строке заголовка формы.")]
        [Category("Стиль окна")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool ControlBox
        {
            get { return base.ControlBox; }
            set { base.ControlBox = value; }
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

        [DisplayName("ПоверхВсех")]
        [Description("Возвращает или задает значение, указывающее, отображать ли форму как форму переднего плана.")]
        [Category("Стиль окна")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool TopMost
        {
            get { return base.TopMost; }
            set { base.TopMost = value; }
        }

        [DisplayName("ПоказатьВПанели")]
        [Description("Возвращает или задает значение, указывающее, отображается ли форма на панели задач Windows.")]
        [Category("Стиль окна")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool ShowInTaskbar
        {
            get { return base.ShowInTaskbar; }
            set { base.ShowInTaskbar = value; }
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

        [DisplayName("ПриАктивизации")]
        [Description("Возвращает или задает код для выполнения, при активизации формы, определенной в коде или заданной пользователем.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Activated_osf
        {
            get { return _Activated_osf; }
            set { _Activated_osf = value; }
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

        [DisplayName("ПриДеактивации")]
        [Description("Возвращает или задает код для выполнения, когда форма теряет фокус и не является активной формой.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Deactivate_osf
        {
            get { return _Deactivate_osf; }
            set { _Deactivate_osf = value; }
        }

        [DisplayName("ПриЗагрузке")]
        [Description("Возвращает или задает код для выполнения  до первоначального отображения формы.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Load_osf
        {
            get { return _Load_osf; }
            set { _Load_osf = value; }
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

        [DisplayName("ПриЗакрытии")]
        [Description("Возвращает или задает код для выполнения при закрытии формы.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string FormClosing_osf
        {
            get { return _FormClosing_osf; }
            set { _FormClosing_osf = value; }
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

        [DisplayName("ПрозрачныйЦвет")]
        [Description("Возвращает или задает цвет, представляющий прозрачные области формы.")]
        [Category("Стиль окна")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color TransparencyKey
        {
            get { return base.TransparencyKey; }
            set { base.TransparencyKey = value; }
        }

        [DisplayName("Размер")]
        [Description("Возвращает или задает высоту и ширину элемента управления.")]
        [Category("Макет")]
        [Browsable(true)]
        [TypeConverter(typeof(MySizeConverter))]
        [Editor(typeof(MySizeEditor), typeof(UITypeEditor))]
        public Size Size_osf
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

        [DisplayName("РазмерПоляАвтоПрокрутки")]
        [Description("Возвращает или задает размер поля автоматической прокрутки.")]
        [Category("Макет")]
        [Browsable(true)]
        [TypeConverter(typeof(MySizeConverter))]
        [Editor(typeof(MySizeEditor), typeof(UITypeEditor))]
        public new Size AutoScrollMargin
        {
            get { return base.AutoScrollMargin; }
            set { base.AutoScrollMargin = value; }
        }

        [DisplayName("СтильГраницыФормы")]
        [Description("Возвращает или задает стиль рамки формы.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new FormBorderStyle FormBorderStyle
        {
            get { return (FormBorderStyle)base.FormBorderStyle; }
            set { base.FormBorderStyle = (System.Windows.Forms.FormBorderStyle)value; }
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
        public Color BackColor_osf
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
        [ReadOnly(true)]
        public new string AccessibleDescription { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new string AccessibleName { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.AccessibleRole AccessibleRole { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool AllowDrop { get; set; }

        [DisplayName("РежимАвтоМасштабирования")]
        [Description("Возвращает или задает режим автоматического масштабирования элемента управления.")]
        [Category("Макет")]
        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.AutoScaleMode AutoScaleMode { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Drawing.Size AutoScrollMinSize { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool AutoSize { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.AutoSizeMode AutoSizeMode { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.AutoValidate AutoValidate { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.ImageLayout BackgroundImageLayout { get; set; }
		
        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Drawing.Color BackColor { get; set; }
		
        [Browsable(false)]
        [ReadOnly(true)]
        public new bool CausesValidation { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.ContextMenuStrip ContextMenuStrip { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.ControlBindingsCollection DataBindings { get; set; }

        [DisplayName("ДвойнаяБуферизация")]
        [Description("Возвращает или задает значение, указывающее, должна ли поверхность этого элемента управления перерисовываться с помощью дополнительного буфера, чтобы уменьшить или предотвратить мерцание.")]
        [Category("Поведение")]
        [Browsable(false)]
        [ReadOnly(true)]
        public new bool DoubleBuffered { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool HelpButton { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.ImeMode ImeMode { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool IsMdiContainer { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.MenuStrip MainMenuStrip { get; set; }
		
        [Browsable(false)]
        public new Size MaximumSize
        {
            get { return base.MaximumSize; }
            set { base.MaximumSize = value; }
        }

        [Browsable(false)]
        public new Size MinimumSize
        {
            get { return base.MinimumSize; }
            set { base.MinimumSize = value; }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public new Double Opacity { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.Padding Padding { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.RightToLeft RightToLeft { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool RightToLeftLayout { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool ShowIcon { get; set; }
		
        [Browsable(false)]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.SizeGripStyle SizeGripStyle { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new object Tag { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.FormWindowState WindowState { get; set; }
		
        [Browsable(false)]
        [ReadOnly(true)]
        public new dynamic CancelButton { get; set; }
		
        [Browsable(false)]
        [ReadOnly(true)]
        public new dynamic AcceptButton { get; set; }

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
