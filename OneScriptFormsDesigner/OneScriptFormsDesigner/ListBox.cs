using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing.Design; 
using System.Drawing;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class ListBox : System.Windows.Forms.ListBox
    {
        private int tic1 = 0; // счетчик для правильной работы смарт-тэгов
        private string _DoubleClick_osf;
        private string _SelectedIndexChanged_osf;
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
        private osfDesigner.DockStyle _Dock;
        private string _TextChanged_osf;
        private string _ControlAdded_osf;
        private string _ControlRemoved_osf;

        public void ListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
            {
                return;
            }
            e.DrawBackground();
            e.DrawFocusRectangle();
            dynamic item = base.Items[e.Index];
            System.Type type = item.GetType();
            System.Drawing.Color color1 = base.ForeColor;
            System.Reflection.PropertyInfo propertyForeColor = type.GetProperty("ForeColor");
            Color colorForeColor = System.Drawing.Color.Empty;
            if (propertyForeColor != null)
            {
                try
                {
                    colorForeColor = (Color)propertyForeColor.GetValue(Items[e.Index], (object[])null);
                }
                catch
                {
                    colorForeColor = (Color)propertyForeColor.GetValue(Items[e.Index], (object[])null);
                }
            }

            if ((e.State & System.Windows.Forms.DrawItemState.Disabled) == System.Windows.Forms.DrawItemState.Disabled)
            {
                try
                {
                    if (!colorForeColor.IsEmpty)
                    {
                        color1 = colorForeColor;
                    }
                }
                catch
                {
                    color1 = System.Drawing.SystemColors.GrayText;
                }
            }
            else if ((e.State & System.Windows.Forms.DrawItemState.Selected) == System.Windows.Forms.DrawItemState.Selected)
            {
                color1 = System.Drawing.SystemColors.HighlightText;
            }
            else
            {
                try
                {
                    if (!colorForeColor.IsEmpty)
                    {
                        color1 = colorForeColor;
                    }
                }
                catch { }
            }
            string s = item.ToString();
            e.Graphics.DrawString(s, base.Font, (System.Drawing.Brush)new System.Drawing.SolidBrush(color1), (float)e.Bounds.X, (float)e.Bounds.Y);
        }
        
        public ListBox()
        {
            base.DisplayMember = "Text";
            base.DrawItem += ListBox_DrawItem;
            Enabled_osf = base.Enabled;
            Visible_osf = base.Visible;
        }

        [DisplayName("ВысотаЭлемента")]
        [Description("Возвращает или задает высоту элемента в ПолеСписка (ListBox).")]
        [Category("Поведение")]
        [Browsable(true)]
        public new int ItemHeight
        {
            get { return base.ItemHeight; }
            set { base.ItemHeight = value; }
        }

        [DisplayName("ГоризонтальнаяМера")]
        [Description("Возвращает или задает ширину, на которую горизонтальная полоса прокрутки ПолеСписка (ListBox) может прокручиваться.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new int HorizontalExtent
        {
            get { return base.HorizontalExtent; }
            set { base.HorizontalExtent = value; }
        }

        [DisplayName("ГоризонтальнаяПрокрутка")]
        [Description("Возвращает или задает значение, указывающее, отображается ли горизонтальная полоса прокрутки в элементе управления.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool HorizontalScrollbar
        {
            get { return base.HorizontalScrollbar; }
            set { base.HorizontalScrollbar = value; }
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
        [Description("Возвращает или задает код для выполнения, при изменении свойства ИндексВыбранного (SelectedIndex).")]
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

        [DisplayName("ИспользоватьТабулятор")]
        [Description("Возвращает или задает значение, указывающее, может ли ПолеСписка (ListBox) распознавать знаки табуляции и превращать их в отступы при рисовании строк списка.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool UseTabStops
        {
            get { return base.UseTabStops; }
            set { base.UseTabStops = value; }
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

        [DisplayName("Многоколоночное")]
        [Description("Возвращает или задает значение, указывающее, поддерживает ли ПолеСписка (ListBox) несколько колонок.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool MultiColumn
        {
            get { return base.MultiColumn; }
            set { base.MultiColumn = value; }
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

        [DisplayName("Отсортирован")]
        [Description("Возвращает или задает значение, указывающее, сортированы ли элементы в ПолеСписка (ListBox) по алфавиту.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool Sorted
        {
            get { return base.Sorted; }
            set { base.Sorted = value; }
        }

        [DisplayName("ПодборВысоты")]
        [Description("Возвращает или задает значение, указывающее, должны ли изменяться размеры элемента управления, чтобы избежать частичного отображения элементов.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public bool IntegralHeight_osf
        {
            get { return base.IntegralHeight; }
            set { base.IntegralHeight = value; }
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

        [DisplayName("ПрокруткаВсегдаОтображается")]
        [Description("Возвращает или задает значение, указывающее, всегда ли отображается вертикальная полоса прокрутки.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool ScrollAlwaysVisible
        {
            get { return base.ScrollAlwaysVisible; }
            set { base.ScrollAlwaysVisible = value; }
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

        [DisplayName("РежимВыбора")]
        [Description("Возвращает или задает метод, которым выбираются элементы в ПолеСписка (ListBox).")]
        [Category("Поведение")]
        [Browsable(true)]
        public new SelectionMode SelectionMode
        {
            get { return (SelectionMode)base.SelectionMode; }
            set { base.SelectionMode = (System.Windows.Forms.SelectionMode)value; }
        }

        [DisplayName("СтильГраницы")]
        [Description("Указывает стиль рамки для объекта ПолеСписка (ListBox).")]
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
        public DockStyle Dock_osf
        {
            get { return _Dock; }
            set
            {
                base.Dock = (System.Windows.Forms.DockStyle)value;
                _Dock = value;
            }
        }
				
        //скроем свойство полученное при наследовании, для того чтобы оно не мешало нашему замещающему свойству использовать свой эдитор и конвертер.
        [Browsable(false)]
        public new System.Windows.Forms.DockStyle Dock { get; set; }

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

        [DisplayName("ШиринаКолонки")]
        [Description("Возвращает или задает ширину колонок в многоколоночном ПолеСписка (ListBox).")]
        [Category("Поведение")]
        [Browsable(true)]
        public new int ColumnWidth
        {
            get { return base.ColumnWidth; }
            set { base.ColumnWidth = value; }
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

        [DisplayName("Элементы")]
        [Description("Возвращает элементы ПолеСписка (ListBox).")]
        [Category("Данные")]
        [Browsable(true)]
        [TypeConverter(typeof(MyCollectionConverter))]
        [Editor(typeof(MyListBoxCollectionEditor), typeof(UITypeEditor))]
        public new System.Windows.Forms.ListBox.ObjectCollection Items
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

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool CausesValidation { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.ContextMenuStrip ContextMenuStrip { get; set; }
		
        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.ControlBindingsCollection DataBindings { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new object DataSource { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.DrawMode DrawMode { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new string FormatString { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool FormattingEnabled { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.ImeMode ImeMode { get; set; }
		
        [Browsable(false)]
        [ReadOnly(true)]
        public new bool IntegralHeight { get; set; }
		
        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.Padding Margin { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Drawing.Size MaximumSize { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Drawing.Size MinimumSize { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.RightToLeft RightToLeft { get; set; }
		
        [Browsable(false)]
        [ReadOnly(true)]
        public new object ValueMember { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new object DisplayMember { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new object Tag { get; set; }

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
                            designer.ActionLists.Add(new ListBoxActionList(designer));
                            tic1 = tic1 + 1;
                        }
                    }
                }
            }
        }

        public class ListBoxActionList : DesignerActionList
        {
            private ListBox _control;
            private DesignerActionUIService designerActionUISvc = null;

            public ListBoxActionList(ControlDesigner designer) : base(designer.Component)
            {
                _control = (ListBox)designer.Component;
                this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
            }

            private PropertyDescriptor GetPropertyByName(String propName)
            {
                return TypeDescriptor.GetProperties(_control)[propName];
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

            public override DesignerActionItemCollection GetSortedActionItems()
            {
                var items = new DesignerActionItemCollection();
                items.Add(new DesignerActionMethodItem(this, "EditItems", "Изменить элементы...", "", true));
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
Элементы ==
";
            }
        }
    }
}
