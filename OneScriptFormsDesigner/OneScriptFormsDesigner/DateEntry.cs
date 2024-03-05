using System.ComponentModel;
using System;

namespace osfDesigner
{
    [DesignTimeVisible(false)]
    public class DateEntry : Component
    {
        public DateEntry()
        {
        }

        public DateEntry(DateTime p1)
        {
            M_DateTime = p1;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public DateTime M_DateTime { get; set; }

        [DisplayName("Значение")]
        [Description("Текущее время, обычно выраженное как дата и время суток.")]
        [Category("Дата")]
        [Browsable(true)]
        public DateTime Value
        {
            get { return M_DateTime; }
            set { M_DateTime = value; }
        }
    }
}
