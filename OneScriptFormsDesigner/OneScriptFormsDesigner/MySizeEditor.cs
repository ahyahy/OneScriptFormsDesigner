using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Windows.Forms.Design;
using System;

namespace osfDesigner
{
    public class MySizeEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService wfes = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            if (wfes != null)
            {
                frmWidthHeight _frmWidthHeight = new frmWidthHeight();

                _frmWidthHeight.numericUpDown1.Value = ((Size)value).Width;
                _frmWidthHeight.numericUpDown2.Value = ((Size)value).Height;
                _frmWidthHeight._wfes = wfes;

                wfes.DropDownControl(_frmWidthHeight);
                int _Width = Convert.ToInt32(_frmWidthHeight.numericUpDown1.Value);
                int _Height = Convert.ToInt32(_frmWidthHeight.numericUpDown2.Value);
                value = new Size(_Width, _Height);
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }

    public class frmWidthHeight : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDown2;
        private Container components = null;

        public IWindowsFormsEditorService _wfes;

        public frmWidthHeight()
        {
            this.ClientSize = new Size(192, 70);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWidthHeight";
            this.ShowInTaskbar = false;
            this.Closed += FrmWidthHeight_Closed;

            label1 = new System.Windows.Forms.Label();
            label1.Parent = this;
            label1.Location = new Point(10, 10);
            label1.Text = "Ширина";
            label1.Width = 55;

            numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            numericUpDown1.Parent = this;
            numericUpDown1.Location = new Point(label1.Left + label1.Width + 5, label1.Top);
            numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            numericUpDown1.ThousandsSeparator = true;
            numericUpDown1.Minimum = 0;
            numericUpDown1.Maximum = 7680;

            label2 = new System.Windows.Forms.Label();
            label2.Parent = this;
            label2.Location = new Point(label1.Left, label1.Bottom + 5);
            label2.Text = "Высота";
            label2.Width = 55;

            numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            numericUpDown2.Parent = this;
            numericUpDown2.Location = new Point(label2.Left + label2.Width + 5, label2.Top);
            numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            numericUpDown2.ThousandsSeparator = true;
            numericUpDown2.Minimum = 0;
            numericUpDown2.Maximum = 4800;

            TopLevel = false;
        }

        private void FrmWidthHeight_Closed(object sender, EventArgs e)
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
