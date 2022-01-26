using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;

namespace osfDesigner
{
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // этот класс добавляет к экземпляру .NET DesignSurface следующие возможности:
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
        private const string _Name_ = "DesignSurfaceExt";
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
            IServiceContainer serviceProvider = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
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
            IServiceContainer serviceProvider = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
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
            IServiceContainer serviceProvider = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
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
            IServiceContainer serviceProvider = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
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
            return this._undoEngine;
        }

        private IComponent CreateRootComponentCore(Type controlType, Size controlSize, DesignerLoader loader)
        {
            const string _signature_ = _Name_ + @"::CreateRootComponentCore()";
            try
            {
                // получим IDesignerHost. Если мы не сможем его получить, тогда откат (возврат null)
                IDesignerHost host = GetIDesignerHost();
                if (null == host)
                {
                    return null;
                }
                // проверьте, установлен ли уже корневой компонент, если это так, то откат (возврат null)
                if (null != host.RootComponent)
                {
                    return null;
                }
                // создайте новый корневой компонент и инициализируйте его с помощью конструктора
                // если компонент не имеет конструктора, откат (возврат null), иначе выполните инициализацию
                if (null != loader)
                {
                    this.BeginLoad(loader);
                    if (this.LoadErrors.Count > 0)
                    {
                        throw new Exception(_signature_ + " - Exception: the BeginLoad(loader) failed!");
                    }



                }
                else
                {
                    if (controlType != null)
                    {
                        this.BeginLoad(controlType);
                        if (this.LoadErrors.Count > 0)
                        {
                            throw new Exception(_signature_ + " - Exception: the BeginLoad(Type) failed! Some error during " + controlType.ToString() + " loading");
                        }
                    }
                }
                // попробуйте изменить размер только что созданного объекта
                IDesignerHost ihost = GetIDesignerHost();
                // Установите цвет фона и размер
                Control ctrl = null;
                if (host.RootComponent is Form)
                {
                    ctrl = this.View as Control;
                    ctrl.BackColor = Color.LightGray;
                    // установите размер
                    PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(ctrl);
                    // Задает свойство через PropertyDescriptor
                    PropertyDescriptor pdS = pdc.Find("Size", false);
                    if (null != pdS)
                    {
                        pdS.SetValue(ihost.RootComponent, controlSize);
                    }
                }
                else if (host.RootComponent is UserControl)
                {
                    ctrl = this.View as Control;
                    ctrl.BackColor = System.Drawing.SystemColors.ControlDarkDark;
                    // установите размер
                    PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(ctrl);
                    // Задает свойство через PropertyDescriptor
                    PropertyDescriptor pdS = pdc.Find("Size", false);
                    if (null != pdS)
                    {
                        pdS.SetValue(ihost.RootComponent, controlSize);
                    }
                }
                else if (host.RootComponent is Control)
                {
                    ctrl = this.View as Control;
                    ctrl.BackColor = Color.LightGray;
                    // установите размер
                    PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(ctrl);
                    // Задает свойство через PropertyDescriptor
                    PropertyDescriptor pdS = pdc.Find("Size", false);
                    if (null != pdS)
                    {
                        pdS.SetValue(ihost.RootComponent, controlSize);
                    }
                }
                else if (host.RootComponent is Component)
                {
                    ctrl = this.View as Control;
                    ctrl.BackColor = Color.White;
                    // не устанавливайте размер
                }
                else
                {
                    // Если тип Хоста не определен
                    ctrl = this.View as Control;
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
                // получим IDesignerHost. Если мы не сможем его получить, тогда откат (возврат null)
                IDesignerHost host = GetIDesignerHost();
                if (null == host)
                {
                    return null;
                }
                // проверьте, установлен ли уже корневой компонент. Если это не так, то откат (возврат null)
                if (null == host.RootComponent)
                {
                    return null;
                }
                // создайте новый компонент и инициализируйте его с помощью конструктора
                // если компонент не имеет конструктора, откат (возврат null), иначе выполните инициализацию
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
                // попробуйте изменить размер/расположение только что созданного объекта
                PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(newComp);
                // Задает свойство через PropertyDescriptor
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
                // зафиксируйте операцию создания/добавления элемента управления в корневой компонент DesignSurface
                // и верните только что созданный элемент управления, чтобы позволить дальнейшим инициализациям
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
            return (IDesignerHost)(this.GetService(typeof(IDesignerHost)));
        }

        public Control GetView()
        {
            Control ctrl = this.View as Control;
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
            TabOrder.HookTabOrder(this.GetIDesignerHost());
            _tabOrderMode = true;
        }

        public void DisposeTabOrder()
        {
            TabOrder.HookTabOrder(this.GetIDesignerHost());
            _tabOrderMode = false;
        }

        // Класс DesignSurface автоматически предоставляет несколько услуг во время проектирования.
        // Класс DesignSurface добавляет все свои службы в своем конструкторе.
        // Большинство этих служб можно переопределить, заменив их в свойстве
        // ServiceContainer. Чтобы заменить службу, переопределите конструктор,
        // вызовите базовый и внесите любые изменения с помощью свойства ServiceContainer.
        private void InitServices()
        {
            // каждая поверхность дизайна имеет свои собственные службы по умолчанию
            // Мы можем оставить службы по умолчанию в их нынешнем состоянии,
            // или мы можем удалить их и заменить своими собственными.
            // Теперь добавьте наши собственные сервисы с помощью IServiceContainer
            // 1. NameCreationService
            _nameCreationService = new NameCreationServiceImp();
            if (_nameCreationService != null)
            {
                this.ServiceContainer.RemoveService(typeof(INameCreationService), false);
                this.ServiceContainer.AddService(typeof(INameCreationService), _nameCreationService);
            }
            // 2. CodeDomComponentSerializationService
            _codeDomComponentSerializationService = new CodeDomComponentSerializationService(this.ServiceContainer);
            if (_codeDomComponentSerializationService != null)
            {
                this.ServiceContainer.RemoveService(typeof(ComponentSerializationService), false);
                this.ServiceContainer.AddService(typeof(ComponentSerializationService), _codeDomComponentSerializationService);
            }
            // 3. IDesignerSerializationService
            _designerSerializationService = new DesignerSerializationServiceImpl(this.ServiceContainer);
            if (_designerSerializationService != null)
            {
                this.ServiceContainer.RemoveService(typeof(IDesignerSerializationService), false);
                this.ServiceContainer.AddService(typeof(IDesignerSerializationService), _designerSerializationService);
            }
            // 4. UndoEngine
            _undoEngine = new UndoEngineExt(this.ServiceContainer);
            // отключим UndoEngine
            _undoEngine.Enabled = false;
            if (_undoEngine != null)
            {
                this.ServiceContainer.RemoveService(typeof(UndoEngine), false);
                this.ServiceContainer.AddService(typeof(UndoEngine), _undoEngine);
            }
            // 5. IMenuCommandService
            this.ServiceContainer.AddService(typeof(IMenuCommandService), new MenuCommandService(this));

            //* 18.12.2021 perfolenta
            // 6.
            IComponentChangeService cs = this.ServiceContainer.GetService(typeof(IComponentChangeService)) as IComponentChangeService;

                if(!( cs == null)){
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


        // выполните некоторые команды Edit menu с помощью MenuCommandServiceImp
        public void DoAction(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                return;
            }

            IMenuCommandService ims = this.GetService(typeof(IMenuCommandService)) as IMenuCommandService;
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
                        // do nothing;
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
