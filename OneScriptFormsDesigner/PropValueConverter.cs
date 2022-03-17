using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing.Text;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class PropValueConverter
    {
        public static void SetPropValue(
            object component,
            string displayName, 
            string valProp, 
            Control parent = null)
        {
            dynamic control = null;
            if (component.GetType().ToString() == "osfDesigner.ColorDialog" ||
                component.GetType().ToString() == "osfDesigner.ColumnHeader" ||
                component.GetType().ToString() == "osfDesigner.DataGridBoolColumn" ||
                component.GetType().ToString() == "osfDesigner.DataGridComboBoxColumnStyle" ||
                component.GetType().ToString() == "osfDesigner.DataGridTableStyle" ||
                component.GetType().ToString() == "osfDesigner.DataGridTextBoxColumn" ||
                component.GetType().ToString() == "osfDesigner.FileSystemWatcher" ||
                component.GetType().ToString() == "osfDesigner.FolderBrowserDialog" ||
                component.GetType().ToString() == "osfDesigner.FontDialog" ||
                component.GetType().ToString() == "osfDesigner.ImageList" ||
                component.GetType().ToString() == "osfDesigner.ListViewItem" ||
                component.GetType().ToString() == "osfDesigner.ListViewSubItem" ||
                component.GetType().ToString() == "osfDesigner.MainMenu" ||
                component.GetType().ToString() == "osfDesigner.MenuItemEntry" ||
                component.GetType().ToString() == "osfDesigner.NotifyIcon" ||
                component.GetType().ToString() == "osfDesigner.OpenFileDialog" ||
                component.GetType().ToString() == "osfDesigner.SaveFileDialog" ||
                component.GetType().ToString() == "osfDesigner.StatusBarPanel" ||
                component.GetType().ToString() == "osfDesigner.Timer" ||
                component.GetType().ToString() == "osfDesigner.ToolBarButton" ||
                component.GetType().ToString() == "osfDesigner.ToolTip")
            {
                control = component;
            }
            else
            {
                control = (Control)component;
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
                return;
            }
            if (valProp == "Ложь")
            {
                bool rez = false;

                string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                PropertyInfo pi = control.GetType().GetProperty(propertyName);
                pi.SetValue(control, rez);
                return;
            }
            if (displayName.Contains("ToolTip на"))
            {
                string toolTipName = OneScriptFormsDesigner.ParseBetween(valProp, null, ".УстановитьПодсказку");
                System.Windows.Forms.ToolTip ToolTip1 = (System.Windows.Forms.ToolTip)OneScriptFormsDesigner.GetComponentByName(toolTipName);
                string caption = OneScriptFormsDesigner.ParseBetween(valProp, "\u0022", "\u0022);");
                ToolTip1.SetToolTip(control, caption);
                control.ToolTip[toolTipName] = caption;
            }
            if (displayName == "ОбластьСсылки")
            {
                if (valProp != null)
                {
                    int start = Int32.Parse(OneScriptFormsDesigner.ParseBetween(valProp, "Ф.ОбластьСсылки(", ","));
                    int length = Int32.Parse(OneScriptFormsDesigner.ParseBetween(valProp, ",", ")"));
                    System.Windows.Forms.LinkArea LinkArea1 = new LinkArea(start, length);

                    string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                    PropertyInfo pi = control.GetType().GetProperty(propertyName);
                    pi.SetValue(control, LinkArea1);
                }
                return;
            }
            if (displayName == "Узлы")
            {
                string nameNode = OneScriptFormsDesigner.ParseBetween(valProp, "Добавить(\u0022", "\u0022)");
                if (nameNode != null) // Нужно добавить узел.
                {
                    string nameNodeParent = OneScriptFormsDesigner.ParseBetween(valProp, "=", ".");
                    if (nameNodeParent == control.Name)
                    {
                        MyTreeNode MyTreeNode1 = new MyTreeNode();
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
                        MyTreeNode MyTreeNode2 = null;
                        NodeSearch(((osfDesigner.TreeView)control), nameNodeParent, ref MyTreeNode2, null);
                        MyTreeNode MyTreeNode1 = new MyTreeNode();
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
                else // Нужно обработать свойство узла.
                {
                    // Найдем узел и установим для него свойство.
                    string nameNode2 = OneScriptFormsDesigner.ParseBetween(valProp, null, ".");
                    MyTreeNode MyTreeNode3 = null;
                    NodeSearch(((osfDesigner.TreeView)control), nameNode2, ref MyTreeNode3, null);

                    string nodeDisplayName = OneScriptFormsDesigner.ParseBetween(valProp, ".", "=");
                    string strNodePropertyValue = OneScriptFormsDesigner.ParseBetween(valProp, "=", ";");

                    SetNodePropValue(MyTreeNode3, nodeDisplayName, strNodePropertyValue);
                }
                return;
            }
            if (displayName == "Шрифт" || displayName == "ШрифтЗаголовков")
            {
                Font parentFont;
                try
                {
                    parentFont = (Font)control.Parent.Font;
                }
                catch
                {
                    parentFont = OneScriptFormsDesigner.RootComponent.Font;
                }
                string fontName = parentFont.Name;
                float fontSize = parentFont.Size;
                FontStyle fontStyle = FontStyle.Regular;

                string[] result = valProp.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                if (control.GetType() == typeof(osfDesigner.ListViewItem) || control.GetType() == typeof(osfDesigner.ListViewSubItem))
                {
                    fontName = "";
                    fontSize = float.Parse("10,2");
                }

                for (int i = 0; i < result.Length; i++)
                {
                    if (i == 0)
                    {
                        string myFontName = OneScriptFormsDesigner.ParseBetween(result[0], "\u0022", null).Replace("\u0022", "").Trim();
                        InstalledFontCollection ifc = new InstalledFontCollection();
                        for (int i1 = 0; i1 < ifc.Families.Length; i1++)
                        {
                            string systemFontName = ifc.Families[i1].Name;
                            if (systemFontName.Replace(" ", "") == myFontName)
                            {
                                fontName = systemFontName;
                            }
                        }
                    }
                    if (i == 1)
                    {
                        fontSize = Single.Parse(result[1].Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                    }
                    if (i == 2)
                    {
                        if (result[2].Contains("Жирный"))
                        {
                            fontStyle = fontStyle + (int)FontStyle.Bold;
                        }
                        if (result[2].Contains("Зачеркнутый"))
                        {
                            fontStyle = fontStyle + (int)FontStyle.Strikeout;
                        }
                        if (result[2].Contains("Курсив"))
                        {
                            fontStyle = fontStyle + (int)FontStyle.Italic;
                        }
                        if (result[2].Contains("Подчеркнутый"))
                        {
                            fontStyle = fontStyle + (int)FontStyle.Underline;
                        }
                    }
                }
                if (displayName == "Шрифт")
                {
                    control.Font = new Font(fontName, fontSize, fontStyle);
                }
                else if (displayName == "ШрифтЗаголовков")
                {
                    ((osfDesigner.DataGridTableStyle)control).HeaderFont = new Font(fontName, fontSize, fontStyle);
                }
                return;
            }
            if (displayName == "ВыделенныеДаты")
            {
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

                MyBoldedDatesList MyBoldedDatesList1 = MonthCalendar1.BoldedDates_osf;
                MyBoldedDatesList1.Add(new DateEntry(rez));

                int count1 = MyBoldedDatesList1.Count;
                DateTime[] DateTime1 = new DateTime[count1];
                for (int i = 0; i < MyBoldedDatesList1.Count; i++)
                {
                    DateTime1[i] = MyBoldedDatesList1[i].Value;
                }
                MonthCalendar1.BoldedDates = DateTime1;
                return;
            }
            if (displayName == "ЕжегодныеДаты")
            {
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

                MyAnnuallyBoldedDatesList MyAnnuallyBoldedDatesList1 = MonthCalendar1.AnnuallyBoldedDates_osf;
                MyAnnuallyBoldedDatesList1.Add(new DateEntry(rez));

                int count1 = MyAnnuallyBoldedDatesList1.Count;
                DateTime[] DateTime1 = new DateTime[count1];
                for (int i = 0; i < MyAnnuallyBoldedDatesList1.Count; i++)
                {
                    DateTime1[i] = MyAnnuallyBoldedDatesList1[i].Value;
                }
                MonthCalendar1.AnnuallyBoldedDates = DateTime1;
                return;
            }
            if (displayName == "ЕжемесячныеДаты")
            {
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

                MyMonthlyBoldedDatesList MyMonthlyBoldedDatesList1 = MonthCalendar1.MonthlyBoldedDates_osf;
                MyMonthlyBoldedDatesList1.Add(new DateEntry(rez));

                int count1 = MyMonthlyBoldedDatesList1.Count;
                DateTime[] DateTime1 = new DateTime[count1];
                for (int i = 0; i < MyMonthlyBoldedDatesList1.Count; i++)
                {
                    DateTime1[i] = MyMonthlyBoldedDatesList1[i].Value;
                }
                MonthCalendar1.MonthlyBoldedDates = DateTime1;
                return;
            }
            if (displayName == "Изображения")
            {
                if (File.Exists(valProp))
                {
                    ImageEntry ImageEntry1 = new ImageEntry();
                    Bitmap Bitmap1 = new Bitmap(valProp);
                    Bitmap1.Tag = valProp;
                    ImageEntry1.Image = Bitmap1;
                    ImageEntry1.Path = valProp;
                    ImageEntry1.FileName = valProp;
                    ((dynamic)component).Images.Add(ImageEntry1);
                    ((dynamic)component).OriginalObj.Images.Add(ImageEntry1.Image);
                }
                else
                {
                    MessageBox.Show("Не найден файл " + valProp);
                }
                return;
            }
            if (displayName == "СписокИзображений" || displayName == "СписокБольшихИзображений" || displayName == "СписокМаленькихИзображений")
            {
                IDesignerHost host = OneScriptFormsDesigner.DesignerHost;
                ComponentCollection ctrlsExisting = host.Container.Components;

                System.Windows.Forms.ImageList ImageList1 = null;
                foreach (Component comp in ctrlsExisting)
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
                return;
            }
            if (displayName == "Изображение" || displayName == "ФоновоеИзображение")
            {
                Bitmap Bitmap = null;
                string rez = valProp.Replace("\u0022", "");
                rez = OneScriptFormsDesigner.ParseBetween(rez, "(", ")");
                try
                {
                    Bitmap = new Bitmap(rez);
                }
                catch { }
                if (Bitmap != null)
                {
                    ImageEntry ImageEntry1 = new ImageEntry();
                    ImageEntry1.Image = Bitmap;
                    ImageEntry1.Path = rez;
                    Bitmap.Tag = rez;

                    if (control.GetType() == typeof(osfDesigner.TabPage))
                    {
                        string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                        control = OneScriptFormsDesigner.RevertOriginalObj(control);
                        PropertyInfo pi = control.GetType().BaseType.GetProperty(propertyName);
                        pi.SetValue(control, Bitmap);
                    }
                    else
                    {
                        string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                        PropertyInfo pi = control.GetType().BaseType.GetProperty(propertyName);
                        pi.SetValue(control, Bitmap);
                    }
                }
                else
                {
                    MessageBox.Show("Не найден файл " + rez);
                }
                return;
            }
            if (displayName == "ВыделенныйДиапазон")
            {
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
                return;
            }
            // Если это цвет.
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
                Color Color1 = Color.Empty;
                string strColor = valProp.Replace("\u0022", "");
                strColor = OneScriptFormsDesigner.ParseBetween(strColor, "(", ")");
                if (strColor.Contains(","))
                {
                    string[] result = strColor.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (result.Length == 3)
                    {
                        Color1 = Color.FromArgb(Int32.Parse(result[0]), Int32.Parse(result[1]), Int32.Parse(result[2]));
                    }
                }
                else
                {
                    Color1 = Color.FromName(OneScriptFormsDesigner.colors[strColor]);
                }
                if (Color1 != Color.Empty)
                {
                    string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                    PropertyInfo pi = control.GetType().GetProperty(propertyName);
                    pi.SetValue(control, Color1);
                }
                return;
            }
            // Если это КнопкаОтмена или КнопкаПринять.
            if (displayName == "КнопкаОтмена" ||
                displayName == "КнопкаПринять")
            {
                IDesignerHost host = OneScriptFormsDesigner.DesignerHost;
                ComponentCollection ctrlsExisting = host.Container.Components;

                IButtonControl IButtonControl1 = null;
                foreach (Component comp in ctrlsExisting)
                {
                    if (comp.Site.Name == valProp)
                    {
                        IButtonControl1 = (IButtonControl)comp;
                        break;
                    }
                }

                if (IButtonControl1 != null)
                {
                    string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                    PropertyInfo pi = control.GetType().GetProperty(propertyName);
                    pi.SetValue(control, IButtonControl1);
                }
                return;
            }
            // Если это ВыбранныйОбъект для сетки свойств.
            if (displayName == "ВыбранныйОбъект")
            {
                IDesignerHost host = OneScriptFormsDesigner.DesignerHost;
                ComponentCollection ctrlsExisting = host.Container.Components;

                Control Control1 = null;
                foreach (Component comp in ctrlsExisting)
                {
                    if (comp.Site.Name == valProp)
                    {
                        Control1 = (Control)comp;
                        break;
                    }
                }

                if (Control1 != null)
                {
                    string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                    PropertyInfo pi = control.GetType().GetProperty(propertyName);
                    pi.SetValue(control, Control1);
                }
                return;
            }
            // Если это событие.
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
                string rez = valProp.Replace("\u0022", "").Replace("(", "").Replace(")", "");

                string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                PropertyInfo pi = control.GetType().GetProperty(propertyName);
                pi.SetValue(control, rez);
                return;
            }
            // Если это Строка.
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
                    valProp = valProp.Replace(" + Ф.Окружение().НоваяСтрока + ", Environment.NewLine);
                }
                else
                {
                    valProp = valProp.Replace("\u0022", "");
                }
                string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                control.GetType().GetProperty(propertyName).SetValue(control, valProp);
                return;
            }
            // Если это Число.
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
                try
                {
                    control.GetType().GetProperty(propertyName).SetValue(control, Decimal.Parse(valProp.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)));
                }
                catch
                {
                    control.GetType().GetProperty(propertyName).SetValue(control, Int32.Parse(valProp));
                }
                return;
            }
            // Если это Размер.
            if (displayName == "МаксимальныйРазмер" ||
                displayName == "МинимальныйРазмер" ||
                displayName == "Размер" ||
                displayName == "РазмерИзображения" ||
                displayName == "РазмерКнопки" ||
                displayName == "РазмерПоляАвтоПрокрутки" ||
                displayName == "РазмерЭлемента")
            {
                Size Size1 = new Size();
                Size1.Width = Int32.Parse(OneScriptFormsDesigner.ParseBetween(valProp, "Ф.Размер(", ","));
                Size1.Height = Int32.Parse(OneScriptFormsDesigner.ParseBetween(valProp, ",", ")"));

                string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                control.GetType().GetProperty(propertyName).SetValue(control, Size1);
                return;
            }
            // Если это Точка.
            if (displayName == "Положение")
            {
                Point Point1 = new Point();
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
                return;
            }
            if (displayName == "Курсор")
            {
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
                return;
            }
            // Если это Перечисление.
            if (displayName == "АвтоРазмер" ||
                displayName == "Активация" ||
                displayName == "Выравнивание" ||
                displayName == "ВыравниваниеИзображения" ||
                displayName == "ВыравниваниеПометки" ||
                displayName == "ВыравниваниеПриРаскрытии" ||
                displayName == "ВыравниваниеТекста" ||
                displayName == "ГлубинаЦвета" ||
                displayName == "КорневойКаталог" ||
                displayName == "НачальноеПоложение" ||
                displayName == "Оформление" ||
                displayName == "ПервыйДеньНедели" ||
                displayName == "ПлоскийСтиль" ||
                displayName == "ПоведениеСсылки" ||
                displayName == "ПолосыПрокрутки" ||
                displayName == "РазмещениеФоновогоИзображения" ||
                displayName == "РегистрСимволов" ||
                displayName == "РежимВыбора" ||
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
                string enumName = OneScriptFormsDesigner.ParseBetween(valProp, "Ф.", ".");
                string type_Name = "osfDesigner." + OneScriptFormsDesigner.namesEnum[enumName];
                Type enumType = Type.GetType(type_Name);
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
                if (component.GetType() == typeof(osfDesigner.ToolBarButton))
                {
                    ((osfDesigner.ToolBarButton)component).Style = (osfDesigner.ToolBarButtonStyle)rez;
                }
                if ((displayName == "Стыковка" && component.GetType() != typeof(osfDesigner.ToolBar)) ||
                    (displayName == "Стыковка" && component.GetType() != typeof(osfDesigner.Splitter)) ||
                    (displayName == "Стыковка" && component.GetType() != typeof(osfDesigner.StatusBar)))
                {
                    ((Control)component).BringToFront();
                }
                return;
            }
            if (displayName == "Значок")
            {
                string rez = null;
                MyIcon MyIcon1 = null;
                rez = valProp.Replace("\u0022", "");
                rez = OneScriptFormsDesigner.ParseBetween(rez, "(", ")");
                try
                {
                    MyIcon1 = new MyIcon(rez);
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
                else if (control.GetType() == typeof(osfDesigner.StatusBarPanel))
                {
                    ((osfDesigner.StatusBarPanel)control).Icon = MyIcon1;
                }
                return;
            }
            if (displayName == "МаксимальнаяДата" ||
                displayName == "МинимальнаяДата" ||
                displayName == "ТекущаяДата")
            {
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
                return;
            }
        }

        public static void NodeSearch(osfDesigner.TreeView treeView, string nameNodeParent, ref MyTreeNode node, TreeNodeCollection treeNodes = null)
        {
            TreeNodeCollection _treeNodes;
            if (treeNodes == null)
            {
                _treeNodes = treeView.Nodes;
            }
            else
            {
                _treeNodes = treeNodes;
            }
            MyTreeNode treeNode = null;
            for (int i = 0; i < _treeNodes.Count; i++)
            {
                treeNode = (MyTreeNode)_treeNodes[i];
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

        public static void SetNodePropValue(MyTreeNode control, string displayName, string valProp)
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
            // Если это Число.
            if (displayName == "Индекс" ||
                displayName == "ИндексВыбранногоИзображения" ||
                displayName == "ИндексИзображения")
            {
                string propertyName = OneScriptFormsDesigner.GetPropName(control, displayName);
                control.GetType().GetProperty(propertyName).SetValue(control, Int32.Parse(valProp));
            }
            // Если это Строка.
            if (displayName == "ПолныйПуть" ||
                displayName == "Текст")
            {
                if (valProp.Contains("Ф.Окружение().НоваяСтрока"))
                {
                    valProp = valProp.Replace("\u0022", "");
                    valProp = valProp.Replace("+Ф.Окружение().НоваяСтрока+", Environment.NewLine);
                    valProp = valProp.Replace(" + Ф.Окружение().НоваяСтрока + ", Environment.NewLine);
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
                            Style1 = Style1 + (int)FontStyle.Bold;
                        }
                        if (result[2].Contains("Зачеркнутый"))
                        {
                            Style1 = Style1 + (int)FontStyle.Strikeout;
                        }
                        if (result[2].Contains("Курсив"))
                        {
                            Style1 = Style1 + (int)FontStyle.Italic;
                        }
                        if (result[2].Contains("Подчеркнутый"))
                        {
                            Style1 = Style1 + (int)FontStyle.Underline;
                        }
                    }
                }
                control.NodeFont = new Font(FontName, FontSize, (FontStyle)Style1);
            }
        }
    }
}
