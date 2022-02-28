using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class MyColorEditor : ColorEditor
    {
        ColorEditor editor;

        public MyColorEditor()
        {
            editor = new ColorEditor();
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Type colorUiType = typeof(ColorEditor).GetNestedType("ColorUI", BindingFlags.NonPublic);
            var colorUiConstructor = colorUiType.GetConstructors()[0];
            var colorUiField = typeof(ColorEditor).GetField("colorUI", BindingFlags.Instance | BindingFlags.NonPublic);
            var colorUiObject = colorUiConstructor.Invoke(new[] { editor }) as Control;
            colorUiField.SetValue(editor, colorUiObject);
            var container = colorUiObject.Controls[0];
            var tab1 = container.Controls[0];
            tab1.Text = "Пользовательский";
            var tab2 = container.Controls[1];
            tab2.Text = "Интернет";
            var tab3 = container.Controls[2];
            tab3.Text = "Системный";
            container.Controls.Add(new System.Windows.Forms.TabPage());
            var tab4 = container.Controls[3];
            tab4.Text = "Свойства";
            System.Windows.Forms.ListView ListView1 = new System.Windows.Forms.ListView();
            tab4.Controls.Add(ListView1);
            ListView1.View = System.Windows.Forms.View.Details;
            ListView1.LabelEdit = false;
            ListView1.GridLines = true;
            ListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            System.Windows.Forms.ListView.ColumnHeaderCollection Columns1 = ListView1.Columns;
            System.Windows.Forms.ColumnHeader ColumnHeader1 = new System.Windows.Forms.ColumnHeader();
            System.Windows.Forms.ColumnHeader ColumnHeader2 = new System.Windows.Forms.ColumnHeader();
            ColumnHeader1.Width = -1;
            ColumnHeader2.Width = -1;
            Columns1.Add(ColumnHeader1);
            Columns1.Add(ColumnHeader2);
            System.Windows.Forms.ListView.ListViewItemCollection Items1 = ListView1.Items;
            Color Color1 = (Color)value;

            System.Windows.Forms.ListViewItem  ListViewItem1 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem.ListViewSubItemCollection SubItems1 = ListViewItem1.SubItems;
            ListViewItem1.Text = "A";
            SubItems1.Add(Color1.A.ToString());
            Items1.Add(ListViewItem1);

            System.Windows.Forms.ListViewItem  ListViewItem2 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem.ListViewSubItemCollection SubItems2 = ListViewItem2.SubItems;
            ListViewItem2.Text = "B";
            SubItems2.Add(Color1.B.ToString());
            Items1.Add(ListViewItem2);

            System.Windows.Forms.ListViewItem  ListViewItem3 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem.ListViewSubItemCollection SubItems3 = ListViewItem3.SubItems;
            ListViewItem3.Text = "G";
            SubItems3.Add(Color1.G.ToString());
            Items1.Add(ListViewItem3);

            System.Windows.Forms.ListViewItem  ListViewItem4 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem.ListViewSubItemCollection SubItems4 = ListViewItem4.SubItems;
            ListViewItem4.Text = "Пусто";
            if (Color1.IsEmpty)
            {
                SubItems4.Add("Истина");
            }
            else
            {
                SubItems4.Add("Ложь");
            }
            Items1.Add(ListViewItem4);

            System.Windows.Forms.ListViewItem  ListViewItem5 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem.ListViewSubItemCollection SubItems5 = ListViewItem5.SubItems;
            ListViewItem5.Text = "Предопределенный";
            if (Color1.IsKnownColor)
            {
                SubItems5.Add("Истина");
            }
            else
            {
                SubItems5.Add("Ложь");
            }
            Items1.Add(ListViewItem5);

            System.Windows.Forms.ListViewItem  ListViewItem6 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem.ListViewSubItemCollection SubItems6 = ListViewItem6.SubItems;
            ListViewItem6.Text = "Именованный";
            if (Color1.IsNamedColor)
            {
                SubItems6.Add("Истина");
            }
            else
            {
                SubItems6.Add("Ложь");
            }
            Items1.Add(ListViewItem6);

            System.Windows.Forms.ListViewItem  ListViewItem7 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem.ListViewSubItemCollection SubItems7 = ListViewItem7.SubItems;
            ListViewItem7.Text = "Системный";
            if (Color1.IsSystemColor)
            {
                SubItems7.Add("Истина");
            }
            else
            {
                SubItems7.Add("Ложь");
            }
            Items1.Add(ListViewItem7);

            System.Windows.Forms.ListViewItem  ListViewItem8 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem.ListViewSubItemCollection SubItems8 = ListViewItem8.SubItems;
            ListViewItem8.Text = "Имя";
            SubItems8.Add(Color1.Name);
            Items1.Add(ListViewItem8);

            System.Windows.Forms.ListViewItem  ListViewItem9 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem.ListViewSubItemCollection SubItems9 = ListViewItem9.SubItems;
            ListViewItem9.Text = "R";
            SubItems9.Add(Color1.R.ToString());
            Items1.Add(ListViewItem9);

            Control Control1 = tab1.Controls[0];
            Control Control2 = tab2.Controls[0];
            Control Control3 = tab3.Controls[0];

            System.Windows.Forms.ListBox ListBox1 = (System.Windows.Forms.ListBox)Control2;
            ListBox1.DrawItem += ListBox1_DrawItem;

            System.Windows.Forms.ListBox ListBox2 = (System.Windows.Forms.ListBox)Control3;
            ListBox2.DrawItem += ListBox2_DrawItem;
	
            int i = 0;
            while (i <= ListBox2.Items.Count - 1)
            {
                Color item = (Color)ListBox2.Items[i];
                string item2Name = item.Name;
                try
                {
                    item2Name = OneScriptFormsDesigner.colors[item2Name];
                    i = i + 1;
                }
                catch
                {
                    ListBox2.Items.Remove(item);
                }
            }

            return editor.EditValue(context, provider, value);
        }

        private void ListBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            System.Windows.Forms.ListBox listbox2 = (System.Windows.Forms.ListBox)sender;
            object item2 = listbox2.Items[e.Index];
            Font font2 = ((Control)sender).Font;
            listbox2.ItemHeight = listbox2.Font.Height;

            Graphics graphics = e.Graphics;
            e.DrawBackground();

            editor.PaintValue(item2, graphics, new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 22, e.Bounds.Height - 4));
            graphics.DrawRectangle(SystemPens.WindowText, new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 22 - 1, e.Bounds.Height - 4 - 1));
            Brush foreBrush2 = new SolidBrush(e.ForeColor);

            string item2Name = ((Color)item2).Name;
            try
            {
                item2Name = OneScriptFormsDesigner.colors[item2Name];
            }
            catch { }

            graphics.DrawString(item2Name, font2, foreBrush2, e.Bounds.X + 26, e.Bounds.Y);
            foreBrush2.Dispose();
        }

        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            System.Windows.Forms.ListBox listbox1 = (System.Windows.Forms.ListBox)sender;
            object item1 = listbox1.Items[e.Index];
            Font font1 = ((Control)sender).Font;
            listbox1.ItemHeight = listbox1.Font.Height;

            Graphics graphics = e.Graphics;
            e.DrawBackground();

            editor.PaintValue(item1, graphics, new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 22, e.Bounds.Height - 4));
            graphics.DrawRectangle(SystemPens.WindowText, new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 22 - 1, e.Bounds.Height - 4 - 1));
            Brush foreBrush1 = new SolidBrush(e.ForeColor);

            string item1Name = ((Color)item1).Name;
            try
            {
                item1Name = OneScriptFormsDesigner.colors[item1Name];
            }
            catch { }

            graphics.DrawString(item1Name, font1, foreBrush1, e.Bounds.X + 26, e.Bounds.Y);
            foreBrush1.Dispose();
        }
    }
}
