using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Collections;

namespace osfDesigner
{
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // этот класс управляет коллекцией экземпляров DesignSurfaceExt2
    // этот класс добавляет к экземплярам DesignSurfaceExt2 следующие объекты:
    // PropertyGridHost 
    //
    // DesignSurfaceExt2
    //     |
    //     +--|PropertyGridHost|
    //     |
    //     +--|Toolbox|
    //     |
    //     +--|ContextMenu|
    //     |
    //     +--|DesignSurfaceExt|
    //             |
    //             +--|DesignSurface|
    //             |
    //             +--|TabOrder|
    //             |
    //             +--|UndoEngine|
    //             |
    //             +--|Cut/Copy/Paste/Delete commands|

    public class DesignSurfaceManagerExt : DesignSurfaceManager
    {
        private const string _Name_ = "DesignSurfaceManagerExt";

        // Список List<> необходим для удаления ранее созданных поверхностей проектирования
        // Note: 
        // Свойство DesignSurfaceManager.DesignSurfaces - это набор поверхностей проектирования
        // которые в настоящее время размещены в DesignSurfaceManager, но доступны только для чтения
        private List<DesignSurfaceExt2> DesignSurfaceExt2Collection = new List<DesignSurfaceExt2>();

        public DesignSurfaceManagerExt() : base()
        {
            Init();
        }
        // Параметры:
        //   parentProvider:
        //      Родительский поставщик услуг. Запросы на обслуживание направляются этому поставщику
        //      если они не могут быть разрешены менеджером поверхности проектирования.
        public DesignSurfaceManagerExt(IServiceProvider parentProvider) : base(parentProvider)
        {
            Init();
        }

        public PropertyGridHost PropertyGridHost { get; private set; }

        private void Init()
        {
            this.PropertyGridHost = new PropertyGridHost(this);
            // добавьте PropertyGridHost и ComboBox в качестве служб чтобы они были доступны для каждой поверхности дизайна
            // (DesignSurface нуждается в PropertyGridHost/ComboBox, а не в размещающем их UserControl, поэтому
            // мы предоставляем PropertyGridHost/ComboBox, встроенный в наш UserControl PropertyGridExt)
            this.ServiceContainer.AddService(typeof(System.Windows.Forms.PropertyGrid), PropertyGridHost.PropertyGrid);
            this.ServiceContainer.AddService(typeof(System.Windows.Forms.ComboBox), PropertyGridHost.ComboBox);
            this.ActiveDesignSurfaceChanged += (object sender, ActiveDesignSurfaceChangedEventArgs e) =>
            {
                //меняем изображение на менюшке Порядок обхода
                Program.pDesignerMainForm1.ChangeImage(pDesigner.DSME.ActiveDesignSurface.TabOrder._tabOrder != null);

                DesignSurfaceExt2 surface = e.NewSurface as DesignSurfaceExt2;
                if (null == surface)
                {
                    return;
                }

                UpdatePropertyGridHost(surface);

                ISelectionService iSel = (ISelectionService)(surface.GetService(typeof(ISelectionService)));
                ICollection collection1 = iSel.GetSelectedComponents();
                System.ComponentModel.Component[] arr = new System.ComponentModel.Component[collection1.Count];
                collection1.CopyTo(arr, 0);
                System.ComponentModel.Component comp = null;
                try
                {
                    comp = arr[0];
                }
                catch { }

                PropertyGridHost.ReloadTreeView();
                if (comp != null)
                {
                    PropertyGridHost.ChangeSelectNode(comp);
                }
            };
        }

        public void UpdatePropertyGridHost(DesignSurfaceExt2 surface)
        {
            IDesignerHost host = (IDesignerHost)surface.GetService(typeof(IDesignerHost));
            if (null == host)
            {
                return;
            }
            if (null == host.RootComponent)
            {
                return;
            }

            // синхронизируем с PropertyGridHost
            this.PropertyGridHost.SelectedObject = host.RootComponent;
            this.PropertyGridHost.ReloadComboBox();
        }

        // Метод CreateDesignSurfaceCore вызывается обоими методами CreateDesignSurface
        // Это реализация, которая фактически создает поверхность проектирования
        // Реализация по умолчанию просто возвращает новую поверхность дизайна, мы переопределяем
        // этот метод, чтобы предоставить пользовательский объект, производный от класса DesignSurface
        // т. е. новый экземпляр DesignSurfaceExt2
        protected override DesignSurface CreateDesignSurfaceCore(IServiceProvider parentProvider)
        {
            return new DesignSurfaceExt2(parentProvider);
        }

