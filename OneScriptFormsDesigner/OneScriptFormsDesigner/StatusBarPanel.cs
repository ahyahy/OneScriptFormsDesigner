using System.ComponentModel;
using System.Drawing.Design; 

namespace osfDesigner
{
    public class StatusBarPanel : System.Windows.Forms.StatusBarPanel
    {

        private MyIcon _icon;

        public StatusBarPanel()
        {
        }

        [DisplayName("АвтоРазмер")]
        [Description("Возвращает или задает значение, указывающее, изменяется ли автоматически размер панели строки состояния.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new StatusBarPanelAutoSize AutoSize
        {
            get { return (StatusBarPanelAutoSize)base.AutoSize; }
            set { base.AutoSize = (System.Windows.Forms.StatusBarPanelAutoSize)value; }
        }

        [DisplayName("Значок")]
        [Description("Возвращает или задает значок на панели строки состояния.")]
        [Category("Внешний вид")]
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

        [DisplayName("МинимальнаяШирина")]
        [Description("Возвращает или задает минимальную ширину панели строки состояния.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new int MinWidth
        {
            get { return base.MinWidth; }
            set { base.MinWidth = value; }
        }

        [DisplayName("СтильГраницы")]
        [Description("Возвращает или задает стиль границы панели строки состояния.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new StatusBarPanelBorderStyle BorderStyle
        {
            get { return (StatusBarPanelBorderStyle)base.BorderStyle; }
            set { base.BorderStyle = (System.Windows.Forms.StatusBarPanelBorderStyle)value; }
        }

        [DisplayName("Текст")]
        [Description("Возвращает или задает текст панели строки состояния.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        [DisplayName("Ширина")]
        [Description("Возвращает или задает ширину панели строки состояния.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new int Width
        {
            get { return base.Width; }
            set { base.Width = value; }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public new dynamic Alignment { get; set; }
		
        [DisplayName("(Name)")]
        [Description("Указывает имя, используемое в коде для идентификации объекта.")]
        [Category("Разработка")]
        [Browsable(true)]
        [ReadOnly(true)]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public new dynamic Style { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new string ToolTipText { get; set; }
		
        [Browsable(false)]
        [ReadOnly(true)]
        public new object Tag { get; set; }

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
