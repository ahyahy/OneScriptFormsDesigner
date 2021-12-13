using System;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;
using osfDesigner.Properties;

namespace osfDesigner
{
    public class PropValueConverter
    {
        //использовать свойство SplitPosition и для правильной пристыковки сплиттера
        //спрятать владельца сплиттера в свойство Tag, при его создании или изменении свойства Dock


        ////////string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
        ////////control.GetType().GetProperty(propertyName).SetValue(control, Int32.Parse(valProp));

        public static void SetPropValue(
            System.ComponentModel.Component component,
            string displayName, 
            string valProp, 
            System.Windows.Forms.Control parent = null)
        {
            //System.Windows.Forms.MessageBox.Show("control=" + control + Environment.NewLine +
            //    "displayName=" + displayName + Environment.NewLine +
            //    "component=" + component + Environment.NewLine +
            //    "valProp=" + valProp + Environment.NewLine +
            //    "");

            dynamic control = null;
            if (component.GetType().ToString() == "osfDesigner.ImageList" ||
                component.GetType().ToString() == "osfDesigner.MainMenu" ||
                component.GetType().ToString() == "osfDesigner.FolderBrowserDialog" ||
                component.GetType().ToString() == "osfDesigner.ColorDialog" ||
                component.GetType().ToString() == "osfDesigner.FontDialog" ||
                component.GetType().ToString() == "osfDesigner.OpenFileDialog" ||
                component.GetType().ToString() == "osfDesigner.SaveFileDialog" ||
                component.GetType().ToString() == "osfDesigner.NotifyIcon" ||
                component.GetType().ToString() == "osfDesigner.FileSystemWatcher" ||
                component.GetType().ToString() == "osfDesigner.ToolTip" ||
                component.GetType().ToString() == "osfDesigner.Timer")
            {
                control = component;
            }
            else
            {
                control = (System.Windows.Forms.Control)component;
            }

            if (displayName == "Родитель")
            {
                if (parent != null)
                {
                    control.Parent = parent;
                }
            }

            if (valProp == "Истина")
            {
                bool rez = true;

                string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                PropertyInfo pi = control.GetType().GetProperty(propertyName);
                pi.SetValue(control, rez);
            }
            if (valProp == "Ложь")
            {
                bool rez = false;

                string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                PropertyInfo pi = control.GetType().GetProperty(propertyName);
                pi.SetValue(control, rez);
            }
            if (displayName.Contains("ToolTip на"))
            {
                //Подсказка1.УстановитьПодсказку(Форма_0, "фор");
                string toolTipName = OneScriptFormsDesigner.ParseBetween(valProp, null, ".УстановитьПодсказку");
                System.Windows.Forms.ToolTip ToolTip1 = (System.Windows.Forms.ToolTip)OneScriptFormsDesigner.GetComponentByName(toolTipName);
                string caption = OneScriptFormsDesigner.ParseBetween(valProp, "\u0022", "\u0022);");
                ToolTip1.SetToolTip(control, caption);
                control.ToolTip[toolTipName] = caption;
            }
            if (displayName == "ЭлементыМеню")
            {
                //Меню0 = ГлавноеМеню1.ЭлементыМеню.Добавить(Ф.ЭлементМеню("Меню0"));
                //Меню1 = Меню0.ЭлементыМеню.Добавить(Ф.ЭлементМеню("Меню1"));
                if (valProp != null)
                {
                    osfDesigner.MainMenu MainMenu1 = (osfDesigner.MainMenu)component;
                    System.Windows.Forms.TreeView TreeView1 = MainMenu1.TreeView;
                    string Text = OneScriptFormsDesigner.ParseBetween(valProp, "(\u0022", "\u0022)");
                    string Name = OneScriptFormsDesigner.ParseBetween(valProp, null, "=");
                    string componentName = component.Site.Name;
                    if (valProp.Contains(componentName + "."))// создаем элемент главного меню
                    {
                        if (!Name.Contains("Сепаратор"))
                        {
                            MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                            MenuItemEntry1.DefaultValues = @"Доступность == Истина
Нажатие == 
Отображать == Истина
Переключатель == Ложь
Помечен == Ложь
ПорядокСлияния == 0
СочетаниеКлавиш == Отсутствие
Текст == 
ТипСлияния == Добавить
(Name) == ";
                            MenuItemEntry1.Text = Text;
                            MenuItemEntry1.Name = Name;
                            MainMenu1.MenuItems.Add(MenuItemEntry1.M_MenuItem);
                            OneScriptFormsDesigner.AddToHashtable(MenuItemEntry1.M_MenuItem, MenuItemEntry1);
                            System.Windows.Forms.TreeNode TreeNode1 = new System.Windows.Forms.TreeNode();
                            TreeNode1.Tag = MenuItemEntry1;
                            TreeNode1.Text = MenuItemEntry1.Text;
                            TreeView1.Nodes.Add(TreeNode1);
                            TreeView1.SelectedNode = TreeNode1;
                        }
                        else
                        {
                            MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                            MenuItemEntry1.DefaultValues = @"Доступность == Истина
Нажатие == 
Отображать == Истина
Переключатель == Ложь
Помечен == Ложь
ПорядокСлияния == 0
СочетаниеКлавиш == Отсутствие
Текст == 
ТипСлияния == Добавить
(Name) == ";
                            MenuItemEntry1.Name = Name;
                            //имя в виде тире не присваивать, заменять на тире только во время формирования сценария
                            MenuItemEntry1.Text = MenuItemEntry1.Name;
                            MainMenu1.MenuItems.Add(MenuItemEntry1.M_MenuItem);
                            OneScriptFormsDesigner.AddToHashtable(MenuItemEntry1.M_MenuItem, MenuItemEntry1);

                            System.Windows.Forms.TreeNode TreeNode1 = new System.Windows.Forms.TreeNode();
                            TreeNode1.Tag = MenuItemEntry1;
                            TreeNode1.Text = MenuItemEntry1.Name;
                            TreeView1.Nodes.Add(TreeNode1);
                            TreeView1.SelectedNode = TreeNode1;

                            // свойство Checked у родителя нужно установить в false
                            if (MenuItemEntry1.Parent.GetType() != typeof(osfDesigner.MainMenu))
                            {
                                ((MenuItem)MenuItemEntry1.Parent).Checked = false;
                            }
                            TreeView1.SelectedNode.Expand();
                        }
                    }
                    else// создаем элемент подменю
                    {
                        if (!Name.Contains("Сепаратор"))
                        {
                            MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                            MenuItemEntry1.DefaultValues = @"Доступность == Истина
Нажатие == 
Отображать == Истина
Переключатель == Ложь
Помечен == Ложь
ПорядокСлияния == 0
СочетаниеКлавиш == Отсутствие
Текст == 
ТипСлияния == Добавить
(Name) == ";
                            MenuItemEntry1.Text = Text;
                            MenuItemEntry1.Name = Name;

                            string nameNodeParent = OneScriptFormsDesigner.ParseBetween(valProp, "=", ".");
                            System.Windows.Forms.TreeNode SelectedNode = null;
                            SelectedNodeSearch(TreeView1, nameNodeParent, ref SelectedNode, null);
                            TreeView1.SelectedNode = SelectedNode;

                            MenuItemEntry MenuItemParent = (MenuItemEntry)TreeView1.SelectedNode.Tag;
                            MenuItemParent.MenuItems.Add(MenuItemEntry1.M_MenuItem);
                            OneScriptFormsDesigner.AddToHashtable(MenuItemEntry1.M_MenuItem, MenuItemEntry1);
                            System.Windows.Forms.TreeNode TreeNode1 = new System.Windows.Forms.TreeNode();
                            TreeNode1.Tag = MenuItemEntry1;
                            TreeNode1.Text = MenuItemEntry1.Text;
                            TreeView1.SelectedNode.Nodes.Add(TreeNode1);

                            // свойство Checked у родителя нужно установить в false
                            if (MenuItemEntry1.Parent.GetType() != typeof(osfDesigner.MainMenu))
                            {
                                ((MenuItem)MenuItemEntry1.Parent).Checked = false;
                            }
                            TreeView1.SelectedNode = TreeNode1;
                        }
                        else
                        {
                            MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                            MenuItemEntry1.DefaultValues = @"Доступность == Истина
Нажатие == 
Отображать == Истина
Переключатель == Ложь
Помечен == Ложь
ПорядокСлияния == 0
СочетаниеКлавиш == Отсутствие
Текст == 
ТипСлияния == Добавить
(Name) == ";
                            MenuItemEntry1.Name = Name;
                            OneScriptFormsDesigner.hashtableSeparatorName.Add(MenuItemEntry1, MenuItemEntry1.Name);
                            //имя в виде тире не присваивать, заменять на тире только во время формирования сценария
                            MenuItemEntry1.Text = MenuItemEntry1.Name;
                            string nameNodeParent = OneScriptFormsDesigner.ParseBetween(valProp, "=", ".");

                            System.Windows.Forms.TreeNode SelectedNode = null;
                            SelectedNodeSearch(TreeView1, nameNodeParent, ref SelectedNode, null);
                            TreeView1.SelectedNode = SelectedNode;

                            MenuItemEntry MenuItemParent = (MenuItemEntry)TreeView1.SelectedNode.Tag;
                            MenuItemParent.MenuItems.Add(MenuItemEntry1.M_MenuItem);
                            OneScriptFormsDesigner.AddToHashtable(MenuItemEntry1.M_MenuItem, MenuItemEntry1);

                            System.Windows.Forms.TreeNode TreeNode1 = new System.Windows.Forms.TreeNode();
                            TreeNode1.Tag = MenuItemEntry1;
                            TreeNode1.Text = MenuItemEntry1.Name;
                            TreeView1.SelectedNode.Nodes.Add(TreeNode1);

                            // свойство Checked у родителя нужно установить в false
                            if (MenuItemEntry1.Parent.GetType() != typeof(osfDesigner.MainMenu))
                            {
                                ((MenuItem)MenuItemEntry1.Parent).Checked = false;
                            }
                            TreeView1.SelectedNode.Expand();
                        }
                    }
                    return;
                }
            }
            //////////////// код для Меню не срабатывает. Выходим из положения меняя положение конструктора нужного главного меню в разделе конструкторов
            //////////////if (displayName == "Меню")
            //////////////{
            //////////////    System.ComponentModel.Design.IDesignerHost host = pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost();
            //////////////    System.ComponentModel.ComponentCollection ctrlsExisting = host.Container.Components;

            //////////////    System.ComponentModel.IComponent MainMenu1 = null;
            //////////////    foreach (System.ComponentModel.Component comp in ctrlsExisting)
            //////////////    {
            //////////////        if (comp.Site.Name == valProp)
            //////////////        {
            //////////////            MainMenu1 = (IComponent)comp;
            //////////////            break;
            //////////////        }
            //////////////    }
            //////////////    if (MainMenu1 != null)
            //////////////    {
            //////////////        string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
            //////////////        //PropertyInfo pi = control.GetType().GetProperty(propertyName);
            //////////////        PropertyInfo pi = control.GetType().BaseType.GetProperty(propertyName);
            //////////////        pi.SetValue(control, (System.Windows.Forms.MainMenu)MainMenu1);
            //////////////        //((Form)control).Menu = (System.Windows.Forms.MainMenu)MainMenu1;
            //////////////    }
            //////////////    return;
            //////////////}
            //            if (displayName == "ОбластьСсылки")
            //            {
            //                if (val != null)
            //                {
            //                    AddToScript(compName + "." + displayName + " = " + "Ф.ОбластьСсылки(" + compValue.Replace(";", ",") + ");");
            //                    return;
            //                }
            //            }
            //            if (displayName == "Подэлементы")
            //            {
            //                if (val != null)
            //                {
            //                    System.Windows.Forms.ListViewItem.ListViewSubItemCollection ListViewSubItemCollection1 = (System.Windows.Forms.ListViewItem.ListViewSubItemCollection)val.SubItems;
            //                    if (ListViewSubItemCollection1.Count > 0)
            //                    {
            //                        osfDesigner.ListViewSubItem ListViewSubItem1;
            //                        for (int i = 1; i < ListViewSubItemCollection1.Count; i++)// первый индекс должен быть 1, а не 0
            //                        {
            //                            ListViewSubItem1 = (osfDesigner.ListViewSubItem)ListViewSubItemCollection1[i];
            //                            AddToScript(ListViewSubItem1.Name + " = Ф.ПодэлементСпискаЭлементов();");
            //                            PropComponent(ListViewSubItem1);
            //                            AddToScript(compName + ".Подэлементы.Добавить(" + ListViewSubItem1.Name + ");");
            //                        }
            //                    }
            //                }
            //                return;
            //            }
            //            if (displayName == "Элементы")
            //            {
            //                if (val != null)
            //                {
            //                    if (val.GetType() == typeof(osfDesigner.ComboBox))
            //                    {
            //                        System.Windows.Forms.ComboBox.ObjectCollection ComboBoxObjectCollection1 = (System.Windows.Forms.ComboBox.ObjectCollection)val.Items;
            //                        if (ComboBoxObjectCollection1.Count > 0)
            //                        {
            //                            osfDesigner.ListItemComboBox ListItemComboBox1;
            //                            string strValue = "";
            //                            for (int i = 0; i < ComboBoxObjectCollection1.Count; i++)
            //                            {
            //                                ListItemComboBox1 = (osfDesigner.ListItemComboBox)ComboBoxObjectCollection1[i];
            //                                if (ListItemComboBox1.ValueType == osfDesigner.DataType.Строка)
            //                                {
            //                                    strValue = strValue + "    " + compName + ".Элементы.Добавить(Ф.ЭлементСписка(\u0022" + ListItemComboBox1.Text + "\u0022, \u0022" + ListItemComboBox1.Text + "\u0022));";
            //                                }
            //                                else if (ListItemComboBox1.ValueType == osfDesigner.DataType.Дата)
            //                                {
            //                                    DateTime DateTime1 = ListItemComboBox1.ValueDateTime;
            //                                    strValue = strValue + "    " + compName + ".Элементы.Добавить(Ф.ЭлементСписка(\u0022" + ListItemComboBox1.Text + "\u0022, " +
            //                                        "Дата(" +
            //                                        DateTime1.ToString("yyyy") + ", " +
            //                                        DateTime1.ToString("MM") + ", " +
            //                                        DateTime1.ToString("dd") + ", " +
            //                                        DateTime1.ToString("HH") + ", " +
            //                                        DateTime1.ToString("mm") + ", " +
            //                                        DateTime1.ToString("ss") + ")" + "));";
            //                                }
            //                                else if (ListItemComboBox1.ValueType == osfDesigner.DataType.Булево)
            //                                {
            //                                    strValue = strValue + "    " + compName + ".Элементы.Добавить(Ф.ЭлементСписка(\u0022" + ListItemComboBox1.Text + "\u0022, " + ListItemComboBox1.Text + "));";
            //                                }
            //                                else if (ListItemComboBox1.ValueType == osfDesigner.DataType.Число)
            //                                {
            //                                    strValue = strValue + "    " + compName + ".Элементы.Добавить(Ф.ЭлементСписка(\u0022" + ListItemComboBox1.Text + "\u0022, " + ListItemComboBox1.Text + "));";
            //                                }
            //                                if (i == 0)
            //                                {
            //                                    strValue = strValue.TrimStart(' ');
            //                                }
            //                                if (i < (ComboBoxObjectCollection1.Count - 1))
            //                                {
            //                                    strValue = strValue + Environment.NewLine;
            //                                }
            //                            }
            //                            AddToScript(strValue);
            //                        }
            //                    }
            //                    else if (val.GetType() == typeof(osfDesigner.ListBox))
            //                    {
            //                        System.Windows.Forms.ListBox.ObjectCollection ListBoxObjectCollection1 = (System.Windows.Forms.ListBox.ObjectCollection)val.Items;
            //                        if (ListBoxObjectCollection1.Count > 0)
            //                        {
            //                            osfDesigner.ListItemListBox ListItemListBox1;
            //                            string strValue = "";
            //                            for (int i = 0; i < ListBoxObjectCollection1.Count; i++)
            //                            {
            //                                ListItemListBox1 = (osfDesigner.ListItemListBox)ListBoxObjectCollection1[i];
            //                                if (ListItemListBox1.ValueType == osfDesigner.DataType.Строка)
            //                                {
            //                                    strValue = strValue + "    " + compName + ".Элементы.Добавить(Ф.ЭлементСписка(\u0022" + ListItemListBox1.Text + "\u0022, \u0022" + ListItemListBox1.Text + "\u0022));";
            //                                }
            //                                else if (ListItemListBox1.ValueType == osfDesigner.DataType.Дата)
            //                                {
            //                                    DateTime DateTime1 = ListItemListBox1.ValueDateTime;
            //                                    strValue = strValue + "    " + compName + ".Элементы.Добавить(Ф.ЭлементСписка(\u0022" + ListItemListBox1.Text + "\u0022, " +
            //                                        "Дата(" +
            //                                        DateTime1.ToString("yyyy") + ", " +
            //                                        DateTime1.ToString("MM") + ", " +
            //                                        DateTime1.ToString("dd") + ", " +
            //                                        DateTime1.ToString("HH") + ", " +
            //                                        DateTime1.ToString("mm") + ", " +
            //                                        DateTime1.ToString("ss") + ")" + "));";
            //                                }
            //                                else if (ListItemListBox1.ValueType == osfDesigner.DataType.Булево)
            //                                {
            //                                    strValue = strValue + "    " + compName + ".Элементы.Добавить(Ф.ЭлементСписка(\u0022" + ListItemListBox1.Text + "\u0022, " + ListItemListBox1.Text + "));";
            //                                }
            //                                else if (ListItemListBox1.ValueType == osfDesigner.DataType.Число)
            //                                {
            //                                    strValue = strValue + "    " + compName + ".Элементы.Добавить(Ф.ЭлементСписка(\u0022" + ListItemListBox1.Text + "\u0022, " + ListItemListBox1.Text + "));";
            //                                }
            //                                if (i == 0)
            //                                {
            //                                    strValue = strValue.TrimStart(' ');
            //                                }
            //                                if (i < (ListBoxObjectCollection1.Count - 1))
            //                                {
            //                                    strValue = strValue + Environment.NewLine;
            //                                }
            //                            }
            //                            AddToScript(strValue);
            //                        }
            //                    }
            //                    else if (val.GetType() == typeof(osfDesigner.ListView))
            //                    {
            //                        System.Windows.Forms.ListView.ListViewItemCollection ListViewItemCollection1 = (System.Windows.Forms.ListView.ListViewItemCollection)val.Items;
            //                        if (ListViewItemCollection1.Count > 0)
            //                        {
            //                            osfDesigner.ListViewItem ListViewItem1;
            //                            for (int i = 0; i < ListViewItemCollection1.Count; i++)
            //                            {
            //                                ListViewItem1 = (osfDesigner.ListViewItem)ListViewItemCollection1[i];
            //                                AddToScript(ListViewItem1.Name + " = Ф.ЭлементСпискаЭлементов();");
            //                                PropComponent(ListViewItem1);
            //                                AddToScript(compName + ".Элементы.Добавить(" + ListViewItem1.Name + ");");
            //                            }
            //                        }
            //                    }
            //                }
            //                return;
            //            }
            //            if (displayName == "Панели")
            //            {
            //                if (val != null)
            //                {
            //                    System.Windows.Forms.StatusBar.StatusBarPanelCollection StatusBarPanelCollection1 = (System.Windows.Forms.StatusBar.StatusBarPanelCollection)val.Panels;
            //                    if (StatusBarPanelCollection1.Count > 0)
            //                    {
            //                        osfDesigner.StatusBarPanel StatusBarPanel1;
            //                        for (int i = 0; i < StatusBarPanelCollection1.Count; i++)
            //                        {
            //                            StatusBarPanel1 = (osfDesigner.StatusBarPanel)StatusBarPanelCollection1[i];
            //                            AddToScript(StatusBarPanel1.Name + " = Ф.ПанельСтрокиСостояния();");
            //                            AddToScript(compName + ".Панели.Добавить(" + StatusBarPanel1.Name + ");");
            //                            PropComponent(StatusBarPanel1);
            //                        }
            //                    }
            //                }
            //                return;
            //            }
            //            if (displayName == "Колонки")
            //            {
            //                if (val != null)
            //                {
            //                    System.Windows.Forms.ListView.ColumnHeaderCollection ColumnHeaderCollection1 = (System.Windows.Forms.ListView.ColumnHeaderCollection)val.Columns;
            //                    if (ColumnHeaderCollection1.Count > 0)
            //                    {
            //                        osfDesigner.ColumnHeader ColumnHeader1;
            //                        for (int i = 0; i < ColumnHeaderCollection1.Count; i++)
            //                        {
            //                            ColumnHeader1 = (osfDesigner.ColumnHeader)ColumnHeaderCollection1[i];
            //                            AddToScript(ColumnHeader1.Name + " = Ф.Колонка();");
            //                            PropComponent(ColumnHeader1);
            //                            AddToScript(compName + ".Колонки.Добавить(" + ColumnHeader1.Name + ");");
            //                        }
            //                    }
            //                }
            //                return;
            //            }
            //            if (displayName == "СтилиКолонкиСеткиДанных")
            //            {
            //                if (val != null)
            //                {
            //                    System.Windows.Forms.GridColumnStylesCollection GridColumnStylesCollection1 = (System.Windows.Forms.GridColumnStylesCollection)val.GridColumnStyles;
            //                    if (GridColumnStylesCollection1.Count > 0)
            //                    {
            //                        for (int i = 0; i < GridColumnStylesCollection1.Count; i++)
            //                        {
            //                            dynamic Style1 = GridColumnStylesCollection1[i];
            //                            if (Style1.GetType() == typeof(osfDesigner.DataGridBoolColumn))
            //                            {
            //                                osfDesigner.DataGridBoolColumn GridColumnStyle1 = (osfDesigner.DataGridBoolColumn)Style1;
            //                                AddToScript(GridColumnStyle1.NameStyle + " = Ф.СтильКолонкиБулево();" + Environment.NewLine +
            //                                    "    " + GridColumnStyle1.NameStyle + ".ИмяОтображаемого = \u0022" + GridColumnStyle1.MappingName + "\u0022;");
            //                                PropComponent(GridColumnStyle1);
            //                                AddToScript(compName + ".СтилиКолонкиСеткиДанных.Добавить(" + GridColumnStyle1.NameStyle + ");");
            //                            }
            //                            else if (Style1.GetType() == typeof(osfDesigner.DataGridTextBoxColumn))
            //                            {
            //                                osfDesigner.DataGridTextBoxColumn GridColumnStyle1 = (osfDesigner.DataGridTextBoxColumn)Style1;
            //                                AddToScript(GridColumnStyle1.NameStyle + " = Ф.СтильКолонкиПолеВвода();" + Environment.NewLine +
            //                                    "    " + GridColumnStyle1.NameStyle + ".ИмяОтображаемого = \u0022" + GridColumnStyle1.MappingName + "\u0022;");
            //                                PropComponent(GridColumnStyle1);
            //                                AddToScript(compName + ".СтилиКолонкиСеткиДанных.Добавить(" + GridColumnStyle1.NameStyle + ");");
            //                            }
            //                            else if (Style1.GetType() == typeof(osfDesigner.DataGridComboBoxColumnStyle))
            //                            {
            //                                osfDesigner.DataGridComboBoxColumnStyle GridColumnStyle1 = (osfDesigner.DataGridComboBoxColumnStyle)Style1;
            //                                AddToScript(GridColumnStyle1.NameStyle + " = Ф.СтильКолонкиПолеВыбора();" + Environment.NewLine +
            //                                    "    " + GridColumnStyle1.NameStyle + ".ИмяОтображаемого = \u0022" + GridColumnStyle1.MappingName + "\u0022;");
            //                                PropComponent(GridColumnStyle1);
            //                                AddToScript(compName + ".СтилиКолонкиСеткиДанных.Добавить(" + GridColumnStyle1.NameStyle + ");");
            //                            }
            //                        }
            //                    }
            //                }
            //                return;
            //            }
            //            if (displayName == "СтилиТаблицы")
            //            {
            //                if (val != null)
            //                {
            //                    System.Windows.Forms.GridTableStylesCollection GridTableStylesCollection1 = (System.Windows.Forms.GridTableStylesCollection)val.TableStyles;
            //                    if (GridTableStylesCollection1.Count > 0)
            //                    {
            //                        osfDesigner.DataGridTableStyle DataGridTableStyle1;
            //                        for (int i = 0; i < GridTableStylesCollection1.Count; i++)
            //                        {
            //                            System.Windows.Forms.DataGridTableStyle OriginalObj = GridTableStylesCollection1[i];
            //                            DataGridTableStyle1 = OneScriptFormsDesigner.RevertSimilarObj(OriginalObj);
            //                            AddToScript(DataGridTableStyle1.NameStyle + " = Ф.СтильТаблицыСеткиДанных();" + Environment.NewLine +
            //                                "    " + DataGridTableStyle1.NameStyle + ".ИмяОтображаемого = \u0022" + DataGridTableStyle1.MappingName + "\u0022;" + Environment.NewLine +
            //                                "    " + compName + ".СтилиТаблицы.Добавить(" + DataGridTableStyle1.NameStyle + ");");
            //                            PropComponent(DataGridTableStyle1);
            //                        }
            //                    }
            //                }
            //                return;
            //            }
            //            if (displayName == "Кнопки")
            //            {
            //                if (val != null)
            //                {
            //                    System.Windows.Forms.ToolBar.ToolBarButtonCollection ToolBarButtonCollection1 = (System.Windows.Forms.ToolBar.ToolBarButtonCollection)val.Buttons;
            //                    if (ToolBarButtonCollection1.Count > 0)
            //                    {
            //                        osfDesigner.ToolBarButton ToolBarButton1;
            //                        for (int i = 0; i < ToolBarButtonCollection1.Count; i++)
            //                        {
            //                            ToolBarButton1 = (osfDesigner.ToolBarButton)ToolBarButtonCollection1[i].Tag;
            //                            AddToScript(ToolBarButton1.Name + " = " + compName + ".Кнопки.Добавить(Ф.КнопкаПанелиИнструментов());");
            //                            PropComponent(ToolBarButton1);
            //                        }
            //                    }
            //                }
            //                return;
            //            }



            if (displayName == "Узлы")
            {
                //Узел0 = Дерево1.Узлы.Добавить("Узел0");
                //Узел1 = Узел0.Узлы.Добавить("Узел1");

                string nameNode = OneScriptFormsDesigner.ParseBetween(valProp, "Добавить(\u0022", "\u0022)");
                if (nameNode != null)// нужно добавить узел
                {
                    string nameNodeParent = OneScriptFormsDesigner.ParseBetween(valProp, "=", ".");
                    if (nameNodeParent == control.Name)
                    {
                        osfDesigner.MyTreeNode MyTreeNode1 = new MyTreeNode();
                        MyTreeNode1.DefaultValues = @"Текст == 
ШрифтУзла == 
Индекс == 0
ИндексВыбранногоИзображения == -1
ИндексИзображения == -1
Помечен == Ложь
ПолныйПуть == 
(Name) == ";
                        MyTreeNode1.Text = nameNode;
                        MyTreeNode1.Name = nameNode;
                        ((osfDesigner.TreeView)control).Nodes.Add(MyTreeNode1);
                    }
                    else
                    {
                        osfDesigner.MyTreeNode MyTreeNode2 = null;
                        NodeSearch(((osfDesigner.TreeView)control), nameNodeParent, ref MyTreeNode2, null);
                        osfDesigner.MyTreeNode MyTreeNode1 = new MyTreeNode();
                        MyTreeNode1.DefaultValues = @"Текст == 
ШрифтУзла == 
Индекс == 0
ИндексВыбранногоИзображения == -1
ИндексИзображения == -1
Помечен == Ложь
ПолныйПуть == 
(Name) == ";
                        MyTreeNode1.Text = nameNode;
                        MyTreeNode1.Name = nameNode;
                        MyTreeNode2.Nodes.Add(MyTreeNode1);
                    }
                }
                else// нужно обработать свойство узла
                {
                    //Узел1.Текст = "Узел111";
                    // найдем узел и установим для него свойство
                    string nameNode2 = OneScriptFormsDesigner.ParseBetween(valProp, null, ".");
                    osfDesigner.MyTreeNode MyTreeNode3 = null;
                    NodeSearch(((osfDesigner.TreeView)control), nameNode2, ref MyTreeNode3, null);

                    string nodeDisplayName = OneScriptFormsDesigner.ParseBetween(valProp, ".", "=");
                    string strNodePropertyValue = OneScriptFormsDesigner.ParseBetween(valProp, "=", ";");

                    SetNodePropValue(MyTreeNode3, nodeDisplayName, strNodePropertyValue);
                }
            }

            if (displayName == "Шрифт" || displayName == "ШрифтУзла" || displayName == "ШрифтЗаголовков")
            {
                //Ф.Шрифт("Microsoft Sans Serif", 10.2, Ф.СтильШрифта.Курсив + Ф.СтильШрифта.Подчеркнутый + Ф.СтильШрифта.Зачеркнутый)

                string[] result = valProp.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                string FontName = control.Parent.Font.Name;
                float FontSize = control.Parent.Font.Size;
                int Style1 = 0;

                for (int i = 0; i < result.Length; i++)
                {
                    if (i == 0)
                    {
                        try
                        {
                            FontName = OneScriptFormsDesigner.ParseBetween(result[0], "\u0022", null).Replace("\u0022", "").Trim();
                        }
                        catch { }
                    }
                    if (i == 1)
                    {
                        FontSize = control.Parent.Font.Size;
                        try
                        {

                            FontSize = Single.Parse(result[1].Trim());
                        }
                        catch
                        {
                            try
                            {
                                FontSize = Single.Parse(result[1].Trim().Replace(".", ","));
                            }
                            catch { }
                        }
                    }
                    if (i == 2)
                    {
                        if (result[2].Contains("Жирный"))
                        {
                            Style1 = Style1 + (int)System.Drawing.FontStyle.Bold;
                        }
                        if (result[2].Contains("Зачеркнутый"))
                        {
                            Style1 = Style1 + (int)System.Drawing.FontStyle.Strikeout;
                        }
                        if (result[2].Contains("Курсив"))
                        {
                            Style1 = Style1 + (int)System.Drawing.FontStyle.Italic;
                        }
                        if (result[2].Contains("Подчеркнутый"))
                        {
                            Style1 = Style1 + (int)System.Drawing.FontStyle.Underline;
                        }
                    }
                }
                control.Font = new System.Drawing.Font(FontName, FontSize, (System.Drawing.FontStyle)Style1);
            }
            if (displayName == "ВыделенныеДаты")
            {
                //9998,12,01,00,00,00
                osfDesigner.MonthCalendar MonthCalendar1 = (osfDesigner.MonthCalendar)control;
                string[] result = valProp.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                DateTime rez = new DateTime();
                for (int i = 0; i < result.Length; i++)
                {
                    if (i == 0)
                    {
                        rez = rez.AddYears(Int32.Parse(result[0]) - 1);
                    }
                    if (i == 1)
                    {
                        rez = rez.AddMonths(Int32.Parse(result[1]) - 1);
                    }
                    if (i == 2)
                    {
                        rez = rez.AddDays(Int32.Parse(result[2]) - 1);
                    }
                    if (i == 3)
                    {
                        rez = rez.AddHours(Int32.Parse(result[3]));
                    }
                    if (i == 4)
                    {
                        rez = rez.AddMinutes(Int32.Parse(result[4]));
                    }
                    if (i == 5)
                    {
                        rez = rez.AddSeconds(Int32.Parse(result[5]));
                    }
                }

                osfDesigner.MyBoldedDatesList MyBoldedDatesList1 = MonthCalendar1.BoldedDates_osf;
                MyBoldedDatesList1.Add(new DateEntry(rez));

                int count1 = MyBoldedDatesList1.Count;
                DateTime[] DateTime1 = new DateTime[count1];
                for (int i = 0; i < MyBoldedDatesList1.Count; i++)
                {
                    DateTime1[i] = MyBoldedDatesList1[i].Value;
                }
                MonthCalendar1.BoldedDates = DateTime1;
            }
            if (displayName == "ЕжегодныеДаты")
            {
                //9998,12,01,00,00,00
                osfDesigner.MonthCalendar MonthCalendar1 = (osfDesigner.MonthCalendar)control;
                string[] result = valProp.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                DateTime rez = new DateTime();
                for (int i = 0; i < result.Length; i++)
                {
                    if (i == 0)
                    {
                        rez = rez.AddYears(Int32.Parse(result[0]) - 1);
                    }
                    if (i == 1)
                    {
                        rez = rez.AddMonths(Int32.Parse(result[1]) - 1);
                    }
                    if (i == 2)
                    {
                        rez = rez.AddDays(Int32.Parse(result[2]) - 1);
                    }
                    if (i == 3)
                    {
                        rez = rez.AddHours(Int32.Parse(result[3]));
                    }
                    if (i == 4)
                    {
                        rez = rez.AddMinutes(Int32.Parse(result[4]));
                    }
                    if (i == 5)
                    {
                        rez = rez.AddSeconds(Int32.Parse(result[5]));
                    }
                }

                osfDesigner.MyAnnuallyBoldedDatesList MyAnnuallyBoldedDatesList1 = MonthCalendar1.AnnuallyBoldedDates_osf;
                MyAnnuallyBoldedDatesList1.Add(new DateEntry(rez));

                int count1 = MyAnnuallyBoldedDatesList1.Count;
                DateTime[] DateTime1 = new DateTime[count1];
                for (int i = 0; i < MyAnnuallyBoldedDatesList1.Count; i++)
                {
                    DateTime1[i] = MyAnnuallyBoldedDatesList1[i].Value;
                }
                MonthCalendar1.AnnuallyBoldedDates = DateTime1;
            }
            if (displayName == "ЕжемесячныеДаты")
            {
                //9998,12,01,00,00,00
                osfDesigner.MonthCalendar MonthCalendar1 = (osfDesigner.MonthCalendar)control;
                string[] result = valProp.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                DateTime rez = new DateTime();
                for (int i = 0; i < result.Length; i++)
                {
                    if (i == 0)
                    {
                        rez = rez.AddYears(Int32.Parse(result[0]) - 1);
                    }
                    if (i == 1)
                    {
                        rez = rez.AddMonths(Int32.Parse(result[1]) - 1);
                    }
                    if (i == 2)
                    {
                        rez = rez.AddDays(Int32.Parse(result[2]) - 1);
                    }
                    if (i == 3)
                    {
                        rez = rez.AddHours(Int32.Parse(result[3]));
                    }
                    if (i == 4)
                    {
                        rez = rez.AddMinutes(Int32.Parse(result[4]));
                    }
                    if (i == 5)
                    {
                        rez = rez.AddSeconds(Int32.Parse(result[5]));
                    }
                }

                osfDesigner.MyMonthlyBoldedDatesList MyMonthlyBoldedDatesList1 = MonthCalendar1.MonthlyBoldedDates_osf;
                MyMonthlyBoldedDatesList1.Add(new DateEntry(rez));

                int count1 = MyMonthlyBoldedDatesList1.Count;
                DateTime[] DateTime1 = new DateTime[count1];
                for (int i = 0; i < MyMonthlyBoldedDatesList1.Count; i++)
                {
                    DateTime1[i] = MyMonthlyBoldedDatesList1[i].Value;
                }
                MonthCalendar1.MonthlyBoldedDates = DateTime1;
            }
            if (displayName == "Изображения")
            {
                ImageEntry ImageEntry1 = new ImageEntry();
                System.Drawing.Bitmap Bitmap1 = new System.Drawing.Bitmap(valProp);
                Bitmap1.Tag = valProp;
                ImageEntry1.Image = Bitmap1;
                ImageEntry1.Path = valProp;
                ImageEntry1.FileName = valProp;
                ((dynamic)component).Images.Add(ImageEntry1);
                ((dynamic)component).OriginalObj.Images.Add(ImageEntry1.Image);
            }
            if (displayName == "СписокИзображений" || displayName == "СписокБольшихИзображений" || displayName == "СписокМаленькихИзображений")
            {
                System.ComponentModel.Design.IDesignerHost host = pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost();
                System.ComponentModel.ComponentCollection ctrlsExisting = host.Container.Components;

                System.Windows.Forms.ImageList ImageList1 = null;
                foreach (System.ComponentModel.Component comp in ctrlsExisting)
                {
                    if (comp.Site.Name == valProp)
                    {
                        ImageList1 = (System.Windows.Forms.ImageList)comp;
                        break;
                    }
                }

                if (ImageList1 != null)
                {
                    string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                    PropertyInfo pi = control.GetType().GetProperty(propertyName);
                    pi.SetValue(control, ImageList1);
                }
            }
            if (displayName == "Изображение" || displayName == "ФоновоеИзображение")
            {
                //Ф.Картинка("C:\444\Pic\maslenica10.JPG")

                System.Drawing.Bitmap Bitmap = null;
                string rez = valProp.Replace("\u0022", "");
                rez = OneScriptFormsDesigner.ParseBetween(rez, "(", ")");
                try
                {
                    Bitmap = new System.Drawing.Bitmap(rez);
                }
                catch { }

                if (Bitmap != null)
                {
                    ImageEntry ImageEntry1 = new ImageEntry();
                    ImageEntry1.Image = Bitmap;
                    ImageEntry1.Path = rez;
                    Bitmap.Tag = rez;

                    string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                    PropertyInfo pi = control.GetType().BaseType.GetProperty(propertyName);
                    pi.SetValue(control, Bitmap);
                }
            }
            if (displayName == "ВыделенныйДиапазон")
            {
                //Ф.ВыделенныйДиапазон(Дата(2021, 11, 01, 00, 00, 00), Дата(2021, 11, 04, 00, 00, 00))
                DateTime rez1 = new DateTime();
                DateTime rez2 = new DateTime();

                string[] result1 = OneScriptFormsDesigner.ParseBetween(valProp, "Ф.ВыделенныйДиапазон(Дата(", "),Дата(").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string[] result2 = OneScriptFormsDesigner.ParseBetween(valProp, "),Дата(", "))").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < result1.Length; i++)
                {
                    if (i == 0)
                    {
                        rez1 = rez1.AddYears(Int32.Parse(result1[0]) - 1);
                    }
                    if (i == 1)
                    {
                        rez1 = rez1.AddMonths(Int32.Parse(result1[1]) - 1);
                    }
                    if (i == 2)
                    {
                        rez1 = rez1.AddDays(Int32.Parse(result1[2]) - 1);
                    }
                    if (i == 3)
                    {
                        rez1 = rez1.AddHours(Int32.Parse(result1[3]));
                    }
                    if (i == 4)
                    {
                        rez1 = rez1.AddMinutes(Int32.Parse(result1[4]));
                    }
                    if (i == 5)
                    {
                        rez1 = rez1.AddSeconds(Int32.Parse(result1[5]));
                    }
                }

                for (int i = 0; i < result2.Length; i++)
                {
                    if (i == 0)
                    {
                        rez2 = rez2.AddYears(Int32.Parse(result2[0]) - 1);
                    }
                    if (i == 1)
                    {
                        rez2 = rez2.AddMonths(Int32.Parse(result2[1]) - 1);
                    }
                    if (i == 2)
                    {
                        rez2 = rez2.AddDays(Int32.Parse(result2[2]) - 1);
                    }
                    if (i == 3)
                    {
                        rez2 = rez2.AddHours(Int32.Parse(result2[3]));
                    }
                    if (i == 4)
                    {
                        rez2 = rez2.AddMinutes(Int32.Parse(result2[4]));
                    }
                    if (i == 5)
                    {
                        rez2 = rez2.AddSeconds(Int32.Parse(result2[5]));
                    }
                }

                System.Windows.Forms.SelectionRange sr = new System.Windows.Forms.SelectionRange(rez1, rez2);

                string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                PropertyInfo pi = control.GetType().GetProperty(propertyName);
                pi.SetValue(control, sr);
            }
            //если это цвет
            if (displayName == "ОсновнойЦвет" ||
                displayName == "ОсновнойЦветЗаголовков" ||
                displayName == "ПрозрачныйЦвет" ||
                displayName == "Цвет" ||
                displayName == "ЦветАктивнойСсылки" ||
                displayName == "ЦветПосещеннойСсылки" ||
                displayName == "ЦветСетки" ||
                displayName == "ЦветСсылки" ||
                displayName == "ЦветФона" ||
                displayName == "ЦветФонаЗаголовка" ||
                displayName == "ЦветФонаЗаголовков" ||
                displayName == "ЦветФонаНечетныхСтрок" ||
                displayName == "ЦветФонаСеткиДанных")
            {
                //Ф.Цвет("РабочийСтол")
                //Ф.Цвет(192, 255, 255)

                System.Drawing.Color Color1 = System.Drawing.Color.Empty;
                string strColor = valProp.Replace("\u0022", "");
                strColor = OneScriptFormsDesigner.ParseBetween(strColor, "(", ")");
                if (strColor.Contains(","))
                {
                    string[] result = strColor.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (result.Length == 3)
                    {
                        Color1 = System.Drawing.Color.FromArgb(Int32.Parse(result[0]), Int32.Parse(result[1]), Int32.Parse(result[2]));
                    }
                }
                else
                {
                    Color1 = System.Drawing.Color.FromName(osfDesigner.OneScriptFormsDesigner.colors[strColor]);
                }
                if (Color1 != System.Drawing.Color.Empty)
                {
                    string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                    PropertyInfo pi = control.GetType().GetProperty(propertyName);
                    pi.SetValue(control, Color1);
                }
            }
            //если valProp это контрол
            if (displayName == "КнопкаОтмена" ||
                displayName == "КнопкаПринять" ||
                displayName == "ВыбранныйОбъект")
            {
                System.ComponentModel.Design.IDesignerHost host = pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost();
                System.ComponentModel.ComponentCollection ctrlsExisting = host.Container.Components;

                System.Windows.Forms.IButtonControl IButtonControl1 = null;
                foreach (System.ComponentModel.Component comp in ctrlsExisting)
                {
                    if (comp.Site.Name == valProp)
                    {
                        IButtonControl1 = (System.Windows.Forms.IButtonControl)comp;
                        break;
                    }
                }

                if (IButtonControl1 != null)
                {
                    string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                    PropertyInfo pi = control.GetType().GetProperty(propertyName);
                    pi.SetValue(control, IButtonControl1);
                }
            }
            //если valProp это событие
            if (displayName == "ВыбранныйЭлементСеткиИзменен" ||
                displayName == "ВыделениеИзменено" ||
                displayName == "ДатаВыбрана" ||
                displayName == "ДатаИзменена" ||
                displayName == "ДвойноеНажатие" ||
                displayName == "Закрыта" ||
                displayName == "ЗначениеИзменено" ||
                displayName == "ЗначениеСвойстваИзменено" ||
                displayName == "ИндексВыбранногоИзменен" ||
                displayName == "КлавишаВверх" ||
                displayName == "КлавишаВниз" ||
                displayName == "КлавишаНажата" ||
                displayName == "КолонкаНажатие" ||
                displayName == "МышьНадЭлементом" ||
                displayName == "МышьПокинулаЭлемент" ||
                displayName == "Нажатие" ||
                displayName == "ПередРазвертыванием" ||
                displayName == "ПередРедактированиемНадписи" ||
                displayName == "ПоложениеИзменено" ||
                displayName == "ПометкаИзменена" ||
                displayName == "ПослеВыбора" ||
                displayName == "ПослеРедактированияНадписи" ||
                displayName == "ПриАктивизации" ||
                displayName == "ПриАктивизацииЭлемента" ||
                displayName == "ПриВходе" ||
                displayName == "ПриВыпадении" ||
                displayName == "ПриДеактивации" ||
                displayName == "ПриЗагрузке" ||
                displayName == "ПриЗадержкеМыши" ||
                displayName == "ПриЗакрытии" ||
                displayName == "ПриИзменении" ||
                displayName == "ПриНажатииКнопки" ||
                displayName == "ПриНажатииКнопкиМыши" ||
                displayName == "ПриОтпусканииМыши" ||
                displayName == "ПриПереименовании" ||
                displayName == "ПриПеремещении" ||
                displayName == "ПриПеремещенииМыши" ||
                displayName == "ПриПерерисовке" ||
                displayName == "ПриПотереФокуса" ||
                displayName == "ПриПрокручивании" ||
                displayName == "ПриСоздании" ||
                displayName == "ПриСрабатыванииТаймера" ||
                displayName == "ПриУдалении" ||
                displayName == "ПриУходе" ||
                displayName == "РазмерИзменен" ||
                displayName == "СсылкаНажата" ||
                displayName == "ТекстИзменен" ||
                displayName == "ТекущаяЯчейкаИзменена" ||
                displayName == "ЭлементДобавлен" ||
                displayName == "ЭлементПомечен" ||
                displayName == "ЭлементУдален")
            {
                //"ПриЗагр()"
                string rez = valProp.Replace("\u0022", "").Replace("(", "").Replace(")", "");

                string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                PropertyInfo pi = control.GetType().GetProperty(propertyName);
                pi.SetValue(control, rez);
            }
            //если valProp это строка
            if (displayName == "ВыбранныйПуть" ||
                displayName == "Заголовок" ||
                displayName == "ИмяОтображаемого" ||
                displayName == "ИмяСтиля" ||
                displayName == "ИмяФайла" ||
                displayName == "НачальныйКаталог" ||
                displayName == "Описание" ||
                displayName == "ПолныйПуть" ||
                displayName == "ПользовательскийФормат" ||
                displayName == "Путь" ||
                displayName == "РазделительПути" ||
                displayName == "РасширениеПоУмолчанию" ||
                displayName == "СимволПароля" ||
                displayName == "Текст" ||
                displayName == "ТекстЗаголовка" ||
                displayName == "ТекстПодсказки" ||
                displayName == "Фильтр")
            {
                if (valProp.Contains("Ф.Окружение().НоваяСтрока"))
                {
                    valProp = valProp.Replace("\u0022", "");
                    valProp = valProp.Replace("+Ф.Окружение().НоваяСтрока+", Environment.NewLine);
                }
                else
                {
                    valProp = valProp.Replace("\u0022", "");
                }
                string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                control.GetType().GetProperty(propertyName).SetValue(control, valProp);
            }
            //если valProp это число
            if (displayName == "АвтоЗадержка" ||
                displayName == "АвтоЗадержкаПоказа" ||
                displayName == "БольшоеИзменение" ||
                displayName == "ВысотаЭлемента" ||
                displayName == "ГоризонтальнаяМера" ||
                displayName == "ЗадержкаОчередногоПоказа" ||
                displayName == "ЗадержкаПоявления" ||
                (displayName == "Значение" && control.GetType() == typeof(osfDesigner.HProgressBar)) ||
                (displayName == "Значение" && control.GetType() == typeof(osfDesigner.VProgressBar)) ||
                (displayName == "Значение" && control.GetType() == typeof(osfDesigner.HScrollBar)) ||
                (displayName == "Значение" && control.GetType() == typeof(osfDesigner.VScrollBar)) ||
                (displayName == "Значение" && control.GetType() == typeof(osfDesigner.NumericUpDown)) ||
                displayName == "Индекс" ||
                displayName == "ИндексВыбранногоИзображения" ||
                displayName == "ИндексИзображения" ||
                displayName == "ИндексФильтра" ||
                displayName == "Интервал" ||
                displayName == "МаксимальнаяДлина" ||
                displayName == "Максимум" ||
                displayName == "МаксимумВыбранных" ||
                displayName == "МаксимумЭлементов" ||
                displayName == "МалоеИзменение" ||
                displayName == "Масштаб" ||
                displayName == "МинимальнаяШирина" ||
                displayName == "МинимальноеРасстояние" ||
                (displayName == "МинимальныйРазмер" && control.GetType() == typeof(osfDesigner.Splitter)) ||
                displayName == "Минимум" ||
                displayName == "Отступ" ||
                displayName == "ОтступМаркера" ||
                displayName == "ПорядокОбхода" ||
                displayName == "ПорядокСлияния" ||
                displayName == "ПравоеОграничение" ||
                displayName == "ПредпочтительнаяВысотаСтрок" ||
                displayName == "ПредпочтительнаяШиринаСтолбцов" ||
                displayName == "Разрядность" ||
                displayName == "Увеличение" ||
                displayName == "Ширина" ||
                displayName == "ШиринаВыпадающегоСписка" ||
                displayName == "ШиринаЗаголовковСтрок" ||
                displayName == "ШиринаКолонки")
            {
                string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                control.GetType().GetProperty(propertyName).SetValue(control, Int32.Parse(valProp));
            }
            //если valProp это Размер
            if (displayName == "МаксимальныйРазмер" ||
                displayName == "МинимальныйРазмер" ||
                displayName == "Размер" ||
                displayName == "РазмерИзображения" ||
                displayName == "РазмерКнопки" ||
                displayName == "РазмерПоляАвтоПрокрутки" ||
                displayName == "РазмерЭлемента")
            {
                //Ф.Размер(670, 600)
                System.Drawing.Size Size1 = new System.Drawing.Size();
                Size1.Width = Int32.Parse(OneScriptFormsDesigner.ParseBetween(valProp, "Ф.Размер(", ","));
                Size1.Height = Int32.Parse(OneScriptFormsDesigner.ParseBetween(valProp, ",", ")"));

                string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                control.GetType().GetProperty(propertyName).SetValue(control, Size1);

                ////////////////////////////////if (control != null)
                ////////////////////////////////{
                ////////////////////////////////    string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                ////////////////////////////////    control.GetType().GetProperty(propertyName).SetValue(control, Size1);
                ////////////////////////////////}
                ////////////////////////////////else
                ////////////////////////////////{
                ////////////////////////////////    string propertyName = OneScriptFormsDesigner.GetPropName(component, displayName);
                ////////////////////////////////    component.GetType().GetProperty(propertyName).SetValue(component, Size1);
                ////////////////////////////////}
            }
            //если valProp это Точка
            if (displayName == "Положение")
            {
                //Ф.Точка(15, 15)
                System.Drawing.Point Point1 = new System.Drawing.Point();
                Point1.X = Int32.Parse(OneScriptFormsDesigner.ParseBetween(valProp, "Ф.Точка(", ","));
                Point1.Y = Int32.Parse(OneScriptFormsDesigner.ParseBetween(valProp, ",", ")"));

                try
                {
                    string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                    PropertyInfo pi = control.GetType().GetProperty(propertyName);
                    pi.SetValue(control, Point1);
                }
                catch
                {
                    string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                    PropertyInfo pi = control.GetType().BaseType.GetProperty(propertyName);
                    pi.SetValue(control, Point1);
                }
            }
            if (displayName == "Курсор")
            {
                //Ф.Курсоры().Луч
                string propNameRu = OneScriptFormsDesigner.ParseBetween(valProp, "Ф.Курсоры().", null);
                System.Windows.Forms.Cursor cursor = null;

                PropertyInfo[] myPropertyInfo = typeof(System.Windows.Forms.Cursors).GetProperties();
                for (int i = 0; i < myPropertyInfo.Length; i++)
                {
                    if (propNameRu == OneScriptFormsDesigner.namesCursorEnRu[myPropertyInfo[i].Name])
                    {
                        cursor = OneScriptFormsDesigner.namesCursorRuEn[propNameRu];
                        break;
                    }
                }

                string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                PropertyInfo pi = control.GetType().BaseType.GetProperty(propertyName);
                pi.SetValue(control, cursor);
            }

