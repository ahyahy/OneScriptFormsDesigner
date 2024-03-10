using osfDesigner.Properties;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class pDesignerMainForm : System.Windows.Forms.Form, IDesignerMainForm
    {
        private string _version = string.Empty;
        public pDesigner pDesignerCore = new pDesigner();
        private IpDesigner IpDesignerCore = null;
        private IContainer components = null;
        private MenuStrip menuStrip1;

        private ToolStripMenuItem _file;
        private ToolStripMenuItem _addForm;
        private ToolStripMenuItem _useSnapLines;
        private ToolStripMenuItem _useGrid;
        private ToolStripMenuItem _useGridWithoutSnapping;
        private ToolStripMenuItem _useNoGuides;
        private ToolStripMenuItem _deleteForm;
        private ToolStripSeparator _stripSeparator1;
        private ToolStripMenuItem _loadScript;
        private ToolStripMenuItem _generateScript;
        private ToolStripMenuItem _generateScriptAs;

        private ToolStripSeparator _stripSeparator2;
        private ToolStripMenuItem _loadForm;
        private ToolStripMenuItem _saveForm;
        private ToolStripMenuItem _saveFormAs;
        private ToolStripSeparator _stripSeparator4;
        private ToolStripMenuItem _exit;

        private ToolStripMenuItem _edit;
        private ToolStripMenuItem _unDo;
        private ToolStripMenuItem _reDo;
        private ToolStripSeparator _stripSeparator3;
        private ToolStripMenuItem _cut;
        private ToolStripMenuItem _copy;
        private ToolStripMenuItem _paste;
        private ToolStripMenuItem _delete;

        private ToolStripMenuItem _view;
        private ToolStripMenuItem _form;
        private ToolStripMenuItem _code;

        private ToolStripMenuItem _tools;
        private ToolStripMenuItem _tabOrder;
        private static ToolStripMenuItem _tabOrder1;

        private ToolStripSeparator _stripSeparator5;
        private ToolStripMenuItem _run;

        private ToolStripSeparator _stripSeparator6;
        private ToolStripMenuItem _settings;

        private ToolStripMenuItem _help;
        private ToolStripMenuItem _about;

        private System.Windows.Forms.Panel pnl4Toolbox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel pnl4pDesigner;
        private System.Windows.Forms.Splitter pnl4splitter;

        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Timer timerLoad;

        private MySettingsForm settingsForm;

        private void timerLoad_Tick(object sender, EventArgs e)
        {
            timerLoad.Stop();
            try
            {
                // Через try, потому что, при загрузке, элемент управления не завершил создание самого себя.
                Application.AddMessageFilter(new PropertyGridMessageFilter(propertyGrid1.GetChildAtPoint(new Point(40, 40)), new MouseEventHandler(propGridView_MouseUp)));
            }
            catch { }
        }

        private void propGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && (
                propertyGrid1.SelectedGridItem.Label == "СписокИзображений" ||
                propertyGrid1.SelectedGridItem.Label == "СписокБольшихИзображений" ||
                propertyGrid1.SelectedGridItem.Label == "СписокМаленькихИзображений"))
            {
                // Пользователь щелкнул левой кнопкой мыши по свойству.
                try
                {
                    propertyGrid1.SelectedGridItem.Expanded = false;
                }
                catch { }
            }
        }

        public delegate void SetProperty(string controlName, string propertyName, IValue propertyValue); // УстановитьСвойство
        public void SetPropertyMethod(string controlName, string propertyName, IValue propertyValue)
        {
            // Найдем и выделим компонент с именем controlName.
            Component Comp1 = (Component)OneScriptFormsDesigner.GetComponentByName(controlName);
            if (Comp1 != null)
            {
                // Позиционируемся на компоненте.
                ISelectionService iSel = (ISelectionService)OneScriptFormsDesigner.DesignerHost.GetService(typeof(ISelectionService));
                if (iSel == null)
                {
                    return;
                }
                iSel.SetSelectedComponents(new IComponent[] { Comp1 });
                OneScriptFormsDesigner.PropertyGridHost.ReloadTreeView();
                OneScriptFormsDesigner.PropertyGridHost.ChangeSelectNode((Component)Comp1);

                Type componentType = Comp1.GetType();
                Type propertyValueType = propertyValue.GetType();
                string propertyNameEn;
                if (componentType == typeof(System.Windows.Forms.ImageList)) // Для подобных объектов (унаследован от System.Windows.Forms.Timer) задаем DefaultValues
                {
                    osfDesigner.ImageList SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(Comp1);
                    SimilarObj.DefaultValues = @"ГлубинаЦвета == Глубина8
Изображения == (Коллекция)
РазмерИзображения == {Ширина=16, Высота=16}
(Name) == " + Comp1.Site.Name + Environment.NewLine;
                    propertyNameEn = OneScriptFormsDesigner.GetPropName(SimilarObj, propertyName);
                }
                else if (componentType == typeof(System.Windows.Forms.TabPage))
                {
                    osfDesigner.TabPage SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(Comp1);
                    propertyNameEn = OneScriptFormsDesigner.GetPropName(SimilarObj, propertyName);
                }
                else
                {
                    propertyNameEn = OneScriptFormsDesigner.GetPropName(Comp1, propertyName);
                }
                // Установим для компонента значение свойства.
                // Если это Число или Перечисление.
                if (propertyValueType == typeof(ScriptEngine.Machine.Values.NumberValue))
                {
                    if (propertyName == "АвтоРазмер" ||
                        propertyName == "АвтоШиринаЗаголовковСтрок" ||
                        propertyName == "Активация" ||
                        propertyName == "Выравнивание" ||
                        propertyName == "ВыравниваниеИзображения" ||
                        propertyName == "ВыравниваниеПометки" ||
                        propertyName == "ВыравниваниеПриРаскрытии" ||
                        propertyName == "ВыравниваниеТекста" ||
                        propertyName == "ГлубинаЦвета" ||
                        propertyName == "ИзменяемыйРазмер" ||
                        propertyName == "РежимАвтоРазмера" ||
                        propertyName == "РежимСортировки" ||
                        propertyName == "КопироватьМаску" ||
                        propertyName == "КорневойКаталог" ||
                        propertyName == "НачальноеПоложение" ||
                        propertyName == "Оформление" ||
                        propertyName == "ПервыйДеньНедели" ||
                        propertyName == "Перенос" ||
                        propertyName == "ПлоскийСтиль" ||
                        propertyName == "ПоведениеСсылки" ||
                        propertyName == "ПолосыПрокрутки" ||
                        propertyName == "РазмещениеФоновогоИзображения" ||
                        propertyName == "РегистрСимволов" ||
                        propertyName == "РежимАвтоРазмераКолонок" ||
                        propertyName == "РежимАвтоРазмераСтрок" ||
                        propertyName == "РежимВставки" ||
                        propertyName == "РежимВыбора" ||
                        propertyName == "РежимВысотыЗаголовковКолонок" ||
                        propertyName == "РежимМасштабирования" ||
                        propertyName == "РежимОтображения" ||
                        propertyName == "РежимРисования" ||
                        propertyName == "РезультатДиалога" ||
                        propertyName == "Сортировка" ||
                        propertyName == "СортировкаСвойств" ||
                        propertyName == "СостояниеФлажка" ||
                        propertyName == "СочетаниеКлавиш" ||
                        propertyName == "Стиль" ||
                        propertyName == "СтильВыпадающегоСписка" ||
                        propertyName == "СтильГраницы" ||
                        propertyName == "СтильГраницыФормы" ||
                        propertyName == "СтильЗаголовка" ||
                        propertyName == "Стыковка" ||
                        propertyName == "ТипСлияния" ||
                        propertyName == "ФильтрУведомлений" ||
                        propertyName == "Формат" ||
                        propertyName == "ФорматМаски" ||
                        propertyName == "Якорь")
                    // Это Перечисление.
                    {
                        TrySetPropertyValue(componentType, propertyNameEn, Comp1, Convert.ToInt32(propertyValue.AsNumber()));

                        if ((propertyName == "Стыковка" && componentType != typeof(osfDesigner.ToolBar)) ||
                            (propertyName == "Стыковка" && componentType != typeof(osfDesigner.Splitter)) ||
                            (propertyName == "Стыковка" && componentType != typeof(osfDesigner.StatusBar)))
                        {
                            ((Control)Comp1).BringToFront();
                        }
                    }
                    else
                    // Это Число.
                    {
                        if (propertyName == "ИндексИзображения" ||
                            propertyName == "ИндексВыбранногоИзображения")
                        {
                            // Сначала добавим компонент СписокИзображений.
                            string NameImageList = AddControlMethod("СписокИзображений");
                            Component Component1 = (Component)OneScriptFormsDesigner.GetComponentByName(NameImageList);
                            if (Component1 != null)
                            {
                                osfDesigner.ImageList SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(Component1);

                                MyList MyList1 = new MyList();
                                osfDesigner.ImageList ImageList1 = SimilarObj;
                                System.Windows.Forms.ImageList.ImageCollection ImageCollection1 = ImageList1.OriginalObj.Images;
                                string Paht1 = "C:\\444\\Pic\\maslenica10.JPG";
                                string Paht2 = "C:\\444\\Pic\\Motif.jpg";
                                string Paht3 = "C:\\444\\Pic\\Games4.JPG";

                                ImageEntry ImageEntry1 = new ImageEntry();
                                ImageEntry1.Image = new Bitmap("" + Paht1);
                                ImageEntry1.Path = Paht1;
                                ImageEntry1.FileName = Paht1;
                                MyList1.Add(ImageEntry1);
                                ImageCollection1.Add(ImageEntry1.Image);

                                ImageEntry ImageEntry2 = new ImageEntry();
                                ImageEntry2.Image = new Bitmap("" + Paht2);
                                ImageEntry2.Path = Paht2;
                                ImageEntry2.FileName = Paht2;
                                MyList1.Add(ImageEntry2);
                                ImageCollection1.Add(ImageEntry2.Image);

                                ImageEntry ImageEntry3 = new ImageEntry();
                                ImageEntry3.Image = new Bitmap("" + Paht3);
                                ImageEntry3.Path = Paht3;
                                ImageEntry3.FileName = Paht3;
                                MyList1.Add(ImageEntry3);
                                ImageCollection1.Add(ImageEntry3.Image);

                                SimilarObj._images = MyList1;
                                if (componentType == typeof(System.Windows.Forms.TabPage))
                                {
                                    TrySetPropertyValue(((Control)Comp1).Parent.GetType(), "ImageList", ((Control)Comp1).Parent, Component1);
                                }
                                else
                                {
                                    TrySetPropertyValue(componentType, "ImageList", Comp1, Component1);
                                }
                            }
                        }
                        else if (propertyName == "Масштаб")
                        {
                            TrySetPropertyValue(componentType, propertyNameEn, Comp1, Convert.ToSingle(propertyValue.AsNumber()));
                            return;
                        }
                        try
                        {
                            TrySetPropertyValue(componentType, propertyNameEn, Comp1, propertyValue.AsNumber());
                        }
                        catch
                        {
                            TrySetPropertyValue(componentType, propertyNameEn, Comp1, Convert.ToInt32(propertyValue.AsNumber()));
                        }
                    }
                }
                // Если это Булево.
                else if (propertyValueType == typeof(ScriptEngine.Machine.Values.BooleanValue))
                {
                    if (propertyName == "ВключаяПодкаталоги")
                    {
                        // Установим значение свойству Путь.
                        SetPropertyMethod(Comp1.Site.Name, "Путь", ValueFactory.Create("C:\\"));
                    }
                    if (propertyName == "ГоризонтальнаяПрокрутка" && componentType == typeof(osfDesigner.ListBox))
                    {
                        //нужно добавить элементы
                        osfDesigner.ListBox ListBox1 = (osfDesigner.ListBox)Comp1;
                        System.Windows.Forms.ListBox.ObjectCollection ListBoxObjectCollection1 = (System.Windows.Forms.ListBox.ObjectCollection)ListBox1.Items;
                        osfDesigner.ListItemListBox ListItemListBox1 = new osfDesigner.ListItemListBox();
                        ListItemListBox1.ValueType = (DataType)0;
                        ListItemListBox1.Value = "Просто строка";
                        ListItemListBox1.Text = ListItemListBox1.Value.ToString();

                        osfDesigner.ListItemListBox ListItemListBox2 = new osfDesigner.ListItemListBox();
                        ListItemListBox2.ValueType = (DataType)2;
                        ListItemListBox2.Value = false;
                        ListItemListBox2.Text = "Ложь";

                        osfDesigner.ListItemListBox ListItemListBox3 = new osfDesigner.ListItemListBox();
                        ListItemListBox3.ValueType = (DataType)1;
                        ListItemListBox3.Value = 18.245M;
                        ListItemListBox3.Text = ListItemListBox3.Value.ToString();

                        osfDesigner.ListItemListBox ListItemListBox4 = new osfDesigner.ListItemListBox();
                        ListItemListBox4.ValueType = (DataType)3;
                        ListItemListBox4.Value = new DateTime(2023, 12, 29);
                        ListItemListBox4.Text = ListItemListBox4.Value.ToString();

                        ListBoxObjectCollection1.Add(ListItemListBox1);
                        ListBoxObjectCollection1.Add(ListItemListBox2);
                        ListBoxObjectCollection1.Add(ListItemListBox3);
                        ListBoxObjectCollection1.Add(ListItemListBox4);
                    }
                    TrySetPropertyValue(componentType, propertyNameEn, Comp1, propertyValue.AsBoolean());
                }
                // Если это Дата.
                else if (propertyValueType == typeof(ScriptEngine.Machine.Values.DateValue))
                {
                    TrySetPropertyValue(componentType, propertyNameEn, Comp1, propertyValue.AsDate());
                }
                // Если это Строка.
                else if (propertyValueType == typeof(ScriptEngine.Machine.Values.StringValue))
                {
                    // Если это СимволПароля? СимволПриглашения.
                    if (propertyValue.AsString() == "System.Char")
                    {
                        TrySetPropertyValue(componentType, propertyNameEn, Comp1, Convert.ToChar('i'));
                    }
                    // Если это Форма - КнопкаОтмена, КнопкаПринять.
                    else if (propertyValue.AsString() == "System.Windows.Forms.IButtonControl")
                    {
                        // Добавим кнопку и зададим её как КнопкаОтмена или КнопкаПринять.
                        string NameButton = AddControlMethod("Кнопка");
                        Component Component1 = (Component)OneScriptFormsDesigner.GetComponentByName(NameButton);
                        if (Component1 != null)
                        {
                            TrySetPropertyValue(componentType, propertyNameEn, Comp1, Component1);
                        }
                    }
                    // Если это Цвет.
                    else if (propertyValue.AsString() == "System.Drawing.Color")
                    {
                        if (propertyName == "ЦветАктивнойСсылки")
                        {
                            TrySetPropertyValue(componentType, propertyNameEn, Comp1, System.Drawing.Color.NavajoWhite);
                        }
                        else
                        {
                            TrySetPropertyValue(componentType, propertyNameEn, Comp1, System.Drawing.Color.Red);
                        }
                    }
                    // Если это Подсказка.
                    else if (propertyValue.AsString() == "System.Collections.Hashtable")
                    {
                        // Ничего не делаем, подсказка уже назначена.
                    }
                    // Если это ПолеСписка - Элементы.
                    else if (propertyValue.AsString() == "System.Windows.Forms.ListBox+ObjectCollection")
                    {
                        osfDesigner.ListBox ListBox1 = (osfDesigner.ListBox)Comp1;
                        System.Windows.Forms.ListBox.ObjectCollection ListBoxObjectCollection1 = (System.Windows.Forms.ListBox.ObjectCollection)ListBox1.Items;
                        osfDesigner.ListItemListBox ListItemListBox1 = new osfDesigner.ListItemListBox();
                        ListItemListBox1.ValueType = (DataType)0;
                        ListItemListBox1.Value = "Просто строка";
                        ListItemListBox1.Text = ListItemListBox1.Value.ToString();

                        osfDesigner.ListItemListBox ListItemListBox2 = new osfDesigner.ListItemListBox();
                        ListItemListBox2.ValueType = (DataType)2;
                        ListItemListBox2.Value = false;
                        ListItemListBox2.Text = "Ложь";

                        osfDesigner.ListItemListBox ListItemListBox3 = new osfDesigner.ListItemListBox();
                        ListItemListBox3.ValueType = (DataType)1;
                        ListItemListBox3.Value = 18.245M;
                        ListItemListBox3.Text = ListItemListBox3.Value.ToString();

                        osfDesigner.ListItemListBox ListItemListBox4 = new osfDesigner.ListItemListBox();
                        ListItemListBox4.ValueType = (DataType)3;
                        ListItemListBox4.Value = new DateTime(2023, 12, 29);
                        ListItemListBox4.Text = ListItemListBox4.Value.ToString();

                        ListBoxObjectCollection1.Add(ListItemListBox1);
                        ListBoxObjectCollection1.Add(ListItemListBox2);
                        ListBoxObjectCollection1.Add(ListItemListBox3);
                        ListBoxObjectCollection1.Add(ListItemListBox4);
                    }
                    // Если это ПолеВыбора - Элементы.
                    else if (propertyValue.AsString() == "System.Windows.Forms.ComboBox+ObjectCollection")
                    {
                        osfDesigner.ComboBox ComboBox1 = (osfDesigner.ComboBox)Comp1;
                        System.Windows.Forms.ComboBox.ObjectCollection ComboBoxObjectCollection1 = (System.Windows.Forms.ComboBox.ObjectCollection)ComboBox1.Items;
                        osfDesigner.ListItemComboBox ListItemComboBox1 = new osfDesigner.ListItemComboBox();
                        ListItemComboBox1.ValueType = (DataType)0;
                        ListItemComboBox1.Value = "Просто строка";
                        ListItemComboBox1.Text = ListItemComboBox1.Value.ToString();

                        osfDesigner.ListItemComboBox ListItemComboBox2 = new osfDesigner.ListItemComboBox();
                        ListItemComboBox2.ValueType = (DataType)2;
                        ListItemComboBox2.Value = false;
                        ListItemComboBox2.Text = "Ложь";

                        osfDesigner.ListItemComboBox ListItemComboBox3 = new osfDesigner.ListItemComboBox();
                        ListItemComboBox3.ValueType = (DataType)1;
                        ListItemComboBox3.Value = 18.245M;
                        ListItemComboBox3.Text = ListItemComboBox3.Value.ToString();

                        osfDesigner.ListItemComboBox ListItemComboBox4 = new osfDesigner.ListItemComboBox();
                        ListItemComboBox4.ValueType = (DataType)3;
                        ListItemComboBox4.Value = new DateTime(2023, 12, 29);
                        ListItemComboBox4.Text = ListItemComboBox4.Value.ToString();

                        ComboBoxObjectCollection1.Add(ListItemComboBox1);
                        ComboBoxObjectCollection1.Add(ListItemComboBox2);
                        ComboBoxObjectCollection1.Add(ListItemComboBox3);
                        ComboBoxObjectCollection1.Add(ListItemComboBox4);
                    }
                    // Если это СтилиТаблицы.
                    else if (propertyValue.AsString() == "System.Windows.Forms.GridTableStylesCollection")
                    {
                        osfDesigner.DataGridTableStyle SimilarObj = new osfDesigner.DataGridTableStyle();
                        System.Windows.Forms.DataGridTableStyle OriginalObj = new System.Windows.Forms.DataGridTableStyle();
                        SimilarObj.OriginalObj = OriginalObj;
                        OneScriptFormsDesigner.AddToDictionary(OriginalObj, SimilarObj);
                        OneScriptFormsDesigner.PassProperties(OriginalObj, SimilarObj); // Передадим свойства.
                        SimilarObj.NameStyle = OneScriptFormsDesigner.RevertDataGridTableStyleName((osfDesigner.DataGrid)Comp1);
                        SimilarObj.Text = "Стиль" + OneScriptFormsDesigner.ParseBetween(SimilarObj.NameStyle, "Стиль", null);
                        ((osfDesigner.DataGrid)Comp1).TableStyles.Add(OriginalObj);

                        SimilarObj.DefaultValues = @"ШрифтЗаголовков == Microsoft Sans Serif; 7,8pt
ПредпочтительнаяВысотаСтрок == 18
ПредпочтительнаяШиринаСтолбцов == 75
ШиринаЗаголовковСтрок == 35
РазрешитьСортировку == Истина
ОтображатьЗаголовкиСтолбцов == Истина
ОтображатьЗаголовкиСтрок == Истина
ИмяОтображаемого == 
СтилиКолонкиСеткиДанных == (Коллекция)
Текст == 
ТолькоЧтение == Ложь
ОсновнойЦвет == ТекстОкна
ОсновнойЦветЗаголовков == ТекстЭлемента
ЦветСетки == ЛицеваяЭлемента
ЦветФона == Окно
ЦветФонаЗаголовков == ЛицеваяЭлемента
ЦветФонаНечетныхСтрок == Окно
";
                    }
                    // Если это Вкладки.
                    else if (propertyValue.AsString() == "System.Windows.Forms.TabControl+TabPageCollection")
                    {
                        // У панели вкладок уже есть автоматически созданные две вкладки. Добавим ещё одну.
                        ToolboxItem toolTabPage1 = new ToolboxItem(typeof(System.Windows.Forms.TabPage));
                        Component compTabPage = (Component)toolTabPage1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                        // Для comp1 уже создан дублер, получим его.
                        osfDesigner.TabPage SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(compTabPage);
                        SimilarObj.OriginalObj = (System.Windows.Forms.TabPage)compTabPage;
                        OneScriptFormsDesigner.PassProperties(compTabPage, SimilarObj); // Передадим свойства.

                        ((System.Windows.Forms.TabControl)Comp1).TabPages.Add((System.Windows.Forms.TabPage)compTabPage);

                        OneScriptFormsDesigner.PropertyGrid.SelectedObject = SimilarObj;
                        SimilarObj.DefaultValues = OneScriptFormsDesigner.GetDefaultValues(SimilarObj, OneScriptFormsDesigner.PropertyGrid);
                    }
                    // Если это Таблица - СтильЗаголовковКолонок, СтильЗаголовковСтрок.
                    else if (propertyValue.AsString() == "System.Windows.Forms.DataGridViewCellStyle")
                    {
                        if (propertyName == "СтильЗаголовковКолонок")
                        {
                            osfDesigner.DataGridView DataGridView1 = (osfDesigner.DataGridView)Comp1;
                            DataGridViewCellStyleHeaders SimilarObj = DataGridView1.columnHeadersDefaultCellStyle;
                            System.Windows.Forms.DataGridViewCellStyle ColumnHeadersDefaultCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle(SimilarObj);
                            ColumnHeadersDefaultCellStyle1.BackColor = Color.Red;
                            DataGridView1.ColumnHeadersDefaultCellStyle = ColumnHeadersDefaultCellStyle1;
                            DataGridView1.columnHeadersDefaultCellStyle.NameStyle = DataGridView1.Name + "СтильЗаголовковКолонок";
                        }
                        else if (propertyName == "СтильЗаголовковСтрок")
                        {
                            osfDesigner.DataGridView DataGridView1 = (osfDesigner.DataGridView)Comp1;
                            DataGridViewCellStyleHeaders SimilarObj = DataGridView1.rowHeadersDefaultCellStyle;
                            System.Windows.Forms.DataGridViewCellStyle RowHeadersDefaultCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle(SimilarObj);
                            RowHeadersDefaultCellStyle1.BackColor = Color.Red;
                            DataGridView1.RowHeadersDefaultCellStyle = RowHeadersDefaultCellStyle1;
                            DataGridView1.rowHeadersDefaultCellStyle.NameStyle = DataGridView1.Name + "СтильЗаголовковСтрок";
                        }
                    }
                    // Если это Таблица - Колонки.
                    else if (propertyValue.AsString() == "System.Windows.Forms.DataGridViewColumnCollection")
                    {
                        osfDesigner.DataGridView DataGridView1 = (osfDesigner.DataGridView)Comp1;
                        // Создадим КолонкаПолеВвода
                        DataGridViewTextBoxColumn DataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
                        DataGridViewTextBoxColumn1.Name = OneScriptFormsDesigner.RevertDataGridViewColumnName(DataGridView1, DataGridViewTextBoxColumn1);
                        DataGridViewTextBoxColumn1.NameColumn = DataGridView1.Name + DataGridViewTextBoxColumn1.Name;
	
                        string TextBoxnameStyle = DataGridView1.Name + DataGridViewTextBoxColumn1.Name + "СтильЯчейки";
                        DataGridViewCellStyle TextBoxSimilarObj = new DataGridViewCellStyle(TextBoxnameStyle, DataGridView1);
                        // Передадим свойства стиля.
                        TextBoxSimilarObj.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());
                        TextBoxSimilarObj.SelectionForeColor = Color.FromName(System.Drawing.KnownColor.Blue.ToString());
                        TextBoxSimilarObj.BackColor = Color.FromName(System.Drawing.KnownColor.Salmon.ToString());
                        TextBoxSimilarObj.SelectionBackColor = Color.FromName(System.Drawing.KnownColor.Lime.ToString());
                        TextBoxSimilarObj.Font = new Font(DataGridView1.Font, System.Drawing.FontStyle.Strikeout);
                        TextBoxSimilarObj.Alignment = osfDesigner.DataGridViewContentAlignment.НизПраво;
                        TextBoxSimilarObj.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
                        TextBoxSimilarObj.WrapMode = DataGridViewTriState.Истина;
                        DataGridViewTextBoxColumn1.DefaultCellStyle = TextBoxSimilarObj;	

                        DataGridViewTextBoxColumn1.DefaultValues = @"
Отображать == Истина
ТекстЗаголовка == КолонкаПолеВвода0
ТекстПодсказки == 
ИмяСвойстваДанных == 
ВесЗаполнения == 100
Закреплено == Ложь
МинимальнаяШирина == 5
РежимАвтоРазмера == НеУстановлено
Ширина == 100
ШиринаРазделителя == 0
ИзменяемыйРазмер == Истина
МаксимальнаяДлина == 32767
РежимСортировки == Автоматически
ТолькоЧтение == Ложь
ИмяКолонки == Таблица1КолонкаПолеВвода0
";
                        DataGridView1.Columns.Add(DataGridViewTextBoxColumn1);
                        // Закончили КолонкаПолеВвода =======================================================================================

                        // Создадим КолонкаКартинка =======================================================================================
                        DataGridViewImageColumn DataGridViewImageColumn1 = new DataGridViewImageColumn();
                        DataGridViewImageColumn1.Name = OneScriptFormsDesigner.RevertDataGridViewColumnName(DataGridView1, DataGridViewImageColumn1);
                        DataGridViewImageColumn1.NameColumn = DataGridView1.Name + DataGridViewImageColumn1.Name;
	
                        string ImagenameStyle = DataGridView1.Name + DataGridViewImageColumn1.Name + "СтильЯчейки";
                        DataGridViewCellStyle ImageSimilarObj = new DataGridViewCellStyle(ImagenameStyle, DataGridView1);
                        // Передадим свойства стиля.
                        ImageSimilarObj.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());
                        ImageSimilarObj.SelectionForeColor = Color.FromName(System.Drawing.KnownColor.Blue.ToString());
                        ImageSimilarObj.BackColor = Color.FromName(System.Drawing.KnownColor.Salmon.ToString());
                        ImageSimilarObj.SelectionBackColor = Color.FromName(System.Drawing.KnownColor.Lime.ToString());
                        ImageSimilarObj.Font = new Font(DataGridView1.Font, System.Drawing.FontStyle.Strikeout);
                        ImageSimilarObj.Alignment = osfDesigner.DataGridViewContentAlignment.НизПраво;
                        ImageSimilarObj.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
                        ImageSimilarObj.WrapMode = DataGridViewTriState.Истина;
                        DataGridViewImageColumn1.DefaultCellStyle = ImageSimilarObj;	

                        DataGridViewImageColumn1.DefaultValues = @"
Описание == 
Отображать == Истина
РазмещениеИзображения == Стандартное
ТекстЗаголовка == КолонкаКартинка0
ТекстПодсказки == 
ИмяСвойстваДанных == 
ВесЗаполнения == 100
Закреплено == Ложь
МинимальнаяШирина == 5
РежимАвтоРазмера == НеУстановлено
Ширина == 100
ШиринаРазделителя == 0
ИзменяемыйРазмер == Истина
РежимСортировки == НеСортируемый
ТолькоЧтение == Ложь
ИмяКолонки == Таблица1КолонкаКартинка0
Картинка == 
";
                        DataGridView1.Columns.Add(DataGridViewImageColumn1);
                        // Закончили КолонкаКартинка =======================================================================================

                        // Создадим КолонкаКнопка =======================================================================================
                        DataGridViewButtonColumn DataGridViewButtonColumn1 = new DataGridViewButtonColumn();
                        DataGridViewButtonColumn1.Name = OneScriptFormsDesigner.RevertDataGridViewColumnName(DataGridView1, DataGridViewButtonColumn1);
                        DataGridViewButtonColumn1.NameColumn = DataGridView1.Name + DataGridViewButtonColumn1.Name;
	
                        string ButtonnameStyle = DataGridView1.Name + DataGridViewButtonColumn1.Name + "СтильЯчейки";
                        DataGridViewCellStyle ButtonSimilarObj = new DataGridViewCellStyle(ButtonnameStyle, DataGridView1);
                        // Передадим свойства стиля.
                        ButtonSimilarObj.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());
                        ButtonSimilarObj.SelectionForeColor = Color.FromName(System.Drawing.KnownColor.Blue.ToString());
                        ButtonSimilarObj.BackColor = Color.FromName(System.Drawing.KnownColor.Salmon.ToString());
                        ButtonSimilarObj.SelectionBackColor = Color.FromName(System.Drawing.KnownColor.Lime.ToString());
                        ButtonSimilarObj.Font = new Font(DataGridView1.Font, System.Drawing.FontStyle.Strikeout);
                        ButtonSimilarObj.Alignment = osfDesigner.DataGridViewContentAlignment.НизПраво;
                        ButtonSimilarObj.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
                        ButtonSimilarObj.WrapMode = DataGridViewTriState.Истина;
                        DataGridViewButtonColumn1.DefaultCellStyle = ButtonSimilarObj;	

                        DataGridViewButtonColumn1.DefaultValues = @"
ИспользоватьТекстКакЗначение == Ложь
Отображать == Истина
ПлоскийСтиль == Стандартный
Текст == 
ТекстЗаголовка == КолонкаКнопка0
ТекстПодсказки == 
ИмяСвойстваДанных == 
ВесЗаполнения == 100
Закреплено == Ложь
МинимальнаяШирина == 5
РежимАвтоРазмера == НеУстановлено
Ширина == 100
ШиринаРазделителя == 0
ИзменяемыйРазмер == Истина
РежимСортировки == НеСортируемый
ТолькоЧтение == Ложь
ИмяКолонки == Таблица1КолонкаКнопка0
";
                        DataGridView1.Columns.Add(DataGridViewButtonColumn1);
                        // Закончили КолонкаКнопка =======================================================================================

                        // Создадим КолонкаПолеВыбора =======================================================================================
                        DataGridViewComboBoxColumn DataGridViewComboBoxColumn1 = new DataGridViewComboBoxColumn();
                        DataGridViewComboBoxColumn1.Name = OneScriptFormsDesigner.RevertDataGridViewColumnName(DataGridView1, DataGridViewComboBoxColumn1);
                        DataGridViewComboBoxColumn1.NameColumn = DataGridView1.Name + DataGridViewComboBoxColumn1.Name;
	
                        string ComboBoxnameStyle = DataGridView1.Name + DataGridViewComboBoxColumn1.Name + "СтильЯчейки";
                        DataGridViewCellStyle ComboBoxSimilarObj = new DataGridViewCellStyle(ComboBoxnameStyle, DataGridView1);
                        // Передадим свойства стиля.
                        ComboBoxSimilarObj.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());
                        ComboBoxSimilarObj.SelectionForeColor = Color.FromName(System.Drawing.KnownColor.Blue.ToString());
                        ComboBoxSimilarObj.BackColor = Color.FromName(System.Drawing.KnownColor.Salmon.ToString());
                        ComboBoxSimilarObj.SelectionBackColor = Color.FromName(System.Drawing.KnownColor.Lime.ToString());
                        ComboBoxSimilarObj.Font = new Font(DataGridView1.Font, System.Drawing.FontStyle.Strikeout);
                        ComboBoxSimilarObj.Alignment = osfDesigner.DataGridViewContentAlignment.НизПраво;
                        ComboBoxSimilarObj.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
                        ComboBoxSimilarObj.WrapMode = DataGridViewTriState.Истина;
                        DataGridViewComboBoxColumn1.DefaultCellStyle = ComboBoxSimilarObj;	

                        DataGridViewComboBoxColumn1.DefaultValues = @"
Отображать == Истина
ПлоскийСтиль == Стандартный
СтильОтображения == КнопкаСписка
ТекстЗаголовка == КолонкаПолеВыбора0
ТекстПодсказки == 
ИмяСвойстваДанных == 
Элементы == (Коллекция)
ВесЗаполнения == 100
Закреплено == Ложь
МинимальнаяШирина == 5
РежимАвтоРазмера == НеУстановлено
Ширина == 100
ШиринаРазделителя == 0
ИзменяемыйРазмер == Истина
МаксимумЭлементов == 8
Отсортирован == Ложь
РежимСортировки == НеСортируемый
ТолькоЧтение == Ложь
ИмяКолонки == Таблица1КолонкаПолеВыбора0
";
                        DataGridView1.Columns.Add(DataGridViewComboBoxColumn1);
                        // Закончили КолонкаПолеВыбора =======================================================================================

                        // Создадим КолонкаСсылка =======================================================================================
                        DataGridViewLinkColumn DataGridViewLinkColumn1 = new DataGridViewLinkColumn();
                        DataGridViewLinkColumn1.Name = OneScriptFormsDesigner.RevertDataGridViewColumnName(DataGridView1, DataGridViewLinkColumn1);
                        DataGridViewLinkColumn1.NameColumn = DataGridView1.Name + DataGridViewLinkColumn1.Name;
	
                        string LinknameStyle = DataGridView1.Name + DataGridViewLinkColumn1.Name + "СтильЯчейки";
                        DataGridViewCellStyle LinkSimilarObj = new DataGridViewCellStyle(LinknameStyle, DataGridView1);
                        // Передадим свойства стиля.
                        LinkSimilarObj.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());
                        LinkSimilarObj.SelectionForeColor = Color.FromName(System.Drawing.KnownColor.Blue.ToString());
                        LinkSimilarObj.BackColor = Color.FromName(System.Drawing.KnownColor.Salmon.ToString());
                        LinkSimilarObj.SelectionBackColor = Color.FromName(System.Drawing.KnownColor.Lime.ToString());
                        LinkSimilarObj.Font = new Font(DataGridView1.Font, System.Drawing.FontStyle.Strikeout);
                        LinkSimilarObj.Alignment = osfDesigner.DataGridViewContentAlignment.НизПраво;
                        LinkSimilarObj.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
                        LinkSimilarObj.WrapMode = DataGridViewTriState.Истина;
                        DataGridViewLinkColumn1.DefaultCellStyle = LinkSimilarObj;	

                        DataGridViewLinkColumn1.DefaultValues = @"
