using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;

namespace osfDesigner
{
    public class ListViewItem : System.Windows.Forms.ListViewItem
    {

        public ListViewItem()
        {
        }

        [DisplayName("ИндексИзображения")]
        [Description("Возвращает или задает индекс изображения, отображаемого для данного элемента.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyImageIndexConverter))]
        public new int ImageIndex
        {
            get { return base.ImageIndex; }
            set { base.ImageIndex = value; }
        }

        [DisplayName("ИспользоватьСтильДляПодэлементов")]
        [Description("Возвращает или задает значение, указывающее, будут ли шрифты, свойства ОсновнойЦвет (ForeColor) и ЦветФона (BackColor) для элемента использоваться для всех его подэлементов.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool UseItemStyleForSubItems
        {
            get { return base.UseItemStyleForSubItems; }
            set { base.UseItemStyleForSubItems = value; }
        }

        [DisplayName("ОсновнойЦвет")]
        [Description("Возвращает или задает цвет текста элемента.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        [DisplayName("Подэлементы")]
        [Description("Возвращает коллекцию, содержащую все подэлементы элемента.")]
        [Category("Данные")]
        [Browsable(true)]
        [TypeConverter(typeof(MyCollectionConverter))]
        [Editor(typeof(MyListViewSubItemCollectionEditor), typeof(UITypeEditor))]
        public new ListViewSubItemCollection SubItems
        {
            get { return base.SubItems; }
        }

        [DisplayName("Помечен")]
        [Description("Возвращает или задает значение, указывающее, является ли элемент помеченным.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool Checked
        {
            get { return base.Checked; }
            set { base.Checked = value; }
        }

        [DisplayName("Текст")]
        [Description("Возвращает или задает текст элемента.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        [DisplayName("ЦветФона")]
        [Description("Возвращает или задает цвет фона текста элемента.")]
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
        [Description("Возвращает или задает шрифт текста, отображаемого элементом.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyFontConverter))]
        public new Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        [Browsable(false)]
        public new dynamic Group { get; set; }
        
        [Browsable(false)]
        public new dynamic ImageKey { get; set; }
        
        [Browsable(false)]
        public new dynamic IndentCount { get; set; }
        
        [Browsable(false)]
        public new dynamic ListView { get; set; }

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
        public new dynamic Position { get; set; }
        
        [Browsable(false)]
        public new dynamic StateImageIndex { get; set; }
        
        [Browsable(false)]
        public new dynamic Tag { get; set; }
        
        [Browsable(false)]
        public new dynamic ToolTipText { get; set; }

        [Browsable(false)]
        public string DefaultValues { get; set; }
			
        [Browsable(false)]
        public string RequiredValues
        {
            get
            {
                return @"
Подэлементы ==
Текст ==
";
            }
        }
    }
}
