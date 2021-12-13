using System.ComponentModel;
using System.Drawing.Design; 
using System.Drawing;

namespace osfDesigner
{
    public class ListViewSubItem : System.Windows.Forms.ListViewItem.ListViewSubItem
    {

        public ListViewSubItem()
        {
        }

        [DisplayName("ОсновнойЦвет")]
        [Description("Возвращает или задает цвет переднего плана текста подэлемента.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        [DisplayName("Текст")]
        [Description("Возвращает или задает текст подэлемента.")]
        [Category("Прочее")]
        [Browsable(true)]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        [DisplayName("ЦветФона")]
        [Description("Возвращает или задает цвет фона текста подэлемента.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        [DisplayName("Шрифт")]
        [Description("Возвращает или задает шрифт текста, отображаемого подэлементом.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyFontConverter))]
        public new Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        [Browsable(false)]
        public new dynamic Bounds { get; set; }

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
