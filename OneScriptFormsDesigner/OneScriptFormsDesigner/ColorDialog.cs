using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;

namespace osfDesigner
{
    public class ColorDialog : System.Windows.Forms.ColorDialog
    {

        public ColorDialog()
        {
        }

        [DisplayName("Цвет")]
        [Description("Возвращает или задает выбранный цвет.")]
        [Category("Данные")]
        [Browsable(true)]
        [TypeConverter(typeof(MyColorConverter))]
        [Editor(typeof(MyColorEditor), typeof(UITypeEditor))]
        public new Color Color
        {
            get { return base.Color; }
            set { base.Color = value; }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool AllowFullOpen { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool AnyColor { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool FullOpen { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool ShowHelp { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool SolidColorOnly { get; set; }

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
                return @"";
            }
        }
    }
}
