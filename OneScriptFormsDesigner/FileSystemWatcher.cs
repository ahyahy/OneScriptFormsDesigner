using System.ComponentModel;
using System.Drawing.Design; 

namespace osfDesigner
{
    public class FileSystemWatcher : System.IO.FileSystemWatcher
    {

        private bool _IncludeSubDirectories_osf;
        private string _Changed_osf;
        private string _Renamed_osf;
        private string _Created_osf;
        private string _Deleted_osf;

        public FileSystemWatcher()
        {
        }

        [DisplayName("ВключаяПодкаталоги")]
        [Description("Возвращает или задает значение, указывающее, следует ли отслеживать подкаталоги в указанном пути.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public  bool IncludeSubDirectories_osf
        {
            get { return _IncludeSubDirectories_osf; }
            set { _IncludeSubDirectories_osf = value; }
        }

        [DisplayName("КомпонентДоступен")]
        [Description("Возвращает или задает значение, определяющее, доступен ли данный компонент.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool EnableRaisingEvents
        {
            get { return base.EnableRaisingEvents; }
            set { base.EnableRaisingEvents = value; }
        }

        [DisplayName("ПриИзменении")]
        [Description("Возвращает или задает код для выполнения при изменении файла или каталога по заданному пути.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Changed_osf
        {
            get { return _Changed_osf; }
            set { _Changed_osf = value; }
        }

        [DisplayName("ПриПереименовании")]
        [Description("Возвращает или задает код для выполнения при переименовании файла или каталога по заданному пути.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Renamed_osf
        {
            get { return _Renamed_osf; }
            set { _Renamed_osf = value; }
        }

        [DisplayName("ПриСоздании")]
        [Description("Возвращает или задает код для выполнения при создании файла или каталога по заданному пути.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Created_osf
        {
            get { return _Created_osf; }
            set { _Created_osf = value; }
        }

        [DisplayName("ПриУдалении")]
        [Description("Возвращает или задает код для выполнения при удалении файла или каталога по заданному пути.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Deleted_osf
        {
            get { return _Deleted_osf; }
            set { _Deleted_osf = value; }
        }

        [DisplayName("Путь")]
        [Description("Возвращает или задает путь отслеживаемого каталога.")]
        [Category("Прочее")]
        [Browsable(true)]
        public new string Path
        {
            get { return base.Path; }
            set { base.Path = value; }
        }

        [DisplayName("Фильтр")]
        [Description("Возвращае или задает строку фильтра, используемую для определения файлов, контролируемых в каталоге.")]
        [Category("Прочее")]
        [Browsable(true)]
        public new string Filter
        {
            get { return base.Filter; }
            set { base.Filter = value; }
        }

        [DisplayName("ФильтрУведомлений")]
        [Description("Возвращает или задает тип отслеживаемых изменений.")]
        [Category("Прочее")]
        [Browsable(true)]
        [Editor(typeof(MyNotifyFiltersEditor), typeof(UITypeEditor))]
        public new NotifyFilters NotifyFilter
        {
            get { return (NotifyFilters)base.NotifyFilter; }
            set { base.NotifyFilter = (System.IO.NotifyFilters)value; }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public new int IncludeSubdirectories { get; set; }

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
