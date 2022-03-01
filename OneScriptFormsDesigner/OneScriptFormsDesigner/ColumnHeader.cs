using System.ComponentModel;
using System.Drawing.Design; 

namespace osfDesigner
{
    public class ColumnHeader : System.Windows.Forms.ColumnHeader
    {

        public ColumnHeader()
        {
        }

        [DisplayName("ВыравниваниеТекста")]
        [Description("Возвращает или задает горизонтальное выравнивание текста, отображаемого в Колонка (ColumnHeader).")]
        [Category("Прочее")]
        [Browsable(true)]
        public new HorizontalAlignment TextAlign
        {
            get { return (HorizontalAlignment)base.TextAlign; }
            set { base.TextAlign = (System.Windows.Forms.HorizontalAlignment)value; }
        }

        [DisplayName("Текст")]
        [Description("Возвращает или задает текст, отображаемый в заголовке колонки.")]
        [Category("Прочее")]
        [Browsable(true)]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        [DisplayName("ТипСортировки")]
        [Description("Возвращает или задает тип сортировки.")]
        [Category("Прочее")]
        [Browsable(true)]
        public SortType SortType { get; set; }

        [DisplayName("Ширина")]
        [Description("Возвращает или задает ширину колонки.")]
        [Category("Прочее")]
        [Browsable(true)]
        public new int Width
        {
            get { return base.Width; }
            set { base.Width = value; }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public new int DisplayIndex { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new int ImageIndex { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new string ImageKey { get; set; }
		
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
