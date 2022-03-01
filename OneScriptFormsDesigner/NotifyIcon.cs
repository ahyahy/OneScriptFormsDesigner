using System.ComponentModel;
using System.Drawing.Design; 

namespace osfDesigner
{
    public class NotifyIcon : System.Windows.Forms.Timer
    {

        private MyIcon _icon;

        public NotifyIcon()
        {
        }

        [DisplayName("ДвойноеНажатие")]
        [Description("Возвращает или задает код для выполнения, когда пользователь дважды щелкает значок в области уведомлений на панели задач.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string DoubleClick_osf { get; set; }

        [DisplayName("Значок")]
        [Description("Возвращает или задает текущий значок.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyIconConverter))]
        [Editor(typeof(MyIconEditor), typeof(UITypeEditor))]
        [DefaultValue(null)]
        public MyIcon Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        [DisplayName("Нажатие")]
        [Description("Возвращает или задает код для выполнения, когда пользователь щелкает значок в области уведомлений.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Click_osf { get; set; }

        [DisplayName("Отображать")]
        [Description("Возвращает или задает значение, определяющее видимость значка в области уведомлений на панели задач.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public  bool Visible { get; set; }

        [DisplayName("ПриНажатииКнопкиМыши")]
        [Description("Возвращает или задает код для выполнения, когда пользователь нажимает кнопку мыши, а указатель мыши находится на значке в области уведомлений панели задач.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseDown_osf { get; set; }

        [DisplayName("ПриОтпусканииМыши")]
        [Description("Возвращает или задает код для выполнения, когда пользователь отпускает кнопку мыши, а указатель мыши находится на значке в области уведомлений панели задач.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseUp_osf { get; set; }

        [DisplayName("ПриПеремещенииМыши")]
        [Description("Возвращает или задает код для выполнения, когда пользователь перемещает мыши, в то время как указатель находится над значком в области уведомлений панели задач.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseMove_osf { get; set; }

        [DisplayName("Текст")]
        [Description("Возвращает или задает текст подсказки, отображаемый при наведении указателя мыши на значок в области уведомлений.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public  string Text { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new object Tag { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool Enabled { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new int Interval { get; set; }

        [Browsable(false)]
        public string DefaultValues { get; set; }
			
        [Browsable(false)]
        public string RequiredValues
        {
            get
            {
                return @"";
            }
        }
    }
}
