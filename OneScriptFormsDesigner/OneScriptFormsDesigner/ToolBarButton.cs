using System.ComponentModel;
using System.Drawing;

namespace osfDesigner
{
    public class ToolBarButton : System.Windows.Forms.ToolBarButton
    {
        public System.Windows.Forms.ToolBarButton M_ToolBarButton;
        private bool _PartialPush_osf;

        [Browsable(false)]
        public System.Windows.Forms.ToolBarButton OriginalObj
        {
            get { return M_ToolBarButton; }
            set { M_ToolBarButton = value; }
        }
        
        public ToolBarButton() : base()
        {
        }
        
        public ToolBarButton(string p1 = null) : base(p1)
        {
        }

        [DisplayName("Доступность")]
        [Description("Возвращает или задает значение, указывающее, включена ли кнопка.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public bool Enabled_osf
        {
            get { return M_ToolBarButton.Enabled; }
            set { M_ToolBarButton.Enabled = value; }
        }

        [DisplayName("ИндексИзображения")]
        [Description("Возвращает или задает значение индекса изображения, назначенного кнопке.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyImageIndexConverter))]
        public new int ImageIndex
        {
            get { return M_ToolBarButton.ImageIndex; }
            set { M_ToolBarButton.ImageIndex = value; }
        }

        [DisplayName("Нажата")]
        [Description("Возвращает или задает значение, указывающее, находится ли кнопка-переключатель в нажатом состоянии.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool Pushed
        {
            get { return M_ToolBarButton.Pushed; }
            set { M_ToolBarButton.Pushed = value; }
        }

        [DisplayName("НейтральноеПоложение")]
        [Description("Возвращает или задает значение, указывающее, частично ли нажата кнопка панели инструментов в стиле тумблера.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public  bool PartialPush_osf
        {
            get { return _PartialPush_osf; }
            set
            {
                M_ToolBarButton.PartialPush = value;
                _PartialPush_osf = value;
            }
        }

        [DisplayName("Отображать")]
        [Description("Возвращает или задает значение, указывающее, видима ли кнопка панели инструментов.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public bool Visible_osf
        {
            get { return M_ToolBarButton.Visible; }
            set { M_ToolBarButton.Visible = value; }
        }

        [DisplayName("Прямоугольник")]
        [Description("Возвращает ограничивающий прямоугольник для кнопки панели инструментов.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyRectangleConverter))]
        public new Rectangle Rectangle
        {
            get { return M_ToolBarButton.Rectangle; }
        }

        [DisplayName("Стиль")]
        [Description("Возвращает или задает стиль кнопки панели инструментов.")]
        [Category("Прочее")]
        [Browsable(true)]
        public new ToolBarButtonStyle Style
        {
            get { return (ToolBarButtonStyle)M_ToolBarButton.Style; }
            set { M_ToolBarButton.Style = (System.Windows.Forms.ToolBarButtonStyle)value; }
        }

        [DisplayName("Текст")]
        [Description("Возвращает или задает текст, отображаемый на кнопке панели инструментов.")]
        [Category("Прочее")]
        [Browsable(true)]
        public new string Text
        {
            get { return M_ToolBarButton.Text; }
            set { M_ToolBarButton.Text = value; }
        }

        [DisplayName("ТекстПодсказки")]
        [Description("Возвращает или задает текст всплывающей подсказки для кнопки панели инструментов.")]
        [Category("Прочее")]
        [Browsable(true)]
        public new string ToolTipText
        {
            get { return M_ToolBarButton.ToolTipText; }
            set { M_ToolBarButton.ToolTipText = value; }
        }

        [Browsable(false)]
        public new dynamic Container { get; set; }

        [Browsable(false)]
        public new dynamic DropDownMenu { get; set; }
        
        [Browsable(false)]
        public new dynamic ImageKey { get; set; }
        
        [DisplayName("(Name)")]
        [Description("Указывает имя, используемое в коде для идентификации объекта.")]
        [Category("Разработка")]
        [Browsable(true)]
        [ReadOnly(true)]
        public new string Name
        {
            get { return M_ToolBarButton.Name; }
            set { M_ToolBarButton.Name = value; }
        }
        
        [Browsable(false)]
        public new dynamic Parent { get; set; }
        
        [Browsable(false)]
        public new bool PartialPush { get; set; }
        
        [Browsable(false)]
        public new dynamic Site { get; set; }
		
        [Browsable(false)]
        public new bool Visible { get; set; }
		
        [Browsable(false)]
        public new bool Enabled { get; set; }
        
        [Browsable(false)]
        public new dynamic Tag { get; set; }

        [Browsable(false)]
        public string DefaultValues { get; set; }
			
        [Browsable(false)]
        public string RequiredValues
        {
            get
            {
                return @"
Текст ==
";
            }
        }
    }
}
