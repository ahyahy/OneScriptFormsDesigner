using System;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace osfDesigner
{
    public class MyMaximumSizeEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService wfes = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            if (wfes != null)
            {
                frmMaxWidthHeight _frmMaxWidthHeight = new frmMaxWidthHeight();

                _frmMaxWidthHeight.numericUpDown1.Value = (int)((System.Drawing.Size)value).Width;
                _frmMaxWidthHeight.numericUpDown2.Value = (int)((System.Drawing.Size)value).Height;
                _frmMaxWidthHeight._wfes = wfes;

                wfes.DropDownControl(_frmMaxWidthHeight);
                int _Width = Convert.ToInt32(_frmMaxWidthHeight.numericUpDown1.Value);
                int _Height = Convert.ToInt32(_frmMaxWidthHeight.numericUpDown2.Value);
                value = new System.Drawing.Size(_Width, _Height);
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            // Вызов модального диалогового окна, в котором запрашивается ввод пользователем данных.
            return UITypeEditorEditStyle.Modal;
        }
    }

    public class frmMaxWidthHeight : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.ComponentModel.Container components = null;

        public IWindowsFormsEditorService _wfes;

        public frmMaxWidthHeight()
        {
            this.ClientSize = new System.Drawing.Size(192, 70);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMaxWidthHeight";
            this.ShowInTaskbar = false;
            this.Closed += FrmMaxWidthHeight_Closed;

            label1 = new System.Windows.Forms.Label();
            label1.Parent = this;
            label1.Location = new System.Drawing.Point(10, 10);
            label1.Text = "Ширина";
            label1.Width = 55;

            numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            numericUpDown1.Parent = this;
            numericUpDown1.Location = new System.Drawing.Point(label1.Left + label1.Width + 5, label1.Top);
            numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            numericUpDown1.ThousandsSeparator = true;
            numericUpDown1.Minimum = 0;
            numericUpDown1.Maximum = 7680;

            label2 = new System.Windows.Forms.Label();
            label2.Parent = this;
            label2.Location = new System.Drawing.Point(label1.Left, label1.Bottom + 5);
            label2.Text = "Высота";
            label2.Width = 55;

            numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            numericUpDown2.Parent = this;
            numericUpDown2.Location = new System.Drawing.Point(label2.Left + label2.Width + 5, label2.Top);
            numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            numericUpDown2.ThousandsSeparator = true;
            numericUpDown2.Minimum = 0;
            numericUpDown2.Maximum = 4800;

            TopLevel = false;
        }

        private void FrmMaxWidthHeight_Closed(object sender, EventArgs e)
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
