using System;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.Design;
using osfDesigner.Properties;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace osfDesigner
{
    public class SaveForm
    {
        public static System.Windows.Forms.TreeView TreeView1 = pDesigner.DSME.PropertyGridHost.TreeView;
        public static System.Windows.Forms.ToolBarButton ButtonSort1 = pDesigner.DSME.PropertyGridHost.ButtonSort;

        public static System.Collections.Generic.Dictionary<string, Component> comps = new Dictionary<string, Component>();
        private static string Template1;
        private static readonly string TemplateOriginal =
@"[<Конструкторы]
[Конструкторы>]
[<Свойства]
[Свойства>]
";
        private static string path;

        public static string GetScriptText(string fileName)
        {
            // 1. Получить перечень текущих свойств формы и всех компонентов
            // 2. Выгрузить обязательные свойства согласно RequiredValues.
            // 3. Сравнить текущие свойства с DefaultValues и измененные выгрузить.
            // 4. Файл сохраненной формы разделен на разделы. Например раздел конструкторов заключен в ограничители [<Конструкторы] и [Конструкторы>]

            path = fileName.Substring(0, fileName.LastIndexOf('\\') + 1);

            comps.Clear();
            Template1 = TemplateOriginal;
            DesignSurfaceManagerExt DesignSurfaceManagerExt = pDesigner.DSME;
            IDesignerEventService des = (IDesignerEventService)DesignSurfaceManagerExt.GetService(typeof(IDesignerEventService));
            if (des != null)
            {
                string compName = "";
                ComponentCollection ctrls = des.ActiveDesigner.Container.Components;
                ArrayList ctrlsArray = new ArrayList();
                ArrayList ctrlsExisting = new ArrayList();
                for (int i = 0; i < ctrls.Count; i++)
                {
                    ctrlsArray.Add(ctrls[i]);
                }

                Form Form1 = (Form)pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost().Container.Components[0];
                for (int i = 0; i < Form1.ArrayListComponentsAddingOrder.Count; i++)
                {
                    Component comp4 = (Component)Form1.ArrayListComponentsAddingOrder[i];
                    if (ctrlsArray.Contains(comp4))
                    {
                        ctrlsExisting.Add(comp4);
                    }
                }

                bool toolTipPresent = false;
                foreach (Component comp in ctrlsExisting)// Проверим наличие подсказок
                {
                    if (comp.Site.Name.Contains("Подсказка"))
                    {
                        toolTipPresent = true;
                    }
                }

                string strComp = "";
                // Запишем в выгрузку имена компонентов в раздел [<Конструкторы].
                // раздел конструкторов будет определять очередность создания компонентов
                for (int i = 0; i < ctrlsExisting.Count; i++)
                {
                    Component comp = (Component)ctrlsExisting[i];
                    compName = comp.Site.Name;
                    comps.Add(compName, comp);// установим соответствие между именем компонента и компонентом
                    string trimName = compName;
                    for (int i1 = 0; i1 < 10; i1++)
                    {
                        trimName = trimName.Replace(i1.ToString(), "");
                    }
                    strComp = compName;
                    if (comp.GetType() == typeof(Form))
                    {
                        strComp = compName + " = Ф.Форма();";
                        Template1 = Template1.Replace(@"[<Конструкторы]", @"[<Конструкторы]" + Environment.NewLine + strComp);
                    }
                    else
                    {
                        strComp = "" + compName + " = Ф." + trimName + "();";
                        Template1 = Template1.Replace(@"[Конструкторы>]", strComp + Environment.NewLine + @"[Конструкторы>]");
                    }
                }
                // Запишем в выгрузку свойства компонентов.
                // Последовательность возмем из древовидной структуры TreeView при сортировке "В порядке создания".
                bool stateSort = ButtonSort1.Pushed;
                ButtonSort1.Pushed = false;
                Component comp2 = OneScriptFormsDesigner.HighlightedComponent();
                pDesigner.DSME.PropertyGridHost.ReloadTreeView();
                if (comp2 != null)
                {
                    pDesigner.DSME.PropertyGridHost.ChangeSelectNode(comp2);
                }

                ArrayList objArrayList2 = new ArrayList();// содержит имена компонентов в иерархии дерева компонентов
                GetNodes1(TreeView1, ref objArrayList2);
                for (int i = 0; i < objArrayList2.Count; i++)
                {
                    Component comp = comps[(string)objArrayList2[i]];
                    Component comp1 = null;
                    if (comp.GetType() == typeof(System.Windows.Forms.TabPage))
                    {
                        try
                        {
                            comp1 = (Component)OneScriptFormsDesigner.RevertSimilarObj(comp);
                        }
                        catch { }
                        if (comp1 != null)
                        {
                            comp = comp1;
                        }
                    }
                    else if (comp.GetType() == typeof(System.Windows.Forms.ImageList))
                    {
                        try
                        {
                            comp1 = (Component)OneScriptFormsDesigner.RevertSimilarObj(comp);
                        }
                        catch { }
                        if (comp1 != null)
                        {
                            comp = comp1;
                        }
                    }
                    else if (comp.GetType() == typeof(System.Windows.Forms.MainMenu))
                    {
                        try
                        {
                            comp1 = (Component)OneScriptFormsDesigner.RevertSimilarObj(comp);
                        }
                        catch { }
                        if (comp1 != null)
                        {
                            comp = comp1;
                        }
                    }
                    compName = comp.Site.Name;
                    Template1 = Template1.Replace(@"[Свойства>]", @"[<" + compName + "]" + Environment.NewLine + @"[Свойства>]");

                    // установим для элемента родителя
                    string strParent = "";
                    try
                    {
                        if (comp.GetType() == typeof(osfDesigner.TabPage))
                        {
                            strParent = ((osfDesigner.TabPage)comp).OriginalObj.Parent.Name;
                        }
                        else
                        {
                            strParent = ((dynamic)comp).Parent.Name;
                        }
                    }
                    catch { }
                    if (strParent != "")
                    {
                        AddToScript(compName + ".Родитель = " + strParent + ";");
                    }

                    if (compName.Contains("ЗначокУведомления"))
                    {
                        if (comp.GetType().GetProperty("Icon").GetValue(comp) == null)
                        {
                            string strIcon = compName + ".Значок = Ф.Значок(\u0022" + "AAABAAEAEBAQAAEABAAoAQAAFgAAACgAAAAQAAAAIAAAAAEABAAAAAAAwAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAACAAACAAAAAgIAAgAAAAIAAgACAgAAAgICAAMDAwAAAAP8AAP8AAAD//wD/AAAA/wD/AP//AAD///8AZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmYAAP//AAD//wAA//8AAP//AAD//wAA//8AAP//AAD//wAA//8AAP//AAD//wAA//8AAP//AAD//wAA//8AAP//" + "\u0022" + "); // обязательно назначить значок";
                            AddToScript(strIcon);
                        }
                        string strVisible = compName + ".Отображать = Истина; // обязательно отобразить значок после создания";
                        AddToScript(strVisible);
                    }

                    PropertyInfo[] myPropertyInfo = comp.GetType().GetProperties();
                    for (int i1 = 0; i1 < myPropertyInfo.Length; i1++)
                    {
                        string valueName = osfDesigner.OneScriptFormsDesigner.GetDisplayName(comp, myPropertyInfo[i1].Name);
                        if (valueName != "")
                        {
                            PropertyDescriptor pd = TypeDescriptor.GetProperties(comp)[myPropertyInfo[i1].Name];
                            try
                            {
                                string compValue = osfDesigner.OneScriptFormsDesigner.ObjectConvertToString(pd.GetValue(comp));
                                RequiredDefaultValuesValues(comp, compName, valueName, compValue);
                            }
                            catch { }
                        }
                    }

                    // Обработаем подсказку, если создана хоть одна подсказка.
                    if (toolTipPresent)
                    {
                        Hashtable Hashtable1 = null;
                        try
                        {
                            Hashtable1 = ((dynamic)comp).ToolTip;
                        }
                        catch { }
                        if (Hashtable1 != null)
                        {
                            foreach (DictionaryEntry de in Hashtable1)
                            {
                                string nameToolTip = (string)de.Key;
                                string compValue = (string)de.Value;
                                compValue = compValue.Replace(Environment.NewLine, "\u0022 + Ф.Окружение().НоваяСтрока + \u0022");
                                AddToScript(nameToolTip + ".УстановитьПодсказку(" + compName + ", \u0022" + compValue + "\u0022);");
                            }
                        }
                    }
                    Template1 = Template1.Replace(@"[Свойства>]", @"[" + compName + ">]" + Environment.NewLine + @"[Свойства>]");

                }
                ButtonSort1.Pushed = stateSort;
                Component comp3 = OneScriptFormsDesigner.HighlightedComponent();
                pDesigner.DSME.PropertyGridHost.ReloadTreeView();
                if (comp3 != null)
                {
                    pDesigner.DSME.PropertyGridHost.ChangeSelectNode(comp3);
                }
            }
            return ReSort(Template1);
        }

        // переформируем выгруженную форму
        private static string ReSort(string Template1)
        {
            string[] stringSeparators = new string[] { Environment.NewLine };

            string str1 = Template1;

            // зададим порядок свойств во фрагментах
            string strProps = OneScriptFormsDesigner.ParseBetween(str1, @"[<Свойства]", @"[Свойства>]");
            ArrayList ArrayList2 = OneScriptFormsDesigner.StrFindBetween(strProps, @"[<", @">]", false);
            for (int i = 0; i < ArrayList2.Count; i++)
            {
                string fragment1 = (string)ArrayList2[i];
                string namecomp = OneScriptFormsDesigner.ParseBetween(fragment1, @"[<", @"]");

                string fragment2 = "";
                // создадим SortedList1
                SortedList SortedList1 = new SortedList();
                SortedList1.Capacity = 1000;
                string[] result = fragment1.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                for (int i1 = 0; i1 < result.Length; i1++)
                {
                    string strResult = result[i1];
                    if (i1 == 0)
                    {
                        SortedList1.Add(i1, strResult);
                    }
                    else if (strResult.Contains(namecomp + "." + "Родитель ="))
                    {
                        SortedList1.Add(1, strResult);
                    }
                    else if (strResult.Contains(namecomp + "." + "Стыковка ="))
                    {
                        SortedList1.Add(2, strResult);
                    }
                    else if (strResult.Contains(namecomp + "." + "Размер ="))
                    {
                        SortedList1.Add(3, strResult);
                    }
                    else if (strResult.Contains(namecomp + "." + "ПорядокОбхода ="))
                    {
                        SortedList1.Add(4, strResult);
                    }
                    else if (strResult.Contains(namecomp + "." + "Флажки ="))
                    {
                        SortedList1.Add(5, strResult);
                    }
                    else
                    {
                        SortedList1.Add(i1 + 500, strResult);
                    }
                }
                ArrayList ArrayList3 = new ArrayList();
                for (int i1 = 0; i1 < SortedList1.Capacity; i1++)
                {
                    if (SortedList1[i1] != null)
                    {
                        ArrayList3.Add(SortedList1[i1]);
                    }
                }
                for (int i4 = 0; i4 < ArrayList3.Count; i4++)
                {
                    string strArrayList = ((string)ArrayList3[i4]).Replace(Environment.NewLine, "").Trim();
                    if (i4 == 0)
                    {
                        fragment2 = fragment2 + strArrayList;
                    }
                    else
                    {
                        fragment2 = fragment2 + Environment.NewLine + strArrayList;
                    }

                }
                str1 = str1.Replace(fragment1, fragment2);
            }

            comps.Clear();
            return str1;
        }

        private static void RequiredDefaultValuesValues(dynamic comp, string compName, string valueName, string compValue)
        {
            if (valueName == "ДвойнаяБуферизация" ||
                valueName == "РежимАвтоМасштабирования" ||
                valueName == "DoubleBuffered")
            {
                return;
            }

            if (comp.RequiredValues.Contains(valueName + " =="))
            {
                TextToScript(compName, valueName, compValue, comp);
            }
            else
            {
                if (!comp.DefaultValues.Contains(valueName + " == " + compValue))
                {
                    try
                    {
                        TextToScript(compName, valueName, compValue, comp);
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("Не обработано: на компоненте = " + compName + " valueName=" + valueName + " compValue=" + compValue);
                    }
                }
            }
        }

        private static void TextToScript(string compName, string valueName, string compValue, dynamic val = null)
        {
            // Пропустим некоторые свойства, они не дают правильно формировать расположение элементов
            if (val.GetType() == typeof(osfDesigner.StatusBar) && (valueName == "Положение" || valueName == "Размер"))
            {
                return;
            }
            if (val.GetType() == typeof(osfDesigner.Splitter) && (valueName == "Курсор" || valueName == "Положение"))
            {
                return;
            }
            if (val.GetType() == typeof(osfDesigner.MenuItemEntry) && (valueName == "Текст"))
            {
                return;
            }
            if (val.GetType() == typeof(osfDesigner.MyTreeNode) && (valueName == "ПолныйПуть"))
            {
                return;
            }
            if (val.GetType() == typeof(osfDesigner.DataGridTableStyle) && (valueName == "ИмяСтиля"))
            {
                return;
            }
            if (val.GetType() == typeof(osfDesigner.DataGridTextBoxColumn) && (valueName == "ИмяСтиля"))
            {
                return;
            }
            if (val.GetType() == typeof(osfDesigner.DataGridComboBoxColumnStyle) && (valueName == "ИмяСтиля"))
            {
                return;
            }
            if (val.GetType() == typeof(osfDesigner.DataGridBoolColumn) && (valueName == "ИмяСтиля"))
            {
                return;
            }
	
            if (val.GetType() == typeof(osfDesigner.Form) && (valueName == "Стыковка"))
            {
                return;
            }
            if (val.GetType() == typeof(osfDesigner.Form) && (valueName == "Якорь"))
            {
                return;
            }
            // закончили пропуск свойств

            if (compValue == "Ложь" || compValue == "Истина")
            {
                AddToScript(compName + "." + valueName + " = " + compValue + ";");
                return;
            }

            if (valueName == "ЭлементыМеню")
            {
                if (val != null)
                {
                    System.Windows.Forms.Menu.MenuItemCollection MenuItemCollection1 = (System.Windows.Forms.Menu.MenuItemCollection)val.MenuItems;
                    if (MenuItemCollection1.Count > 0)
                    {
                        MenuItemEntry MenuItemEntry1;
                        for (int i = 0; i < MenuItemCollection1.Count; i++)
                        {
                            MenuItemEntry1 = OneScriptFormsDesigner.RevertSimilarObj(MenuItemCollection1[i]);
                            string strName = MenuItemEntry1.Name.Contains("Сепаратор") ? "-" : MenuItemEntry1.Text;
                            AddToScript(MenuItemEntry1.Name + " = " + compName + ".ЭлементыМеню.Добавить(Ф.ЭлементМеню(\u0022" + strName + "\u0022));");
                            PropComponent(MenuItemEntry1);
                            if (MenuItemEntry1.MenuItems.Count > 0)
                            {
                                GetMenuItems((MenuItemEntry)MenuItemEntry1);
                            }
                        }
                    }
                    return;
                }
            }

            if (valueName == "Меню")
            {
                if (compValue != null)
                {
                    AddToScript(compName + "." + valueName + " = " + compValue + ";");
                    return;
                }
                return;
            }
            if (valueName == "ОбластьСсылки")
            {
                if (val != null)
                {
                    AddToScript(compName + "." + valueName + " = " + "Ф.ОбластьСсылки(" + compValue.Replace(";", ",") + ");");
                    return;
                }
            }
            if (valueName == "Подэлементы")
            {
                if (val != null)
                {
                    System.Windows.Forms.ListViewItem.ListViewSubItemCollection ListViewSubItemCollection1 = (System.Windows.Forms.ListViewItem.ListViewSubItemCollection)val.SubItems;
                    if (ListViewSubItemCollection1.Count > 0)
                    {
                        osfDesigner.ListViewSubItem ListViewSubItem1;
                        for (int i = 1; i < ListViewSubItemCollection1.Count; i++)// первый индекс должен быть 1, а не 0
                        {
                            ListViewSubItem1 = (osfDesigner.ListViewSubItem)ListViewSubItemCollection1[i];
                            AddToScript(ListViewSubItem1.Name + " = Ф.ПодэлементСпискаЭлементов();");
                            PropComponent(ListViewSubItem1);
                            AddToScript(compName + ".Подэлементы.Добавить(" + ListViewSubItem1.Name + ");");
                        }
                    }
                }
                return;
            }
            if (valueName == "Элементы")
            {
                if (val != null)
                {
                    if (val.GetType() == typeof(osfDesigner.ComboBox))
                    {
                        System.Windows.Forms.ComboBox.ObjectCollection ComboBoxObjectCollection1 = (System.Windows.Forms.ComboBox.ObjectCollection)val.Items;
                        if (ComboBoxObjectCollection1.Count > 0)
                        {
                            osfDesigner.ListItemComboBox ListItemComboBox1;
                            string strValue = "";
                            for (int i = 0; i < ComboBoxObjectCollection1.Count; i++)
                            {
                                ListItemComboBox1 = (osfDesigner.ListItemComboBox)ComboBoxObjectCollection1[i];
                                if (ListItemComboBox1.ValueType == osfDesigner.DataType.Строка)
                                {
                                    strValue = strValue + compName + ".Элементы.Добавить(Ф.ЭлементСписка(\u0022" + ListItemComboBox1.Text + "\u0022, \u0022" + ListItemComboBox1.Text + "\u0022));";
                                }
                                else if (ListItemComboBox1.ValueType == osfDesigner.DataType.Дата)
                                {
                                    DateTime DateTime1 = ListItemComboBox1.ValueDateTime;
                                    strValue = strValue + compName + ".Элементы.Добавить(Ф.ЭлементСписка(\u0022" + ListItemComboBox1.Text + "\u0022, " +
                                        "Дата(" +
                                        DateTime1.ToString("yyyy") + ", " +
                                        DateTime1.ToString("MM") + ", " +
                                        DateTime1.ToString("dd") + ", " +
                                        DateTime1.ToString("HH") + ", " +
                                        DateTime1.ToString("mm") + ", " +
                                        DateTime1.ToString("ss") + ")" + "));";
                                }
                                else if (ListItemComboBox1.ValueType == osfDesigner.DataType.Булево)
                                {
                                    strValue = strValue + compName + ".Элементы.Добавить(Ф.ЭлементСписка(\u0022" + ListItemComboBox1.Text + "\u0022, " + ListItemComboBox1.Text + "));";
                                }
                                else if (ListItemComboBox1.ValueType == osfDesigner.DataType.Число)
                                {
                                    strValue = strValue + compName + ".Элементы.Добавить(Ф.ЭлементСписка(\u0022" + ListItemComboBox1.Text + "\u0022, " + ListItemComboBox1.Text + "));";
                                }
                                if (i == 0)
                                {
                                    strValue = strValue.TrimStart(' ');
                                }
                                if (i < (ComboBoxObjectCollection1.Count - 1))
                                {
                                    strValue = strValue + Environment.NewLine;
                                }
                            }
                            AddToScript(strValue);
                        }
                    }
                    else if (val.GetType() == typeof(osfDesigner.ListBox))
                    {
                        System.Windows.Forms.ListBox.ObjectCollection ListBoxObjectCollection1 = (System.Windows.Forms.ListBox.ObjectCollection)val.Items;
                        if (ListBoxObjectCollection1.Count > 0)
                        {
                            osfDesigner.ListItemListBox ListItemListBox1;
                            string strValue = "";
                            for (int i = 0; i < ListBoxObjectCollection1.Count; i++)
                            {
                                ListItemListBox1 = (osfDesigner.ListItemListBox)ListBoxObjectCollection1[i];
                                if (ListItemListBox1.ValueType == osfDesigner.DataType.Строка)
                                {
                                    strValue = strValue + compName + ".Элементы.Добавить(Ф.ЭлементСписка(\u0022" + ListItemListBox1.Text + "\u0022, \u0022" + ListItemListBox1.Text + "\u0022));";
                                }
                                else if (ListItemListBox1.ValueType == osfDesigner.DataType.Дата)
                                {
                                    DateTime DateTime1 = ListItemListBox1.ValueDateTime;
                                    strValue = strValue + compName + ".Элементы.Добавить(Ф.ЭлементСписка(\u0022" + ListItemListBox1.Text + "\u0022, " +
                                        "Дата(" +
                                        DateTime1.ToString("yyyy") + ", " +
                                        DateTime1.ToString("MM") + ", " +
                                        DateTime1.ToString("dd") + ", " +
                                        DateTime1.ToString("HH") + ", " +
                                        DateTime1.ToString("mm") + ", " +
                                        DateTime1.ToString("ss") + ")" + "));";
                                }
                                else if (ListItemListBox1.ValueType == osfDesigner.DataType.Булево)
                                {
                                    strValue = strValue + compName + ".Элементы.Добавить(Ф.ЭлементСписка(\u0022" + ListItemListBox1.Text + "\u0022, " + ListItemListBox1.Text + "));";
                                }
                                else if (ListItemListBox1.ValueType == osfDesigner.DataType.Число)
                                {
                                    strValue = strValue + compName + ".Элементы.Добавить(Ф.ЭлементСписка(\u0022" + ListItemListBox1.Text + "\u0022, " + ListItemListBox1.Text + "));";
                                }
                                if (i == 0)
                                {
                                    strValue = strValue.TrimStart(' ');
                                }
                                if (i < (ListBoxObjectCollection1.Count - 1))
                                {
                                    strValue = strValue + Environment.NewLine;
                                }
                            }
                            AddToScript(strValue);
                        }
                    }
                    else if (val.GetType() == typeof(osfDesigner.ListView))
                    {
                        System.Windows.Forms.ListView.ListViewItemCollection ListViewItemCollection1 = (System.Windows.Forms.ListView.ListViewItemCollection)val.Items;
                        if (ListViewItemCollection1.Count > 0)
                        {
                            osfDesigner.ListViewItem ListViewItem1;
                            for (int i = 0; i < ListViewItemCollection1.Count; i++)
                            {
                                ListViewItem1 = (osfDesigner.ListViewItem)ListViewItemCollection1[i];
                                AddToScript(ListViewItem1.Name + " = Ф.ЭлементСпискаЭлементов();");
                                PropComponent(ListViewItem1);
                                AddToScript(compName + ".Элементы.Добавить(" + ListViewItem1.Name + ");");
                            }
                        }
                    }
                }
                return;
            }
            if (valueName == "Панели")
            {
                if (val != null)
                {
                    System.Windows.Forms.StatusBar.StatusBarPanelCollection StatusBarPanelCollection1 = (System.Windows.Forms.StatusBar.StatusBarPanelCollection)val.Panels;
                    if (StatusBarPanelCollection1.Count > 0)
                    {
                        osfDesigner.StatusBarPanel StatusBarPanel1;
                        for (int i = 0; i < StatusBarPanelCollection1.Count; i++)
                        {
                            StatusBarPanel1 = (osfDesigner.StatusBarPanel)StatusBarPanelCollection1[i];
                            AddToScript(StatusBarPanel1.Name + " = Ф.ПанельСтрокиСостояния();");
                            AddToScript(compName + ".Панели.Добавить(" + StatusBarPanel1.Name + ");");
                            PropComponent(StatusBarPanel1);
                        }
                    }
                }
                return;
            }
            if (valueName == "Колонки")
            {
                if (val != null)
                {
                    System.Windows.Forms.ListView.ColumnHeaderCollection ColumnHeaderCollection1 = (System.Windows.Forms.ListView.ColumnHeaderCollection)val.Columns;
                    if (ColumnHeaderCollection1.Count > 0)
                    {
                        osfDesigner.ColumnHeader ColumnHeader1;
                        for (int i = 0; i < ColumnHeaderCollection1.Count; i++)
                        {
                            ColumnHeader1 = (osfDesigner.ColumnHeader)ColumnHeaderCollection1[i];
                            AddToScript(ColumnHeader1.Name + " = Ф.Колонка();");
                            PropComponent(ColumnHeader1);
                            AddToScript(compName + ".Колонки.Добавить(" + ColumnHeader1.Name + ");");
                        }
                    }
                }
                return;
            }
            if (valueName == "СтилиКолонкиСеткиДанных")
            {
                if (val != null)
                {
                    System.Windows.Forms.GridColumnStylesCollection GridColumnStylesCollection1 = (System.Windows.Forms.GridColumnStylesCollection)val.GridColumnStyles;
                    if (GridColumnStylesCollection1.Count > 0)
                    {
                        for (int i = 0; i < GridColumnStylesCollection1.Count; i++)
                        {
                            dynamic Style1 = GridColumnStylesCollection1[i];
                            if (Style1.GetType() == typeof(osfDesigner.DataGridBoolColumn))
                            {
                                osfDesigner.DataGridBoolColumn GridColumnStyle1 = (osfDesigner.DataGridBoolColumn)Style1;
                                AddToScript(GridColumnStyle1.NameStyle + " = Ф.СтильКолонкиБулево();" + Environment.NewLine +
                                    GridColumnStyle1.NameStyle + ".ИмяОтображаемого = \u0022" + GridColumnStyle1.MappingName + "\u0022;");
                                PropComponent(GridColumnStyle1);
                                AddToScript(compName + ".СтилиКолонкиСеткиДанных.Добавить(" + GridColumnStyle1.NameStyle + ");");
                            }
                            else if (Style1.GetType() == typeof(osfDesigner.DataGridTextBoxColumn))
                            {
                                osfDesigner.DataGridTextBoxColumn GridColumnStyle1 = (osfDesigner.DataGridTextBoxColumn)Style1;
                                AddToScript(GridColumnStyle1.NameStyle + " = Ф.СтильКолонкиПолеВвода();" + Environment.NewLine +
                                    GridColumnStyle1.NameStyle + ".ИмяОтображаемого = \u0022" + GridColumnStyle1.MappingName + "\u0022;");
                                PropComponent(GridColumnStyle1);
                                AddToScript(compName + ".СтилиКолонкиСеткиДанных.Добавить(" + GridColumnStyle1.NameStyle + ");");
                            }
                            else if (Style1.GetType() == typeof(osfDesigner.DataGridComboBoxColumnStyle))
                            {
                                osfDesigner.DataGridComboBoxColumnStyle GridColumnStyle1 = (osfDesigner.DataGridComboBoxColumnStyle)Style1;
                                AddToScript(GridColumnStyle1.NameStyle + " = Ф.СтильКолонкиПолеВыбора();" + Environment.NewLine +
                                    GridColumnStyle1.NameStyle + ".ИмяОтображаемого = \u0022" + GridColumnStyle1.MappingName + "\u0022;");
                                PropComponent(GridColumnStyle1);
                                AddToScript(compName + ".СтилиКолонкиСеткиДанных.Добавить(" + GridColumnStyle1.NameStyle + ");");
                            }
                        }
                    }
                }
                return;
            }
            if (valueName == "СтилиТаблицы")
            {
                if (val != null)
                {
                    System.Windows.Forms.GridTableStylesCollection GridTableStylesCollection1 = (System.Windows.Forms.GridTableStylesCollection)val.TableStyles;
                    if (GridTableStylesCollection1.Count > 0)
                    {
                        osfDesigner.DataGridTableStyle DataGridTableStyle1;
                        for (int i = 0; i < GridTableStylesCollection1.Count; i++)
                        {
                            System.Windows.Forms.DataGridTableStyle OriginalObj = GridTableStylesCollection1[i];
                            DataGridTableStyle1 = OneScriptFormsDesigner.RevertSimilarObj(OriginalObj);
                            AddToScript(DataGridTableStyle1.NameStyle + " = Ф.СтильТаблицыСеткиДанных();" + Environment.NewLine +
                                DataGridTableStyle1.NameStyle + ".ИмяОтображаемого = \u0022" + DataGridTableStyle1.MappingName + "\u0022;" + Environment.NewLine +
                                compName + ".СтилиТаблицы.Добавить(" + DataGridTableStyle1.NameStyle + ");");
                            PropComponent(DataGridTableStyle1);
                        }
                    }
                }
                return;
            }
            if (valueName == "Кнопки")
            {
                if (val != null)
                {
                    System.Windows.Forms.ToolBar.ToolBarButtonCollection ToolBarButtonCollection1 = (System.Windows.Forms.ToolBar.ToolBarButtonCollection)val.Buttons;
                    if (ToolBarButtonCollection1.Count > 0)
                    {
                        osfDesigner.ToolBarButton ToolBarButton1;
                        for (int i = 0; i < ToolBarButtonCollection1.Count; i++)
                        {
                            ToolBarButton1 = (osfDesigner.ToolBarButton)ToolBarButtonCollection1[i].Tag;
                            AddToScript(ToolBarButton1.Name + " = " + compName + ".Кнопки.Добавить(Ф.КнопкаПанелиИнструментов());");
                            PropComponent(ToolBarButton1);
                        }
                    }
                }
                return;
            }
            if (valueName == "Узлы")
            {
                if (val != null)
                {
                    System.Windows.Forms.TreeNodeCollection TreeNodeCollection1 = (System.Windows.Forms.TreeNodeCollection)val.Nodes;
                    if (TreeNodeCollection1.Count > 0)
                    {
                        osfDesigner.MyTreeNode MyTreeNode1;
                        for (int i = 0; i < TreeNodeCollection1.Count; i++)
                        {
                            MyTreeNode1 = (osfDesigner.MyTreeNode)TreeNodeCollection1[i];
                            AddToScript(MyTreeNode1.Name + " = " + compName + ".Узлы.Добавить(\u0022" + MyTreeNode1.Name + "\u0022);");
                            PropComponent(MyTreeNode1);
                            if (MyTreeNode1.Nodes.Count > 0)
                            {
                                GetNodes(MyTreeNode1);
                            }
                        }
                    }
                }
                return;
            }
            if (valueName == "Шрифт" || valueName == "ШрифтУзла" || valueName == "ШрифтЗаголовков")
            {
                string FontName = "";
                string FontSize = "";
                string FontStyle = "";

                string[] separators = new string[] { ";" };
                string[] result = compValue.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < result.Length; i++)
                {
                    if (i == 0)
                    {
                        FontName = result[0];
                    }
                    if (i == 1)
                    {
                        FontSize = result[1].TrimStart(' ');
                        FontSize = FontSize.Replace("pt", "");
                        FontSize = FontSize.Replace(",", ".");
                    }
                    if (i == 2)
                    {
                        FontStyle = result[2].Trim(' ');
                        FontStyle = FontStyle.Replace("стиль=", "Ф.СтильШрифта.");
                        FontStyle = FontStyle.Replace(", ", " + Ф.СтильШрифта.");
                    }
                }
                AddToScript(compName + "." + valueName + " = Ф.Шрифт(\u0022" + FontName + "\u0022, " + FontSize + ", " + FontStyle + ");");
                return;
            }
            if (valueName == "ВыделенныеДаты")
            {
                if (val != null)
                {
                    MyBoldedDatesList MyBoldedDatesList1 = (MyBoldedDatesList)val.BoldedDates_osf;
                    string strDateTimes = "";
                    if (MyBoldedDatesList1.Count > 0)
                    {
                        for (int i1 = 0; i1 < MyBoldedDatesList1.Count; i1++)
                        {
                            strDateTimes = strDateTimes + compName + "." + valueName + ".Добавить(Дата(" +
                                    MyBoldedDatesList1[i1].Value.ToString("yyyy") + ", " +
                                    MyBoldedDatesList1[i1].Value.ToString("MM") + ", " +
                                    MyBoldedDatesList1[i1].Value.ToString("dd") + ", " +
                                    MyBoldedDatesList1[i1].Value.ToString("HH") + ", " +
                                    MyBoldedDatesList1[i1].Value.ToString("mm") + ", " +
                                    MyBoldedDatesList1[i1].Value.ToString("ss") + "));";
                            if (i1 == 0)
                            {
                                strDateTimes = strDateTimes.TrimStart(' ');
                            }
                            if (i1 < (MyBoldedDatesList1.Count - 1))
                            {
                                strDateTimes = strDateTimes + Environment.NewLine;
                            }
                        }
                        AddToScript(strDateTimes);
                    }
                }
                return;
            }
            if (valueName == "ЕжегодныеДаты")
            {
                if (val != null)
                {
                    MyAnnuallyBoldedDatesList MyAnnuallyBoldedDatesList1 = (MyAnnuallyBoldedDatesList)val.AnnuallyBoldedDates_osf;
                    string strDateTimes = "";
                    if (MyAnnuallyBoldedDatesList1.Count > 0)
                    {
                        for (int i1 = 0; i1 < MyAnnuallyBoldedDatesList1.Count; i1++)
                        {
                            strDateTimes = strDateTimes + compName + "." + valueName + ".Добавить(Дата(" +
                                    MyAnnuallyBoldedDatesList1[i1].Value.ToString("yyyy") + ", " +
                                    MyAnnuallyBoldedDatesList1[i1].Value.ToString("MM") + ", " +
                                    MyAnnuallyBoldedDatesList1[i1].Value.ToString("dd") + ", " +
                                    MyAnnuallyBoldedDatesList1[i1].Value.ToString("HH") + ", " +
                                    MyAnnuallyBoldedDatesList1[i1].Value.ToString("mm") + ", " +
                                    MyAnnuallyBoldedDatesList1[i1].Value.ToString("ss") + "));";
                            if (i1 == 0)
                            {
                                strDateTimes = strDateTimes.TrimStart(' ');
                            }
                            if (i1 < (MyAnnuallyBoldedDatesList1.Count - 1))
                            {
                                strDateTimes = strDateTimes + Environment.NewLine;
                            }
                        }
                        AddToScript(strDateTimes);
                    }
                }
                return;
            }
            if (valueName == "ЕжемесячныеДаты")
            {
                if (val != null)
                {
                    MyMonthlyBoldedDatesList MyMonthlyBoldedDatesList1 = (MyMonthlyBoldedDatesList)val.MonthlyBoldedDates_osf;
                    string strDateTimes = "";
                    if (MyMonthlyBoldedDatesList1.Count > 0)
                    {
                        for (int i1 = 0; i1 < MyMonthlyBoldedDatesList1.Count; i1++)
                        {
                            strDateTimes = strDateTimes + compName + "." + valueName + ".Добавить(Дата(" +
                                    MyMonthlyBoldedDatesList1[i1].Value.ToString("yyyy") + ", " +
                                    MyMonthlyBoldedDatesList1[i1].Value.ToString("MM") + ", " +
                                    MyMonthlyBoldedDatesList1[i1].Value.ToString("dd") + ", " +
                                    MyMonthlyBoldedDatesList1[i1].Value.ToString("HH") + ", " +
                                    MyMonthlyBoldedDatesList1[i1].Value.ToString("mm") + ", " +
                                    MyMonthlyBoldedDatesList1[i1].Value.ToString("ss") + "));";
                            if (i1 == 0)
                            {
                                strDateTimes = strDateTimes.TrimStart(' ');
                            }
                            if (i1 < (MyMonthlyBoldedDatesList1.Count - 1))
                            {
                                strDateTimes = strDateTimes + Environment.NewLine;
                            }
                        }
                        AddToScript(strDateTimes);
                    }
                }
                return;
            }
            if (valueName == "Изображения")
            {
                if (val != null)
                {
                    osfDesigner.MyList MyList1 = (osfDesigner.MyList)val.Images;
                    string str1 = "";
                    if (MyList1.Count > 0)
                    {
                        for (int i1 = 0; i1 < MyList1.Count; i1++)
                        {
                            string newFileName = MyList1[i1].Path.Substring(MyList1[i1].Path.LastIndexOf('\\') + 1);
                            string newPath = path + newFileName;
                            if (!System.IO.File.Exists(newPath))
                            {
                                System.IO.File.Copy(MyList1[i1].Path, newPath);
                            }

                            str1 = str1 + compName + ".Изображения.Добавить(Ф.Картинка(\u0022" + newPath + "\u0022));";
                            if (i1 == 0)
                            {
                                str1 = str1.TrimStart(' ');
                            }
                            if (i1 < (MyList1.Count - 1))
                            {
                                str1 = str1 + Environment.NewLine;
                            }
                        }
                        AddToScript(str1);
                    }
                }
                return;
            }
            if (valueName == "СписокИзображений")
            {
                if (val != null)
                {
                    System.Windows.Forms.ImageList ImageList1 = (System.Windows.Forms.ImageList)val.ImageList;
                    ImageList SimilarObj = (ImageList)OneScriptFormsDesigner.RevertSimilarObj(ImageList1);
                    AddToScript(compName + ".СписокИзображений = " + compValue + ";");
                }
                return;
            }
            if (valueName == "СписокБольшихИзображений")
            {
                if (val != null)
                {
                    System.Windows.Forms.ImageList ImageList1 = (System.Windows.Forms.ImageList)val.LargeImageList;
                    ImageList SimilarObj = (ImageList)OneScriptFormsDesigner.RevertSimilarObj(ImageList1);
                    AddToScript(compName + ".СписокБольшихИзображений = " + compValue + ";" + Environment.NewLine);
                }
                return;
            }
            if (valueName == "СписокМаленькихИзображений")
            {
                if (val != null)
                {
                    System.Windows.Forms.ImageList ImageList1 = (System.Windows.Forms.ImageList)val.SmallImageList;
                    ImageList SimilarObj = (ImageList)OneScriptFormsDesigner.RevertSimilarObj(ImageList1);
                    AddToScript(compName + ".СписокМаленькихИзображений = " + compValue + ";" + Environment.NewLine);
                }
                return;
            }
            if (valueName == "Изображение" || valueName == "ФоновоеИзображение")
            {
                if (compValue != "System.Drawing.Bitmap ()")
                {
                    string FileName = OneScriptFormsDesigner.ParseBetween(compValue, "(", ")");
                    string newFileName = FileName.Substring(FileName.LastIndexOf('\\') + 1);
                    string newPath = path + newFileName;
                    if (!System.IO.File.Exists(newPath))
                    {
                        System.IO.File.Copy(FileName, newPath);
                    }
                    AddToScript(compName + "." + valueName + " = Ф.Картинка(\u0022" + newPath + "\u0022);");
                }
                return;
            }
            if (valueName == "ВыделенныйДиапазон")
            {
                if (val != null)
                {
                    SelectionRange SelectionRange1 = (SelectionRange)val.SelectionRange;
                    string str1 = compName + "." + valueName + " = " + "Ф.ВыделенныйДиапазон(Дата(" +
                                                        SelectionRange1.Start.ToString("yyyy") + ", " +
                                                        SelectionRange1.Start.ToString("MM") + ", " +
                                                        SelectionRange1.Start.ToString("dd") + ", " +
                                                        SelectionRange1.Start.ToString("HH") + ", " +
                                                        SelectionRange1.Start.ToString("mm") + ", " +
                                                        SelectionRange1.Start.ToString("ss") + "), " +
                                                        "Дата(" +
                                                        SelectionRange1.End.ToString("yyyy") + ", " +
                                                        SelectionRange1.End.ToString("MM") + ", " +
                                                        SelectionRange1.End.ToString("dd") + ", " +
                                                        SelectionRange1.End.ToString("HH") + ", " +
                                                        SelectionRange1.End.ToString("mm") + ", " +
                                                        SelectionRange1.End.ToString("ss") + "));";
                    AddToScript(str1);
                }
                return;
            }
            //если это цвет
            if (valueName == "ОсновнойЦвет" ||
                valueName == "ОсновнойЦветЗаголовков" ||
                valueName == "ПрозрачныйЦвет" ||
                valueName == "Цвет" ||
                valueName == "ЦветАктивнойСсылки" ||
                valueName == "ЦветПосещеннойСсылки" ||
                valueName == "ЦветСетки" ||
                valueName == "ЦветСсылки" ||
                valueName == "ЦветФона" ||
                valueName == "ЦветФонаЗаголовка" ||
                valueName == "ЦветФонаЗаголовков" ||
                valueName == "ЦветФонаНечетныхСтрок" ||
                valueName == "ЦветФонаСеткиДанных")
            {
                string str1 = "";
                if (val != null)
                {
                    if (val.ToString() == "Color [Empty]")
                    {
                        str1 = compName + "." + valueName + " = Ф.Цвет(0, 0, 0);";
                    }
                    else if (compValue.Contains(";"))
                    {
                        str1 = compName + "." + valueName + " = Ф.Цвет(" + compValue.Replace(";", ",") + ");";
                    }
                    else
                    {
                        str1 = compName + "." + valueName + " = Ф.Цвет(\u0022" + compValue + "\u0022);";
                    }
                }
                AddToScript(str1);
                return;
            }
            //если compValue это контрол
            if (valueName == "КнопкаОтмена" ||
                valueName == "КнопкаПринять" ||
                (valueName == "Значение" && val.GetType() == typeof(osfDesigner.UserControl)) ||
                valueName == "ВыбранныйОбъект")
            {
                AddToScript(compName + "." + valueName + " = " + compValue + ";");
                return;
            }
            //если compValue это строка для ФорматированноеПолеВвода (RichTextBox)
            if (valueName == "Текст" && val.GetType() == typeof(osfDesigner.RichTextBox))
            {
                compValue = compValue.Replace("\n", "\u0022 + Ф.Окружение().НоваяСтрока + \u0022");
                AddToScript(compName + "." + valueName + " = \u0022" + compValue + "\u0022;");
                return;
            }
            //если compValue это событие
            if (valueName == "ВыбранныйЭлементСеткиИзменен" ||
                valueName == "ВыделениеИзменено" ||
                valueName == "ДатаВыбрана" ||
                valueName == "ДатаИзменена" ||
                valueName == "ДвойноеНажатие" ||
                valueName == "Закрыта" ||
                valueName == "ЗначениеИзменено" ||
                valueName == "ЗначениеСвойстваИзменено" ||
                valueName == "ИндексВыбранногоИзменен" ||
                valueName == "КлавишаВверх" ||
                valueName == "КлавишаВниз" ||
                valueName == "КлавишаНажата" ||
                valueName == "КолонкаНажатие" ||
                valueName == "МышьНадЭлементом" ||
                valueName == "МышьПокинулаЭлемент" ||
                valueName == "Нажатие" ||
                valueName == "ПередРазвертыванием" ||
                valueName == "ПередРедактированиемНадписи" ||
                valueName == "ПоложениеИзменено" ||
                valueName == "ПометкаИзменена" ||
                valueName == "ПослеВыбора" ||
                valueName == "ПослеРедактированияНадписи" ||
                valueName == "ПриАктивизации" ||
                valueName == "ПриАктивизацииЭлемента" ||
                valueName == "ПриВходе" ||
                valueName == "ПриВыпадении" ||
                valueName == "ПриДеактивации" ||
                valueName == "ПриЗагрузке" ||
                valueName == "ПриЗадержкеМыши" ||
                valueName == "ПриЗакрытии" ||
                valueName == "ПриИзменении" ||
                valueName == "ПриНажатииКнопки" ||
                valueName == "ПриНажатииКнопкиМыши" ||
                valueName == "ПриОтпусканииМыши" ||
                valueName == "ПриПереименовании" ||
                valueName == "ПриПеремещении" ||
                valueName == "ПриПеремещенииМыши" ||
                valueName == "ПриПерерисовке" ||
                valueName == "ПриПотереФокуса" ||
                valueName == "ПриПрокручивании" ||
                valueName == "ПриСоздании" ||
                valueName == "ПриСрабатыванииТаймера" ||
                valueName == "ПриУдалении" ||
                valueName == "ПриУходе" ||
                valueName == "РазмерИзменен" ||
                valueName == "СсылкаНажата" ||
                valueName == "ТекстИзменен" ||
                valueName == "ТекущаяЯчейкаИзменена" ||
                valueName == "ЭлементДобавлен" ||
                valueName == "ЭлементПомечен" ||
                valueName == "ЭлементУдален")
            {
                string strNameProc = compValue.Replace("(", "").Replace(")", "");
                AddToScript(compName + "." + valueName + " = \u0022" + strNameProc + "\u0022;");
                return;
            }
            //если compValue это строка
            if (valueName == "ВыбранныйПуть" ||
                valueName == "Заголовок" ||
                valueName == "ИмяСтиля" ||
                valueName == "ИмяФайла" ||
                valueName == "НачальныйКаталог" ||
                valueName == "Описание" ||
                valueName == "ПолныйПуть" ||
                valueName == "ПользовательскийФормат" ||
                valueName == "Путь" ||
                valueName == "РазделительПути" ||
                valueName == "РасширениеПоУмолчанию" ||
                valueName == "СимволПароля" ||
                valueName == "Текст" ||
                valueName == "ТекстЗаголовка" ||
                valueName == "ТекстПодсказки" ||
                valueName == "Фильтр")
            {
                compValue = compValue.Replace(Environment.NewLine, "\u0022 + Ф.Окружение().НоваяСтрока + \u0022");
                AddToScript(compName + "." + valueName + " = \u0022" + compValue + "\u0022;");
                return;
            }
            //если compValue это число
            if (valueName == "АвтоЗадержка" ||
                valueName == "АвтоЗадержкаПоказа" ||
                valueName == "БольшоеИзменение" ||
                valueName == "ВысотаЭлемента" ||
                valueName == "ГоризонтальнаяМера" ||
                valueName == "ЗадержкаОчередногоПоказа" ||
                valueName == "ЗадержкаПоявления" ||
                (valueName == "Значение" && val.GetType() == typeof(osfDesigner.HProgressBar)) ||
                (valueName == "Значение" && val.GetType() == typeof(osfDesigner.VProgressBar)) ||
                (valueName == "Значение" && val.GetType() == typeof(osfDesigner.HScrollBar)) ||
                (valueName == "Значение" && val.GetType() == typeof(osfDesigner.VScrollBar)) ||
                (valueName == "Значение" && val.GetType() == typeof(osfDesigner.NumericUpDown)) ||
                (valueName == "Индекс" && val.GetType() != typeof(osfDesigner.MyTreeNode)) ||
                valueName == "ИндексВыбранногоИзображения" ||
                valueName == "ИндексИзображения" ||
                valueName == "ИндексФильтра" ||
                valueName == "Интервал" ||
                valueName == "МаксимальнаяДлина" ||
                valueName == "Максимум" ||
                valueName == "МаксимумВыбранных" ||
                valueName == "МаксимумЭлементов" ||
                valueName == "МалоеИзменение" ||
                valueName == "Масштаб" ||
                valueName == "МинимальнаяШирина" ||
                valueName == "МинимальноеРасстояние" ||
                (valueName == "МинимальныйРазмер" && compName.Contains("Splitter")) ||
                valueName == "Минимум" ||
                valueName == "Отступ" ||
                valueName == "ОтступМаркера" ||
                valueName == "ПорядокОбхода" ||
                valueName == "ПорядокСлияния" ||
                valueName == "ПравоеОграничение" ||
                valueName == "ПредпочтительнаяВысотаСтрок" ||
                valueName == "ПредпочтительнаяШиринаСтолбцов" ||
                valueName == "Разрядность" ||
                valueName == "Увеличение" ||
                valueName == "Ширина" ||
                valueName == "ШиринаВыпадающегоСписка" ||
                valueName == "ШиринаЗаголовковСтрок" ||
                valueName == "ШиринаКолонки")
            {
                AddToScript(compName + "." + valueName + " = " + compValue.Replace(",", ".") + ";");
                return;
            }
            //если compValue это Размер
            if (valueName == "МаксимальныйРазмер" ||
                valueName == "МинимальныйРазмер" ||
                valueName == "Размер" ||
                valueName == "РазмерИзображения" ||
                valueName == "РазмерКнопки" ||
                valueName == "РазмерПоляАвтоПрокрутки" ||
                valueName == "РазмерЭлемента")
            {
                if (compName.Contains("Календарь") ||
                    compName.Contains("Вкладка"))
                {
                    return;
                }
                string str1 = compValue.Replace("{Ширина=", "");
                str1 = str1.Replace("Высота=", "");
                str1 = str1.Replace("}", "");
                string[] separators = new string[] { ", " };
                string[] result = str1.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                str1 = "Ф.Размер(" + result[0] + ", " + result[1] + ");";
                AddToScript(compName + "." + valueName + " = " + str1);
                return;
            }
            //если compValue это Точка
            if (valueName == "Положение")
            {
                if (val.GetType() == typeof(Form))
                {
                    if (((Form)val).StartPosition == FormStartPosition.Вручную)
                    {
                        string str1 = compValue.Replace("{Икс=", "");
                        str1 = str1.Replace("Игрек=", "");
                        str1 = str1.Replace("}", "");
                        string[] separators = new string[] { ", " };
                        string[] result = str1.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        str1 = "Ф.Точка(" + result[0] + ", " + result[1] + ");";
                        AddToScript(compName + "." + valueName + " = " + str1);
                    }
                }
                else
                {
                    string str1 = compValue.Replace("{Икс=", "");
                    str1 = str1.Replace("Игрек=", "");
                    str1 = str1.Replace("}", "");
                    string[] separators = new string[] { ", " };
                    string[] result = str1.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    str1 = "Ф.Точка(" + result[0] + ", " + result[1] + ");";
                    AddToScript(compName + "." + valueName + " = " + str1);
                }
                return;
            }
            //если это перечисление
            if (valueName == "ТипСлияния")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.СлияниеМеню." + compValue + ";");
                return;
            }
            if (valueName == "АвтоРазмер")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.АвтоРазмерПанелиСтрокиСостояния." + compValue + ";");
                return;
            }
            if (valueName == "Якорь")
            {
                string str1 = "";
                string[] separators = new string[] { ", " };
                string[] result = compValue.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < result.Length; i++)
                {
                    str1 = str1 + "Ф.СтилиПривязки." + result[i] + " + ";
                }
                str1 = str1 + ";";
                str1 = str1.Replace(" + ;", ";");
                AddToScript(compName + "." + valueName + " = " + str1);
                return;
            }
            if (valueName == "НачальноеПоложение")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.НачальноеПоложениеФормы." + compValue + ";");
                return;
            }
            if (valueName == "Формат")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.ФорматПоляКалендаря." + compValue + ";");
                return;
            }
            if (valueName == "Курсор")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.Курсоры()." + compValue + ";");
                return;
            }
            if (valueName == "ГлубинаЦвета" || valueName == "СочетаниеКлавиш")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф." + valueName + "." + compValue + ";");
                return;
            }
            if (valueName == "Стиль")
            {
                AddToScript(compName + ".Стиль = Ф.СтильКнопокПанелиИнструментов." + compValue + ";");
                return;
            }
            if ((valueName == "Оформление" && (val.GetType() == typeof(osfDesigner.RadioButton) || val.GetType() == typeof(osfDesigner.CheckBox))) ||
                valueName == "ПлоскийСтиль" ||
                valueName == "ПоведениеСсылки" ||
                valueName == "ПолосыПрокрутки" ||
                valueName == "РегистрСимволов" ||
                valueName == "РежимВыбора" ||
                valueName == "РежимОтображения" ||
                valueName == "РежимРисования" ||
                valueName == "РезультатДиалога" ||
                valueName == "СортировкаСвойств" ||
                valueName == "СостояниеФлажка" ||
                valueName == "СтильГраницыФормы")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф." + valueName + "." + compValue + ";");
                return;
            }
            if (valueName == "СтильГраницы")
            {
                if (val.GetType() == typeof(osfDesigner.StatusBarPanel))
                {
                    AddToScript(compName + "." + valueName + " = " + "Ф.СтильГраницыПанелиСтрокиСостояния." + compValue + ";");
                    return;
                }
                else
                {
                    AddToScript(compName + "." + valueName + " = " + "Ф." + valueName + "." + compValue + ";");
                    return;
                }
            }
            if (valueName == "Сортировка")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.ПорядокСортировки." + compValue + ";");
                return;
            }
            if (valueName == "СтильЗаголовка")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.СтильЗаголовкаКолонки." + compValue + ";");
                return;
            }
            if (valueName == "Активация")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.АктивацияЭлемента." + compValue + ";");
                return;
            }
            if (valueName == "РазмещениеФоновогоИзображения")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.РазмещениеИзображения." + compValue + ";");
                return;
            }
            if (valueName == "ВыравниваниеПриРаскрытии")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.ЛевоеПравоеВыравнивание." + compValue + ";");
                return;
            }
            if (valueName == "СтильВыпадающегоСписка")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.СтильПоляВыбора." + compValue + ";");
                return;
            }
            if (valueName == "ВыравниваниеПометки")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.ВыравниваниеСодержимого." + compValue + ";");
                return;
            }
            if (valueName == "РежимМасштабирования" && val.GetType() == typeof(osfDesigner.PictureBox))
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.РежимРазмераПоляКартинки." + compValue + ";");
                return;
            }
            if (valueName == "РежимМасштабирования" && val.GetType() == typeof(osfDesigner.TabControl))
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.РежимРазмераВкладок." + compValue + ";");
                return;
            }
            if (valueName == "Оформление" && val.GetType() == typeof(osfDesigner.ToolBar))
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.ОформлениеПанелиИнструментов." + compValue + ";");
                return;
            }
            if (valueName == "Выравнивание" && val.GetType() == typeof(osfDesigner.TabControl))
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.ВыравниваниеВкладок." + compValue + ";");
                return;
            }
            if (valueName == "Выравнивание" && val.GetType() == typeof(osfDesigner.ListView))
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.ВыравниваниеВСпискеЭлементов." + compValue + ";");
                return;
            }
            if (valueName == "Выравнивание" && (val.GetType() == typeof(osfDesigner.DataGridBoolColumn) ||
                val.GetType() == typeof(osfDesigner.DataGridTextBoxColumn) ||
                val.GetType() == typeof(osfDesigner.DataGridComboBoxColumnStyle)))
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.ГоризонтальноеВыравнивание." + compValue + ";");
                return;
            }
            if (valueName == "ФильтрУведомлений")
            {
                string str1 = "";
                string[] separators = new string[] { ", " };
                string[] result = compValue.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < result.Length; i++)
                {
                    str1 = str1 + "Ф.ФильтрыУведомления." + result[i] + " + ";
                }
                str1 = str1 + ";";
                str1 = str1.Replace(" + ;", ";");
                AddToScript(compName + "." + valueName + " = " + str1);
                return;
            }
            if (valueName == "ВыравниваниеТекста" && val.GetType() == typeof(osfDesigner.ColumnHeader))
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.ГоризонтальноеВыравнивание." + compValue + ";");
                return;
            }
            if (valueName == "ВыравниваниеТекста" && val.GetType() == typeof(osfDesigner.ToolBar))
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.ВыравниваниеТекстаВПанелиИнструментов." + compValue + ";");
                return;
            }
            if ((valueName == "ВыравниваниеИзображения" ||
                valueName == "ВыравниваниеТекста"
                ) && (
                val.GetType() == typeof(osfDesigner.Button) ||
                val.GetType() == typeof(osfDesigner.RadioButton) ||
                val.GetType() == typeof(osfDesigner.CheckBox) ||
                val.GetType() == typeof(osfDesigner.Label)))
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.ВыравниваниеСодержимого." + compValue + ";");
                return;
            }
            if (valueName == "ПервыйДеньНедели")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.День." + compValue + ";");
                return;
            }
            if (valueName == "КорневойКаталог")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.ОсобаяПапка." + compValue + ";");
                return;
            }
            if (valueName == "Стыковка")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.СтильСтыковки." + compValue + ";");
                return;
            }
            if (valueName == "Значок")
            {
                AddToScript(compName + "." + valueName + " = " + "Ф.Значок(\u0022" + compValue + "\u0022);");
                return;
            }
            if (valueName == "МаксимальнаяДата" ||
                valueName == "МинимальнаяДата" ||
                valueName == "ТекущаяДата")
            {
                DateTime DateTime1 = System.DateTime.Parse(compValue);
                AddToScript(compName + "." + valueName + " = " + "Дата(" +
                    DateTime1.ToString("yyyy") + ", " +
                    DateTime1.ToString("MM") + ", " +
                    DateTime1.ToString("dd") + ", " +
                    DateTime1.ToString("HH") + ", " +
                    DateTime1.ToString("mm") + ", " +
                    DateTime1.ToString("ss") + ");");
                return;
            }
        }

        private static void AddToScript(string str)
        {
            if (OneScriptFormsDesigner.ParseBetween(Template1, null, str) == null)
            {
                Template1 = Template1.Replace(@"[Свойства>]", str + Environment.NewLine + @"[Свойства>]");
            }
        }

        private static void GetNodes(osfDesigner.MyTreeNode treeNode)
        {
            osfDesigner.MyTreeNode MyTreeNode1;
            for (int i = 0; i < treeNode.Nodes.Count; i++)
            {
                MyTreeNode1 = (osfDesigner.MyTreeNode)treeNode.Nodes[i];
                AddToScript(MyTreeNode1.Name + " = " + treeNode.Name + ".Узлы.Добавить(\u0022" + MyTreeNode1.Name + "\u0022);");
                PropComponent(MyTreeNode1);
                if (MyTreeNode1.Nodes.Count > 0)
                {
                    GetNodes(MyTreeNode1);
                }
            }
        }

        private static void GetNodes1(System.Windows.Forms.TreeView TreeView, ref System.Collections.ArrayList objArrayList2)
        {
            for (int i = 0; i < TreeView.Nodes.Count; i++)
            {
                System.Windows.Forms.TreeNode TreeNode1 = TreeView.Nodes[i];
                objArrayList2.Add(TreeNode1.Name);
                if (TreeNode1.Nodes.Count > 0)
                {
                    GetNodes2(TreeNode1, ref objArrayList2);
                }
            }
        }

        private static void GetNodes2(System.Windows.Forms.TreeNode treeNode, ref System.Collections.ArrayList objArrayList2)
        {
            for (int i = 0; i < treeNode.Nodes.Count; i++)
            {
                System.Windows.Forms.TreeNode TreeNode1 = treeNode.Nodes[i];
                objArrayList2.Add(TreeNode1.Name);
                if (TreeNode1.Nodes.Count > 0)
                {
                    GetNodes2(TreeNode1, ref objArrayList2);
                }
            }
        }

        private static void GetMenuItems(MenuItemEntry menuItem)
        {
            MenuItemEntry MenuItemEntry1;
            for (int i = 0; i < menuItem.MenuItems.Count; i++)
            {
                MenuItemEntry1 = OneScriptFormsDesigner.RevertSimilarObj(menuItem.MenuItems[i]);
                PropertyDescriptor pd = TypeDescriptor.GetProperties(MenuItemEntry1.Parent)["Name"];
                string strParent = (string)pd.GetValue(MenuItemEntry1.Parent);

                string strName = MenuItemEntry1.Name.Contains("Сепаратор") ? "-" : MenuItemEntry1.Text;
                AddToScript(MenuItemEntry1.Name + " = " + strParent + ".ЭлементыМеню.Добавить(Ф.ЭлементМеню(\u0022" + strName + "\u0022));");
                PropComponent(MenuItemEntry1);
                if (MenuItemEntry1.MenuItems.Count > 0)
                {
                    GetMenuItems(MenuItemEntry1);
                }
            }
        }

        private static void PropComponent(dynamic comp)
        {
            PropertyInfo[] myPropertyInfo = comp.GetType().GetProperties();
            for (int i = 0; i < myPropertyInfo.Length; i++)
            {
                string valueName = osfDesigner.OneScriptFormsDesigner.GetDisplayName(comp, myPropertyInfo[i].Name);
                if (valueName != "" && !((valueName == "(Name)") || (valueName == "Прямоугольник")))
                {
                    PropertyDescriptor pd = TypeDescriptor.GetProperties(comp)[myPropertyInfo[i].Name];
                    try
                    {
                        string compValue = osfDesigner.OneScriptFormsDesigner.ObjectConvertToString(pd.GetValue(comp));
                        if (comp.GetType() == typeof(osfDesigner.DataGridTableStyle) ||
                            comp.GetType() == typeof(osfDesigner.DataGridBoolColumn) ||
                            comp.GetType() == typeof(osfDesigner.DataGridTextBoxColumn) ||
                            comp.GetType() == typeof(osfDesigner.DataGridComboBoxColumnStyle))
                        {
                            RequiredDefaultValuesValues(comp, comp.NameStyle, valueName, compValue);
                        }
                        else
                        {
                            RequiredDefaultValuesValues(comp, comp.Name, valueName, compValue);
                        }
                    }
                    catch { }
                }
            }
        }
    }
}
