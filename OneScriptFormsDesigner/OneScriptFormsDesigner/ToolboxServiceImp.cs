using System.Collections;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;

namespace osfDesigner
{
    // Это шлюз между пользовательским интерфейсом панели элементов среды разработки и конструкторами
    // Конструкторы постоянно запрашивают панель элементов, когда курсор над ней, чтобы получить обратную связь с выбранным элементом управления.
    // НАПОМИНАНИЕ:
    //     Этот класс реализует интерфейс IToolboxService. Он НЕ создает ПолеСписка (ListBox), он просто запоминает созданный 
    //     пользователем элемент, а затем на него ссылается через ToolboxServiceImp::Toolbox свойство.

    public class ToolboxServiceImp : IToolboxService
    {
        public IDesignerHost DesignerHost { get; private set; }
        // Наша реальная панель элементов (Toolbox).
        public System.Windows.Forms.ListBox Toolbox { get; set; }

        public ToolboxServiceImp(IDesignerHost host)
        {
			this.DesignerHost = host;
            Toolbox = null;
		}

        // Добавление создателя, который будет преобразовывать нестандартные элементы в указанном формате в элементы 
        // панели элементов (ToolboxItems), связанные с узлом.
        void IToolboxService.AddCreator(ToolboxItemCreatorCallback creator, string format, IDesignerHost host)
        {
            // НЕРЕАЛИЗОВАННО - Здесь мы не обрабатываем нестандартные элементы. Наш набор элементов является постоянным.
        }

        // Добавление создателя, который преобразует нестандартные элементы в указанном формате в элементы панели элементов (ToolboxItems).
        void IToolboxService.AddCreator(ToolboxItemCreatorCallback creator, string format)
        {
            // НЕРЕАЛИЗОВАННО - Здесь мы не обрабатываем нестандартные элементы. Наш набор элементов является постоянным.
        }

        // Добавление элемента (ToolboxItem) к нашей панели элементов, в определенной категории, связанного с определенным узлом.
        void IToolboxService.AddLinkedToolboxItem(ToolboxItem toolboxItem, string category, IDesignerHost host)
        {
            // НЕРЕАЛИЗОВАННО - Мы в итоге не сделали целый проект, так что нет необходимости
            // добавления пользовательских элементов (несмотря на то, что у нас есть вкладка для таких элементов).
        }

        // Добавление элемента (ToolboxItem) к нашей панели элементов, связанного с определенным узлом.
        void IToolboxService.AddLinkedToolboxItem(ToolboxItem toolboxItem, IDesignerHost host)
        {
            // НЕРЕАЛИЗОВАННО - Мы в итоге не сделали целый проект, так что нет необходимости
            // добавления пользовательских элементов (несмотря на то, что у нас есть вкладка для таких элементов).
        }

        // Добавление элемента (ToolboxItem) к нашей панели элементов, в определенной категории.
        void IToolboxService.AddToolboxItem(ToolboxItem toolboxItem, string category)
        {
            // У нас нет категории.
            ((IToolboxService) this).AddToolboxItem(toolboxItem);
        }

        // Добавление элемента (ToolboxItem) к нашей панели элементов.
        void IToolboxService.AddToolboxItem(ToolboxItem toolboxItem)
        {
            Toolbox.Items.Add(toolboxItem);
        }

        // Наша панель элементов имеет категории, похожие на категории Visual Studio, но вы можете группировать их любым способом. 
        // Просто поставьте вашу службу IToolboxService в известность что категорий нет, верните null.
        CategoryNameCollection IToolboxService.CategoryNames
        {
            get { return null; }
        }

        // Необходимо для перетаскивания. Мы десериализуем элемент панели элементов, когда опускаем в область конструктора.
        // Элемент панели элементов (ToolboxItem) упаковывается в объект данных (DataObject). Мы просто работаем
        // со стандартными элементами и одним узлом, поэтому параметр host игнорируется.
        ToolboxItem IToolboxService.DeserializeToolboxItem(object serializedObject, IDesignerHost host)
        {
            return ((IToolboxService)this).DeserializeToolboxItem(serializedObject);
        }

        // Мы десериализуем элемент панели элементов, когда опускаем в область конструктора.
        // Элемент панели элементов (ToolboxItem) упаковывается в объект данных (DataObject).
        ToolboxItem IToolboxService.DeserializeToolboxItem(object serializedObject)
        {
            return (ToolboxItem) ((DataObject) serializedObject).GetData(typeof(ToolboxItem));
        }

        // Возвращается выбранный элемент (ToolboxItem) в нашей панели элементов, если он связан с этим узлом.
        // Поскольку все наши элементы связаны с нашим единственным узлом, параметр host проигнорирован.
        ToolboxItem IToolboxService.GetSelectedToolboxItem(IDesignerHost host)
        {
            return ((IToolboxService)this).GetSelectedToolboxItem();
        }

        // Возвращается выбранный элемент (ToolboxItem) в нашей панели элементов (Toolbox).
        ToolboxItem IToolboxService.GetSelectedToolboxItem()
        {
            if (null == Toolbox || null == Toolbox.SelectedItem)
            {
                return null;
            }

            ToolboxItem tbItem = (ToolboxItem) Toolbox.SelectedItem;
            if (tbItem.DisplayName.ToUpper().Contains("POINTER"))
            {
                return null;
            }

            return tbItem;
        }

        // Получить все элементы категории.
        ToolboxItemCollection IToolboxService.GetToolboxItems(string category, IDesignerHost host)
        {
            // У нас нет категории.
            return ((IToolboxService) this).GetToolboxItems();
        }