        // Получим новый DesignSurfaceExt2 и загрузим его с соответствующим типом корневого компонента.
        public DesignSurfaceExt2 CreateDesignSurfaceExt2()
        {
            // с классом DesignSurfaceManager бесполезно добавлять новые службы 
            // для каждой поверхности дизайна, которую мы собираемся создать,
            // из-за параметра "IServiceProvider" метода CreateDesignSurface(IServiceProvider).
            // Этот параметр позволяет каждой созданной поверхности дизайнера
            // использовать сервисы DesignSurfaceManager.
            // Будет создан новый объединенный поставщик услуг, который сначала запросит
            // у этого поставщика услугу, а затем делегирует любые сбои 
            // к объекту диспетчера поверхности проектирования.
            // Note:
            //     следующая строка кода создает совершенно новую поверхность дизайна, которая добавляется
            //     в коллекцию Designsurfeces, т.е. свойство "this.DesignSurfaces"(.Count увеличится на единицу)
            DesignSurfaceExt2 surface = (DesignSurfaceExt2)(this.CreateDesignSurface(this.ServiceContainer));

            // каждый раз, когда создается совершенно новая поверхность дизайна, подписывайте наш обработчик на
            //  его SelectionService.SelectionChanged событие для синхронизации PropertyGridHost
            ISelectionService selectionService = (ISelectionService)(surface.GetService(typeof(ISelectionService)));
            if (null != selectionService)
            {
                selectionService.SelectionChanged += (object sender, EventArgs e) =>
                {
                    ISelectionService selectService = sender as ISelectionService;
                    if (null == selectService)
                    {
                        return;
                    }
                    if (0 == selectService.SelectionCount)
                    {
                        return;
                    }
                    // Синхронизация с PropertyGridHost
                    System.Windows.Forms.PropertyGrid propertyGrid = (System.Windows.Forms.PropertyGrid)this.GetService(typeof(System.Windows.Forms.PropertyGrid));
                    if (null == propertyGrid)
                    {
                        return;
                    }
                    ArrayList comps = new ArrayList();
                    comps.AddRange(selectService.GetSelectedComponents());
                    propertyGrid.SelectedObjects = comps.ToArray();

                };
            }
            DesignSurfaceExt2Collection.Add(surface);
            this.ActiveDesignSurface = surface;

            // и вернем поверхность дизайнера (чтобы можно было вызвать её метод BeginLoad())
            return surface;
        }

        public void DeleteDesignSurfaceExt2(DesignSurfaceExt2 item)
        {
            DesignSurfaceExt2Collection.Remove(item);
            try
            {
                item.Dispose();
            }
            catch { }
            int currentIndex = DesignSurfaceExt2Collection.Count - 1;
            if (currentIndex >= 0)
            {
                ActiveDesignSurface = DesignSurfaceExt2Collection[currentIndex];
            }
            else
            {
                ActiveDesignSurface = null;
            }
        }

        public void DeleteDesignSurfaceExt2(int index)
        {
            DesignSurfaceExt2 item = DesignSurfaceExt2Collection[index];
            DesignSurfaceExt2Collection.RemoveAt(index);
            try
            {
                item.Dispose();
            }
            catch { }
            int currentIndex = DesignSurfaceExt2Collection.Count - 1;
            if (currentIndex >= 0)
            {
                ActiveDesignSurface = DesignSurfaceExt2Collection[currentIndex];
            }
            else
            {
                ActiveDesignSurface = null;
            }
        }

        // просмотрим коллекцию всех поверхностей дизайнера DesignSurface чтобы узнать новое имя формы
        public string GetValidFormName()
        {
            // мы решили использовать "Form_" с символом подчеркивания в качестве шаблона потому что .NET сервисы
            // дизайнера предоставляют имя типа: "FormN" с N=1,2,3,4,... поэтому использование "Форма",
            // без символа подчеркивания в качестве шаблона, вызовет некоторые проблемы, когда мы ищем, используется имя
            // или нет использование другого шаблона (с подчеркиванием) позволяет избежать этой проблемы
            string newFormNameHeader = "Form_";
            newFormNameHeader = newFormNameHeader.Replace("Form_", "Форма_");
            int newFormNametrailer = -1;
            string newFormName = string.Empty;
            bool isNew = true;
            do
            {
                isNew = true;
                newFormNametrailer++;
                newFormName = newFormNameHeader + newFormNametrailer;
                foreach (DesignSurfaceExt2 item in DesignSurfaceExt2Collection)
                {
                    string currentFormName = item.GetIDesignerHost().RootComponent.Site.Name;
                    isNew &= ((newFormName == currentFormName) ? false : true);
                }
            } while (!isNew);
            return newFormName;
        }

        public new DesignSurfaceExt2 ActiveDesignSurface
        {
            get { return base.ActiveDesignSurface as DesignSurfaceExt2; }
            set { base.ActiveDesignSurface = value; }
        }

        //* 18.12.2021 perfolenta
        public IEnumerable<DesignSurfaceExt2> GetDesignSurfaces()
        {
            return DesignSurfaceExt2Collection;
        }
        //***

    }
}
