using System.ComponentModel;

namespace osfDesigner
{
    public class SaveFileDialog : System.Windows.Forms.Timer
    {

        public SaveFileDialog()
        {
        }

        [DisplayName("ВосстанавливатьКаталог")]
        [Description("Возвращает или задает значение, указывающее, восстанавливает ли диалоговое окно ранее выбранный каталог в качестве текущего каталога перед закрытием.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public  bool RestoreDirectory { get; set; }

        [DisplayName("ДобавитьРасширение")]
        [Description("Возвращает или задает значение, указывающее, будет ли диалоговое окно автоматически добавлять расширение к имени файла, если пользователь опускает расширение.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public  bool AddExtension { get; set; }

        [DisplayName("Заголовок")]
        [Description("Возвращает или задает заголовок для окна ФайловыйДиалог (FileDialog).")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public  string Title { get; set; }

        [DisplayName("ИмяФайла")]
        [Description("Возвращает или задает строку, содержащую имя файла, выбранное в окне ФайловыйДиалог (FileDialog).")]
        [Category("Данные")]
        [Browsable(true)]
        public  string FileName { get; set; }

        [DisplayName("ИндексФильтра")]
        [Description("Возвращает или задает индекс фильтра, выбранного в настоящее время в окне ФайловыйДиалог (FileDialog). Первый фильтр имеет индекс 1. Значение по умолчанию это один.")]
        [Category("Поведение")]
        [Browsable(true)]
        public  int FilterIndex { get; set; }

        [DisplayName("НачальныйКаталог")]
        [Description("Возвращает или задает начальный каталог, отображаемый в окне ФайловыйДиалог (FileDialog).")]
        [Category("Данные")]
        [Browsable(true)]
        public  string InitialDirectory { get; set; }

        [DisplayName("ПодтверждениеПерезаписи")]
        [Description("Возвращает или задает значение, указывающее, отображается ли в диалоговом окне ДиалогСохраненияФайла (SaveFileDialog) предупреждение, если пользователь указывает имя файла, которое уже существует.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public  bool OverwritePrompt { get; set; }

        [DisplayName("ПодтверждениеСоздания")]
        [Description("Возвращает или задает значение, указывающее, запрашивает ли диалоговое окно разрешение пользователя на создание файла, если пользователь указывает несуществующий файл.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public  bool CreatePrompt { get; set; }

        [DisplayName("ПроверятьСуществованиеПути")]
        [Description("Возвращает или задает значение, указывающее, будет ли диалоговое окно отображать предупреждение, если пользователь указывает путь, который не существует.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public  bool CheckPathExists { get; set; }

        [DisplayName("ПроверятьСуществованиеФайла")]
        [Description("Возвращает или задает значение, указывающее, будет ли диалоговое окно отображать предупреждение, если пользователь указывает имя не существующего файла.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public  bool CheckFileExists { get; set; }

        [DisplayName("РазыменоватьСсылки")]
        [Description("Возвращает или задает значение, указывающее, будет ли диалоговое окно возвращать местоположение файла, на который ссылается ярлык, или вернёт местоположение ярлыка (.lnk).")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public  bool DereferenceLinks { get; set; }

        [DisplayName("РасширениеПоУмолчанию")]
        [Description("Возвращает или задает расширение имени файла по умолчанию.")]
        [Category("Поведение")]
        [Browsable(true)]
        public  string DefaultExt { get; set; }

        [DisplayName("Фильтр")]
        [Description("Возвращает или задает текущую строку фильтра имен файлов, которая определяет варианты, доступные в поле диалогового окна 'Сохранить как файл типа' или 'Файлы типа'.")]
        [Category("Поведение")]
        [Browsable(true)]
        public  string Filter { get; set; }

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
