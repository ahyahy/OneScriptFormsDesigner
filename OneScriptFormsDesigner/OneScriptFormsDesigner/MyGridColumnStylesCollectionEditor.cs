using System;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Reflection;

namespace osfDesigner
{
    public class MyGridColumnStylesCollectionEditor : CollectionEditor
    {
        private System.ComponentModel.Design.CollectionEditor.CollectionForm collectionForm;
        private System.Windows.Forms.Form frmCollectionEditorForm;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel AddRemoveTableLayoutPanel1;
        private System.Windows.Forms.Label PropertiesLabel1 = null;
        private System.Windows.Forms.Label MembersLabel1 = null;
        private System.Windows.Forms.ListBox ListBox1;
        private System.Windows.Forms.PropertyGrid PropertyGrid1;
        private System.Windows.Forms.TableLayoutPanel OkCancelTableLayoutPanel1;
        private System.Windows.Forms.Button ButtonOk1 = null;
        private System.Windows.Forms.Button ButtonCancel1 = null;
        private System.Windows.Forms.Button ButtonAdd1 = null;
        private System.Windows.Forms.Button ButtonRemove1 = null;
        private System.Windows.Forms.Button ButtonUp1 = null;
        private System.Windows.Forms.Button ButtonDown1 = null;
        private System.Windows.Forms.PropertyGrid TopLevelPropertyGrid1;
        private System.Windows.Forms.DataGridTableStyle DataGridTableStyle1;

        // Определите статическое событие, чтобы отобразить внутреннюю сетку свойств
        public delegate void MyPropertyValueChangedEventHandler(object sender, PropertyValueChangedEventArgs e);

        // Унаследуйте конструктор по умолчанию из стандартного редактора коллекций...
        public MyGridColumnStylesCollectionEditor(Type type) : base(type)
        {
        }

        //создадим перечень нужных нам в коллекции типов
        protected override Type[] CreateNewItemTypes()
        {
            Type[] Type1;
            Type1 = new Type[]
            {
                typeof(osfDesigner.DataGridTextBoxColumn),
                typeof(osfDesigner.DataGridBoolColumn),
                typeof(osfDesigner.DataGridComboBoxColumnStyle)
            };
            return Type1;
        }