ИспользоватьТекстКакСсылку == Ложь
Отображать == Истина
Текст == 
ТекстЗаголовка == КолонкаСсылка0
ТекстПодсказки == 
ЦветАктивнойСсылки == Красный
ЦветПосещеннойСсылки == 128; 0; 128
ЦветСсылки == 0; 0; 255
ИмяСвойстваДанных == 
ВесЗаполнения == 100
Закреплено == Ложь
МинимальнаяШирина == 5
РежимАвтоРазмера == НеУстановлено
Ширина == 100
ШиринаРазделителя == 0
ИзменяемыйРазмер == Истина
ПоведениеСсылки == СистемныйПоУмолчанию
РежимСортировки == НеСортируемый
ТолькоЧтение == Ложь
ЦветПосещеннойИзменяется == Истина
ИмяКолонки == Таблица1КолонкаСсылка0
";
                        DataGridView1.Columns.Add(DataGridViewLinkColumn1);
                        // Закончили КолонкаСсылка =======================================================================================

                        // Создадим КолонкаФлажок =======================================================================================
                        DataGridViewCheckBoxColumn DataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
                        DataGridViewCheckBoxColumn1.Name = OneScriptFormsDesigner.RevertDataGridViewColumnName(DataGridView1, DataGridViewCheckBoxColumn1);
                        DataGridViewCheckBoxColumn1.NameColumn = DataGridView1.Name + DataGridViewCheckBoxColumn1.Name;
	
                        string CheckBoxnameStyle = DataGridView1.Name + DataGridViewCheckBoxColumn1.Name + "СтильЯчейки";
                        DataGridViewCellStyle CheckBoxSimilarObj = new DataGridViewCellStyle(CheckBoxnameStyle, DataGridView1);
                        // Передадим свойства стиля.
                        CheckBoxSimilarObj.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());
                        CheckBoxSimilarObj.SelectionForeColor = Color.FromName(System.Drawing.KnownColor.Blue.ToString());
                        CheckBoxSimilarObj.BackColor = Color.FromName(System.Drawing.KnownColor.Salmon.ToString());
                        CheckBoxSimilarObj.SelectionBackColor = Color.FromName(System.Drawing.KnownColor.Lime.ToString());
                        CheckBoxSimilarObj.Font = new Font(DataGridView1.Font, System.Drawing.FontStyle.Strikeout);
                        CheckBoxSimilarObj.Alignment = osfDesigner.DataGridViewContentAlignment.НизПраво;
                        CheckBoxSimilarObj.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
                        CheckBoxSimilarObj.WrapMode = DataGridViewTriState.Истина;
                        DataGridViewCheckBoxColumn1.DefaultCellStyle = CheckBoxSimilarObj;	

                        DataGridViewCheckBoxColumn1.DefaultValues = @"
