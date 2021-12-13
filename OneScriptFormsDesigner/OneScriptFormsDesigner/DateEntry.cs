using System.ComponentModel;

namespace osfDesigner
{
    [DesignTimeVisible(false)]
    public class DateEntry : Component
    {
        public DateEntry()
        {
        }

        public DateEntry(System.DateTime p1)
        {
            M_DateTime = p1;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public System.DateTime M_DateTime { get; set; }

        [DisplayName("Значение")]
        [Description("Текущее время, обычно выраженное как дата и время суток.")]
        [Category("Дата")]
        [Browsable(true)]
        public System.DateTime Value
        {
            get { return M_DateTime; }
            set { M_DateTime = value; }
        }
    }
}