        // Переопределите этот метод, чтобы получить доступ к форме редактора коллекции. 
        protected override CollectionForm CreateCollectionForm()
        {
            // Получение макета редактора коллекции по умолчанию...
            collectionForm = base.CreateCollectionForm();
            DataGridTableStyle1 = OneScriptFormsDesigner.RevertOriginalObj((osfDesigner.DataGridTableStyle)this.Context.Instance);
            collectionForm.Text = "Редактор коллекции СтилиКолонкиСеткиДанных";

            collectionForm.Shown += delegate (object sender, EventArgs e)
            {
                PropertiesLabel1.Text = "Свойства:";
                TopLevelPropertyGrid1 = pDesigner.DSME.PropertyGridHost.PropertyGrid;
            };

            frmCollectionEditorForm = (System.Windows.Forms.Form)collectionForm;
            TableLayoutPanel1 = (System.Windows.Forms.TableLayoutPanel)frmCollectionEditorForm.Controls[0];
            if (TableLayoutPanel1 != null)
            {
                for (int i = 0; i < TableLayoutPanel1.Controls.Count; i++)
                {
                    if (i == 0)
                    {
                        ButtonDown1 = (System.Windows.Forms.Button)TableLayoutPanel1.Controls[0];
                        ButtonDown1.Click += ButtonDown1_Click;
                    }
                    if (i == 1)
                    {
                        AddRemoveTableLayoutPanel1 = (System.Windows.Forms.TableLayoutPanel)TableLayoutPanel1.Controls[1];
                    }
                    if (i == 2)
                    {
                        PropertiesLabel1 = (System.Windows.Forms.Label)TableLayoutPanel1.Controls[2];
                        PropertiesLabel1.Text = "Свойства:";
                    }
                    if (i == 3)
                    {
                        MembersLabel1 = (System.Windows.Forms.Label)TableLayoutPanel1.Controls[3];
                        MembersLabel1.Text = "Члены:";
                    }
                    if (i == 4)
                    {
                        ListBox1 = (System.Windows.Forms.ListBox)TableLayoutPanel1.Controls[4];
                        ListBox1.DrawItem += ListBox1_DrawItem;
                    }
                    // Получите ссылку на внутреннюю сетку свойств и подключите к ней обработчик событий.
                    if (i == 5)
                    {
                        PropertyGrid1 = (System.Windows.Forms.PropertyGrid)TableLayoutPanel1.Controls[5];
                        PropertyGrid1.SelectedGridItemChanged += PropertyGrid1_SelectedGridItemChanged;
                        PropertyGrid1.SelectedObjectsChanged += PropertyGrid1_SelectedObjectsChanged;

                        // также сделать доступным окно с подсказками по параметрам в нижней части 
                        PropertyGrid1.HelpVisible = true;
                        PropertyGrid1.HelpBackColor = System.Drawing.SystemColors.Info;
                    }
                    if (i == 6)
                    {
                        OkCancelTableLayoutPanel1 = (System.Windows.Forms.TableLayoutPanel)TableLayoutPanel1.Controls[6];
                    }
                    if (i == 7)
                    {
                        ButtonUp1 = (System.Windows.Forms.Button)TableLayoutPanel1.Controls[7];
                        ButtonUp1.Click += ButtonUp1_Click;
                    }
                }
            }
            if (AddRemoveTableLayoutPanel1 != null)
            {
                for (int i = 0; i < AddRemoveTableLayoutPanel1.Controls.Count; i++)
                {
                    if (i == 0)
                    {
                        ButtonAdd1 = (System.Windows.Forms.Button)AddRemoveTableLayoutPanel1.Controls[0];
                        ButtonAdd1.Click += ButtonAdd1_Click;
                        ButtonAdd1.Text = "Добавить";

                        PropertyInfo PropertyInfo3 = ButtonAdd1.GetType().GetProperty("ShowSplit", BindingFlags.Public | BindingFlags.Instance);
                        PropertyInfo3.SetValue(ButtonAdd1, true);
                        foreach (ToolStripMenuItem item in ButtonAdd1.ContextMenuStrip.Items)
                        {
                            if (item.Text == "DataGridBoolColumn")
                            {
                                item.Text = "СтильКолонкиБулево";
                                item.Click += Item_BoolColumn_Click;
                            }
                            else if (item.Text == "DataGridTextBoxColumn")
                            {
                                item.Text = "СтильКолонкиПолеВвода";
                                item.Click += Item_BoolColumn_Click;
                            }
                            else if (item.Text == "DataGridComboBoxColumnStyle")
                            {
                                item.Text = "СтильКолонкиПолеВыбора";
                                item.Click += Item_ComboBoxColumn_Click;
                            }
                        }
                    }
                    if (i == 1)
                    {
                        ButtonRemove1 = (System.Windows.Forms.Button)AddRemoveTableLayoutPanel1.Controls[1];
                        ButtonRemove1.Click += ButtonRemove1_Click;
                        ButtonRemove1.Text = "Удалить";
                    }
                }
            }
            if (OkCancelTableLayoutPanel1 != null)
            {
                for (int i = 0; i < OkCancelTableLayoutPanel1.Controls.Count; i++)
                {
                    if (i == 0)
                    {
                        ButtonOk1 = (System.Windows.Forms.Button)OkCancelTableLayoutPanel1.Controls[0];
                        ButtonOk1.Text = "ОК";
                    }
                    if (i == 1)
                    {
                        ButtonCancel1 = (System.Windows.Forms.Button)OkCancelTableLayoutPanel1.Controls[1];
                        ButtonCancel1.Text = "Отмена";
                    }
                }
            }

            return collectionForm;
        }