            //если valProp это Перечисление
            if (displayName == "АвтоРазмер" ||
                displayName == "Активация" ||
                displayName == "Выравнивание" ||
                displayName == "ВыравниваниеИзображения" ||
                displayName == "ВыравниваниеПометки" ||
                displayName == "ВыравниваниеПриРаскрытии" ||
                displayName == "ВыравниваниеТекста" ||
                displayName == "ВыравниваниеТекста" ||
                displayName == "ВыравниваниеТекста" ||
                displayName == "ГлубинаЦвета" ||
                displayName == "КорневойКаталог" ||
                displayName == "НачальноеПоложение" ||
                displayName == "Оформление" ||
                displayName == "Оформление" ||
                displayName == "ПервыйДеньНедели" ||
                displayName == "ПлоскийСтиль" ||
                displayName == "ПоведениеСсылки" ||
                displayName == "ПолосыПрокрутки" ||
                displayName == "РазмещениеФоновогоИзображения" ||
                displayName == "РегистрСимволов" ||
                displayName == "РежимВыбора" ||
                displayName == "РежимМасштабирования" ||
                displayName == "РежимМасштабирования" ||
                displayName == "РежимОтображения" ||
                displayName == "РежимРисования" ||
                displayName == "РезультатДиалога" ||
                displayName == "Сортировка" ||
                displayName == "СортировкаСвойств" ||
                displayName == "СостояниеФлажка" ||
                displayName == "СочетаниеКлавиш" ||
                displayName == "Стиль" ||
                displayName == "СтильВыпадающегоСписка" ||
                displayName == "СтильГраницы" ||
                displayName == "СтильГраницыФормы" ||
                displayName == "СтильЗаголовка" ||
                displayName == "Стыковка" ||
                displayName == "ТипСлияния" ||
                displayName == "ФильтрУведомлений" ||
                displayName == "Формат" ||
                displayName == "Якорь")
            {
                ////////Ф.СтилиПривязки.Верх + Ф.СтилиПривязки.Лево
                string enumName = OneScriptFormsDesigner.ParseBetween(valProp, "Ф.", ".");
                string type_Name = "osfDesigner." + OneScriptFormsDesigner.namesEnum[enumName];
                System.Type enumType = Type.GetType(type_Name);
                var names = Enum.GetNames(enumType);
                int rez = 0;
                foreach (var name in names)
                {
                    if (valProp.Contains(name))
                    {
                        rez = rez + (int)Enum.Parse(enumType, name);
                    }
                    var num = (int)Enum.Parse(enumType, name);
                }

                try
                {
                    string propertyName = OneScriptFormsDesigner.GetPropName(component, displayName);
                    PropertyInfo pi = component.GetType().GetProperty(propertyName);
                    pi.SetValue(component, rez);
                }
                catch
                {
                    string propertyName = OneScriptFormsDesigner.GetPropName(component, displayName);
                    PropertyInfo pi = component.GetType().BaseType.GetProperty(propertyName);
                    pi.SetValue(component, rez);
                }


                ////////////////////if (control != null)
                ////////////////////{
                ////////////////////    string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                ////////////////////    PropertyInfo pi = control.GetType().BaseType.GetProperty(propertyName);
                ////////////////////    pi.SetValue(control, rez);
                ////////////////////}
                ////////////////////else
                ////////////////////{
                ////////////////////    try
                ////////////////////    {
                ////////////////////        string propertyName = OneScriptFormsDesigner.GetPropName(component, displayName);
                ////////////////////        PropertyInfo pi = component.GetType().GetProperty(propertyName);
                ////////////////////        pi.SetValue(component, rez);
                ////////////////////    }
                ////////////////////    catch
                ////////////////////    {
                ////////////////////        string propertyName = OneScriptFormsDesigner.GetPropName(component, displayName);
                ////////////////////        PropertyInfo pi = component.GetType().BaseType.GetProperty(propertyName);
                ////////////////////        pi.SetValue(component, rez);
                ////////////////////    }
                ////////////////////}
            }
            if (displayName == "Значок")
            {
                //Ф.Значок("C:\444\Pic\Иконка.ico")

                string rez = null;
                osfDesigner.MyIcon MyIcon1 = null;
                rez = valProp.Replace("\u0022", "");
                rez = OneScriptFormsDesigner.ParseBetween(rez, "(", ")");
                try
                {
                    MyIcon1 = new osfDesigner.MyIcon(rez);
                    MyIcon1.Path = rez;
                }
                catch { }

                if (control.GetType() == typeof(osfDesigner.Form))
                {
                    ((osfDesigner.Form)control).Icon = MyIcon1;
                }
                else if (control.GetType() == typeof(osfDesigner.NotifyIcon))
                {
                    if (rez != "AAABAAEAEBAQAAEABAAoAQAAFgAAACgAAAAQAAAAIAAAAAEABAAAAAAAwAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAACAAACAAAAAgIAAgAAAAIAAgACAgAAAgICAAMDAwAAAAP8AAP8AAAD//wD/AAAA/wD/AP//AAD///8AZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmYAAP//AAD//wAA//8AAP//AAD//wAA//8AAP//AAD//wAA//8AAP//AAD//wAA//8AAP//AAD//wAA//8AAP//")
                    {
                        ((osfDesigner.NotifyIcon)control).Icon = MyIcon1;
                    }
                }
            }
            if (displayName == "МаксимальнаяДата" ||
                displayName == "МинимальнаяДата" ||
                displayName == "ТекущаяДата")
            {
                //Дата(9998, 12, 01, 00, 00, 00)
                string[] result = valProp.Replace("Дата(", "").Replace(")", "").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                DateTime rez = new DateTime();
                for (int i = 0; i < result.Length; i++)
                {
                    if (i == 0)
                    {
                        rez = rez.AddYears(Int32.Parse(result[0]) - 1);
                    }
                    if (i == 1)
                    {
                        rez = rez.AddMonths(Int32.Parse(result[1]) - 1);
                    }
                    if (i == 2)
                    {
                        rez = rez.AddDays(Int32.Parse(result[2]) - 1);
                    }
                    if (i == 3)
                    {
                        rez = rez.AddHours(Int32.Parse(result[3]));
                    }
                    if (i == 4)
                    {
                        rez = rez.AddMinutes(Int32.Parse(result[4]));
                    }
                    if (i == 5)
                    {
                        rez = rez.AddSeconds(Int32.Parse(result[5]));
                    }
                }

                string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                PropertyInfo pi = control.GetType().GetProperty(propertyName);
                pi.SetValue(control, rez);
            }
        }