Отображать == Истина
ПлоскийСтиль == Стандартный
ТекстЗаголовка == КолонкаФлажок0
ТекстПодсказки == 
ИмяСвойстваДанных == 
ВесЗаполнения == 100
Закреплено == Ложь
МинимальнаяШирина == 5
РежимАвтоРазмера == НеУстановлено
Ширина == 100
ШиринаРазделителя == 0
ИзменяемыйРазмер == Истина
РежимСортировки == НеСортируемый
ТолькоЧтение == Ложь
ТриСостояния == Ложь
ИмяКолонки == Таблица1КолонкаФлажок0
";
                        DataGridView1.Columns.Add(DataGridViewCheckBoxColumn1);
                        // Закончили КолонкаФлажок =======================================================================================
                    }
                    // Если это СписокЭлементов - Колонки.
                    else if (propertyValue.AsString() == "System.Windows.Forms.ListView+ColumnHeaderCollection")
                    {
                        // Создаем колонку списка элементов.
                        osfDesigner.ListView ListView1 = (osfDesigner.ListView)Comp1;
                        osfDesigner.ColumnHeader ColumnHeader1 = new ColumnHeader();
                        ColumnHeader1.Name = OneScriptFormsDesigner.RevertColumnHeaderName(ListView1);
                        ColumnHeader1.Text = "Колонка" + OneScriptFormsDesigner.ParseBetween(ColumnHeader1.Name, "Колонка", null);

                        ColumnHeader1.DefaultValues = @"ВыравниваниеТекста == Лево
Текст == 
ТипСортировки == Текст
Ширина == 60
(Name) == 
";
                        ListView1.Columns.Add(ColumnHeader1);
                    }
                    // Если это СтрокаСостояния - Панели.
                    else if (propertyValue.AsString() == "System.Windows.Forms.StatusBar+StatusBarPanelCollection")
                    {
                        // Создаем панель.
                        osfDesigner.StatusBar StatusBar1 = (osfDesigner.StatusBar)Comp1;
                        StatusBarPanel StatusBarPanel1 = new StatusBarPanel();
                        StatusBarPanel1.Name = OneScriptFormsDesigner.RevertStatusBarPanelName(StatusBar1);
                        StatusBarPanel1.Text = "Панель" + OneScriptFormsDesigner.ParseBetween(StatusBarPanel1.Name, "Панель", null);

                        StatusBarPanel1.DefaultValues = @"АвтоРазмер == Отсутствие
Значок == 
СтильГраницы == Утопленная
Текст == 
Ширина == 100
МинимальнаяШирина == 10
(Name) == 
";
                        StatusBar1.Panels.Add(StatusBarPanel1);
                        SetPropertyMethod(StatusBar1.Name, "ПоказатьПанели", ValueFactory.Create(true));
                    }
                    // Если это Элементы.
                    else if (propertyValue.AsString() == "System.Windows.Forms.ListView+ListViewItemCollection")
                    {
                        // Создаем элемент списка элементов.
                        osfDesigner.ListView ListView1 = (osfDesigner.ListView)Comp1;
                        ListViewItem ListViewItem1 = new ListViewItem();
                        ListViewItem1.Name = OneScriptFormsDesigner.RevertListViewItemName(ListView1);
                        string str1 = OneScriptFormsDesigner.ParseBetween(ListViewItem1.Name, "СписокЭлементов", null);
                        ListViewItem1.Text = "Элемент" + OneScriptFormsDesigner.ParseBetween(str1, "Элемент", null);

                        ListViewItem1.DefaultValues = @"ИспользоватьСтильДляПодэлементов == Истина
ОсновнойЦвет == ТекстОкна
Помечен == Ложь
Текст == 
ЦветФона == Окно
Шрифт == Microsoft Sans Serif; 7,8pt
Подэлементы == (Коллекция)
ИндексИзображения == -1
(Name) == 
";
                        ListView1.Items.Add(ListViewItem1);

                        // Создаем подэлемент списка элементов.
                        ListViewSubItem ListViewSubItem1 = new ListViewSubItem();
                        ListViewSubItem1.Name = OneScriptFormsDesigner.RevertListViewSubItemName(ListViewItem1);
                        ListViewSubItem1.Text = "Подэлемент" + OneScriptFormsDesigner.ParseBetween(ListViewSubItem1.Name, "Подэлемент", null);
                        ListViewSubItem1.DefaultValues = @"ОсновнойЦвет == ТекстОкна
Текст == 
ЦветФона == Окно
Шрифт == Microsoft Sans Serif; 7,8pt
(Name) == 
";
                        ListViewItem1.SubItems.Add(ListViewSubItem1);
                    }
                    // Если это Кнопки.
                    else if (propertyValue.AsString() == "System.Windows.Forms.ToolBar+ToolBarButtonCollection")
                    {
                        // Добавим кнопку панели инструментов.
                        osfDesigner.ToolBar ToolBar1 = (osfDesigner.ToolBar)Comp1;
                        System.Windows.Forms.ToolBarButton OriginalObj = new System.Windows.Forms.ToolBarButton();
                        osfDesigner.ToolBarButton SimilarObj = new osfDesigner.ToolBarButton();
                        OneScriptFormsDesigner.PassProperties(OriginalObj, SimilarObj); // Передадим свойства.
                        SimilarObj.OriginalObj = OriginalObj;
                        SimilarObj.Parent = OriginalObj.Parent;
                        SimilarObj.Style = (osfDesigner.ToolBarButtonStyle)OriginalObj.Style;
                        OriginalObj.Tag = SimilarObj;
                        SimilarObj.Name = OneScriptFormsDesigner.RevertToolBarButtonName(ToolBar1);
                        SimilarObj.Text = "Кн" + OneScriptFormsDesigner.ParseBetween(SimilarObj.Name, "Кн", null);
                        ToolBar1.Buttons.Add(OriginalObj);
                        SimilarObj.DefaultValues = @"Доступность == Истина
ИндексИзображения == -1
Нажата == Ложь
НейтральноеПоложение == Ложь
Отображать == Истина
Прямоугольник == 
Стиль == СтандартнаяТрехмерная
Текст == 
ТекстПодсказки == 
(Name) == 
";
                        // Добавим кнопку-разделитель панели инструментов.
                        System.Windows.Forms.ToolBarButton OriginalObj2 = new System.Windows.Forms.ToolBarButton();
                        osfDesigner.ToolBarButton SimilarObj2 = new osfDesigner.ToolBarButton();
                        OneScriptFormsDesigner.PassProperties(OriginalObj2, SimilarObj2); // Передадим свойства.
                        SimilarObj2.OriginalObj = OriginalObj2;
                        SimilarObj2.Parent = OriginalObj2.Parent;
                        OriginalObj2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
                        SimilarObj2.Style = (osfDesigner.ToolBarButtonStyle)OriginalObj2.Style;
                        OriginalObj2.Tag = SimilarObj2;
                        SimilarObj2.Name = OneScriptFormsDesigner.RevertToolBarButtonName(ToolBar1);
                        SimilarObj2.Text = "Кн" + OneScriptFormsDesigner.ParseBetween(SimilarObj2.Name, "Кн", null);
                        ToolBar1.Buttons.Add(OriginalObj2);
                        SimilarObj2.DefaultValues = @"Доступность == Истина
ИндексИзображения == -1
Нажата == Ложь
НейтральноеПоложение == Ложь
Отображать == Истина
Прямоугольник == 
Стиль == СтандартнаяТрехмерная
Текст == 
ТекстПодсказки == 
(Name) == 
";
                        // Добавим кнопку-тумблер панели инструментов.
                        System.Windows.Forms.ToolBarButton OriginalObj3 = new System.Windows.Forms.ToolBarButton();
                        osfDesigner.ToolBarButton SimilarObj3 = new osfDesigner.ToolBarButton();
                        OneScriptFormsDesigner.PassProperties(OriginalObj3, SimilarObj3); // Передадим свойства.
                        SimilarObj3.OriginalObj = OriginalObj3;
                        SimilarObj3.Parent = OriginalObj3.Parent;
                        OriginalObj3.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
                        SimilarObj3.Style = (osfDesigner.ToolBarButtonStyle)OriginalObj3.Style;
                        OriginalObj3.Tag = SimilarObj3;
                        SimilarObj3.Name = OneScriptFormsDesigner.RevertToolBarButtonName(ToolBar1);
                        SimilarObj3.Text = "Кн" + OneScriptFormsDesigner.ParseBetween(SimilarObj3.Name, "Кн", null);
                        ToolBar1.Buttons.Add(OriginalObj3);
                        SimilarObj3.DefaultValues = @"Доступность == Истина
ИндексИзображения == -1
Нажата == Ложь
НейтральноеПоложение == Ложь
Отображать == Истина
Прямоугольник == 
Стиль == СтандартнаяТрехмерная
Текст == 
ТекстПодсказки == 
(Name) == 
";
                        // Добавим кнопку с выпадающим списком панели инструментов.
                        System.Windows.Forms.ToolBarButton OriginalObj4 = new System.Windows.Forms.ToolBarButton();
                        osfDesigner.ToolBarButton SimilarObj4 = new osfDesigner.ToolBarButton();
                        OneScriptFormsDesigner.PassProperties(OriginalObj4, SimilarObj4); // Передадим свойства.
                        SimilarObj4.OriginalObj = OriginalObj4;
                        SimilarObj4.Parent = OriginalObj4.Parent;
                        OriginalObj4.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
                        SimilarObj4.Style = (osfDesigner.ToolBarButtonStyle)OriginalObj4.Style;
                        OriginalObj4.Tag = SimilarObj4;
                        SimilarObj4.Name = OneScriptFormsDesigner.RevertToolBarButtonName(ToolBar1);
                        SimilarObj4.Text = "Кн" + OneScriptFormsDesigner.ParseBetween(SimilarObj4.Name, "Кн", null);
                        ToolBar1.Buttons.Add(OriginalObj4);
                        SimilarObj4.DefaultValues = @"Доступность == Истина
ИндексИзображения == -1
Нажата == Ложь
НейтральноеПоложение == Ложь
Отображать == Истина
Прямоугольник == 
Стиль == СтандартнаяТрехмерная
Текст == 
ТекстПодсказки == 
(Name) == 
";
                    }
                    // Если это ВыбранныйОбъект.
                    else if (propertyValue.AsString() == "System.Object")
                    {
                        if (Comp1.GetType() == typeof(osfDesigner.PropertyGrid))
                        {
                            // Добавим кнопку и зададим её как ВыбранныйОбъект
                            string NameButton = AddControlMethod("Кнопка");
                            Component Component1 = (Component)OneScriptFormsDesigner.GetComponentByName(NameButton);
                            if (Component1 != null)
                            {
                                TrySetPropertyValue(componentType, propertyNameEn, Comp1, Component1);
                            }
                        }
                    }
                    // Если это ОбластьСсылки.
                    else if (propertyValue.AsString() == "System.Windows.Forms.LinkArea")
                    {
                        System.Windows.Forms.LinkArea LinkArea1 = new LinkArea(0, 1);
                        TrySetPropertyValue(componentType, propertyNameEn, Comp1, LinkArea1);
                    }
                    // Если это Изображение.
                    else if (propertyValue.AsString() == "System.Drawing.Image")
                    {
                        string Paht1 = "C:\\444\\Pic\\maslenica10.JPG";
                        System.Drawing.Bitmap Bitmap1 = new Bitmap(Paht1);
                        ImageEntry ImageEntry1 = new ImageEntry();
                        ImageEntry1.Image = Bitmap1;
                        ImageEntry1.Path = Paht1;
                        Bitmap1.Tag = Paht1;

                        if (Comp1.GetType() == typeof(osfDesigner.TabPage))
                        {
                            osfDesigner.TabPage SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(Comp1);
                            TrySetPropertyValue(componentType, propertyNameEn, SimilarObj, Bitmap1);
                        }
                        else
                        {
                            TrySetPropertyValue(componentType, propertyNameEn, Comp1, Bitmap1);
                        }
                    }
                    // Если это СписокИзображений.
                    else if (propertyValue.AsString() == "System.Windows.Forms.ImageList")
                    {
                        // Сначала добавим компонент СписокИзображений.
                        string NameImageList = AddControlMethod("СписокИзображений");
                        Component Component1 = (Component)OneScriptFormsDesigner.GetComponentByName(NameImageList);
                        if (Component1 != null)
                        {
                            osfDesigner.ImageList SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(Component1);

                            MyList MyList1 = new MyList();
                            osfDesigner.ImageList ImageList1 = SimilarObj;
                            System.Windows.Forms.ImageList.ImageCollection ImageCollection1 = ImageList1.OriginalObj.Images;
                            string Paht1 = "C:\\444\\Pic\\maslenica10.JPG";
                            string Paht2 = "C:\\444\\Pic\\Motif.jpg";
                            string Paht3 = "C:\\444\\Pic\\Games4.JPG";

                            ImageEntry ImageEntry1 = new ImageEntry();
                            ImageEntry1.Image = new Bitmap("" + Paht1);
                            ImageEntry1.Path = Paht1;
                            ImageEntry1.FileName = Paht1;
                            MyList1.Add(ImageEntry1);
                            ImageCollection1.Add(ImageEntry1.Image);

                            ImageEntry ImageEntry2 = new ImageEntry();
                            ImageEntry2.Image = new Bitmap("" + Paht2);
                            ImageEntry2.Path = Paht2;
                            ImageEntry2.FileName = Paht2;
                            MyList1.Add(ImageEntry2);
                            ImageCollection1.Add(ImageEntry2.Image);

                            ImageEntry ImageEntry3 = new ImageEntry();
                            ImageEntry3.Image = new Bitmap("" + Paht3);
                            ImageEntry3.Path = Paht3;
                            ImageEntry3.FileName = Paht3;
                            MyList1.Add(ImageEntry3);
                            ImageCollection1.Add(ImageEntry3.Image);

                            SimilarObj._images = MyList1;
                            TrySetPropertyValue(componentType, propertyNameEn, Comp1, Component1);
                        }
                    }
                    // Если это Значок.
                    else if (propertyValue.AsString() == "osfDesigner.MyIcon")
                    {
                        string Path1 = "C:\\444\\Pic\\tray2.ico";
                        System.Drawing.Icon Icon1 = new Icon(Path1);
                        MyIcon MyIcon1 = new MyIcon(Icon1);
                        MyIcon1.M_Icon = Icon1;
                        MyIcon1.Path = Path1;
                        TrySetPropertyValue(componentType, propertyNameEn, Comp1, MyIcon1);
                    }
                    // Если это Размер.
                    else if (propertyValue.AsString() == "System.Drawing.Size")
                    {
                        System.Drawing.Size Size1 = (System.Drawing.Size)componentType.GetProperty(propertyNameEn).GetValue(Comp1);
                        System.Drawing.Size newSize = new System.Drawing.Size(Size1.Width + 20, Size1.Height + 20);
                        if (propertyName == "МаксимальныйРазмер")
                        {
                            newSize = new System.Drawing.Size(Size1.Width + 120, Size1.Height + 120);
                        }
                        if (propertyName == "МинимальныйРазмер" && componentType == typeof(osfDesigner.Form))
                        {
                            newSize = new System.Drawing.Size(120, 120);
                        }
                        TrySetPropertyValue(componentType, propertyNameEn, Comp1, newSize);
                    }
                    // Если это Шрифт.
                    else if (propertyValue.AsString() == "System.Drawing.Font")
                    {
                        System.Drawing.Font Font1 = new System.Drawing.Font(this.Font.Name, this.Font.Size, System.Drawing.FontStyle.Strikeout);
                        TrySetPropertyValue(componentType, propertyNameEn, Comp1, Font1);
                    }
                    // Если это Положение.
                    else if (propertyValue.AsString() == "System.Drawing.Point")
                    {
                        if (componentType == typeof(osfDesigner.Form))
                        {
                            // Установим свойство НачальноеПоложение в значение Вручную (Manual) (то есть 0)
                            SetPropertyMethod("Форма_0", "НачальноеПоложение", ValueFactory.Create(0));
                        }
                        System.Drawing.Point Point1 = (System.Drawing.Point)componentType.GetProperty(propertyNameEn).GetValue(Comp1);
                        System.Drawing.Point newPoint = new Point(Point1.X + 20, Point1.Y + 20);
                        TrySetPropertyValue(componentType, propertyNameEn, Comp1, newPoint);
                    }
                    // Если это ВыделенныеДаты.
                    else if (propertyValue.AsString() == "osfDesigner.MyBoldedDatesList")
                    {
                        ((osfDesigner.MonthCalendar)Comp1)._boldedDates.Add(new DateEntry(new System.DateTime(2023, 11, 10)));
                        ((osfDesigner.MonthCalendar)Comp1)._boldedDates.Add(new DateEntry(new System.DateTime(2023, 11, 12)));
                        ((osfDesigner.MonthCalendar)Comp1)._boldedDates.Add(new DateEntry(new System.DateTime(2023, 11, 15)));
                    }
                    // Если это ВыделенныйДиапазон.
                    else if (propertyValue.AsString() == "System.Windows.Forms.SelectionRange")
                    {
                        System.Windows.Forms.SelectionRange sr = new System.Windows.Forms.SelectionRange(new System.DateTime(2023, 11, 10), new System.DateTime(2023, 11, 15));
                        TrySetPropertyValue(componentType, propertyNameEn, Comp1, sr);
                    }
                    // Если это ЕжегодныеДаты.
                    else if (propertyValue.AsString() == "osfDesigner.MyAnnuallyBoldedDatesList")
                    {
                        ((osfDesigner.MonthCalendar)Comp1)._annuallyBoldedDates.Add(new DateEntry(new System.DateTime(2023, 11, 10)));
                        ((osfDesigner.MonthCalendar)Comp1)._annuallyBoldedDates.Add(new DateEntry(new System.DateTime(2023, 11, 12)));
                        ((osfDesigner.MonthCalendar)Comp1)._annuallyBoldedDates.Add(new DateEntry(new System.DateTime(2023, 11, 15)));
                    }
                    // Если это ЕжемесячныеДаты.
                    else if (propertyValue.AsString() == "osfDesigner.MyMonthlyBoldedDatesList")
                    {
                        ((osfDesigner.MonthCalendar)Comp1)._monthlyBoldedDates.Add(new DateEntry(new System.DateTime(2023, 11, 10)));
                        ((osfDesigner.MonthCalendar)Comp1)._monthlyBoldedDates.Add(new DateEntry(new System.DateTime(2023, 11, 12)));
                        ((osfDesigner.MonthCalendar)Comp1)._monthlyBoldedDates.Add(new DateEntry(new System.DateTime(2023, 11, 15)));
                    }
                    // Если это Изображения.
                    else if (propertyValue.AsString() == "osfDesigner.MyList")
                    {
                        osfDesigner.ImageList SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(Comp1);

                        MyList MyList1 = new MyList();
                        osfDesigner.ImageList ImageList1 = SimilarObj;
                        System.Windows.Forms.ImageList.ImageCollection ImageCollection1 = ImageList1.OriginalObj.Images;
                        string Paht1 = "C:\\444\\Pic\\maslenica10.JPG";
                        string Paht2 = "C:\\444\\Pic\\Motif.jpg";
                        string Paht3 = "C:\\444\\Pic\\Games4.JPG";

                        ImageEntry ImageEntry1 = new ImageEntry();
                        ImageEntry1.Image = new Bitmap("" + Paht1);
                        ImageEntry1.Path = Paht1;
                        ImageEntry1.FileName = Paht1;
                        MyList1.Add(ImageEntry1);
                        ImageCollection1.Add(ImageEntry1.Image);

                        ImageEntry ImageEntry2 = new ImageEntry();
                        ImageEntry2.Image = new Bitmap("" + Paht2);
                        ImageEntry2.Path = Paht2;
                        ImageEntry2.FileName = Paht2;
                        MyList1.Add(ImageEntry2);
                        ImageCollection1.Add(ImageEntry2.Image);

                        ImageEntry ImageEntry3 = new ImageEntry();
                        ImageEntry3.Image = new Bitmap("" + Paht3);
                        ImageEntry3.Path = Paht3;
                        ImageEntry3.FileName = Paht3;
                        MyList1.Add(ImageEntry3);
                        ImageCollection1.Add(ImageEntry3.Image);

                        SimilarObj._images = MyList1;
                    }
                    // Если это Узлы.
                    else if (propertyValue.AsString() == "System.Windows.Forms.TreeNodeCollection")
                    {
                        osfDesigner.TreeView TreeView1 = (osfDesigner.TreeView)Comp1;

                        // Добавим узел Узел0.
                        MyTreeNode TreeNode1 = new MyTreeNode();
                        TreeNode1.TreeView = TreeView1;
                        TreeNode1.Name = OneScriptFormsDesigner.RevertNodeName(TreeView1);
                        TreeNode1.Text = "Узел" + OneScriptFormsDesigner.ParseBetween(TreeNode1.Name, "Узел", null);
                        TreeView1.Nodes.Add(TreeNode1);

                        // Добавим узел Узел1.
                        MyTreeNode TreeNode2 = new MyTreeNode();
                        TreeNode2.TreeView = TreeView1;
                        TreeNode2.Name = OneScriptFormsDesigner.RevertNodeName(TreeView1);
                        TreeNode2.Text = "Узел" + OneScriptFormsDesigner.ParseBetween(TreeNode2.Name, "Узел", null);
                        TreeView1.Nodes.Add(TreeNode2);

                        // Добавим узел Узел2.
                        MyTreeNode TreeNode3 = new MyTreeNode();
                        TreeNode3.TreeView = TreeView1;
                        TreeNode3.Name = OneScriptFormsDesigner.RevertNodeName(TreeView1);
                        TreeNode3.Text = "Узел" + OneScriptFormsDesigner.ParseBetween(TreeNode3.Name, "Узел", null);
                        TreeView1.Nodes.Add(TreeNode3);

                        // Добавим подузел Узел3.
                        MyTreeNode TreeNode4 = new MyTreeNode();
                        TreeNode4.TreeView = TreeView1;
                        TreeNode4.Name = OneScriptFormsDesigner.RevertNodeName(TreeView1);
                        TreeNode4.Text = "Узел" + OneScriptFormsDesigner.ParseBetween(TreeNode4.Name, "Узел", null);
                        TreeNode2.Nodes.Add(TreeNode4);
                        TreeNode2.Expand();

                        // Добавим подузел Узел4.
                        MyTreeNode TreeNode5 = new MyTreeNode();
                        TreeNode5.TreeView = TreeView1;
                        TreeNode5.Name = OneScriptFormsDesigner.RevertNodeName(TreeView1);
                        TreeNode5.Text = "Узел" + OneScriptFormsDesigner.ParseBetween(TreeNode5.Name, "Узел", null);
                        TreeNode2.Nodes.Add(TreeNode5);
                        TreeNode2.Expand();
                    }
                    // Если это Меню.
                    else if (propertyValue.AsString() == "System.Windows.Forms.MainMenu")
                    {
                        // Сначала добавим компонент ГлавноеМеню.
                        string NameMainMenu = AddControlMethod("ГлавноеМеню");
                        // Найдем и выделим компонент с именем NameMainMenu.
                        Component Component1 = (Component)OneScriptFormsDesigner.GetComponentByName(NameMainMenu);
                        if (Component1 != null)
                        {
                            System.Windows.Forms.MainMenu MainMenu1 = ((System.Windows.Forms.MainMenu)Component1);
                            osfDesigner.MainMenu SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(MainMenu1);
                            System.Windows.Forms.TreeView TreeView1 = SimilarObj.TreeView;
                            SimilarObj.FrmMenuItems = new frmMenuItems(SimilarObj);

                            //==========================================================================
                            // Добавим Меню0.
                            MenuItemEntry MenuItemEntry0 = new MenuItemEntry();
                            MenuItemEntry0.Name = OneScriptFormsDesigner.RevertMenuName(SimilarObj);
                            MenuItemEntry0.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry0.Name.Replace("ГлавноеМеню", ""), "Меню", null);
                            SimilarObj.MenuItems.Add(MenuItemEntry0.M_MenuItem);
                            OneScriptFormsDesigner.AddToDictionary(MenuItemEntry0.M_MenuItem, MenuItemEntry0);
                            TreeNode TreeNode0 = TreeView1.Nodes.Add("Меню0", MenuItemEntry0.Text);
                            TreeNode0.Tag = MenuItemEntry0;
                            TreeNode0.Text = MenuItemEntry0.Text;
                            //==========================================================================
                            // Добавим подменю Меню1.
                            MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                            MenuItemEntry1.Name = OneScriptFormsDesigner.RevertMenuName(SimilarObj);
                            MenuItemEntry1.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry1.Name.Replace("ГлавноеМеню", ""), "Меню", null);
                            MenuItemEntry MenuItemParent1 = (MenuItemEntry)TreeView1.Nodes.Find("Меню0", true)[0].Tag;
                            MenuItemParent1.MenuItems.Add(MenuItemEntry1.M_MenuItem);
                            OneScriptFormsDesigner.AddToDictionary(MenuItemEntry1.M_MenuItem, MenuItemEntry1);
                            TreeNode TreeNode1 = TreeView1.Nodes.Add("Меню1", MenuItemEntry1.Text);
                            TreeNode1.Tag = MenuItemEntry1;
                            TreeNode1.Text = MenuItemEntry1.Text;
                            TreeView1.Nodes.Find("Меню0", true)[0].Nodes.Add(TreeNode1);
                            //==========================================================================
                            // Добавим подменю Меню2.
                            MenuItemEntry MenuItemEntry2 = new MenuItemEntry();
                            MenuItemEntry2.Name = OneScriptFormsDesigner.RevertMenuName(SimilarObj);
                            MenuItemEntry2.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry2.Name.Replace("ГлавноеМеню", ""), "Меню", null);
                            MenuItemEntry MenuItemParent2 = (MenuItemEntry)TreeView1.Nodes.Find("Меню0", true)[0].Tag;
                            MenuItemParent2.MenuItems.Add(MenuItemEntry2.M_MenuItem);
                            OneScriptFormsDesigner.AddToDictionary(MenuItemEntry2.M_MenuItem, MenuItemEntry2);
                            TreeNode TreeNode2 = TreeView1.Nodes.Add("Меню2", MenuItemEntry2.Text);
                            TreeNode2.Tag = MenuItemEntry2;
                            TreeNode2.Text = MenuItemEntry2.Text;
                            TreeView1.Nodes.Find("Меню0", true)[0].Nodes.Add(TreeNode2);
                            //==========================================================================
                            // Добавим подменю Сепаратор1.
                            MenuItemEntry MenuItemEntry3 = new MenuItemEntry();
                            MenuItemEntry3.Name = OneScriptFormsDesigner.RevertSeparatorName(SimilarObj);
                            // Имя в виде тире не присваивать, заменять на тире только во время формирования сценария.
                            MenuItemEntry3.Text = "Сепаратор" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry3.Name.Replace("ГлавноеМеню", ""), "Сепаратор", null);
                            MenuItemEntry MenuItemParent3 = (MenuItemEntry)TreeView1.Nodes.Find("Меню0", true)[0].Tag;
                            MenuItemParent3.MenuItems.Add(MenuItemEntry3.M_MenuItem);
                            OneScriptFormsDesigner.AddToDictionary(MenuItemEntry3.M_MenuItem, MenuItemEntry3);
                            TreeNode TreeNode3 = TreeView1.Nodes.Add("Сепаратор1", MenuItemEntry3.Text);
                            TreeNode3.Tag = MenuItemEntry3;
                            TreeNode3.Text = MenuItemEntry3.Text;
                            TreeView1.Nodes.Find("Меню0", true)[0].Nodes.Add(TreeNode3);
                            //==========================================================================
                            // Добавим подменю Меню3.
                            MenuItemEntry MenuItemEntry4 = new MenuItemEntry();
                            MenuItemEntry4.Name = OneScriptFormsDesigner.RevertMenuName(SimilarObj);
                            MenuItemEntry4.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry4.Name.Replace("ГлавноеМеню", ""), "Меню", null);
                            MenuItemEntry MenuItemParent4 = (MenuItemEntry)TreeView1.Nodes.Find("Меню0", true)[0].Tag;
                            MenuItemParent4.MenuItems.Add(MenuItemEntry4.M_MenuItem);
                            OneScriptFormsDesigner.AddToDictionary(MenuItemEntry4.M_MenuItem, MenuItemEntry4);
                            TreeNode TreeNode4 = TreeView1.Nodes.Add("Меню3", MenuItemEntry4.Text);
                            TreeNode4.Tag = MenuItemEntry4;
                            TreeNode4.Text = MenuItemEntry4.Text;
                            TreeView1.Nodes.Find("Меню0", true)[0].Nodes.Add(TreeNode4);
                            //==========================================================================
                            // Добавим Меню4.
                            MenuItemEntry MenuItemEntry5 = new MenuItemEntry();
                            MenuItemEntry5.Name = OneScriptFormsDesigner.RevertMenuName(SimilarObj);
                            MenuItemEntry5.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry5.Name.Replace("ГлавноеМеню", ""), "Меню", null);
                            SimilarObj.MenuItems.Add(MenuItemEntry5.M_MenuItem);
                            OneScriptFormsDesigner.AddToDictionary(MenuItemEntry5.M_MenuItem, MenuItemEntry5);
                            TreeNode TreeNode5 = TreeView1.Nodes.Add("Меню4", MenuItemEntry5.Text);
                            TreeNode5.Tag = MenuItemEntry5;
                            TreeNode5.Text = MenuItemEntry5.Text;
                            //==========================================================================
                            // Добавим подменю Меню5.
                            MenuItemEntry MenuItemEntry6 = new MenuItemEntry();
                            MenuItemEntry6.Name = OneScriptFormsDesigner.RevertMenuName(SimilarObj);
                            MenuItemEntry6.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry6.Name.Replace("ГлавноеМеню", ""), "Меню", null);
                            MenuItemEntry MenuItemParent6 = (MenuItemEntry)TreeView1.Nodes.Find("Меню4", true)[0].Tag;
                            MenuItemParent6.MenuItems.Add(MenuItemEntry6.M_MenuItem);
                            OneScriptFormsDesigner.AddToDictionary(MenuItemEntry6.M_MenuItem, MenuItemEntry6);
                            TreeNode TreeNode6 = TreeView1.Nodes.Add("Меню5", MenuItemEntry6.Text);
                            TreeNode6.Tag = MenuItemEntry6;
                            TreeNode6.Text = MenuItemEntry6.Text;
                            TreeView1.Nodes.Find("Меню4", true)[0].Nodes.Add(TreeNode6);
                            //==========================================================================
                            // Добавим подменю Меню6.
                            MenuItemEntry MenuItemEntry7 = new MenuItemEntry();
                            MenuItemEntry7.Name = OneScriptFormsDesigner.RevertMenuName(SimilarObj);
                            MenuItemEntry7.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry7.Name.Replace("ГлавноеМеню", ""), "Меню", null);
                            MenuItemEntry MenuItemParent7 = (MenuItemEntry)TreeView1.Nodes.Find("Меню5", true)[0].Tag;
                            MenuItemParent7.MenuItems.Add(MenuItemEntry7.M_MenuItem);
                            OneScriptFormsDesigner.AddToDictionary(MenuItemEntry7.M_MenuItem, MenuItemEntry7);
                            TreeNode TreeNode7 = TreeView1.Nodes.Add("Меню6", MenuItemEntry7.Text);
                            TreeNode7.Tag = MenuItemEntry7;
                            TreeNode7.Text = MenuItemEntry7.Text;
                            TreeView1.Nodes.Find("Меню5", true)[0].Nodes.Add(TreeNode7);
                            //==========================================================================
                            // Добавим подменю Меню7.
                            MenuItemEntry MenuItemEntry8 = new MenuItemEntry();
                            MenuItemEntry8.Name = OneScriptFormsDesigner.RevertMenuName(SimilarObj);
                            MenuItemEntry8.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry8.Name.Replace("ГлавноеМеню", ""), "Меню", null);
                            MenuItemEntry MenuItemParent8 = (MenuItemEntry)TreeView1.Nodes.Find("Меню6", true)[0].Tag;
                            MenuItemParent8.MenuItems.Add(MenuItemEntry8.M_MenuItem);
                            OneScriptFormsDesigner.AddToDictionary(MenuItemEntry8.M_MenuItem, MenuItemEntry8);
                            TreeNode TreeNode8 = TreeView1.Nodes.Add("Меню6", MenuItemEntry8.Text);
                            TreeNode8.Tag = MenuItemEntry8;
                            TreeNode8.Text = MenuItemEntry8.Text;
                            TreeView1.Nodes.Find("Меню6", true)[0].Nodes.Add(TreeNode8);
                            //==========================================================================
                            ((Form)Comp1).Menu = MainMenu1;
                        }
                    }
                    // Если это Форма - Значок.
                    else if (propertyValue.AsString() == "System.Drawing.Icon")
                    {
                        string Path1 = "C:\\444\\Pic\\tray2.ico";
                        System.Drawing.Icon Icon1 = new Icon(Path1);
                        MyIcon MyIcon1 = new MyIcon(Icon1);
                        MyIcon1.M_Icon = Icon1;
                        MyIcon1.Path = Path1;
                        ((osfDesigner.Form)Comp1).Icon = MyIcon1;
                    }
                    // Если это ГлавноеМеню.
                    else if (propertyValue.AsString() == "System.Windows.Forms.Menu+MenuItemCollection")
                    {
                        System.Windows.Forms.MainMenu MainMenu1 = ((System.Windows.Forms.MainMenu)Comp1);
                        osfDesigner.MainMenu SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(Comp1);
                        System.Windows.Forms.TreeView TreeView1 = SimilarObj.TreeView;
                        SimilarObj.FrmMenuItems = new frmMenuItems(SimilarObj);

                        //==========================================================================
                        // Добавим Меню0.
                        MenuItemEntry MenuItemEntry0 = new MenuItemEntry();
                        MenuItemEntry0.Name = OneScriptFormsDesigner.RevertMenuName(SimilarObj);
                        MenuItemEntry0.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry0.Name.Replace("ГлавноеМеню", ""), "Меню", null);
                        SimilarObj.MenuItems.Add(MenuItemEntry0.M_MenuItem);
                        OneScriptFormsDesigner.AddToDictionary(MenuItemEntry0.M_MenuItem, MenuItemEntry0);
                        TreeNode TreeNode0 = TreeView1.Nodes.Add("Меню0", MenuItemEntry0.Text);
                        TreeNode0.Tag = MenuItemEntry0;
                        TreeNode0.Text = MenuItemEntry0.Text;
                        //==========================================================================
                        // Добавим подменю Меню1.
                        MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                        MenuItemEntry1.Name = OneScriptFormsDesigner.RevertMenuName(SimilarObj);
                        MenuItemEntry1.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry1.Name.Replace("ГлавноеМеню", ""), "Меню", null);
                        MenuItemEntry MenuItemParent1 = (MenuItemEntry)TreeView1.Nodes.Find("Меню0", true)[0].Tag;
                        MenuItemParent1.MenuItems.Add(MenuItemEntry1.M_MenuItem);
                        OneScriptFormsDesigner.AddToDictionary(MenuItemEntry1.M_MenuItem, MenuItemEntry1);
                        TreeNode TreeNode1 = TreeView1.Nodes.Add("Меню1", MenuItemEntry1.Text);
                        TreeNode1.Tag = MenuItemEntry1;
                        TreeNode1.Text = MenuItemEntry1.Text;
                        TreeView1.Nodes.Find("Меню0", true)[0].Nodes.Add(TreeNode1);
                        //==========================================================================
                        // Добавим подменю Меню2.
                        MenuItemEntry MenuItemEntry2 = new MenuItemEntry();
                        MenuItemEntry2.Name = OneScriptFormsDesigner.RevertMenuName(SimilarObj);
                        MenuItemEntry2.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry2.Name.Replace("ГлавноеМеню", ""), "Меню", null);
                        MenuItemEntry MenuItemParent2 = (MenuItemEntry)TreeView1.Nodes.Find("Меню0", true)[0].Tag;
                        MenuItemParent2.MenuItems.Add(MenuItemEntry2.M_MenuItem);
                        OneScriptFormsDesigner.AddToDictionary(MenuItemEntry2.M_MenuItem, MenuItemEntry2);
                        TreeNode TreeNode2 = TreeView1.Nodes.Add("Меню2", MenuItemEntry2.Text);
                        TreeNode2.Tag = MenuItemEntry2;
                        TreeNode2.Text = MenuItemEntry2.Text;
                        TreeView1.Nodes.Find("Меню0", true)[0].Nodes.Add(TreeNode2);
                        //==========================================================================
                        // Добавим подменю Сепаратор1.
                        MenuItemEntry MenuItemEntry3 = new MenuItemEntry();
                        MenuItemEntry3.Name = OneScriptFormsDesigner.RevertSeparatorName(SimilarObj);
                        // Имя в виде тире не присваивать, заменять на тире только во время формирования сценария.
                        MenuItemEntry3.Text = "Сепаратор" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry3.Name.Replace("ГлавноеМеню", ""), "Сепаратор", null);
                        MenuItemEntry MenuItemParent3 = (MenuItemEntry)TreeView1.Nodes.Find("Меню0", true)[0].Tag;
                        MenuItemParent3.MenuItems.Add(MenuItemEntry3.M_MenuItem);
                        OneScriptFormsDesigner.AddToDictionary(MenuItemEntry3.M_MenuItem, MenuItemEntry3);
                        TreeNode TreeNode3 = TreeView1.Nodes.Add("Сепаратор1", MenuItemEntry3.Text);
                        TreeNode3.Tag = MenuItemEntry3;
                        TreeNode3.Text = MenuItemEntry3.Text;
                        TreeView1.Nodes.Find("Меню0", true)[0].Nodes.Add(TreeNode3);
                        //==========================================================================
                        // Добавим подменю Меню3.
                        MenuItemEntry MenuItemEntry4 = new MenuItemEntry();
                        MenuItemEntry4.Name = OneScriptFormsDesigner.RevertMenuName(SimilarObj);
                        MenuItemEntry4.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry4.Name.Replace("ГлавноеМеню", ""), "Меню", null);
                        MenuItemEntry MenuItemParent4 = (MenuItemEntry)TreeView1.Nodes.Find("Меню0", true)[0].Tag;
                        MenuItemParent4.MenuItems.Add(MenuItemEntry4.M_MenuItem);
                        OneScriptFormsDesigner.AddToDictionary(MenuItemEntry4.M_MenuItem, MenuItemEntry4);
                        TreeNode TreeNode4 = TreeView1.Nodes.Add("Меню3", MenuItemEntry4.Text);
                        TreeNode4.Tag = MenuItemEntry4;
                        TreeNode4.Text = MenuItemEntry4.Text;
                        TreeView1.Nodes.Find("Меню0", true)[0].Nodes.Add(TreeNode4);
                        //==========================================================================
                        // Добавим Меню4.
                        MenuItemEntry MenuItemEntry5 = new MenuItemEntry();
                        MenuItemEntry5.Name = OneScriptFormsDesigner.RevertMenuName(SimilarObj);
                        MenuItemEntry5.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry5.Name.Replace("ГлавноеМеню", ""), "Меню", null);
                        SimilarObj.MenuItems.Add(MenuItemEntry5.M_MenuItem);
                        OneScriptFormsDesigner.AddToDictionary(MenuItemEntry5.M_MenuItem, MenuItemEntry5);
                        TreeNode TreeNode5 = TreeView1.Nodes.Add("Меню4", MenuItemEntry5.Text);
                        TreeNode5.Tag = MenuItemEntry5;
                        TreeNode5.Text = MenuItemEntry5.Text;
                        //==========================================================================
                        // Добавим подменю Меню5.
                        MenuItemEntry MenuItemEntry6 = new MenuItemEntry();
                        MenuItemEntry6.Name = OneScriptFormsDesigner.RevertMenuName(SimilarObj);
                        MenuItemEntry6.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry6.Name.Replace("ГлавноеМеню", ""), "Меню", null);
                        MenuItemEntry MenuItemParent6 = (MenuItemEntry)TreeView1.Nodes.Find("Меню4", true)[0].Tag;
                        MenuItemParent6.MenuItems.Add(MenuItemEntry6.M_MenuItem);
                        OneScriptFormsDesigner.AddToDictionary(MenuItemEntry6.M_MenuItem, MenuItemEntry6);
                        TreeNode TreeNode6 = TreeView1.Nodes.Add("Меню5", MenuItemEntry6.Text);
                        TreeNode6.Tag = MenuItemEntry6;
                        TreeNode6.Text = MenuItemEntry6.Text;
                        TreeView1.Nodes.Find("Меню4", true)[0].Nodes.Add(TreeNode6);
                        //==========================================================================
                        // Добавим подменю Меню6.
                        MenuItemEntry MenuItemEntry7 = new MenuItemEntry();
                        MenuItemEntry7.Name = OneScriptFormsDesigner.RevertMenuName(SimilarObj);
                        MenuItemEntry7.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry7.Name.Replace("ГлавноеМеню", ""), "Меню", null);
                        MenuItemEntry MenuItemParent7 = (MenuItemEntry)TreeView1.Nodes.Find("Меню5", true)[0].Tag;
                        MenuItemParent7.MenuItems.Add(MenuItemEntry7.M_MenuItem);
                        OneScriptFormsDesigner.AddToDictionary(MenuItemEntry7.M_MenuItem, MenuItemEntry7);
                        TreeNode TreeNode7 = TreeView1.Nodes.Add("Меню6", MenuItemEntry7.Text);
                        TreeNode7.Tag = MenuItemEntry7;
                        TreeNode7.Text = MenuItemEntry7.Text;
                        TreeView1.Nodes.Find("Меню5", true)[0].Nodes.Add(TreeNode7);
                        //==========================================================================
                        // Добавим подменю Меню7.
                        MenuItemEntry MenuItemEntry8 = new MenuItemEntry();
                        MenuItemEntry8.Name = OneScriptFormsDesigner.RevertMenuName(SimilarObj);
                        MenuItemEntry8.Text = "Меню" + OneScriptFormsDesigner.ParseBetween(MenuItemEntry8.Name.Replace("ГлавноеМеню", ""), "Меню", null);
                        MenuItemEntry MenuItemParent8 = (MenuItemEntry)TreeView1.Nodes.Find("Меню6", true)[0].Tag;
                        MenuItemParent8.MenuItems.Add(MenuItemEntry8.M_MenuItem);
                        OneScriptFormsDesigner.AddToDictionary(MenuItemEntry8.M_MenuItem, MenuItemEntry8);
                        TreeNode TreeNode8 = TreeView1.Nodes.Add("Меню6", MenuItemEntry8.Text);
                        TreeNode8.Tag = MenuItemEntry8;
                        TreeNode8.Text = MenuItemEntry8.Text;
                        TreeView1.Nodes.Find("Меню6", true)[0].Nodes.Add(TreeNode8);
                        //==========================================================================
                        ((Form)OneScriptFormsDesigner.RootComponent).Menu = MainMenu1;
                    }
                    // Если это просто строка.
                    else
                    {
                        if (Comp1.GetType() == typeof(System.Windows.Forms.TabPage))
                        {
                            osfDesigner.TabPage SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(Comp1);
                            TrySetPropertyValue(SimilarObj.GetType(), propertyNameEn, SimilarObj, propertyValue.AsString());
                        }
                        else
                        {
                            TrySetPropertyValue(componentType, propertyNameEn, Comp1, propertyValue.AsString());
                        }
                    }
                }
                // Если это Курсор.
                else if (propertyValueType.ToString() == "osf.ClCursor")
                {
                    TrySetPropertyValue(componentType, propertyNameEn, Comp1, ((dynamic)propertyValue.AsObject()).Base_obj.M_Cursor);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("propertyValue.AsString() = " + propertyValue.AsString() + "\r\n" +
                        "propertyValueType.ToString() = " + propertyValueType.ToString() + "\r\n" +
                        //"col = " + col + "\r\n" +
                        //"col = " + col + "\r\n" +
                        //"col = " + col + "\r\n" +
                        "");
                }

                // Позиционируемся на компоненте и обновим его.
                if (iSel == null)
                {
                    return;
                }
                IDesignSurfaceExt surface = OneScriptFormsDesigner.ActiveDesignSurface;
                IDesignerHost idh = (IDesignerHost)surface.GetIDesignerHost();
                OneScriptFormsDesigner.PropertyGrid.SelectedObject = idh.RootComponent;
                iSel.SetSelectedComponents(new IComponent[] { Comp1 });
                OneScriptFormsDesigner.PropertyGridHost.ReloadTreeView();
                OneScriptFormsDesigner.PropertyGridHost.ChangeSelectNode((Component)Comp1);
            }
        }

        public delegate IValue GetPropertyValue(string controlName, string propertyName); // ПолучитьЗначениеСвойства
        public IValue GetPropertyValueMethod(string controlName, string propertyName)
        {
            // Найдем и выделим компонент с именем controlName.
            Component Comp1 = (Component)OneScriptFormsDesigner.GetComponentByName(controlName);
            if (Comp1 != null)
            {
                string valueToString;
                Type valueType;
                string propertyNameEn;

                if (Comp1.GetType() == typeof(System.Windows.Forms.TabPage))
                {
                    // Для Comp1 уже создан дублер, получим его.
                    osfDesigner.TabPage SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(Comp1);
                    propertyNameEn = OneScriptFormsDesigner.GetPropName(SimilarObj, propertyName);
                }
                else
                {
                    propertyNameEn = OneScriptFormsDesigner.GetPropName(Comp1, propertyName);
                }
                // Получим для компонента значение свойства.
                dynamic val1;
                try
                {
                    val1 = Comp1.GetType().GetProperty(propertyNameEn).GetValue(Comp1);
                }
                catch
                {
                    try
                    {
                        val1 = Comp1.GetType().BaseType.GetProperty(propertyNameEn).GetValue(Comp1);
                    }
                    catch
                    {
                        return null;
                    }
                }
                try
                {
                    valueType = val1.GetType();
                    valueToString = val1.ToString();
                }
                catch
                {
                    return null;
                }

                if (valueType == typeof(System.Decimal))
                {
                    return ValueFactory.Create((System.Decimal)val1);
                }
                else if(valueType == typeof(System.Int32))
                {
                    return ValueFactory.Create((System.Int32)val1);
                }
                else if (valueType == typeof(System.String))
                {
                    return ValueFactory.Create((String)val1);
                }
                else if (valueType == typeof(System.Boolean))
                {
                    return ValueFactory.Create((Boolean)val1);
                }
                else if (valueType == typeof(System.DateTime))
                {
                    return ValueFactory.Create((DateTime)val1);
                }
                else if (valueType == typeof(System.Char))
                {
                    string str1 = System.Char.ToString(val1);
                    int code = System.Char.ConvertToUtf32(str1, 0);
                    if (code == 0)
                    {
                        return ValueFactory.Create("");
                    }
                    else
                    {
                        return ValueFactory.Create("" + str1);
                    }
                }
                else if (valueType == typeof(System.Drawing.Color) ||
                    valueType == typeof(System.Drawing.Font) ||
                    valueType == typeof(System.Drawing.Size) ||
                    valueType == typeof(System.Drawing.Point) ||
                    valueType == typeof(System.Windows.Forms.Day)
                    )
                {
                    return ValueFactory.Create(valueToString);
                }
                else if (valueType.BaseType == typeof(System.Enum))
                {
                    return ValueFactory.Create((int)val1);
                }
                else
                {
                    return ValueFactory.Create(valueToString);
                }
            }
            return null;
        }

        public delegate string GetPropertyType(string controlName, string propertyName); // ПолучитьТипСвойства
        public string GetPropertyTypeMethod(string controlName, string propertyName)
        {
            Component Comp1 = (Component)OneScriptFormsDesigner.GetComponentByName(controlName);
            if (Comp1 != null)
            {
                Type componentType;
                string propertyNameEn;

                if (Comp1.GetType() == typeof(System.Windows.Forms.MainMenu))
                {
                    // Для Comp1 уже создан дублер, получим его.
                    osfDesigner.MainMenu SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(Comp1);
                    componentType = SimilarObj.GetType();
                    propertyNameEn = OneScriptFormsDesigner.GetPropName(SimilarObj, propertyName);
                }
                else if (propertyName.Contains("ToolTip на"))
                {
                    Component ToolTipComp1 = (Component)OneScriptFormsDesigner.GetComponentByName("Подсказка1");
                    if (ToolTipComp1 != null)
                    {
                        return "System.Collections.Hashtable";
                    }
                    // Создадим подсказку.
                    ToolboxItem toolToolTip1 = new ToolboxItem(typeof(osfDesigner.ToolTip));
                    Component comp1 = (Component)toolToolTip1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                    // Установим подсказку для Comp1.
                    string toolTipName = comp1.Site.Name;
                    System.Windows.Forms.ToolTip ToolTip1 = (System.Windows.Forms.ToolTip)OneScriptFormsDesigner.GetComponentByName(toolTipName);
                    string caption = "Это " + toolTipName;
                    ToolTip1.SetToolTip(((dynamic)Comp1), caption);
                    ((dynamic)Comp1).ToolTip[toolTipName] = caption;

                    return "System.Collections.Hashtable";
                }
                else if (Comp1.GetType() == typeof(System.Windows.Forms.ImageList))
                {
                    // Для Comp1 уже создан дублер, получим его.
                    osfDesigner.ImageList SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(Comp1);
                    componentType = SimilarObj.GetType();
                    propertyNameEn = OneScriptFormsDesigner.GetPropName(SimilarObj, propertyName);
                }
                else if (Comp1.GetType() == typeof(System.Windows.Forms.TabPage))
                {
                    // Для Comp1 уже создан дублер, получим его.
                    osfDesigner.TabPage SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(Comp1);
                    componentType = SimilarObj.GetType();
                    propertyNameEn = OneScriptFormsDesigner.GetPropName(SimilarObj, propertyName);
                }
                else
                {
                    componentType = Comp1.GetType();
                    propertyNameEn = OneScriptFormsDesigner.GetPropName(Comp1, propertyName);
                }

                // Получим для компонента тип свойства.
                try
                {
                    return componentType.GetProperty(propertyNameEn).PropertyType.ToString();
                }
                catch
                {
                    try
                    {
                        return componentType.BaseType.GetProperty(propertyNameEn).PropertyType.ToString();
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        public delegate void CloseDesigner(); // ЗакрытьДизайнер
        public void CloseDesignerMethod()
        {
            Dictionary<System.Windows.Forms.TabPage, bool>.KeyCollection keys = OneScriptFormsDesigner.dictionaryTabPageChanged.Keys;
            System.Windows.Forms.TabControl.TabPageCollection TabPageCollection1 = pDesigner.TabControl.TabPages;
            foreach (System.Windows.Forms.TabPage item in TabPageCollection1)
            {
                OneScriptFormsDesigner.dictionaryTabPageChanged[item] = false;
            }
            ((osfDesigner.pDesignerMainForm)osfDesigner.Program.pDesignerMainForm1).Close();
        }

        public delegate void LoadForm(string fileName); // ОткрытьФорму
        public void LoadFormMethod(string fileName)
        {
            ((osfDesigner.pDesignerMainForm)osfDesigner.Program.pDesignerMainForm1).LoadForm_Click(fileName, false);
        }

        public delegate void RunScript(); // ЗапуститьСценарий
        public void RunScriptMethod()
        {
            OneScriptFormsDesigner.PropertyGrid.Refresh();
            Form savedForm = (Form)OneScriptFormsDesigner.DesignerHost.RootComponent;
            string Script = SaveScript.GetScriptText();
            if (OneScriptFormsDesigner.RootComponent.ScriptStyle == ScriptStyle.СтильПриложения)
            {
                string strFind = @"// маркерКонцаПроцедуры
КонецПроцедуры

ПодготовкаКомпонентов();";
                string strReplace = @"// маркерКонцаПроцедуры
КонецПроцедуры" + Environment.NewLine +
"ПодключитьВнешнююКомпоненту(" + "\u0022" + Settings.Default["dllPath"] + "\u0022" + @");" + Environment.NewLine +
@"" + savedForm.NameObjectOneScriptForms + @" = Новый ФормыДляОдноСкрипта();
ПриСозданииФормы(" + savedForm.NameObjectOneScriptForms + @".Форма());
" + savedForm.NameObjectOneScriptForms + @".ЗапуститьОбработкуСобытий();";
                Script = Script.Replace(strFind, strReplace);
            }
            if ((bool)Settings.Default["visualSyleForms"])
            {
                string strFind = @"" + savedForm.NameObjectOneScriptForms + @" = Новый ФормыДляОдноСкрипта();";
                string strReplace = @"" + savedForm.NameObjectOneScriptForms + @" = Новый ФормыДляОдноСкрипта();" + Environment.NewLine +
                @"    " + savedForm.NameObjectOneScriptForms + @".ВключитьВизуальныеСтили();";
                Script = Script.Replace(strFind, strReplace);
            }
            string strTempFile = String.Format(Path.GetTempPath() + "oscript_{0}_{1}.os", DateTime.Now.ToString("yyyyMMddHHmmssfff"), Guid.NewGuid().ToString().Replace("-", ""));
            File.WriteAllText(strTempFile, Script, Encoding.UTF8);

            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            psi.Arguments = strTempFile;
            psi.FileName = (string)Settings.Default["osPath"];
            System.Diagnostics.Process.Start(psi);
        }

        public delegate void UnloadForm(string fileName); // СохранитьФорму
        public void UnloadFormMethod(string fileName)
        {
            IDesignSurfaceExt surface = OneScriptFormsDesigner.ActiveDesignSurface;
            IDesignerHost idh = (IDesignerHost)surface.GetIDesignerHost();
            Form unloadForm = (Form)idh.RootComponent;
            unloadForm.Path = fileName;
            File.WriteAllText(fileName, SaveForm.GetScriptText(fileName), Encoding.UTF8);
        }

        public delegate void GenerateScript(string fileName); // СохранитьСценарий
        public void GenerateScriptMethod(string fileName)
        {
            SaveScript.comps.Clear();

            string scriptText = SaveScript.GetScriptText(fileName);
            if ((bool)Settings.Default["visualSyleForms"])
            {
                Form savedForm = (Form)OneScriptFormsDesigner.DesignerHost.RootComponent;
                string strFind = @"" + savedForm.NameObjectOneScriptForms + @" = Новый ФормыДляОдноСкрипта();";
                string strReplace = @"" + savedForm.NameObjectOneScriptForms + @" = Новый ФормыДляОдноСкрипта();" + Environment.NewLine +
                @"    " + savedForm.NameObjectOneScriptForms + @".ВключитьВизуальныеСтили();";
                scriptText = scriptText.Replace(strFind, strReplace);
            }
            File.WriteAllText(fileName, scriptText, Encoding.UTF8);
        }

        public delegate void RemoveControls(); // УдалитьКонтролы
        public void RemoveControlsMethod()
        {
            ISelectionService iSel = (ISelectionService)OneScriptFormsDesigner.DesignerHost.GetService(typeof(ISelectionService));
            if (iSel != null)
            {
                ComponentCollection ctrlsExisting = OneScriptFormsDesigner.DesignerHost.Container.Components;
                iSel.SetSelectedComponents(ctrlsExisting);
                IpDesignerCore.DeleteOnDesignSurfaceWithoutWarning();
            }
        }

        public delegate string AddControl(string controlName); // ДобавитьКонтрол
        public string AddControlMethod(string controlName)
        {
            IDesignSurfaceExt surface = OneScriptFormsDesigner.ActiveDesignSurface;
            Type type1;
            string type_NameEn;

            if (controlName == "Форма")
            {
                type1 = typeof(osfDesigner.Form);
            }
            else
            {
                type_NameEn = "osfDesigner." + OneScriptFormsDesigner.namesRuEn[controlName];
                type1 = Type.GetType(type_NameEn);
            }
            if (type1 == typeof(osfDesigner.MainMenu))
            {
                ToolboxItem toolMainMenu1 = new ToolboxItem(typeof(System.Windows.Forms.MainMenu));
                Component comp1 = (Component)toolMainMenu1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                return comp1.Site.Name;
            }
            else if (type1 == typeof(osfDesigner.Form))
            {
                Component comp1 = OneScriptFormsDesigner.RootComponent;
                return comp1.Site.Name;
            }
            else if (type1 == typeof(osfDesigner.Timer))
            {
                ToolboxItem toolTimer1 = new ToolboxItem(typeof(osfDesigner.Timer));
                Component comp1 = (Component)toolTimer1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                return comp1.Site.Name;
            }
            else if (type1 == typeof(osfDesigner.ToolTip))
            {
                ToolboxItem toolToolTip1 = new ToolboxItem(typeof(osfDesigner.ToolTip));
                Component comp1 = (Component)toolToolTip1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                return comp1.Site.Name;
            }
            else if (type1 == typeof(osfDesigner.ImageList))
            {
                ToolboxItem toolImageList1 = new ToolboxItem(typeof(System.Windows.Forms.ImageList));
                Component comp1 = (Component)toolImageList1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                return comp1.Site.Name;
            }
            else if (type1 == typeof(osfDesigner.FolderBrowserDialog))
            {
                ToolboxItem toolFolderBrowserDialog1 = new ToolboxItem(typeof(osfDesigner.FolderBrowserDialog));
                Component comp1 = (Component)toolFolderBrowserDialog1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                return comp1.Site.Name;
            }
            else if (type1 == typeof(osfDesigner.ColorDialog))
            {
                ToolboxItem toolColorDialog1 = new ToolboxItem(typeof(osfDesigner.ColorDialog));
                Component comp1 = (Component)toolColorDialog1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                return comp1.Site.Name;
            }
            else if (type1 == typeof(osfDesigner.FontDialog))
            {
                ToolboxItem toolFontDialog1 = new ToolboxItem(typeof(osfDesigner.FontDialog));
                Component comp1 = (Component)toolFontDialog1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                return comp1.Site.Name;
            }
            else if (type1 == typeof(osfDesigner.OpenFileDialog))
            {
                ToolboxItem toolOpenFileDialog1 = new ToolboxItem(typeof(osfDesigner.OpenFileDialog));
                Component comp1 = (Component)toolOpenFileDialog1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                return comp1.Site.Name;
            }
            else if (type1 == typeof(osfDesigner.SaveFileDialog))
            {
                ToolboxItem toolSaveFileDialog1 = new ToolboxItem(typeof(osfDesigner.SaveFileDialog));
                Component comp1 = (Component)toolSaveFileDialog1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                return comp1.Site.Name;
            }
            else if (type1 == typeof(osfDesigner.NotifyIcon))
            {
                ToolboxItem toolNotifyIcon1 = new ToolboxItem(typeof(osfDesigner.NotifyIcon));
                Component comp1 = (Component)toolNotifyIcon1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                return comp1.Site.Name;
            }
            else if (type1 == typeof(osfDesigner.FileSystemWatcher))
            {
                ToolboxItem toolFileSystemWatcher1 = new ToolboxItem(typeof(osfDesigner.FileSystemWatcher));
                Component comp1 = (Component)toolFileSystemWatcher1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                return comp1.Site.Name;
            }
            else if (type1 == typeof(osfDesigner.TabPage))
            {
                // Сначала создадим панель вкладок.
                string NameTabControl = AddControlMethod("ПанельВкладок");
                // Найдем и выделим компонент с именем NameTabControl.
                Component Component1 = (Component)OneScriptFormsDesigner.GetComponentByName(NameTabControl);
                // У панели вкладок уже есть автоматически созданные две вкладки. Добавим ещё одну.
                ToolboxItem toolTabPage1 = new ToolboxItem(typeof(System.Windows.Forms.TabPage));
                Component compTabPage = (Component)toolTabPage1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                // Для comp1 уже создан дублер, получим его.
                osfDesigner.TabPage SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(compTabPage);
                SimilarObj.OriginalObj = (System.Windows.Forms.TabPage)compTabPage;
                OneScriptFormsDesigner.PassProperties(compTabPage, SimilarObj); // Передадим свойства.

                ((System.Windows.Forms.TabControl)Component1).TabPages.Add((System.Windows.Forms.TabPage)compTabPage);

                OneScriptFormsDesigner.PropertyGrid.SelectedObject = SimilarObj;
                SimilarObj.DefaultValues = OneScriptFormsDesigner.GetDefaultValues(SimilarObj, OneScriptFormsDesigner.PropertyGrid);

                return compTabPage.Site.Name;
            }
            else
            {
                IDesignerHost idh = (IDesignerHost)surface.GetIDesignerHost();
                IToolboxUser itu = (IToolboxUser)idh.GetDesigner(idh.RootComponent);
                itu.ToolPicked(new ToolboxItem(type1));
                // Выберем указанный элемент.
                // Элемент управления только что размещен в области конструктора (в DesignSurface), поэтому он остается выбранным.
                // Получим SelectionService.
                ISelectionService selectionService = (ISelectionService)(idh.GetService(typeof(ISelectionService)));
                object objCtrlJustPlaced = selectionService.PrimarySelection;
                // Мы знаем, что это элемент управления (то есть тип Control).
                Control ctrlJustPlaced = (Control)objCtrlJustPlaced;
                ctrlJustPlaced.Location = new Point(50, 50);

                OneScriptFormsDesigner.PropertyGrid.SelectedObject = idh.RootComponent;

                ISelectionService iSel = (ISelectionService)OneScriptFormsDesigner.DesignerHost.GetService(typeof(ISelectionService));
                if (iSel != null)
                {
                    iSel.SetSelectedComponents(new IComponent[] { ctrlJustPlaced });
                    OneScriptFormsDesigner.PropertyGridHost.ReloadTreeView();
                    OneScriptFormsDesigner.PropertyGridHost.ChangeSelectNode((Component)ctrlJustPlaced);
                }
                return ctrlJustPlaced.Site.Name;
            }
        }

        public void TrySetPropertyValue(System.Type componentType, string propertyNameEn, Component Comp1, dynamic propertyValue)
        {
            try
            {
                componentType.GetProperty(propertyNameEn).SetValue(Comp1, propertyValue);
            }
            catch
            {
                componentType.BaseType.GetProperty(propertyNameEn).SetValue(Comp1, propertyValue);
            }
        }

        public pDesignerMainForm()
        {
            if ((bool)Settings.Default["visualSyleForms"])
            {
                Application.EnableVisualStyles();
            }
            ComponentResourceManager resources = new ComponentResourceManager(typeof(pDesignerMainForm));
            propertyGrid1 = OneScriptFormsDesigner.PropertyGrid;
            menuStrip1 = new MenuStrip();
            _file = new ToolStripMenuItem();
            _addForm = new ToolStripMenuItem();
            _useSnapLines = new ToolStripMenuItem();
            _useGrid = new ToolStripMenuItem();
            _useGridWithoutSnapping = new ToolStripMenuItem();
            _useNoGuides = new ToolStripMenuItem();
            _deleteForm = new ToolStripMenuItem();
            _stripSeparator1 = new ToolStripSeparator();
            _loadScript = new ToolStripMenuItem();
            _generateScript = new ToolStripMenuItem();
            _generateScriptAs = new ToolStripMenuItem();
            _stripSeparator2 = new ToolStripSeparator();
            _loadForm = new ToolStripMenuItem();
            _saveForm = new ToolStripMenuItem();
            _saveFormAs = new ToolStripMenuItem();
            _stripSeparator4 = new ToolStripSeparator();
            _exit = new ToolStripMenuItem();

            _edit = new ToolStripMenuItem();
            _unDo = new ToolStripMenuItem();
            _reDo = new ToolStripMenuItem();
            _stripSeparator3 = new ToolStripSeparator();
            _cut = new ToolStripMenuItem();
            _copy = new ToolStripMenuItem();
            _paste = new ToolStripMenuItem();
            _delete = new ToolStripMenuItem();

            _view = new ToolStripMenuItem();
            _form = new ToolStripMenuItem();
            _code = new ToolStripMenuItem();

            _tools = new ToolStripMenuItem();
            _tabOrder = new ToolStripMenuItem();
            _tabOrder1 = _tabOrder;

            _stripSeparator5 = new ToolStripSeparator();
            _run = new ToolStripMenuItem();

            _stripSeparator6 = new ToolStripSeparator();
            _settings = new ToolStripMenuItem();

            _help = new ToolStripMenuItem();
            _about = new ToolStripMenuItem();
            pnl4Toolbox = new System.Windows.Forms.Panel();
            listBox1 = new System.Windows.Forms.ListBox();
            pnl4pDesigner = new System.Windows.Forms.Panel();
            pnl4splitter = new System.Windows.Forms.Splitter();
            menuStrip1.SuspendLayout();
            pnl4Toolbox.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] {
            _file,
            _edit,
            _view,
            _tools,
            _help});
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Padding = new Padding(8, 2, 0, 2);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // _file
            // 
            _file.DropDownItems.AddRange(new ToolStripItem[] {
            _addForm,
            _deleteForm,
            _stripSeparator1,
            _loadScript,
            _generateScript,
            _generateScriptAs,
            _stripSeparator2,
            _loadForm,
            _saveForm,
            _saveFormAs,
            _stripSeparator4,
            _exit});
            _file.Name = "_file";
            _file.Size = new Size(54, 24);
            _file.Text = "Файл";
            // 
            // _addForm
            // 
            _addForm.DropDownItems.AddRange(new ToolStripItem[] {
            _useSnapLines,
            _useGrid,
            _useGridWithoutSnapping,
            _useNoGuides});
            string str_addForm = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABR0lEQVR42u3avQ/BQBgGcBcEia2LSESE1WK3+p/tdoNuxcBgsxJKPTc0/ZBe2lzPfXjf5Fxocvf8KqmrK2s4Ukx3AIIIIJHuMDIG/tJBu+tOI1ldDkl/Exx0052qZPU4IH7DIQHaTHcqyQo45InW0p1EskIOCdGaupNI1osghhVBTKsviG1Llvg3kCCmFEHqTHDOf4bJR1ZBouJbhgOrtu4zFnJCgDFBSkBk7x4z4xKEIIrqHyBXBPCMgUTq/lp6s+xKXR0EI6/RrRRBePkIOf8FZI9uqhCyQcilC5AtQi5cgBxZMr5SyAXdQCEkfYlWftXy0Q0LDrfR+gXHHmg7wdAeAk6yU1WDlL2cljoRNi1RhCCbIELYPyxRCEIQgggwrPr8+iH1nY8Ekt56sxUS5nd1bcJkcju1Pe3MAwNxWf8IhxNFENPqA2/lwIZlxdeeAAAAAElFTkSuQmCC";
            _addForm.Image = OneScriptFormsDesigner.Base64ToImage(str_addForm);
            _addForm.Name = "_addForm";
            _addForm.Size = new Size(221, 26);
            _addForm.Text = "Добавить Форму";
            // 
            // _useSnapLines
            // 
            string str_useSnapLines = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAAd0lEQVR42u3YSw7AIAgFwHr/Q7er7uuvoM7bC0xiSLRcm6RED5ARckfOCgKyIKS3dlU9EBAQkO7GowMCsjokNCCVea9dS79PZ0FAQEBAQEBAQEDSQKbnCMjsp+vIWUFAMkD+XgzDvkxBQEAOhUQHBCQbZKlsA3kAIttEM9KSwFkAAAAASUVORK5CYII=";
            _useSnapLines.Image = OneScriptFormsDesigner.Base64ToImage(str_useSnapLines);
            _useSnapLines.Text = "Использовать линии привязки";
            _useSnapLines.Click += _useSnapLines_Click;
            // 
            // _useGrid
            // 
            string str_useGrid = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAAf0lEQVR42u3UsRGAMBDEQNwX5dMXkDvgM3Q30oyzC7zJr6Ok9fcHhHxA7iEUt0uFXO87GyDbrhYSmxBaQmjVQnBndboTQtvVQmITQksIrVoI7qxOd0Jou1pIbEJoCaFVC8Gd1elOCG1XC4lNCC0htGohuLM63Qmh7WohsQmh9QCKkWUzuKisgQAAAABJRU5ErkJggg==";
            _useGrid.Image = OneScriptFormsDesigner.Base64ToImage(str_useGrid);
            _useGrid.Text = "Использовать сетку";
            _useGrid.Click += _useGrid_Click;
            // 
            // _useGridWithoutSnapping
            // 
            _useGridWithoutSnapping.Text = "Использовать сетку без привязки";
            _useGridWithoutSnapping.Click += _useGridWithoutSnapping_Click;
            // 
            // _useNoGuides
            // 
            _useNoGuides.Name = "_useNoGuides";
            _useNoGuides.Size = new Size(316, 26);
            _useNoGuides.Text = "Не использовать ориентиры";
            _useNoGuides.Click += _useNoGuides_Click;
            // 
            // _deleteForm
            // 
            string str_deleteForm = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABLklEQVR42u2asQrCMBCGDSoquLmIi4iuLu6uvrO7u4PdKg4uPoKi1XoHVlultTVc7xrvg2soheT/KCVpU1NzBMMdQEUyRELuMDYOeGhBnbjTWNJGkfidQKEjd6qcdFAgOkERH2rCncoSH0UuUA3uJJYEKBJA1bmTWHJVEWGoiDQ+RKq2ZInmQBWRgopIQ0Wk8b8i3G+P33KpSJHBKPnfZ0QqtCIh3TN1M8mVOp0I9LyEZkEkgngQclqGyBaaMaHICkLOXRBZQ8iZCyI78+qfVOQATZ9QZA8hh+Qij949aAYpl5tQ3ZRrZ6hNRtc9CDhKDlVMxJmZPe+8ULYM6aKxTBldokhDRaShItJIiMS33qoqErzv6lZJJpHbqe1pZ34YiOD+dvUrz184nEBFpHEHIdCPhqDjZfIAAAAASUVORK5CYII=";
            _deleteForm.Image = OneScriptFormsDesigner.Base64ToImage(str_deleteForm);
            _deleteForm.Name = "_deleteForm";
            _deleteForm.Size = new Size(221, 26);
            _deleteForm.Text = "Удалить Форму";
            _deleteForm.Click += _deleteForm_Click;
            // 
            // _stripSeparator1
            // 
            _stripSeparator1.Name = "_stripSeparator1";
            // 
            // _loadScript
            // 
            string str_loadScript = "AAABAAEAMjIAAAEAIADIKAAAFgAAACgAAAAyAAAAZгEAIгAAoCgшшшшшшшшшшшшшццццEзкзкзкзкнкнкуAAAAQццгAPкзкзкзкзкнкнкуццAAзкзкзкзкзкццгAD / нкщщееrLZN / wAAAPкнкццAPкAAAAйщщееrLZN / wAAAPкн8ццA / wAAAPкщщееенкуццD / н + stk3 / щщееенкццAPкAAAAйщщееrLZN / 6y2Tfкн8ццA / wAAAPкщщеееrLZN / wAAAPкн8цгAAD / н + stk3 / щщееенкуцгAAPкн + stk3 / щщееенкцAAAAнкAAAAйщщееrLZN / 6y2Tfкн8цгAAD / нкAAAAйщщееrLZN / wAAAPкуцгAAPкнкщrLZN / 7u6Wv + stk3 / щеенкгQцAA / wAAAPкнкщщееrLZN / 6y2TfкнкцAAD / нкн + stk3 / щщееrLZN / 6y2Tfкн8цAAPкнкAAAAйщщееrLZN / wAAAPкуцAA / wAAAPкнкщщееенкцAAD / нкн + stk3 / щщееrLZN / 6y2Tfкн8цAAPкугAAAD / н + stk3 / щщееrLZN / wAAAPкн8гнкгAAAAPкAAAAйееееuLNRйщееrLZN / wAAAPкугAAAD / н8гнкееееrLZN / 6y2Tf + 7ulr / щееенкгAAAAPкугAAAD / н + stk3 / ееееrLZN / 7izUf + stk3 / щееrLZN / 6y2Tfкн8гнкгAAAAPкн + stk3 / ееееu7paйщееrLZN / wAAAPкугAAAD / н8цAAPкAAAAйеееrLZN / 6y2Tf + 7ulr / щееенкнкуцAA / wAAAPкщщееенкнкцAAD / н + stk3 / щщееrLZN / 6y2Tfкнкн8цAAPкн + stk3 / щщееrLZN / wAAAPкнкуцгAAPкAAAAйщщеенкнкцAAAAнкAAAAйщщеrLZN / wAAAPкнкн8цгAAD / зкзкзкзкзкуцгAAPкзкзкзкзкзкццAPкзкзкзкзкнкггYAAAD / н8шшшццAAPкн8ццгAPкушшшццAA / wAAAPкуццAAнкшшшццAAD / нкццгAD / н8шшшцгнкуццггAPкушцD / зкзкзкццц / wAAAPкшAAAAзкзкзкуцгAAAYцгAAD / н8шAAPкзкзкз8цццггPкн8ццццAAAD / нкAAAACgшшццццAAзкз8шшшGзкнкнкшшшцзкнкушшшшшшшшшшшшшшшшццAA////////wAD////////AAP///////8гAAAAHwгAAAAfгAAAAB8гAAAADwгAAAAPгг8гAAAADwгAAAAPггcгAAAABwгAAAAHггcгAAAABwгAAAADггMггwгAAAADггMггwAAQгABAABгAAEAAEгAAQAAQгABAABгAAEAAGггYгAAAABgгAAAAGггcгAAAABwгAAAAHггcгAAAAB4гAIAAH/////+HwAAf/////4fAAB//////h8AAH/////8PwAAf/wAAAA/AAB/+AAAAHcAAH/wуAAP+D/////гf////8AAAAD/////wADAAf/////AAP///////8AA////////wAD////////AAA==";
            str_loadScript = str_loadScript.Replace("з", "нкнкн");
            str_loadScript = str_loadScript.Replace("щ", "еееее");
            str_loadScript = str_loadScript.Replace("ш", "ццццц");
            str_loadScript = str_loadScript.Replace("г", "AAAAA");
            str_loadScript = str_loadScript.Replace("н", "уAAAP");
            str_loadScript = str_loadScript.Replace("е", "rLZNй");
            str_loadScript = str_loadScript.Replace("к", "8AAAD /");
            str_loadScript = str_loadScript.Replace("у", "AAAA / w");
            str_loadScript = str_loadScript.Replace("ц", "AAAAAAAAAAAA");
            str_loadScript = str_loadScript.Replace("й", "/ 6y2Tf + stk3 /");
            _loadScript.Image = OneScriptFormsDesigner.Base64ToImage(str_loadScript);
            _loadScript.Name = "_loadScript";
            _loadScript.Size = new Size(221, 26);
            _loadScript.Text = "Открыть сценарий";
            _loadScript.Click += _loadScript_Click;
            // 
            // _generateScript
            // 
            string str_generateScript = "AAABAAEAMjIAAAEAIADIKAAAFgAAACgAAAAyAAAAZгEAIгAAoCgцццццццццццццццццццццццццццццццццццццццццццццццццццццццццццццццццццццццгAAD / нунунунунунунунунунунунуAAAA / wццццгAAAAGнунунунунунунунунунунунунуAAAA / wAAAAYцццггPунунунунунунунунунунунунунуAAAA / wцццAAAAD / нуеrLZN / 6y2TfуAAAAйееееееrLZN / 6y2TfуAAAAйенуAAAA / wццггAPуAAAAйеrLZNкуеееееееrLZNкуеенуцццкуrLZN / 6y2TfуAAAAйн + stk3 / ееееееен + stk3 / rLZNкуrLZN / 6y2Tfун8ццггAD / н + stk3 / rLZNкуrLZN / 6y2TfуAAAAйееееееrLZN / 6y2TfуAAAAйн + stk3 / rLZNкуAAAA / wццггAPуAAAAйеrLZNкуеееееееrLZNкуеенуцццкуеен + stk3 / ееееееен + stk3 / еrLZN / 6y2Tfун8ццггAD / н + stk3 / еrLZN / 6y2TfуAAAAйееееееrLZN / 6y2TfуAAAAйеrLZNкуAAAA / wццггAPуAAAAйеrLZNкуеееееееrLZNкуеенуцццкуеен + stk3 / ееееееен + stk3 / еrLZN / 6y2Tfун8ццггAD / н + stk3 / еrLZN / 6y2TfуAAAAйееееееrLZN / 6y2TfуAAAAйеrLZNкуAAAA / wццггAPуAAAAйеrLZNкуеееееееrLZNкуеенуцццкуеен + stk3 / ееееееен + stk3 / еrLZN / 6y2Tfун8ццггAD / н + stk3 / еrLZN / 6y2TfуAAAAйееееееrLZN / 6y2TfуAAAAйеrLZNкуAAAA / wццггAPуAAAAйеrLZNкуеееееееrLZNкуеенуцццкуеен + stk3 / ееееееен + stk3 / еrLZN / 6y2Tfун8ццггAD / н + stk3 / еrLZN / 6y2Tfун + stk3 / ееееееrLZNкуAAAAйеrLZNкуAAAA / wццггAPуAAAAйеrLZN / 6y2Tfунунунунунунунун + stk3 / еенуцццкуееrLZN / 6y2TfунунунунунунунуееrLZN / 6y2Tfун8ццггAD / н + stk3 / ееееееееееееrLZNкуAAAA / wццггAPуAAAAйеееееееееееенуцццкуееееееееееееrLZN / 6y2Tfун8ццггAD / н + stk3 / ееееееееееееrLZNкуAAAA / wццггAPуAAAAйеееееееееееенуцццкуееrLZN / 6y2Tfунунунунунунун + stk3 / ееrLZN / 6y2Tfун8ццггAD / н + stk3 / еенунунунунунунун + stk3 / ееrLZNкуAAAA / wццггAPуAAAAйеrLZNкуAAAA / 6щзAKщзAKщзAKщзAKщзAKщзAKy2TQAAAAD / н + stk3 / еенуцццкуеен + зAKщзAKщзAKщзAKщзAKщзAKщзAKy2TQAAAAD / AAAAйеrLZN / 6y2Tfун8ццггAD / н + stk3 / еrLZN / 6y2TfуAAAA / 6щзAKщзAKщзAKщзAKщшнун + згPуееrLZNкуAAAA / wццггAPуAAAAйеrLZNкуrLZNAKщзAKщзAKщзAKщзAKy2TQAAAAD / нуAAAA / 6щшн + stk3 / еенуцццкуеен + зAKщзAKщзAKщзAKщзгPуrLZNгPуrLZNAKy2TQAAAAD / AAAAйеrLZN / 6y2Tfун8ццггAD / н + stk3 / еrLZN / 6y2TfуAAAA / 6щзAKщзAKщзAKщзAKщшн + шн + згPуеенуAAAA / wццггAPуAAAAйеrLZNкуrLZNAKщзAKщзAKщзAKщзAKy2TQAAAAD / AAAA / 6y2TQAAAAD / AAAA / 6щшн + stk3 / еrLZNкун8цццAнуеен + зAKщзAKщзAKщзAKщзгPуrLZNгPуrLZNAKy2TQAAAAD / AAAAйrLZN / 6y2TfунуAAAABwцццAAAAD / н + stk3 / еrLZN / 6y2TfуAAAA / 6щзAKщstk0гAKщзAKщзAKщшн + шн + згPуенуAAAA / wAAAAcцццггPуAAAAйеrLZNкуrLZNAKщзAKщзAKщзAKщзAKy2TQAAAAD / нуAAAA / 6щшн + stk3 / rLZNкун8AAAAHццццнуеен + зAKщзAKщзAKщзAKщзгPунуrLZNAKy2TQAAAAD / AAAA / 6y2TfунуAAAABgццццгAAAD / н + stk3 / еrLZN / 6y2TfуAAAA / 6щзAKщзAKщзAKщзAKщзAKщзAKщзгPунуAAAA / wAAAAcцццццAAPун + stk3 / еrLZNкуrLZNAKщзAKщзAKщзAKщзAKщзAKщзAKщшнун8AAAAGццццццAPунунунунунунунунунунунуAAAABgццццццгABgAAAPунунунунунунунунунунуAAAA / wAAAAYцццццццггPунунунунунунунунунун8цццццццццццццццццццццццццццггACзцццццццццццццццццццццгKщstk0цццццццццццццццццццццг////////wAD////////AAP///////8AA/гAPwADwгAPAAPгAA8AA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAPAAOгAA8AA4AAQAAAHwADgгA/AAOгAH8AA4гA/wADgгH/AAPгA/8AA8гH/wAD8AAAAB//AAP///////8AAP///////wAA////////AAA==";
            str_generateScript = str_generateScript.Replace("з", "шrLZN");
            str_generateScript = str_generateScript.Replace("щ", "y2TQC");
            str_generateScript = str_generateScript.Replace("ш", "stk0A");
            str_generateScript = str_generateScript.Replace("г", "AAAAA");
            str_generateScript = str_generateScript.Replace("н", "AAAAк");
            str_generateScript = str_generateScript.Replace("е", "rLZNй");
            str_generateScript = str_generateScript.Replace("к", "/wAAAP");
            str_generateScript = str_generateScript.Replace("у", "8AAAD/");
            str_generateScript = str_generateScript.Replace("ц", "AAAAAAAAAAAA");
            str_generateScript = str_generateScript.Replace("й", "/6y2Tf+stk3/");
            _generateScript.Image = OneScriptFormsDesigner.Base64ToImage(str_generateScript);
            _generateScript.Name = "_generateScript";
            _generateScript.Size = new Size(221, 26);
            _generateScript.Text = "Сохранить сценарий";
            _generateScript.Click += _generateScript_Click;
            _generateScript.ShortcutKeys = Keys.Control | Keys.S;
            _generateScript.ShowShortcutKeys = true;
            // 
            // _generateScriptAs
            // 
            string str_generateScriptAs = "AAABAAEAMjIAAAEAIADIKAAAFgAAACgAAAAyAAAAZгEAIгAAoCgцццццццццццццццццццццццццццццццццццццццццццццццццццццццццццццццццццццццгAAD / нунунунунунунунунунунунуAAAA / wццццгAAAAGнунунунунунунунунунунунунуAAAA / wAAAAYцццггPунунунунунунунунунунунунунуAAAA / wцццAAAAD / нуеrLZN / 6y2TfуAAAAйееееееrLZN / 6y2TfуAAAAйенуAAAA / wццггAPуAAAAйеrLZNкуеееееееrLZNкуеенуцццкуrLZN / 6y2TfуAAAAйн + stk3 / ееееееен + stk3 / rLZNкуrLZN / 6y2Tfун8ццггAD / н + stk3 / rLZNкуrLZN / 6y2TfуAAAAйееееееrLZN / 6y2TfуAAAAйн + stk3 / rLZNкуAAAA / wццггAPуAAAAйеrLZNкуеееееееrLZNкуеенуцццкуеен + stk3 / ееееееен + stk3 / еrLZN / 6y2Tfун8ццггAD / н + stk3 / еrLZN / 6y2TfуAAAAйееееееrLZN / 6y2TfуAAAAйеrLZNкуAAAA / wццггAPуAAAAйеrLZNкуеееееееrLZNкуеенуцццкуеен + stk3 / ееееееен + stk3 / еrLZN / 6y2Tfун8ццггAD / н + stk3 / еrLZN / 6y2TfуAAAAйееееееrLZN / 6y2TfуAAAAйеrLZNкуAAAA / wццггAPуAAAAйеrLZNкуеееееееrLZNкуеенуцццкуеен + stk3 / ееееееен + stk3 / еrLZN / 6y2Tfун8ццггAD / н + stk3 / еrLZN / 6y2TfуAAAAйееееееrLZN / 6y2TfуAAAAйеrLZNкуAAAA / wццггAPуAAAAйеrLZNкуеееееееrLZNкуеенуцццкуеен + stk3 / ееееееен + stk3 / еrLZN / 6y2Tfун8ццггAD / н + stk3 / еrLZN / 6y2Tfун + stk3 / ееееееrLZNкуAAAAйеrLZNкуAAAA / wццггAPуAAAAйеrLZN / 6y2Tfунунунунунунунун + stk3 / еенуцццкуееrLZN / 6y2TfунунунунунунунуееrLZN / 6y2Tfун8ццггAD / н + stk3 / ееееееееееееrLZNкуAAAA / wццггAPуAAAAйеееееееееееенуцццкуееееееееееееrLZN / 6y2Tfун8ццггAD / н + stk3 / ееееееееееееrLZNкуAAAA / wццггAPуAAAAйеееееееееееенуцццкуееrLZN / 6y2Tfунунунунунунун + stk3 / ееrLZN / 6y2Tfун8ццггAD / н + stk3 / еенунунунунунунун + stk3 / ееrLZNкуAAAA / wццггAPуAAAAйеrLZNкуAAAA / 6щзAKщзAKщзAKщзAKщзAKщзAKy2TQAAAAD / н + stk3 / еенуцццкуеен + зAKщзAKщзAKщзAKщзAKщзAKщзAKy2TQAAAAD / AAAAйеrLZN / 6y2Tfун8ццггAD / н + stk3 / еrLZN / 6y2TfуAAAA / 6щзAKщзAKщзAKщзAKщшнун + згPуееrLZNкуAAAA / wццггAPуAAAAйеrLZNкуrLZNAKщзAKщзAKщзAKщзAKy2TQAAAAD / нуAAAA / 6щшн + stk3 / еенуцццкуеен + зAKщзAKщзAKщзAKщзгPуrLZNгPуrLZNAKy2TQAAAAD / AAAAйеrLZN / 6y2Tfун8ццггAD / н + stk3 / еrLZN / 6y2TfуAAAA / 6щзAKщзAKщзAKщзAKщшн + шн + згPуеенуAAAA / wццггAPуAAAAйеrLZNкуrLZNAKщзAKщзAKщзAKщзAKy2TQAAAAD / AAAA / 6y2TQAAAAD / AAAA / 6щшн + stk3 / еrLZNкун8цццAнуеен + зAKщзAKщзAKщзAKщзгPуrLZNгPуrLZNAKy2TQAAAAD / AAAAйrLZN / 6y2TfунуAAAABwцццAAAAD / н + stk3 / еrLZN / 6y2TfуAAAA / 6щзAKщstk0гAKщзAKщзAKщшн + шн + згPуенуAAAA / wAAAAcцццггPуAAAAйеrLZNкуrLZNAKщзAKщзAKщзAKщзAKy2TQAAAAD / нуAAAA / 6щшн + stk3 / rLZNкун8AAAAHццццнуеен + зAKщзAKщзAKщзAKщзгPунуrLZNAKy2TQAAAAD / AAAA / 6y2TfунуAAAABgццццгAAAD / н + stk3 / еrLZN / 6y2TfуAAAA / 6щзAKщзAKщзAKщзAKщзAKщзAKщзгPунуAAAA / wAAAAcцццццAAPун + stk3 / еrLZNкуrLZNAKщзAKщзAKщзAKщзAKщзAKщзAKщшнун8AAAAGццццццAPунунунунунунунунунунунуAAAABgццццццгABgAAAPунунунунунунунунунунуAAAA / wAAAAYцццццццггPунунунунунунунунунун8цццццццццццццццццццццццццццггACзцццццццццццццццццццццгKщstk0цццццццццццццццццццццг////////wAD////////AAP///////8AA/гAPwADwгAPAAPгAA8AA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAHAAOгAAcAA4гABwADgгAPAAOгAA8AA4AAQAAAHwADgгA/AAOгAH8AA4гA/wADgгH/AAPгA/8AA8гH/wAD8AAAAB//AAP///////8AAP///////wAA////////AAA==";
            str_generateScriptAs = str_generateScriptAs.Replace("з", "шrLZN");
            str_generateScriptAs = str_generateScriptAs.Replace("щ", "y2TQC");
            str_generateScriptAs = str_generateScriptAs.Replace("ш", "stk0A");
            str_generateScriptAs = str_generateScriptAs.Replace("г", "AAAAA");
            str_generateScriptAs = str_generateScriptAs.Replace("н", "AAAAк");
            str_generateScriptAs = str_generateScriptAs.Replace("е", "rLZNй");
            str_generateScriptAs = str_generateScriptAs.Replace("к", "/wAAAP");
            str_generateScriptAs = str_generateScriptAs.Replace("у", "8AAAD/");
            str_generateScriptAs = str_generateScriptAs.Replace("ц", "AAAAAAAAAAAA");
            str_generateScriptAs = str_generateScriptAs.Replace("й", "/6y2Tf+stk3/");
            _generateScriptAs.Image = OneScriptFormsDesigner.Base64ToImage(str_generateScriptAs);
            _generateScriptAs.Name = "_generateScriptAs";
            _generateScriptAs.Size = new Size(221, 26);
            _generateScriptAs.Text = "Сохранить сценарий как";
            _generateScriptAs.Click += _generateScriptAs_Click;
            // 
            // _stripSeparator2
            // 
            _stripSeparator2.Name = "_stripSeparator2";
            // 
            // _loadForm
            // 
            string str_loadForm = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAA/klEQVR42u3Z0QqDMAwFUDO2vez/f3UwN+iqOKE2qdUVe2NzoS/6kmMMVEvdSUK1CzBIAuI0P4ihkLtfr0IPoypkTyfgMBHEZRRFMf7h1xMGkoNIYI6otTwEADPMdl8EAoAJLv4FOaBix19WBNmCgYdMRRpEailixt0JC1GECDRtQdDmZzk3WRA0BIeJIBq6sQuCijAIWlaHnRTORzc1wIX38bthEMScAsLthsXfQagIBjLvRgxiEIPIn70ShFC/qhaQy69+FoLaDQYS/GNQsbdqE6IMYZDqkV4rgwAgGoGgYoRTsTRESdLHCorCQq5+vWtXtiE3vz6rMq0xCFq+0/uOMfSQbkgAAAAASUVORK5CYII=";
            _loadForm.Image = OneScriptFormsDesigner.Base64ToImage(str_loadForm);
            _loadForm.Name = "_loadForm";
            _loadForm.Text = "Открыть форму";
            _loadForm.Click += _loadForm_Click;
            // 
            // _saveForm
            // 
            string str_saveForm = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABBUlEQVR42u3awQ6CMBAEUPai/v/f6qXCgYTAbruF0p3qTMIBgW1fS4RYZfqRSHQHCKmAJNB+uS94zNs7ELHmOW+fK5DImTiN2UPSbkeUC5J17GpEH0QXxoRYHQ2ALHlNhdu9O0Tr7FprW1s5LzszsBDjXHNmwiDbUbcgNRh4iBczBMTAiLmDDClhhoIomHjIrlFCwiAeZFdIa4y3bjOIUqxpSoPTFHIXxjPDzSFRIQQthKBliG+tE+3iPkcq28Z7sntrD/Gu5alPCCGEEEIIIYQQohTuDSksNfwhpNTQ3RDruBtiFYuE1Pwaf8AAJ7s+sgTlDwO5HBZGc7cH6syI+8MRQwhavjI5HUJUEs5VAAAAAElFTkSuQmCC";
            _saveForm.Image = OneScriptFormsDesigner.Base64ToImage(str_saveForm);
            _saveForm.Name = "_saveForm";
            _saveForm.Text = "Сохранить форму";
            _saveForm.Click += _saveForm_Click;
            // 
            // _saveFormAs
            // 
            _saveFormAs.Image = OneScriptFormsDesigner.Base64ToImage(str_saveForm);
            _saveFormAs.Name = "_saveFormAs";
            _saveFormAs.Text = "Сохранить форму как";
            _saveFormAs.Click += _saveFormAs_Click;
            // 
            // _stripSeparator4
            // 
            _stripSeparator4.Name = "_stripSeparator4";
            // 
            // _exit
            // 
            _exit.Name = "_exit";
            _exit.Text = "Выход";
            _exit.Click += _exit_Click;
            // 
            // _edit
            // 
            _edit.DropDownItems.AddRange(new ToolStripItem[] {
            _unDo,
            _reDo,
            _stripSeparator3,
            _cut,
            _copy,
            _paste,
            _delete});
            _edit.Name = "_edit";
            _edit.Size = new Size(69, 24);
            _edit.Text = "Правка";
            // 
            // _unDo
            // 
            string str_unDo = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAA2klEQVR42u3YSw6AIAwEULz/oTGamBAjyqdlZky7YSfzRH5uOWnXFoAABMDoQSnlfDaCgCP80UoCrvCSgDK8HOAeXgrwFF4GUAsvAXgLTw/4Ck8NaAk/WrPoT4BneAvIK2BV+BlIFYAIP4KgBPQg6D6hXgTNJHYDsCNgG1nrSzEDtHTavQQaIOCHudmRpThOz4wsxYUGAnjqWA5w71wSUAaQmcS1IBLLqFVBNjLvsL8DmB3mAjAAMLnQoAAmV0oUwOxSvxrg8ltlBcD9xxZ7BQBdAUBXANAlD9gBBDWIAQ4VHAYAAAAASUVORK5CYII=";
            _unDo.Image = OneScriptFormsDesigner.Base64ToImage(str_unDo);
            _unDo.Name = "_unDo";
            _unDo.ShortcutKeys = Keys.Control | Keys.Z;
            _unDo.Size = new Size(212, 26);
            _unDo.Text = "Отменить";
            _unDo.Click += _unDo_Click;
            // 
            // _reDo
            // 
            string str_reDo = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAA10lEQVR42u3YSw7EIAwDUHr/QzOaXqD52IkjJRt21A+JEnjumV3PAhawgIaPnnPvOwwG/EcEohWAQLQDsggJQAYhA4gipAARhBzAi5AEeBBpgCVMtCyIMIAZ3INwA6qCWxEuQEf4L4QZ0Bk+DVANPwKQ3sTK4aEA1+lZdZAxwlvmhbUSyJWyzgtt5pArZZkX3k5XAigXmioA7UpZAaBe6tmbmP6swvyNlj1ssRCoorYSFShqM7cAFCCKkAJEEHIAL0IS4IFIAyyQEYDuWkB3LaC7FtBd4wE/1ESIAWn6qDIAAAAASUVORK5CYII=";
            _reDo.Image = OneScriptFormsDesigner.Base64ToImage(str_reDo);
            _reDo.Name = "_reDo";
            _reDo.ShortcutKeys = Keys.Control | Keys.Y;
            _reDo.Size = new Size(212, 26);
            _reDo.Text = "Вернуть";
            _reDo.Click += _reDo_Click;
            // 
            // _stripSeparator3
            // 
            _stripSeparator3.Name = "_stripSeparator3";
            // 
            // _cut
            // 
            string str_cut = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABNUlEQVR42u2Y0Q7CIAxF4f8/ekYjZpmU3sItlIS+OTI9BxDa5ivtHfkIHIEjsHc0BXJK1eHrMxQjqgISeESRPwEUPooEJHCHfI6HF6gBektokwgLiC/9hnkCyNatTmYEgZFDY+kWshwY8BbSACPBiwKWH7AKsMBVAQ8JNrwqwBLxADcJWECeAJ7wZgEEqkDMSkm60+nWbTkznxqqB2qXnveWoQoU4Br8+9mMeoJWkQmr8f3IB6cKSP+H8vy+QvTMlbGFWuPS9goh0APPlqAeoyg8U8QsYLmNZzQHYIHuislZQhVgACxJ5jxmzuU7Zze23AsarWRklJRMiS2KelNNDLZVaI2t0dVYLmARcekLUfMaoNIbFgjf3NVmIhK8KIBIRIBvCrREosBDAtHjCKyOI7A6thd4ARhzzAEzNxSrAAAAAElFTkSuQmCC";
            _cut.Image = OneScriptFormsDesigner.Base64ToImage(str_cut);
            _cut.ImageTransparentColor = Color.Magenta;
            _cut.Name = "_cut";
            _cut.ShortcutKeys = Keys.Control | Keys.X;
            _cut.Size = new Size(212, 26);
            _cut.Text = "Вырезать";
            _cut.Click += OnMenuClick;
            // 
            // _copy
            // 
            string str_copy = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABOElEQVR42u3WrQ7CMBAH8DUIBAaD4AnAYtCEZ8DxEih4BhwejeMZCBoECRiQ6CUEMwghY7AlJCPpPq7tbddwfzOxNe2vubtMOJZHlH0ABqS8CwrYwwoAKiIT0F94oM1Xw1q0buf6otOoxC8BBYEGOHsvcbkFDjYCFRA+sRHoAGxEIQBMRGEALIQRwPfQBgIGUQOAEUYBs80DtPmoW43WHa++aNfVSosEIOyX+zNwVBBkAOFTBQECZNW6LkAFQQ4ARZAqIRUEWYAEoQ84rLfSD915TwuQMwww3sRAJC3A52Cy9bgASAkByyPxIhiAOUbzhAGqTTxdnnIfcjJo0QPIpkxW0KdQUmQlBGnitNJjgAbAaY73eQ0/ifcEAyAhOUZtBkBC8lfivwGmwgAG2ARADgNIhgFlx3rAG9GomUA3I+5MAAAAAElFTkSuQmCC";
            _copy.Image = OneScriptFormsDesigner.Base64ToImage(str_copy);
            _copy.ImageTransparentColor = Color.Magenta;
            _copy.Name = "_copy";
            _copy.ShortcutKeys = Keys.Control | Keys.C;
            _copy.Size = new Size(212, 26);
            _copy.Text = "Копировать";
            _copy.Click += OnMenuClick;
            // 
            // _paste
            // 
            string str_paste = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABIklEQVR42u3awQnCMBQG4OTkFL07gY6Rg6eCriAFcQoRiiso9CbFKcQNvIpLeHoWaaGWtEmapH2h77/12b68DxPxUM4CDx97ADSAY5ZHu1i86rX0fJsnG/FED5ANX+V0uUbb9eqNGsAYQK0lb6+hBzQHbasPC4DOy/92LQCtpXshVQ+BVqmBOGT5ch+Le4+ljRFdD4BR2Tjc+AMrAAD87i02iRNBsbnKfrzZzz2gGt4HQILwDeCs+3BqLcuh1mJQgI8QgAAEGBkg+S334vQKSB8fb99SspgBAQhAAAJMCFAuaBVJPwJoA1yHAAQIDWB6iFV4AkxuCxGAAJaA4P9KBA9wHWuAChEEQBWMACMEVoA2AjNAJyjPwLQAvoZXzRr82ypful5dQCOEOe0AAAAASUVORK5CYII=";
            _paste.Image = OneScriptFormsDesigner.Base64ToImage(str_paste);
            _paste.ImageTransparentColor = Color.Magenta;
            _paste.Name = "_paste";
            _paste.ShortcutKeys = Keys.Control | Keys.V;
            _paste.Size = new Size(212, 26);
            _paste.Text = "Вставить";
            _paste.Click += OnMenuClick;
            // 
            // _delete
            // 
            string str_delete = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAABmJLR0QA/wD/AP+gvaeTAAACJUlEQVRoge2YvW/TQBiHn7sjY9UFiSIYOjSiVGyt5HYAUkLyR0NVRwgJkBiRGGAEqaIsSQZKFfsYiFFUXXJ3vtdIiHtG34d/j+9svzZkMplMJhPIfFgcTEdFv6v5p4OTvfmwOIgZo0M7zsaH+wp7Zmr7av78+GF8vM1MR0XfmGqisOV8fPQodJwK6TQbH+7rypwDd5eHLqzSz7bO3n5sE/Ym01HRN7UtgXvLQ9+sqYdbL95/8I31CjjCN4hIOMI3BEl4t1DvuncF/HQ07Shbn6dspw3hAWrqW5VvjqAt9GNwvFuZugR2Hc2tVsITPnjOIAGQlZAKDxECICMhGR4iBSBNQjo8tBCAdhJdhIeWAhAn0VV4SBCAMIla2UVX4SFRADwSikssNXDHMVTkRZgsAN6VcCFWiogIQJSEaB0lJgC/JRamfq3c+x0L31H6iVR4iCinQ7ju2Z62mHXtoldriZhA86i0ip0N3W6nFoA3Ebkonue8C7H7IHkFNoa3XAIXjmHJpXhDkoD3Dav106oyj4EvjnYRidZbKKY8mA5O9oypSuC+r28srQTa1DZdSUQLpBRmXUhECUhUlUuJScocqwTfxFIl8fbkzedKq1Pgq6M5+sYOWoEu6vm/9lHf5ceIxNzeLaQWlV7TL/ltuv3y3acN20mjF2vrqj/5Qk40Oz16oLUu+Rd/LTasSCjJ8A0rEr3Q8NF0/nt9VPRjf69nMplM5v/mF/i6x8b172ZWAAAAAElFTkSuQmCC";
            _delete.Image = OneScriptFormsDesigner.Base64ToImage(str_delete);
            _delete.Name = "_delete";
            _delete.ShortcutKeys = Keys.Delete;
            _delete.Size = new Size(212, 26);
            _delete.Text = "Удалить";
            _delete.Click += OnMenuClick;
            // 
            // _view
            // 
            _view.DropDownItems.AddRange(new ToolStripItem[] {
            _form,
            _code});
            _view.Name = "_view";
            _view.Size = new Size(50, 24);
            _view.Text = "Вид";
            // 
            // _form
            // 
            string str_form = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAA1klEQVR42u3a4QqCMBQF4EaB9R69/wv5IAktavfHYippebVzdz0HJvhjcj5EpsxwcJKALkDIBOSJLqMxyKFJo0O3UeYskPJOCOiGbvVlLgLIJwJp07iiWynTCuSexgndRJkokJjGEd1EmQchxkKItYwgtb2y5DWQECshxFr2C0F/q8z1IoQQQlaCWA0hP119iwwKbgf5x8NUlCSEEEJQkDlMWDjvw/z9ruyEEELI9ASrIcRafELKrbdaIXG4q1sTptfb1fa0mx8GctCL3tK8f+FwEUKs5QXoOIWG//RH0wAAAABJRU5ErkJggg==";
            _form.Image = OneScriptFormsDesigner.Base64ToImage(str_form);
            _form.Name = "_form";
            _form.Size = new Size(50, 24);
            _form.Text = "Форма";
            _form.Click += _form_Click;
            _form.Enabled = false;
            _form.CheckState = System.Windows.Forms.CheckState.Checked;
            // 
            // _code
            // 
            string str_code = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAAvElEQVR42u3WQQqDMBSE4eQ2FWk3Hsoz9VBd6cLb6BNcdSE1IWby+g8ICYjOpz4wru8heEj8C0gcP+sNHSbr8PIA2bNYj644xM6JJdp/PagsjApkseMRMj4zCch+fVvPtuxD4puRgRz7ZIwUJAcjB0nFSEJSMLKQqxgJyI+ZrcfTA+S0R1XIVTAQIC1BSv3in90LCJCWIHcGCJAWIW6GHQgQhh0IkCoQN8MOBAjDDgQIECBAKkNUAqSluIFsV0sN9+kjczYAAAAASUVORK5CYII=";
            _code.Image = OneScriptFormsDesigner.Base64ToImage(str_code);
            _code.Name = "_code";
            _code.Size = new Size(50, 24);
            _code.Text = "Сценарий";
            _code.Click += _code_Click;
            _code.CheckState = System.Windows.Forms.CheckState.Unchecked;
            // 
            // _tools
            // 
            _tools.DropDownItems.AddRange(new ToolStripItem[] {
            _tabOrder,
            _stripSeparator5,
            _run,
            _stripSeparator6,
            _settings});
            _tools.Name = "_tools";
            _tools.Size = new Size(113, 24);
            _tools.Text = "Инструменты";
            // 
            // _tabOrder
            // 
            string str_tabOrder = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAA3ElEQVR42u3ZSw6AIAwEULj/oTExcdOApjD9menWiPOiQNU+Wu3qaEBvbXvIcZ8eCDgJv4v4BMhQqwsgwsMBq1DyIqjwIQBkeAJ2AGgEJ/FbuK/BUy6jGsApwmwj0wC8awqYpBzrQ9jS3j4CCLAATEC1JjEBlQHo5u6pZQuTrZXQItI1c1BARDvtDvAKT8DbQGkfoVm4UpNYhiu5jGoAlgiXjSyiCIiu/wJE0npvZAQQcAgQGPONbHeBSNVKmHwb9Wzm4ADvdjoEEPl3hgA0gpP4tFIuo14I040sc11VMcAB84B/6gAAAABJRU5ErkJggg==";
            _tabOrder.Image = OneScriptFormsDesigner.Base64ToImage(str_tabOrder);
            _tabOrder.Name = "_tabOrder";
            _tabOrder.Size = new Size(217, 26);
            _tabOrder.Text = "Порядок обхода";
            _tabOrder.Click += _tabOrder_Click;
            // 
            // _stripSeparator5
            // 
            _stripSeparator5.Name = "_stripSeparator5";
            // 
            // _run
            // 
            string str_run = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABkUlEQVR42t3Z0Q7CIAwFUPv/H42JmWYD2t4WCkUejDrAHjsGbPT6k0K7AwBL0RyZIVrw6SEWQEoIDyjVoTbsFBAcwIe+FeIDJIO0kSLBJ4OMIxJAnhF7AB3E7+UoxGaIjPgGZh/kSyE8gjuzDZfd5s1mBDX1exgmG9EQ7HRqYygghP+QAMFjhGxEQfR5Yhwiq5YgdIw5G7MhthnbMthJP4tmQXzLDuTyC2QDhdinYO8CENh3sF1sR2g4MFbp4DEIqYK+wKt/bCOCq3QcoldxD4LGr6g2SFKEDpFKIoTUSI4yGQJtOG+LGoSwNJY3PJsR6yFBiLWQQATa0ey7gtMRSGflLnlURjHkXrDGQcwYci9Y4yEwprcKAHZ6URAfhlvKrIaUK26q9tIFadx0Ztt7T4OUKt4hjJypOEhh1lgSJh2kl4n7dzVG+EOQWzoxEA2hYPhdZn+QT0d8OkQywWRGKtIEE3K/WQ1aGPweSNjdf5IGuBPBYUKfxbBjYRBRQ8KfjLEZGUQsLyrkBMQVJzQRpi9vUR3TM0g50FMAAAAASUVORK5CYII=";
            _run.Image = OneScriptFormsDesigner.Base64ToImage(str_run);
            _run.Name = "_run";
            _run.Text = "Запуск";
            _run.Click += _run_Click;
            _run.ShortcutKeys = Keys.Control | Keys.F6;
            _run.ShowShortcutKeys = true;
            // 
            // _stripSeparator6
            // 
            _stripSeparator6.Name = "_stripSeparator6";
            // 
            // _settings
            // 
            string str_settings = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABI0lEQVR42u3YwQ6EMAgEUP3/j3aTPRhFaGkLOLNZrqKZp9JY92P7jdr/kKgA29aNcHzbQCEeADTEAlzDXns8iHKIhpBBZxClEInQQs4iXoNEI8og2YhySCRCXjMd0gq6iqCGWItGGSTitWot3zQQ2fvajFhBPRh1JpCGXR7Xeqzz4SAaRisLCAXpYTxPCQZigbyLRCrEEyrqxqRBKhFpkGxECeQNRDhkBNHbLXq2xCmQVUSvup8wEZBMRNnPhxVExPyEQFAQSxAkxDQEDTEFQUQMQ1ARQxBkhBuCjnBBGBBdCAuiCWFCmBA2hAphRDwgrIgbhBlxQtgRKoQRMQRBRpwQGbT1ixIRYUKuYRkQN4gWWhYq4gFpYZARKkQDoSOaELb6AFfXABC6bvmCAAAAAElFTkSuQmCC";
            _settings.Image = OneScriptFormsDesigner.Base64ToImage(str_settings);
            _settings.Name = "_settings";
            _settings.Text = "Параметры";
            _settings.Click += _settings_Click;
            // 
            // _help
            // 
            _help.DropDownItems.AddRange(new ToolStripItem[] { _about });
            _help.Name = "_help";
            _help.Size = new Size(77, 24);
            _help.Text = "Помощь";
            // 
            // _about
            // 
            string str_about = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABCklEQVR42u3WwRKDIAyE4fLmvjntpRenSDbZTaBDzo7+n4NCu157TzuAA1gE8LlPB69vSwDQcDbGDWCEMyAwQBEeQUAAdbwHYQZkxaMIEyA7HkFMAVXxVsQjwBvfBw9tzvs9IaiAbl23xE1vCFDFsxEUABrvQUCAjHgUMEJIAd9AyzU0ABL/FHcPY/2Z7ogwYBS3FcCL3AbA3NjSAawPuATAjk8FKOKXAETi0wCq+FIAI/4AVpgpYGXEpTiN/hrVEjIDIois////A7yIjOODFDBCKN7+FMBCqOJNgAgiOrN4M6ACYYmHAJkIazwMyEAg8S6ACoKGhwEsiDecBvBCouF0QNUcQPVsD3gDeqycMcHL1j4AAAAASUVORK5CYII=";
            _about.Image = OneScriptFormsDesigner.Base64ToImage(str_about);
            _about.Name = "_about";
            _about.Size = new Size(187, 26);
            _about.Text = "О программе...";
            _about.Click += _about_Click;
            // 
            // pnl4Toolbox
            // 
            pnl4Toolbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            pnl4Toolbox.Controls.Add(listBox1);
            pnl4Toolbox.Dock = System.Windows.Forms.DockStyle.Left;
            pnl4Toolbox.Location = new Point(0, 26);
            pnl4Toolbox.Name = "pnl4Toolbox";
            pnl4Toolbox.Size = new Size(163, 489);
            pnl4Toolbox.TabIndex = 2;
            // 
            // listBox1
            // 
            listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 16;
            listBox1.Location = new Point(0, 0);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(159, 485);
            listBox1.TabIndex = 0;
            // 
            // pnl4pDesigner
            // 
            pnl4pDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            pnl4pDesigner.Location = new Point(163, 26);
            pnl4pDesigner.Name = "pnl4pDesigner";
            pnl4pDesigner.Size = new Size(726, 489);
            pnl4pDesigner.TabIndex = 3;
            // 
            // pnl4splitter
            // 
            pnl4splitter.BackColor = Color.LightSteelBlue;
            pnl4splitter.Location = new Point(163, 26);
            pnl4splitter.Name = "pnl4splitter";
            pnl4splitter.Size = new Size(5, 489);
            pnl4splitter.TabIndex = 4;
            pnl4splitter.TabStop = false;
            // 
            // pDesignerMainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(889, 515);
            Controls.Add(pnl4splitter);
            Controls.Add(pnl4pDesigner);
            Controls.Add(pnl4Toolbox);
            Controls.Add(menuStrip1);
            string str_Icon = "AAABAAEAAAAQAAEABABooAAAFgAAACgйQAAAAIAAAEABйAAKййAAEййAAAAACAAACAAAAAgIAAgAAAAIAAgACAgAAAgICAAMDAwAAAAP8AAP8AAAD//wD/AAAA/wD/AP//AAD///8ккккккккккккккккккккккккккккккккккккккккккйййййAAAеееец////8ййAAAееееццййAAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDееццццwййеццййAPееццццййDец/////8ййAеецццц8ййPец/////wййDееццццwййеццййAPееццццййDец/////8ййAеецццц8ййPец/////wййDееццццwййеццййAPееццццййDец/////8ййAеецццц8ййPец/////wййDееццццwййеццййAPееццццййDец/////8ййAеецццц8ййPец/////wййDееццццwййеццййAPее//8Aццц//ййDццц/8Pцццц//8ййAее//8AAP8йййййPAAAADцц/8ADцццц//wййDгкййAAAP8AAAD/ййAAAADwйй8AAAAADц/////8AAAцццц//ййAPццц/wкййAAAP8AAAAA/wййAAAPййDwйц////8AAAAPцццц8ййAццц/wкййAAAP8йP8ййAAA8ййP/wAAAADц///8AAAAADццццwййDццц/кййAAAP8йAD/ййD//wййAP//AAAAц//8йAццццййAPцццкййAAAP8йAAA/wйAAD///ййй//AADц/8йAAPг/8ййAццц8кййAAP8йAAAAP8йA//йййAAAA//8Pц8йAAADг/wййDцццwкййAP8йAAAAAD/AAAAD//wййййDц/8йAAAAAг/ййAPцццкййAP8ййA/wAA//ййййAAAAD/////8ййPццц//8ййAццц8кййP8ййAAP///wййййй////8ййADццц//wййDцццwйDе/////8ййAAAD//wйййййAA//8ййAAAццц//ййAPцццйDе/////8ййAAAAA8йййййAAAAA8ййAAAAPццц8ййAццц8йPе/////wкйййййAAAAццц/wййDцццwйец8кйййййAAAццц//ййAPцццйDец/кйййййAAццц//8ййAццц8йPец/wкйййййгwййDцццwйец//8кййййAAAAAг/ййAPцццйDец///кййййAAAAг/8ййAццц8йPец///wкййййAAг//wййDцццwйец////8кййййAццццййAPцццйDец/////кййййцццц8ййAццц8йPец/////wкйййAAAAцццц/wййDцццwйецц8кйййAAAцццц//ййAPцццйDецц/кйййAAцццц//8ййAццц8йPецц/wкйййцгwййDцццwйецц//кйййDцгййAPцццйDецц/8кйййAцццц//8ййAццц8йPецц8кйййADцццц//wййDцццwйецц/wкйййAAцццц//ййAPцццйDеццwкйййAADцццц/8ййAццц8йPеццкйййAAAAцццц/wййDцццwйеццкйййAAAAAPццццййAPцццйDец/////8ййййADц/ййййAAцццц8ййAццц8йPец////8ййййAц////8йййAAAAADццццwййDцццwйец/////wйййAAAADцц/8йййAAAAAццццййAPцццйDец////wйййAAAADцц///йййAAAADг//8ййAццц8йPец////йййAAAAцццйййAAAAг//wййDцццwйец////8йййAAAццц/wйййAADг//ййAPцццйDец////wйййAAццц//8йййAAPг/8ййAццц8йPец///wйййAADццц//wйййAADг/wййDцццwйец////йййAADг8йййAAPг/ййAPцццйDец///8йййADг//йййAAг/8ййAццц8йPец//8йййAAPг/8йййADг/wййDцццwйццгwйййййAPццццйййййAPц////ййAPцццйDццгйййййAAцццц/wйййййц////8ййAццц8йPццццц//8йййййAцццц//йййййDц////wййDцццwйццгwйййййDцццц//wййййAAAAAPц////ййAPцццйDццгйййййAPцццц//йййййц////8ййAццц8йPццццц//8йййййAцццц//8ййййAAAAADц////wййDцццwйццгwйййййцгwййййAAAAAPц////ййAPцццйDццгйййййDцг8ййййAAAAAц////8ййAццц8йPццццц//8йййййPцгwййййAAAADц////wййDцццwйццгwйййййцг/ййййAAAAAPц////ййAPцццйDццгйййййDцг8ййййAAAAAц////8ййAццц8йPццццц//8йййййPцццц//8ййййAAAAADц////wййDцццwйццгwйййййDцццц//wййййAAAAAPц////ййAPцццйDццгйййййAPцццц//йййййц////8ййAццц8йPццццц//8йййййAцццц//8ййййAAAAADц////wййDцццwйццгwйййййDцццц/8йййййPц////ййAPцццйDццгйййййAAцццц/wйййййц////8ййAццц8йPццццц//8йййййADццццwйййййDц////wййDцццwйец//йййAAAAг//wйййAPг/ййAPцццйDец///8йййADг//йййAAг/8ййAццц8йPец///wйййAAг/йййAADг/wййDцццwйец////йййAADгйййAAAPг/ййAPцццйDец///8йййAAAццц//8йййAAPг/8ййAццц8йPец////йййAAAPццц8йййAAAг//wййDцццwйец////8йййAAADцц/////8йййAAADг//ййAPцццйDец/////йййAAAAцц////8йййAAADг//8ййAццц8йPец////8йййAAAAAцц//йййAAAAAPг//wййDцццwйец/////wййййц/////wййййццццййAPцццйDец/////8ййййAц//8ййййAцццц8ййAццц8йPец/////wййййAAAA8PййййAAAADццццwййDцццwйецц8кйййAAADцццц/ййAPцццйDеццwкйййAAAPцццц8ййAццц8йPецц8кйййAAPцццц/wййDцццwйецц/wкйййAPцццц//ййAPцццйDецц/8кйййAцццц//8ййAццц8йPецц//кйййцгwййDцццwйецц//8кййAAAAADцгййAPцццйDецц/8кйййAцццц//8ййAццц8йPецц8кйййAAPцццц/wййDцццwйецц8кйййAAADцццц/ййAPцццйDец/////8кйййAAAAAцццц8ййAццц8йPец////8кййййPг//wййDцццwйец////8кййййADг//ййAPцццйDец///8кййййAAAг/8ййAццц8йPец//8кййййAAAAPгwййDцццwйец//8кййййAAAAADгййAPцццйDец/8кйййййAццц//8ййAццц8йPец8кйййййAAPццц/wййDцццwйец8кйййййAAADццц/ййAPцццйDецwййAAAA8йййййAAAAA8ййAAAAPццц8ййAццц8йPец8ййAAA//йййййAAAA//ййAAAPццц/wййDцццwйец//ййAA////йййййAP///wййAPццц//ййAPцццйDец//wййцййййAAAAц8ййPццц//8ййAццц8йPец//8йAAAAAц//ййййAPц//йAAAAAPгwййDцццwкййAAAPйAAAA8йAD/8йййAAAD/8Aц/wйAAAPг/ййAPцццкййAAAADwйAA8йAAAD//wййAAAAD///AADц/8йAAPг/8ййAццц8кййAAAAA8йA8ййP//8ййP//AAAAAPц//йAPг//wййDцццwкййAAAAAPй8ййAAADwйй/wйц///wAAAAAPццццййAPцццкйййDwAAAA8ййAAAAPййD8йDц///8AAAAPцццц8ййAццц8кйййA8AAA8ййAAAAA8ййPwйPц////AAAPцццц/wййDцццwкйййAPAA8ййAAAAADwйй/йAц/////wAPцццц//ййAPцццкйййAADw8йййPййD8йDц/////8Pцццц//8ййAццц8кйййAAA8йййA8ййPwйPе///wййDцццwйнуууууzMzMzwйй/йAе////ййAPцццйDMнуууууzMzPййD8йDе///8ййAццц8йMнуууууzMzM8ййPwйPе///wййDцццwйнуууууzMzMzwйй/йAе////ййAPцццйDMzMzMzйййййAууwAAAуzMAAAMzMzMzPййD8йDе///8ййAццц8йMzMzMwAццццц8AуzMzMAP//8AzMzMzAD///AMzMzM8ййPwйPе///wййDцццwйzMzMwPццццц//8MуzMD/////8MzMzAцDMzMzwйй/йAе////ййAPцццйDMzMwPццг/DMуDц/DMzAц/wzMwPййD8йDе///8ййAццц8йMzMzAццг/8MуwPц8MzMDц/DMzA8ййPwйPе///wййDцццwйzMzAеDMzMzMwPц//DMDц//wzA/wйй/йAе////ййAPцццйDMzMDццг//8MzMzMzAц//8MwPц//DMD/ййD8йDе///8ййAццц8йMzMwPццг//wуDц//wzAц//8MwP8ййPwйPе///wййDцццwйzMzAеDMzMzMwPц//DMDц//wzAцц///йAе////ййAPцццйDMzMDццг//8MzMzMzAц//8MwPц//DMDц//wуйDе///8ййAццц8йMzMzAццг/8MуwPц8MzMDц/DMzAц/wуwйPе///wййDцццwйzMzMDццг/wуzAц/wzMwPц8MzMDц/DMzMzMzйAе////ййAPцццйDMzMzAццгwуzMwP/////wzMzMD/////8MzMzAцDMуйDе///8ййAццц8йMzMzMwAццццц8AуzMzMAP//8AzMzMzAD///AMzMzMwA///wDMуwйPе///wййDцццwйуwйййййMууAAAMуzAAADMуwAAAуzMzйAе////ййAPцццйDMннууйDе///8ййAццц8йMннууwйPе///wййDцццwйннууwйAе////ййAPцццйAMннуzMzMwйADе///8ййAццц8ккййййAAPе///wййDцццwккййййAAе////ййAPцццккййййAADе///8ййAццц8ккййййAAPе///wййDцццwккййййAAе////ййAPццц8ккййййAе////8ййAццц/wккййййDе////wййDццц/8ккйййAAAAADе/////ййAPццц//8ккйййAAAец8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8кккккккккккккккккккккккккккккккккккккккккккккккййййAAADMннннууййAMннннууwййAннннууzййADMннннууййAMннннууwййAннннууzййADMннннууййAMннннууwййAннннууzййADMуzMккAAуууzMAAAAAMууzMAAAAAMууzMAAAAAMуzMййAMуzADее/8AууzMzMzAD/////AMуzMzMzAD/////AMуzMzMzAD/////AMуwййAуwAее////8AууzMwAц//AMуzMwAц//AMуzMwAц//AMzMzMzййADMzMzMwPеец8MууwPц////DMуwPц////DMуwPц////DMzMzMййAMzMzMwPеец//DMуzMzMwPц/////wуwPц/////wуwPц/////wzMzMwййAzMzMwPеец///wуzMzMwPцц8MzMzMwPцц8MzMzMwPцц8MzMzййADMzMzAеец////DMуzMzAцц/wzMzMzAцц/wzMzMzAцц/wzMzMййAMzMzAеец/////wуzMzAцц//8MzMzAцц//8MzMzAцц//8MzMwййAzMzMDеец/////DMуzMDцц//wzMzMDцц//wzMzMDцц//wzMzййADMzMDееццwуzMDцц///8MzMDцц///8MzMDцц///8MzMййAMzMwPееццDMуwPцц///wzMwPцц///wzMwPцц///wzMwййAzMzAеецц8MуzAцц////DMzAцц////DMzAцц////DMzййADMzMDееццwуzMDцц///8MzMDцц///8MzMDцц///8MzMййAMzMwPееццDMуwPцц///wzMwPцц///wzMwPцц///wzMwййAzMzAеецц8MуzAцц////DMzAцц////DMzAцц////DMzййADMzMDееццwуzMDцц///8MzMDцц///8MzMDцц///8MzMййAMzMwPееццDMуwPцц///wzMwPцц///wzMwPцц///wzMwййAzMzMDеец/////DMуzMDцц//wzMzMDцц//wzMzMDцц//wzMzййADMzMwPеец////8MуzMwPцц//DMzMwPцц//DMzMwPцц//DMzMййAMzMzMDеец///8MуzMzMDцц/DMzMzMDцц/DMzMzMDцц/DMzMwййAzMzMwPеец///wуzMzMwPцц8MzMzMwPцц8MzMzMwPцц8MzMzййADMzMzMDеец//wууDц/////8MуDц/////8MуDц/////8MzMzMййAMzMzMzAеец/wууzAц////8MуzAц////8MуzAц////8MzMzMwййAуwAее////8AууzMwAц//AMуzMwAц//AMуzMwAц//AMzMzMzййADMуwAее//AMууzMzMwA/////wDMуzMzMwA/////wDMуzMzMwA/////wDMуййAMуzMwккADMуууwAAAAAууzMwAAAAAууzMwAAAAAуzMwййAннннууzййADMннннууййAMннннууwййAннннууzййADMннннууййAAннннууwййAAMннннуzMzMwкккккккккккккккккккккккккккккккккккккккккккккккйййAAAD/gкAAB//4кAAAB/+кAAAAB/wкAAAAD+кAAAAAHwкAAAAAPкй4кAAAAABgкAAAAAEккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккAAIкAAAAABgкAAAAAHкй8кAAAAAD4кAAAAAfwкAAAAD/gкAAAAf/gкAAAH//gкAAB/w==";
            str_Icon = str_Icon.Replace("г", "ццц///");
            str_Icon = str_Icon.Replace("н", "уууууу");
            str_Icon = str_Icon.Replace("е", "цццццц");
            str_Icon = str_Icon.Replace("к", "йййййй");
            str_Icon = str_Icon.Replace("у", "zMzMzM");
            str_Icon = str_Icon.Replace("ц", "//////");
            str_Icon = str_Icon.Replace("й", "AAAAAA");
            Icon = new Icon(new MemoryStream(Convert.FromBase64String(str_Icon)));
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4);
            Name = "pDesignerMainForm";
            Text = "Дизайнер форм для OneScriptForms";
            Load += pDesignerMainForm_Load;
            //* 18.12.2021 perfolenta
            FormClosing += pDesignerMainForm_Closing;
            //***
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            pnl4Toolbox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

            // Элемент управления: (pDesigner)pDesignerCore.
            IpDesignerCore = pDesignerCore as IpDesigner;
            pDesignerCore.Parent = pnl4pDesigner;

            // Добавим указатель.
            ToolboxItem toolPointer = new ToolboxItem();
            toolPointer.DisplayName = "<Указатель>";
            toolPointer.Bitmap = new Bitmap(16, 16);
            listBox1.Items.Add(toolPointer);

            // Добавим элементы управления.
            ToolboxItem toolButton = new ToolboxItem(typeof(Button));
            toolButton.DisplayName = "Кнопка (Button)";
            listBox1.Items.Add(toolButton);

            ToolboxItem toolCheckBox = new ToolboxItem(typeof(CheckBox));
            toolCheckBox.DisplayName = "Флажок (CheckBox)";
            listBox1.Items.Add(toolCheckBox);

            ToolboxItem toolColorDialog = new ToolboxItem(typeof(ColorDialog));
            toolColorDialog.DisplayName = "ДиалогВыбораЦвета (ColorDialog)";
            listBox1.Items.Add(toolColorDialog);

            ToolboxItem toolComboBox = new ToolboxItem(typeof(ComboBox));
            toolComboBox.DisplayName = "ПолеВыбора (ComboBox)";
            listBox1.Items.Add(toolComboBox);

            ToolboxItem toolDataGrid = new ToolboxItem(typeof(DataGrid));
            toolDataGrid.DisplayName = "СеткаДанных (DataGrid)";
            listBox1.Items.Add(toolDataGrid);
	
            ToolboxItem toolDataGridView = new ToolboxItem(typeof(DataGridView));
            toolDataGridView.DisplayName = "Таблица (DataGridView)";
            listBox1.Items.Add(toolDataGridView);

            ToolboxItem toolDateTimePicker = new ToolboxItem(typeof(DateTimePicker));
            toolDateTimePicker.DisplayName = "ПолеКалендаря (DateTimePicker)";
            listBox1.Items.Add(toolDateTimePicker);

            ToolboxItem toolFileSystemWatcher = new ToolboxItem(typeof(FileSystemWatcher));
            toolFileSystemWatcher.DisplayName = "НаблюдательФайловойСистемы (FileSystemWatcher)";
            listBox1.Items.Add(toolFileSystemWatcher);

            ToolboxItem toolFontDialog = new ToolboxItem(typeof(FontDialog));
            toolFontDialog.DisplayName = "ДиалогВыбораШрифта (FontDialog)";
            listBox1.Items.Add(toolFontDialog);

            ToolboxItem toolFolderBrowserDialog = new ToolboxItem(typeof(FolderBrowserDialog));
            toolFolderBrowserDialog.DisplayName = "ДиалогВыбораКаталога (FolderBrowserDialog)";
            listBox1.Items.Add(toolFolderBrowserDialog);

            ToolboxItem toolGroupBox = new ToolboxItem(typeof(GroupBox));
            toolGroupBox.DisplayName = "РамкаГруппы (GroupBox)";
            listBox1.Items.Add(toolGroupBox);

            ToolboxItem toolHProgressBar = new ToolboxItem(typeof(HProgressBar));
            toolHProgressBar.DisplayName = "ИндикаторГоризонтальный (HProgressBar)";
            listBox1.Items.Add(toolHProgressBar);

            ToolboxItem toolVProgressBar = new ToolboxItem(typeof(VProgressBar));
            toolVProgressBar.DisplayName = "ИндикаторВертикальный (VProgressBar)";
            listBox1.Items.Add(toolVProgressBar);

            ToolboxItem toolHScrollBar = new ToolboxItem(typeof(HScrollBar));
            toolHScrollBar.DisplayName = "ГоризонтальнаяПрокрутка (HScrollBar)";
            listBox1.Items.Add(toolHScrollBar);

            ToolboxItem toolImageList = new ToolboxItem(typeof(System.Windows.Forms.ImageList));
            toolImageList.DisplayName = "СписокИзображений (ImageList)";
            listBox1.Items.Add(toolImageList);

            ToolboxItem toolLabel = new ToolboxItem(typeof(Label));
            toolLabel.DisplayName = "Надпись (Label)";
            listBox1.Items.Add(toolLabel);

            ToolboxItem toolLinkLabel = new ToolboxItem(typeof(LinkLabel));
            toolLinkLabel.DisplayName = "НадписьСсылка (LinkLabel)";
            listBox1.Items.Add(toolLinkLabel);

            ToolboxItem toolListBox = new ToolboxItem(typeof(ListBox));
            toolListBox.DisplayName = "ПолеСписка (ListBox)";
            listBox1.Items.Add(toolListBox);

            ToolboxItem toolListView = new ToolboxItem(typeof(ListView));
            toolListView.DisplayName = "СписокЭлементов (ListView)";
            listBox1.Items.Add(toolListView);

            ToolboxItem toolMainMenu = new ToolboxItem(typeof(System.Windows.Forms.MainMenu));
            toolMainMenu.DisplayName = "ГлавноеМеню (MainMenu)";
            listBox1.Items.Add(toolMainMenu);
	
            ToolboxItem toolMaskedTextBox = new ToolboxItem(typeof(MaskedTextBox));
            toolMaskedTextBox.DisplayName = "МаскаПоляВвода (MaskedTextBox)";
            listBox1.Items.Add(toolMaskedTextBox);
	
            ToolboxItem toolMonthCalendar = new ToolboxItem(typeof(MonthCalendar));
            toolMonthCalendar.DisplayName = "Календарь (MonthCalendar)";
            listBox1.Items.Add(toolMonthCalendar);

            ToolboxItem toolNotifyIcon = new ToolboxItem(typeof(NotifyIcon));
            toolNotifyIcon.DisplayName = "ЗначокУведомления (NotifyIcon)";
            listBox1.Items.Add(toolNotifyIcon);

            ToolboxItem toolNumericUpDown = new ToolboxItem(typeof(NumericUpDown));
            toolNumericUpDown.DisplayName = "РегуляторВверхВниз (NumericUpDown)";
            listBox1.Items.Add(toolNumericUpDown);

            ToolboxItem toolOpenFileDialog = new ToolboxItem(typeof(OpenFileDialog));
            toolOpenFileDialog.DisplayName = "ДиалогОткрытияФайла (OpenFileDialog)";
            listBox1.Items.Add(toolOpenFileDialog);

            ToolboxItem toolPanel = new ToolboxItem(typeof(Panel));
            toolPanel.DisplayName = "Панель (Panel)";
            listBox1.Items.Add(toolPanel);

            ToolboxItem toolPictureBox = new ToolboxItem(typeof(PictureBox));
            toolPictureBox.DisplayName = "ПолеКартинки (PictureBox)";
            listBox1.Items.Add(toolPictureBox);

            ToolboxItem toolPropertyGrid = new ToolboxItem(typeof(PropertyGrid));
            toolPropertyGrid.DisplayName = "СеткаСвойств (PropertyGrid)";
            listBox1.Items.Add(toolPropertyGrid);

            ToolboxItem toolRadioButton = new ToolboxItem(typeof(RadioButton));
            toolRadioButton.DisplayName = "Переключатель (RadioButton)";
            listBox1.Items.Add(toolRadioButton);

            ToolboxItem toolRichTextBox = new ToolboxItem(typeof(RichTextBox));
            toolRichTextBox.DisplayName = "ФорматированноеПолеВвода (RichTextBox)";
            listBox1.Items.Add(toolRichTextBox);

            ToolboxItem toolSaveFileDialog = new ToolboxItem(typeof(SaveFileDialog));
            toolSaveFileDialog.DisplayName = "ДиалогСохраненияФайла (SaveFileDialog)";
            listBox1.Items.Add(toolSaveFileDialog);

            ToolboxItem toolSplitter = new ToolboxItem(typeof(Splitter));
            toolSplitter.DisplayName = "Разделитель (Splitter)";
            listBox1.Items.Add(toolSplitter);

            ToolboxItem toolStatusBar = new ToolboxItem(typeof(StatusBar));
            toolStatusBar.DisplayName = "СтрокаСостояния (StatusBar)";
            listBox1.Items.Add(toolStatusBar);

            ToolboxItem toolTabControl = new ToolboxItem(typeof(TabControl));
            toolTabControl.DisplayName = "ПанельВкладок (TabControl)";
            listBox1.Items.Add(toolTabControl);

            ToolboxItem toolTextBox = new ToolboxItem(typeof(TextBox));
            toolTextBox.DisplayName = "ПолеВвода (TextBox)";
            listBox1.Items.Add(toolTextBox);

            ToolboxItem toolTimer = new ToolboxItem(typeof(Timer));
            toolTimer.DisplayName = "Таймер (Timer)";
            listBox1.Items.Add(toolTimer);

            ToolboxItem toolToolBar = new ToolboxItem(typeof(ToolBar));
            toolToolBar.DisplayName = "ПанельИнструментов (ToolBar)";
            listBox1.Items.Add(toolToolBar);

            ToolboxItem toolToolTip = new ToolboxItem(typeof(ToolTip));
            toolToolTip.DisplayName = "Подсказка (ToolTip)";
            listBox1.Items.Add(toolToolTip);

            ToolboxItem toolTreeView = new ToolboxItem(typeof(TreeView));
            toolTreeView.DisplayName = "Дерево (TreeView)";
            listBox1.Items.Add(toolTreeView);

            ToolboxItem toolUserControl = new ToolboxItem(typeof(UserControl));
            toolUserControl.DisplayName = "ПользовательскийЭлементУправления (UserControl)";
            listBox1.Items.Add(toolUserControl);

            ToolboxItem toolVScrollBar = new ToolboxItem(typeof(VScrollBar));
            toolVScrollBar.DisplayName = "ВертикальнаяПрокрутка (VScrollBar)";
            listBox1.Items.Add(toolVScrollBar);

            listBox1.Sorted = true;
            listBox1.HorizontalScrollbar = true;
            IpDesignerCore.Toolbox = listBox1;
        }

        private void _form_Click(object sender, EventArgs e)
        {
            pDesigner.SplitterpDesigner.Visible = true;
            pDesigner.CodePanel.Visible = false;
            _addForm.Enabled = true; // "Добавить Форму"
            _deleteForm.Enabled = true; // "Удалить Форму"
            _edit.Enabled = true; // "Правка"
            _tools.Enabled = true; // "Инструменты"
            pDesigner.SplitterpDesigner.Panel2Collapsed = false;
            pnl4Toolbox.Visible = true;
            _form.Enabled = false;
            _code.Enabled = true;
            _form.CheckState = System.Windows.Forms.CheckState.Checked;
            _code.CheckState = System.Windows.Forms.CheckState.Unchecked;
        }

        private void _code_Click(object sender, EventArgs e) // Сценарий
        {
            SaveScript.comps.Clear();
            pDesigner.SplitterpDesigner.Visible = false;
            pDesigner.CodePanel.Visible = true;
            _addForm.Enabled = false; // "Добавить Форму"
            _deleteForm.Enabled = false; // "Удалить Форму"
            _edit.Enabled = false; // "Правка"
            _tools.Enabled = false; // "Инструменты"
            pDesigner.SplitterpDesigner.Panel2Collapsed = true;
            pnl4Toolbox.Visible = false;
            _form.Enabled = true;
            _code.Enabled = false;
            _form.CheckState = System.Windows.Forms.CheckState.Unchecked;
            _code.CheckState = System.Windows.Forms.CheckState.Checked;

            string strScript = null;
            // Если для запускаемой формы свойство Путь не заполнено или там указан файл .osd тогда формируем скрипт и запускаем.
            Form savedForm = (Form)OneScriptFormsDesigner.DesignerHost.RootComponent;
            bool scriptMissing = false;
            if (savedForm.Path == null)
            {
                scriptMissing = true;
            }
            else
            {
                if (savedForm.Path.Substring(savedForm.Path.Length - 4) == ".osd")
                {
                    scriptMissing = true;
                }
            }
            if (scriptMissing)
            {
                OneScriptFormsDesigner.PropertyGrid.Refresh();
                strScript = GenerateScriptCode(null, null);

                if (OneScriptFormsDesigner.RootComponent.ScriptStyle == ScriptStyle.СтильПриложения)
                {
                    string strFind = @"// маркерКонцаПроцедуры
КонецПроцедуры

ПодготовкаКомпонентов();";
                    string strReplace = @"// маркерКонцаПроцедуры
КонецПроцедуры" + Environment.NewLine +
"ПодключитьВнешнююКомпоненту(" + "\u0022" + Settings.Default["dllPath"] + "\u0022" + @");" + Environment.NewLine +
@"" + savedForm.NameObjectOneScriptForms + @" = Новый ФормыДляОдноСкрипта();
ПриСозданииФормы(" + savedForm.NameObjectOneScriptForms + @".Форма());
" + savedForm.NameObjectOneScriptForms + @".ЗапуститьОбработкуСобытий();";
                    strScript = strScript.Replace(strFind, strReplace);
                }

                if ((bool)Settings.Default["visualSyleForms"])
                {
                    string strFind = @"" + savedForm.NameObjectOneScriptForms + @" = Новый ФормыДляОдноСкрипта();";
                    string strReplace = @"" + savedForm.NameObjectOneScriptForms + @" = Новый ФормыДляОдноСкрипта();" + Environment.NewLine +
                    @"    " + savedForm.NameObjectOneScriptForms + @".ВключитьВизуальныеСтили();";
                    strScript = strScript.Replace(strFind, strReplace);
                }
            }
            else // иначе там может быть указан только файл сценария .os и тогда формируем скрипт с учетом кода в этом файле.
            {
                string oldScriptText = File.ReadAllText(savedForm.Path);
                strScript = GenerateScriptCode(oldScriptText, savedForm.Path);
            }
            pDesigner.RichTextBox.Text = strScript;
        }

        private void _loadScript_Click(object sender, EventArgs e) // Открыть сценарий
        {
            // Прочитаем строку Base64 следующую в сценарии после строки Процедура ПодготовкаКомпонентов()
            // или после строки Процедура ПриСозданииФормы(_Форма) Экспорт.
            // Расшифруем её и загрузим из неё форму.
            // При открытии поле Путь заполнить именем файла с расширением os.
            System.Windows.Forms.OpenFileDialog OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            OpenFileDialog1.RestoreDirectory = true;
            OpenFileDialog1.Filter = "OS files(*.os)|*.os";
            if (OpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            string strFile = File.ReadAllText(OpenFileDialog1.FileName);
            string base64Text = OneScriptFormsDesigner.ParseBetween(strFile, "osdText = \u0022", "\u0022;");
            if (base64Text == null)
            {
                MessageBox.Show(
                    "Не найдена закомментированная переменная osdText в процедуре инициализации сценария.",
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1
                    );
                return;
            }
            string osdText;
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64Text);
                osdText = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                // Возможно открываемый сценарий был сохранен из дизайнера не в том каталоге где сейчас находится.
                // В любом случае меняем путь записанный в свойстве Путь сценария на текущий путь открываемого сценария.
                string _path = OneScriptFormsDesigner.ParseBetween(osdText, ".Путь = \u0022", "\u0022;");
                osdText = osdText.Replace(_path, OpenFileDialog1.FileName);
            }
            catch
            {
                MessageBox.Show(
                    "Не удалось восстановить форму из переменной osdText в процедуре инициализации сценария. Возможно строка повреждена.",
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1
                    );
                return;
            }

            LoadForm_Click(OpenFileDialog1.FileName, true, osdText);
        }

        public static string RemoveDuplicates(string input)
        {
            ArrayList ArrayList1 = new ArrayList();
            string output = string.Empty;
            string[] stringSeparators = new string[] { Environment.NewLine };
            string[] parts = input.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
            {
                if (!ArrayList1.Contains(part))
                {
                    ArrayList1.Add(part);
                }
            }
            ArrayList1.Sort();
            for (int i = 0; i < ArrayList1.Count; i++)
            {
                output += ArrayList1[i];
                output += Environment.NewLine;
            }
            output = output + Environment.NewLine;

            string[] peremArray = output.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
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
            newPerem = newPerem.Trim() + Environment.NewLine;
            return newPerem;
        }

        private string GenerateScriptCode(string oldScriptText = null, string path = null)
        {
            string strReplace = null;
            string strFind = null;
            string scriptText;
            string newScriptText;
            dynamic plainTextBytes;

            SaveScript.comps.Clear();
            scriptText = SaveScript.GetScriptText(path);
            Form savedForm = (Form)OneScriptFormsDesigner.DesignerHost.RootComponent;

            if ((bool)Settings.Default["visualSyleForms"])
            {
                strFind = @"" + savedForm.NameObjectOneScriptForms + @" = Новый ФормыДляОдноСкрипта();";
                strReplace = @"" + savedForm.NameObjectOneScriptForms + @" = Новый ФормыДляОдноСкрипта();" + Environment.NewLine +
                @"    " + savedForm.NameObjectOneScriptForms + @".ВключитьВизуальныеСтили();";
                scriptText = scriptText.Replace(strFind, strReplace);
                strFind = null;
                strReplace = null;
            }

            if (path != null)
            {
                plainTextBytes = System.Text.Encoding.UTF8.GetBytes(SaveForm.GetScriptText(path));
                string osdText = System.Convert.ToBase64String(plainTextBytes);
                string strAdd = @"    // ВАЖНО: Необходимая процедура для поддержки конструктора — не изменяйте содержимое этой процедуры с помощью редактора кода." + Environment.NewLine;
                if (OneScriptFormsDesigner.RootComponent.ScriptStyle == ScriptStyle.СтильПриложения)
                {
                    strFind = @"Процедура ПриСозданииФормы(_Форма) Экспорт";
                    strReplace = @"Процедура ПриСозданииФормы(_Форма) Экспорт" + Environment.NewLine + strAdd +
                        @"    // osdText = " + "\u0022" + osdText + "\u0022;";
                }
                else
                {
                    strFind = @"Процедура ПодготовкаКомпонентов()";
                    strReplace = @"Процедура ПодготовкаКомпонентов()" + Environment.NewLine + strAdd +
                        @"    // osdText = " + "\u0022" + osdText + "\u0022;";
                }

                if (!scriptText.Contains(strAdd))
                {
                    scriptText = scriptText.Replace(strFind, strReplace);
                }
                strFind = null;
                strReplace = null;
            }

            if (oldScriptText != null && !oldScriptText.Contains(@"Процедура ПриСозданииФормы(_Форма) Экспорт"))
            {
                if (!oldScriptText.Contains(@"Процедура ПодготовкаКомпонентов()"))
                {
                    oldScriptText = null;
                }
            }

            if (oldScriptText != null)
            {
                // Сделаем замены в исходном скрипте.
                // Заменим Процедура ПодготовкаКомпонентов() или Процедура ПриСозданииФормы(_Форма) Экспорт.
                if (OneScriptFormsDesigner.RootComponent.ScriptStyle == ScriptStyle.СтильПриложения) // стиль приложения
                {
                    if (oldScriptText.Contains(@"Процедура ПодготовкаКомпонентов()")) // если до этого был стиль скрипта
                    {
                        oldScriptText = oldScriptText.Replace(Environment.NewLine + @"ПодготовкаКомпонентов();", "");
                        oldScriptText = oldScriptText.Replace(Environment.NewLine + @"" + savedForm.NameObjectOneScriptForms + @".ЗапуститьОбработкуСобытий();" + Environment.NewLine, "");
                        oldScriptText = oldScriptText.Replace(Environment.NewLine + @"" + savedForm.NameObjectOneScriptForms + @".ЗапуститьОбработкуСобытий();", "");
                        strFind = @"Процедура ПодготовкаКомпонентов()" +
                            OneScriptFormsDesigner.ParseBetween(oldScriptText, @"Процедура ПодготовкаКомпонентов()", "КонецПроцедуры") +
                            @"КонецПроцедуры";
                        strReplace = @"Процедура ПриСозданииФормы(_Форма) Экспорт" +
                            OneScriptFormsDesigner.ParseBetween(scriptText, @"Процедура ПриСозданииФормы(_Форма) Экспорт", "КонецПроцедуры" + Environment.NewLine) +
                            @"КонецПроцедуры";
                    }
                    else // если до этого был стиль приложения
                    {
                        strFind = @"Процедура ПриСозданииФормы(_Форма) Экспорт" +
                            OneScriptFormsDesigner.ParseBetween(oldScriptText, @"Процедура ПриСозданииФормы(_Форма) Экспорт", "КонецПроцедуры") +
                            @"КонецПроцедуры";
                        strReplace = @"Процедура ПриСозданииФормы(_Форма) Экспорт" +
                            OneScriptFormsDesigner.ParseBetween(scriptText, @"Процедура ПриСозданииФормы(_Форма) Экспорт", "КонецПроцедуры") +
                            @"КонецПроцедуры";
                    }
                }
                else // стиль скрипта
                {
                    if (oldScriptText.Contains(@"Процедура ПодготовкаКомпонентов()")) // если до этого был стиль скрипта
                    {
                        strFind = @"Процедура ПодготовкаКомпонентов()" +
                            OneScriptFormsDesigner.ParseBetween(oldScriptText, @"Процедура ПодготовкаКомпонентов()", "КонецПроцедуры") +
                            @"КонецПроцедуры";
                        strReplace = @"Процедура ПодготовкаКомпонентов()" +
                            OneScriptFormsDesigner.ParseBetween(scriptText, @"Процедура ПодготовкаКомпонентов()", "КонецПроцедуры") +
                            @"КонецПроцедуры";
                    }
                    else // если до этого был стиль приложения
                    {
                        strFind = @"Процедура ПриСозданииФормы(_Форма) Экспорт" +
                            OneScriptFormsDesigner.ParseBetween(oldScriptText, @"Процедура ПриСозданииФормы(_Форма) Экспорт", "КонецПроцедуры") +
                            @"КонецПроцедуры";
                        strReplace = @"Процедура ПодготовкаКомпонентов()" +
                            OneScriptFormsDesigner.ParseBetween(scriptText, @"Процедура ПодготовкаКомпонентов()", "КонецПроцедуры") +
                            @"КонецПроцедуры";

                        // Нужно найти последнюю процедуру или последнюю функцию и после неё вставить строку:
                        // Environment.NewLine + Environment.NewLine + @"ПодготовкаКомпонентов();";

                        StringReader reader = new StringReader(oldScriptText);
                        string line;
                        string reverseText = "";
                        for (line = reader.ReadLine(); line != null; line = reader.ReadLine())
                        {
                            reverseText =  line + Environment.NewLine + reverseText;
                        }
                        reader = new StringReader(reverseText);
                        string directText = "";
                        bool notFound = true;
                        for (line = reader.ReadLine(); line != null; line = reader.ReadLine())
                        {
                            if (notFound)
                            {
                                if (line.Contains("КонецПроцедуры") || line.Contains("КонецФункции"))
                                {
                                    if (!reverseText.Contains(@"ПодготовкаКомпонентов();"))
                                    {
                                        line = line + Environment.NewLine + Environment.NewLine + @"ПодготовкаКомпонентов();";
                                    }
                                    notFound = false;
                                }
                            }
                            directText = line + Environment.NewLine + directText;
                        }
                        oldScriptText = directText;
                    }
                }
                oldScriptText = oldScriptText.Replace(strFind, strReplace);
                strFind = null;
                strReplace = null;

                // Если ни одной переменной не задано, значит нужно дописать сверу - "Перем " + savedForm.NameObjectOneScriptForms + ";".
                string str4 = OneScriptFormsDesigner.ParseBetween(oldScriptText, null, "Процедура");
                if (!str4.Contains("Перем "))
                {
                    oldScriptText = "Перем " + savedForm.NameObjectOneScriptForms + ";" + Environment.NewLine + oldScriptText;
                }

                // В коде выше процедуры подготовки компонентов располагается объявление переменных и обработчики событий.
                // Допишем туда новые переменные и новые обработчики событий.
                // Переменные располагаются в разделе объявления переменных до первой процедуры или функции.
                // Соберём все имена объявленных переменных исходного скрипта в массив ArrayOldPeremName.
                string oldPerem = OneScriptFormsDesigner.ParseBetween(oldScriptText, null, "Процедура");
                string oldPerem2 = OneScriptFormsDesigner.ParseBetween(oldPerem, null, "Функция");
                if (oldPerem2 != null)
                {
                    oldPerem = oldPerem2;
                }
                // Отсечем объявление переменных от комментариев и другого подобного кода
                // и получим имена переменных через точку с запятой. В этой строке (oldPeremName) будем искать имена новых переменных 
                // и в случае не нахождения добавлять их в код.
                ArrayList ArrayOldPerem = OneScriptFormsDesigner.StrFindBetween(oldPerem, "Перем", ";", false);
                string oldPeremName = ";";
                foreach (string item in ArrayOldPerem)
                {
                    string name = item.Replace(" ", "").Replace("Перем", "").Replace(",", ";").Trim();
                    oldPeremName = oldPeremName + name;
                }

                string newPerem = OneScriptFormsDesigner.ParseBetween(scriptText, null, "Процедура");
                string newPerem2 = OneScriptFormsDesigner.ParseBetween(newPerem, null, "Функция");
                if (newPerem2 != null)
                {
                    newPerem = newPerem2;
                }

                ArrayList ArrayListNewPerem = OneScriptFormsDesigner.StrFindBetween(newPerem, "Перем", ";", false);
                ArrayList ArrayListNewPerem2 = new ArrayList();
                for (int i = 0; i < ArrayListNewPerem.Count; i++)
                {
                    string strres = (string)ArrayListNewPerem[i];
                    string name = strres.Replace(@"Перем ", ";");
                    if (!oldPeremName.Contains(name))
                    {
                        ArrayListNewPerem2.Add(strres);
                    }
                }
                newPerem = "";
                for (int i = 0; i < ArrayListNewPerem2.Count; i++)
                {
                    newPerem = newPerem + ArrayListNewPerem2[i] + Environment.NewLine;
                }

                newPerem = oldPerem.TrimEnd() + Environment.NewLine + newPerem + Environment.NewLine;
                newScriptText = oldScriptText.Replace(oldPerem, newPerem);

                // Новые обработчики событий. Они располагаются по тексту всегда только выше
                // Процедура ПодготовкаКомпонентов() или выше
                // Процедура ПриСозданииФормы(_Форма) Экспорт
                string blokProc1 = OneScriptFormsDesigner.ParseBetween(scriptText, null, @"Процедура ПодготовкаКомпонентов()");
                string blokProc2 = OneScriptFormsDesigner.ParseBetween(scriptText, null, @"Процедура ПриСозданииФормы(_Форма) Экспорт");
                if (blokProc2 != null)
                {
                    blokProc1 = blokProc2;
                }
                string oldBlokProcFanc = "";
                string oldBlokProcFanc1 = OneScriptFormsDesigner.ParseBetween(newScriptText, null, @"Процедура ПодготовкаКомпонентов()");
                string oldBlokProcFanc2 = OneScriptFormsDesigner.ParseBetween(newScriptText, null, @"Процедура ПриСозданииФормы(_Форма) Экспорт");
                if (oldBlokProcFanc2 != null)
                {
                    oldBlokProcFanc = oldBlokProcFanc2;
                }
                else
                {
                    oldBlokProcFanc = oldBlokProcFanc1;
                }
                string oldBlokProcFanc3 = oldBlokProcFanc;
                oldBlokProcFanc3 = oldBlokProcFanc3.TrimEnd() + Environment.NewLine;

                ArrayList ArrayListNewProc = OneScriptFormsDesigner.StrFindBetween(blokProc1, "Процедура", "КонецПроцедуры", false);
                for (int i = 0; i < ArrayListNewProc.Count; i++)
                {
                    string strres = (string)ArrayListNewProc[i];
                    // Получим имя процедуры.
                    string nameProc = (string)OneScriptFormsDesigner.StrFindBetween(strres, "Процедура", "Экспорт", false)[0];

                    if (!oldBlokProcFanc.Contains(nameProc))
                    {
                        oldBlokProcFanc3 = oldBlokProcFanc3 + Environment.NewLine + strres + Environment.NewLine;
                    }
                }
                oldBlokProcFanc3 = oldBlokProcFanc3.TrimEnd() + Environment.NewLine + Environment.NewLine;
                newScriptText = newScriptText.Replace(oldBlokProcFanc, oldBlokProcFanc3);
            }
            else
            {
                newScriptText = scriptText;
            }

            return newScriptText;
        }

        private void _generateScript_Click(object sender, EventArgs e) // Сохранить сценарий
        {
            // Если в поле Путь указан файл .osd или оно пустое, тогда запускаем диалог выбора файла для сохранения
            // поле Путь заполнить именем файла с расширением os.

            string[] stringSeparators = new string[] { Environment.NewLine };
            Form savedForm = (Form)OneScriptFormsDesigner.DesignerHost.RootComponent;
            string oldScriptText = null;
            string path = savedForm.Path;
            bool startSaveDialog = false;
            if (path == null)
            {
                startSaveDialog = true;
            }
            else
            {
                if (path.Substring(path.Length - 4) == ".osd" || !File.Exists(path))	
                {
                    startSaveDialog = true;
                }
            }
            if (startSaveDialog)
            {
                // Запускаем диалог выбора файла для сохранения в .os
                System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog1.Title = "Сохранение (Свойство Путь не заполнено или указанный путь не существует)";	
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.Filter = "OS files(*.os)|*.os";

                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                savedForm.Path = saveFileDialog1.FileName;
                path = savedForm.Path;
            }
            else
            {
                oldScriptText = File.ReadAllText(path);
            }

            string strScript = GenerateScriptCode(oldScriptText, path);
            File.WriteAllText(savedForm.Path, strScript, Encoding.UTF8);

            // Запомним состояние дизайнера с этой формой.
            OneScriptFormsDesigner.DesignSurfaceState(true);
            OneScriptFormsDesigner.PropertyGridHost.PropertyGrid.Refresh();
        }

        private void _generateScriptAs_Click(object sender, EventArgs e) // Сохранить сценарий как
        {
            Form savedForm = (Form)OneScriptFormsDesigner.DesignerHost.RootComponent;

            // Запускаем диалог выбора файла для сохранения в .os
            string path = savedForm.Path;
            string oldScriptText = null;
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.Filter = "OS files(*.os)|*.os";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            savedForm.Path = saveFileDialog1.FileName;

            if (path != null)
            {
                try
                {
                    oldScriptText = File.ReadAllText(path);
                }
                catch { }
            }

            string strScript = GenerateScriptCode(oldScriptText, savedForm.Path);
            File.WriteAllText(savedForm.Path, strScript, Encoding.UTF8);

            // Запомним состояние дизайнера с этой формой.
            OneScriptFormsDesigner.DesignSurfaceState(true);
            OneScriptFormsDesigner.PropertyGridHost.PropertyGrid.Refresh();
        }

        private void _run_Click(object sender, EventArgs e) // Запуск
        {
            // Если для запускаемой формы свойство Путь не заполнено или там указан файл .osd тогда формируем скрипт и запускаем.
            Form savedForm = (Form)OneScriptFormsDesigner.DesignerHost.RootComponent;
            bool scriptMissing = false;
            if (savedForm.Path == null)
            {
                scriptMissing = true;
            }
            else
            {
                if (savedForm.Path.Substring(savedForm.Path.Length - 4) == ".osd")
                {
                    scriptMissing = true;
                }
            }
            if (scriptMissing)
            {
                OneScriptFormsDesigner.PropertyGrid.Refresh();
                string strScript = GenerateScriptCode(null, null);

                if (savedForm.ScriptStyle == ScriptStyle.СтильПриложения)
                {
                    // Найдем последнюю процедуру или последнюю функцию и после неё вставим строку:
                    //"ПодключитьВнешнююКомпоненту(" + "\u0022" + Settings.Default["dllPath"] + "\u0022" + @");" + Environment.NewLine +
                    //@"Ф = Новый ФормыДляОдноСкрипта();
                    //ПриСозданииФормы(Ф.Форма());

                    StringReader reader = new StringReader(strScript);
                    string line;
                    string reverseText = "";
                    for (line = reader.ReadLine(); line != null; line = reader.ReadLine())
                    {
                        reverseText = line + Environment.NewLine + reverseText;
                    }
                    reader = new StringReader(reverseText);
                    string directText = "";
                    bool notFound = true;
                    for (line = reader.ReadLine(); line != null; line = reader.ReadLine())
                    {
                        if (notFound)
                        {
                            if (line.Contains("КонецПроцедуры") || line.Contains("КонецФункции"))
                            {
                                line = line + Environment.NewLine + Environment.NewLine +
                                    @"ПодключитьВнешнююКомпоненту(" + "\u0022" + Settings.Default["dllPath"] + "\u0022" + @");" + Environment.NewLine +
                                    @"" + savedForm.NameObjectOneScriptForms + @" = Новый ФормыДляОдноСкрипта();" + Environment.NewLine +
                                    @"ПриСозданииФормы(" + savedForm.NameObjectOneScriptForms + @".Форма());";
                                notFound = false;
                            }
                        }
                        directText = line + Environment.NewLine + directText;
                    }
                    strScript = directText;
                    strScript = strScript + Environment.NewLine +
                    @"" + savedForm.NameObjectOneScriptForms + @".ЗапуститьОбработкуСобытий();" + Environment.NewLine;
                }

                string strTempFile = String.Format(Path.GetTempPath() + "oscript_{0}_{1}.os", DateTime.Now.ToString("yyyyMMddHHmmssfff"), Guid.NewGuid().ToString().Replace("-", ""));
                File.WriteAllText(strTempFile, strScript, Encoding.UTF8);

                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                psi.Arguments = strTempFile;
                psi.FileName = (string)Settings.Default["osPath"];
                System.Diagnostics.Process.Start(psi);
            }
            else // иначе там может быть указан только файл сценария .os и тогда формируем скрипт с учетом кода в этом файле.
            {
                string oldScriptText = File.ReadAllText(savedForm.Path);
                string strScript = GenerateScriptCode(oldScriptText, savedForm.Path);

                if (savedForm.ScriptStyle == ScriptStyle.СтильПриложения)
                {
                    // Найдем последнюю процедуру или последнюю функцию и после неё вставим строку:
                    //"ПодключитьВнешнююКомпоненту(" + "\u0022" + Settings.Default["dllPath"] + "\u0022" + @");" + Environment.NewLine +
                    //@"Ф = Новый ФормыДляОдноСкрипта();
                    //ПриСозданииФормы(Ф.Форма());

                    StringReader reader = new StringReader(strScript);
                    string line;
                    string reverseText = "";
                    for (line = reader.ReadLine(); line != null; line = reader.ReadLine())
                    {
                        reverseText = line + Environment.NewLine + reverseText;
                    }
                    reader = new StringReader(reverseText);
                    string directText = "";
                    bool notFound = true;
                    for (line = reader.ReadLine(); line != null; line = reader.ReadLine())
                    {
                        if (notFound)
                        {
                            if (line.Contains("КонецПроцедуры") || line.Contains("КонецФункции"))
                            {
                                line = line + Environment.NewLine + Environment.NewLine +
                                    @"ПодключитьВнешнююКомпоненту(" + "\u0022" + Settings.Default["dllPath"] + "\u0022" + @");" + Environment.NewLine +
                                    @"" + savedForm.NameObjectOneScriptForms + @" = Новый ФормыДляОдноСкрипта();" + Environment.NewLine +
                                    @"ПриСозданииФормы(" + savedForm.NameObjectOneScriptForms + @".Форма());";
                                notFound = false;
                            }
                        }
                        directText = line + Environment.NewLine + directText;
                    }
                    strScript = directText;
                    strScript = strScript + Environment.NewLine +
                    @"" + savedForm.NameObjectOneScriptForms + @".ЗапуститьОбработкуСобытий();" + Environment.NewLine;
                }

                // #Использовать ".\" 
                string directiva1 = savedForm.Path.Substring(0, savedForm.Path.LastIndexOf('\\') + 1);
                strScript = strScript.Replace("#Использовать \u0022.\\\u0022", "#Использовать \u0022" + directiva1 + "\u0022");

                string strTempFile = String.Format(Path.GetTempPath() + "oscript_{0}_{1}.os", DateTime.Now.ToString("yyyyMMddHHmmssfff"), Guid.NewGuid().ToString().Replace("-", ""));
                File.WriteAllText(strTempFile, strScript, Encoding.UTF8);

                string strTempFile2 = String.Format(Path.GetTempPath() + "oscript_{0}_{1}.os", DateTime.Now.ToString("yyyyMMddHHmmssfff"), Guid.NewGuid().ToString().Replace("-", ""));
                string strScript2 = @"
Проц = СоздатьПроцесс(""""""C:\Program Files\OneScript\bin\oscript.exe"""" """"" + strTempFile + @""""""",,Истина,Истина);" + @"
Проц.Запустить();
Проц.ОжидатьЗавершения();
ПотокВывода = Проц.ПотокВывода;
СтрПотокВывода = """";
Если ПотокВывода.ЕстьДанные Тогда
    Пока ПотокВывода.ЕстьДанные Цикл
        СтрПотокВывода = СтрПотокВывода + ПотокВывода.ПрочитатьСтроку();
    КонецЦикла;
Иначе
КонецЕсли;
Если СтрНайти(СтрПотокВывода, ""{"") > 0 Тогда
    Сообщить(СтрПотокВывода);
КонецЕсли;
";
                File.WriteAllText(strTempFile2, strScript2, Encoding.UTF8);
                string output;
                using (System.Diagnostics.Process process = new System.Diagnostics.Process())
                {
                    process.StartInfo.Arguments = strTempFile2;
                    process.StartInfo.FileName = (string)Settings.Default["osPath"];
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.Start();

                    StreamReader reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                }
                if (output.Contains("{"))
                {
                    MessageBox.Show(output);
                }
            }
        }

        private void _settings_Click(object sender, EventArgs e)
        {
            settingsForm = new MySettingsForm();

            if (settingsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Записываем значения в Settings.
                Settings.Default["osPath"] = settingsForm.OSPath;
                Settings.Default["dllPath"] = settingsForm.DLLPath;
                Settings.Default["visualSyleDesigner"] = settingsForm.SyleDesigner;
                Settings.Default["visualSyleForms"] = settingsForm.SyleForms;
                Settings.Default.Save();
            }
        }

        private void _exit_Click(object sender, EventArgs e)
        {
            //* 18.12.2021 perfolenta
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
            //***
        }

        private void _saveForm_Click(object sender, EventArgs e) // Сохранить форму
        {
            // Если в поле Путь указан файл .os или оно пустое, тогда запускаем диалог выбора файла для сохранения
            // поле Путь заполнить именем файла с расширением osd.

            string[] stringSeparators = new string[] { Environment.NewLine };
            Form savedForm = (Form)OneScriptFormsDesigner.DesignerHost.RootComponent;
            string path = savedForm.Path;
            bool startSaveDialog = false;
            if (path == null)
            {
                startSaveDialog = true;
            }
            else
            {
                if (path.Substring(path.Length - 3) == ".os")
                {
                    startSaveDialog = true;
                }
            }
            if (startSaveDialog)
            {
                // Запускаем диалог выбора файла для сохранения в .osd
                System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.Filter = "OSD files(*.osd)|*.osd";
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                savedForm.Path = saveFileDialog1.FileName;
                File.WriteAllText(saveFileDialog1.FileName, SaveForm.GetScriptText(saveFileDialog1.FileName), Encoding.UTF8);
            }
            else
            {
                File.WriteAllText(savedForm.Path, SaveForm.GetScriptText(savedForm.Path), Encoding.UTF8);
            }

            // Запомним состояние дизайнера с этой формой.
            OneScriptFormsDesigner.DesignSurfaceState(true);
            OneScriptFormsDesigner.PropertyGridHost.PropertyGrid.Refresh();
        }

        private void _saveFormAs_Click(object sender, EventArgs e) // Сохранить форму как
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.Filter = "OSD files(*.osd)|*.osd";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            ((Form)OneScriptFormsDesigner.DesignerHost.RootComponent).Path = saveFileDialog1.FileName;
            File.WriteAllText(saveFileDialog1.FileName, SaveForm.GetScriptText(saveFileDialog1.FileName), Encoding.UTF8);

            // Запомним состояние дизайнера с этой формой.
            OneScriptFormsDesigner.DesignSurfaceState(true);
            OneScriptFormsDesigner.PropertyGridHost.PropertyGrid.Refresh();
        }

        private void _loadForm_Click(object sender, EventArgs e) // Открыть форму
        {
            System.Windows.Forms.OpenFileDialog OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            OpenFileDialog1.RestoreDirectory = true;
            OpenFileDialog1.Filter = "OSD files(*.osd)|*.osd";
            if (OpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            LoadForm_Click(OpenFileDialog1.FileName, true, null);
        }

        public void LoadForm_Click(string fileName, bool choosingName, string sFile = null)
        {
            string strFile;
            if (sFile != null)
            {
                strFile = sFile;
            }
            else
            {
                strFile = File.ReadAllText(fileName);
            }

            string NameObjectOneScriptForms = OneScriptFormsDesigner.ParseBetween(strFile, ".ИмяОбъектаФормыДляОдноСкрипта = \u0022", "\u0022;");

            OneScriptFormsDesigner.block2 = true;

            string[] result = null;
            string[] stringSeparators = new string[] { Environment.NewLine };
            string ComponentBlok = null;
            string rootBlok = null;

            string strOSD = "";
            result = strFile.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < result.Length; i++)
            {
                // Для этих свойств пробелы в части строки после знака равно, не удаляем.
                string strres = result[i];
                if (strres.Contains(".ВыбранныйПуть = \u0022") || 
                    strres.Contains(".Заголовок = \u0022") || 
                    strres.Contains(".ИмяФайла = \u0022") || 
                    strres.Contains(".Маска = \u0022") || 
                    strres.Contains(".НачальныйКаталог = \u0022") || 
                    strres.Contains(".Описание = \u0022") || 
                    strres.Contains(".ПолныйПуть = \u0022") || 
                    strres.Contains(".ПользовательскийФормат = \u0022") || 
                    strres.Contains(".Путь = \u0022") || 
                    strres.Contains(".РазделительПути = \u0022") || 
                    strres.Contains(".Текст = \u0022") || 
                    strres.Contains(".ТекстЗаголовка = \u0022") || 
                    strres.Contains(".ТекстПодсказки = \u0022") || 
                    strres.Contains(".Фильтр = \u0022")
                    )
                {
                    string strBefore = OneScriptFormsDesigner.ParseBetween(strres, null, " = \u0022");
                    string strAfter = OneScriptFormsDesigner.ParseBetween(strres, "= \u0022", null);
                    strOSD = strOSD + strBefore.Replace(" ", "") + "=\u0022" + strAfter + Environment.NewLine;
                }
                else if (strres.Contains(".УстановитьПодсказку("))
                {
                    string strBefore = OneScriptFormsDesigner.ParseBetween(strres, null, ", \u0022");
                    string strAfter = OneScriptFormsDesigner.ParseBetween(strres, ", \u0022", null);
                    strOSD = strOSD + strBefore.Replace(" ", "") + ", \u0022" + strAfter + Environment.NewLine;
                }
                else if (strres.Contains(".Элементы.Добавить(" + NameObjectOneScriptForms + @".ЭлементСписка("))
                {
                    strOSD = strOSD + strres.Replace("\u0022, ", "\u0022,") + Environment.NewLine;
                }
                else
                {
                    strOSD = strOSD + strres.Replace(" ", "") + Environment.NewLine;
                }
            }
            result = null;

            strOSD = strOSD.Trim();

            // Соберем из блока конструкторов имена компонентов в CompNames.
            List<string> CompNames = new List<string>();
            Dictionary<string, object> dictObjects = new Dictionary<string, object>(); // Словарь для соответствия имени переменной в скрипте объекту в библиотеке.
            string ConstructorBlok = OneScriptFormsDesigner.ParseBetween(strOSD, @"[<Конструкторы]", @"[Конструкторы>]");
            result = ConstructorBlok.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < result.Length; i++)
            {
                string s = OneScriptFormsDesigner.ParseBetween(result[i], null, @"=" + NameObjectOneScriptForms + @".");
                if (s != null)
                {
                    if (s.Substring(0, 2) != @"//")
                    {
                        CompNames.Add(s);
                        dictObjects.Add(s, null);
                    }
                }
            }
            result = null;

            // Добавим вкладку и создадим на ней загружаемую форму.
            DesignSurfaceExt2 var1 = null;
            if (choosingName)
            {
                var1 = IpDesignerCore.AddDesignSurface<Form>(640, 480, AlignmentModeEnum.SnapLines, new Size(1, 1), CompNames[0]);
            }
            else
            {
                var1 = IpDesignerCore.AddDesignSurface<Form>(640, 480, AlignmentModeEnum.SnapLines, new Size(1, 1), "");
            }
            Component rootComponent = (Component)var1.ComponentContainer.Components[0];
            ((Form)rootComponent).Path = fileName;	

            dictObjects[CompNames[0]] = rootComponent;

            string formName = CompNames[0];
            rootComponent.GetType().GetProperty("Text").SetValue(rootComponent, formName);
            rootBlok = OneScriptFormsDesigner.ParseBetween(strOSD, @"[<" + formName + @"]", @"[" + formName + @">]");
            if (rootBlok != null)
            {
                // Установим для формы свойства.
                result = rootBlok.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                for (int i1 = 0; i1 < result.Length; i1++)
                {
                    string strCurrent = result[i1];
                    if (strCurrent.Length >= 2)
                    {
                        if (strCurrent.Substring(0, 2) != @"//")
                        {
                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, formName + ".", "=");
                            if (displayName != "КнопкаОтмена" && displayName != "КнопкаПринять" && !strCurrent.StartsWith("Подсказка"))
                            {
                                string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                PropValueConverter.SetPropValue(rootComponent, displayName, strPropertyValue, NameObjectOneScriptForms);
                            }
                        }
                    }
                }
                propertyGrid1.Refresh();
                result = null;
            }

            // Создадим остальные компоненты но пока не устанавливаем для них свойства, так как могут быть не все родители созданы.
            IDesignSurfaceExt surface = OneScriptFormsDesigner.ActiveDesignSurface;
            for (int i = 1; i < CompNames.Count; i++)
            {
                string componentName = CompNames[i];
                string type_NameRu = componentName;
                for (int i1 = 0; i1 < 10; i1++)
                {
                    type_NameRu = type_NameRu.Replace(i1.ToString(), "");
                }

                string type_NameEn = "osfDesigner." + OneScriptFormsDesigner.namesRuEn[type_NameRu];
                Type type = Type.GetType(type_NameEn);

                if (type == typeof(osfDesigner.ImageList))
                {
                    ToolboxItem toolImageList1 = new ToolboxItem(typeof(System.Windows.Forms.ImageList));
                    Component comp1 = (Component)toolImageList1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                    // Для comp1 уже создан дублер, получим его.
                    osfDesigner.ImageList SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(comp1);
                    dictObjects[componentName] = SimilarObj;

                    SimilarObj.DefaultValues = @"ГлубинаЦвета == Глубина8
Изображения == (Коллекция)
РазмерИзображения == {Ширина=16, Высота=16}
(Name) == " + comp1.Site.Name + Environment.NewLine;
                }
                else if (type == typeof(osfDesigner.MainMenu))
                {
                    ToolboxItem toolMainMenu1 = new ToolboxItem(typeof(System.Windows.Forms.MainMenu));
                    Component comp1 = (Component)toolMainMenu1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                    // Для comp1 уже создан дублер, получим его.
                    osfDesigner.MainMenu SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(comp1);
                    dictObjects[componentName] = SimilarObj;

                    SimilarObj.FrmMenuItems = new frmMenuItems(SimilarObj);
                }
                else if (type == typeof(osfDesigner.TabPage))
                {
                    ToolboxItem toolTabPage1 = new ToolboxItem(typeof(System.Windows.Forms.TabPage));
                    Component comp1 = (Component)toolTabPage1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                    // Для comp1 уже создан дублер, получим его.
                    osfDesigner.TabPage SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(comp1);
                    SimilarObj.OriginalObj = (System.Windows.Forms.TabPage)comp1;
                    OneScriptFormsDesigner.PassProperties(comp1, SimilarObj); // Передадим свойства.
                    dictObjects[componentName] = SimilarObj;

                    OneScriptFormsDesigner.PropertyGrid.SelectedObject = SimilarObj;
                    SimilarObj.DefaultValues = OneScriptFormsDesigner.GetDefaultValues(SimilarObj, OneScriptFormsDesigner.PropertyGrid);
                }
                else if (type == typeof(osfDesigner.TabControl))
                {
                    ToolboxItem toolTabControl1 = new ToolboxItem(typeof(osfDesigner.TabControl));
                    Component comp1 = (Component)toolTabControl1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                    // Удалим две вкладки, которые дизайнер для панели вкладок создает автоматически.
                    IDesignerEventService des = (IDesignerEventService)pDesigner.DSME.GetService(typeof(IDesignerEventService));
                    if (des != null)
                    {
                        for (int i1 = 0; i1 < ((osfDesigner.TabControl)comp1).TabPages.Count; i1++)
                        {
                            des.ActiveDesigner.Container.Remove(((osfDesigner.TabControl)comp1).TabPages[i1]);
                        }
                        ((osfDesigner.TabControl)comp1).TabPages.Clear();
                    }
                    dictObjects[componentName] = comp1;
                }

                else if (type == typeof(osfDesigner.FileSystemWatcher) ||
                    type == typeof(osfDesigner.FolderBrowserDialog) ||
                    type == typeof(osfDesigner.ColorDialog) ||
                    type == typeof(osfDesigner.FontDialog) ||
                    type == typeof(osfDesigner.OpenFileDialog) ||
                    type == typeof(osfDesigner.SaveFileDialog) ||
                    type == typeof(osfDesigner.NotifyIcon) ||
                    type == typeof(osfDesigner.ToolTip) ||
                    type == typeof(osfDesigner.Timer))
                {
                    ToolboxItem toolComp1 = new ToolboxItem(type);
                    Component comp1 = (Component)toolComp1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                    dictObjects[componentName] = comp1;
                }
                else
                {
                    Component control1 = surface.CreateControl(type, new Size(200, 20), new Point(10, 200));
                    dictObjects[componentName] = control1;
                }
                ((Component)dictObjects[componentName]).Site.Name = componentName;
            }

            // Установим свойства компонентов.
            for (int i = 1; i < CompNames.Count; i++)
            {
                string componentName = CompNames[i];
                Component control = (Component)dictObjects[componentName];
                ComponentBlok = OneScriptFormsDesigner.ParseBetween(strOSD, @"[<" + componentName + @"]", @"[" + componentName + @">]");
                if (ComponentBlok != null)
                {
                    result = ComponentBlok.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    for (int i1 = 0; i1 < result.Length; i1++)
                    {
                        string strCurrent = result[i1];
                        if (strCurrent.Length >= 2)
                        {
                            if (strCurrent.Substring(0, 2) != @"//")
                            {
                                if (componentName.Contains("СписокИзображений"))
                                {
                                    if (strCurrent.Contains("="))
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, NameObjectOneScriptForms, null);
                                    }
                                    else
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", ".");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "" + NameObjectOneScriptForms + @".Картинка(" + "\u0022", "\u0022)");
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, NameObjectOneScriptForms, null);
                                    }
                                }
                                else if (componentName.Contains("Календарь"))
                                {
                                    if (strCurrent.Contains("="))
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                        Control parent = (Control)dictObjects[parentName];
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, NameObjectOneScriptForms, parent);
                                    }
                                    else
                                    {
                                        if (strCurrent.Contains("УстановитьПодсказку"))
                                        {
                                            string displayName = "ToolTip на " + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".УстановитьПодсказку");
                                            string strPropertyValue = strCurrent;
                                            PropValueConverter.SetPropValue(control, displayName, strPropertyValue, NameObjectOneScriptForms, null);
                                        }
                                        else
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", ".");
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "Дата(", "))");
                                            string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                            Control parent = (Control)dictObjects[parentName];
                                            PropValueConverter.SetPropValue(control, displayName, strPropertyValue, NameObjectOneScriptForms, parent);
                                        }
                                    }
                                }
                                else if (componentName.Contains("ГлавноеМеню"))
                                {
                                    string controlName = ((osfDesigner.MainMenu)control).Name;
                                    if (strCurrent.Contains(".ЭлементыМеню.Добавить(" + NameObjectOneScriptForms + @".ЭлементМеню("))
                                    {
                                        osfDesigner.MainMenu MainMenu1 = (osfDesigner.MainMenu)control;
                                        System.Windows.Forms.TreeView TreeView1 = MainMenu1.TreeView;
                                        string Text = OneScriptFormsDesigner.ParseBetween(strCurrent, "(\u0022", "\u0022)");
                                        string Name = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                        if (strCurrent.Contains(componentName + ".")) // Создаем элемент главного меню.
                                        {
                                            if (!Name.Contains("Сепаратор"))
                                            {
                                                MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                                                dictObjects.Add(componentName + Name, MenuItemEntry1);
                                                MenuItemEntry1.Name = Name;
                                                MenuItemEntry1.Text = Text;
                                                MenuItemEntry1.DefaultValues = @"Доступность == Истина
Нажатие == 
Отображать == Истина
Переключатель == Ложь
Помечен == Ложь
ПорядокСлияния == 0
СочетаниеКлавиш == Отсутствие
Текст == " + Name + Environment.NewLine + 
@"ТипСлияния == Добавить
(Name) == " + Name + Environment.NewLine;
                                                MainMenu1.MenuItems.Add(MenuItemEntry1.M_MenuItem);
                                                OneScriptFormsDesigner.AddToDictionary(MenuItemEntry1.M_MenuItem, MenuItemEntry1);
                                                TreeNode TreeNode1 = new TreeNode();
                                                TreeNode1.Tag = MenuItemEntry1;
                                                TreeNode1.Text = MenuItemEntry1.Text;
                                                TreeView1.Nodes.Add(TreeNode1);
                                                TreeView1.SelectedNode = TreeNode1;
                                            }
                                            else
                                            {
                                                MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                                                dictObjects.Add(componentName + Name, MenuItemEntry1);
                                                MenuItemEntry1.Name = Name;
                                                MenuItemEntry1.Text = Text;
                                                MenuItemEntry1.DefaultValues = @"Доступность == Истина
Нажатие == 
Отображать == Истина
Переключатель == Ложь
Помечен == Ложь
ПорядокСлияния == 0
СочетаниеКлавиш == Отсутствие
Текст == " + Name + Environment.NewLine +
@"ТипСлияния == Добавить
(Name) == " + Name + Environment.NewLine;
                                                MainMenu1.MenuItems.Add(MenuItemEntry1.M_MenuItem);
                                                OneScriptFormsDesigner.AddToDictionary(MenuItemEntry1.M_MenuItem, MenuItemEntry1);

                                                TreeNode TreeNode1 = new TreeNode();
                                                TreeNode1.Tag = MenuItemEntry1;
                                                TreeNode1.Text = MenuItemEntry1.Text;
                                                TreeView1.Nodes.Add(TreeNode1);
                                                TreeView1.SelectedNode = TreeNode1;

                                                // Свойство Checked у родителя нужно установить в false.
                                                if (MenuItemEntry1.Parent.GetType() != typeof(osfDesigner.MainMenu))
                                                {
                                                    ((MenuItem)MenuItemEntry1.Parent).Checked = false;
                                                }
                                                TreeView1.SelectedNode.Expand();
                                            }
                                        }
                                        else // Создаем элемент подменю.
                                        {
                                            if (!Name.Contains("Сепаратор"))
                                            {
                                                MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                                                dictObjects.Add(componentName + Name, MenuItemEntry1);
                                                MenuItemEntry1.Name = Name;
                                                MenuItemEntry1.Text = Text;
                                                MenuItemEntry1.DefaultValues = @"Доступность == Истина
Нажатие == 
Отображать == Истина
Переключатель == Ложь
Помечен == Ложь
ПорядокСлияния == 0
СочетаниеКлавиш == Отсутствие
Текст == " + Name + Environment.NewLine +
@"ТипСлияния == Добавить
(Name) == " + Name + Environment.NewLine;

                                                string nameNodeParent = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ".");
                                                TreeNode SelectedNode = null;
                                                SelectedNodeSearch(TreeView1, nameNodeParent, ref SelectedNode, null);
                                                TreeView1.SelectedNode = SelectedNode;

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
                                                TreeView1.SelectedNode = TreeNode1;
                                            }
                                            else
                                            {
                                                MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                                                dictObjects.Add(componentName + Name, MenuItemEntry1);
                                                MenuItemEntry1.Name = Name;
                                                MenuItemEntry1.Text = Text;
                                                MenuItemEntry1.DefaultValues = @"Доступность == Истина
Нажатие == 
Отображать == Истина
Переключатель == Ложь
Помечен == Ложь
ПорядокСлияния == 0
СочетаниеКлавиш == Отсутствие
Текст == " + Name + Environment.NewLine +
@"ТипСлияния == Добавить
(Name) == " + Name + Environment.NewLine;
                                                string nameNodeParent = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ".");

                                                TreeNode SelectedNode = null;
                                                SelectedNodeSearch(TreeView1, nameNodeParent, ref SelectedNode, null);
                                                TreeView1.SelectedNode = SelectedNode;

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
                                            }
                                        }
                                    }
                                    else // Обрабатываем как свойство элемента меню.
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                        string nameObj = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                        object control2 = (object)dictObjects[nameObj];
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, NameObjectOneScriptForms, null);
                                    }
                                }
                                else if (control.GetType() == typeof(osfDesigner.FileSystemWatcher) ||
                                    control.GetType() == typeof(osfDesigner.FolderBrowserDialog) ||
                                    control.GetType() == typeof(osfDesigner.ColorDialog) ||
                                    control.GetType() == typeof(osfDesigner.FontDialog) ||
                                    control.GetType() == typeof(osfDesigner.OpenFileDialog) ||
                                    control.GetType() == typeof(osfDesigner.SaveFileDialog) ||
                                    control.GetType() == typeof(osfDesigner.NotifyIcon) ||
                                    control.GetType() == typeof(osfDesigner.ToolTip) ||
                                    control.GetType() == typeof(osfDesigner.Timer))
                                {
                                    string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                    string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=");
                                    if (strPropertyValue.Substring(strPropertyValue.Length - 2) == "\u0022;")
                                    {
                                        strPropertyValue = strPropertyValue.Remove(strPropertyValue.Length - 2);
                                    }
                                    else
                                    {
                                        strPropertyValue = strPropertyValue.Remove(strPropertyValue.Length - 1);
                                    }
                                    PropValueConverter.SetPropValue(control, displayName, strPropertyValue, NameObjectOneScriptForms, null);
                                }
                                else if (strCurrent.StartsWith("Подсказка"))
                                {
                                    string displayName = "ToolTip на " + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".УстановитьПодсказку");
                                    string strPropertyValue = strCurrent;
                                    PropValueConverter.SetPropValue(control, displayName, strPropertyValue, NameObjectOneScriptForms, null);
                                }
                                else if (componentName.Contains("Вкладка"))
                                {
                                    string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                    string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                    string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                    Control parent = (Control)dictObjects[parentName];
                                    PropValueConverter.SetPropValue(control, displayName, strPropertyValue, NameObjectOneScriptForms, parent);
                                }
                                else if (componentName.Contains("СтрокаСостояния"))
                                {
                                    string controlName = ((osfDesigner.StatusBar)control).Name;
                                    string header = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                    if (header.Contains("СтрокаСостояния") && !header.Contains("Панель"))
                                    {
                                        if (strCurrent.Contains(".Панели.Добавить(")) // Добавляем панель.
                                        {
                                            string nameItem = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                            StatusBarPanel StatusBarPanel1 = (StatusBarPanel)dictObjects[nameItem];
                                            ((osfDesigner.StatusBar)control).Panels.Add(StatusBarPanel1);
                                        }
                                        else // Обрабатываем как свойство строки состояния.
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                            Control parent = (Control)dictObjects[parentName];
                                            PropValueConverter.SetPropValue(control, displayName, strPropertyValue, NameObjectOneScriptForms, parent);
                                        }
                                    }
                                    else 
                                    {
                                        if (strCurrent.Contains("" + NameObjectOneScriptForms + @".ПанельСтрокиСостояния();")) // Создаем панель.
                                        {
                                            StatusBarPanel StatusBarPanel1 = new StatusBarPanel();
                                            string nameItem = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            StatusBarPanel1.Name = nameItem;
                                            dictObjects.Add(controlName + nameItem, StatusBarPanel1);

                                            StatusBarPanel1.DefaultValues = @"АвтоРазмер == Отсутствие
Значок == 
СтильГраницы == Утопленная
Текст == 
Ширина == 100
МинимальнаяШирина == 10
(Name) == 
";
                                        }
                                        else // Обрабатываем как свойство панели строки состояния.
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                            string nameObj = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                            object control2 = dictObjects[nameObj];
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, NameObjectOneScriptForms, null);
                                        }
                                    }
                                }
                                else if (componentName.Contains("ПанельИнструментов"))
                                {
                                    string controlName = ((osfDesigner.ToolBar)control).Name;
                                    string header = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                    if (strCurrent.Contains(".Кнопки.Добавить(")) // Добавляем кнопку панели инструментов.
                                    {
                                        System.Windows.Forms.ToolBarButton OriginalObj = new System.Windows.Forms.ToolBarButton();
                                        osfDesigner.ToolBarButton SimilarObj = new osfDesigner.ToolBarButton();
                                        OneScriptFormsDesigner.PassProperties(OriginalObj, SimilarObj); // Передадим свойства.
                                        SimilarObj.OriginalObj = OriginalObj;
                                        SimilarObj.Parent = OriginalObj.Parent;
                                        SimilarObj.Style = (osfDesigner.ToolBarButtonStyle)OriginalObj.Style;
                                        OriginalObj.Tag = SimilarObj;

                                        string nameItem = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                        SimilarObj.Name = nameItem;
                                        ((osfDesigner.ToolBar)control).Buttons.Add(OriginalObj);
                                        dictObjects.Add(controlName + nameItem, SimilarObj);

                                        SimilarObj.DefaultValues = @"Доступность == Истина
ИндексИзображения == -1
Нажата == Ложь
НейтральноеПоложение == Ложь
Отображать == Истина
Прямоугольник == 
Стиль == СтандартнаяТрехмерная
Текст == 
ТекстПодсказки == 
(Name) == 
";
                                    }
                                    else if (!header.Contains("Кн")) // Обрабатываем как свойство панели инструментов.
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                        Control parent = (Control)dictObjects[parentName];
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, NameObjectOneScriptForms, parent);
                                    }
                                    else // Обрабатываем как свойство кнопки панели инструментов.
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                        string nameObj = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                        object control2 = dictObjects[nameObj];
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, NameObjectOneScriptForms, null);
                                    }
                                }
                                else if (componentName.Contains("СписокЭлементов"))
                                {
                                    string controlName = ((osfDesigner.ListView)control).Name;
                                    string header = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                    string fragment1 = OneScriptFormsDesigner.ParseBetween(header, "СписокЭлементов", null);
                                    fragment1 = fragment1.Replace("Колонка", "флажок").Replace("Элемент", "флажок").Replace("Подэлемент", "флажок");
                                    if (header.Contains("СписокЭлементов") && !fragment1.Contains("флажок")) // Обрабатываем как свойство списка элементов.
                                    {
                                        if (strCurrent.Contains(".Элементы.Добавить(")) // Добавляем элемент списка элементов.
                                        {
                                            string nameItem = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                            ListViewItem ListViewItem1 = (ListViewItem)dictObjects[nameItem];
                                            ((osfDesigner.ListView)control).Items.Add(ListViewItem1);
                                        }
                                        else if (strCurrent.Contains(".Колонки.Добавить(")) // Добавляем колонку списка элементов.
                                        {
                                            string nameItem = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                            ColumnHeader ColumnHeader1 = (ColumnHeader)dictObjects[nameItem];
                                            ((osfDesigner.ListView)control).Columns.Add(ColumnHeader1);
                                        }
                                        else // Обрабатываем как свойство для СписокЭлементов.
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                            Control parent = (Control)dictObjects[parentName];
                                            PropValueConverter.SetPropValue(control, displayName, strPropertyValue, NameObjectOneScriptForms, parent);
                                        }
                                    }
                                    else
                                    {
                                        if (strCurrent.Contains("" + NameObjectOneScriptForms + @".ЭлементСпискаЭлементов();")) // Создаем элемент списка элементов.
                                        {
                                            ListViewItem ListViewItem1 = new ListViewItem();
                                            string nameItem = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            ListViewItem1.Name = nameItem;
                                            dictObjects.Add(controlName + nameItem, ListViewItem1);

                                            ListViewItem1.DefaultValues = @"ИспользоватьСтильДляПодэлементов == Истина
ОсновнойЦвет == ТекстОкна
Помечен == Ложь
Текст == 
ЦветФона == Окно
Шрифт == Microsoft Sans Serif; 7,8pt
Подэлементы == (Коллекция)
ИндексИзображения == -1
(Name) == 
";
                                        }
                                        else if (strCurrent.Contains("" + NameObjectOneScriptForms + @".ПодэлементСпискаЭлементов();")) // Создаем подэлемент списка элементов.
                                        {
                                            ListViewSubItem ListViewSubItem1 = new ListViewSubItem();
                                            string nameItem = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            ListViewSubItem1.Name = nameItem;
                                            dictObjects.Add(controlName + nameItem, ListViewSubItem1);

                                            ListViewSubItem1.DefaultValues = @"ОсновнойЦвет == ТекстОкна
Текст == 
ЦветФона == Окно
Шрифт == Microsoft Sans Serif; 7,8pt
(Name) == 
";
                                        }
                                        else if (strCurrent.Contains("" + NameObjectOneScriptForms + @".Колонка();")) // Создаем колонку списка элементов.
                                        {
                                            ColumnHeader ColumnHeader1 = new ColumnHeader();
                                            string nameItem = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            ColumnHeader1.Name = nameItem;
                                            dictObjects.Add(controlName + nameItem, ColumnHeader1);

                                            ColumnHeader1.DefaultValues = @"ВыравниваниеТекста == Лево
Текст == 
ТипСортировки == Текст
Ширина == 60
(Name) == 
";
                                        }
                                        else if (strCurrent.Contains(".Подэлементы.Добавить(")) // Добавляем подэлемент списка элементов.
                                        {
                                            string nameItem = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                            ListViewItem ListViewItem1 = (osfDesigner.ListViewItem)dictObjects[nameItem];
                                            string nameSubItem = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                            ListViewSubItem ListViewSubItem1 = (osfDesigner.ListViewSubItem)dictObjects[nameSubItem];
                                            ListViewItem1.SubItems.Add(ListViewSubItem1);
                                        }
                                        else // Обрабатываем как свойство для элемента или подэлемента СписокЭлементов.
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                            string nameObj = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                            object control2 = dictObjects[nameObj];
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, NameObjectOneScriptForms, null);
                                        }
                                    }
                                }
                                else if (componentName.Contains("ПолеСписка"))
                                {
                                    if (strCurrent.Contains(".Элементы.Добавить(" + NameObjectOneScriptForms + @".ЭлементСписка(")) // Добавляем элемент поля списка.
                                    {
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "(" + NameObjectOneScriptForms + @".ЭлементСписка", ");");
                                        string itemText = OneScriptFormsDesigner.ParseBetween(strPropertyValue, "(", ",").Replace("\u0022", "");
                                        string itemValue = OneScriptFormsDesigner.ParseBetween(strPropertyValue, ",", ")");

                                        osfDesigner.ListItemListBox ListItemListBox1 = new ListItemListBox();
                                        ListItemListBox1.Text = itemText;

                                        if (itemValue.StartsWith("\u0022") && itemValue.EndsWith("\u0022")) // Тип Строка.
                                        {
                                            itemValue = itemValue.Replace("\u0022", "");
                                            ListItemListBox1.Value = itemValue;
                                            ListItemListBox1.ValueType = DataType.Строка;
                                        }
                                        else if (strPropertyValue.Contains("Ложь") || strPropertyValue.Contains("Истина")) // Тип Булево.
                                        {
                                            ListItemListBox1.Value = true;
                                            ListItemListBox1.ValueType = DataType.Булево;
                                            if (itemValue == "Ложь")
                                            {
                                                ListItemListBox1.Value = false;
                                            }
                                        }
                                        else if (strPropertyValue.Contains("Дата")) // Тип Дата.
                                        {
                                            DateTime rez1 = new DateTime();
                                            string[] result1 = OneScriptFormsDesigner.ParseBetween(strPropertyValue, "Дата(", "))").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                            for (int i2 = 0; i2 < result1.Length; i2++)
                                            {
                                                if (i2 == 0)
                                                {
                                                    rez1 = rez1.AddYears(Int32.Parse(result1[0]) - 1);
                                                }
                                                if (i2 == 1)
                                                {
                                                    rez1 = rez1.AddMonths(Int32.Parse(result1[1]) - 1);
                                                }
                                                if (i2 == 2)
                                                {
                                                    rez1 = rez1.AddDays(Int32.Parse(result1[2]) - 1);
                                                }
                                                if (i2 == 3)
                                                {
                                                    rez1 = rez1.AddHours(Int32.Parse(result1[3]));
                                                }
                                                if (i2 == 4)
                                                {
                                                    rez1 = rez1.AddMinutes(Int32.Parse(result1[4]));
                                                }
                                                if (i2 == 5)
                                                {
                                                    rez1 = rez1.AddSeconds(Int32.Parse(result1[5]));
                                                }
                                            }
                                            ListItemListBox1.Value = rez1;
                                            ListItemListBox1.ValueType = DataType.Дата;
                                        }
                                        else // Тип Число.
                                        {
                                            itemValue = itemValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                                            ListItemListBox1.Value = Decimal.Parse(itemValue);
                                            ListItemListBox1.ValueType = DataType.Число;
                                        }
                                        ((ListBox)control).Items.Add(ListItemListBox1);
                                    }
                                    else // Обрабатываем как свойство поля списка.
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                        Control parent = (Control)dictObjects[parentName];
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, NameObjectOneScriptForms, parent);
                                    }
                                }
                                else if (componentName.Contains("ПолеВыбора"))
                                {
                                    if (strCurrent.Contains(".Элементы.Добавить(" + NameObjectOneScriptForms + @".ЭлементСписка(")) // Добавляем элемент поля выбора.
                                    {
                                        // Определяем тип элемента списка и создаем его.
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "(" + NameObjectOneScriptForms + @".ЭлементСписка", ");");
                                        string itemText = OneScriptFormsDesigner.ParseBetween(strPropertyValue, "(", ",").Replace("\u0022", "");
                                        string itemValue = OneScriptFormsDesigner.ParseBetween(strPropertyValue, ",", ")");

                                        osfDesigner.ListItemComboBox ListItemComboBox1 = new ListItemComboBox();
                                        ListItemComboBox1.Text = itemText;

                                        if (itemValue.StartsWith("\u0022") && itemValue.EndsWith("\u0022")) // Тип Строка.
                                        {
                                            itemValue = itemValue.Replace("\u0022", "");
                                            ListItemComboBox1.Value = itemValue;
                                            ListItemComboBox1.ValueType = DataType.Строка;
                                        }
                                        else if (strPropertyValue.Contains("Ложь") || strPropertyValue.Contains("Истина")) // Тип Булево.
                                        {
                                            ListItemComboBox1.Value = true;
                                            ListItemComboBox1.ValueType = DataType.Булево;
                                            if (itemValue == "Ложь")
                                            {
                                                ListItemComboBox1.Value = false;
                                            }
                                        }
                                        else if (strPropertyValue.Contains("Дата")) // Тип Дата.
                                        {
                                            DateTime rez1 = new DateTime();
                                            string[] result1 = OneScriptFormsDesigner.ParseBetween(strPropertyValue, "Дата(", "))").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                            for (int i2 = 0; i2 < result1.Length; i2++)
                                            {
                                                if (i2 == 0)
                                                {
                                                    rez1 = rez1.AddYears(Int32.Parse(result1[0]) - 1);
                                                }
                                                if (i2 == 1)
                                                {
                                                    rez1 = rez1.AddMonths(Int32.Parse(result1[1]) - 1);
                                                }
                                                if (i2 == 2)
                                                {
                                                    rez1 = rez1.AddDays(Int32.Parse(result1[2]) - 1);
                                                }
                                                if (i2 == 3)
                                                {
                                                    rez1 = rez1.AddHours(Int32.Parse(result1[3]));
                                                }
                                                if (i2 == 4)
                                                {
                                                    rez1 = rez1.AddMinutes(Int32.Parse(result1[4]));
                                                }
                                                if (i2 == 5)
                                                {
                                                    rez1 = rez1.AddSeconds(Int32.Parse(result1[5]));
                                                }
                                            }
                                            ListItemComboBox1.Value = rez1;
                                            ListItemComboBox1.ValueType = DataType.Дата;
                                        }
                                        else // Это тип Число.
                                        {
                                            itemValue = itemValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                                            ListItemComboBox1.Value = Decimal.Parse(itemValue);
                                            ListItemComboBox1.ValueType = DataType.Число;
                                        }
                                        ((ComboBox)control).Items.Add(ListItemComboBox1);
                                    }
                                    else // Обрабатываем как свойство поля выбора.
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                        Control parent = (Control)dictObjects[parentName];
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, NameObjectOneScriptForms, parent);
                                    }
                                }
                                else if (componentName.Contains("СеткаДанных"))
                                {
                                    string controlName = ((osfDesigner.DataGrid)control).Name;
                                    string header = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                    if (header.Contains("СеткаДанных") && !header.Contains("Стиль")) // Обрабатываем как свойство сетки данных.
                                    {
                                        if (!strCurrent.Contains(".СтилиТаблицы.Добавить("))
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                            Control parent = (Control)dictObjects[parentName];
                                            PropValueConverter.SetPropValue(control, displayName, strPropertyValue, NameObjectOneScriptForms, parent);
                                        }
                                    }
                                    else
                                    {
                                        if (strCurrent.Contains("" + NameObjectOneScriptForms + @".СтильТаблицыСеткиДанных();"))
                                        {
                                            osfDesigner.DataGridTableStyle SimilarObj = new osfDesigner.DataGridTableStyle();
                                            System.Windows.Forms.DataGridTableStyle OriginalObj = new System.Windows.Forms.DataGridTableStyle();
                                            SimilarObj.OriginalObj = OriginalObj;
                                            OneScriptFormsDesigner.AddToDictionary(OriginalObj, SimilarObj);
                                            OneScriptFormsDesigner.PassProperties(OriginalObj, SimilarObj); // Передадим свойства.
                                            string nameStyle = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            SimilarObj.NameStyle = nameStyle;
                                            ((osfDesigner.DataGrid)control).TableStyles.Add(OriginalObj);
                                            dictObjects.Add(controlName + nameStyle, SimilarObj);

                                            SimilarObj.DefaultValues = @"ШрифтЗаголовков == Microsoft Sans Serif; 7,8pt
ПредпочтительнаяВысотаСтрок == 18
ПредпочтительнаяШиринаСтолбцов == 75
ШиринаЗаголовковСтрок == 35
РазрешитьСортировку == Истина
ОтображатьЗаголовкиСтолбцов == Истина
ОтображатьЗаголовкиСтрок == Истина
ИмяОтображаемого == 
СтилиКолонкиСеткиДанных == (Коллекция)
Текст == 
ТолькоЧтение == Ложь
ОсновнойЦвет == ТекстОкна
ОсновнойЦветЗаголовков == ТекстЭлемента
ЦветСетки == ЛицеваяЭлемента
ЦветФона == Окно
ЦветФонаЗаголовков == ЛицеваяЭлемента
ЦветФонаНечетныхСтрок == Окно
";
                                        }
                                        else if (strCurrent.Contains("" + NameObjectOneScriptForms + @".СтильКолонкиБулево();"))
                                        {
                                            string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            DataGridBoolColumn DataGridBoolColumn1 = new DataGridBoolColumn();
                                            dictObjects.Add(controlName + nameObj, DataGridBoolColumn1);
                                            DataGridBoolColumn1.NameStyle = nameObj;

                                            DataGridBoolColumn1.DefaultValues = @"Ширина == 75
Выравнивание == Лево
ТекстЗаголовка == 
ИмяОтображаемого == 
Текст == 
ТолькоЧтение == Ложь
";
                                        }
                                        else if (strCurrent.Contains("" + NameObjectOneScriptForms + @".СтильКолонкиПолеВвода();"))
                                        {
                                            string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            DataGridTextBoxColumn DataGridTextBoxColumn1 = new DataGridTextBoxColumn();
                                            dictObjects.Add(controlName + nameObj, DataGridTextBoxColumn1);
                                            DataGridTextBoxColumn1.NameStyle = nameObj;

                                            DataGridTextBoxColumn1.DefaultValues = @"Ширина == 75
Выравнивание == Лево
ТекстЗаголовка == 
ДвойноеНажатие == 
ИмяОтображаемого == 
Текст == 
ТолькоЧтение == Ложь
";
                                        }
                                        else if (strCurrent.Contains("" + NameObjectOneScriptForms + @".СтильКолонкиПолеВыбора();"))
                                        {
                                            string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            DataGridComboBoxColumnStyle DataGridComboBoxColumnStyle1 = new DataGridComboBoxColumnStyle();
                                            dictObjects.Add(controlName + nameObj, DataGridComboBoxColumnStyle1);
                                            DataGridComboBoxColumnStyle1.NameStyle = nameObj;

                                            DataGridComboBoxColumnStyle1.DefaultValues = @"Ширина == 75
Выравнивание == Лево
ТекстЗаголовка == 
ИмяОтображаемого == 
Текст == 
ТолькоЧтение == Ложь
";
                                        }
                                        else if (strCurrent.Contains(".СтилиКолонкиСеткиДанных.Добавить("))
                                        {
                                            string nameTableStyle = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                            osfDesigner.DataGridTableStyle tableStyle = (osfDesigner.DataGridTableStyle)dictObjects[nameTableStyle];
                                            string nameColumnStyle = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                            Component columnStyle = (Component)dictObjects[nameColumnStyle];
                                            tableStyle.OriginalObj.GridColumnStyles.Add((DataGridColumnStyle)columnStyle);
                                        }
                                        else
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                            string nameObj = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                            Component control2 = (Component)dictObjects[nameObj];
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, NameObjectOneScriptForms, null);
                                        }
                                    }
                                }
                                else if (componentName.Contains("Дерево"))
                                {
                                    string header = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                    if (header.Contains("Дерево") && !header.Contains("Узел")) // Обрабатываем как свойство дерева.
                                    {
                                        if (strCurrent.Contains("Узлы"))
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", ".");
                                            PropValueConverter.SetPropValue(control, displayName, strCurrent, NameObjectOneScriptForms, null);
                                        }
                                        else
                                        {

                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                            Control parent = (Control)dictObjects[parentName];
                                            PropValueConverter.SetPropValue(control, displayName, strPropertyValue, NameObjectOneScriptForms, parent);
                                        }
                                    }
                                    else // Обрабатываем как свойство узла.
                                    {
                                        if (strCurrent.Contains("Узлы"))
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", ".");
                                            PropValueConverter.SetPropValue(control, displayName, strCurrent, NameObjectOneScriptForms, null);
                                        }
                                        else
                                        {
                                            string displayName = "Узлы";
                                            PropValueConverter.SetPropValue(control, displayName, strCurrent, NameObjectOneScriptForms, null);
                                        }
                                    }
                                }
                                else if (componentName.Contains("Таблица"))
                                {
                                    osfDesigner.DataGridView DataGridView1 = (osfDesigner.DataGridView)control;
                                    string controlName = DataGridView1.Name;
                                    // Обработаем КолонкаПолеВвода =======================================================================================
                                    if (strCurrent.Contains("=" + NameObjectOneScriptForms + @".КолонкаПолеВвода();"))
                                    {
                                        string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                        DataGridViewTextBoxColumn DataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
                                        dictObjects.Add(nameObj, DataGridViewTextBoxColumn1);
                                        DataGridViewTextBoxColumn1.Name = nameObj.Replace(controlName, "");
                                        DataGridViewTextBoxColumn1.NameColumn = nameObj;

                                        DataGridViewTextBoxColumn1.DefaultValues = @"
Отображать == Истина
ТекстЗаголовка == КолонкаПолеВвода0
ТекстПодсказки == 
ИмяСвойстваДанных == 
ВесЗаполнения == 100
Закреплено == Ложь
МинимальнаяШирина == 5
РежимАвтоРазмера == НеУстановлено
Ширина == 100
ШиринаРазделителя == 0
ИзменяемыйРазмер == Истина
МаксимальнаяДлина == 32767
РежимСортировки == Автоматически
ТолькоЧтение == Ложь
ИмяКолонки == Таблица1КолонкаПолеВвода0
";
                                    }
                                    else if (strCurrent.Contains("КолонкаПолеВвода") &&
                                        !(strCurrent.Contains("=" + NameObjectOneScriptForms + @".КолонкаПолеВвода();") || 
                                        strCurrent.Contains("=" + NameObjectOneScriptForms + @".СтильЯчейки();") || 
                                        strCurrent.Contains("Колонки.Добавить(")
                                        )) // Обрабатываем как свойство КолонкаПолеВвода
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                        string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                        object control2 = dictObjects[nameObj];
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, NameObjectOneScriptForms, null);
                                    }
                                    else if (strCurrent.Contains(".Колонки.Добавить(") && strCurrent.Contains("КолонкаПолеВвода"))
                                    {
                                        string nameColumn = OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                        DataGridViewTextBoxColumn column = (DataGridViewTextBoxColumn)dictObjects[nameColumn];
                                        DataGridView1.Columns.Add((DataGridViewTextBoxColumn)column);
                                    }
                                    // Закончили КолонкаПолеВвода =======================================================================================

                                    // Обработаем КолонкаКартинка =======================================================================================
                                    else if(strCurrent.Contains("=" + NameObjectOneScriptForms + @".КолонкаКартинка();"))
                                    {
                                        string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                        DataGridViewImageColumn DataGridViewImageColumn1 = new DataGridViewImageColumn();
                                        dictObjects.Add(nameObj, DataGridViewImageColumn1);
                                        DataGridViewImageColumn1.Name = nameObj.Replace(controlName, "");
                                        DataGridViewImageColumn1.NameColumn = nameObj;

                                        DataGridViewImageColumn1.DefaultValues = @"
Описание == 
Отображать == Истина
РазмещениеИзображения == Стандартное
ТекстЗаголовка == КолонкаКартинка0
ТекстПодсказки == 
ИмяСвойстваДанных == 
ВесЗаполнения == 100
Закреплено == Ложь
МинимальнаяШирина == 5
РежимАвтоРазмера == НеУстановлено
Ширина == 100
ШиринаРазделителя == 0
ИзменяемыйРазмер == Истина
РежимСортировки == НеСортируемый
ТолькоЧтение == Ложь
ИмяКолонки == Таблица1КолонкаКартинка0
Картинка == 
";
                                    }
                                    else if (strCurrent.Contains("КолонкаКартинка") &&
                                        !(strCurrent.Contains("=" + NameObjectOneScriptForms + @".КолонкаКартинка();") ||
                                        strCurrent.Contains("=" + NameObjectOneScriptForms + @".СтильЯчейки();") ||
                                        strCurrent.Contains("Колонки.Добавить(")
                                        )) // Обрабатываем как свойство КолонкаКартинка
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                        string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                        object control2 = dictObjects[nameObj];
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, NameObjectOneScriptForms, null);
                                    }
                                    else if (strCurrent.Contains(".Колонки.Добавить(") && strCurrent.Contains("КолонкаКартинка"))
                                    {
                                        string nameColumn = OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                        DataGridViewImageColumn column = (DataGridViewImageColumn)dictObjects[nameColumn];
                                        DataGridView1.Columns.Add((DataGridViewImageColumn)column);
                                    }
                                    // Закончили КолонкаКартинка =======================================================================================

                                    // Обработаем КолонкаКнопка =======================================================================================
                                    else if (strCurrent.Contains("=" + NameObjectOneScriptForms + @".КолонкаКнопка();"))
                                    {
                                        string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                        DataGridViewButtonColumn DataGridViewButtonColumn1 = new DataGridViewButtonColumn();
                                        dictObjects.Add(nameObj, DataGridViewButtonColumn1);
                                        DataGridViewButtonColumn1.Name = nameObj.Replace(controlName, "");
                                        DataGridViewButtonColumn1.NameColumn = nameObj;

                                        DataGridViewButtonColumn1.DefaultValues = @"
ИспользоватьТекстКакЗначение == Ложь
Отображать == Истина
ПлоскийСтиль == Стандартный
Текст == 
ТекстЗаголовка == КолонкаКнопка0
ТекстПодсказки == 
ИмяСвойстваДанных == 
ВесЗаполнения == 100
Закреплено == Ложь
МинимальнаяШирина == 5
РежимАвтоРазмера == НеУстановлено
Ширина == 100
ШиринаРазделителя == 0
ИзменяемыйРазмер == Истина
РежимСортировки == НеСортируемый
ТолькоЧтение == Ложь
ИмяКолонки == Таблица1КолонкаКнопка0
";
                                    }
                                    else if (strCurrent.Contains("КолонкаКнопка") &&
                                        !(strCurrent.Contains("=" + NameObjectOneScriptForms + @".КолонкаКнопка();") ||
                                        strCurrent.Contains("=" + NameObjectOneScriptForms + @".СтильЯчейки();") ||
                                        strCurrent.Contains("Колонки.Добавить(")
                                        )) // Обрабатываем как свойство КолонкаКнопка
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                        string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                        object control2 = dictObjects[nameObj];
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, NameObjectOneScriptForms, null);
                                    }
                                    else if (strCurrent.Contains(".Колонки.Добавить(") && strCurrent.Contains("КолонкаКнопка"))
                                    {
                                        string nameColumn = OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                        DataGridViewButtonColumn column = (DataGridViewButtonColumn)dictObjects[nameColumn];
                                        DataGridView1.Columns.Add((DataGridViewButtonColumn)column);
                                    }
                                    // Закончили КолонкаКнопка =======================================================================================

                                    // Обработаем КолонкаПолеВыбора =======================================================================================
                                    else if (strCurrent.Contains("=" + NameObjectOneScriptForms + @".КолонкаПолеВыбора();"))
                                    {
                                        string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                        DataGridViewComboBoxColumn DataGridViewComboBoxColumn1 = new DataGridViewComboBoxColumn();
                                        dictObjects.Add(nameObj, DataGridViewComboBoxColumn1);
                                        DataGridViewComboBoxColumn1.Name = nameObj.Replace(controlName, "");
                                        DataGridViewComboBoxColumn1.NameColumn = nameObj;

                                        DataGridViewComboBoxColumn1.DefaultValues = @"
Отображать == Истина
ПлоскийСтиль == Стандартный
СтильОтображения == КнопкаСписка
ТекстЗаголовка == КолонкаПолеВыбора0
ТекстПодсказки == 
ИмяСвойстваДанных == 
Элементы == (Коллекция)
ВесЗаполнения == 100
Закреплено == Ложь
МинимальнаяШирина == 5
РежимАвтоРазмера == НеУстановлено
Ширина == 100
ШиринаРазделителя == 0
ИзменяемыйРазмер == Истина
МаксимумЭлементов == 8
Отсортирован == Ложь
РежимСортировки == НеСортируемый
ТолькоЧтение == Ложь
ИмяКолонки == Таблица1КолонкаПолеВыбора0
";
                                    }
                                    else if (strCurrent.Contains("КолонкаПолеВыбора") &&
                                        !(strCurrent.Contains("=" + NameObjectOneScriptForms + @".КолонкаПолеВыбора();") ||
                                        strCurrent.Contains("=" + NameObjectOneScriptForms + @".СтильЯчейки();") ||
                                        strCurrent.Contains("Колонки.Добавить(") ||
                                        strCurrent.Contains("Элементы.Добавить(")	
                                        )) // Обрабатываем как свойство КолонкаПолеВыбора
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                        string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                        object control2 = dictObjects[nameObj];
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, NameObjectOneScriptForms, null);
                                    }
                                    else if (strCurrent.Contains(".Колонки.Добавить(") && strCurrent.Contains("КолонкаПолеВыбора"))
                                    {
                                        string nameColumn = OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                        DataGridViewComboBoxColumn column = (DataGridViewComboBoxColumn)dictObjects[nameColumn];
                                        DataGridView1.Columns.Add((DataGridViewComboBoxColumn)column);
                                    }
                                    else if (strCurrent.Contains(".Элементы.Добавить(") && strCurrent.Contains("КолонкаПолеВыбора")) // Добавляем элемент поля выбора.
                                    {
                                        string nameColumn = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                        DataGridViewComboBoxColumn column = (DataGridViewComboBoxColumn)dictObjects[nameColumn];

                                        // Определяем тип элемента списка и создаем его.
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, ".Элементы.Добавить(", ");");
                                        string itemText = strPropertyValue;
                                        string itemValue = strPropertyValue;

                                        osfDesigner.ListItemComboBox ListItemComboBox1 = new ListItemComboBox();
                                        ListItemComboBox1.Text = itemText;

                                        if (itemValue.StartsWith("\u0022") && itemValue.EndsWith("\u0022")) // Тип Строка.
                                        {
                                            itemValue = itemValue.Replace("\u0022", "");
                                            ListItemComboBox1.Value = itemValue;
                                            ListItemComboBox1.ValueType = DataType.Строка;
                                        }
                                        else if (strPropertyValue.Contains("Ложь") || strPropertyValue.Contains("Истина")) // Тип Булево.
                                        {
                                            ListItemComboBox1.Value = true;
                                            ListItemComboBox1.ValueType = DataType.Булево;
                                            if (itemValue == "Ложь")
                                            {
                                                ListItemComboBox1.Value = false;
                                            }
                                        }
                                        else if (strPropertyValue.Contains("Дата(")) // Тип Дата.
                                        {
                                            DateTime rez1 = new DateTime();
                                            string[] result1 = OneScriptFormsDesigner.ParseBetween(strPropertyValue, "Дата(", ")").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                            for (int i2 = 0; i2 < result1.Length; i2++)
                                            {
                                                if (i2 == 0)
                                                {
                                                    rez1 = rez1.AddYears(Int32.Parse(result1[0]) - 1);
                                                }
                                                if (i2 == 1)
                                                {
                                                    rez1 = rez1.AddMonths(Int32.Parse(result1[1]) - 1);
                                                }
                                                if (i2 == 2)
                                                {
                                                    rez1 = rez1.AddDays(Int32.Parse(result1[2]) - 1);
                                                }
                                                if (i2 == 3)
                                                {
                                                    rez1 = rez1.AddHours(Int32.Parse(result1[3]));
                                                }
                                                if (i2 == 4)
                                                {
                                                    rez1 = rez1.AddMinutes(Int32.Parse(result1[4]));
                                                }
                                                if (i2 == 5)
                                                {
                                                    rez1 = rez1.AddSeconds(Int32.Parse(result1[5]));
                                                }
                                            }
                                            ListItemComboBox1.Value = rez1;
                                            ListItemComboBox1.ValueType = DataType.Дата;
                                        }
                                        else // Это тип Число.
                                        {
                                            itemValue = itemValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                                            ListItemComboBox1.Value = Decimal.Parse(itemValue);
                                            ListItemComboBox1.ValueType = DataType.Число;
                                        }
                                        column.Items.Add(ListItemComboBox1);
                                    }	
                                    // Закончили КолонкаПолеВыбора =======================================================================================

                                    // Обработаем КолонкаСсылка =======================================================================================
                                    else if (strCurrent.Contains("=" + NameObjectOneScriptForms + @".КолонкаСсылка();"))
                                    {
                                        string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                        DataGridViewLinkColumn DataGridViewLinkColumn1 = new DataGridViewLinkColumn();
                                        dictObjects.Add(nameObj, DataGridViewLinkColumn1);
                                        DataGridViewLinkColumn1.Name = nameObj.Replace(controlName, "");
                                        DataGridViewLinkColumn1.NameColumn = nameObj;

                                        DataGridViewLinkColumn1.DefaultValues = @"
ИспользоватьТекстКакСсылку == Ложь
Отображать == Истина
Текст == 
ТекстЗаголовка == КолонкаСсылка0
ТекстПодсказки == 
ЦветАктивнойСсылки == Красный
ЦветПосещеннойСсылки == 128; 0; 128
ЦветСсылки == 0; 0; 255
ИмяСвойстваДанных == 
ВесЗаполнения == 100
Закреплено == Ложь
МинимальнаяШирина == 5
РежимАвтоРазмера == НеУстановлено
Ширина == 100
ШиринаРазделителя == 0
ИзменяемыйРазмер == Истина
ПоведениеСсылки == СистемныйПоУмолчанию
РежимСортировки == НеСортируемый
ТолькоЧтение == Ложь
ЦветПосещеннойИзменяется == Истина
ИмяКолонки == Таблица1КолонкаСсылка0
";
                                    }
                                    else if (strCurrent.Contains("КолонкаСсылка") &&
                                        !(strCurrent.Contains("=" + NameObjectOneScriptForms + @".КолонкаСсылка();") ||
                                        strCurrent.Contains("=" + NameObjectOneScriptForms + @".СтильЯчейки();") ||
                                        strCurrent.Contains("Колонки.Добавить(")
                                        )) // Обрабатываем как свойство КолонкаСсылка
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                        string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                        object control2 = dictObjects[nameObj];
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, NameObjectOneScriptForms, null);
                                    }
                                    else if (strCurrent.Contains(".Колонки.Добавить(") && strCurrent.Contains("КолонкаСсылка"))
                                    {
                                        string nameColumn = OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                        DataGridViewLinkColumn column = (DataGridViewLinkColumn)dictObjects[nameColumn];
                                        DataGridView1.Columns.Add((DataGridViewLinkColumn)column);
                                    }
                                    // Закончили КолонкаСсылка =======================================================================================

                                    // Обработаем КолонкаФлажок =======================================================================================
                                    else if (strCurrent.Contains("=" + NameObjectOneScriptForms + @".КолонкаФлажок();"))
                                    {
                                        string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                        DataGridViewCheckBoxColumn DataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
                                        dictObjects.Add(nameObj, DataGridViewCheckBoxColumn1);
                                        DataGridViewCheckBoxColumn1.Name = nameObj.Replace(controlName, "");
                                        DataGridViewCheckBoxColumn1.NameColumn = nameObj;

                                        DataGridViewCheckBoxColumn1.DefaultValues = @"
Отображать == Истина
ПлоскийСтиль == Стандартный
ТекстЗаголовка == КолонкаФлажок0
ТекстПодсказки == 
ИмяСвойстваДанных == 
ВесЗаполнения == 100
Закреплено == Ложь
МинимальнаяШирина == 5
РежимАвтоРазмера == НеУстановлено
Ширина == 100
ШиринаРазделителя == 0
ИзменяемыйРазмер == Истина
РежимСортировки == НеСортируемый
ТолькоЧтение == Ложь
ТриСостояния == Ложь
ИмяКолонки == Таблица1КолонкаФлажок0
";
                                    }
                                    else if (strCurrent.Contains("КолонкаФлажок") &&
                                        !(strCurrent.Contains("=" + NameObjectOneScriptForms + @".КолонкаФлажок();") ||
                                        strCurrent.Contains("=" + NameObjectOneScriptForms + @".СтильЯчейки();") ||
                                        strCurrent.Contains("Колонки.Добавить(")
                                        )) // Обрабатываем как свойство КолонкаФлажок
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                        string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                        object control2 = dictObjects[nameObj];
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, NameObjectOneScriptForms, null);
                                    }
                                    else if (strCurrent.Contains(".Колонки.Добавить(") && strCurrent.Contains("КолонкаФлажок"))
                                    {
                                        string nameColumn = OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                        DataGridViewCheckBoxColumn column = (DataGridViewCheckBoxColumn)dictObjects[nameColumn];
                                        DataGridView1.Columns.Add((DataGridViewCheckBoxColumn)column);
                                    }
                                    // Закончили КолонкаФлажок =======================================================================================

                                    // Теперь обработаем стили =============================================================================
                                    else if (strCurrent.Contains("СтильЗаголовковКолонок=" + NameObjectOneScriptForms + @".СтильЯчейки();"))	
                                    {
                                        string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                        DataGridViewCellStyleHeaders ColumnHeadersDefaultCellStyle1 = DataGridView1.columnHeadersDefaultCellStyle;
                                        dictObjects.Add(nameObj, ColumnHeadersDefaultCellStyle1);
                                        ColumnHeadersDefaultCellStyle1.NameStyle = nameObj;

                                        ColumnHeadersDefaultCellStyle1.DefaultValues = @"
ОсновнойЦвет == ТекстОкна
ОсновнойЦветВыделенного == ТекстВыбранных
ЦветФона == ЛицеваяЭлемента
ЦветФонаВыделенного == ФонВыбранных
Шрифт == Microsoft Sans Serif; 7,8pt
Выравнивание == СерединаЛево
Заполнение == 0; 0; 0; 0
Перенос == Истина
ИмяСтиля == Таблица1СтильЯчейки
";
                                    }
                                    else if(strCurrent.Contains("СтильЗаголовковСтрок=" + NameObjectOneScriptForms + @".СтильЯчейки();"))
                                    {
                                        string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                        DataGridViewCellStyleHeaders RowHeadersDefaultCellStyle1 = DataGridView1.rowHeadersDefaultCellStyle;
                                        dictObjects.Add(nameObj, RowHeadersDefaultCellStyle1);
                                        RowHeadersDefaultCellStyle1.NameStyle = nameObj;

                                        RowHeadersDefaultCellStyle1.DefaultValues = @"
ОсновнойЦвет == ТекстОкна
ОсновнойЦветВыделенного == ТекстВыбранных
ЦветФона == ЛицеваяЭлемента
ЦветФонаВыделенного == ФонВыбранных
Шрифт == Microsoft Sans Serif; 7,8pt
Выравнивание == СерединаЛево
Заполнение == 0; 0; 0; 0
Перенос == Истина
ИмяСтиля == Таблица1СтильЯчейки
";
                                    }
                                    else if (strCurrent.Contains("СтильЯчейки=" + NameObjectOneScriptForms + @".СтильЯчейки();"))
                                    {
                                        // Найдем имя стиля ячейки.
                                        string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                        // Найдем имя колонки для которой применяется стиль ячейки.
                                        string nameObjColumn = OneScriptFormsDesigner.ParseBetween(nameObj, null, "СтильЯчейки");
                                        // Найдем колонку для которой применяется стиль ячейки.
                                        System.Windows.Forms.DataGridViewColumn control2 = (System.Windows.Forms.DataGridViewColumn)dictObjects[nameObjColumn];
                                        DataGridViewCellStyle DefaultCellStyle1 = new DataGridViewCellStyle(nameObj, DataGridView1);
                                        dictObjects.Add(nameObj, DefaultCellStyle1);
                                        control2.DefaultCellStyle = DefaultCellStyle1;
                                    }
                                    else if (strCurrent.Contains("СтильЗаголовковКолонок.") || 
                                        strCurrent.Contains("СтильЗаголовковСтрок.") || 
                                        strCurrent.Contains("СтильЯчейки."))
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                        string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                        object control2 = dictObjects[nameObj];
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, NameObjectOneScriptForms, null);
                                    }
                                    else if (strCurrent.Contains(".СтильЗаголовковКолонок="))
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                        Control parent = (Control)dictObjects[parentName];
                                        DataGridView1.ColumnHeadersDefaultCellStyle = (DataGridViewCellStyleHeaders)dictObjects[strPropertyValue];
                                    }
                                    else if (strCurrent.Contains(".СтильЗаголовковСтрок="))
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                        Control parent = (Control)dictObjects[parentName];
                                        DataGridView1.RowHeadersDefaultCellStyle = (DataGridViewCellStyleHeaders)dictObjects[strPropertyValue];
                                    }
                                    else // Обрабатываем как свойство таблицы.
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                        Control parent = (Control)dictObjects[parentName];
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, NameObjectOneScriptForms, parent);
                                    }
                                }
                                else
                                {
                                    string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                    string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                    string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                    Control parent = (Control)dictObjects[parentName];

                                    if (parent.GetType() == typeof(osfDesigner.TabPage))
                                    {
                                        parent = OneScriptFormsDesigner.RevertOriginalObj(parent);
                                    }

                                    PropValueConverter.SetPropValue(control, displayName, strPropertyValue, NameObjectOneScriptForms, parent);
                                }
                            }
                        }
                    }

                    if (control.GetType() == typeof(osfDesigner.ToolBar) || 
                        control.GetType() == typeof(osfDesigner.Splitter) || 
                        control.GetType() == typeof(osfDesigner.StatusBar))
                    {
                        ((Control)control).BringToFront();
                    }

                    result = null;
                    ComponentBlok = null;
                    propertyGrid1.Refresh();
                }

                if (componentName.Contains("ГлавноеМеню"))
                {
                    OneScriptFormsDesigner.PassProperties(control, OneScriptFormsDesigner.RevertOriginalObj(control)); // Передадим свойства.
                }
            }

            // Если для формы заданы КнопкаОтмена и/или КнопкаПринять, установим их.
            if (rootBlok != null)
            {
                // Установим для формы свойства.
                result = rootBlok.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                for (int i1 = 0; i1 < result.Length; i1++)
                {
                    string strCurrent = result[i1];
                    if (strCurrent.Length >= 2)
                    {
                        if (strCurrent.Substring(0, 2) != @"//")
                        {
                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, formName + ".", "=");
                            if (displayName == "КнопкаОтмена" || displayName == "КнопкаПринять")
                            {
                                string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                PropValueConverter.SetPropValue(rootComponent, displayName, strPropertyValue, NameObjectOneScriptForms);
                            }
	
                            if (displayName == "Меню")
                            {
                                string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                ((Form)rootComponent).Menu = (System.Windows.Forms.MainMenu)dictObjects[strPropertyValue];
                            }

                            if (strCurrent.StartsWith("Подсказка"))
                            {
                                displayName = "ToolTip на " + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".УстановитьПодсказку");
                                string strPropertyValue = strCurrent;
                                PropValueConverter.SetPropValue(rootComponent, displayName, strPropertyValue, NameObjectOneScriptForms);
                            }
                        }
                    }
                }
                propertyGrid1.Refresh();
            }

            ComponentCollection ctrlsExisting = OneScriptFormsDesigner.DesignerHost.Container.Components;
            ISelectionService iSel = (ISelectionService)OneScriptFormsDesigner.DesignerHost.GetService(typeof(ISelectionService));
            if (iSel == null)
            {
                return;
            }
            iSel.SetSelectedComponents(new IComponent[] { ctrlsExisting[0] });

            OneScriptFormsDesigner.PropertyGridHost.ReloadTreeView();
            OneScriptFormsDesigner.PropertyGridHost.ChangeSelectNode((Component)ctrlsExisting[0]);

            OneScriptFormsDesigner.block2 = false;

            // Запомним начальное состояние дизайнера после загрузки этой формы.
            OneScriptFormsDesigner.DesignSurfaceState(true);
        }

        public string Version
        {
            get
            {
                if (string.IsNullOrEmpty(_version))
                {
                    // Получение версии файла запущенной сборки.
                    System.Diagnostics.FileVersionInfo FVI = System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
                    _version = FVI.ProductVersion;
                }
                return _version;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void ChangeImage(bool change)
        {
            if (change)
            {
                _tabOrder1.CheckState = System.Windows.Forms.CheckState.Checked;
            }
            else
            {
                _tabOrder1.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
        }

        public Control GetmainForm()
        {
            return this;
        }

        private void pDesignerMainForm_Load(object sender, EventArgs e)
        {
            // Таймер для обеспечения срабатывания по правой кнопке мыши сворачивания раскрытого свойства СписокИзображений.
            timerLoad = new System.Windows.Forms.Timer();
            timerLoad.Enabled = true;
            timerLoad.Tick += new EventHandler(timerLoad_Tick);
        }

        private void _deleteForm_Click(object sender, EventArgs e)
        {
            if (pDesigner.TabControl.TabPages.Count <= 1)
            {
                MessageBox.Show(
                    "Удалить единственную форму не допускается.",
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1
                    );
            }
            else
            {
                OneScriptFormsDesigner.block2 = true;
                OneScriptFormsDesigner.dictionaryTabPageChanged.Remove(pDesigner.TabControl.SelectedTab);
                IpDesignerCore.RemoveDesignSurface(IpDesignerCore.ActiveDesignSurface);
                OneScriptFormsDesigner.block2 = false;
            }
        }

        private void _unDo_Click(object sender, EventArgs e)
        {
            IpDesignerCore.UndoOnDesignSurface();
        }

        private void _reDo_Click(object sender, EventArgs e)
        {
            IpDesignerCore.RedoOnDesignSurface();
        }

        private void OnMenuClick(object sender, EventArgs e)
        {
            string cmd = (sender as ToolStripMenuItem).Text;
            if (cmd == "Вырезать")
            {
                IpDesignerCore.CutOnDesignSurface();
            }
            else if (cmd == "Копировать")
            {
                IpDesignerCore.CopyOnDesignSurface();
            }
            else if (cmd == "Вставить")
            {
                IpDesignerCore.PasteOnDesignSurface();
            }
            else if (cmd == "Удалить")
            {
                IpDesignerCore.DeleteOnDesignSurface();
            }
        }

        private void _tabOrder_Click(object sender, EventArgs e)
        {
            IpDesignerCore.SwitchTabOrder();

            if (_tabOrder.CheckState == System.Windows.Forms.CheckState.Unchecked)
            {
                _tabOrder.CheckState = System.Windows.Forms.CheckState.Checked;
            }
            else if (_tabOrder.CheckState == System.Windows.Forms.CheckState.Checked)
            {
                _tabOrder.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
        }

        private void _about_Click(object sender, EventArgs e)
        {
            string str1 = "Дизайнер форм от ahyahy " + Environment.NewLine + 
                "Версия " + Version + Environment.NewLine + 
                "(Создана на основе программы: " + Environment.NewLine + 
                "picoFormDesigner coded by Paolo Foti " + Environment.NewLine +
                "Version is: 1.0.0.0)";
            MessageBox.Show(str1, "Дизайнер форм для OneScriptForms", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void _useSnapLines_Click(object sender, EventArgs e)
        {
            OneScriptFormsDesigner.block2 = true;
            IpDesignerCore.AddDesignSurface<Form>(640, 480, AlignmentModeEnum.SnapLines, new Size(1, 1));
            OneScriptFormsDesigner.block2 = false;

            // Запомним начальное состояние дизайнера после добавления новой формы.
            OneScriptFormsDesigner.DesignSurfaceState(true);
            OneScriptFormsDesigner.dictionaryTabPageChanged[pDesigner.TabControl.SelectedTab] = true;
        }

        private void _useGrid_Click(object sender, EventArgs e)
        {
            OneScriptFormsDesigner.block2 = true;
            IpDesignerCore.AddDesignSurface<Form>(640, 480, AlignmentModeEnum.Grid, new Size(16, 16));
            OneScriptFormsDesigner.block2 = false;

            // Запомним начальное состояние дизайнера после добавления новой формы.
            OneScriptFormsDesigner.DesignSurfaceState(true);
            OneScriptFormsDesigner.dictionaryTabPageChanged[pDesigner.TabControl.SelectedTab] = true;
        }

        private void _useGridWithoutSnapping_Click(object sender, EventArgs e)
        {
            OneScriptFormsDesigner.block2 = true;
            IpDesignerCore.AddDesignSurface<Form>(640, 480, AlignmentModeEnum.GridWithoutSnapping, new Size(16, 16));
            OneScriptFormsDesigner.block2 = false;

            // Запомним начальное состояние дизайнера после добавления новой формы.
            OneScriptFormsDesigner.DesignSurfaceState(true);
            OneScriptFormsDesigner.dictionaryTabPageChanged[pDesigner.TabControl.SelectedTab] = true;
        }

        private void _useNoGuides_Click(object sender, EventArgs e)
        {
            OneScriptFormsDesigner.block2 = true;
            IpDesignerCore.AddDesignSurface<Form>(640, 480, AlignmentModeEnum.NoGuides, new Size(1, 1));
            OneScriptFormsDesigner.block2 = false;

            // Запомним начальное состояние дизайнера после добавления новой формы.
            OneScriptFormsDesigner.DesignSurfaceState(true);
            OneScriptFormsDesigner.dictionaryTabPageChanged[pDesigner.TabControl.SelectedTab] = true;
        }

        private void pDesignerMainForm_Closing(object sender, CancelEventArgs e)
        {
            bool closeDesigner = true;
            foreach (KeyValuePair<System.Windows.Forms.TabPage, bool> keyValue in OneScriptFormsDesigner.dictionaryTabPageChanged)
            {
                if (keyValue.Value)
                {
                    closeDesigner = false;
                    break;
                }
            }

            if (!closeDesigner)
            {
                string str1 = "Одна из редактируемых форм изменена! Изменения будут потеряны!\n\nВыйти из конструктора форм?";
                if (MessageBox.Show(str1, "Дизайнер форм для OneScriptForms", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    e.Cancel = false;
                    return;
                }
            }
        }

        public static void SelectedNodeSearch(System.Windows.Forms.TreeView treeView, string nameNodeParent, ref TreeNode node, TreeNodeCollection treeNodes = null)
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
            TreeNode treeNode = null;
            for (int i = 0; i < _treeNodes.Count; i++)
            {
                treeNode = _treeNodes[i];
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
    }

    public class PropertyGridMessageFilter : IMessageFilter
    {
        public Control Control; // Элемент управления для мониторинга.

        public MouseEventHandler MouseUp;

        public PropertyGridMessageFilter(Control c, MouseEventHandler meh)
        {
            Control = c;
            MouseUp = meh;
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (!Control.IsDisposed && m.HWnd == Control.Handle && MouseUp != null)
            {
                System.Windows.Forms.MouseButtons mb = System.Windows.Forms.MouseButtons.None;

                switch (m.Msg)
                {
                    case 0x0202:/*WM_LBUTTONUP, see winuser.h*/
                        mb = System.Windows.Forms.MouseButtons.Left;
                        break;
                    case 0x0205:/*WM_RBUTTONUP*/
                        mb = System.Windows.Forms.MouseButtons.Right;
                        break;
                }

                if (mb != System.Windows.Forms.MouseButtons.None)
                {
                    MouseEventArgs e = new MouseEventArgs(mb, 1, m.LParam.ToInt32() & 0xFFff, m.LParam.ToInt32() >> 16, 0);
                    MouseUp(Control, e);
                }
            }
            return false;
        }
    }
}