        // Получить все элементы.
        ToolboxItemCollection IToolboxService.GetToolboxItems(string category)
        {
            // У нас нет категории.
            return ((IToolboxService) this).GetToolboxItems();
        }

        // Получить все элементы. Однако мы всегда используем наш текущий узел.
        ToolboxItemCollection IToolboxService.GetToolboxItems(IDesignerHost host)
        {
            return ((IToolboxService) this).GetToolboxItems();
        }

        // Получить все элементы.
        ToolboxItemCollection IToolboxService.GetToolboxItems()
        {
            if (null == Toolbox)
            {
                return null;
            }

            ToolboxItem[] arr = new ToolboxItem[Toolbox.Items.Count];
            Toolbox.Items.CopyTo(arr, 0);

            return new ToolboxItemCollection(arr);
        }

        // Мы всегда используем стандартные элементы (ToolboxItems), поэтому они всегда поддерживаются. Поэтому возвращаем true.
        bool IToolboxService.IsSupported(object serializedObject, ICollection filterAttributes)
        {
            return true;
        }

        // Мы всегда используем стандартные элементы, поэтому они всегда поддерживаются. Поэтому возвращаем true.
        bool IToolboxService.IsSupported(object serializedObject, IDesignerHost host)
        {
            return true;
        }

        // Проверка того что, сериализованный объект является элементом панели элементов (ToolboxItem). 
        // В нашем случае все наши элементы являются стандартными и из набора констант, и все они элементы панели элементов, 
        // так что если мы можем десериализовать его в нашем стандартном пути, тогда это действительно элемент панели элементов (ToolboxItem).
        // Узел игнорируется.
        bool IToolboxService.IsToolboxItem(object serializedObject, IDesignerHost host)
        {
            return ((IToolboxService) this).IsToolboxItem(serializedObject);
        }

        // Проверка того что, сериализованный объект является элементом панели элементов (ToolboxItem). 
        // В нашем случае все наши элементы являются стандартными и из набора констант, и все они элементы панели элементов, 
        // так что если мы можем десериализовать его в нашем стандартном пути, тогда это действительно элементы панели элементов (ToolboxItem).
        bool IToolboxService.IsToolboxItem(object serializedObject)
        {
            // Если мы можем десериализовать его, то это элемент панели элементов (ToolboxItem).
            if (((IToolboxService) this).DeserializeToolboxItem(serializedObject) != null)
            {
                return true;
            }

            return false;
        }

        // Обновление панели элементов (Toolbox).
        void IToolboxService.Refresh()
        {
            Toolbox.Refresh();
        }

        // Удаление создателя для указанного формата, связанного с определенным узлом.
        void IToolboxService.RemoveCreator(string format, IDesignerHost host)
        {
            // НЕРЕАЛИЗОВАННО - Здесь мы не обрабатываем нестандартные элементы. Наш набор элементов является постоянным.
        }

        // Удаление создателя для указанного формата.
        void IToolboxService.RemoveCreator(string format)
        {
            // НЕРЕАЛИЗОВАННО - Здесь мы не обрабатываем нестандартные элементы. Наш набор элементов является постоянным.
        }

        // Удаление элемента (ToolboxItem) из указанной категории в нашей панели элементов.
        void IToolboxService.RemoveToolboxItem(ToolboxItem toolboxItem, string category)
        {
            ((IToolboxService) this).RemoveToolboxItem(toolboxItem);
        }

        // Удаление элемента (ToolboxItem) из нашей панели элементов.
        void IToolboxService.RemoveToolboxItem(ToolboxItem toolboxItem)
        {
            if (null == Toolbox)
            {
                return;
            }

            Toolbox.SelectedItem = null;
            Toolbox.Items.Remove(toolboxItem);
        }

        // Если панель элементов имеет категории, тогда нужно знать какая категория выбрана.
        string IToolboxService.SelectedCategory
        {
            get { return null; }
            set { }
        }

        // Этот метод вызывается после вызова метода ToolPicked. В нашем случае мы выбираем указатель. 
        void IToolboxService.SelectedToolboxItemUsed()
        {
            if (null == Toolbox)
            {
                return;
            }

            Toolbox.SelectedItem = null;
        }

        // Сериализация элемента, необходимая для перетаскивания. Мы сериализуем панель элементов, упаковывая ее в объект данных (DataObject).
        object IToolboxService.SerializeToolboxItem(ToolboxItem toolboxItem)
        {
            DataObject dataObject = new DataObject();
            dataObject.SetData(typeof(ToolboxItem), toolboxItem);
            return dataObject;
        }

        // Если выбран инструмент, мы возможно, захотим установить курсор в какое нибудь интересующее место над областью конструктора. 
        // Если мы это сделаем, то мы определим это здесь и вернем true. В противном случае мы возвращаем значение false, чтобы вызывающий 
        // мог установить курсор в каком-либо месте по умолчанию.
        bool IToolboxService.SetCursor()
        {
            if (null == Toolbox || null == Toolbox.SelectedItem)
            {
                return false;
            }

            // <Pointer> не является инструментом, служит для отмены взятия элемента из Toolbox.
            ToolboxItem tbItem = (ToolboxItem) Toolbox.SelectedItem;
            if (tbItem.DisplayName.ToUpper().Contains("POINTER"))
            {
                return false;
            }

            if (null != Toolbox.SelectedItem)
            {
                Cursor.Current = Cursors.Cross;
                return true;
            }
            return false;
        }

        // Установить выбранным элемент в панели элементов.
        void IToolboxService.SetSelectedToolboxItem(ToolboxItem toolboxItem)
        {
            if (null == Toolbox)
            {
                return;
            }

            Toolbox.SelectedItem = toolboxItem;
        }
    }
}
