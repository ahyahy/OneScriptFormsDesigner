using System.ComponentModel;

namespace osfDesigner
{
    public class Timer : System.Windows.Forms.Timer
    {

        private string _Tick_osf;

        public Timer()
        {
        }

        [DisplayName("Интервал")]
        [Description("Возвращает или задает время в миллисекундах между отметками таймера.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new int Interval
        {
            get { return base.Interval; }
            set { base.Interval = value; }
        }

        [DisplayName("ПриСрабатыванииТаймера")]
        [Description("Возвращает или задает код для выполнения по истечении указанного интервала таймера и таймер включен.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Tick_osf
        {
            get { return _Tick_osf; }
            set { _Tick_osf = value; }
        }

        [Browsable(false)]
        public new dynamic Container { get; set; }
        
        [Browsable(false)]
        public new dynamic Enabled { get; set; }

        [Browsable(false)]
        public new dynamic Site { get; set; }
        
        [Browsable(false)]
        public new dynamic Tag { get; set; }

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
