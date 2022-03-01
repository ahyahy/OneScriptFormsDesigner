using System.ComponentModel;

namespace osfDesigner
{
    public class MenuItemEntry : FilterablePropertyBase
    {
        public System.Windows.Forms.MenuItem M_MenuItem;

        public MenuItemEntry()
        {
            M_MenuItem = new System.Windows.Forms.MenuItem();
            Hide = "Скрыть";
        }

        [Browsable(false)]
        public string Hide { get; set; }

        [DisplayName("Доступность")]
        [Description("Возвращает или задает значение, указывающее, доступен ли элемент меню.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        [DynamicPropertyFilter("Hide", "Показать")]
        public bool Enabled
        {
            get { return M_MenuItem.Enabled; }
            set { M_MenuItem.Enabled = value; }
        }

        [Browsable(false)]
        public System.Windows.Forms.Menu.MenuItemCollection MenuItems
        {
            get { return M_MenuItem.MenuItems; }
        }

        [Browsable(false)]
        public System.Windows.Forms.Menu Parent
        {
            get { return M_MenuItem.Parent; }
        }

        [DisplayName("Нажатие")]
        [Description("Возвращает или задает код для выполнения, когда элемент меню нажат или выбран с помощью сочетания клавиш или ключа доступа, определенного для пункта меню.")]
        [Category("Прочее")]
        [Browsable(true)]
        [DynamicPropertyFilter("Hide", "Показать")]
        public string Click { get; set; }

        [DisplayName("Отображать")]
        [Description("Возвращает или задает значение, указывающее, является ли пункт меню видимым.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        [DynamicPropertyFilter("Hide", "Показать")]
        public bool Visible
        {
            get { return M_MenuItem.Visible; }
            set { M_MenuItem.Visible = value; }
        }

        [DisplayName("Переключатель")]
        [Description("Возвращает или задает значение, указывающее, отображается ли в элементе меню переключатель вместо флажка.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        [DynamicPropertyFilter("Hide", "Показать")]
        public bool RadioCheck
        {
            get { return M_MenuItem.RadioCheck; }
            set { M_MenuItem.RadioCheck = value; }
        }

        [DisplayName("Помечен")]
        [Description("Возвращает или задает значение, указывающее, что установлен флажок рядом с текстом пункта меню.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        [DynamicPropertyFilter("Hide", "Показать")]
        public bool Checked
        {
            get { return M_MenuItem.Checked; }
            set { M_MenuItem.Checked = value; }
        }

        [DisplayName("ПорядокСлияния")]
        [Description("Возвращает или задает значение, указывающее относительное положение элемента меню при его слиянии с другим.")]
        [Category("Прочее")]
        [Browsable(true)]
        [DynamicPropertyFilter("Hide", "Показать")]
        public int MergeOrder
        {
            get { return M_MenuItem.MergeOrder; }
            set { M_MenuItem.MergeOrder = value; }
        }

        [DisplayName("СочетаниеКлавиш")]
        [Description("Возвращает или задает значение, указывающее комбинацию клавиш связанных с пунктом меню.")]
        [Category("Прочее")]
        [Browsable(true)]
        [DynamicPropertyFilter("Hide", "Показать")]
        public Shortcut Shortcut
        {
            get { return (Shortcut)(int)M_MenuItem.Shortcut; }
            set { M_MenuItem.Shortcut = (System.Windows.Forms.Shortcut)value; }
        }

        [DisplayName("Текст")]
        [Description("Возвращает или задает значение, указывающее заголовок пункта меню.")]
        [Category("Прочее")]
        [Browsable(true)]
        [DynamicPropertyFilter("Hide", "Показать")]
        public string Text
        {
            get { return M_MenuItem.Text; }
            set { M_MenuItem.Text = value; }
        }

        [DisplayName("ТипСлияния")]
        [Description("Возвращает или задает значение, указывающее поведение данного элемента меню при слиянии его меню с другим меню.")]
        [Category("Прочее")]
        [Browsable(true)]
        [DynamicPropertyFilter("Hide", "Показать")]
        public osfDesigner.MenuMerge MergeType
        {
            get { return (osfDesigner.MenuMerge)(int)M_MenuItem.MergeType; }
            set { M_MenuItem.MergeType = (System.Windows.Forms.MenuMerge)value; }
        }

        [DisplayName("(Name)")]
        [Description("Указывает имя, используемое в коде для идентификации объекта.")]
        [Category("Разработка")]
        [Browsable(true)]
        [ReadOnly(true)]
        public string Name
        {
            get { return M_MenuItem.Name; }
            set { M_MenuItem.Name = value; }
        }

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
