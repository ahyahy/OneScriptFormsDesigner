using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;

namespace osfDesigner
{
    public class FontDialog : System.Windows.Forms.FontDialog
    {

        public FontDialog()
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

        [DisplayName("Шрифт")]
        [Description("Возвращает или задает выбранный шрифт.")]
        [Category("Данные")]
        [Browsable(true)]
        [TypeConverter(typeof(MyFontConverter))]
        public new Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool AllowScriptChange { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool AllowSimulations { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool AllowVectorFonts { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool AllowVerticalFonts { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool FixedPitchOnly { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool FontMustExist { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new int MaxSize { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new int MinSize { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool ScriptsOnly { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool ShowApply { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool ShowColor { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool ShowEffects { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool ShowHelp { get; set; }

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