        public static void NodeSearch(osfDesigner.TreeView treeView, string nameNodeParent, ref osfDesigner.MyTreeNode node, System.Windows.Forms.TreeNodeCollection treeNodes = null)
        {
            System.Windows.Forms.TreeNodeCollection _treeNodes;
            if (treeNodes == null)
            {
                _treeNodes = treeView.Nodes;
            }
            else
            {
                _treeNodes = treeNodes;
            }
            osfDesigner.MyTreeNode treeNode = null;
            for (int i = 0; i < _treeNodes.Count; i++)
            {
                treeNode = (osfDesigner.MyTreeNode)_treeNodes[i];
                if (treeNode.Name == nameNodeParent)
                {
                    node = treeNode;
                    break;
                }
                if (treeNode.Nodes.Count > 0)
                {
                    NodeSearch(treeView, nameNodeParent, ref node, treeNode.Nodes);
                }
            }
        }

        public static void SelectedNodeSearch(System.Windows.Forms.TreeView treeView, string nameNodeParent, ref System.Windows.Forms.TreeNode node, System.Windows.Forms.TreeNodeCollection treeNodes = null)
        {
            System.Windows.Forms.TreeNodeCollection _treeNodes;
            if (treeNodes == null)
            {
                _treeNodes = treeView.Nodes;
            }
            else
            {
                _treeNodes = treeNodes;
            }
            System.Windows.Forms.TreeNode treeNode = null;
            for (int i = 0; i < _treeNodes.Count; i++)
            {
                treeNode = (System.Windows.Forms.TreeNode)_treeNodes[i];
                if (((MenuItemEntry)treeNode.Tag).Name == nameNodeParent)
                {
                    node = treeNode;
                    break;
                }
                if (treeNode.Nodes.Count > 0)
                {
                    SelectedNodeSearch(treeView, nameNodeParent, ref node, treeNode.Nodes);
                }
            }
        }

