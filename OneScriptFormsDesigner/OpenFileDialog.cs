using System.ComponentModel;

namespace osfDesigner
{
    public class OpenFileDialog : System.Windows.Forms.Timer
    {

        public OpenFileDialog()
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

        [DisplayName("ПоказатьТолькоДляЧтения")]
        [Description("Возвращает или задает значение, указывающее, имеется ли в диалоговом окне флажок 'доступно только для чтения'.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public  bool ShowReadOnly { get; set; }

        [DisplayName("ПомеченТолькоЧтение")]
        [Description("Возвращает или задает значение, указывающее, установлен ли флажок доступности 'только для чтения'.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public  bool ReadOnlyChecked { get; set; }

        [DisplayName("ПроверятьСуществованиеПути")]
        [Description("Возвращает или задает значение, указывающее, будет ли диалоговое окно отображать предупреждение, если пользователь указывает путь, который не существует.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public  bool CheckPathExists { get; set; }

        [DisplayName("ПроверятьСуществованиеФайла")]
        [Description("Возвращает или задает значение, указывающее, отображает ли диалоговое окно предупреждение, если пользователь указывает несуществующий путь.")]
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
