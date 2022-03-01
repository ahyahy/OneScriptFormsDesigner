using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.IO;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class MyMenuItemsEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService wfes = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            if (wfes != null)
            {
                osfDesigner.MainMenu mainMenu = (osfDesigner.MainMenu)context.Instance;
                if (mainMenu.FrmMenuItems == null)
                {
                    mainMenu.FrmMenuItems = new frmMenuItems(mainMenu);
                }
                mainMenu.FrmMenuItems._wfes = wfes;
                wfes.ShowDialog(mainMenu.FrmMenuItems);
            }
            return null;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }

    public class frmMenuItems : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label2;
        public osfDesigner.MainMenu MainMenu;
        public System.Windows.Forms.TreeView TreeView1;
        private System.Windows.Forms.Button ButtonAddRoot;
        private System.Windows.Forms.Button ButtonAddChild;
        private System.Windows.Forms.Button ButtonAddSeparator;
        private System.Windows.Forms.Button ButtonDelete;
        private System.Windows.Forms.Button ButtonMoveUp;
        private System.Windows.Forms.Button ButtonMoveDown;
        private System.Windows.Forms.Button ButtonCollapse;
        private System.Windows.Forms.Button ButtonExpand;
        private System.Windows.Forms.Button ButtonOK;
        private System.Windows.Forms.PropertyGrid PropertyGrid1;
        private System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.Panel Panel3;
        private System.Windows.Forms.Panel Panel4;
        private System.Windows.Forms.Panel Panel5;
        private System.Windows.Forms.Panel Panel6;
        private System.Windows.Forms.Panel Panel7;
        private System.Windows.Forms.Panel Panel8;
        private System.Windows.Forms.Panel Panel9;
        private System.Windows.Forms.Panel Panel10;
        private System.Windows.Forms.Panel Panel11;
        private System.Windows.Forms.Panel Panel12;
        private System.Windows.Forms.Panel Panel13;
        private System.Windows.Forms.Panel Panel14;
        public IWindowsFormsEditorService _wfes;

        public frmMenuItems(osfDesigner.MainMenu mainMenu)
        {
            MainMenu = mainMenu;
            Size = new Size(864, 485);
            Text = "Редактор коллекции ЭлементыМеню";
            ControlBox = true;
            HelpButton = true;
            ShowIcon = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            SizeGripStyle = SizeGripStyle.Auto;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmMenuItems";
            ShowInTaskbar = false;
            MinimumSize = new Size(797, 485);
            Closed += FrmMenuItems_Closed;
            Load += FrmMenuItems_Load;
            CenterToScreen();

            // Правая панель с сеткой свойств PropertyGrid1, надписью Label2 и кнопками ButtonOK и ButtonCancel.
            Panel2 = new System.Windows.Forms.Panel();
            Panel2.Parent = this;
            Panel2.Dock = System.Windows.Forms.DockStyle.Fill;

            // Левая панель с деревом TreeView1, надписью Label1 и кнопками ButtonAddRoot, ButtonAddChild, ButtonAddSeparator, ButtonCollapse, ButtonExpand, ButtonMoveUp, ButtonMoveDown и ButtonDelete.
            Panel1 = new System.Windows.Forms.Panel();
            Panel1.Parent = this;
            Panel1.Dock = System.Windows.Forms.DockStyle.Left;
            Panel1.Width = 430;

            // Панель с ButtonCollapse, ButtonExpand, ButtonMoveUp, ButtonMoveDown и ButtonDelete.
            Panel3 = new System.Windows.Forms.Panel();
            Panel3.Parent = Panel1;
            Panel3.Dock = System.Windows.Forms.DockStyle.Fill;

            // Панель с Label1, TreeView1, ButtonAddRoot и ButtonAddChild, ButtonAddSeparator.
            Panel4 = new System.Windows.Forms.Panel();
            Panel4.Parent = Panel1;
            Panel4.Dock = System.Windows.Forms.DockStyle.Left;
            Panel4.Width = 353;

            // Отступ слева от края формы.
            Panel5 = new System.Windows.Forms.Panel();
            Panel5.Parent = Panel1;
            Panel5.Dock = System.Windows.Forms.DockStyle.Left;
            Panel5.Width = 16;

            // Панель с TreeView1.
            Panel6 = new System.Windows.Forms.Panel();
            Panel6.Parent = Panel4;
            Panel6.Dock = System.Windows.Forms.DockStyle.Fill;

            // Панель с Label1.
            Panel7 = new System.Windows.Forms.Panel();
            Panel7.Parent = Panel4;
            Panel7.Dock = System.Windows.Forms.DockStyle.Top;
            Panel7.Height = 40;

            // Панель с ButtonAddRoot и ButtonAddChild, ButtonAddSeparator.
            Panel8 = new System.Windows.Forms.Panel();
            Panel8.Parent = Panel4;
            Panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            Panel8.Height = 87;

            // Панель с ButtonOK и ButtonCancel PropertyGrid1 Label2.
            Panel9 = new System.Windows.Forms.Panel();
            Panel9.Parent = Panel2;
            Panel9.Dock = System.Windows.Forms.DockStyle.Fill;

            // Отступ справа от края формы.
            Panel10 = new System.Windows.Forms.Panel();
            Panel10.Parent = Panel2;
            Panel10.Dock = System.Windows.Forms.DockStyle.Right;
            Panel10.Width = 16;

            // Панель с PropertyGrid1.
            Panel12 = new System.Windows.Forms.Panel();
            Panel12.Parent = Panel9;
            Panel12.Dock = System.Windows.Forms.DockStyle.Fill;

            // Панель с Label2.
            Panel11 = new System.Windows.Forms.Panel();
            Panel11.Parent = Panel9;
            Panel11.Dock = System.Windows.Forms.DockStyle.Top;
            Panel11.Height = 40;

            // Панель с ButtonOK и ButtonCancel.
            Panel13 = new System.Windows.Forms.Panel();
            Panel13.Parent = Panel9;
            Panel13.Dock = System.Windows.Forms.DockStyle.Bottom;
            Panel13.Height = 53;

            // Панель для выравнивания справа кнопок ButtonOK и ButtonCancel.
            Panel14 = new System.Windows.Forms.Panel();
            Panel14.Parent = Panel13;
            Panel14.Dock = System.Windows.Forms.DockStyle.Right;
            Panel14.Width = 234;

            ButtonAddRoot = new System.Windows.Forms.Button();
            ButtonAddRoot.Parent = Panel8;
            ButtonAddRoot.Bounds = new Rectangle(1, 8, 112, 37);
            ButtonAddRoot.Text = "Добавить меню";
            ButtonAddRoot.Click += ButtonAddRoot_Click;

            ButtonAddChild = new System.Windows.Forms.Button();
            ButtonAddChild.Parent = Panel8;
            ButtonAddChild.Bounds = new Rectangle(120, 8, 105, 37);
            ButtonAddChild.Text = "Добавить подменю";
            ButtonAddChild.Click += ButtonAddChild_Click;

            ButtonAddSeparator = new System.Windows.Forms.Button();
            ButtonAddSeparator.Parent = Panel8;
            ButtonAddSeparator.Bounds = new Rectangle(233, 8, 120, 37);
            ButtonAddSeparator.Text = "Добавить разделитель";
            ButtonAddSeparator.Click += ButtonAddSeparator_Click;

            Label1 = new System.Windows.Forms.Label();
            Label1.Parent = Panel7;
            Label1.Bounds = new Rectangle(1, 16, 276, 20);
            Label1.Text = "Выберите меню для правки:";

            TreeView1 = MainMenu.TreeView;
            TreeView1.Parent = Panel6;
            TreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            TreeView1.HideSelection = false;
            TreeView1.AfterSelect += TreeView1_AfterSelect;

            ButtonDelete = new System.Windows.Forms.Button();
            ButtonDelete.Parent = Panel3;
            ButtonDelete.Bounds = new Rectangle(10, 41, 35, 28);
            ButtonDelete.Text = "";
            ButtonDelete.Image = new Bitmap(new MemoryStream(Convert.FromBase64String("AAABAAEAEBAQAAEABAAoAQAAFgAAACgAAAAQAAAAIAAAAAEABAAAAAAAwAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAACAAACAAAAAgIAAgAAAAIAAgACAgAAAgICAAMDAwAAAAP8AAP8AAAD//wD/AAAA/wD/AP//AAD///8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD//////////5/x//+P8f//g+P//+HD///wh///+A////w////4D///4IP//4HB//+H8P//n/j/////////////")));
            ButtonDelete.Enabled = false;
            ButtonDelete.Click += ButtonDelete_Click;
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(ButtonDelete, "Удалить");

            ButtonMoveUp = new System.Windows.Forms.Button();
            ButtonMoveUp.Parent = Panel3;
            ButtonMoveUp.Bounds = new Rectangle(10, 76, 35, 28);
            ButtonMoveUp.Text = "";
            ButtonMoveUp.Image = new Bitmap(new MemoryStream(Convert.FromBase64String("AAABAAEAEBACAAEAAQCwAAAAFgAAACgAAAAQAAAAIAAAAAEAAQAAAAAAgAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAA////AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD8P////D////w////8P////D////w////8P///AAD//wAA//+AAf//wAP//+AH///wD///+B////w////+f///")));
            ButtonMoveUp.Enabled = false;
            ButtonMoveUp.Click += ButtonMoveUp_Click;
            System.Windows.Forms.ToolTip ToolTip2 = new System.Windows.Forms.ToolTip();
            ToolTip2.SetToolTip(ButtonMoveUp, "Переместить вверх");

            ButtonMoveDown = new System.Windows.Forms.Button();
            ButtonMoveDown.Parent = Panel3;
            ButtonMoveDown.Bounds = new Rectangle(10, 111, 35, 28);
            ButtonMoveDown.Text = "";
            ButtonMoveDown.Image = new Bitmap(new MemoryStream(Convert.FromBase64String("AAABAAEAEBACAAEAAQCwAAAAFgAAACgAAAAQAAAAIAAAAAEAAQAAAAAAgAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAA////AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD+f////D////gf///wD///4Af//8AD//+AAf//AAD//wAA///8P////D////w////8P////D////w////8P///")));
            ButtonMoveDown.Enabled = false;
            ButtonMoveDown.Click += ButtonMoveDown_Click;
            System.Windows.Forms.ToolTip ToolTip3 = new System.Windows.Forms.ToolTip();
            ToolTip3.SetToolTip(ButtonMoveDown, "Переместить вниз");

            ButtonExpand = new System.Windows.Forms.Button();
            ButtonExpand.Parent = Panel3;
            ButtonExpand.Bounds = new Rectangle(10, 146, 35, 28);
            ButtonExpand.Text = "";
            ButtonExpand.Image = new Bitmap(new MemoryStream(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAADAFBMVEUBAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACnEzGyAAAAAXRSTlMAQObYZgAAADpJREFUeNpjZIAARob/MAac+o8kACH/w5lQZWARRiQ+WATBYUAxFEmAkaAAYTOwWovuMEynY3oOyfsAOL4KE5tREc4AAAAASUVORK5CYII=")));
            ButtonExpand.Enabled = false;
            ButtonExpand.Click += ButtonExpand_Click;
            System.Windows.Forms.ToolTip ToolTip4 = new System.Windows.Forms.ToolTip();
            ToolTip4.SetToolTip(ButtonExpand, "Развернуть все");

            ButtonCollapse = new System.Windows.Forms.Button();
            ButtonCollapse.Parent = Panel3;
            ButtonCollapse.Bounds = new Rectangle(10, 181, 35, 28);
            ButtonCollapse.Text = "";
            ButtonCollapse.Image = new Bitmap(new MemoryStream(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAADAFBMVEUBAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACnEzGyAAAAAXRSTlMAQObYZgAAADlJREFUeNpjZGBgYGSAgf9QDiOCD2UzwvkwSUYYH66aEcpHMpABlwAjQQHCZuCwFt1h6E5H9xyK9wE4vgoTXfUx9wAAAABJRU5ErkJggg==")));
            ButtonCollapse.Enabled = false;
            ButtonCollapse.Click += ButtonCollapse_Click;
            System.Windows.Forms.ToolTip ToolTip5 = new System.Windows.Forms.ToolTip();
            ToolTip5.SetToolTip(ButtonCollapse, "Свернуть все");

            ButtonOK = new System.Windows.Forms.Button();
            ButtonOK.Parent = Panel14;
            ButtonOK.Bounds = new Rectangle(132, 10, 100, 28);
            ButtonOK.Text = "ОК";
            ButtonOK.Click += ButtonOK_Click;

            PropertyGrid1 = new System.Windows.Forms.PropertyGrid();
            PropertyGrid1.Parent = Panel12;
            PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            PropertyGrid1.PropertyValueChanged += PropertyGrid1_PropertyValueChanged;

            Label2 = new System.Windows.Forms.Label();
            Label2.Parent = Panel11;
            Label2.Bounds = new Rectangle(1, 16, 68, 20);
            Label2.Text = "&Свойства:";

            TopLevel = true;
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            TreeNode DeletedTreeNode = TreeView1.SelectedNode;
            MenuItemEntry CurrentMenuItem1 = (MenuItemEntry)DeletedTreeNode.Tag;
            Menu CurrentMenuItem1Parent = CurrentMenuItem1.Parent;
            CurrentMenuItem1Parent.MenuItems.Remove(CurrentMenuItem1.M_MenuItem);

            try
            {
                DeletedTreeNode.Parent.Nodes.Remove(DeletedTreeNode);
            }
            catch
            {
                TreeView1.Nodes.Remove(DeletedTreeNode);
            }

            UpdateButtonsState();
        }

        private void ButtonMoveDown_Click(object sender, EventArgs e)
        {
            TreeNode TreeNode1 = TreeView1.SelectedNode;
            MenuItemEntry MenuItem1 = (MenuItemEntry)TreeNode1.Tag;
            int Index1 = TreeNode1.Index;
            if (TreeNode1.Parent != null)
            {
                TreeNode Parent = TreeNode1.Parent;
                int Count1 = Parent.Nodes.Count;
                Parent.Nodes.Remove(TreeNode1);
                MenuItemEntry MenuItem1Parent = (MenuItemEntry)Parent.Tag;
                MenuItem1Parent.MenuItems.Remove(MenuItem1.M_MenuItem);
                if (Index1 == Count1 - 1)
                {
                    if (Parent.Parent != null)
                    {
                        TreeNode Parent2 = Parent.Parent;
                        Parent2.Nodes.Insert(Parent.Index + 1, TreeNode1);
                        MenuItemEntry MenuItem1Parent2 = (MenuItemEntry)Parent2.Tag;
                        MenuItem1Parent2.MenuItems.Add(Parent.Index + 1, MenuItem1.M_MenuItem);
                    }
                    else
                    {
                        TreeView1.Nodes.Insert(Parent.Index + 1, TreeNode1);
                        MainMenu.MenuItems.Add(Parent.Index + 1, MenuItem1.M_MenuItem);
                    }
                }
                else
                {
                    TreeNode Next = Parent.Nodes[Index1];
                    Next.Nodes.Insert(0, TreeNode1);
                    MenuItemEntry MenuItem1Next = (MenuItemEntry)Next.Tag;
                    MenuItem1Next.MenuItems.Add(0, MenuItem1.M_MenuItem);
                }
            }
            else
            {
                TreeView1.Nodes.Remove(TreeNode1);
                MainMenu.MenuItems.Remove(MenuItem1.M_MenuItem);
                TreeNode Next = TreeView1.Nodes[Index1];
                Next.Nodes.Insert(0, TreeNode1);
                MenuItemEntry MenuItem1Next = (MenuItemEntry)Next.Tag;
                MenuItem1Next.MenuItems.Add(0, MenuItem1.M_MenuItem);
            }
            TreeView1.SelectedNode = TreeNode1;
        }

        private void ButtonMoveUp_Click(object sender, EventArgs e)
        {
            TreeNode TreeNode1 = TreeView1.SelectedNode;
            MenuItemEntry MenuItem1 = (MenuItemEntry)TreeNode1.Tag;
            int Index1 = TreeNode1.Index;
            if (TreeNode1.Parent != null)
            {
                TreeNode Parent = TreeNode1.Parent;
                Parent.Nodes.Remove(TreeNode1);
                MenuItemEntry MenuItem1Parent = (MenuItemEntry)Parent.Tag;
                MenuItem1Parent.MenuItems.Remove(MenuItem1.M_MenuItem);
                if (Index1 > 0)
                {
                    TreeNode Previos = Parent.Nodes[Index1 - 1];
                    Previos.Nodes.Add(TreeNode1);
                    MenuItemEntry MenuItem1Previos = (MenuItemEntry)Previos.Tag;
                    MenuItem1Previos.MenuItems.Add(MenuItem1.M_MenuItem);
                }
                else
                {
                    if (Parent.Parent != null)
                    {
                        TreeNode Parent2 = Parent.Parent;
                        Parent2.Nodes.Insert(Parent.Index, TreeNode1);
                        MenuItemEntry MenuItem1Parent2 = (MenuItemEntry)Parent2.Tag;
                        MenuItem1Parent2.MenuItems.Add(Parent.Index - 1, MenuItem1.M_MenuItem);
                    }
                    else
                    {
                        TreeView1.Nodes.Insert(Parent.Index, TreeNode1);
                        MainMenu.MenuItems.Add(Parent.Index - 1, MenuItem1.M_MenuItem);
                    }
                }
            }
            else
            {
                TreeView1.Nodes.Remove(TreeNode1);
                MainMenu.MenuItems.Remove(MenuItem1.M_MenuItem);
                TreeNode Previos = TreeView1.Nodes[Index1 - 1];
                Previos.Nodes.Add(TreeNode1);
                MenuItemEntry MenuItem1Previos = (MenuItemEntry)Previos.Tag;
                MenuItem1Previos.MenuItems.Add(MenuItem1.M_MenuItem);
            }
            TreeView1.SelectedNode = TreeNode1;
        }

        private void ButtonCollapse_Click(object sender, EventArgs e)
        {
            TreeView1.CollapseAll();
            UpdateButtonsState();
            TreeView1.SelectedNode = TreeView1.Nodes[0];
        }

        private void ButtonExpand_Click(object sender, EventArgs e)
        {
            TreeView1.ExpandAll();
            UpdateButtonsState();
            TreeView1.SelectedNode = TreeView1.Nodes[0];
        }

        private void PropertyGrid1_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            MenuItemEntry MenuItemEntry1 = (MenuItemEntry)((System.Windows.Forms.PropertyGrid)sender).SelectedObject;
            if (e.ChangedItem.Label == "Текст")
            {
                TreeView1.SelectedNode.Text = MenuItemEntry1.Text;
            }
            if (e.ChangedItem.Label == "Помечен")
            {
                if (e.ChangedItem.Value.ToString() == "True")
                {
                    if (MenuItemEntry1.Parent.GetType() == typeof(osfDesigner.MainMenu) || MenuItemEntry1.M_MenuItem.IsParent)
                    {
                        MessageBox.Show("Значение Истина допустимо только для элемента меню, не имеющего дочерних элементов и не принадлежащего к верхнему уровню.");
                        PropertyDescriptor pd = TypeDescriptor.GetProperties(MenuItemEntry1)["Checked"];
                        pd.SetValue(MenuItemEntry1, (bool)e.OldValue);
                    }
                }
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            MenuItemEntry CurrentMenuItem1 = (MenuItemEntry)e.Node.Tag;

            if (e.Node.Text.Contains("Сепаратор"))
            {
                CurrentMenuItem1.Hide = "Скрыть";
            }
            else
            {
                CurrentMenuItem1.Hide = "Показать";
            }
            UpdateButtonsState();
            PropertyGrid1.SelectedObject = e.Node.Tag;
        }

        private void FrmMenuItems_Load(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonAddRoot_Click(object sender, EventArgs e)
        {
            MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
            MenuItemEntry1.Name = OneScriptFormsDesigner.RevertMenuName(MainMenu);
            MenuItemEntry1.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry1.Name.Replace("ГлавноеМеню", ""), "Меню", null);
            MainMenu.MenuItems.Add(MenuItemEntry1.M_MenuItem);
            OneScriptFormsDesigner.AddToDictionary(MenuItemEntry1.M_MenuItem, MenuItemEntry1);
            TreeNode TreeNode1 = new TreeNode();
            TreeNode1.Tag = MenuItemEntry1;
            TreeNode1.Text = MenuItemEntry1.Text;
            TreeView1.Nodes.Add(TreeNode1);

            ButtonDelete.Enabled = true;
            ButtonExpand.Enabled = true;
            ButtonCollapse.Enabled = true;
            ButtonAddChild.Enabled = true;
            ButtonAddSeparator.Enabled = false;
            TreeView1.SelectedNode = TreeNode1;
            UpdateButtonsState();
            PropertyGrid1.SelectedObject = MenuItemEntry1;
            PropertyGrid1.Refresh();
            MenuItemEntry1.DefaultValues = OneScriptFormsDesigner.GetDefaultValues(MenuItemEntry1, PropertyGrid1);
            TreeView1.Focus();
        }

        private void ButtonAddChild_Click(object sender, EventArgs e)
        {
            MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
            MenuItemEntry1.Name = OneScriptFormsDesigner.RevertMenuName(MainMenu);
            MenuItemEntry1.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry1.Name.Replace("ГлавноеМеню", ""), "Меню", null);
            MenuItemEntry MenuItemParent = (MenuItemEntry)TreeView1.SelectedNode.Tag;
            MenuItemParent.MenuItems.Add(MenuItemEntry1.M_MenuItem);
            OneScriptFormsDesigner.AddToDictionary(MenuItemEntry1.M_MenuItem, MenuItemEntry1);
            TreeNode TreeNode1 = new TreeNode();
            TreeNode1.Tag = MenuItemEntry1;
            TreeNode1.Text = MenuItemEntry1.Text;
            TreeView1.SelectedNode.Nodes.Add(TreeNode1);

            // Свойство Checked у родителя нужно установить в false.
            if (MenuItemEntry1.Parent.GetType() != typeof(osfDesigner.MainMenu))
            {
                ((MenuItem)MenuItemEntry1.Parent).Checked = false;
            }

            TreeView1.SelectedNode.Expand();
            UpdateButtonsState();
            MenuItemEntry1.Hide = "Показать";
            PropertyGrid1.SelectedObject = MenuItemEntry1;
            PropertyGrid1.Refresh();
            MenuItemEntry1.DefaultValues = OneScriptFormsDesigner.GetDefaultValues(MenuItemEntry1, PropertyGrid1);
            MenuItemEntry1.Hide = "Скрыть";
            PropertyGrid1.SelectedObject = TreeView1.SelectedNode.Tag;
            TreeView1.Focus();
        }

        private void ButtonAddSeparator_Click(object sender, EventArgs e)
        {
            MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
            MenuItemEntry1.Name = OneScriptFormsDesigner.RevertSeparatorName(MainMenu);
            // Имя в виде тире не присваивать, заменять на тире только во время формирования сценария.
            MenuItemEntry1.Text = "Сепаратор" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry1.Name.Replace("ГлавноеМеню", ""), "Сепаратор", null);
            MenuItemEntry MenuItemParent = (MenuItemEntry)TreeView1.SelectedNode.Tag;
            MenuItemParent.MenuItems.Add(MenuItemEntry1.M_MenuItem);
            OneScriptFormsDesigner.AddToDictionary(MenuItemEntry1.M_MenuItem, MenuItemEntry1);

            TreeNode TreeNode1 = new TreeNode();
            TreeNode1.Tag = MenuItemEntry1;
            TreeNode1.Text = MenuItemEntry1.Text;
            TreeView1.SelectedNode.Nodes.Add(TreeNode1);

            // Свойство Checked у родителя нужно установить в false.
            if (MenuItemEntry1.Parent.GetType() != typeof(osfDesigner.MainMenu))
            {
                ((MenuItem)MenuItemEntry1.Parent).Checked = false;
            }

            TreeView1.SelectedNode.Expand();
            UpdateButtonsState();
            PropertyGrid1.SelectedObject = MenuItemEntry1;
            PropertyGrid1.Refresh();
            MenuItemEntry1.DefaultValues = OneScriptFormsDesigner.GetDefaultValues(MenuItemEntry1, PropertyGrid1);
            PropertyGrid1.SelectedObject = TreeView1.SelectedNode.Tag;
            TreeView1.Focus();
        }

        public void UpdateButtonsState()
        {
            if (TreeView1.Nodes.Count > 0)
            {
                ButtonDelete.Enabled = true;
                ButtonExpand.Enabled = true;
                ButtonCollapse.Enabled = true;
                ButtonAddChild.Enabled = true;
            }
            else
            {
                ButtonDelete.Enabled = false;
                ButtonExpand.Enabled = false;
                ButtonCollapse.Enabled = false;
                ButtonAddChild.Enabled = false;
                ButtonAddSeparator.Enabled = false;
            }
            try
            {
                if (TreeView1.SelectedNode.Text.Contains("Сепаратор"))
                {
                    ButtonAddSeparator.Enabled = false;
                    ButtonAddChild.Enabled = false;
                }
                else
                {
                    ButtonAddSeparator.Enabled = true;
                }
            }
            catch { }

            try
            {
                if (TreeView1.SelectedNode.Index == 0 && TreeView1.SelectedNode.Parent == null)
                {
                    if (TreeView1.Nodes.Count > 1)
                    {
                        ButtonMoveDown.Enabled = true;
                        ButtonMoveUp.Enabled = false;
                    }
                    else
                    {
                        ButtonMoveDown.Enabled = false;
                        ButtonMoveUp.Enabled = false;
                    }
                }
                else if (TreeView1.SelectedNode.Index == (TreeView1.Nodes.Count - 1) && TreeView1.SelectedNode.Parent == null)
                {
                    if (TreeView1.Nodes.Count > 1)
                    {
                        ButtonMoveDown.Enabled = false;
                        ButtonMoveUp.Enabled = true;
                    }
                    else
                    {
                        ButtonMoveDown.Enabled = false;
                        ButtonMoveUp.Enabled = false;
                    }
                }
                else
                {
                    ButtonMoveDown.Enabled = true;
                    ButtonMoveUp.Enabled = true;
                }
            }
            catch { }
        }

        private void FrmMenuItems_Closed(object sender, EventArgs e)
        {
            OneScriptFormsDesigner.SetDesignSurfaceState();
            _wfes.CloseDropDown();
        }
    }
}
