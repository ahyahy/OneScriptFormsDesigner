using System.ComponentModel;

namespace osfDesigner
{
    public class FolderBrowserDialog : System.Windows.Forms.Timer
    {

        public FolderBrowserDialog()
        {
        }

        [DisplayName("ВыбранныйПуть")]
        [Description("Возвращает или задает путь, выбранный пользователем.")]
        [Category("Просмотр папок")]
        [Browsable(true)]
        public string SelectedPath { get; set; }

        [DisplayName("КорневойКаталог")]
        [Description("Возвращает или задает корневой каталог, с которой начинается просмотр.")]
        [Category("Просмотр папок")]
        [Browsable(true)]
        public SpecialFolder RootFolder { get; set; }

        [DisplayName("Описание")]
        [Description("Возвращает или задает описательный текст, отображаемый над элементом управления древовидного представления в диалоговом окне.")]
        [Category("Просмотр папок")]
        [Browsable(true)]
        public string Description { get; set; }

        [DisplayName("ПоказатьКнопкуНовогоКаталога")]
        [Description("Возвращает или задает значение, указывающее, появляется ли кнопка  в диалоговом окне браузера папок.")]
        [Category("Просмотр папок")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public bool ShowNewFolderButton { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new object Tag { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool Enabled { get; set; }
        
        [Browsable(false)]
        [ReadOnly(true)]
        public new int Interval { get; set; }

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
