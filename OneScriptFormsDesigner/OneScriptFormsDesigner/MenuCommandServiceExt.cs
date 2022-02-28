using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    class MenuCommandServiceExt : IMenuCommandService
    {
        // Этот ServiceProvider является экземпляром DesignsurfaceExt2, переданным в качестве параметра внутри конструктора.
        IServiceProvider _serviceProvider = null;
        MenuCommandService _menuCommandService = null;

        public MenuCommandServiceExt(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _menuCommandService = new MenuCommandService(serviceProvider);
        }
        
        public void ShowContextMenu(CommandID menuID, int x, int y)
        {
            ContextMenu contextMenu = new ContextMenu();

            // Добавим стандартные команды CUT/COPY/PASTE/DELETE.
            MenuCommand command = FindCommand(StandardCommands.Cut);
            if (command != null)
            {
                MenuItem menuItem = new MenuItem("Вырезать", new EventHandler(OnMenuClicked));
                menuItem.Tag = command;
                contextMenu.MenuItems.Add(menuItem);
            }
            command = FindCommand(StandardCommands.Copy);
            if (command != null)
            {
                MenuItem menuItem = new MenuItem("Копировать", new EventHandler(OnMenuClicked));
                menuItem.Tag = command;
                contextMenu.MenuItems.Add(menuItem);
            }
            command = FindCommand(StandardCommands.Paste);
            if (command != null)
            {
                MenuItem menuItem = new MenuItem("Вставить", new EventHandler(OnMenuClicked));
                menuItem.Tag = command;
                contextMenu.MenuItems.Add(menuItem);
            }
            command = FindCommand(StandardCommands.Delete);
            if (command != null)
            {
                MenuItem menuItem = new MenuItem("Удалить", new EventHandler(OnMenuClicked));
                menuItem.Tag = command;
                contextMenu.MenuItems.Add(menuItem);
            }

            // Покажем контекстное меню.
            DesignSurface surface = (DesignSurface)_serviceProvider;
            Control viewService = (Control)surface.View;
            
            if (viewService != null)
            {
                contextMenu.Show(viewService, viewService.PointToClient(new Point(x, y)));
            }
        }

        // Управление при выборе контекстного меню.
        private void OnMenuClicked(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null && menuItem.Tag is MenuCommand)
            {
                MenuCommand command = menuItem.Tag as MenuCommand;
                if (command.CommandID.ID == 17)
                {
                    System.Windows.Forms.DialogResult fact = MessageBox.Show(
                        "Действительно удалить выбранные компоненты?",
                        "",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2
                       );

                    if (fact == System.Windows.Forms.DialogResult.OK || fact == System.Windows.Forms.DialogResult.Yes)
                    {
                        command.Invoke();
                    }
                }
                else
                {
                    command.Invoke();
                }
            }
        }

        public void AddCommand(MenuCommand command)
        {
            _menuCommandService.AddCommand(command);
        }

        public void AddVerb(DesignerVerb verb)
        {
            _menuCommandService.AddVerb(verb);
        }

        public MenuCommand FindCommand(CommandID commandID)
        {
            return _menuCommandService.FindCommand(commandID);
        }

        public bool GlobalInvoke(CommandID commandID)
        {
            return _menuCommandService.GlobalInvoke(commandID);
        }

        public void RemoveCommand(MenuCommand command)
        {
            _menuCommandService.RemoveCommand(command);
        }
    
        public void RemoveVerb(DesignerVerb verb)
        {
            _menuCommandService.RemoveVerb(verb);
        }

        public DesignerVerbCollection Verbs
        {
            get { return _menuCommandService.Verbs; }
        }
    }
}