        private void Item_ComboBoxColumn_Click(object sender, EventArgs e)
        {
            dynamic ColumnStyle1 = PropertyGrid1.SelectedObject;
            ColumnStyle1.NameStyle = OneScriptFormsDesigner.RevertDataGridColumnStyleName(DataGridTableStyle1, ColumnStyle1);
            ListBox1.Refresh();
            PropertyGrid1.SelectedObject = ColumnStyle1;
            GetDefaultValues();
        }

        private void Item_TextBoxColumn_Click(object sender, EventArgs e)
        {
            dynamic ColumnStyle1 = PropertyGrid1.SelectedObject;
            ColumnStyle1.NameStyle = OneScriptFormsDesigner.RevertDataGridColumnStyleName(DataGridTableStyle1, ColumnStyle1);
            ListBox1.Refresh();
            PropertyGrid1.SelectedObject = ColumnStyle1;
            GetDefaultValues();
        }

        private void Item_BoolColumn_Click(object sender, EventArgs e)
        {
            dynamic ColumnStyle1 = PropertyGrid1.SelectedObject;
            ColumnStyle1.NameStyle = OneScriptFormsDesigner.RevertDataGridColumnStyleName(DataGridTableStyle1, ColumnStyle1);
            ListBox1.Refresh();
            PropertyGrid1.SelectedObject = ColumnStyle1;
            GetDefaultValues();
        }

        private void ButtonAdd1_Click(object sender, EventArgs e)
        {
            dynamic ColumnStyle1 = PropertyGrid1.SelectedObject;
            ColumnStyle1.NameStyle = OneScriptFormsDesigner.RevertDataGridColumnStyleName(DataGridTableStyle1, ColumnStyle1);
            ListBox1.Refresh();
            PropertyGrid1.SelectedObject = ColumnStyle1;
            GetDefaultValues();
        }

