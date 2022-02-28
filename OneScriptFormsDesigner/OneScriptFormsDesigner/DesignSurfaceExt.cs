using System.ComponentModel.Design.Serialization;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    // Этот класс добавляет к экземпляру .NET DesignSurface следующие возможности:
    //     * TabOrder
    //     * UndoEngine
    //     * Cut/Copy/Paste/Delete commands
    //
    // DesignSurfaceExt
    //     |
    //     +--|DesignSurface|
    //     |
    //     +--|TabOrder|
    //     |
    //     +--|UndoEngine|
    //     |
    //     +--|Cut/Copy/Paste/Delete commands|
    //
    public class DesignSurfaceExt : DesignSurface, IDesignSurfaceExt
    {
        private TabOrderHooker _tabOrder = null;
        public static bool _tabOrderMode = false;
        private UndoEngineExt _undoEngine = null;
        private NameCreationServiceImp _nameCreationService = null;
        private DesignerSerializationServiceImpl _designerSerializationService = null;
        private CodeDomComponentSerializationService _codeDomComponentSerializationService = null;

        //* 18.12.2021 perfolenta 
        private bool _dirty = false;
        //***

        public DesignSurfaceExt() : base()
        {
            InitServices();
        }

        public DesignSurfaceExt(IServiceProvider parentProvider) : base(parentProvider)
        {
            InitServices();
        }
        public DesignSurfaceExt(Type rootComponentType) : base(rootComponentType)
        {
            InitServices();
        }

        public DesignSurfaceExt(IServiceProvider parentProvider, Type rootComponentType) : base(parentProvider, rootComponentType)
        {
            InitServices();
        }

        public void SwitchTabOrder()
        {
            if (false == IsTabOrderMode)
            {
                InvokeTabOrder();
            }
            else
            {
                DisposeTabOrder();
            }
        }

        public void UseSnapLines()
        {
            IServiceContainer serviceProvider = GetService(typeof(IServiceContainer)) as IServiceContainer;
            DesignerOptionService opsService = serviceProvider.GetService(typeof(DesignerOptionService)) as DesignerOptionService;
            if (null != opsService)
            {
                serviceProvider.RemoveService(typeof(DesignerOptionService));
            }
            DesignerOptionService opsService2 = new DesignerOptionServiceExt4SnapLines();
            serviceProvider.AddService(typeof(DesignerOptionService), opsService2);
        }

        public void UseGrid(Size gridSize)
        {
            IServiceContainer serviceProvider = GetService(typeof(IServiceContainer)) as IServiceContainer;
            DesignerOptionService opsService = serviceProvider.GetService(typeof(DesignerOptionService)) as DesignerOptionService;
            if (null != opsService)
            {
                serviceProvider.RemoveService(typeof(DesignerOptionService));
            }
            DesignerOptionService opsService2 = new DesignerOptionServiceExt4Grid(gridSize);
            serviceProvider.AddService(typeof(DesignerOptionService), opsService2);
        }

        public void UseGridWithoutSnapping(Size gridSize)
        {
            IServiceContainer serviceProvider = GetService(typeof(IServiceContainer)) as IServiceContainer;
            DesignerOptionService opsService = serviceProvider.GetService(typeof(DesignerOptionService)) as DesignerOptionService;
            if (null != opsService)
            {
                serviceProvider.RemoveService(typeof(DesignerOptionService));
            }
            DesignerOptionService opsService2 = new DesignerOptionServiceExt4GridWithoutSnapping(gridSize);
            serviceProvider.AddService(typeof(DesignerOptionService), opsService2);
        }

        public void UseNoGuides()
        {
            IServiceContainer serviceProvider = GetService(typeof(IServiceContainer)) as IServiceContainer;
            DesignerOptionService opsService = serviceProvider.GetService(typeof(DesignerOptionService)) as DesignerOptionService;
            if (null != opsService)
            {
                serviceProvider.RemoveService(typeof(DesignerOptionService));
            }
            DesignerOptionService opsService2 = new DesignerOptionServiceExt4NoGuides();
            serviceProvider.AddService(typeof(DesignerOptionService), opsService2);
        }

        public UndoEngineExt GetUndoEngineExt()
        {
            return _undoEngine;
        }

        private IComponent CreateRootComponentCore(Type controlType, Size controlSize, DesignerLoader loader)
        {
            try
            {
                // Получим IDesignerHost. Если мы не сможем его получить, тогда откат (возврат null)
                IDesignerHost host = GetIDesignerHost();
                if (null == host)
                {
                    return null;
                }
                // Проверьте, установлен ли уже корневой компонент.
                if (null != host.RootComponent)
                {
                    return null;
                }
                // Создайте новый корневой компонент и инициализируйте его с помощью конструктора.
                // Если компонент не имеет конструктора - откат, иначе выполните инициализацию.
                if (null != loader)
                {
                    BeginLoad(loader);
                    if (LoadErrors.Count > 0)
                    {
                        throw new Exception(@"DesignSurfaceExt::CreateRootComponentCore() - Исключение: сбой в BeginLoad(loader)!");
                    }
                }
                else
                {
                    if (controlType != null)
                    {
                        BeginLoad(controlType);
                        if (LoadErrors.Count > 0)
                        {
                            throw new Exception(@"DesignSurfaceExt::CreateRootComponentCore() - Исключение: сбой в BeginLoad(Type)! Некоторая ошибка во время " + controlType.ToString() + " загрузки");
                        }
                    }
                }
                // Попробуйте изменить размер только что созданного объекта.
                IDesignerHost ihost = GetIDesignerHost();
                // Установите цвет фона и размер.
                Control ctrl = null;
                if (host.RootComponent is Form)
                {
                    ctrl = View as Control;
                    ctrl.BackColor = Color.LightGray;
                    // Установите размер.
                    PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(ctrl);
                    // Задайте свойство через PropertyDescriptor.
                    PropertyDescriptor pdS = pdc.Find("Size", false);
                    if (null != pdS)
                    {
                        pdS.SetValue(ihost.RootComponent, controlSize);
                    }
                }
                else if (host.RootComponent is UserControl)
                {
                    ctrl = View as Control;
                    ctrl.BackColor = SystemColors.ControlDarkDark;
                    // Установите размер.
                    PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(ctrl);
                    // Задайте свойство через PropertyDescriptor.
                    PropertyDescriptor pdS = pdc.Find("Size", false);
                    if (null != pdS)
                    {
                        pdS.SetValue(ihost.RootComponent, controlSize);
                    }
                }
                else if (host.RootComponent is Control)
                {
                    ctrl = View as Control;
                    ctrl.BackColor = Color.LightGray;
                    // Установите размер.
                    PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(ctrl);
                    // Задайте свойство через PropertyDescriptor.
                    PropertyDescriptor pdS = pdc.Find("Size", false);
                    if (null != pdS)
                    {
                        pdS.SetValue(ihost.RootComponent, controlSize);
                    }
                }
                else if (host.RootComponent is Component)
                {
                    ctrl = View as Control;
                    ctrl.BackColor = Color.White;
                    // Не устанавливайте размер.
                }
                else
                {
                    // Тип Хоста не определен.
                    ctrl = View as Control;
                    ctrl.BackColor = Color.Red;
                }
                return ihost.RootComponent;
            }
            catch (Exception exx)
            {
                Debug.WriteLine(exx.Message);
                if (null != exx.InnerException)
                {
                    Debug.WriteLine(exx.InnerException.Message);
                }
                throw;
            }
        }

        public IComponent CreateRootComponent(Type controlType, Size controlSize)
        {
            return CreateRootComponentCore(controlType, controlSize, null);
        }

        public IComponent CreateRootComponent(DesignerLoader loader, Size controlSize)
        {
            return CreateRootComponentCore(null, controlSize, loader);
        }

        public Control CreateControl(Type controlType, Size controlSize, Point controlLocation)
        {
            try
            {
                // Получим IDesignerHost. Если мы не сможем его получить, тогда откат (возврат null).
                IDesignerHost host = GetIDesignerHost();
                if (null == host)
                {
                    return null;
                }
                // Проверьте, установлен ли уже корневой компонент. Если это не так - откат (возврат null).
                if (null == host.RootComponent)
                {
                    return null;
                }
                // Создайте новый компонент и инициализируйте его с помощью конструктора.
                // Если компонент не имеет конструктора - откат (возврат null), иначе выполните инициализацию.
                IComponent newComp = host.CreateComponent(controlType);
                if (null == newComp)
                {
                    return null;
                }
                IDesigner designer = host.GetDesigner(newComp);
                if (null == designer)
                {
                    return null;
                }
                if (designer is IComponentInitializer)
                {
                    ((IComponentInitializer)designer).InitializeNewComponent(null);
                }
                // Попробуйте изменить размер/расположение только что созданного объекта.
                PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(newComp);
                // Задайте свойство через PropertyDescriptor.
                PropertyDescriptor pdS = pdc.Find("Size", false);
                if (null != pdS)
                {
                    pdS.SetValue(newComp, controlSize);
                }
                PropertyDescriptor pdL = pdc.Find("Location", false);
                if (null != pdL)
                {
                    pdL.SetValue(newComp, controlLocation);
                }
                // Зафиксируйте операцию создания/добавления элемента управления в корневой компонент DesignSurface
                // и верните только что созданный элемент управления, чтобы провести дальнейшие инициализации.
                ((Control)newComp).Parent = host.RootComponent as Control;
                return newComp as Control;
            }
            catch (Exception exx)
            {
                Debug.WriteLine(exx.Message);
                if (null != exx.InnerException)
                {
                    Debug.WriteLine(exx.InnerException.Message);
                }
                throw;
            }
        }

        public IDesignerHost GetIDesignerHost()
        {
            return (IDesignerHost)(GetService(typeof(IDesignerHost)));
        }

        public Control GetView()
        {
            Control ctrl = View as Control;
            if (null == ctrl)
            {
                return null;
            }
            ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
            return ctrl;
        }

        public bool IsTabOrderMode
        {
            get { return _tabOrderMode; }
        }

        public TabOrderHooker TabOrder
        {
            get
            {
                if (_tabOrder == null)
                {
                    _tabOrder = new TabOrderHooker();
                }
                return _tabOrder;
            }
            set { _tabOrder = value; }
        }

        public void InvokeTabOrder()
        {
            TabOrder.HookTabOrder(GetIDesignerHost());
            _tabOrderMode = true;
        }

        public void DisposeTabOrder()
        {
            TabOrder.HookTabOrder(GetIDesignerHost());
            _tabOrderMode = false;
        }

        // Класс DesignSurface автоматически предоставляет несколько услуг во время проектирования.
        // Класс DesignSurface добавляет все свои службы в своем конструкторе.
        // Большинство этих служб можно переопределить, заменив их в свойстве ServiceContainer. 
        // Чтобы заменить службу, переопределите конструктор, вызовите базовый и внесите любые изменения с помощью свойства ServiceContainer.
        private void InitServices()
        {
            // Каждая поверхность дизайна имеет свои собственные службы по умолчанию.
            // Мы можем оставить службы по умолчанию в их нынешнем состоянии, или мы можем удалить их и заменить своими собственными.
            // Добавьте наши собственные сервисы с помощью IServiceContainer.
            // 1. NameCreationService
            _nameCreationService = new NameCreationServiceImp();
            if (_nameCreationService != null)
            {
                ServiceContainer.RemoveService(typeof(INameCreationService), false);
                ServiceContainer.AddService(typeof(INameCreationService), _nameCreationService);
            }
            // 2. CodeDomComponentSerializationService
            _codeDomComponentSerializationService = new CodeDomComponentSerializationService(ServiceContainer);
            if (_codeDomComponentSerializationService != null)
            {
                ServiceContainer.RemoveService(typeof(ComponentSerializationService), false);
                ServiceContainer.AddService(typeof(ComponentSerializationService), _codeDomComponentSerializationService);
            }
            // 3. IDesignerSerializationService
            _designerSerializationService = new DesignerSerializationServiceImpl(ServiceContainer);
            if (_designerSerializationService != null)
            {
                ServiceContainer.RemoveService(typeof(IDesignerSerializationService), false);
                ServiceContainer.AddService(typeof(IDesignerSerializationService), _designerSerializationService);
            }
            // 4. UndoEngine
            _undoEngine = new UndoEngineExt(ServiceContainer);
            // Отключим UndoEngine.
            _undoEngine.Enabled = false;
            if (_undoEngine != null)
            {
                ServiceContainer.RemoveService(typeof(UndoEngine), false);
                ServiceContainer.AddService(typeof(UndoEngine), _undoEngine);
            }
            // 5. IMenuCommandService
            ServiceContainer.AddService(typeof(IMenuCommandService), new MenuCommandService(this));

            //* 18.12.2021 perfolenta
            // 6.
            IComponentChangeService cs = ServiceContainer.GetService(typeof(IComponentChangeService)) as IComponentChangeService;

                if (!(cs == null)){
                cs.ComponentChanged += new ComponentChangedEventHandler(OnComponentChanged);
                cs.ComponentAdded += new ComponentEventHandler(OnComponentAdded);
                cs.ComponentRemoved += new ComponentEventHandler(OnComponentRemoved);
            };

            //***
        }

        //* 18.12.2021 perfolenta
        private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            _dirty = true;
        }
        private void OnComponentAdded(object sender, ComponentEventArgs e)
        {
            _dirty = true;
        }
        private void OnComponentRemoved(object sender, ComponentEventArgs e)
        {
            _dirty = true;
        }
        //***

        // Выполните некоторые команды меню Edit с помощью MenuCommandServiceImp.
        public void DoAction(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                return;
            }

            IMenuCommandService ims = GetService(typeof(IMenuCommandService)) as IMenuCommandService;
            if (null == ims)
            {
                return;
            }

            try
            {
                switch (command.ToUpper())
                {
                    case "CUT":
                        ims.GlobalInvoke(StandardCommands.Cut);
                        break;
                    case "COPY":
                        ims.GlobalInvoke(StandardCommands.Copy);
                        break;
                    case "PASTE":
                        ims.GlobalInvoke(StandardCommands.Paste);
                        break;
                    case "DELETE":
                        ims.GlobalInvoke(StandardCommands.Delete);
                        break;
                    default:
                        // Ничего не делаем;
                        break;
                }
            }
            catch { }
        }

        //* 17.12.2021 perfolenta
        public bool Dirty
        {
            get
            {
                return _dirty;
            }
            set
            {
                _dirty = value;
            }
        }
        //***
    }
}
