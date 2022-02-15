using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class MyCollectionEditor : CollectionEditor
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

        // Унаследуйте конструктор по умолчанию из стандартного редактора коллекций.
        public MyCollectionEditor(Type type) : base(type)
        {
        }

        // Переопределите этот метод, чтобы получить доступ к форме редактора коллекции. 
        protected override CollectionForm CreateCollectionForm()
        {
            // Получение макета редактора коллекции по умолчанию.
            collectionForm = base.CreateCollectionForm();
            collectionForm.Text = "Редактор коллекции Изображения";
            collectionForm.Shown += CollectionForm_Shown;
            collectionForm.FormClosed += CollectionForm_FormClosed;

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
                        ListBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
                    }
                    // Получите ссылку на внутреннюю сетку свойств и подключите к ней обработчик событий.
                    if (i == 5)
                    {
                        PropertyGrid1 = (System.Windows.Forms.PropertyGrid)TableLayoutPanel1.Controls[5];
                        PropertyGrid1.SelectedGridItemChanged += PropertyGrid1_SelectedGridItemChanged;
                        PropertyGrid1.PropertyValueChanged += PropertyGrid1_PropertyValueChanged;

                        // Также сделайте доступным окно с подсказками по параметрам в нижней части.
                        PropertyGrid1.HelpVisible = true;
                        PropertyGrid1.HelpBackColor = SystemColors.Info;
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

        private void CollectionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            OneScriptFormsDesigner.SetDesignSurfaceState();
        }

        private void CollectionForm_Shown(object sender, EventArgs e)
        {
            PropertiesLabel1.Text = "Свойства:";
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertiesLabel1.Text = "Свойства:";
        }

        private void PropertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            PropertiesLabel1.Text = "Свойства:";
        }

        private void ButtonRemove1_Click(object sender, EventArgs e)
        {
            ImageList ImageList1 = (ImageList)this.Context.Instance;
            MyList MyList1 = ImageList1.Images;
            ImageList1.Images.Clear();
            MethodInfo MethodInfo3 = collectionForm.GetType().GetMethod("AddItems", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo3.Invoke(collectionForm, new object[] { MyList1 });
        }

        private void ButtonDown1_Click(object sender, EventArgs e)
        {
            object SelectedItem1 = ListBox1.SelectedItem;
            ImageList ImageList1 = (ImageList)this.Context.Instance;
            MyList MyList1 = ImageList1.Images;
            ImageList1.Images.Clear();
            MethodInfo MethodInfo3 = collectionForm.GetType().GetMethod("AddItems", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo3.Invoke(collectionForm, new object[] { MyList1 });
            ListBox1.SelectedItem = SelectedItem1;
            if (ListBox1.SelectedIndex != (ListBox1.Items.Count - 1))
            {
                ListBox1.SetSelected(ListBox1.Items.Count - 1, false);
            }
            PropertiesLabel1.Text = "Свойства:";
        }

        private void ButtonUp1_Click(object sender, EventArgs e)
        {
            object SelectedItem1 = ListBox1.SelectedItem;
            ImageList ImageList1 = (ImageList)this.Context.Instance;
            MyList MyList1 = ImageList1.Images;
            ImageList1.Images.Clear();
            MethodInfo MethodInfo3 = collectionForm.GetType().GetMethod("AddItems", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo3.Invoke(collectionForm, new object[] { MyList1 });
            ListBox1.SelectedItem = SelectedItem1;
            if (ListBox1.SelectedIndex != (ListBox1.Items.Count - 1))
            {
                ListBox1.SetSelected(ListBox1.Items.Count - 1, false);
            }
        }

        void PropertyGrid1_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
        }

        private void ButtonAdd1_Click(object sender, EventArgs e)
        {
            MethodInfo MethodInfo1 = collectionForm.GetType().GetMethod("PerformRemove", BindingFlags.NonPublic | BindingFlags.Instance);

            if (ListBox1.Items.Count > 0)
            {
                MethodInfo1.Invoke(collectionForm, new object[] { });
            }
            System.Windows.Forms.OpenFileDialog OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            OpenFileDialog1.Filter = "Файлы изображений (*.BMP;*.jpg;*.GIF)|*.BMP;*.jpg;*.GIF; *.ICO";
            OpenFileDialog1.Multiselect = true;
            if (OpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            else
            {
                MyList MyList1 = new MyList();
                osfDesigner.ImageList ImageList1 = (ImageList)this.Context.Instance;
                System.Windows.Forms.ImageList.ImageCollection ImageCollection1 = ImageList1.OriginalObj.Images;

                for (int i = 0; i < OpenFileDialog1.FileNames.Length; i++)
                {
                    ImageEntry ImageEntry1 = new ImageEntry();
                    ImageEntry1.Image = new Bitmap("" + OpenFileDialog1.FileNames[i]);
                    ImageEntry1.Path = OpenFileDialog1.FileNames[i];
                    ImageEntry1.FileName = OpenFileDialog1.SafeFileNames[i];
                    MyList1.Add(ImageEntry1);
                    ImageCollection1.Add(ImageEntry1.Image);
                }
                MethodInfo MethodInfo2 = collectionForm.GetType().GetMethod("AddItems", BindingFlags.NonPublic | BindingFlags.Instance);
                MethodInfo2.Invoke(collectionForm, new object[] { MyList1 });
            }
        }

        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index != -1)
            {
                ListItem ListItem1 = new ListItem(ListBox1.Items[e.Index]);
                string FileName = "";
                try
                {
                    ImageEntry ImageEntry1 = ((ImageList)this.Context.Instance).Images[e.Index];
                    ListItem1.Value = ImageEntry1.M_Bitmap;
                    FileName = ImageEntry1.FileName;
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

                Rectangle res = new Rectangle(e.Bounds.X + offset, 
                                            e.Bounds.Y,
                                            e.Bounds.Width - offset,
                                            e.Bounds.Height);
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

                    PaintValueEventArgs PaintValueEventArgs1 = new PaintValueEventArgs(this.Context, ListItem1.Value, Graphics1, Rectangle2);
                    this.PaintValue(PaintValueEventArgs1);
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
                string itemText = FileName;
                try
                {
                    Graphics1.DrawString(itemText, 
                        ListBox1.Font, 
                        textBrush, 
                        new Rectangle(e.Bounds.X + offset, e.Bounds.Y, e.Bounds.Width - offset, e.Bounds.Height));
                }

                finally
                {
                    textBrush?.Dispose();
                }

                // Проверьте, нужно ли нам изменять горизонтальный экстент списка.
                int width = offset + (int)Graphics1.MeasureString(itemText, ListBox1.Font).Width;
                if (width > e.Bounds.Width && ListBox1.HorizontalExtent < width)
                {
                    ListBox1.HorizontalExtent = width;
                }
            }
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            try
            {
                if (e == null || e.Value == null)
                {
                    return;
                }
                System.Drawing.Image Image1 = (System.Drawing.Image)e.Value;
                if (Image1 == null)
                {
                    return;
                }
                if (Image1 != null)
                {
                    Rectangle Rectangle1 = e.Bounds;
                    Rectangle1.Width--;
                    Rectangle1.Height--;
                    e.Graphics.DrawRectangle(SystemPens.WindowFrame, Rectangle1);
                    e.Graphics.DrawImage(Image1, e.Bounds);
                }
            }
            catch { }
        }
    }
}