        public static void SetNodePropValue(osfDesigner.MyTreeNode control, string displayName, string valProp)
        {
            if (displayName == "Помечен")
            {
                bool bool1 = false;
                if (valProp == "Истина")
                {
                    bool1 = true;
                }
                control.Checked = bool1;
            }
            //если valProp это число
            if (displayName == "Индекс" ||
                displayName == "ИндексВыбранногоИзображения" ||
                displayName == "ИндексИзображения")
            {
                string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                control.GetType().GetProperty(propertyName).SetValue(control, Int32.Parse(valProp));
            }
            //если valProp это строка
            if (displayName == "ПолныйПуть" ||
                displayName == "Текст")
            {
                if (valProp.Contains("Ф.Окружение().НоваяСтрока"))
                {
                    valProp = valProp.Replace("\u0022", "");
                    valProp = valProp.Replace("+Ф.Окружение().НоваяСтрока+", Environment.NewLine);
                }
                else
                {
                    valProp = valProp.Replace("\u0022", "");
                }
                string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                control.GetType().GetProperty(propertyName).SetValue(control, valProp);
            }
            if (displayName == "ШрифтУзла")
            {
                //Ф.Шрифт("Microsoft Sans Serif", 10.2, Ф.СтильШрифта.Курсив + Ф.СтильШрифта.Подчеркнутый + Ф.СтильШрифта.Зачеркнутый)

                string[] result = valProp.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                string FontName = "";
                float FontSize = 0f;
                int Style1 = 0;

                for (int i = 0; i < result.Length; i++)
                {
                    if (i == 0)
                    {
                        try
                        {
                            FontName = OneScriptFormsDesigner.ParseBetween(result[0], "\u0022", null).Replace("\u0022", "").Trim();
                        }
                        catch { }
                    }
                    if (i == 1)
                    {
                        try
                        {
                            FontSize = Single.Parse(result[1].Trim());
                        }
                        catch
                        {
                            try
                            {
                                FontSize = Single.Parse(result[1].Trim().Replace(".", ","));
                            }
                            catch { }
                        }
                    }
                    if (i == 2)
                    {
                        if (result[2].Contains("Жирный"))
                        {
                            Style1 = Style1 + (int)System.Drawing.FontStyle.Bold;
                        }
                        if (result[2].Contains("Зачеркнутый"))
                        {
                            Style1 = Style1 + (int)System.Drawing.FontStyle.Strikeout;
                        }
                        if (result[2].Contains("Курсив"))
                        {
                            Style1 = Style1 + (int)System.Drawing.FontStyle.Italic;
                        }
                        if (result[2].Contains("Подчеркнутый"))
                        {
                            Style1 = Style1 + (int)System.Drawing.FontStyle.Underline;
                        }
                    }
                }
                control.NodeFont = new System.Drawing.Font(FontName, FontSize, (System.Drawing.FontStyle)Style1);
            }
        }
    }
}
