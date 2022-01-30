using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    // Этот класс добавляет в экземпляр DesignSurfaceExt следующие средства:
    //     * Toolbox механизм (Контейнер ToolBox должен быть предоставлен пользователем)
    //     * ContextMenu on DesignSurface с командами Cut/Copy/Paste/Delete
    //
    // DesignSurfaceExt2
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
    //
    public class DesignSurfaceExt2 : DesignSurfaceExt, IDesignSurfaceExt2
    {
        private MenuCommandServiceExt _menuCommandService = null;
        private ToolboxServiceImp _toolboxService = null;

        public DesignSurfaceExt2() : base()
        {
            InitServices();
        }
        public DesignSurfaceExt2(IServiceProvider parentProvider) : base(parentProvider)
        {
            InitServices();
        }
        public DesignSurfaceExt2(Type rootComponentType) : base(rootComponentType)
        {
            InitServices();
        }
        public DesignSurfaceExt2(IServiceProvider parentProvider, Type rootComponentType) : base(parentProvider, rootComponentType)
        {
            InitServices();
        }

        public ToolboxServiceImp GetIToolboxService()
        {
            return (ToolboxServiceImp) this.GetService(typeof(IToolboxService));
        }

        public void EnableDragandDrop()
        {
            // Для управления перетаскиванием элементов.
            Control ctrl = this.GetView();
            if (null == ctrl)
            {
                return;
            }
            ctrl.AllowDrop = true;
            ctrl.DragDrop += new DragEventHandler(OnDragDrop);

            // Включить захват элемента внутри нашей панели инструментов.
            ToolboxServiceImp tbs = this.GetIToolboxService();
            if (null == tbs)
            {
                return;
            }
            if (null == tbs.Toolbox)
            {
                return;
            }
            tbs.Toolbox.MouseDown += new MouseEventHandler(OnListboxMouseDown);
        }

        // Управление Drag&Drop для элементов, содержащихся внутри Toolbox.
        private void OnListboxMouseDown(object sender, MouseEventArgs e)
        {
            ToolboxServiceImp tbs = this.GetIToolboxService();
            if (null == tbs)
            {
                return;
            }
            if (null == tbs.Toolbox)
            {
                return;
            }
            if (null == tbs.Toolbox.SelectedItem)
            {
                return;
            }
            tbs.Toolbox.DoDragDrop(tbs.Toolbox.SelectedItem, DragDropEffects.Copy | DragDropEffects.Move);
        }

        // Управление взятием и бросанием элементов.
        public void OnDragDrop(object sender, DragEventArgs e)
        {
            // Если пользователь не перетаскивает элемент ToolboxItem, то ничего не делаем.
            if (!e.Data.GetDataPresent(typeof(ToolboxItem)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            // Теперь извлеките узел данных.
            ToolboxItem item = e.Data.GetData(typeof(ToolboxItem)) as ToolboxItem;
            e.Effect = DragDropEffects.Copy;
            item.CreateComponents(this.GetIDesignerHost());
        }

        // Класс DesignSurface автоматически предоставляет несколько услуг во время проектирования.
        // Класс DesignSurface добавляет все свои службы в свой конструктор. Большинство этих служб можно переопределить, заменив их в свойстве ServiceContainer. 
        // Чтобы заменить службу, переопределите конструктор, вызовите базовый конструктор и внесите любые изменения с помощью свойства ServiceContainer.
        private void InitServices()
        {
            // Каждая поверхность дизайна имеет свои собственные службы по умолчанию.
            // Мы можем оставить службы по умолчанию в их нынешнем состоянии, или мы можем удалить их и заменить своими собственными.
            // Теперь добавьте наши собственные сервисы с помощью IServiceContainer.
            _menuCommandService = new MenuCommandServiceExt(this);
            if (_menuCommandService != null)
            {
                // Удалите старую службу, т. е. службу DesignsurfaceExt.
                this.ServiceContainer.RemoveService(typeof(IMenuCommandService), false);
                // Добавьте новую IMenuCommandService.
                this.ServiceContainer.AddService(typeof(IMenuCommandService), _menuCommandService);
            }
            // IToolboxService
            _toolboxService = new ToolboxServiceImp(this.GetIDesignerHost());
            if (_toolboxService != null)
            {
                this.ServiceContainer.RemoveService(typeof(IToolboxService), false);
                this.ServiceContainer.AddService(typeof(IToolboxService), _toolboxService);
            }
        }
    }
}