        private void GetDefaultValues()
        {
            // Заполним для компонента начальные свойства. Они нужны будут при создании скрипта.
            dynamic comp = PropertyGrid1.SelectedObject;
            if (comp.DefaultValues != null)
            {
                return;
            }
            string DefaultValues1 = "";
            object pg = PropertyGrid1;
            object view1 = typeof(System.Windows.Forms.PropertyGrid).GetField("gridView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(pg);
            dynamic GridItemCollection1 = (dynamic)view1.GetType().InvokeMember("GetAllGridEntries", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, view1, null);
            foreach (GridItem GridItem in GridItemCollection1)
            {
                if (GridItem.PropertyDescriptor == null)// исключим из обхода категории
                {
                    continue;
                }
                if (GridItem.Label == "Locked")// исключим из обхода ненужные свойства
                {
                    continue;
                }
                if (GridItem.PropertyDescriptor.Category != GridItem.Label)
                {
                    string str7 = "";
                    string strTab = "            ";
                    str7 = str7 + osfDesigner.OneScriptFormsDesigner.ObjectConvertToString(GridItem.Value);
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
            comp.DefaultValues = DefaultValues1;
        }

        public string GetGridSubEntries(GridItemCollection gridItems, string str, string strTab)
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

        private void UpdateListBox1()
        {
            int index = ListBox1.SelectedIndex;
            DataGridTableStyle1.GridColumnStyles.Clear();
            MethodInfo MethodInfo3 = collectionForm.GetType().GetMethod("AddItems", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo3.Invoke(collectionForm, new object[] { DataGridTableStyle1.GridColumnStyles });
            ListBox1.SelectedIndex = index;
            if (index != ListBox1.Items.Count - 1)
            {
                ListBox1.SetSelected(ListBox1.Items.Count - 1, false);
            }
            collectionForm.Refresh();
        }

        private void PropertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            PropertiesLabel1.Text = "Свойства:";
        }

        private void PropertyGrid1_SelectedObjectsChanged(object sender, EventArgs e)
        {

        }

        private void ButtonRemove1_Click(object sender, EventArgs e)
        {
            UpdateListBox1();
        }

        private void ButtonDown1_Click(object sender, System.EventArgs e)
        {
            UpdateListBox1();
        }

        private void ButtonUp1_Click(object sender, System.EventArgs e)
        {
            UpdateListBox1();
        }

        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index != -1)
            {
                ListItem ListItem1 = new ListItem(ListBox1.Items[e.Index]);
                string ListItem1Text = "";
                try
                {
                    dynamic ColumnStyle1 = DataGridTableStyle1.GridColumnStyles[e.Index];
                    ListItem1Text = ColumnStyle1.NameStyle;
                }
                catch { }
                Graphics Graphics1 = e.Graphics;

                int Count1 = ListBox1.Items.Count;
                int maxCount1;
                if (Count1 > 1)
                {
                    maxCount1 = Count1 - 1;
                }
                else
                {
                    maxCount1 = Count1;
                }
                SizeF sizeW = Graphics1.MeasureString(maxCount1.ToString(System.Globalization.CultureInfo.CurrentCulture), ListBox1.Font);

                int charactersInNumber = ((int)(Math.Log(maxCount1) / Math.Log(10)) + 1);
                int w = 4 + charactersInNumber * (ListBox1.Font.Height / 2);

                w = Math.Max(w, (int)Math.Ceiling(sizeW.Width));
                w += SystemInformation.BorderSize.Width * 4;

                Rectangle button = new Rectangle(e.Bounds.X, e.Bounds.Y, w, e.Bounds.Height);

                ControlPaint.DrawButton(Graphics1, button, ButtonState.Normal);
                button.Inflate(-SystemInformation.BorderSize.Width * 2, -SystemInformation.BorderSize.Height * 2);

                int offset = w;

                Color backColor = SystemColors.Window;
                Color textColor = SystemColors.WindowText;
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    backColor = SystemColors.Highlight;
                    textColor = SystemColors.HighlightText;
                }

                Rectangle res = new Rectangle(e.Bounds.X + offset, e.Bounds.Y, e.Bounds.Width - offset, e.Bounds.Height);
                Graphics1.FillRectangle(new SolidBrush(backColor), res);
                if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
                {
                    ControlPaint.DrawFocusRectangle(Graphics1, res);
                }

                offset += 2;

                if (this != null && this.GetPaintValueSupported())
                {
                    Rectangle Rectangle2 = new Rectangle(e.Bounds.X + offset, e.Bounds.Y + 1, 20, e.Bounds.Height - 3);
                    Graphics1.DrawRectangle(SystemPens.ControlText,
                        Rectangle2.X,
                        Rectangle2.Y,
                        Rectangle2.Width - 1,
                        Rectangle2.Height - 1);
                    Rectangle2.Inflate(-1, -1);

                    PaintValueEventArgs PaintValueEventArgs1 = new System.Drawing.Design.PaintValueEventArgs(this.Context, ListItem1.Value, Graphics1, Rectangle2);
                    this.PaintValue(PaintValueEventArgs1);
                    offset += 26 + 1;
                }

                StringFormat StringFormat1 = new StringFormat();
                try
                {
                    StringFormat1.Alignment = StringAlignment.Center;
                    Graphics1.DrawString(e.Index.ToString(System.Globalization.CultureInfo.CurrentCulture),
                        ListBox1.Font,
                        SystemBrushes.ControlText,
                        new Rectangle(e.Bounds.X, e.Bounds.Y, w, e.Bounds.Height),
                        StringFormat1);
                }
                finally
                {
                    StringFormat1?.Dispose();
                }

                Brush textBrush = new SolidBrush(textColor);
                Graphics1.DrawString(ListItem1Text, ListBox1.Font, textBrush, new Rectangle(e.Bounds.X + offset, e.Bounds.Y, e.Bounds.Width - offset, e.Bounds.Height));
                textBrush.Dispose();
            }
        }
    }
}
