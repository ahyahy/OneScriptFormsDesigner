using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class MyToolBarButtonCollectionEditor : CollectionEditor
    {
        private CollectionForm collectionForm;
        private System.Windows.Forms.Form frmCollectionEditorForm;
        private TableLayoutPanel TableLayoutPanel1;
        private TableLayoutPanel AddRemoveTableLayoutPanel1;
        private System.Windows.Forms.Label PropertiesLabel1 = null;
        private System.Windows.Forms.Label MembersLabel1 = null;
        private System.Windows.Forms.ListBox ListBox1;
        private System.Windows.Forms.PropertyGrid PropertyGrid1;
        private TableLayoutPanel OkCancelTableLayoutPanel1;
        private System.Windows.Forms.Button ButtonOk1 = null;
        private System.Windows.Forms.Button ButtonCancel1 = null;
        private System.Windows.Forms.Button ButtonAdd1 = null;
        private System.Windows.Forms.Button ButtonRemove1 = null;
        private System.Windows.Forms.Button ButtonUp1 = null;
        private System.Windows.Forms.Button ButtonDown1 = null;
        private System.Windows.Forms.PropertyGrid TopLevelPropertyGrid1;
        private ToolBar ToolBar1;

        public MyToolBarButtonCollectionEditor(Type type) : base(type)
        {
        }

        protected override CollectionForm CreateCollectionForm()
        {
            collectionForm = base.CreateCollectionForm();
            ToolBar1 = (ToolBar)Context.Instance;
            collectionForm.Text = "Редактор коллекции Кнопки";
            collectionForm.Shown += CollectionForm_Shown;
            collectionForm.FormClosed += CollectionForm_FormClosed;

            frmCollectionEditorForm = collectionForm;
            TableLayoutPanel1 = (TableLayoutPanel)frmCollectionEditorForm.Controls[0];
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
                        AddRemoveTableLayoutPanel1 = (TableLayoutPanel)TableLayoutPanel1.Controls[1];
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
                        ListBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
                    }
                    if (i == 5)
                    {
                        PropertyGrid1 = (System.Windows.Forms.PropertyGrid)TableLayoutPanel1.Controls[5];
                        PropertyGrid1.SelectedGridItemChanged += PropertyGrid1_SelectedGridItemChanged;
                        PropertyGrid1.SelectedObjectsChanged += PropertyGrid1_SelectedObjectsChanged;
                        PropertyGrid1.HelpVisible = true;
                        PropertyGrid1.HelpBackColor = SystemColors.Info;
                    }
                    if (i == 6)
                    {
                        OkCancelTableLayoutPanel1 = (TableLayoutPanel)TableLayoutPanel1.Controls[6];
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

        private void CollectionForm_Shown(object sender, EventArgs e)
        {
            TopLevelPropertyGrid1 = OneScriptFormsDesigner.PropertyGrid;
            PropertiesLabel1.Text = "Свойства:";
        }

        private void CollectionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            OneScriptFormsDesigner.SetDesignSurfaceState();
        }

        private void PropertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            PropertiesLabel1.Text = "Свойства:";
        }

        private void PropertyGrid1_SelectedObjectsChanged(object sender, EventArgs e)
        {
            if (PropertyGrid1.SelectedObject != null)
            {
                if (PropertyGrid1.SelectedObject.GetType() != typeof(osfDesigner.ToolBarButton))
                {
                    System.Windows.Forms.ToolBarButton OriginalObj = (System.Windows.Forms.ToolBarButton)PropertyGrid1.SelectedObject;
                    if (OriginalObj.Tag == null)
                    {
                        osfDesigner.ToolBarButton SimilarObj = new osfDesigner.ToolBarButton();
                        OneScriptFormsDesigner.PassProperties(OriginalObj, SimilarObj); // Передадим свойства.
                        SimilarObj.OriginalObj = OriginalObj;
                        SimilarObj.Parent = OriginalObj.Parent;
                        SimilarObj.Style = (osfDesigner.ToolBarButtonStyle)OriginalObj.Style;

                        OriginalObj.Tag = SimilarObj;
                        PropertyGrid1.SelectedObject = SimilarObj;
                        PropertiesLabel1.Text = "Свойства:";
                    }
                    else
                    {
                        osfDesigner.ToolBarButton SimilarObj = (osfDesigner.ToolBarButton)OriginalObj.Tag;
                        PropertyGrid1.SelectedObject = SimilarObj;
                    }
                }
            }
        }

        private void ButtonRemove1_Click(object sender, EventArgs e)
        {
            int index = ListBox1.SelectedIndex;
            ToolBar1.Buttons.Clear();
            MethodInfo MethodInfo3 = collectionForm.GetType().GetMethod("AddItems", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo3.Invoke(collectionForm, new object[] { ToolBar1.Buttons });
            object SelectedItem1 = ListBox1.SelectedItem;
            ListBox1.SelectedItem = null;
            ListBox1.SelectedIndex = index;
            collectionForm.Refresh();
        }

        private void ButtonDown1_Click(object sender, EventArgs e)
        {
            object SelectedItem1 = ListBox1.SelectedItem;
            ToolBar1.Buttons.Clear();
            MethodInfo MethodInfo3 = collectionForm.GetType().GetMethod("AddItems", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo3.Invoke(collectionForm, new object[] { ToolBar1.Buttons });
            ListBox1.SelectedItem = SelectedItem1;
            if (ListBox1.SelectedIndex != (ListBox1.Items.Count - 1))
            {
                ListBox1.SetSelected(ListBox1.Items.Count - 1, false);
            }
            PropertiesLabel1.Text = "Свойства:";
            collectionForm.Refresh();
        }

        private void ButtonUp1_Click(object sender, EventArgs e)
        {
            object SelectedItem1 = ListBox1.SelectedItem;
            ToolBar1.Buttons.Clear();
            MethodInfo MethodInfo3 = collectionForm.GetType().GetMethod("AddItems", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo3.Invoke(collectionForm, new object[] { ToolBar1.Buttons });
            ListBox1.SelectedItem = SelectedItem1;
            if (ListBox1.SelectedIndex != (ListBox1.Items.Count - 1))
            {
                ListBox1.SetSelected(ListBox1.Items.Count - 1, false);
            }
            collectionForm.Refresh();
        }

        private void ButtonAdd1_Click(object sender, EventArgs e)
        {
            osfDesigner.ToolBarButton SimilarObj = (osfDesigner.ToolBarButton)PropertyGrid1.SelectedObject;
            SimilarObj.Name = OneScriptFormsDesigner.RevertToolBarButtonName(ToolBar1);
            SimilarObj.Text = "Кн" + OneScriptFormsDesigner.ParseBetween(SimilarObj.Name, "Кн", null);
            ListBox1.Refresh();

            ToolBar1.Buttons.Clear();
            MethodInfo MethodInfo3 = collectionForm.GetType().GetMethod("AddItems", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo3.Invoke(collectionForm, new object[] { ToolBar1.Buttons });

            PropertyGrid1.SelectedObject = SimilarObj;

            collectionForm.Refresh();
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertiesLabel1.Text = "Свойства:";
        }

        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index != -1)
            {
                ListItem ListItem1 = new ListItem(ListBox1.Items[e.Index]);
                string ButtonText = "";
                try
                {
                    System.Windows.Forms.ToolBarButton ToolBarButton1 = ToolBar1.Buttons[e.Index];
                    ListItem1.Value = ToolBarButton1;
                    if (ToolBarButton1.Text == "")
                    {
                        ButtonText = "Кнопка";
                    }
                    else
                    {
                        ButtonText = ToolBarButton1.Text;
                    }
                }
                catch { }
                Graphics Graphics1 = e.Graphics;

                int Count1 = ListBox1.Items.Count;
                int maxCount1 = Count1;
                if (Count1 > 1)
                {
                    maxCount1 = Count1 - 1;
                }
                SizeF sizeW = Graphics1.MeasureString(maxCount1.ToString(CultureInfo.CurrentCulture), ListBox1.Font);

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

                if (this != null && GetPaintValueSupported())
                {
                    Rectangle Rectangle2 = new Rectangle(e.Bounds.X + offset, e.Bounds.Y + 1, 20, e.Bounds.Height - 3);
                    Graphics1.DrawRectangle(SystemPens.ControlText,
                        Rectangle2.X,
                        Rectangle2.Y,
                        Rectangle2.Width - 1,
                        Rectangle2.Height - 1);
                    Rectangle2.Inflate(-1, -1);

                    PaintValueEventArgs PaintValueEventArgs1 = new PaintValueEventArgs(Context, ListItem1.Value, Graphics1, Rectangle2);
                    PaintValue(PaintValueEventArgs1);
                    offset += 26 + 1;
                }

                StringFormat StringFormat1 = new StringFormat();
                try
                {
                    StringFormat1.Alignment = StringAlignment.Center;
                    Graphics1.DrawString(e.Index.ToString(CultureInfo.CurrentCulture),
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

                Graphics1.DrawString(ButtonText, ListBox1.Font, textBrush, new Rectangle(e.Bounds.X + offset, e.Bounds.Y, e.Bounds.Width - offset, e.Bounds.Height));
                textBrush.Dispose();
            }
        }
    }
}
