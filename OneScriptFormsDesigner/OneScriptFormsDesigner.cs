using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    [ContextClass("ДизайнерФормДляОдноСкрипта", "OneScriptFormsDesigner")]
    public class OneScriptFormsDesigner : AutoContext<OneScriptFormsDesigner>
    {
        public static Dictionary<object, object> dictionary = new Dictionary<object, object>(); // Хранит связь исходного объекта с его дублером.
        public static Dictionary<string, string> dictionaryDesignerTabName = new Dictionary<string, string>(); // Хранит имя вкладок дизайнера.
        public static Dictionary<object, object> dictionaryDesignerTabRootComponent = new Dictionary<object, object>(); // Хранит связь rootComponent и создаваемой для него вкладки дризайнера.
        public static Dictionary<DesignSurfaceExt2, string> dictionaryDesignSurfaceState = new Dictionary<DesignSurfaceExt2, string>(); // Хранит связь между поверхностью дизайнера и снимком свойств его компонентов.
        public static Dictionary<System.Windows.Forms.TabPage, bool> dictionaryTabPageChanged = new Dictionary<System.Windows.Forms.TabPage, bool>(); // Хранит связь между вкладкой дизайнера и статусом его измененности.
        public static int tic = 0; // Счетчик для правильной работы TabControl, пропуск двух шагов по созданию дизайнером двух вкладок по умолчанию.
        public static bool block1 = false; // Тригер для блокировки выделения объекта на форме.
        public static bool block2 = false; // Тригер для блокировки проверки измененности формы. Если true - не проверять.

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("User32")]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        [ScriptConstructor]
        public static IRuntimeContextInstance Constructor()
        {
            return new OneScriptFormsDesigner();
        }

        [ContextMethod("Дизайнер", "Designer")]
        public void Designer(string p1 = null)
        {
            if (p1 == "ВосстановитьКонсоль")
            {
                RestoreConsole();
            }
            else if (p1 == "СкрытьКонсоль")
            {
                HideConsole();
            }
            else
            {
                MinimizedConsole();
            }
            var thread = new Thread(() =>
            {
                Program Program1 = new Program();
                Program1.Main();
            }
           );
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }
	
        [ContextMethod("ПолучитьЗначениеСвойства", "GetPropertyValue")]
        public IValue GetPropertyValue(string controlName, string propertyName)
        {
            osfDesigner.pDesignerMainForm Form1 = (osfDesigner.pDesignerMainForm)osfDesigner.Program.pDesignerMainForm1;
            IValue propertyValue = (IValue)Form1.Invoke(new osfDesigner.pDesignerMainForm.GetPropertyValue(Form1.GetPropertyValueMethod), controlName, propertyName);
            return propertyValue;
        }

        [ContextMethod("ПолучитьТипСвойства", "GetPropertyType")]
        public string GetPropertyType(string controlName, string propertyName)
        {
            osfDesigner.pDesignerMainForm Form1 = (osfDesigner.pDesignerMainForm)osfDesigner.Program.pDesignerMainForm1;
            string typeName = (string)Form1.Invoke(new osfDesigner.pDesignerMainForm.GetPropertyType(Form1.GetPropertyTypeMethod), controlName, propertyName);
            return typeName;
        }

        [ContextMethod("ЗакрытьДизайнер", "CloseDesigner")]
        public void CloseDesigner()
        {
            osfDesigner.pDesignerMainForm Form1 = (osfDesigner.pDesignerMainForm)osfDesigner.Program.pDesignerMainForm1;
            Form1.Invoke(new osfDesigner.pDesignerMainForm.CloseDesigner(Form1.CloseDesignerMethod));
        }

        [ContextMethod("ОткрытьФорму", "LoadForm")]
        public void LoadForm(string fileName)
        {
            osfDesigner.pDesignerMainForm Form1 = (osfDesigner.pDesignerMainForm)osfDesigner.Program.pDesignerMainForm1;
            Form1.Invoke(new osfDesigner.pDesignerMainForm.LoadForm(Form1.LoadFormMethod), fileName);
        }

        [ContextMethod("ЗапуститьСценарий", "RunScript")]
        public void RunScript()
        {
            osfDesigner.pDesignerMainForm Form1 = (osfDesigner.pDesignerMainForm)osfDesigner.Program.pDesignerMainForm1;
            Form1.Invoke(new osfDesigner.pDesignerMainForm.RunScript(Form1.RunScriptMethod));
        }

        [ContextMethod("СформироватьСценарий", "GenerateScript")]
        public void GenerateScript(string fileName)
        {
            osfDesigner.pDesignerMainForm Form1 = (osfDesigner.pDesignerMainForm)osfDesigner.Program.pDesignerMainForm1;
            Form1.Invoke(new osfDesigner.pDesignerMainForm.GenerateScript(Form1.GenerateScriptMethod), fileName);
        }

        [ContextMethod("СохранитьФорму", "UnloadForm")]
        public void UnloadForm(string fileName)
        {
            osfDesigner.pDesignerMainForm Form1 = (osfDesigner.pDesignerMainForm)osfDesigner.Program.pDesignerMainForm1;
            Form1.Invoke(new osfDesigner.pDesignerMainForm.UnloadForm(Form1.UnloadFormMethod), fileName);
        }

        [ContextMethod("УстановитьСвойство", "SetProperty")]
        public void SetProperty(string controlName, string propertyName, IValue propertyValue)
        {
            osfDesigner.pDesignerMainForm Form1 = (osfDesigner.pDesignerMainForm)osfDesigner.Program.pDesignerMainForm1;
            Form1.Invoke(new osfDesigner.pDesignerMainForm.SetProperty(Form1.SetPropertyMethod), controlName, propertyName, propertyValue);
        }

        [ContextMethod("ДобавитьКонтрол", "AddControl")]
        public string AddControl(string controlName)
        {
            osfDesigner.pDesignerMainForm Form1 = (osfDesigner.pDesignerMainForm)osfDesigner.Program.pDesignerMainForm1;
            string name1 = (string)Form1.Invoke(new osfDesigner.pDesignerMainForm.AddControl(Form1.AddControlMethod), controlName);
            return name1;
        }
	
        [ContextMethod("УдалитьКонтролы", "RemoveControls")]
        public void RemoveControls()
        {
            osfDesigner.pDesignerMainForm Form1 = (osfDesigner.pDesignerMainForm)osfDesigner.Program.pDesignerMainForm1;
            string name1 = (string)Form1.Invoke(new osfDesigner.pDesignerMainForm.RemoveControls(Form1.RemoveControlsMethod));
        }

        [ContextProperty("ВизуальныеСтилиФормВключены", "FormVisualStylesTurnOn")]
        public bool FormVisualStylesTurnOn
        {
            get
            {
                if ((bool)osfDesigner.Properties.Settings.Default["visualSyleForms"])
                {
                    return true;
                }
                return false;
            }
        }	

        [ContextMethod("ДизайнерОткрыт", "DesignerOpen")]
        public bool DesignerOpen()
        {
            if (osfDesigner.Program.pDesignerMainForm1.GetmainForm().Visible)
            {
                return true;
            }
            return false;
        }	
	
        public static void PassProperties(dynamic p1, dynamic p2)
        {
            // p1 - исходный объект (OriginalObj).
            // p2 - дублёр исходного объекта (SimilarObj), для отображения свойств в сетке свойств.
            PropertyInfo[] PropertyInfo = p2.GetType().GetProperties();
            for (int i = 0; i < PropertyInfo.Length; i++)
            {
                try
                {
                    if (p1.GetType() == typeof(System.Windows.Forms.TabPage))
                    {
                        if (PropertyInfo[i].Name !=  "Parent")
                        {
                            p2.GetType().GetProperty(PropertyInfo[i].Name).SetValue(p2, p1.GetType().GetProperty(PropertyInfo[i].Name).GetValue(p1));
                        }
                    }
                    else
                    {
                        p2.GetType().GetProperty(PropertyInfo[i].Name).SetValue(p2, p1.GetType().GetProperty(PropertyInfo[i].Name).GetValue(p1));
                    }
                }
                catch { }
            }
        }

        public static void ReturnProperties(dynamic p1, dynamic p2)
        {
            // p1 - исходный объект (OriginalObj).
            // p2 - дублёр исходного объекта (SimilarObj), для отображения свойств в сетке свойств.
            PropertyInfo[] PropertyInfo = p1.GetType().GetProperties();
            for (int i = 0; i < PropertyInfo.Length; i++)
            {
                try
                {
                    p1.GetType().GetProperty(PropertyInfo[i].Name).SetValue(p1, p2.GetType().GetProperty(PropertyInfo[i].Name).GetValue(p2));
                }
                catch { }
            }
        }

        private static void GetNodes1(System.Windows.Forms.TreeView TreeView, ref ArrayList ArrayList, ref int max)
        {
            int num = 0;
            for (int i = 0; i < TreeView.Nodes.Count; i++)
            {
                TreeNode TreeNode1 = TreeView.Nodes[i];
                ArrayList.Add(TreeNode1.Name);

                num = Int32.Parse(ParseBetween(TreeNode1.Name, "Узел", null));
                if ((num + 1) > max)
                {
                    max = num + 1;
                }

                if (TreeNode1.Nodes.Count > 0)
                {
                    GetNodes2(TreeNode1, ref ArrayList, ref max);
                }
            }
        }

        private static void GetNodes2(TreeNode treeNode, ref ArrayList ArrayList, ref int max)
        {
            int num = 0;
            for (int i = 0; i < treeNode.Nodes.Count; i++)
            {
                TreeNode TreeNode1 = treeNode.Nodes[i];
                ArrayList.Add(TreeNode1.Name);

                num = Int32.Parse(ParseBetween(TreeNode1.Name, "Узел", null));
                if ((num + 1) > max)
                {
                    max = num + 1;
                }

                if (TreeNode1.Nodes.Count > 0)
                {
                    GetNodes2(TreeNode1, ref ArrayList, ref max);
                }
            }
        }

        public static string RevertNodeName(System.Windows.Forms.TreeView p1)
        {
            string name = p1.Name + "Узел";
            int max = 0;
            string newName = name + Convert.ToString(max);
            ArrayList ArrayList1 = new ArrayList();
            GetNodes1(p1, ref ArrayList1, ref max);

            for (int i = -1; i < max; i++)
            {
                newName = name + Convert.ToString(i + 1);
                if (!ArrayList1.Contains(newName))
                {
                    return newName;
                }
            }
            return newName;
        }

        public static string RevertDesignerTabName(string p1)
        {
            string p2 = "Вкладка" + (dictionaryDesignerTabName.Count).ToString() + "(" + p1 + ")";
            dictionaryDesignerTabName.Add(p2, p1);
            return p2;
        }
	
        public static string RevertStatusBarPanelName(StatusBar p1)
        {
            string name = p1.Name + "Панель";
            int max = 0;
            int num = 0;
            string newName = name + Convert.ToString(max);
            ArrayList ArrayList1 = new ArrayList();
            for (int i = 0; i < p1.Panels.Count; i++)
            {
                string NameItem = p1.Panels[i].Name;
                if (NameItem != null)
                {
                    if (NameItem != "")
                    {
                        ArrayList1.Add(NameItem);
                        num = Int32.Parse(ParseBetween(NameItem, "Панель", null));
                        if ((num + 1) > max)
                        {
                            max = num + 1;
                        }
                    }
                }
            }

            for (int i = -1; i < max; i++)
            {
                newName = name + Convert.ToString(i + 1);
                if (!ArrayList1.Contains(newName))
                {
                    return newName;
                }
            }
            return newName;
        }

        public static string RevertColumnHeaderName(ListView p1)
        {
            string name = p1.Name + "Колонка";
            int max = 0;
            int num = 0;
            string newName = name + Convert.ToString(max);
            ArrayList ArrayList1 = new ArrayList();
            for (int i = 0; i < p1.Columns.Count; i++)
            {
                string NameItem = p1.Columns[i].Name;
                if (NameItem != null)
                {
                    if (NameItem != "")
                    {
                        ArrayList1.Add(NameItem);
                        num = Int32.Parse(ParseBetween(NameItem, "Колонка", null));
                        if ((num + 1) > max)
                        {
                            max = num + 1;
                        }
                    }
                }
            }

            for (int i = -1; i < max; i++)
            {
                newName = name + Convert.ToString(i + 1);
                if (!ArrayList1.Contains(newName))
                {
                    return newName;
                }
            }
            return newName;
        }

        public static string RevertListViewItemName(ListView p1)
        {
            string name = p1.Name + "Элемент";
            int max = 0;
            int num = 0;
            string newName = name + Convert.ToString(max);
            ArrayList ArrayList1 = new ArrayList();
            for (int i = 0; i < p1.Items.Count; i++)
            {
                string NameItem = p1.Items[i].Name;
                if (NameItem != null)
                {
                    if (NameItem != "")
                    {
                        ArrayList1.Add(NameItem);
                        string str1 = ParseBetween(NameItem, "СписокЭлементов", null);
                        num = Int32.Parse(ParseBetween(str1, "Элемент", null));
                        if ((num + 1) > max)
                        {
                            max = num + 1;
                        }
                    }
                }
            }

            for (int i = -1; i < max; i++)
            {
                newName = name + Convert.ToString(i + 1);
                if (!ArrayList1.Contains(newName))
                {
                    return newName;
                }
            }
            return newName;
        }

        public static string RevertListViewSubItemName(ListViewItem p1)
        {
            string name = p1.Name + "Подэлемент";
            int max = 0;
            int num = 0;
            string newName = name + Convert.ToString(max);
            ArrayList ArrayList1 = new ArrayList();
            for (int i = 0; i < p1.SubItems.Count; i++)
            {
                string NameItem = p1.SubItems[i].Name;
                if (NameItem != null)
                {
                    if (NameItem != "" && NameItem.Contains("Подэлемент"))
                    {
                        ArrayList1.Add(NameItem);
                        num = Int32.Parse(ParseBetween(NameItem, "Подэлемент", null));
                        if ((num + 1) > max)
                        {
                            max = num + 1;
                        }
                    }
                }
            }

            for (int i = -1; i < max; i++)
            {
                newName = name + Convert.ToString(i + 1);
                if (!ArrayList1.Contains(newName))
                {
                    return newName;
                }
            }
            return newName;
        }

        public static void BypassMainMenu2(Menu Menu1, ref ArrayList ArrayList, ref int max)
        {
            for (int i = 0; i < Menu1.MenuItems.Count; i++)
            {
                int num = 0;
                Menu CurrentMenuItem1 = (Menu)Menu1.MenuItems[i];
                string Name = CurrentMenuItem1.Name;
                string fragment = Name.Replace("ГлавноеМеню", "флажок");
                if (Name != "" && !fragment.Contains("Меню"))
                {
                    ArrayList.Add(CurrentMenuItem1.Name);
                    num = Int32.Parse(ParseBetween(CurrentMenuItem1.Name, "Сепаратор", null));
                    if ((num + 1) > max)
                    {
                        max = num + 1;
                    }
                }

                if (CurrentMenuItem1.MenuItems.Count > 0)
                {
                    BypassMainMenu2(CurrentMenuItem1, ref ArrayList, ref max);
                }
            }
        }

        public static void BypassMainMenu(Menu Menu1, ref ArrayList ArrayList, ref int max)
        {
            for (int i = 0; i < Menu1.MenuItems.Count; i++)
            {
                int num = 0;
                Menu CurrentMenuItem1 = (Menu)Menu1.MenuItems[i];
                string Name = CurrentMenuItem1.Name;

                if (Name != "" && !Name.Contains("Сепаратор"))
                {
                    ArrayList.Add(CurrentMenuItem1.Name);
                    num = Int32.Parse(ParseBetween(CurrentMenuItem1.Name.Replace("ГлавноеМеню", ""), "Меню", null));
                    if ((num + 1) > max)
                    {
                        max = num + 1;
                    }
                }

                if (CurrentMenuItem1.MenuItems.Count > 0)
                {
                    BypassMainMenu(CurrentMenuItem1, ref ArrayList, ref max);
                }
            }
        }

        public static string RevertMenuName(MainMenu p1)
        {
            string name = p1.Name + "Меню";
            int max = 0;
            string newName = name + Convert.ToString(max);
            ArrayList ArrayList1 = new ArrayList();
            BypassMainMenu(p1, ref ArrayList1, ref max);

            for (int i = -1; i < max; i++)
            {
                newName = name + Convert.ToString(i + 1);
                if (!ArrayList1.Contains(newName))
                {
                    return newName;
                }
            }
            return newName;
        }

        public static string RevertSeparatorName(MainMenu p1)
        {
            string name = p1.Name + "Сепаратор";
            int max = 0;
            string newName = name + Convert.ToString(max);
            ArrayList ArrayList1 = new ArrayList();
            BypassMainMenu2(p1, ref ArrayList1, ref max);

            for (int i = -1; i < max; i++)
            {
                newName = name + Convert.ToString(i + 1);
                if (!ArrayList1.Contains(newName))
                {
                    return newName;
                }
            }
            return newName;
        }

        public static string RevertToolBarButtonName(ToolBar p1)
        {
            string name = p1.Name + "Кн";
            int max = 0;
            int num = 0;
            string newName = name + Convert.ToString(max);
            ArrayList ArrayList1 = new ArrayList();
            for (int i = 0; i < p1.Buttons.Count; i++)
            {
                string Name = p1.Buttons[i].Name;
                if (Name != null)
                {
                    if (Name != "")
                    {
                        ArrayList1.Add(Name);

                        num = Int32.Parse(ParseBetween(Name, "Кн", null));
                        if ((num + 1) > max)
                        {
                            max = num + 1;
                        }
                    }
                }
            }

            for (int i = -1; i < max; i++)
            {
                newName = name + Convert.ToString(i + 1);
                if (!ArrayList1.Contains(newName))
                {
                    return newName;
                }
            }
            return newName;
        }

        public static string RevertDataGridTableStyleName(DataGrid p1)
        {
            string name = p1.Name + "Стиль";
            int max = 0;
            int num = 0;
            string newName = name + Convert.ToString(max);
            ArrayList ArrayList1 = new ArrayList();
            for (int i = 0; i < p1.TableStyles.Count; i++)
            {
                string NameStyle = RevertSimilarObj(p1.TableStyles[i]).NameStyle;
                if (NameStyle != null)
                {
                    ArrayList1.Add(NameStyle);
                    num = Int32.Parse(ParseBetween(NameStyle, "Стиль", null));
                    if ((num + 1) > max)
                    {
                        max = num + 1;
                    }
                }
            }

            for (int i = -1; i < max; i++)
            {
                newName = name + Convert.ToString(i + 1);
                if (!ArrayList1.Contains(newName))
                {
                    return newName;
                }
            }
            return newName;
        }

        public static string RevertDataGridColumnStyleName(dynamic p1, dynamic p2)
        {
            // p1 - стиль таблицы сетки данных.
            // p2 - стиль колонки сетки данных.
            if (p2.GetType() == typeof(osfDesigner.DataGridBoolColumn))
            {
                osfDesigner.DataGridTableStyle DataGridTableStyle1 = RevertSimilarObj(p1);
                string name = DataGridTableStyle1.NameStyle + p2.NameStyle + "СтильКолонкиБулево";
                int max = 0;
                int num = 0;
                string newName = name + Convert.ToString(max);
                ArrayList ArrayList1 = new ArrayList();
                for (int i = 0; i < DataGridTableStyle1.GridColumnStyles.Count; i++)
                {
                    string NameStyle = ((dynamic)DataGridTableStyle1.GridColumnStyles[i]).NameStyle;
                    if (NameStyle != null)
                    {
                        if (NameStyle.Contains("СтильКолонкиБулево"))
                        {
                            ArrayList1.Add(NameStyle);
                            num = Int32.Parse(ParseBetween(NameStyle, "СтильКолонкиБулево", null));
                            if ((num + 1) > max)
                            {
                                max = num + 1;
                            }
                        }
                    }
                }

                for (int i = -1; i < max; i++)
                {
                    newName = name + Convert.ToString(i + 1);
                    if (!ArrayList1.Contains(newName))
                    {
                        return newName;
                    }
                }
                return newName;
            }
            else if (p2.GetType() == typeof(osfDesigner.DataGridTextBoxColumn))
            {
                osfDesigner.DataGridTableStyle DataGridTableStyle1 = RevertSimilarObj(p1);
                string name = DataGridTableStyle1.NameStyle + p2.NameStyle + "СтильКолонкиПолеВвода";
                int max = 0;
                int num = 0;
                string newName = name + Convert.ToString(max);
                ArrayList ArrayList1 = new ArrayList();
                for (int i = 0; i < DataGridTableStyle1.GridColumnStyles.Count; i++)
                {
                    string NameStyle = ((dynamic)DataGridTableStyle1.GridColumnStyles[i]).NameStyle;
                    if (NameStyle != null)
                    {
                        if (NameStyle.Contains("СтильКолонкиПолеВвода"))
                        {
                            ArrayList1.Add(NameStyle);
                            num = Int32.Parse(ParseBetween(NameStyle, "СтильКолонкиПолеВвода", null));
                            if ((num + 1) > max)
                            {
                                max = num + 1;
                            }
                        }
                    }
                }

                for (int i = -1; i < max; i++)
                {
                    newName = name + Convert.ToString(i + 1);
                    if (!ArrayList1.Contains(newName))
                    {
                        return newName;
                    }
                }
                return newName;
            }
            else if (p2.GetType() == typeof(osfDesigner.DataGridComboBoxColumnStyle))
            {
                osfDesigner.DataGridTableStyle DataGridTableStyle1 = RevertSimilarObj(p1);
                string name = DataGridTableStyle1.NameStyle + p2.NameStyle + "СтильКолонкиПолеВыбора";
                int max = 0;
                int num = 0;
                string newName = name + Convert.ToString(max);
                ArrayList ArrayList1 = new ArrayList();
                for (int i = 0; i < DataGridTableStyle1.GridColumnStyles.Count; i++)
                {
                    string NameStyle = ((dynamic)DataGridTableStyle1.GridColumnStyles[i]).NameStyle;
                    if (NameStyle != null)
                    {
                        if (NameStyle.Contains("СтильКолонкиПолеВыбора"))
                        {
                            ArrayList1.Add(NameStyle);
                            num = Int32.Parse(ParseBetween(NameStyle, "СтильКолонкиПолеВыбора", null));
                            if ((num + 1) > max)
                            {
                                max = num + 1;
                            }
                        }
                    }
                }

                for (int i = -1; i < max; i++)
                {
                    newName = name + Convert.ToString(i + 1);
                    if (!ArrayList1.Contains(newName))
                    {
                        return newName;
                    }
                }
                return newName;
            }
            return null;
        }
	
        public static string RevertDataGridViewColumnName(dynamic p1, dynamic p2)
        {
            // p1 - таблица.
            // p2 - колонка таблицы.
            osfDesigner.DataGridView DataGridView1 = p1;
            string name = "";
            if (p2.GetType() == typeof(osfDesigner.DataGridViewTextBoxColumn))
            {
                name = "КолонкаПолеВвода";
            }
            else if (p2.GetType() == typeof(osfDesigner.DataGridViewLinkColumn))
            {
                name = "КолонкаСсылка";
            }
            else if (p2.GetType() == typeof(osfDesigner.DataGridViewImageColumn))
            {
                name = "КолонкаКартинка";
            }
            else if (p2.GetType() == typeof(osfDesigner.DataGridViewComboBoxColumn))
            {
                name = "КолонкаПолеВыбора";
            }
            else if (p2.GetType() == typeof(osfDesigner.DataGridViewCheckBoxColumn))
            {
                name = "КолонкаФлажок";
            }
            else if (p2.GetType() == typeof(osfDesigner.DataGridViewButtonColumn))
            {
                name = "КолонкаКнопка";
            }
            else
            {
                return null;
            }

            int max = 0;
            int num = 0;
            string newName = name + Convert.ToString(max);
            ArrayList ArrayList1 = new ArrayList();
            for (int i = 0; i < DataGridView1.Columns.Count; i++)
            {
                string Name = ((dynamic)DataGridView1.Columns[i]).Name;
                if (Name != null)
                {
                    if (Name.Contains(name))
                    {
                        ArrayList1.Add(Name);
                        num = Int32.Parse(ParseBetween(Name, name, null));
                        if ((num + 1) > max)
                        {
                            max = num + 1;
                        }
                    }
                }
            }

            for (int i = -1; i < max; i++)
            {
                newName = name + Convert.ToString(i + 1);
                if (!ArrayList1.Contains(newName))
                {
                    return newName;
                }
            }
            return newName;
        }

        public static void AddToDictionary(dynamic p1, dynamic p2)
        {
            // p1 - исходный объект (OriginalObj).
            // p2 - дублёр исходного объекта (SimilarObj), для отображения свойств в сетке свойств.
            if (!dictionary.ContainsKey(p1))
            {
                dictionary.Add(p1, p2);
            }
        }

        public static void AddToDictionaryDesignerTabRootComponent(dynamic p1, dynamic p2)
        {
            // p1 - RootComponent, форма.
            // p2 - DesignerTab, вкладка дизайнера для этой формы.
            if (!dictionaryDesignerTabRootComponent.ContainsKey(p1))
            {
                dictionaryDesignerTabRootComponent.Add(p1, p2);
            }
        }

        public static dynamic RevertDesignerTab(dynamic rootComponent)
        {
            try
            {
                return dictionaryDesignerTabRootComponent[rootComponent];
            }
            catch
            {
                return null;
            }
        }

        public static dynamic RevertSimilarObj(dynamic OriginalObj)
        {
            try
            {
                return dictionary[OriginalObj];
            }
            catch
            {
                return null;
            }
        }

        public static dynamic RevertOriginalObj(dynamic SimilarObj)
        {
            foreach (KeyValuePair<object, object> keyValue in dictionary)
            {
                if (keyValue.Value.Equals(SimilarObj))
                {
                    return keyValue.Key;
                }
            }
            return null;
        }
	
        public static void ChangeVisibilityBasedOnMode(ref object[] v, List<string> list)
        {
            List<string> PropertiesToHide = list;
            foreach (var vObject in v)
            {
                PropertyInfo[] myPropertyInfo = vObject.GetType().GetProperties();
                var properties = myPropertyInfo;
                foreach (var p in properties)
                {
                    foreach (string hideProperty in PropertiesToHide)
                    {
                        if (p.Name.ToLower() == hideProperty.ToLower())
                        {
                            setBrowsableProperty(hideProperty, false, vObject);
                        }
                    }
                }
            }
        }

        private static void setBrowsableProperty(string strPropertyName, bool bIsBrowsable, object vObject)
        {
            try
            {
                PropertyDescriptor theDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(vObject.GetType())[strPropertyName];
                BrowsableAttribute theDescriptorBrowsableAttribute = (BrowsableAttribute)theDescriptor.Attributes[typeof(BrowsableAttribute)];
                FieldInfo isBrowsable = theDescriptorBrowsableAttribute.GetType().GetField("Browsable", BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Instance);
                isBrowsable.SetValue(theDescriptorBrowsableAttribute, bIsBrowsable);
            }
            catch { }
        }

        public static string ObjectConvertToString(object p1)
        {
            object obj = p1;
            if (obj != null)
            {
                Type objType = p1.GetType();
                TypeConverter Converter1 = System.ComponentModel.TypeDescriptor.GetConverter(objType);
                if (objType == typeof(bool))
                {
                    return MyBooleanConverter.ConvertToString(obj);
                }
                else if (objType == typeof(Size))
                {
                    return MySizeConverter.ConvertToString(obj);
                }
                else if (objType == typeof(Cursor))
                {
                    return MyCursorConverter.ConvertToString(obj);
                }
                else if (objType == typeof(Color))
                {
                    return MyColorConverter.ConvertToString(Converter1.ConvertToString(obj));
                }
                else if (objType == typeof(Point))
                {
                    return MyLocationConverter.ConvertToString(obj);
                }
                else if (objType == typeof(Font))
                {
                    return MyFontConverter.ConvertToString(Converter1.ConvertToString(obj));
                }
                else if (objType == typeof(Bitmap))
                {
                    return Converter1.ConvertToString((Bitmap)obj) + " (" + ((Bitmap)obj).Tag + ")";
                }
                else if (objType == typeof(MyIcon))
                {
                    return MyIconConverter.ConvertToString(obj);
                }
                return Converter1.ConvertToString(obj);
            }
            return "";
        }

        public void HideConsole()
        {
            ShowWindow(GetConsoleWindow(), 0);
        }

        public void MinimizedConsole()
        {
            ShowWindow(GetConsoleWindow(), 7);
        }

        public void RestoreConsole()
        {
            ShowWindow(GetConsoleWindow(), 9);
        }
	
        public static string GetDisplayName(object value, string memberName)
        {
            string DisplayName = "";
            try
            {
                PropertyDescriptor PropertyDescriptorCollection1 = System.ComponentModel.TypeDescriptor.GetProperties(value.GetType())[memberName];
                AttributeCollection attributes = System.ComponentModel.TypeDescriptor.GetProperties(value.GetType())[memberName].Attributes;
                DisplayNameAttribute myDisplayNameAttribute = (DisplayNameAttribute)attributes[typeof(DisplayNameAttribute)];
                DisplayName = myDisplayNameAttribute.DisplayName;
            }
            catch { }
            return DisplayName;
        }

        public static string GetPropName(object value, string displayName)
        {
            PropertyInfo[] properties = value.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    if (attr.GetType() == typeof(DisplayNameAttribute))
                    {
                        DisplayNameAttribute DisplayNameAttribute1 = (DisplayNameAttribute)attr;
                        if (DisplayNameAttribute1 != null)
                        {
                            var attributeName = DisplayNameAttribute1.DisplayName;
                            if (attributeName == displayName)
                            {
                                return prop.Name;
                            }
                        }
                    }
                }
            }
            return null;
        }
	
        public static Dictionary<string, string> colors = new Dictionary<string, string>
            {
                {"Aquamarine", "Аквамариновый"},
                {"Аквамариновый", "Aquamarine"},
                {"AntiqueWhite", "АнтичныйБелый"},
                {"АнтичныйБелый", "AntiqueWhite"},
                {"Beige", "Бежевый"},
                {"Бежевый", "Beige"},
                {"WhiteSmoke", "БелаяДымка"},
                {"БелаяДымка", "WhiteSmoke"},
                {"White", "Белый"},
                {"Белый", "White"},
                {"NavajoWhite", "БелыйНавахо"},
                {"БелыйНавахо", "NavajoWhite"},
                {"Turquoise", "Бирюзовый"},
                {"Бирюзовый", "Turquoise"},
                {"Bisque", "Бисквитный"},
                {"Бисквитный", "Bisque"},
                {"PaleTurquoise", "БледноБирюзовый"},
                {"БледноБирюзовый", "PaleTurquoise"},
                {"Cornsilk", "БледноЖелтый"},
                {"БледноЖелтый", "Cornsilk"},
                {"PaleGreen", "БледноЗеленый"},
                {"БледноЗеленый", "PaleGreen"},
                {"PaleGoldenrod", "БледноЗолотистый"},
                {"БледноЗолотистый", "PaleGoldenrod"},
                {"CornflowerBlue", "Васильковый"},
                {"Васильковый", "CornflowerBlue"},
                {"HotTrack", "Выделенный"},
                {"Выделенный", "HotTrack"},
                {"ControlLightLight", "ВыделеныйЭлемент"},
                {"ВыделеныйЭлемент", "ControlLightLight"},
                {"DeepPink", "ГлубокийРозовый"},
                {"ГлубокийРозовый", "DeepPink"},
                {"DeepSkyBlue", "Голубой"},
                {"Голубой", "DeepSkyBlue"},
                {"ActiveBorder", "ГраницаАктивного"},
                {"ГраницаАктивного", "ActiveBorder"},
                {"InactiveBorder", "ГраницаНеактивного"},
                {"ГраницаНеактивного", "InactiveBorder"},
                {"LightSlateGray", "ГрифельноСерый"},
                {"ГрифельноСерый", "LightSlateGray"},
                {"SlateBlue", "ГрифельноСиний"},
                {"ГрифельноСиний", "SlateBlue"},
                {"YellowGreen", "ЖелтоЗеленый"},
                {"ЖелтоЗеленый", "YellowGreen"},
                {"Yellow", "Желтый"},
                {"Желтый", "Yellow"},
                {"ActiveCaption", "ЗаголовокАктивного"},
                {"ЗаголовокАктивного", "ActiveCaption"},
                {"InactiveCaption", "ЗаголовокНеактивного"},
                {"ЗаголовокНеактивного", "InactiveCaption"},
                {"DodgerBlue", "ЗащитноСиний"},
                {"ЗащитноСиний", "DodgerBlue"},
                {"SpringGreen", "ЗеленаяВесна"},
                {"ЗеленаяВесна", "SpringGreen"},
                {"LawnGreen", "ЗеленаяЛужайка"},
                {"ЗеленаяЛужайка", "LawnGreen"},
                {"SeaGreen", "ЗеленоеМоре"},
                {"ЗеленоеМоре", "SeaGreen"},
                {"GreenYellow", "ЗеленоЖелтый"},
                {"ЗеленоЖелтый", "GreenYellow"},
                {"Green", "Зеленый"},
                {"Зеленый", "Green"},
                {"LimeGreen", "ЗеленыйЛайм"},
                {"ЗеленыйЛайм", "LimeGreen"},
                {"ForestGreen", "ЗеленыйЛесной"},
                {"ЗеленыйЛесной", "ForestGreen"},
                {"Goldenrod", "Золотарник"},
                {"Золотарник", "Goldenrod"},
                {"Gold", "Золотой"},
                {"Золотой", "Gold"},
                {"Indigo", "Индиго"},
                {"Индиго", "Indigo"},
                {"IndianRed", "ИндийскийКрасный"},
                {"ИндийскийКрасный", "IndianRed"},
                {"Firebrick", "Кирпичный"},
                {"Кирпичный", "Firebrick"},
                {"SaddleBrown", "КожаноКоричневый"},
                {"КожаноКоричневый", "SaddleBrown"},
                {"Coral", "Коралловый"},
                {"Коралловый", "Coral"},
                {"Maroon", "КоричневоМалиновый"},
                {"КоричневоМалиновый", "Maroon"},
                {"Brown", "Коричневый"},
                {"Коричневый", "Brown"},
                {"RoyalBlue", "КоролевскийСиний"},
                {"КоролевскийСиний", "RoyalBlue"},
                {"Red", "Красный"},
                {"Красный", "Red"},
                {"Crimson", "Кровавый"},
                {"Кровавый", "Crimson"},
                {"Lavender", "Лаванда"},
                {"Лаванда", "Lavender"},
                {"Azure", "Лазурный"},
                {"Лазурный", "Azure"},
                {"Lime", "Лайм"},
                {"Лайм", "Lime"},
                {"PaleVioletRed", "Лиловый"},
                {"Лиловый", "PaleVioletRed"},
                {"Control", "ЛицеваяЭлемента"},
                {"ЛицеваяЭлемента", "Control"},
                {"Salmon", "Лососевый"},
                {"Лососевый", "Salmon"},
                {"Linen", "Льняной"},
                {"Льняной", "Linen"},
                {"Magenta", "Малиновый"},
                {"Малиновый", "Magenta"},
                {"Honeydew", "Медовый"},
                {"Медовый", "Honeydew"},
                {"Menu", "Меню"},
                {"Меню", "Menu"},
                {"Moccasin", "Мокасиновый"},
                {"Мокасиновый", "Moccasin"},
                {"Aqua", "МорскаяВолна"},
                {"МорскаяВолна", "Aqua"},
                {"SeaShell", "МорскаяРакушка"},
                {"МорскаяРакушка", "SeaShell"},
                {"MintCream", "МятноКремовый"},
                {"МятноКремовый", "MintCream"},
                {"SkyBlue", "НебесноГолубой"},
                {"НебесноГолубой", "SkyBlue"},
                {"LightSkyBlue", "НебесноГолубойСветлый"},
                {"НебесноГолубойСветлый", "LightSkyBlue"},
                {"OliveDrab", "НежноОливковый"},
                {"НежноОливковый", "OliveDrab"},
                {"Window", "Окно"},
                {"Окно", "Window"},
                {"Olive", "Оливковый"},
                {"Оливковый", "Olive"},
                {"OrangeRed", "ОранжевоКрасный"},
                {"ОранжевоКрасный", "OrangeRed"},
                {"Orange", "Оранжевый"},
                {"Оранжевый", "Orange"},
                {"Orchid", "Орхидея"},
                {"Орхидея", "Orchid"},
                {"Sienna", "Охра"},
                {"Охра", "Sienna"},
                {"PeachPuff", "Персиковый"},
                {"Персиковый", "PeachPuff"},
                {"SandyBrown", "Песочный"},
                {"Песочный", "SandyBrown"},
                {"PapayaWhip", "ПобегПапайи"},
                {"ПобегПапайи", "PapayaWhip"},
                {"Info", "Подсказка"},
                {"Подсказка", "Info"},
                {"ScrollBar", "ПолосаПрокрутки"},
                {"ПолосаПрокрутки", "ScrollBar"},
                {"MidnightBlue", "ПолуночноСиний"},
                {"ПолуночноСиний", "MidnightBlue"},
                {"PowderBlue", "ПороховаяСинь"},
                {"ПороховаяСинь", "PowderBlue"},
                {"GhostWhite", "ПризрачноБелый"},
                {"ПризрачноБелый", "GhostWhite"},
                {"Transparent", "Прозрачный"},
                {"Прозрачный", "Transparent"},
                {"Purple", "Пурпурный"},
                {"Пурпурный", "Purple"},
                {"IsEmpty", "Пусто"},
                {"Пусто", "IsEmpty"},
                {"Wheat", "Пшеничный"},
                {"Пшеничный", "Wheat"},
                {"AppWorkspace", "РабочаяОбластьПриложения"},
                {"РабочаяОбластьПриложения", "AppWorkspace"},
                {"Desktop", "РабочийСтол"},
                {"РабочийСтол", "Desktop"},
                {"WindowFrame", "РамкаОкна"},
                {"РамкаОкна", "WindowFrame"},
                {"RosyBrown", "РозовоКоричневый"},
                {"РозовоКоричневый", "RosyBrown"},
                {"LavenderBlush", "РозовоЛавандовый"},
                {"РозовоЛавандовый", "LavenderBlush"},
                {"Pink", "Розовый"},
                {"Розовый", "Pink"},
                {"LightSeaGreen", "СветлаяМорскаяВолна"},
                {"СветлаяМорскаяВолна", "LightSeaGreen"},
                {"LightYellow", "СветлоЖелтый"},
                {"СветлоЖелтый", "LightYellow"},
                {"LightGoldenrodYellow", "СветлоЖелтыйЗолотистый"},
                {"СветлоЖелтыйЗолотистый", "LightGoldenrodYellow"},
                {"LightGreen", "СветлоЗеленый"},
                {"СветлоЗеленый", "LightGreen"},
                {"LightCoral", "СветлоКоралловый"},
                {"СветлоКоралловый", "LightCoral"},
                {"Peru", "СветлоКоричневый"},
                {"СветлоКоричневый", "Peru"},
                {"BlanchedAlmond", "СветлоКремовый"},
                {"СветлоКремовый", "BlanchedAlmond"},
                {"LemonChiffon", "СветлоЛимонный"},
                {"СветлоЛимонный", "LemonChiffon"},
                {"LightPink", "СветлоРозовый"},
                {"СветлоРозовый", "LightPink"},
                {"LightGray", "СветлоСерый"},
                {"СветлоСерый", "LightGray"},
                {"LightBlue", "СветлоСиний"},
                {"СветлоСиний", "LightBlue"},
                {"LightCyan", "СветлыйЗеленоватоГолубой"},
                {"СветлыйЗеленоватоГолубой", "LightCyan"},
                {"LightSalmon", "СветлыйЛососевый"},
                {"СветлыйЛососевый", "LightSalmon"},
                {"ControlLight", "СветлыйЭлемента"},
                {"СветлыйЭлемента", "ControlLight"},
                {"Silver", "Серебристый"},
                {"Серебристый", "Silver"},
                {"CadetBlue", "СероСиний"},
                {"СероСиний", "CadetBlue"},
                {"Gray", "Серый"},
                {"Серый", "Gray"},
                {"GrayText", "СерыйТекст"},
                {"СерыйТекст", "GrayText"},
                {"SlateGray", "СерыйШифер"},
                {"СерыйШифер", "SlateGray"},
                {"LightSteelBlue", "СинеГолубойСоСтальнымОттенком"},
                {"СинеГолубойСоСтальнымОттенком", "LightSteelBlue"},
                {"Teal", "СинеЗеленый"},
                {"СинеЗеленый", "Teal"},
                {"BlueViolet", "СинеФиолетовый"},
                {"СинеФиолетовый", "BlueViolet"},
                {"Blue", "Синий"},
                {"Синий", "Blue"},
                {"AliceBlue", "СинийЭлис"},
                {"СинийЭлис", "AliceBlue"},
                {"SteelBlue", "СиняяСталь"},
                {"СиняяСталь", "SteelBlue"},
                {"Plum", "Сливовый"},
                {"Сливовый", "Plum"},
                {"Ivory", "СлоноваяКость"},
                {"СлоноваяКость", "Ivory"},
                {"OldLace", "СтароеКружево"},
                {"СтароеКружево", "OldLace"},
                {"HighlightText", "ТекстВыбранных"},
                {"ТекстВыбранных", "HighlightText"},
                {"ActiveCaptionText", "ТекстЗаголовкаАктивного"},
                {"ТекстЗаголовкаАктивного", "ActiveCaptionText"},
                {"InactiveCaptionText", "ТекстЗаголовкаНеактивного"},
                {"ТекстЗаголовкаНеактивного", "InactiveCaptionText"},
                {"MenuText", "ТекстМеню"},
                {"ТекстМеню", "MenuText"},
                {"WindowText", "ТекстОкна"},
                {"ТекстОкна", "WindowText"},
                {"InfoText", "ТекстПодсказки"},
                {"ТекстПодсказки", "InfoText"},
                {"ControlText", "ТекстЭлемента"},
                {"ТекстЭлемента", "ControlText"},
                {"DarkSalmon", "ТемнаяЛососина"},
                {"ТемнаяЛососина", "DarkSalmon"},
                {"DarkSeaGreen", "ТемнаяМорскаяВолна"},
                {"ТемнаяМорскаяВолна", "DarkSeaGreen"},
                {"DarkOrchid", "ТемнаяОрхидея"},
                {"ТемнаяОрхидея", "DarkOrchid"},
                {"ControlDarkDark", "ТемнаяТеньЭлемента"},
                {"ТемнаяТеньЭлемента", "ControlDarkDark"},
                {"DarkSlateGray", "ТемноАспидныйСерый"},
                {"ТемноАспидныйСерый", "DarkSlateGray"},
                {"DarkCyan", "ТемноГолубой"},
                {"ТемноГолубой", "DarkCyan"},
                {"DarkGreen", "ТемноЗеленый"},
                {"ТемноЗеленый", "DarkGreen"},
                {"DarkRed", "ТемноКрасный"},
                {"ТемноКрасный", "DarkRed"},
                {"DarkTurquoise", "ТемноМандариновый"},
                {"ТемноМандариновый", "DarkTurquoise"},
                {"DarkMagenta", "ТемноПурпурный"},
                {"ТемноПурпурный", "DarkMagenta"},
                {"DarkGray", "ТемноСерый"},
                {"ТемноСерый", "DarkGray"},
                {"DarkBlue", "ТемноСиний"},
                {"ТемноСиний", "DarkBlue"},
                {"DarkViolet", "ТемноФиолетовый"},
                {"ТемноФиолетовый", "DarkViolet"},
                {"DarkSlateBlue", "ТемныйГрифельноСиний"},
                {"ТемныйГрифельноСиний", "DarkSlateBlue"},
                {"DarkGoldenrod", "ТемныйЗолотой"},
                {"ТемныйЗолотой", "DarkGoldenrod"},
                {"DarkOliveGreen", "ТемныйОливковоЗеленый"},
                {"ТемныйОливковоЗеленый", "DarkOliveGreen"},
                {"DarkOrange", "ТемныйОранжевый"},
                {"ТемныйОранжевый", "DarkOrange"},
                {"DarkKhaki", "ТемныйХаки"},
                {"ТемныйХаки", "DarkKhaki"},
                {"ControlDark", "ТеньЭлемента"},
                {"ТеньЭлемента", "ControlDark"},
                {"Tomato", "Томатный"},
                {"Томатный", "Tomato"},
                {"Gainsboro", "ТуманноБелый"},
                {"ТуманноБелый", "Gainsboro"},
                {"MistyRose", "ТусклоРозовый"},
                {"ТусклоРозовый", "MistyRose"},
                {"DimGray", "ТусклоСерый"},
                {"ТусклоСерый", "DimGray"},
                {"MediumAquamarine", "УмеренныйАквамарин"},
                {"УмеренныйАквамарин", "MediumAquamarine"},
                {"MediumTurquoise", "УмеренныйБирюзовый"},
                {"УмеренныйБирюзовый", "MediumTurquoise"},
                {"MediumSpringGreen", "УмеренныйВесеннеЗеленый"},
                {"УмеренныйВесеннеЗеленый", "MediumSpringGreen"},
                {"MediumSlateBlue", "УмеренныйГрифельноСиний"},
                {"УмеренныйГрифельноСиний", "MediumSlateBlue"},
                {"MediumSeaGreen", "УмеренныйМорскаяВолна"},
                {"УмеренныйМорскаяВолна", "MediumSeaGreen"},
                {"MediumOrchid", "УмеренныйОрхидея"},
                {"УмеренныйОрхидея", "MediumOrchid"},
                {"MediumBlue", "УмеренныйСиний"},
                {"УмеренныйСиний", "MediumBlue"},
                {"MediumVioletRed", "УмеренныйФиолетовоКрасный"},
                {"УмеренныйФиолетовоКрасный", "MediumVioletRed"},
                {"MediumPurple", "УмеренныйФиолетовый"},
                {"УмеренныйФиолетовый", "MediumPurple"},
                {"Violet", "Фиолетовый"},
                {"Фиолетовый", "Violet"},
                {"Highlight", "ФонВыбранных"},
                {"ФонВыбранных", "Highlight"},
                {"Fuchsia", "Фуксия"},
                {"Фуксия", "Fuchsia"},
                {"Khaki", "Хаки"},
                {"Хаки", "Khaki"},
                {"Tan", "ЦветЗагара"},
                {"ЦветЗагара", "Tan"},
                {"FloralWhite", "ЦветочноБелый"},
                {"ЦветочноБелый", "FloralWhite"},
                {"BurlyWood", "ЦветПлотнойДревесины"},
                {"ЦветПлотнойДревесины", "BurlyWood"},
                {"Navy", "ЦветФормыМорскихОфицеров"},
                {"ЦветФормыМорскихОфицеров", "Navy"},
                {"Cyan", "Циан"},
                {"Циан", "Cyan"},
                {"Black", "Черный"},
                {"Черный", "Black"},
                {"Thistle", "Чертополох"},
                {"Чертополох", "Thistle"},
                {"Chartreuse", "Шартрез"},
                {"Шартрез", "Chartreuse"},
                {"Chocolate", "Шоколадный"},
                {"Шоколадный", "Chocolate"},
                {"Snow", "ЯркийБелый"},
                {"ЯркийБелый", "Snow"},
                {"HotPink", "ЯркоРозовый"},
                {"ЯркоРозовый", "HotPink"}
            };
	
        public static Dictionary<string, string> namesEnRu = new Dictionary<string, string>
            {
                {"RadioButton", "Переключатель"},
                {"Button", "Кнопка"},
                {"CheckBox", "Флажок"},
                {"ColorDialog", "ДиалогВыбораЦвета"},
                {"ComboBox", "ПолеВыбора"},
                {"DataGridView", "Таблица"},
                {"DataGrid", "СеткаДанных"},
                {"DateTimePicker", "ПолеКалендаря"},
                {"FileSystemWatcher", "НаблюдательФайловойСистемы"},
                {"FolderBrowserDialog", "ДиалогВыбораКаталога"},
                {"FontDialog", "ДиалогВыбораШрифта"},
                {"GroupBox", "РамкаГруппы"},
                {"HProgressBar", "ИндикаторГоризонтальный"},
                {"HScrollBar", "ГоризонтальнаяПрокрутка"},
                {"ImageList", "СписокИзображений"},
                {"LinkLabel", "НадписьСсылка"},
                {"Label", "Надпись"},
                {"ListBox", "ПолеСписка"},
                {"ListView", "СписокЭлементов"},
                {"MainMenu", "ГлавноеМеню"},
                {"MaskedTextBox", "МаскаПоляВвода"},
                {"MonthCalendar", "Календарь"},
                {"NotifyIcon", "ЗначокУведомления"},
                {"NumericUpDown", "РегуляторВверхВниз"},
                {"OpenFileDialog", "ДиалогОткрытияФайла"},
                {"Panel", "Панель"},
                {"PictureBox", "ПолеКартинки"},
                {"VProgressBar", "ИндикаторВертикальный"},
                {"ProgressBar", "Индикатор"},
                {"PropertyGrid", "СеткаСвойств"},
                {"RichTextBox", "ФорматированноеПолеВвода"},
                {"SaveFileDialog", "ДиалогСохраненияФайла"},
                {"Splitter", "Разделитель"},
                {"StatusBar", "СтрокаСостояния"},
                {"TabControl", "ПанельВкладок"},
                {"TabPage", "Вкладка"},
                {"TextBox", "ПолеВвода"},
                {"Timer", "Таймер"},
                {"ToolBar", "ПанельИнструментов"},
                {"ToolTip", "Подсказка"},
                {"TreeView", "Дерево"},
                {"UserControl", "ПользовательскийЭлементУправления"},
                {"VScrollBar", "ВертикальнаяПрокрутка"}
            };

        public static Dictionary<string, string> namesRuEn = new Dictionary<string, string>
            {
                {"Переключатель", "RadioButton"},
                {"Кнопка", "Button"},
                {"Флажок", "CheckBox"},
                {"ДиалогВыбораЦвета", "ColorDialog"},
                {"ПолеВыбора", "ComboBox"},
                {"Таблица", "DataGridView"},
                {"СеткаДанных", "DataGrid"},
                {"ПолеКалендаря", "DateTimePicker"},
                {"НаблюдательФайловойСистемы", "FileSystemWatcher"},
                {"ДиалогВыбораКаталога", "FolderBrowserDialog"},
                {"ДиалогВыбораШрифта", "FontDialog"},
                {"РамкаГруппы", "GroupBox"},
                {"ИндикаторГоризонтальный", "HProgressBar"},
                {"ГоризонтальнаяПрокрутка", "HScrollBar"},
                {"СписокИзображений", "ImageList"},
                {"НадписьСсылка", "LinkLabel"},
                {"Надпись", "Label"},
                {"ПолеСписка", "ListBox"},
                {"СписокЭлементов", "ListView"},
                {"ГлавноеМеню", "MainMenu"},
                {"МаскаПоляВвода", "MaskedTextBox"},
                {"Календарь", "MonthCalendar"},
                {"ЗначокУведомления", "NotifyIcon"},
                {"РегуляторВверхВниз", "NumericUpDown"},
                {"ДиалогОткрытияФайла", "OpenFileDialog"},
                {"Панель", "Panel"},
                {"ПолеКартинки", "PictureBox"},
                {"ИндикаторВертикальный", "VProgressBar"},
                {"Индикатор", "ProgressBar"},
                {"СеткаСвойств", "PropertyGrid"},
                {"ФорматированноеПолеВвода", "RichTextBox"},
                {"ДиалогСохраненияФайла", "SaveFileDialog"},
                {"Разделитель", "Splitter"},
                {"СтрокаСостояния", "StatusBar"},
                {"ПанельВкладок", "TabControl"},
                {"Вкладка", "TabPage"},
                {"ПолеВвода", "TextBox"},
                {"Таймер", "Timer"},
                {"ПанельИнструментов", "ToolBar"},
                {"Подсказка", "ToolTip"},
                {"Дерево", "TreeView"},
                {"ПользовательскийЭлементУправления", "UserControl"},
                {"ВертикальнаяПрокрутка", "VScrollBar"}
            };

        public static Dictionary<string, string> namesEnum = new Dictionary<string, string>
            {
                {"АвтоРазмерПанелиСтрокиСостояния", "StatusBarPanelAutoSize"},
                {"АктивацияЭлемента", "ItemActivation"},
                {"ВыравниваниеВСпискеЭлементов", "ListViewAlignment"},
                {"ВыравниваниеВкладок", "TabAlignment"},
                {"ВыравниваниеСодержимого", "ContentAlignment"},
                {"ВыравниваниеСодержимогоЯчейки", "DataGridViewContentAlignment"},
                {"ВыравниваниеТекстаВПанелиИнструментов", "ToolBarTextAlign"},
                {"ГлубинаЦвета", "ColorDepth"},
                {"ГоризонтальноеВыравнивание", "HorizontalAlignment"},
                {"День", "Day"},
                {"ДеревоДействие", "TreeViewAction"},
                {"Звуки", "Sounds"},
                {"ЗначокОкнаСообщений", "MessageBoxIcon"},
                {"Клавиши", "Keys"},
                {"КнопкиМыши", "MouseButtons"},
                {"КнопкиОкнаСообщений", "MessageBoxButtons"},
                {"ЛевоеПравоеВыравнивание", "LeftRightAlignment"},
                {"НаблюдательИзмененияВида", "WatcherChangeTypes"},
                {"НачальноеПоложениеФормы", "FormStartPosition"},
                {"ОриентацияПолосы", "ScrollOrientation"},
                {"ОсобаяПапка", "SpecialFolder"},
                {"Оформление", "Appearance"},
                {"ОформлениеВкладок", "TabAppearance"},
                {"ОформлениеПанелиИнструментов", "ToolBarAppearance"},
                {"ПлоскийСтиль", "FlatStyle"},
                {"ПоведениеСсылки", "LinkLabelLinkBehavior"},
                {"ПозицияПоиска", "SeekOrigin"},
                {"ПолосыПрокрутки", "ScrollBars"},
                {"ПорядокСортировки", "SortOrder"},
                {"ПричинаЗакрытия", "CloseReason"},
                {"РазмещениеИзображения", "ImageLayout"},
                {"РазмещениеИзображенияЯчейки", "DataGridViewImageCellLayout"},	
                {"РегистрСимволов", "CharacterCasing"},
                {"РежимАвтоРазмераКолонки", "DataGridViewAutoSizeColumnMode"},
                {"РежимАвтоРазмераКолонок", "DataGridViewAutoSizeColumnsMode"},
                {"РежимАвтоРазмераСтрок", "DataGridViewAutoSizeRowsMode"},
                {"РежимВставки", "InsertKeyMode"},
                {"РежимВыбора", "SelectionMode"},
                {"РежимВыбораТаблицы", "DataGridViewSelectionMode"},
                {"РежимВысотыЗаголовковКолонок", "DataGridViewColumnHeadersHeightSizeMode"},
                {"РежимОтображения", "View"},
                {"РежимРазмераВкладок", "TabSizeMode"},
                {"РежимРазмераПоляКартинки", "PictureBoxSizeMode"},
                {"РежимРисования", "DrawMode"},
                {"РежимСортировки", "DataGridViewColumnSortMode"},	
                {"РежимШириныЗаголовковСтрок", "DataGridViewRowHeadersWidthSizeMode"},
                {"РезультатДиалога", "DialogResult"},
                {"СлияниеМеню", "MenuMerge"},
                {"СортировкаСвойств", "PropertySort"},
                {"СостояниеОкнаФормы", "FormWindowState"},
                {"СостояниеСтрокиДанных", "DataRowState"},
                {"СостояниеФлажка", "CheckState"},
                {"СочетаниеКлавиш", "Shortcut"},
                {"СтилиПривязки", "AnchorStyles"},
                {"СтильГраницы", "BorderStyle"},
                {"СтильГраницыПанелиСтрокиСостояния", "StatusBarPanelBorderStyle"},
                {"СтильГраницыФормы", "FormBorderStyle"},
                {"СтильЗаголовкаКолонки", "ColumnHeaderStyle"},
                {"СтильКнопокПанелиИнструментов", "ToolBarButtonStyle"},
                {"СтильОкнаПроцесса", "ProcessWindowStyle"},
                {"СтильПоляВыбора", "ComboBoxStyle"},
                {"СтильПоляВыбораЯчейки", "DataGridViewComboBoxDisplayStyle"},
                {"СтильСтыковки", "DockStyle"},
                {"СтильШрифта", "FontStyle"},
                {"СтильШтриховки", "HatchStyle"},
                {"СтильЭлементаУправления", "ControlStyles"},
                {"ТипДанных", "DataType"},
                {"ТипСобытияПрокрутки", "ScrollEventType"},
                {"ТипСортировки", "SortType"},
                {"ТипЭлементаСетки", "GridItemType"},
                {"ТриСостояния", "DataGridViewTriState"},
                {"ФильтрыУведомления", "NotifyFilters"},
                {"ФлагиМыши", "MouseFlags"},
                {"ФорматМаски", "MaskFormat"},
                {"ФорматПикселей", "PixelFormat"},
                {"ФорматПоляКалендаря", "FormatDateTimePicker"},
                {"ФорматированноеПолеВводаПоиск", "RichTextBoxFinds"},
                {"ФорматированноеПолеВводаТипыПотоков", "RichTextBoxStreamType"}
            };

        public static Dictionary<string, Cursor> namesCursorRuEn = new Dictionary<string, Cursor>
            {
                {"БезДвижения2D", Cursors.NoMove2D},
                {"БезДвиженияВертикально", Cursors.NoMoveVert},
                {"БезДвиженияГоризонтально", Cursors.NoMoveHoriz},
                {"ВРазделитель", Cursors.VSplit},
                {"ГРазделитель", Cursors.HSplit},
                {"КурсорВ", Cursors.PanEast},
                {"КурсорЗ", Cursors.PanWest},
                {"КурсорОжидания", Cursors.WaitCursor},
                {"КурсорС", Cursors.PanNorth},
                {"КурсорСВ", Cursors.PanNE},
                {"КурсорСЗ", Cursors.PanNW},
                {"КурсорЮ", Cursors.PanSouth},
                {"КурсорЮВ", Cursors.PanSE},
                {"КурсорЮЗ", Cursors.PanSW},
                {"Ладонь", Cursors.Hand},
                {"Луч", Cursors.IBeam},
                {"Нет", Cursors.No},
                {"Перекрестие", Cursors.Cross},
                {"ПоУмолчанию", Cursors.Default},
                {"ПриСтарте", Cursors.AppStarting},
                {"РазмерЗВ", Cursors.SizeWE},
                {"РазмерСВЮЗ", Cursors.SizeNESW},
                {"РазмерСЗЮВ", Cursors.SizeNWSE},
                {"РазмерСЮ", Cursors.SizeNS},
                {"РазмерЧетырехконечный", Cursors.SizeAll},
                {"Справка", Cursors.Help},
                {"Стрелка", Cursors.Arrow},
                {"СтрелкаВверх", Cursors.UpArrow}
            };

        public static Dictionary<string, string> namesCursorEnRu = new Dictionary<string, string>
            {
                {"NoMove2D", "БезДвижения2D"},
                {"NoMoveVert", "БезДвиженияВертикально"},
                {"NoMoveHoriz", "БезДвиженияГоризонтально"},
                {"VSplit", "ВРазделитель"},
                {"HSplit", "ГРазделитель"},
                {"PanEast", "КурсорВ"},
                {"PanWest", "КурсорЗ"},
                {"WaitCursor", "КурсорОжидания"},
                {"PanNorth", "КурсорС"},
                {"PanNE", "КурсорСВ"},
                {"PanNW", "КурсорСЗ"},
                {"PanSouth", "КурсорЮ"},
                {"PanSE", "КурсорЮВ"},
                {"PanSW", "КурсорЮЗ"},
                {"Hand", "Ладонь"},
                {"IBeam", "Луч"},
                {"No", "Нет"},
                {"Cross", "Перекрестие"},
                {"Default", "ПоУмолчанию"},
                {"AppStarting", "ПриСтарте"},
                {"SizeWE", "РазмерЗВ"},
                {"SizeNESW", "РазмерСВЮЗ"},
                {"SizeNWSE", "РазмерСЗЮВ"},
                {"SizeNS", "РазмерСЮ"},
                {"SizeAll", "РазмерЧетырехконечный"},
                {"Help", "Справка"},
                {"Arrow", "Стрелка"},
                {"UpArrow", "СтрелкаВверх"}
            };

        public static ArrayList StrFindBetween(string p1, string p2 = null, string p3 = null, bool p4 = true, bool p5 = true)
        {
            // p1 - исходная строка.
            // p2 - подстрока поиска от которой ведем поиск.
            // p3 - подстрока поиска до которой ведем поиск.
            // p4 - не включать p2 и p3 в результат.
            // p5 - в результат не будут включены участки, содержащие другие найденные участки, удовлетворяющие переданным параметрам.
            // Возвращает массив строк.
            string str1 = p1;
            int Position1;
            ArrayList ArrayList1 = new ArrayList();
            if (p2 != null && p3 == null)
            {
                Position1 = str1.IndexOf(p2);
                while (Position1 >= 0)
                {
                    ArrayList1.Add("" + ((p4) ? str1.Substring(Position1 + p2.Length) : str1.Substring(Position1)));
                    str1 = str1.Substring(Position1 + 1);
                    Position1 = str1.IndexOf(p2);
                }
            }
            else if (p2 == null && p3 != null)
            {
                Position1 = str1.IndexOf(p3) + 1;
                int SumPosition1 = Position1;
                while (Position1 > 0)
                {
                    ArrayList1.Add("" + ((p4) ? str1.Substring(0, SumPosition1 - 1) : str1.Substring(0, SumPosition1 - 1 + p3.Length)));
                    try
                    {
                        Position1 = str1.Substring(SumPosition1 + 1).IndexOf(p3) + 1;
                        SumPosition1 = SumPosition1 + Position1 + 1;
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            else if (p2 != null && p3 != null)
            {
                Position1 = str1.IndexOf(p2);
                while (Position1 >= 0)
                {
                    string Стр2;
                    Стр2 = (p4) ? str1.Substring(Position1 + p2.Length) : str1.Substring(Position1);
                    int Position2 = Стр2.IndexOf(p3) + 1;
                    int SumPosition2 = Position2;
                    while (Position2 > 0)
                    {
                        if (p5)
                        {
                            if (Стр2.Substring(0, SumPosition2 - 1).IndexOf(p3) <= -1)
                            {
                                ArrayList1.Add("" + ((p4) ? Стр2.Substring(0, SumPosition2 - 1) : Стр2.Substring(0, SumPosition2 - 1 + p3.Length)));
                            }
                        }
                        else
                        {
                            ArrayList1.Add("" + ((p4) ? Стр2.Substring(0, SumPosition2 - 1) : Стр2.Substring(0, SumPosition2 - 1 + p3.Length)));
                        }
                        try
                        {
                            Position2 = Стр2.Substring(SumPosition2 + 1).IndexOf(p3) + 1;
                            SumPosition2 = SumPosition2 + Position2 + 1;
                        }
                        catch
                        {
                            break;
                        }
                    }
                    str1 = str1.Substring(Position1 + 1);
                    Position1 = str1.IndexOf(p2);
                }
            }
            return ArrayList1;
        }
	
        public static Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }
	
        public static string ParseBetween(string p1, string p2 = null, string p3 = null)
        {
            // p1 - исходная строка.
            // p2 - подстрока поиска от которой ведем поиск.
            // p3 - подстрока поиска до которой ведем поиск.
            // Возвращает строку, ограниченную подстроками p2 и p3.
            string str1 = p1;
            int Position1;
            if (p2 != null && p3 == null)
            {
                Position1 = str1.IndexOf(p2);
                if (Position1 >= 0)
                {
                    return str1.Substring(Position1 + p2.Length);
                }
            }
            else if (p2 == null && p3 != null)
            {
                Position1 = str1.IndexOf(p3) + 1;
                if (Position1 > 0)
                {
                    return str1.Substring(0, Position1 - 1);
                }
            }
            else if (p2 != null && p3 != null)
            {
                Position1 = str1.IndexOf(p2);
                while (Position1 >= 0)
                {
                    string Стр2;
                    Стр2 = str1.Substring(Position1 + p2.Length);
                    int Position2 = Стр2.IndexOf(p3) + 1;
                    int SumPosition2 = Position2;
                    while (Position2 > 0)
                    {
                        if (Стр2.Substring(0, SumPosition2 - 1).IndexOf(p3) <= -1)
                        {
                            return Стр2.Substring(0, SumPosition2 - 1);
                        }
                        try
                        {
                            Position2 = Стр2.Substring(SumPosition2 + 1).IndexOf(p3) + 1;
                            SumPosition2 = SumPosition2 + Position2 + 1;
                        }
                        catch
                        {
                            break;
                        }
                    }
                    str1 = str1.Substring(Position1 + 1);
                    Position1 = str1.IndexOf(p2);
                }
            }
            return null;
        }

        public static Component HighlightedComponent()
        {
            // Возвращает выделенный в настоящее время компонент.
            ISelectionService iSel = (ISelectionService)DesignerHost.GetService(typeof(ISelectionService));
            if (iSel != null)
            {
                return (Component)iSel.PrimarySelection;
            }
            return null;
        }

        public static Component GetComponentByName(string name)
        {
            // Возвращает компонент найденный по имени.
            ISelectionService iSel = (ISelectionService)DesignerHost.GetService(typeof(ISelectionService));
            if (iSel != null)
            {
                ComponentCollection ctrlsExisting = DesignerHost.Container.Components;
                for (int i = 0; i < ctrlsExisting.Count; i++)
                {
                    if (ctrlsExisting[i].Site.Name == name)
                    {
                        return (Component)ctrlsExisting[i];
                    }
                }
            }
            return null;
        }

        public static string GetNameByComponent(Component comp)
        {
            // Возвращает имя для компонента.
            Component comp1 = comp;

            if (comp.GetType() == typeof(System.Windows.Forms.TabPage) || 
                comp.GetType() == typeof(System.Windows.Forms.ImageList) || 
                comp.GetType() == typeof(System.Windows.Forms.MainMenu))
            {
                comp1 = RevertSimilarObj(comp);
            }
            return comp1.Site.Name;
        }

        public static string GetDefaultValues(dynamic comp, System.Windows.Forms.PropertyGrid propertyGrid)
        {
            // Заполним для компонента начальные свойства. Они нужны будут при создании скрипта.
            if (comp.DefaultValues == null)
            {
                string DefaultValues1 = "";
                object view1 = typeof(System.Windows.Forms.PropertyGrid).GetField("gridView", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(propertyGrid);
                GridItemCollection GridItemCollection1 = (GridItemCollection)view1.GetType().InvokeMember("GetAllGridEntries", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, view1, null);
                if (GridItemCollection1 != null)
                {
                    foreach (GridItem GridItem in GridItemCollection1)
                    {
                        if (GridItem.PropertyDescriptor == null)  // Исключим из обхода категории.
                        {
                            continue;
                        }
                        if (GridItem.Label == "Locked")  // Исключим из обхода ненужные свойства.
                        {
                            continue;
                        }
                        if (GridItem.PropertyDescriptor.Category != GridItem.Label)
                        {
                            string str7 = "";
                            string strTab = "            ";
                            str7 = str7 + ObjectConvertToString(GridItem.Value);
                            if (GridItem.GridItems.Count > 0)
                            {
                                strTab = strTab + "\t\t";
                                str7 = str7 + Environment.NewLine;
                                str7 = str7 + GetGridSubEntries(GridItem.GridItems, "", strTab);
                                DefaultValues1 = DefaultValues1 + "" + GridItem.Label + " == " + str7 + Environment.NewLine;
                                strTab = "\t\t";
                            }
                            else
                            {
                                DefaultValues1 = DefaultValues1 + "" + GridItem.Label + " == " + str7 + Environment.NewLine;
                            }
                        }
                    }
                    return DefaultValues1;
                }
            }
            return null;
        }

        public static string GetGridSubEntries(GridItemCollection gridItems, string str, string strTab)
        {
            foreach (var item in gridItems)
            {
                GridItem _item = (GridItem)item;
                str = str + strTab + _item.Label + " = " + _item.Value + Environment.NewLine;
                if (_item.GridItems.Count > 0)
                {
                    strTab = strTab + "\t\t";
                    str = GetGridSubEntries(_item.GridItems, str, strTab);
                    strTab = "\t\t";
                }
            }
            return str;
        }

        private static void GetState(dynamic obj, ref string state)
        {
            dynamic myobj;
            if (obj.GetType() == typeof(System.Windows.Forms.MainMenu))
            {
                myobj = RevertSimilarObj(obj);
                osfDesigner.MainMenu mainMenu = myobj;
                state = state + obj.Name + " = " + myobj + Environment.NewLine;
            }
            myobj = obj;
            Type type = myobj.GetType();
            PropertyInfo[] PropertyInfo = type.GetProperties();
            for (int i2 = 0; i2 < PropertyInfo.Length; i2++)
            {
                string propName = PropertyInfo[i2].Name;
                if (GetDisplayName(myobj, propName) != "")
                {
                    dynamic propValue = null;
                    try
                    {
                        propValue = type.GetProperty(propName).GetValue(myobj);
                    }
                    catch
                    {
                        propValue = type.BaseType.GetProperty(propName).GetValue(myobj);
                    }
                    if (propValue != null)
                    {
                        if (Convert.ToString(propValue) != "")
                        {
                            if (propName == "Icon")
                            {
                                try
                                {
                                    state = state + propName + " = " + ((dynamic)myobj).Icon.Path + Environment.NewLine;
                                }
                                catch
                                {
                                    state = state + propName + " = " + Convert.ToString(propValue) + Environment.NewLine;
                                }
                            }
                            else if(propName == "DefaultCellStyle" && (
                                obj.GetType() == typeof(osfDesigner.DataGridViewTextBoxColumn) ||
                                obj.GetType() == typeof(osfDesigner.DataGridViewButtonColumn) ||
                                obj.GetType() == typeof(osfDesigner.DataGridViewCheckBoxColumn) ||
                                obj.GetType() == typeof(osfDesigner.DataGridViewComboBoxColumn) ||
                                obj.GetType() == typeof(osfDesigner.DataGridViewImageColumn) ||
                                obj.GetType() == typeof(osfDesigner.DataGridViewLinkColumn)
                                ))
                            {
                            }
                            else
                            {
                                state = state + propName + " = " + Convert.ToString(propValue) + Environment.NewLine;
                            }
                        }
                    }
                }
            }
        }

        private static void GetNodes3(dynamic treeView_treeNode, ref ArrayList ArrayList)
        {
            for (int i = 0; i < treeView_treeNode.Nodes.Count; i++)
            {
                TreeNode TreeNode1 = treeView_treeNode.Nodes[i];
                ArrayList.Add(TreeNode1);

                if (TreeNode1.Nodes.Count > 0)
                {
                    GetNodes3(TreeNode1, ref ArrayList);
                }
            }
        }

        public static void BypassMainMenu3(Menu Menu1, ref ArrayList ArrayList)
        {
            for (int i = 0; i < Menu1.MenuItems.Count; i++)
            {
                Menu CurrentMenuItem1 = (Menu)Menu1.MenuItems[i];
                ArrayList.Add(CurrentMenuItem1);

                if (CurrentMenuItem1.MenuItems.Count > 0)
                {
                    BypassMainMenu3(CurrentMenuItem1, ref ArrayList);
                }
            }
        }

        public static string DesignSurfaceState(bool overwrite)
        {
            // Делает снимок свойств всех компонентов на переданной в параметре поверхности дизайнера
            // и фиксирует его в dictionaryDesignSurfaceState.
            if (!block2)
            {
                string state = "";
                DesignSurfaceExt2 surface = ActiveDesignSurface;
                ComponentCollection ctrlsExisting = surface.ComponentContainer.Components;
                for (int i = 0; i < ctrlsExisting.Count; i++)
                {
                    Component component = (Component)ctrlsExisting[i];
                    GetState(component, ref state);

                    if (component.GetType() == typeof(osfDesigner.TreeView))
                    {
                        ArrayList ArrayList1 = new ArrayList();
                        GetNodes3((osfDesigner.TreeView)component, ref ArrayList1);
                        for (int i1 = 0; i1 < ArrayList1.Count; i1++)
                        {
                            MyTreeNode treeNode = (MyTreeNode)ArrayList1[i1];
                            GetState(treeNode, ref state);
                        }
                    }
                    if (component.GetType() == typeof(osfDesigner.StatusBar))
                    {
                        StatusBar statusBar = (osfDesigner.StatusBar)component;
                        for (int i1 = 0; i1 < statusBar.Panels.Count; i1++)
                        {
                            GetState(statusBar.Panels[i1], ref state);
                        }
                    }
                    if (component.GetType() == typeof(osfDesigner.ListView))
                    {
                        ListView listView = (osfDesigner.ListView)component;
                        for (int i1 = 0; i1 < listView.Columns.Count; i1++)
                        {
                            GetState(listView.Columns[i1], ref state);
                        }
                        for (int i1 = 0; i1 < listView.Items.Count; i1++)
                        {
                            ListViewItem listViewItem = (osfDesigner.ListViewItem)listView.Items[i1];
                            GetState(listViewItem, ref state);
                            if (listViewItem.SubItems.Count > 0)
                            {
                                for (int i2 = 1; i2 < listViewItem.SubItems.Count; i2++)
                                {
                                    ListViewSubItem listViewSubItem = (osfDesigner.ListViewSubItem)listViewItem.SubItems[i2];
                                    GetState(listViewSubItem, ref state);
                                }
                            }
                        }
                    }
                    if (component.GetType() == typeof(System.Windows.Forms.MainMenu))
                    {
                        ArrayList ArrayList1 = new ArrayList();
                        MainMenu mainMenu = RevertSimilarObj((System.Windows.Forms.MainMenu)component);
                        BypassMainMenu3(mainMenu, ref ArrayList1);
                        for (int i1 = 0; i1 < ArrayList1.Count; i1++)
                        {
                            MenuItemEntry menuItemEntry = RevertSimilarObj(ArrayList1[i1]);
                            GetState(menuItemEntry, ref state);
                        }
                    }
                    if (component.GetType() == typeof(osfDesigner.ToolBar))
                    {
                        ToolBar toolBar = (osfDesigner.ToolBar)component;
                        for (int i1 = 0; i1 < toolBar.Buttons.Count; i1++)
                        {
                            System.Windows.Forms.ToolBarButton OriginalObj = toolBar.Buttons[i1];
                            osfDesigner.ToolBarButton SimilarObj = new osfDesigner.ToolBarButton();
                            PassProperties(OriginalObj, SimilarObj); // Передадим свойства.
                            SimilarObj.OriginalObj = OriginalObj;
                            SimilarObj.Parent = OriginalObj.Parent;
                            SimilarObj.Style = (osfDesigner.ToolBarButtonStyle)OriginalObj.Style;

                            state = state + "Enabled = " + Convert.ToString(SimilarObj.Enabled_osf) + Environment.NewLine;
                            state = state + "ImageIndex = " + Convert.ToString(SimilarObj.ImageIndex) + Environment.NewLine;
                            state = state + "Pushed = " + Convert.ToString(SimilarObj.Pushed) + Environment.NewLine;
                            state = state + "Visible = " + Convert.ToString(SimilarObj.Visible_osf) + Environment.NewLine;
                            state = state + "Rectangle = " + Convert.ToString(SimilarObj.Rectangle) + Environment.NewLine;
                            state = state + "Style = " + Convert.ToString(SimilarObj.Style) + Environment.NewLine;
                            state = state + "Text = " + Convert.ToString(SimilarObj.Text) + Environment.NewLine;
                            state = state + "ToolTipText = " + Convert.ToString(SimilarObj.ToolTipText) + Environment.NewLine;
                            state = state + "Name = " + Convert.ToString(SimilarObj.Name) + Environment.NewLine;
                        }
                    }
                    if (component.GetType() == typeof(osfDesigner.DataGrid))
                    {
                        DataGrid dataGrid = (osfDesigner.DataGrid)component;
                        for (int i1 = 0; i1 < dataGrid.TableStyles.Count; i1++)
                        {
                            System.Windows.Forms.DataGridTableStyle dataGridTableStyle = dataGrid.TableStyles[i1];
                            dynamic SimilarObj = RevertSimilarObj(dataGridTableStyle);
                            GetState(SimilarObj, ref state);
                            for (int i2 = 0; i2 < dataGridTableStyle.GridColumnStyles.Count; i2++)
                            {
                                GetState(dataGridTableStyle.GridColumnStyles[i2], ref state);
                            }
                        }
                    }
                    if (component.GetType() == typeof(osfDesigner.DataGridView))
                    {
                        DataGridView dataGridView = (osfDesigner.DataGridView)component;
                        TypeConverter Converter1 = new System.Drawing.FontConverter();

                        for (int i1 = 0; i1 < dataGridView.Columns.Count; i1++)
                        {
                            DataGridViewColumn dataGridViewColumn = dataGridView.Columns[i1];
                            GetState(dataGridViewColumn, ref state);

                            System.Windows.Forms.DataGridViewCellStyle defaultCellStyle = (System.Windows.Forms.DataGridViewCellStyle)dataGridViewColumn.DefaultCellStyle;
                            GetState(defaultCellStyle, ref state);

                            state = state + "DefaultCellStyle_Font = " + MyFontConverter.ConvertToString(Converter1.ConvertToString(defaultCellStyle.Font)) + Environment.NewLine;

                            if (dataGridView.Columns[i1].GetType() == typeof(osfDesigner.DataGridViewComboBoxColumn))
                            {
                                osfDesigner.DataGridViewComboBoxColumn dataGridViewComboBoxColumn = (osfDesigner.DataGridViewComboBoxColumn)dataGridView.Columns[i1];
                                System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection items = dataGridViewComboBoxColumn.Items;
                                for (int i2 = 0; i2 < items.Count; i2++)
                                {
                                    osfDesigner.ListItemComboBox listItem = (osfDesigner.ListItemComboBox)items[i2];
                                    state = state + "Value = " + Convert.ToString(listItem.Value.ToString()) + Environment.NewLine;
                                }
                            }
                        }

                        System.Windows.Forms.DataGridViewCellStyle columnHeadersDefaultCellStyle = dataGridView.ColumnHeadersDefaultCellStyle;
                        state = state + "ColumnHeadersDefaultCellStyle_Font = " + MyFontConverter.ConvertToString(Converter1.ConvertToString(columnHeadersDefaultCellStyle.Font)) + Environment.NewLine;

                        System.Windows.Forms.DataGridViewCellStyle rowHeadersDefaultCellStyle = dataGridView.RowHeadersDefaultCellStyle;
                        state = state + "RowHeadersDefaultCellStyle_Font = " + MyFontConverter.ConvertToString(Converter1.ConvertToString(rowHeadersDefaultCellStyle.Font)) + Environment.NewLine;
                    }
                    if (component.GetType() == typeof(osfDesigner.TabControl))
                    {
                        TabControl tabControl = (osfDesigner.TabControl)component;
                        for (int i1 = 0; i1 < tabControl.TabPages.Count; i1++)
                        {
                            osfDesigner.TabPage tabPage = RevertSimilarObj(tabControl.TabPages[i1]);
                            GetState(tabPage, ref state);
                        }
                    }
                    if (component.GetType() == typeof(osfDesigner.ListBox))
                    {
                        ListBox listBox = (osfDesigner.ListBox)component;
                        for (int i1 = 0; i1 < listBox.Items.Count; i1++)
                        {
                            ListItemListBox item = (osfDesigner.ListItemListBox)listBox.Items[i1];
                            state = state + "ValueType = " + Convert.ToString(item.ValueType) + Environment.NewLine;
                            state = state + "Text = " + Convert.ToString(item.Text) + Environment.NewLine;
                            try
                            {
                                state = state + "ValueBool = " + Convert.ToString(item.ValueBool) + Environment.NewLine;
                            }
                            catch { }
                            try
                            {
                                state = state + "ValueDateTime = " + Convert.ToString(item.ValueDateTime) + Environment.NewLine;
                            }
                            catch { }
                            try
                            {
                                state = state + "ValueString = " + Convert.ToString(item.ValueString) + Environment.NewLine;
                            }
                            catch { }
                            try
                            {
                                state = state + "ValueNumber = " + Convert.ToString(item.ValueNumber) + Environment.NewLine;
                            }
                            catch { }
                        }
                    }
                    if (component.GetType() == typeof(System.Windows.Forms.ImageList))
                    {
                        ImageList imageList = RevertSimilarObj((System.Windows.Forms.ImageList)component);
                        GetState(imageList, ref state);
                        MyList myList = imageList.Images;
                        for (int i1 = 0; i1 < myList.Count; i1++)
                        {
                            ImageEntry imageEntry = myList[i1];
                            GetState(imageEntry, ref state);
                        }
                    }
                    if (component.GetType() == typeof(osfDesigner.MonthCalendar))
                    {
                        MonthCalendar monthCalendar = (osfDesigner.MonthCalendar)component;
                        MyBoldedDatesList myBoldedDatesList = monthCalendar.BoldedDates_osf;
                        for (int i1 = 0; i1 < myBoldedDatesList.Count; i1++)
                        {
                            DateEntry dateEntry = myBoldedDatesList[i1];
                            state = state + "dateEntry = " + Convert.ToString(dateEntry) + Environment.NewLine;
                        }
                        MyAnnuallyBoldedDatesList myAnnuallyBoldedDatesList = monthCalendar.AnnuallyBoldedDates_osf;
                        for (int i1 = 0; i1 < myAnnuallyBoldedDatesList.Count; i1++)
                        {
                            DateEntry dateEntry = myAnnuallyBoldedDatesList[i1];
                            state = state + "dateEntry = " + Convert.ToString(dateEntry) + Environment.NewLine;
                        }
                        MyMonthlyBoldedDatesList myMonthlyBoldedDatesList = monthCalendar.MonthlyBoldedDates_osf;
                        for (int i1 = 0; i1 < myMonthlyBoldedDatesList.Count; i1++)
                        {
                            DateEntry dateEntry = myMonthlyBoldedDatesList[i1];
                            state = state + "dateEntry = " + Convert.ToString(dateEntry) + Environment.NewLine;
                        }
                    }
                    if (component.GetType() == typeof(osfDesigner.ComboBox))
                    {
                        ComboBox comboBox = (osfDesigner.ComboBox)component;
                        for (int i1 = 0; i1 < comboBox.Items.Count; i1++)
                        {
                            ListItemComboBox item = (osfDesigner.ListItemComboBox)comboBox.Items[i1];
                            state = state + "ValueType = " + Convert.ToString(item.ValueType) + Environment.NewLine;
                            state = state + "Text = " + Convert.ToString(item.Text) + Environment.NewLine;
                            try
                            {
                                state = state + "ValueBool = " + Convert.ToString(item.ValueBool) + Environment.NewLine;
                            }
                            catch { }
                            try
                            {
                                state = state + "ValueDateTime = " + Convert.ToString(item.ValueDateTime) + Environment.NewLine;
                            }
                            catch { }
                            try
                            {
                                state = state + "ValueString = " + Convert.ToString(item.ValueString) + Environment.NewLine;
                            }
                            catch { }
                            try
                            {
                                state = state + "ValueNumber = " + Convert.ToString(item.ValueNumber) + Environment.NewLine;
                            }
                            catch { }
                        }
                    }

                }
                state = state.Trim();
                if (!dictionaryDesignSurfaceState.ContainsKey(surface))
                {
                    dictionaryDesignSurfaceState.Add(surface, state);
                    dictionaryTabPageChanged[pDesigner.TabControl.SelectedTab] = false;
                }
                else
                {
                    if (overwrite)
                    {
                        dictionaryDesignSurfaceState[surface] = state;
                        dictionaryTabPageChanged[pDesigner.TabControl.SelectedTab] = false;
                    }
                }
                pDesigner.TabControl.Refresh();
                return state;
            }
            return "";
        }

        public static void SetDesignSurfaceState()
        {
            string state = DesignSurfaceState(false);
            if (state != "")
            {
                try
                {
                    if (state != dictionaryDesignSurfaceState[ActiveDesignSurface])
                    {
                        dictionaryTabPageChanged[pDesigner.TabControl.SelectedTab] = true;
                    }
                    else
                    {
                        dictionaryTabPageChanged[pDesigner.TabControl.SelectedTab] = false;
                    }
                }
                catch { }
                pDesigner.TabControl.Refresh();
            }
        }
	
        public static DesignSurfaceExt2 ActiveDesignSurface
        {
            get { return pDesigner.DSME.ActiveDesignSurface; }
        }

        public static IDesignerHost DesignerHost
        {
            get { return ActiveDesignSurface.GetIDesignerHost(); }
        }

        public static Form RootComponent
        {
            get { return (Form)DesignerHost.RootComponent; }
        }

        public static PropertyGridHost PropertyGridHost
        {
            get { return pDesigner.DSME.PropertyGridHost; }
        }
	
        public static System.Windows.Forms.PropertyGrid PropertyGrid
        {
            get { return PropertyGridHost.PropertyGrid; }
        }
    }
}
