using System.ComponentModel;

namespace osfDesigner
{
    public class ToolTip : System.Windows.Forms.ToolTip
    {

        public ToolTip()
        {
        }

        [DisplayName("АвтоЗадержка")]
        [Description("Возвращает или задает автоматическую задержку всплывающей подсказки в миллисекундах.")]
        [Category("Прочее")]
        [Browsable(true)]
        public new int AutomaticDelay
        {
            get { return base.AutomaticDelay; }
            set { base.AutomaticDelay = value; }
        }

        [DisplayName("АвтоЗадержкаПоказа")]
        [Description("Возвращает или задает интервал времени, в миллисекундах, в течение которого всплывающая подсказка отображается на экране, когда указатель мыши останавливается в границах элемента управления с текстом данной подсказки.")]
        [Category("Прочее")]
        [Browsable(true)]
        public new int AutoPopDelay
        {
            get { return base.AutoPopDelay; }
            set { base.AutoPopDelay = value; }
        }

        [DisplayName("Активна")]
        [Description("Возвращает или задает значение, указывающее, активна ли в настоящий момент всплывающая подсказка.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool Active
        {
            get { return base.Active; }
            set { base.Active = value; }
        }

        [DisplayName("ЗадержкаОчередногоПоказа")]
        [Description("Возвращает или задает интервал времени в миллисекундах, который должен пройти перед появлением окна очередной всплывающей подсказки при перемещении указателя мыши с одного элемента управления на другой.")]
        [Category("Прочее")]
        [Browsable(true)]
        public new int ReshowDelay
        {
            get { return base.ReshowDelay; }
            set { base.ReshowDelay = value; }
        }

        [DisplayName("ЗадержкаПоявления")]
        [Description("Возвращает или задает интервал времени в миллисекундах, в течение которого указатель мыши должен оставаться в границах элемента управления, прежде чем появится окно всплывающей подсказки.")]
        [Category("Прочее")]
        [Browsable(true)]
        public new int InitialDelay
        {
            get { return base.InitialDelay; }
            set { base.InitialDelay = value; }
        }

        [DisplayName("ПоказыватьВсегда")]
        [Description("Возвращает или задает значение, указывающее, отображается ли окно всплывающей подсказки, если родительский элемент управления не активен.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool ShowAlways
        {
            get { return base.ShowAlways; }
            set { base.ShowAlways = value; }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public new dynamic BackColor { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new dynamic ForeColor { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new dynamic IsBalloon { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new dynamic OwnerDraw { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new dynamic StripAmpersands { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new dynamic Tag { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new dynamic ToolTipIcon { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new dynamic ToolTipTitle { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new dynamic UseAnimation { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new dynamic UseFading { get; set; }

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
