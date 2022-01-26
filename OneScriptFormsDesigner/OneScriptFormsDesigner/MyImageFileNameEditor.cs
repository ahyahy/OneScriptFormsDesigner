using System;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace osfDesigner
{
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public class MyImageFileNameEditor : FileNameEditor
    {
        private System.Windows.Forms.OpenFileDialog _openFileDialog;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService wfes = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            if (wfes != null)
            {
                frmImageFileName _frmImageFileName = new frmImageFileName(value, osfDesigner.Program.pDesignerMainForm1.GetmainForm());
                _frmImageFileName._wfes = wfes;

                if (value == null)
                {
                    if (_openFileDialog == null)
                    {
                        _openFileDialog = new System.Windows.Forms.OpenFileDialog();
                        InitializeDialog(_openFileDialog);
                    }
                    if (value is string)
                    {
                        _openFileDialog.FileName = (string)value;
                    }
                    if (_openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        System.Drawing.Image Imag1 = System.Drawing.Image.FromFile(_openFileDialog.FileName);
                        Imag1.Tag = _openFileDialog.FileName;
                        return Imag1;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    System.Windows.Forms.DialogResult res1 = _frmImageFileName.ShowDialog();
                    if (res1 == System.Windows.Forms.DialogResult.OK)
                    {
                        if (_openFileDialog == null)
                        {
                            _openFileDialog = new System.Windows.Forms.OpenFileDialog();
                            InitializeDialog(_openFileDialog);
                        }
                        if (value is string)
                        {
                            _openFileDialog.FileName = (string)value;
                        }
                        if (_openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            System.Drawing.Image Imag1 = System.Drawing.Image.FromFile(_openFileDialog.FileName);
                            Imag1.Tag = _openFileDialog.FileName;
                            return Imag1;
                        }
                        else
                        {
                            return value;
                        }
                    }
                    else if (res1 == System.Windows.Forms.DialogResult.No)
                    {
                        return null;
                    }
                    else
                    {
                        return value;
                    }
                }
            }
            return null;
        }

        protected override void InitializeDialog(System.Windows.Forms.OpenFileDialog openFileDialog)
        {
            if (openFileDialog == null)
            {
                throw new ArgumentNullException(nameof(openFileDialog));
            }
            openFileDialog.Filter = "(*.BMP;*.JPG;*.GIF;*.ICO)|*.BMP;*.JPG;*.GIF;*.ICO";
            openFileDialog.Title = "Открыть";
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
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
                System.Drawing.Image img = (System.Drawing.Image)e.Value;
                if (img == null)
                {
                    return;
                }
                PaintValueEventArgs pi = new PaintValueEventArgs(e.Context, img, e.Graphics, e.Bounds);
                if (img != null)
                {
                    Rectangle r = e.Bounds;
                    r.Width--;
                    r.Height--;
                    e.Graphics.DrawRectangle(SystemPens.WindowFrame, r);
                    e.Graphics.DrawImage(img, e.Bounds);
                }
            }
            catch { }
        }
    }

    public class frmImageFileName : System.Windows.Forms.Form
    {
        private System.ComponentModel.Container components = null;
        public IWindowsFormsEditorService _wfes;
        private System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.Panel Panel3;
        private System.Windows.Forms.Button ButtonChange;
        private System.Windows.Forms.Button ButtonDel;

        public frmImageFileName(object value, System.Windows.Forms.Control mainForm)
        {
            this.Text = "Текущее изображение";
            this.Name = "frmImageFileName";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.Closed += frmImageFileName_Closed;
            this.MinimumSize = new System.Drawing.Size(300, 150);
            this.Width = 700;
            this.Height = 200;
            this.Location = new Point(mainForm.Left + ((mainForm.Width / 2) - (700 / 2)), mainForm.Top + ((mainForm.Height / 2) - (200 / 2)));
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;

            // панель с ListView1
            Panel1 = new System.Windows.Forms.Panel();
            Panel1.Parent = this;
            Panel1.Dock = System.Windows.Forms.DockStyle.Fill;

            // панель с ButtonChange и ButtonDel
            Panel2 = new System.Windows.Forms.Panel();
            Panel2.Parent = this;
            Panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            Panel2.Height = 32;

            // панель с ButtonChange и ButtonDel
            Panel3 = new System.Windows.Forms.Panel();
            Panel3.Parent = Panel2;
            Panel3.Dock = System.Windows.Forms.DockStyle.Right;
            Panel3.Width = 238;

            ButtonChange = new System.Windows.Forms.Button();
            ButtonChange.Parent = Panel3;
            ButtonChange.Bounds = new System.Drawing.Rectangle(6, 5, 110, 22);
            ButtonChange.Text = "Изменить";
            ButtonChange.Click += ButtonChange_Click;

            ButtonDel = new System.Windows.Forms.Button();
            ButtonDel.Parent = Panel3;
            ButtonDel.Bounds = new System.Drawing.Rectangle(122, 5, 110, 22);
            ButtonDel.Text = "Удалить";
            ButtonDel.Click += ButtonDel_Click;

            System.Windows.Forms.ListView ListView1 = new System.Windows.Forms.ListView();
            ListView1.Parent = Panel1;
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

            if (value != null)
            {
                Image Image1 = value as Image;
                int HorizontalResolution = Convert.ToInt32(Image1.HorizontalResolution);
                int PhysicalDimensionWidth = Convert.ToInt32(Image1.PhysicalDimension.Width);
                int PhysicalDimensionHeight = Convert.ToInt32(Image1.PhysicalDimension.Height);
                System.Drawing.Imaging.PixelFormat PixelFormat = Image1.PixelFormat;
                System.Drawing.Imaging.ImageFormat RawFormat = Image1.RawFormat;
                int SizeWidth = Image1.Size.Width;
                int SizeHeight = Image1.Size.Height;
                object Tag = Image1.Tag;
                int VerticalResolution = Convert.ToInt32(Image1.VerticalResolution);

                System.Windows.Forms.ListViewItem ListViewItem1 = new System.Windows.Forms.ListViewItem();
                System.Windows.Forms.ListViewItem.ListViewSubItemCollection SubItems1 = ListViewItem1.SubItems;
                ListViewItem1.Text = "ГоризонтальноеРазрешение (HorizontalResolution)";
                SubItems1.Add("" + HorizontalResolution);
                Items1.Add(ListViewItem1);

                System.Windows.Forms.ListViewItem ListViewItem2 = new System.Windows.Forms.ListViewItem();
                System.Windows.Forms.ListViewItem.ListViewSubItemCollection SubItems2 = ListViewItem2.SubItems;
                ListViewItem2.Text = "ФизическийРазмер (PhysicalDimension)";
                SubItems2.Add("{ Ширина = " + PhysicalDimensionWidth + ", Высота = " + PhysicalDimensionHeight + "}");
                Items1.Add(ListViewItem2);

                System.Windows.Forms.ListViewItem ListViewItem3 = new System.Windows.Forms.ListViewItem();
                System.Windows.Forms.ListViewItem.ListViewSubItemCollection SubItems3 = ListViewItem3.SubItems;
                ListViewItem3.Text = "ФорматПикселей (PixelFormat)";
                SubItems3.Add("" + PixelFormat);
                Items1.Add(ListViewItem3);

                System.Windows.Forms.ListViewItem ListViewItem4 = new System.Windows.Forms.ListViewItem();
                System.Windows.Forms.ListViewItem.ListViewSubItemCollection SubItems4 = ListViewItem4.SubItems;
                ListViewItem4.Text = "ФорматФайлаИзображения (RawFormat)";
                SubItems4.Add("" + RawFormat);
                Items1.Add(ListViewItem4);

                System.Windows.Forms.ListViewItem ListViewItem5 = new System.Windows.Forms.ListViewItem();
                System.Windows.Forms.ListViewItem.ListViewSubItemCollection SubItems5 = ListViewItem5.SubItems;
                ListViewItem5.Text = "Размер (Size)";
                SubItems5.Add("" + "{ Ширина = " + SizeWidth + ", Высота = " + SizeHeight + "}");
                Items1.Add(ListViewItem5);

                System.Windows.Forms.ListViewItem ListViewItem6 = new System.Windows.Forms.ListViewItem();
                System.Windows.Forms.ListViewItem.ListViewSubItemCollection SubItems6 = ListViewItem6.SubItems;
                ListViewItem6.Text = "Метка (Tag)";
                SubItems6.Add("" + Tag);
                Items1.Add(ListViewItem6);

                System.Windows.Forms.ListViewItem ListViewItem7 = new System.Windows.Forms.ListViewItem();
                System.Windows.Forms.ListViewItem.ListViewSubItemCollection SubItems7 = ListViewItem7.SubItems;
                ListViewItem7.Text = "ВертикальноеРазрешение (VerticalResolution)";
                SubItems7.Add("" + VerticalResolution);
                Items1.Add(ListViewItem7);
            }
        }

        private void ButtonDel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            Close();
        }

        private void ButtonChange_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void frmImageFileName_Closed(object sender, EventArgs e)
        {
            _wfes.CloseDropDown();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}
