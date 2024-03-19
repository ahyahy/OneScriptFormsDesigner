using osfDesigner.Properties;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class SaveScript
    {
        public static string Indent1 = "    ";
        public static System.Windows.Forms.TreeView TreeView1 = OneScriptFormsDesigner.PropertyGridHost.TreeView;
        public static System.Windows.Forms.ToolBarButton ButtonSort1 = OneScriptFormsDesigner.PropertyGridHost.ButtonSort;
        public static Dictionary<string, Component> comps = new Dictionary<string, Component>();
        private static string TheBeginningProcedureStyleScript = @"Процедура ПодготовкаКомпонентов()";
        private static string TheBeginningProcedureStyleApp = @"Процедура ПриСозданииФормы(_Форма) Экспорт";
        private static string TheBeginningProcedure;
        private static string TheEndProcedure = @"КонецПроцедуры";
        private static string Template1;
        private static string TemplateOriginalStyleScript;
        private static string TemplateOriginalStyleApp;
        private static string path = null;

        public static string GetScriptText(string fileName = null)
        {
            // 1. Получить перечень текущих свойств формы и всех компонентов
            // 2. Выгрузить обязательные свойства согласно RequiredValues.
            // 3. Сравнить текущие свойства с DefaultValues и измененные выгрузить.

            Form savedForm = (Form)OneScriptFormsDesigner.DesignerHost.RootComponent;
            string NameObjectOneScriptForms = savedForm.NameObjectOneScriptForms;

            TemplateOriginalStyleScript =
@"Перем " + NameObjectOneScriptForms + @";
// конец Перем

Процедура ПодготовкаКомпонентов()
    ПодключитьВнешнююКомпоненту(" + "\u0022" + Settings.Default["dllPath"] + "\u0022" + @");
    " + NameObjectOneScriptForms + @" = Новый ФормыДляОдноСкрипта();

    // блок КонецКонструкторы

    // блок КонецСвойства
    // маркерКонцаПроцедуры
КонецПроцедуры

ПодготовкаКомпонентов();

// ...

" + NameObjectOneScriptForms + @".ЗапуститьОбработкуСобытий();
";

            TemplateOriginalStyleApp =
@"Перем " + NameObjectOneScriptForms + @";
// конец Перем

Процедура ПриСозданииФормы(_Форма) Экспорт
    ПодключитьВнешнююКомпоненту(" + "\u0022" + Settings.Default["dllPath"] + "\u0022" + @");
    " + NameObjectOneScriptForms + @" = Новый ФормыДляОдноСкрипта();

    // блок КонецКонструкторы

    // блок КонецСвойства
    // маркерКонцаПроцедуры
КонецПроцедуры

// ...
";

            if (fileName != null)
            {
                path = fileName.Substring(0, fileName.LastIndexOf('\\') + 1);
            }

            comps.Clear();

            if (savedForm.ScriptStyle == ScriptStyle.СтильПриложения)
            {
                Template1 = TemplateOriginalStyleApp;
                TheBeginningProcedure = TheBeginningProcedureStyleApp;
            }
            else
            {
                Template1 = TemplateOriginalStyleScript;
                TheBeginningProcedure = TheBeginningProcedureStyleScript;
            }

            IDesignerEventService des = (IDesignerEventService)pDesigner.DSME.GetService(typeof(IDesignerEventService));
            if (des != null)
            {
                string compName = "";
                ComponentCollection ctrlsExisting = des.ActiveDesigner.Container.Components;

                // Проверим наличие подсказок.
                bool toolTipPresent = false;
                foreach (Component comp in ctrlsExisting)
                {
                    if (comp.Site.Name.Contains("Подсказка"))
                    {
                        toolTipPresent = true;
                    }
                }

                string strForm = "";
                // Запишем в скрипт создание компонентов.
                foreach (Component comp in ctrlsExisting)
                {
                    compName = comp.Site.Name;
                    comps.Add(compName, comp); // Установим соответствие между именем компонента и компонентом.
                    string trimName = compName;
                    for (int i = 0; i < 10; i++)
                    {
                        trimName = trimName.Replace(i.ToString(), "");
                    }
                    if (comp.GetType() == typeof(Form))
                    {
                        Form Form1 = (Form)comp;
                        if (Form1.ScriptStyle == ScriptStyle.СтильПриложения)
                        {
                            strForm = "// блок Форма" + Environment.NewLine +
                                    Indent1 + comp.Site.Name + " = " + @"_Форма;" + Environment.NewLine +
                                    Indent1 + comp.Site.Name + ".Отображать = Истина;" + Environment.NewLine +
                                    Indent1 + comp.Site.Name + ".Показать();" + Environment.NewLine +
                                    Indent1 + comp.Site.Name + ".Активизировать();" + Environment.NewLine +
                                    "    // блок конецФорма";
                        }
                        else
                        {
                            strForm = "// блок Форма" + Environment.NewLine +
                                    Indent1 + comp.Site.Name + " = " + NameObjectOneScriptForms + @".Форма();" + Environment.NewLine +
                                    Indent1 + comp.Site.Name + ".Отображать = Истина;" + Environment.NewLine +
                                    Indent1 + comp.Site.Name + ".Показать();" + Environment.NewLine +
                                    Indent1 + comp.Site.Name + ".Активизировать();" + Environment.NewLine +
                                    "    // блок конецФорма";
                        }	
                    }
                    else
                    {
                        strForm = "" + compName + " = " + NameObjectOneScriptForms + @"." + trimName + "();";
                    }

                    if (compName.Contains("ИндикаторВертикальный"))
                    {
                        Template1 = Template1.Replace("// блок КонецКонструкторы", "" + compName + " = " + NameObjectOneScriptForms + @".Индикатор(Истина);" + Environment.NewLine + "    // блок КонецКонструкторы");
                        Template1 = Template1.Replace("// конец Перем", "Перем " + compName + ";" + Environment.NewLine + "// конец Перем");
                    }
                    else if (compName.Contains("ИндикаторГоризонтальный"))
                    {
                        Template1 = Template1.Replace("// блок КонецКонструкторы", "" + compName + " = " + NameObjectOneScriptForms + @".Индикатор(Ложь);" + Environment.NewLine + "    // блок КонецКонструкторы");
                        Template1 = Template1.Replace("// конец Перем", "Перем " + compName + ";" + Environment.NewLine + "// конец Перем");	
                    }
                    else
                    {
                        Template1 = Template1.Replace("// блок КонецКонструкторы", strForm + Environment.NewLine + "    // блок КонецКонструкторы");
                        Template1 = Template1.Replace("// конец Перем", "Перем " + compName + ";" + Environment.NewLine + "// конец Перем");
                    }
                }
                // Запишем свойства компонентов.
                // Последовательность возмем из древовидной структуры TreeView при сортировке "В порядке создания".
                bool stateSort = ButtonSort1.Pushed;
                ButtonSort1.Pushed = false;
                Component comp2 = OneScriptFormsDesigner.HighlightedComponent();
                OneScriptFormsDesigner.PropertyGridHost.ReloadTreeView();
                if (comp2 != null)
                {
                    OneScriptFormsDesigner.PropertyGridHost.ChangeSelectNode(comp2);
                }

                ArrayList objArrayList2 = new ArrayList(); // Содержит имена компонентов в иерархии дерева компонентов.
                GetNodes1(TreeView1, ref objArrayList2);
                for (int i = 0; i < objArrayList2.Count; i++)
                {
                    Component comp = comps[(string)objArrayList2[i]];
                    if (comp.GetType() == typeof(System.Windows.Forms.TabPage))
                    {
                        try
                        {
                            comp = (Component)OneScriptFormsDesigner.RevertSimilarObj(comp);
                        }
                        catch { }
                    }
                    else if (comp.GetType() == typeof(System.Windows.Forms.ImageList))
                    {
                        try
                        {
                            comp = (Component)OneScriptFormsDesigner.RevertSimilarObj(comp);
                        }
                        catch { }
                    }
                    else if (comp.GetType() == typeof(System.Windows.Forms.MainMenu))
                    {
                        try
                        {
                            comp = (Component)OneScriptFormsDesigner.RevertSimilarObj(comp);
                        }
                        catch { }
                    }
                    compName = comp.Site.Name;
                    Template1 = Template1.Replace("// блок КонецСвойства", "// блок " + compName + "." + Environment.NewLine + "    // блок КонецСвойства");

                    // Установим для элемента родителя.
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
                            string strIcon = compName + ".Значок = " + NameObjectOneScriptForms + @".Значок(" + "\u0022" + "AAABAAEAEBAQAAEABAAoAQAAFgAAACgAAAAQAAAAIAAAAAEABAAAAAAAwAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAACAAACAAAAAgIAAgAAAAIAAgACAgAAAgICAAMDAwAAAAP8AAP8AAAD//wD/AAAA/wD/AP//AAD///8AZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmZmYAAP//AAD//wAA//8AAP//AAD//wAA//8AAP//AAD//wAA//8AAP//AAD//wAA//8AAP//AAD//wAA//8AAP//" + "\u0022" + "); // обязательно назначить значок";
                            AddToScript(strIcon);
                        }
                        string strVisible = compName + ".Отображать = Истина;";
                        AddToScript(strVisible);
                    }

                    PropertyInfo[] myPropertyInfo = comp.GetType().GetProperties();
                    for (int i1 = 0; i1 < myPropertyInfo.Length; i1++)
                    {
                        string valueName = OneScriptFormsDesigner.GetDisplayName(comp, myPropertyInfo[i1].Name);
                        if (valueName != "")
                        {
                            PropertyDescriptor pd = TypeDescriptor.GetProperties(comp)[myPropertyInfo[i1].Name];
                            try
                            {
                                string compValue = OneScriptFormsDesigner.ObjectConvertToString(pd.GetValue(comp));
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
                                compValue = compValue.Replace(Environment.NewLine, "\u0022 + " + NameObjectOneScriptForms + @".Окружение().НоваяСтрока + " + "\u0022");
                                if (compValue.Trim() != "")
                                {
                                    AddToScript(nameToolTip + ".УстановитьПодсказку(" + compName + ", \u0022" + compValue + "\u0022);");
                                }
                            }
                        }
                    }
                    if (compName.Contains("ПанельИнструментов") ||
                        compName.Contains("Разделитель") ||
                        compName.Contains("СтрокаСостояния"))
                    {
                        AddToScript(compName + ".НаПереднийПлан();");
                    }
                }
                ButtonSort1.Pushed = stateSort;
                Component comp3 = OneScriptFormsDesigner.HighlightedComponent();
                OneScriptFormsDesigner.PropertyGridHost.ReloadTreeView();
                if (comp3 != null)
                {
                    OneScriptFormsDesigner.PropertyGridHost.ChangeSelectNode(comp3);
                }
            }

            string script = ReSort(Template1);
            string newScript = "";
            string[] result = script.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i1 = 0; i1 < result.Length; i1++)
            {
                string strCurrent = result[i1];

                if (strCurrent.Contains(@"// блок КонецКонструкторы") || 
                    strCurrent.Contains(@"// конец Перем") || 
                    (strCurrent.Contains(@"// блок Форма") && strCurrent.Trim().Replace(@"// блок Форма", "").Length == 0))
                {
                }
                else if (strCurrent.Contains(@"// блок"))
                {
                    newScript = newScript + Environment.NewLine;
                }
                else if (strCurrent.Trim() == "")
                {
                    newScript = newScript + Environment.NewLine;
                }
                else
                {
                    newScript = newScript + strCurrent + Environment.NewLine;
                }
            }

            newScript = newScript.Trim() + Environment.NewLine;
            string newScript2 = OneScriptFormsDesigner.ParseBetween(newScript, TheBeginningProcedure, TheEndProcedure);
            string newScript3 = OneScriptFormsDesigner.DeleteDoubleEmptyLines(newScript2) + Environment.NewLine;
            newScript = newScript.Replace(newScript2, newScript3);

            newScript = newScript.Replace(Environment.NewLine + @"    // маркерКонцаПроцедуры" + Environment.NewLine, "");
            return newScript;
        }

        // Переформируем выгруженный код.
        private static string ReSort(string Template1)
        {
            Form savedForm = (Form)OneScriptFormsDesigner.DesignerHost.RootComponent;
            string NameObjectOneScriptForms = savedForm.NameObjectOneScriptForms;

            string[] stringSeparators = new string[] { Environment.NewLine };
            string str1 = Template1;
            // Для удобочитаемости сделаем сортировку в разделе объявления переменных.
            string strPerem = (string)OneScriptFormsDesigner.StrFindBetween(str1, @"Перем " + NameObjectOneScriptForms + @";", @"// конец Перем")[0];
            string[] peremArray = strPerem.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            SortedList<string, string> slPerem = new SortedList<string, string>();
            for (int i = 0; i < peremArray.Length; i++)
            {
                string strnum = peremArray[i].Replace(";", "");
                string trimName = strnum;
                for (int i1 = 0; i1 < 10; i1++)
                {
                    trimName = trimName.Replace(i1.ToString(), "");
                }
                strnum = strnum.Replace(trimName, "");
                int capacity = 10; // Разрядность для сортировки.
                // Только форма может не иметь в конце имени нумерации, учтем это.
                if (strnum.Length == 0) // Это переименованная форма без цифр на конце.
                {
                    strnum = "0";
                    for (int i1 = 0; i1 < capacity - strnum.Length; i1++)
                    {
                        strnum = "0" + strnum;
                    }
                }
                else
                {
                    for (int i1 = 0; i1 < capacity - strnum.Length; i1++)
                    {
                        strnum = "0" + strnum;
                    }
                }
                slPerem.Add(trimName + strnum, peremArray[i]);
            }
            List<string> PeremList = new List<string>();
            IList<string> listKeyPerem = slPerem.Keys;
            for (int i = 0; i < listKeyPerem.Count; i++)
            {
                PeremList.Add(slPerem[listKeyPerem[i]]);
            }

            string newPerem = "";
            for (int i = 0; i < PeremList.Count; i++)
            {
                if (i == (PeremList.Count - 1))
                {
                    newPerem = newPerem + Environment.NewLine + PeremList[i] + Environment.NewLine;
                }
                else
                {
                    newPerem = newPerem + Environment.NewLine + PeremList[i];
                }
            }
            str1 = str1.Replace(strPerem, newPerem);

            // Подправим код для подсказок.
            string strDuplicate = str1;
            string[] result2 = strDuplicate.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < result2.Length; i++)
            {
                string strCurrent = result2[i];
                if (strCurrent.Contains(".УстановитьПодсказку("))
                {
                    string nameToolTip = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".УстановитьПодсказку(").Trim();
                    str1 = str1.Replace(strCurrent + Environment.NewLine, "");
                    str1 = str1.Replace(@"// блок " + nameToolTip + ".", @"// блок " + nameToolTip + "." + Environment.NewLine + strCurrent);
                }
            }

            // Удалим повторы строк.
            ArrayList ArrayList2 = OneScriptFormsDesigner.StrFindBetween(str1, @"// блок", @"    // блок", false);
            ArrayList repeats = new ArrayList(); // Массив строк без повторов.
            string strBefore;
            string strAfter;
            for (int i = 0; i < ArrayList2.Count; i++)
            {
                strBefore = "";
                strAfter = "";
                repeats.Clear();
                string fragment1 = (string)ArrayList2[i];
                string[] result = fragment1.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                for (int i1 = 0; i1 < result.Length; i1++)
                {
                    string strCurrent = result[i1];
                    if (!strCurrent.Contains(@"// блок"))
                    {
                        strBefore = strBefore + strCurrent + Environment.NewLine;
                        if (!repeats.Contains(strCurrent))
                        {
                            repeats.Add(strCurrent);
                        }
                    }
                }
                for (int i2 = 0; i2 < repeats.Count; i2++)
                {
                    strAfter = strAfter + repeats[i2] + Environment.NewLine;
                }
                if (strBefore != strAfter && strBefore.Length != 0)
                {
                    str1 = str1.Replace(strBefore, strAfter);
                }
            }

            // Зададим порядок свойств во фрагментах.
            ArrayList2 = OneScriptFormsDesigner.StrFindBetween(str1, @"// блок", @"    // блок", false);
            for (int i = 0; i < ArrayList2.Count; i++)
            {
                string fragment1 = (string)ArrayList2[i];
                string namecomp = OneScriptFormsDesigner.ParseBetween(fragment1, @"[<", @"]");

                string fragment2 = "";
                SortedList SortedList1 = new SortedList();
                SortedList1.Capacity = 10000;
                string[] result = fragment1.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                decimal lastNumber = 0.00001M;
                for (int i1 = 0; i1 < result.Length; i1++)
                {
                    string strResult = result[i1];
                    if (i1 == 0)
                    {
                        SortedList1.Add(0.0M, strResult);
                    }
                    else if (strResult.Contains(namecomp + "." + "Родитель ="))
                    {
                        SortedList1.Add(1.0M, strResult);
                    }
                    else if (strResult.Contains(namecomp + "." + "Стыковка ="))
                    {
                        SortedList1.Add(2.0M, strResult);
                    }
                    else if (strResult.Contains(namecomp + ".НаПереднийПлан();"))
                    {
                        SortedList1.Add(3.0M, strResult);
                    }
                    else if (strResult.Contains(namecomp + "." + "АвтоРазмер ="))
                    {
                        SortedList1.Add(4.0M, strResult);
                    }
                    else if (strResult.Contains(namecomp + "." + "Размер ="))
                    {
                        SortedList1.Add(5.0M, strResult);
                    }
                    else if (strResult.Contains(namecomp + "." + "ПорядокОбхода ="))
                    {
                        SortedList1.Add(6.0M, strResult);
                    }
                    else if (strResult.Contains(namecomp + "." + "Флажки ="))
                    {
                        SortedList1.Add(7.0M, strResult);
                    }
                    else if (strResult.Contains(namecomp + "." + "Путь ="))
                    {
                        SortedList1.Add(8.0M, strResult);
                    }
                    else if (strResult.Contains(namecomp + "." + "Элементы.Добавить(" + NameObjectOneScriptForms + @".ЭлементСписка("))
                    {
                        // Элементов может быть больше одного.
                        SortedList1.Add(9.0M + lastNumber, strResult);
                        lastNumber = lastNumber + 0.00001M;
                    }
                    else if (strResult.Contains(namecomp + "." + "ГоризонтальнаяПрокрутка ="))
                    {
                        SortedList1.Add(10.0M, strResult);
                    }
                    else
                    {
                        SortedList1.Add(Convert.ToDecimal(i1 + 500), strResult);
                    }
                }
                ArrayList ArrayList3 = new ArrayList();
                for (int i1 = 0; i1 < SortedList1.Count; i1++)
                {
                    if (SortedList1[SortedList1.GetKey(i1)] != null)
                    {
                        ArrayList3.Add(SortedList1[SortedList1.GetKey(i1)]);
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
                        fragment2 = fragment2 + Environment.NewLine + Indent1 + strArrayList;
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
                if (!comp.DefaultValues.Contains(valueName + " == " + compValue + Environment.NewLine))
                {
                    try
                    {
                        TextToScript(compName, valueName, compValue, comp);
                    }
                    catch
                    {
                        MessageBox.Show("Не обработано: на компоненте = " + compName + " valueName=" + valueName + " compValue=" + compValue);
                    }
                }
            }
        }

        private static void TextToScript(string compName, string valueName, string compValue, dynamic val = null)
        {
            Form savedForm = (Form)OneScriptFormsDesigner.DesignerHost.RootComponent;
            string NameObjectOneScriptForms = savedForm.NameObjectOneScriptForms;

            // Пропустим некоторые свойства.
            if ((val.GetType() == typeof(osfDesigner.DataGridViewCellStyle) || val.GetType() == typeof(osfDesigner.DataGridViewCellStyleHeaders)) && (valueName == "ИмяСтиля"))	
            {
                return;
            }	
            if (val.GetType() == typeof(osfDesigner.StatusBar) && (valueName == "Положение" || valueName == "Размер"))
            {
                return;
            }
            if (val.GetType() == typeof(osfDesigner.Splitter) && (valueName == "Курсор" || valueName == "Положение"))
            {
                return;
            }
            if ((val.GetType() == typeof(osfDesigner.MenuItemEntry) ||
                val.GetType() == typeof(osfDesigner.DataGridTableStyle) ||
                val.GetType() == typeof(osfDesigner.DataGridTextBoxColumn) ||
                val.GetType() == typeof(osfDesigner.DataGridComboBoxColumnStyle) ||
                val.GetType() == typeof(osfDesigner.DataGridBoolColumn)) && (valueName == "Текст"))
            {
                return;
            }
            if (val.GetType() == typeof(osfDesigner.Form) && (valueName == "Путь"))
            {
                return;
            }
            if (val.GetType() == typeof(MyTreeNode) && (valueName == "ПолныйПуть"))
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
            // Закончили пропуск свойств.

            if ((compValue == "Ложь" || compValue == "Истина") && !(
                (valueName == "ИзменяемыйРазмер" && val.GetType() == typeof(osfDesigner.DataGridViewTextBoxColumn)) ||
                (valueName == "ИзменяемыйРазмер" && val.GetType() == typeof(osfDesigner.DataGridViewButtonColumn)) ||
                (valueName == "ИзменяемыйРазмер" && val.GetType() == typeof(osfDesigner.DataGridViewCheckBoxColumn)) ||
                (valueName == "ИзменяемыйРазмер" && val.GetType() == typeof(osfDesigner.DataGridViewComboBoxColumn)) ||
                (valueName == "ИзменяемыйРазмер" && val.GetType() == typeof(osfDesigner.DataGridViewImageColumn)) ||
                (valueName == "ИзменяемыйРазмер" && val.GetType() == typeof(osfDesigner.DataGridViewLinkColumn)) ||
                (valueName == "Перенос" && val.GetType() == typeof(osfDesigner.DataGridViewCellStyle)) ||
                (valueName == "Перенос" && val.GetType() == typeof(osfDesigner.DataGridViewCellStyleHeaders))))
            {
                AddToScript(compName + "." + valueName + " = " + compValue + ";");
                return;
            }
            if (valueName == "ЭлементыМеню")
            {
                if (val != null)
                {
                    Menu.MenuItemCollection MenuItemCollection1 = (Menu.MenuItemCollection)val.MenuItems;
                    if (MenuItemCollection1.Count > 0)
                    {
                        MenuItemEntry MenuItemEntry1;
                        for (int i = 0; i < MenuItemCollection1.Count; i++)
                        {
                            MenuItemEntry1 = OneScriptFormsDesigner.RevertSimilarObj(MenuItemCollection1[i]);
                            string strName = MenuItemEntry1.Name.Contains("Сепаратор") ? "-" : MenuItemEntry1.Text;
                            AddToScript(MenuItemEntry1.Name + " = " + compName + ".ЭлементыМеню.Добавить(" + NameObjectOneScriptForms + @".ЭлементМеню(" + "\u0022" + strName + "\u0022));");

                            string hide = MenuItemEntry1.Hide;
                            MenuItemEntry1.Hide = "Показать";
                            PropComponent(MenuItemEntry1);
                            MenuItemEntry1.Hide = hide;

                            if (MenuItemEntry1.MenuItems.Count > 0)
                            {
                                GetMenuItems((MenuItemEntry)MenuItemEntry1);
                            }
                        }
                    }
                }
                return;
            }
            if (valueName == "Меню")
            {
                if (compValue != null)
                {
                    AddToScript(compName + "." + valueName + " = " + compValue + ";");
                }
                return;
            }
            if (valueName == "ОбластьСсылки")
            {
                if (val != null)
                {
                    AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ОбластьСсылки(" + compValue.Replace(";", ",") + ");");
                }
                return;
            }
            if (valueName == "Подэлементы")
            {
                if (val != null)
                {
                    System.Windows.Forms.ListViewItem.ListViewSubItemCollection ListViewSubItemCollection1 = (System.Windows.Forms.ListViewItem.ListViewSubItemCollection)val.SubItems;
                    if (ListViewSubItemCollection1.Count > 0)
                    {
                        osfDesigner.ListViewSubItem ListViewSubItem1;
                        for (int i = 1; i < ListViewSubItemCollection1.Count; i++) // Первый индекс должен быть 1, а не 0.
                        {
                            ListViewSubItem1 = (osfDesigner.ListViewSubItem)ListViewSubItemCollection1[i];
                            AddToScript(ListViewSubItem1.Name + " = " + NameObjectOneScriptForms + @".ПодэлементСпискаЭлементов();");
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
                                if (ListItemComboBox1.ValueType == DataType.Строка)
                                {
                                    strValue = strValue + Indent1 + compName + ".Элементы.Добавить(" + NameObjectOneScriptForms + @".ЭлементСписка(" + "\u0022" + ListItemComboBox1.Text + "\u0022, \u0022" + ListItemComboBox1.Text + "\u0022));";
                                }
                                else if (ListItemComboBox1.ValueType == DataType.Дата)
                                {
                                    DateTime DateTime1 = ListItemComboBox1.ValueDateTime;
                                    strValue = strValue + Indent1 + compName + ".Элементы.Добавить(" + NameObjectOneScriptForms + @".ЭлементСписка(" + "\u0022" + ListItemComboBox1.Text + "\u0022, " +
                                        "Дата(" +
                                        DateTime1.ToString("yyyy") + ", " +
                                        DateTime1.ToString("MM") + ", " +
                                        DateTime1.ToString("dd") + ", " +
                                        DateTime1.ToString("HH") + ", " +
                                        DateTime1.ToString("mm") + ", " +
                                        DateTime1.ToString("ss") + ")" + "));";
                                }
                                else if (ListItemComboBox1.ValueType == DataType.Булево)
                                {
                                    strValue = strValue + Indent1 + compName + ".Элементы.Добавить(" + NameObjectOneScriptForms + @".ЭлементСписка(" + "\u0022" + ListItemComboBox1.Text + "\u0022, " + ListItemComboBox1.Text + "));";
                                }
                                else if (ListItemComboBox1.ValueType == DataType.Число)
                                {
                                    strValue = strValue + Indent1 + compName + ".Элементы.Добавить(" + NameObjectOneScriptForms + @".ЭлементСписка(" + "\u0022" + ListItemComboBox1.Text.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".") + "\u0022, " + ListItemComboBox1.Text.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".") + "));";
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
                                if (ListItemListBox1.ValueType == DataType.Строка)
                                {
                                    strValue = strValue + Indent1 + compName + ".Элементы.Добавить(" + NameObjectOneScriptForms + @".ЭлементСписка(" + "\u0022" + ListItemListBox1.Text + "\u0022, \u0022" + ListItemListBox1.Text + "\u0022));";
                                }
                                else if (ListItemListBox1.ValueType == DataType.Дата)
                                {
                                    DateTime DateTime1 = ListItemListBox1.ValueDateTime;
                                    strValue = strValue + Indent1 + compName + ".Элементы.Добавить(" + NameObjectOneScriptForms + @".ЭлементСписка(" + "\u0022" + ListItemListBox1.Text + "\u0022, " +
                                        "Дата(" +
                                        DateTime1.ToString("yyyy") + ", " +
                                        DateTime1.ToString("MM") + ", " +
                                        DateTime1.ToString("dd") + ", " +
                                        DateTime1.ToString("HH") + ", " +
                                        DateTime1.ToString("mm") + ", " +
                                        DateTime1.ToString("ss") + ")" + "));";
                                }
                                else if (ListItemListBox1.ValueType == DataType.Булево)
                                {
                                    strValue = strValue + Indent1 + compName + ".Элементы.Добавить(" + NameObjectOneScriptForms + @".ЭлементСписка(" + "\u0022" + ListItemListBox1.Text + "\u0022, " + ListItemListBox1.Text + "));";
                                }
                                else if (ListItemListBox1.ValueType == DataType.Число)
                                {
                                    strValue = strValue + Indent1 + compName + ".Элементы.Добавить(" + NameObjectOneScriptForms + @".ЭлементСписка(" + "\u0022" + ListItemListBox1.Text.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".") + "\u0022, " + ListItemListBox1.Text.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".") + "));";
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
                                AddToScript(ListViewItem1.Name + " = " + NameObjectOneScriptForms + @".ЭлементСпискаЭлементов();");
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
                            AddToScript(StatusBarPanel1.Name + " = " + NameObjectOneScriptForms + @".ПанельСтрокиСостояния();");
                            AddToScript(compName + ".Панели.Добавить(" + StatusBarPanel1.Name + ");");
                            PropComponent(StatusBarPanel1);
                        }
                    }
                }
                return;
            }
            if (valueName == "Картинка")
            {
                DataGridViewImageColumn DataGridViewImageColumn1 = (DataGridViewImageColumn)val;
                if (compValue != "System.Drawing.Bitmap ()")
                {
                    string FileName = OneScriptFormsDesigner.ParseBetween(compValue, "(", ")");
                    string newPath = "";
                    if (path != null)
                    {
                        string newFileName = FileName.Substring(FileName.LastIndexOf('\\') + 1);
                        newPath = path + newFileName;
                        if (!File.Exists(newPath))
                        {
                            File.Copy(FileName, newPath);
                        }
                    }
                    else
                    {
                        newPath = FileName;
                    }
                    AddToScript(DataGridViewImageColumn1.NameColumn + ".Картинка = " + NameObjectOneScriptForms + @".Картинка(" + "\u0022" + newPath + "\u0022);");
                }
                return;
            }	
            if (valueName == "Колонки")
            {
                if (val != null)
                {
                    if (val.Columns.GetType() == typeof(System.Windows.Forms.DataGridViewColumnCollection))
                    {
                        System.Windows.Forms.DataGridViewColumnCollection DataGridViewColumnCollection1 = (System.Windows.Forms.DataGridViewColumnCollection)val.Columns;
                        if (DataGridViewColumnCollection1.Count > 0)
                        {
                            for (int i = 0; i < DataGridViewColumnCollection1.Count; i++)
                            {
                                if (DataGridViewColumnCollection1[i].GetType() == typeof(osfDesigner.DataGridViewLinkColumn))
                                {
                                    osfDesigner.DataGridViewLinkColumn DataGridViewLinkColumn1 = (osfDesigner.DataGridViewLinkColumn)DataGridViewColumnCollection1[i];
                                    AddToScript(DataGridViewLinkColumn1.NameColumn + " = " + NameObjectOneScriptForms + @".КолонкаСсылка();");
                                    PropComponent(DataGridViewLinkColumn1);
                                    AddToScript(compName + ".Колонки.Добавить(" + DataGridViewLinkColumn1.NameColumn + ");");
                                }
                                if (DataGridViewColumnCollection1[i].GetType() == typeof(osfDesigner.DataGridViewImageColumn))
                                {
                                    osfDesigner.DataGridViewImageColumn DataGridViewImageColumn1 = (osfDesigner.DataGridViewImageColumn)DataGridViewColumnCollection1[i];
                                    AddToScript(DataGridViewImageColumn1.NameColumn + " = " + NameObjectOneScriptForms + @".КолонкаКартинка();");
                                    PropComponent(DataGridViewImageColumn1);
                                    AddToScript(compName + ".Колонки.Добавить(" + DataGridViewImageColumn1.NameColumn + ");");
                                }
                                if (DataGridViewColumnCollection1[i].GetType() == typeof(osfDesigner.DataGridViewComboBoxColumn))
                                {
                                    osfDesigner.DataGridViewComboBoxColumn DataGridViewComboBoxColumn1 = (osfDesigner.DataGridViewComboBoxColumn)DataGridViewColumnCollection1[i];
                                    AddToScript(DataGridViewComboBoxColumn1.NameColumn + " = " + NameObjectOneScriptForms + @".КолонкаПолеВыбора();");
                                    PropComponent(DataGridViewComboBoxColumn1);
	
                                    // Учитываем элементы поля выбора
                                    System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection Items = DataGridViewComboBoxColumn1.Items;
                                    for (int i2 = 0; i2 < Items.Count; i2++)
                                    {
                                        osfDesigner.ListItemComboBox ListItemComboBox1 = (osfDesigner.ListItemComboBox)Items[i2];
                                        dynamic Value1 = ListItemComboBox1.Value;
                                        if (Value1.GetType() == typeof(System.String))
                                        {
                                            AddToScript(DataGridViewComboBoxColumn1.NameColumn + ".Элементы.Добавить(\u0022" + Value1 + "\u0022);");
                                        }
                                        else if (Value1.GetType() == typeof(System.Decimal))
                                        {
                                            AddToScript(DataGridViewComboBoxColumn1.NameColumn + ".Элементы.Добавить(" + Convert.ToString(Value1).Replace(",", ".") + ");");
                                        }
                                        else if (Value1.GetType() == typeof(System.Boolean))
                                        {
                                            AddToScript(DataGridViewComboBoxColumn1.NameColumn + ".Элементы.Добавить(" + Convert.ToString(Value1).Replace("False", "Ложь").Replace("True", "Истина") + ");");
                                        }
                                        else if (Value1.GetType() == typeof(System.DateTime))
                                        {
                                            AddToScript(DataGridViewComboBoxColumn1.NameColumn + ".Элементы.Добавить(Дата(" +
                                                Value1.ToString("yyyy") + ", " +
                                                Value1.ToString("MM") + ", " +
                                                Value1.ToString("dd") + ", " +
                                                Value1.ToString("HH") + ", " +
                                                Value1.ToString("mm") + ", " +
                                                Value1.ToString("ss") + "));");
                                        }
                                    }	
                                    AddToScript(compName + ".Колонки.Добавить(" + DataGridViewComboBoxColumn1.NameColumn + ");");
                                }
                                if (DataGridViewColumnCollection1[i].GetType() == typeof(osfDesigner.DataGridViewCheckBoxColumn))
                                {
                                    osfDesigner.DataGridViewCheckBoxColumn DataGridViewCheckBoxColumn1 = (osfDesigner.DataGridViewCheckBoxColumn)DataGridViewColumnCollection1[i];
                                    AddToScript(DataGridViewCheckBoxColumn1.NameColumn + " = " + NameObjectOneScriptForms + @".КолонкаФлажок();");
                                    PropComponent(DataGridViewCheckBoxColumn1);
                                    AddToScript(compName + ".Колонки.Добавить(" + DataGridViewCheckBoxColumn1.NameColumn + ");");
                                }

                                if (DataGridViewColumnCollection1[i].GetType() == typeof(osfDesigner.DataGridViewButtonColumn))
                                {
                                    osfDesigner.DataGridViewButtonColumn DataGridViewButtonColumn1 = (osfDesigner.DataGridViewButtonColumn)DataGridViewColumnCollection1[i];
                                    AddToScript(DataGridViewButtonColumn1.NameColumn + " = " + NameObjectOneScriptForms + @".КолонкаКнопка();");
                                    PropComponent(DataGridViewButtonColumn1);
                                    AddToScript(compName + ".Колонки.Добавить(" + DataGridViewButtonColumn1.NameColumn + ");");
                                }
                                if (DataGridViewColumnCollection1[i].GetType() == typeof(osfDesigner.DataGridViewTextBoxColumn))
                                {
                                    osfDesigner.DataGridViewTextBoxColumn DataGridViewTextBoxColumn1 = (osfDesigner.DataGridViewTextBoxColumn)DataGridViewColumnCollection1[i];
                                    AddToScript(DataGridViewTextBoxColumn1.NameColumn + " = " + NameObjectOneScriptForms + @".КолонкаПолеВвода();");
                                    PropComponent(DataGridViewTextBoxColumn1);
                                    AddToScript(compName + ".Колонки.Добавить(" + DataGridViewTextBoxColumn1.NameColumn + ");");
                                }

                            }
                        }
                    }
                    else if (val.Columns.GetType() == typeof(System.Windows.Forms.ListView.ColumnHeaderCollection))
                    {
                        System.Windows.Forms.ListView.ColumnHeaderCollection ColumnHeaderCollection1 = (System.Windows.Forms.ListView.ColumnHeaderCollection)val.Columns;
                        if (ColumnHeaderCollection1.Count > 0)
                        {
                            osfDesigner.ColumnHeader ColumnHeader1;
                            for (int i = 0; i < ColumnHeaderCollection1.Count; i++)
                            {
                                ColumnHeader1 = (osfDesigner.ColumnHeader)ColumnHeaderCollection1[i];
                                AddToScript(ColumnHeader1.Name + " = " + NameObjectOneScriptForms + @".Колонка();");
                                PropComponent(ColumnHeader1);
                                AddToScript(compName + ".Колонки.Добавить(" + ColumnHeader1.Name + ");");
                            }
                        }
                    }
                }
                return;
            }
            if (valueName == "СтильЗаголовковКолонок")
            {
                if (val.GetType() == typeof(osfDesigner.DataGridView))
                {
                    osfDesigner.DataGridView DataGridView1 = (osfDesigner.DataGridView)val;
                    osfDesigner.DataGridViewCellStyleHeaders ColumnHeadersDefaultCellStyle1 = (osfDesigner.DataGridViewCellStyleHeaders)DataGridView1.columnHeadersDefaultCellStyle;
                    if (!(ColumnHeadersDefaultCellStyle1.NameStyle == "" || ColumnHeadersDefaultCellStyle1.NameStyle == null))
                    {
                        AddToScript(ColumnHeadersDefaultCellStyle1.NameStyle + " = " + NameObjectOneScriptForms + @".СтильЯчейки();");
                        PropComponent(ColumnHeadersDefaultCellStyle1);
                        AddToScript(compName + ".СтильЗаголовковКолонок = " + ColumnHeadersDefaultCellStyle1.NameStyle + ";");
                    }
                }
                return;
            }
            if (valueName == "СтильЗаголовковСтрок")
            {
                if (val.GetType() == typeof(osfDesigner.DataGridView))
                {
                    osfDesigner.DataGridView DataGridView1 = (osfDesigner.DataGridView)val;
                    osfDesigner.DataGridViewCellStyleHeaders RowHeadersDefaultCellStyle1 = (osfDesigner.DataGridViewCellStyleHeaders)DataGridView1.rowHeadersDefaultCellStyle;
                    if (!(RowHeadersDefaultCellStyle1.NameStyle == "" || RowHeadersDefaultCellStyle1.NameStyle == null))
                    {
                        AddToScript(RowHeadersDefaultCellStyle1.NameStyle + " = " + NameObjectOneScriptForms + @".СтильЯчейки();");
                        PropComponent(RowHeadersDefaultCellStyle1);
                        AddToScript(compName + ".СтильЗаголовковСтрок = " + RowHeadersDefaultCellStyle1.NameStyle + ";");
                    }
                }
                return;
            }	
            if (valueName == "СтильЯчейки")
            {
                if (val != null)
                {
                    if (val.GetType() == typeof(osfDesigner.DataGridViewTextBoxColumn))
                    {
                        osfDesigner.DataGridViewTextBoxColumn DataGridViewTextBoxColumn1 = (osfDesigner.DataGridViewTextBoxColumn)val;
                        osfDesigner.DataGridViewCellStyle DataGridViewCellStyle1 = (osfDesigner.DataGridViewCellStyle)DataGridViewTextBoxColumn1.DefaultCellStyle;
                        if (DataGridViewCellStyle1 != null)
                        {
                            AddToScript(DataGridViewCellStyle1.NameStyle + " = " + NameObjectOneScriptForms + @".СтильЯчейки();");
                            PropComponent(DataGridViewCellStyle1);
                            AddToScript(DataGridViewTextBoxColumn1.NameColumn + ".СтильЯчейки = " + DataGridViewCellStyle1.NameStyle + ";");
                        }
                    }
                    else if (val.GetType() == typeof(osfDesigner.DataGridViewButtonColumn))
                    {
                        osfDesigner.DataGridViewButtonColumn DataGridViewButtonColumn1 = (osfDesigner.DataGridViewButtonColumn)val;
                        osfDesigner.DataGridViewCellStyle DataGridViewCellStyle1 = (osfDesigner.DataGridViewCellStyle)DataGridViewButtonColumn1.DefaultCellStyle;
                        if (DataGridViewCellStyle1 != null)
                        {
                            AddToScript(DataGridViewCellStyle1.NameStyle + " = " + NameObjectOneScriptForms + @".СтильЯчейки();");
                            PropComponent(DataGridViewCellStyle1);
                            AddToScript(DataGridViewButtonColumn1.NameColumn + ".СтильЯчейки = " + DataGridViewCellStyle1.NameStyle + ";");
                        }
                    }
                    else if (val.GetType() == typeof(osfDesigner.DataGridViewCheckBoxColumn))
                    {
                        osfDesigner.DataGridViewCheckBoxColumn DataGridViewCheckBoxColumn1 = (osfDesigner.DataGridViewCheckBoxColumn)val;
                        osfDesigner.DataGridViewCellStyle DataGridViewCellStyle1 = (osfDesigner.DataGridViewCellStyle)DataGridViewCheckBoxColumn1.DefaultCellStyle;
                        if (DataGridViewCellStyle1 != null)
                        {
                            AddToScript(DataGridViewCellStyle1.NameStyle + " = " + NameObjectOneScriptForms + @".СтильЯчейки();");
                            PropComponent(DataGridViewCellStyle1);
                            AddToScript(DataGridViewCheckBoxColumn1.NameColumn + ".СтильЯчейки = " + DataGridViewCellStyle1.NameStyle + ";");
                        }
                    }
                    else if (val.GetType() == typeof(osfDesigner.DataGridViewComboBoxColumn))
                    {
                        osfDesigner.DataGridViewComboBoxColumn DataGridViewComboBoxColumn1 = (osfDesigner.DataGridViewComboBoxColumn)val;
                        osfDesigner.DataGridViewCellStyle DataGridViewCellStyle1 = (osfDesigner.DataGridViewCellStyle)DataGridViewComboBoxColumn1.DefaultCellStyle;
                        if (DataGridViewCellStyle1 != null)
                        {
                            AddToScript(DataGridViewCellStyle1.NameStyle + " = " + NameObjectOneScriptForms + @".СтильЯчейки();");
                            PropComponent(DataGridViewCellStyle1);
                            AddToScript(DataGridViewComboBoxColumn1.NameColumn + ".СтильЯчейки = " + DataGridViewCellStyle1.NameStyle + ";");
                        }
                    }
                    else if (val.GetType() == typeof(osfDesigner.DataGridViewImageColumn))
                    {
                        osfDesigner.DataGridViewImageColumn DataGridViewImageColumn1 = (osfDesigner.DataGridViewImageColumn)val;
                        osfDesigner.DataGridViewCellStyle DataGridViewCellStyle1 = (osfDesigner.DataGridViewCellStyle)DataGridViewImageColumn1.DefaultCellStyle;
                        if (DataGridViewCellStyle1 != null)
                        {
                            AddToScript(DataGridViewCellStyle1.NameStyle + " = " + NameObjectOneScriptForms + @".СтильЯчейки();");
                            PropComponent(DataGridViewCellStyle1);
                            AddToScript(DataGridViewImageColumn1.NameColumn + ".СтильЯчейки = " + DataGridViewCellStyle1.NameStyle + ";");
                        }
                    }
                    else if (val.GetType() == typeof(osfDesigner.DataGridViewLinkColumn))
                    {
                        osfDesigner.DataGridViewLinkColumn DataGridViewLinkColumn1 = (osfDesigner.DataGridViewLinkColumn)val;
                        osfDesigner.DataGridViewCellStyle DataGridViewCellStyle1 = (osfDesigner.DataGridViewCellStyle)DataGridViewLinkColumn1.DefaultCellStyle;
                        if (DataGridViewCellStyle1 != null)
                        {
                            AddToScript(DataGridViewCellStyle1.NameStyle + " = " + NameObjectOneScriptForms + @".СтильЯчейки();");
                            PropComponent(DataGridViewCellStyle1);
                            AddToScript(DataGridViewLinkColumn1.NameColumn + ".СтильЯчейки = " + DataGridViewCellStyle1.NameStyle + ";");
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
                                AddToScript(GridColumnStyle1.NameStyle + " = " + NameObjectOneScriptForms + @".СтильКолонкиБулево();" + Environment.NewLine +
                                    Indent1 + GridColumnStyle1.NameStyle + ".ИмяОтображаемого = \u0022" + GridColumnStyle1.MappingName + "\u0022;");
                                PropComponent(GridColumnStyle1);
                                AddToScript(compName + ".СтилиКолонкиСеткиДанных.Добавить(" + GridColumnStyle1.NameStyle + ");");
                            }
                            else if (Style1.GetType() == typeof(osfDesigner.DataGridTextBoxColumn))
                            {
                                osfDesigner.DataGridTextBoxColumn GridColumnStyle1 = (osfDesigner.DataGridTextBoxColumn)Style1;
                                AddToScript(GridColumnStyle1.NameStyle + " = " + NameObjectOneScriptForms + @".СтильКолонкиПолеВвода();" + Environment.NewLine +
                                    Indent1 + GridColumnStyle1.NameStyle + ".ИмяОтображаемого = \u0022" + GridColumnStyle1.MappingName + "\u0022;");
                                PropComponent(GridColumnStyle1);
                                AddToScript(compName + ".СтилиКолонкиСеткиДанных.Добавить(" + GridColumnStyle1.NameStyle + ");");
                            }
                            else if (Style1.GetType() == typeof(osfDesigner.DataGridComboBoxColumnStyle))
                            {
                                osfDesigner.DataGridComboBoxColumnStyle GridColumnStyle1 = (osfDesigner.DataGridComboBoxColumnStyle)Style1;
                                AddToScript(GridColumnStyle1.NameStyle + " = " + NameObjectOneScriptForms + @".СтильКолонкиПолеВыбора();" + Environment.NewLine +
                                    Indent1 + GridColumnStyle1.NameStyle + ".ИмяОтображаемого = \u0022" + GridColumnStyle1.MappingName + "\u0022;");
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
                            AddToScript(DataGridTableStyle1.NameStyle + " = " + NameObjectOneScriptForms + @".СтильТаблицыСеткиДанных();" + Environment.NewLine +
                                Indent1 + DataGridTableStyle1.NameStyle + ".ИмяОтображаемого = \u0022" + DataGridTableStyle1.MappingName + "\u0022;" + Environment.NewLine +
                                Indent1 + compName + ".СтилиТаблицы.Добавить(" + DataGridTableStyle1.NameStyle + ");");
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
                            AddToScript(ToolBarButton1.Name + " = " + compName + ".Кнопки.Добавить(" + NameObjectOneScriptForms + @".КнопкаПанелиИнструментов());");
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
                    TreeNodeCollection TreeNodeCollection1 = (TreeNodeCollection)val.Nodes;
                    if (TreeNodeCollection1.Count > 0)
                    {
                        MyTreeNode MyTreeNode1;
                        for (int i = 0; i < TreeNodeCollection1.Count; i++)
                        {
                            MyTreeNode1 = (MyTreeNode)TreeNodeCollection1[i];
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

                string fontNameAndSize = OneScriptFormsDesigner.ParseBetween(compValue, null, "pt");
                fontNameAndSize = fontNameAndSize.Replace(CultureInfo.CurrentCulture.TextInfo.ListSeparator, "~~~")
                                                 .Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".");
                string fontStyles = "стиль" + OneScriptFormsDesigner.ParseBetween(compValue, "стиль", null);
                fontStyles = fontStyles.Replace(CultureInfo.CurrentCulture.TextInfo.ListSeparator, ",")
                                       .Replace(" ", "");

                string[] separators = new string[] { "~~~" };
                string[] result = fontNameAndSize.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < result.Length; i++)
                {
                    if (i == 0)
                    {
                        FontName = result[0];
                    }
                    if (i == 1)
                    {
                        FontSize = result[1].Replace(" ", "");
                    }
                }
                FontStyle = fontStyles.Replace("стиль=", "" + NameObjectOneScriptForms + @".СтильШрифта.")
                                      .Replace(",", " + " + NameObjectOneScriptForms + @".СтильШрифта.")
                                      .Replace("стиль", "");
                AddToScript(compName + "." + valueName + " = " + NameObjectOneScriptForms + @".Шрифт(" + "\u0022" + FontName + "\u0022, " + FontSize + ", " + FontStyle + ");");
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
                            strDateTimes = strDateTimes + Indent1 + compName + "." + valueName + ".Добавить(Дата(" +
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
                            strDateTimes = strDateTimes + Indent1 + compName + "." + valueName + ".Добавить(Дата(" +
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
                            strDateTimes = strDateTimes + Indent1 + compName + "." + valueName + ".Добавить(Дата(" +
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
                            string newPath = MyList1[i1].Path;
                            if (path != null)
                            {
                                string newFileName = MyList1[i1].Path.Substring(MyList1[i1].Path.LastIndexOf('\\') + 1);
                                newPath = path + newFileName;
                                if (!File.Exists(newPath))
                                {
                                    File.Copy(MyList1[i1].Path, newPath);
                                }
                            }

                            str1 = str1 + Indent1 + compName + ".Изображения.Добавить(" + NameObjectOneScriptForms + @".Картинка(" + "\u0022" + newPath + "\u0022));";
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
                    if (!File.Exists(newPath))
                    {
                        File.Copy(FileName, newPath);
                    }
                    AddToScript(compName + "." + valueName + " = " + NameObjectOneScriptForms + @".Картинка(" + "\u0022" + newPath + "\u0022);");
                }
                return;
            }
            if (valueName == "ВыделенныйДиапазон")
            {
                if (val != null)
                {
                    SelectionRange SelectionRange1 = (SelectionRange)val.SelectionRange;
                    string str1 = compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ВыделенныйДиапазон(Дата(" +
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
            // Если это Дата.
            if (valueName == "Значение" && val.GetType() == typeof(osfDesigner.DateTimePicker))
            {
                compValue = compValue.Replace(" ", ".").Replace(":", ".");
                string[] result = compValue.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                DateTime rez = new DateTime();
                for (int i = 0; i < result.Length; i++)
                {
                    if (i == 0)
                    {
                        rez = rez.AddDays(Int32.Parse(result[0]) - 1);
                    }
                    if (i == 1)
                    {
                        rez = rez.AddMonths(Int32.Parse(result[1]) - 1);
                    }
                    if (i == 2)
                    {
                        rez = rez.AddYears(Int32.Parse(result[2]) - 1);
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
                string strDate = "Дата(" +
                    rez.ToString("yyyy") + ", " +
                    rez.ToString("MM") + ", " +
                    rez.ToString("dd") + ", " +
                    rez.ToString("HH") + ", " +
                    rez.ToString("mm") + ", " +
                    rez.ToString("ss") + ")";
                AddToScript(compName + "." + valueName + " = " + strDate + ";");
                return;
            }
            // Если это цвет.
            if (valueName == "КонечныйЦвет" ||
                valueName == "НачальныйЦвет" ||
                valueName == "ОсновнойЦвет" ||
                valueName == "ОсновнойЦветВыделенного" ||
                valueName == "ОсновнойЦветЗаголовков" ||
                valueName == "ПрозрачныйЦвет" ||
                valueName == "Цвет" ||
                valueName == "ЦветАктивнойСсылки" ||
                valueName == "ЦветКруга" ||
                valueName == "ЦветЛинии" ||
                valueName == "ЦветПосещеннойСсылки" ||
                valueName == "ЦветСетки" ||
                valueName == "ЦветСсылки" ||
                valueName == "ЦветФона" ||
                valueName == "ЦветФонаВыделенного" ||
                valueName == "ЦветФонаЗаголовка" ||
                valueName == "ЦветФонаЗаголовков" ||
                valueName == "ЦветФонаНечетныхСтрок" ||
                valueName == "ЦветФонаСеткиДанных" ||
                valueName == "ЦветФонаТаблицы")
            {
                string str1 = "";
                if (val != null)
                {
                    if (val.ToString() == "Color [Empty]")
                    {
                        str1 = compName + "." + valueName + " = " + NameObjectOneScriptForms + @".Цвет(0, 0, 0);";
                    }
                    else if (compValue.Contains(";") || compValue.Contains(","))
                    {
                        str1 = compName + "." + valueName + " = " + NameObjectOneScriptForms + @".Цвет(" + compValue.Replace(";", ",") + ");";
                    }
                    else
                    {
                        str1 = compName + "." + valueName + " = " + NameObjectOneScriptForms + @".Цвет(" + "\u0022" + compValue + "\u0022);";
                    }
                }
                AddToScript(str1);
                return;
            }
            // Если это контрол.
            if (valueName == "КнопкаОтмена" ||
                valueName == "КнопкаПринять" ||
                (valueName == "Значение" && val.GetType() == typeof(osfDesigner.UserControl)) ||
                valueName == "ВыбранныйОбъект")
            {
                AddToScript(compName + "." + valueName + " = " + compValue + ";");
                return;
            }
            // Если это строка для ФорматированноеПолеВвода (RichTextBox).
            if (valueName == "Текст" && val.GetType() == typeof(osfDesigner.RichTextBox))
            {
                compValue = compValue.Replace("\n", "\u0022 + " + NameObjectOneScriptForms + @".Окружение().НоваяСтрока + " + "\u0022");
                AddToScript(compName + "." + valueName + " = \u0022" + compValue + "\u0022;");
                return;
            }
            // Если это событие.
            if (valueName == "ВводОтклонен" ||
                valueName == "ВыбранныйЭлементСеткиИзменен" ||
                valueName == "ВыделениеИзменено" ||
                valueName == "ДатаВыбрана" ||
                valueName == "ДатаИзменена" ||
                valueName == "ДвойноеНажатие" ||
                valueName == "ДвойноеНажатиеЯчейки" ||
                valueName == "Закрыта" ||
                valueName == "ЗначениеИзменено" ||
                valueName == "ЗначениеСвойстваИзменено" ||
                valueName == "ЗначениеЯчейкиИзменено" ||
                valueName == "ИндексВыбранногоИзменен" ||
                valueName == "КлавишаВверх" ||
                valueName == "КлавишаВниз" ||
                valueName == "КлавишаНажата" ||
                valueName == "КолонкаНажатие" ||
                valueName == "МышьНадЭлементом" ||
                valueName == "МышьНадЯчейкой" ||
                valueName == "МышьПокинулаЭлемент" ||
                valueName == "МышьПокинулаЯчейку" ||
                valueName == "Нажатие" ||
                valueName == "НажатиеСодержимогоЯчейки" ||
                valueName == "НажатиеЯчейки" ||
                valueName == "ПередРазвертыванием" ||
                valueName == "ПередРедактированиемНадписи" ||
                valueName == "ПоложениеИзменено" ||
                valueName == "ПометкаИзменена" ||
                valueName == "ПослеВыбора" ||
                valueName == "ПослеРедактированияНадписи" ||
                valueName == "ПриАктивизации" ||
                valueName == "ПриАктивизацииЭлемента" ||
                valueName == "ПриВходе" ||
                valueName == "ПриВходеВСтроку" ||
                valueName == "ПриВходеВЯчейку" ||
                valueName == "ПриВыпадении" ||
                valueName == "ПриДеактивации" ||
                valueName == "ПриЗагрузке" ||
                valueName == "ПриЗадержкеМыши" ||
                valueName == "ПриЗакрытии" ||
                valueName == "ПриИзменении" ||
                valueName == "ПриНажатииЗаголовкаКолонки" ||
                valueName == "ПриНажатииЗаголовкаСтроки" ||
                valueName == "ПриНажатииКнопки" ||
                valueName == "ПриНажатииКнопкиМыши" ||
                valueName == "ПриНажатииКнопкиМышиВЯчейке" ||
                valueName == "ПриОтпусканииМыши" ||
                valueName == "ПриОтпусканииМышиНадЯчейкой" ||
                valueName == "ПриПереименовании" ||
                valueName == "ПриПеремещении" ||
                valueName == "ПриПеремещенииМыши" ||
                valueName == "ПриПеремещенииМышиНадЯчейкой" ||
                valueName == "ПриПерерисовке" ||
                valueName == "ПриПотереФокуса" ||
                valueName == "ПриПрокручивании" ||
                valueName == "ПриСоздании" ||
                valueName == "ПриСрабатыванииТаймера" ||
                valueName == "ПриУдалении" ||
                valueName == "ПриУходе" ||
                valueName == "ПриУходеИзСтроки" ||
                valueName == "ПриУходеИзЯчейки" ||
                valueName == "РазмерИзменен" ||
                valueName == "РедактированиеЯчейкиЗавершено" ||
                valueName == "РедактированиеЯчейкиНачато" ||
                valueName == "СсылкаНажата" ||
                valueName == "ТекстИзменен" ||
                valueName == "ТекущаяЯчейкаИзменена" ||
                valueName == "ТекущаяЯчейкаИзменена" ||
                valueName == "ЭлементДобавлен" ||
                valueName == "ЭлементПомечен" ||
                valueName == "ЭлементУдален")
            {
                string strNameProc = compValue.Replace("(", "").Replace(")", "").Replace(" ", "").Trim();
                if (strNameProc == "")
                {
                    return;
                }
                string strProc = @"Процедура " + strNameProc + @"() Экспорт
    Сообщить(" + "\u0022" + strNameProc + "()\u0022" + @");
КонецПроцедуры
";
                if (OneScriptFormsDesigner.ParseBetween(Template1, null, strProc) == null)
                {
                    Template1 = Template1.Replace(@"Процедура ПодготовкаКомпонентов(", strProc + Environment.NewLine + @"Процедура ПодготовкаКомпонентов(");
                }
                strNameProc = "" + NameObjectOneScriptForms + @".Действие(ЭтотОбъект, " + "\u0022" + strNameProc + "\u0022);";
                AddToScript(compName + "." + valueName + " = " + strNameProc);
                return;
            }
            // Если это Строка.
            if (valueName == "ВыбранныйПуть" ||
                valueName == "Заголовок" ||
                valueName == "ИмяСвойстваДанных" ||
                valueName == "ИмяСтиля" ||
                valueName == "ИмяФайла" ||
                valueName == "Маска" ||
                valueName == "НачальныйКаталог" ||
                valueName == "Описание" ||
                valueName == "ПолныйПуть" ||
                valueName == "ПользовательскийФормат" ||
                valueName == "Путь" ||
                valueName == "РазделительПути" ||
                valueName == "РасширениеПоУмолчанию" ||
                valueName == "СимволПароля" ||
                valueName == "СимволПриглашения" ||
                valueName == "Текст" ||
                valueName == "ТекстЗаголовка" ||
                valueName == "ТекстПодсказки" ||
                valueName == "Фильтр")
            {
                compValue = compValue.Replace(Environment.NewLine, "\u0022 + " + NameObjectOneScriptForms + @".Окружение().НоваяСтрока + " + "\u0022");
                AddToScript(compName + "." + valueName + " = \u0022" + compValue + "\u0022;");
                return;
            }
            // Если это Число.
            if (valueName == "АвтоЗадержка" ||
                valueName == "АвтоЗадержкаПоказа" ||
                valueName == "БольшоеИзменение" ||
                valueName == "ВесЗаполнения" ||	
                valueName == "ВысотаЗаголовковКолонок" ||
                valueName == "ВысотаЭлемента" ||
                valueName == "ГоризонтальнаяМера" ||
                valueName == "ЗадержкаОчередногоПоказа" ||
                valueName == "ЗадержкаПоявления" ||
                (valueName == "Значение" && val.GetType() == typeof(osfDesigner.CircularProgressBar)) ||
                (valueName == "Значение" && val.GetType() == typeof(osfDesigner.HProgressBar)) ||
                (valueName == "Значение" && val.GetType() == typeof(osfDesigner.VProgressBar)) ||
                (valueName == "Значение" && val.GetType() == typeof(osfDesigner.HScrollBar)) ||
                (valueName == "Значение" && val.GetType() == typeof(osfDesigner.VScrollBar)) ||
                (valueName == "Значение" && val.GetType() == typeof(osfDesigner.NumericUpDown)) ||
                (valueName == "Индекс" && val.GetType() != typeof(MyTreeNode)) ||
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
                (valueName == "МинимальныйРазмер" && val.GetType() == typeof(osfDesigner.Splitter)) ||
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
                valueName == "ШиринаКолонки" ||
                valueName == "ШиринаЛинии" ||
                valueName == "ШиринаПолосы" ||
                valueName == "ШиринаРазделителя")	
            {
                AddToScript(compName + "." + valueName + " = " + compValue.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".") + ";");
                return;
            }
            // Если это Размер.
            if (valueName == "МаксимальныйРазмер" ||
                valueName == "МинимальныйРазмер" ||
                valueName == "Размер" ||
                valueName == "РазмерИзображения" ||
                valueName == "РазмерКнопки" ||
                valueName == "РазмерПоляАвтоПрокрутки" ||
                valueName == "РазмерЭлемента")
            {
                if (compName.Contains("Вкладка") && valueName != "РазмерПоляАвтоПрокрутки")
                {
                    return;
                }
                string str1 = compValue.Replace("{Ширина=", "");
                str1 = str1.Replace("Высота=", "");
                str1 = str1.Replace("}", "");
                string[] separators = new string[] { ", " };
                string[] result = str1.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                str1 = "" + NameObjectOneScriptForms + @".Размер(" + result[0] + ", " + result[1] + ");";
                AddToScript(compName + "." + valueName + " = " + str1);
                return;
            }
            // Если это Точка.
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
                        str1 = "" + NameObjectOneScriptForms + @".Точка(" + result[0] + ", " + result[1] + ");";
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
                    str1 = "" + NameObjectOneScriptForms + @".Точка(" + result[0] + ", " + result[1] + ");";
                    AddToScript(compName + "." + valueName + " = " + str1);
                }
                return;
            }
            // Если это Перечисление.
            if (valueName == "РежимТекста")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".РежимТекста." + compValue + ";");
                return;
            }
            if (valueName == "ФормаИндикатора")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ФормаИндикатора." + compValue + ";");
                return;
            }
            if (valueName == "РазмещениеИзображения")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".РазмещениеИзображенияЯчейки." + compValue + ";");
                return;
            }
            if (valueName == "СтильОтображения")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".СтильПоляВыбораЯчейки." + compValue + ";");
                return;
            }
            if (valueName == "РежимСортировки")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".РежимСортировки." + compValue + ";");
                return;
            }
            if (valueName == "РежимАвтоРазмера")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".РежимАвтоРазмераКолонки." + compValue + ";");
                return;
            }	
            if (valueName == "ИзменяемыйРазмер" && (
                val.GetType() == typeof(osfDesigner.DataGridViewTextBoxColumn) ||
                val.GetType() == typeof(osfDesigner.DataGridViewButtonColumn) ||
                val.GetType() == typeof(osfDesigner.DataGridViewCheckBoxColumn) ||
                val.GetType() == typeof(osfDesigner.DataGridViewComboBoxColumn) ||
                val.GetType() == typeof(osfDesigner.DataGridViewImageColumn) ||
                val.GetType() == typeof(osfDesigner.DataGridViewLinkColumn)))
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ТриСостояния." + compValue + ";");
                return;
            }	
            if (valueName == "Перенос" && (val.GetType() == typeof(osfDesigner.DataGridViewCellStyle) || val.GetType() == typeof(osfDesigner.DataGridViewCellStyleHeaders)))
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ТриСостояния." + compValue + ";");
                return;
            }	
            if (valueName == "РежимАвтоРазмераКолонок")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".РежимАвтоРазмераКолонок." + compValue + ";");
                return;
            }
            if (valueName == "РежимАвтоРазмераСтрок")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".РежимАвтоРазмераСтрок." + compValue + ";");
                return;
            }
            if (valueName == "РежимВыбора" && val.GetType() == typeof(osfDesigner.DataGridView))
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".РежимВыбораТаблицы." + compValue + ";");
                return;
            }
            if (valueName == "РежимВысотыЗаголовковКолонок")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".РежимВысотыЗаголовковКолонок." + compValue + ";");
                return;
            }
            if (valueName == "АвтоШиринаЗаголовковСтрок")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".РежимШириныЗаголовковСтрок." + compValue + ";");
                return;
            }
            if (valueName == "ФорматМаски")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ФорматМаски." + compValue + ";");
                return;
            }
            if (valueName == "РежимВставки")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".РежимВставки." + compValue + ";");
                return;
            }
            if (valueName == "КопироватьМаску")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ФорматМаски." + compValue + ";");
                return;
            }
            if (valueName == "ТипСлияния")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".СлияниеМеню." + compValue + ";");
                return;
            }
            if (valueName == "АвтоРазмер")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".АвтоРазмерПанелиСтрокиСостояния." + compValue + ";");
                return;
            }
            if (valueName == "Якорь")
            {
                string str1 = "";
                string[] separators = new string[] { ", " };
                string[] result = compValue.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < result.Length; i++)
                {
                    str1 = str1 + "" + NameObjectOneScriptForms + @".СтилиПривязки." + result[i] + " + ";
                }
                str1 = str1 + ";";
                str1 = str1.Replace(" + ;", ";");
                AddToScript(compName + "." + valueName + " = " + str1);
                return;
            }
            if (valueName == "НачальноеПоложение")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".НачальноеПоложениеФормы." + compValue + ";");
                return;
            }
            if (valueName == "Формат")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ФорматПоляКалендаря." + compValue + ";");
                return;
            }
            if (valueName == "Курсор")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".Курсоры()." + compValue + ";");
                return;
            }
            if (valueName == "ГлубинаЦвета" || valueName == "СочетаниеКлавиш")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @"." + valueName + "." + compValue + ";");
                return;
            }
            if (valueName == "Стиль")
            {
                AddToScript(compName + ".Стиль = " + NameObjectOneScriptForms + @".СтильКнопокПанелиИнструментов." + compValue + ";");
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
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @"." + valueName + "." + compValue + ";");
                return;
            }
            if (valueName == "СтильГраницы")
            {
                if (val.GetType() == typeof(osfDesigner.StatusBarPanel))
                {
                    AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".СтильГраницыПанелиСтрокиСостояния." + compValue + ";");
                }
                else
                {
                    AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @"." + valueName + "." + compValue + ";");
                }
                return;
            }
            if (valueName == "Сортировка")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ПорядокСортировки." + compValue + ";");
                return;
            }
            if (valueName == "СтильЗаголовка")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".СтильЗаголовкаКолонки." + compValue + ";");
                return;
            }
            if (valueName == "Активация")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".АктивацияЭлемента." + compValue + ";");
                return;
            }
            if (valueName == "РазмещениеФоновогоИзображения")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".РазмещениеИзображения." + compValue + ";");
                return;
            }
            if (valueName == "ВыравниваниеПриРаскрытии")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ЛевоеПравоеВыравнивание." + compValue + ";");
                return;
            }
            if (valueName == "СтильВыпадающегоСписка")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".СтильПоляВыбора." + compValue + ";");
                return;
            }
            if (valueName == "ВыравниваниеПометки")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ВыравниваниеСодержимого." + compValue + ";");
                return;
            }
            if (valueName == "РежимМасштабирования" && val.GetType() == typeof(osfDesigner.PictureBox))
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".РежимРазмераПоляКартинки." + compValue + ";");
                return;
            }
            if (valueName == "РежимМасштабирования" && val.GetType() == typeof(osfDesigner.TabControl))
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".РежимРазмераВкладок." + compValue + ";");
                return;
            }
            if (valueName == "Оформление" && val.GetType() == typeof(osfDesigner.ToolBar))
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ОформлениеПанелиИнструментов." + compValue + ";");
                return;
            }
            if (valueName == "Выравнивание" && (val.GetType() == typeof(osfDesigner.DataGridViewCellStyle) || val.GetType() == typeof(osfDesigner.DataGridViewCellStyleHeaders)))	
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ВыравниваниеСодержимогоЯчейки." + compValue + ";");
                return;
            }	
            if (valueName == "Выравнивание" && val.GetType() == typeof(osfDesigner.TabControl))
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ВыравниваниеВкладок." + compValue + ";");
                return;
            }
            if (valueName == "Выравнивание" && val.GetType() == typeof(osfDesigner.ListView))
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ВыравниваниеВСпискеЭлементов." + compValue + ";");
                return;
            }
            if (valueName == "Выравнивание" && (val.GetType() == typeof(osfDesigner.DataGridBoolColumn) ||
                val.GetType() == typeof(osfDesigner.DataGridTextBoxColumn) ||
                val.GetType() == typeof(osfDesigner.DataGridComboBoxColumnStyle)))
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ГоризонтальноеВыравнивание." + compValue + ";");
                return;
            }
            if (valueName == "ФильтрУведомлений")
            {
                string str1 = "";
                string[] separators = new string[] { ", " };
                string[] result = compValue.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < result.Length; i++)
                {
                    str1 = str1 + "" + NameObjectOneScriptForms + @".ФильтрыУведомления." + result[i] + " + ";
                }
                str1 = str1 + ";";
                str1 = str1.Replace(" + ;", ";");
                AddToScript(compName + "." + valueName + " = " + str1);
                return;
            }
            if (valueName == "ВыравниваниеТекста" && (val.GetType() == typeof(osfDesigner.ColumnHeader) || 
                val.GetType() == typeof(osfDesigner.MaskedTextBox)))
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ГоризонтальноеВыравнивание." + compValue + ";");
                return;
            }
            if (valueName == "ВыравниваниеТекста" && val.GetType() == typeof(osfDesigner.ToolBar))
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ВыравниваниеТекстаВПанелиИнструментов." + compValue + ";");
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
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ВыравниваниеСодержимого." + compValue + ";");
                return;
            }
            if (valueName == "ПервыйДеньНедели")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".День." + compValue + ";");
                return;
            }
            if (valueName == "КорневойКаталог")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".ОсобаяПапка." + compValue + ";");
                return;
            }
            if (valueName == "Стыковка")
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".СтильСтыковки." + compValue + ";");
                AddToScript(compName + ".НаПереднийПлан();");
                return;
            }
            if (valueName == "Значок")
            {
                string FileName = compValue;
                string newFileName = FileName.Substring(FileName.LastIndexOf('\\') + 1);
                string newPath = path + newFileName;
                if (!File.Exists(newPath))
                {
                    File.Copy(FileName, newPath);
                }
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".Значок(" + "\u0022" + newPath + "\u0022);");
                return;
            }
            if (valueName == "МаксимальнаяДата" ||
                valueName == "МинимальнаяДата" ||
                valueName == "ТекущаяДата")
            {
                DateTime DateTime1 = DateTime.Parse(compValue);
                AddToScript(compName + "." + valueName + " = " + "Дата(" +
                    DateTime1.ToString("yyyy") + ", " +
                    DateTime1.ToString("MM") + ", " +
                    DateTime1.ToString("dd") + ", " +
                    DateTime1.ToString("HH") + ", " +
                    DateTime1.ToString("mm") + ", " +
                    DateTime1.ToString("ss") + ");");
                return;
            }
            if (valueName == "Заполнение" && (val.GetType() == typeof(osfDesigner.DataGridViewCellStyle) || (val.GetType() == typeof(osfDesigner.DataGridViewCellStyleHeaders))))	
            {
                AddToScript(compName + "." + valueName + " = " + "" + NameObjectOneScriptForms + @".Заполнение(" + compValue.Replace(";", ",") + ");");
                return;
            }	
        }
        
        private static void AddToScript(string str)
        {
            Template1 = Template1.Replace("// блок КонецСвойства", str + Environment.NewLine + "    // блок КонецСвойства");
        }

        private static void GetNodes(MyTreeNode treeNode)
        {
            MyTreeNode MyTreeNode1;
            for (int i = 0; i < treeNode.Nodes.Count; i++)
            {
                MyTreeNode1 = (MyTreeNode)treeNode.Nodes[i];
                AddToScript(MyTreeNode1.Name + " = " + treeNode.Name + ".Узлы.Добавить(\u0022" + MyTreeNode1.Name + "\u0022);");
                PropComponent(MyTreeNode1);
                if (MyTreeNode1.Nodes.Count > 0)
                {
                    GetNodes(MyTreeNode1);
                }
            }
        }

        private static void GetNodes1(System.Windows.Forms.TreeView TreeView, ref ArrayList objArrayList2)
        {
            for (int i = 0; i < TreeView.Nodes.Count; i++)
            {
                TreeNode TreeNode1 = TreeView.Nodes[i];
                objArrayList2.Add(TreeNode1.Name);
                if (TreeNode1.Nodes.Count > 0)
                {
                    GetNodes2(TreeNode1, ref objArrayList2);
                }
            }
        }

        private static void GetNodes2(TreeNode treeNode, ref ArrayList objArrayList2)
        {
            for (int i = 0; i < treeNode.Nodes.Count; i++)
            {
                TreeNode TreeNode1 = treeNode.Nodes[i];
                objArrayList2.Add(TreeNode1.Name);
                if (TreeNode1.Nodes.Count > 0)
                {
                    GetNodes2(TreeNode1, ref objArrayList2);
                }
            }
        }

        private static void GetMenuItems(MenuItemEntry menuItem)
        {
            Form savedForm = (Form)OneScriptFormsDesigner.DesignerHost.RootComponent;
            string NameObjectOneScriptForms = savedForm.NameObjectOneScriptForms;

            MenuItemEntry MenuItemEntry1;
            for (int i = 0; i < menuItem.MenuItems.Count; i++)
            {
                MenuItemEntry1 = OneScriptFormsDesigner.RevertSimilarObj(menuItem.MenuItems[i]);
                PropertyDescriptor pd = TypeDescriptor.GetProperties(MenuItemEntry1.Parent)["Name"];
                string strParent = (string)pd.GetValue(MenuItemEntry1.Parent);
                string strName = MenuItemEntry1.Name.Contains("Сепаратор") ? "-" : MenuItemEntry1.Text;
                AddToScript(MenuItemEntry1.Name + " = " + strParent + ".ЭлементыМеню.Добавить(" + NameObjectOneScriptForms + @".ЭлементМеню(" + "\u0022" + strName + "\u0022));");

                string hide = MenuItemEntry1.Hide;
                MenuItemEntry1.Hide = "Показать";
                PropComponent(MenuItemEntry1);
                MenuItemEntry1.Hide = hide;

                if (MenuItemEntry1.MenuItems.Count > 0)
                {
                    GetMenuItems(MenuItemEntry1);
                }
            }
        }

        private static void PropComponent(dynamic comp)
        {
            string comp_Name;
            if (comp.GetType() == typeof(osfDesigner.DataGridTableStyle) ||
                comp.GetType() == typeof(osfDesigner.DataGridBoolColumn) ||
                comp.GetType() == typeof(osfDesigner.DataGridTextBoxColumn) ||
                comp.GetType() == typeof(osfDesigner.DataGridComboBoxColumnStyle) ||
                comp.GetType() == typeof(osfDesigner.DataGridViewCellStyleHeaders) ||	
                comp.GetType() == typeof(osfDesigner.DataGridViewCellStyle))
            {
                comp_Name = comp.NameStyle;
            }
            else if (comp.GetType() == typeof(osfDesigner.DataGridViewTextBoxColumn) ||
                comp.GetType() == typeof(osfDesigner.DataGridViewButtonColumn) ||
                comp.GetType() == typeof(osfDesigner.DataGridViewCheckBoxColumn) ||
                comp.GetType() == typeof(osfDesigner.DataGridViewComboBoxColumn) ||
                comp.GetType() == typeof(osfDesigner.DataGridViewImageColumn) ||
                comp.GetType() == typeof(osfDesigner.DataGridViewLinkColumn))
            {
                comp_Name = comp.NameColumn;
            }	
            else
            {
                comp_Name = comp.Name;
            }

            PropertyInfo[] myPropertyInfo = comp.GetType().GetProperties();
            for (int i = 0; i < myPropertyInfo.Length; i++)
            {
                string propName = myPropertyInfo[i].Name;
                string valueName = OneScriptFormsDesigner.GetDisplayName(comp, propName);
                if (valueName != "" && !((valueName == "(Name)") || (valueName == "Прямоугольник")))
                {
                    PropertyDescriptor pd = TypeDescriptor.GetProperties(comp)[propName];
                    try
                    {
                        string compValue = OneScriptFormsDesigner.ObjectConvertToString(pd.GetValue(comp));
                        RequiredDefaultValuesValues(comp, comp_Name, valueName, compValue);
                    }
                    catch { }
                }
            }
        }
    }
}
