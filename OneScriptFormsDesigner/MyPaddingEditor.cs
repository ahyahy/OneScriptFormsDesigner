using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class MyPaddingEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService wfes = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            if (wfes != null)
            {
                frmPadding _frmPadding = new frmPadding();

                _frmPadding.numericUpDown1.Value = ((Padding)value).Left;
                _frmPadding.numericUpDown2.Value = ((Padding)value).Top;
                _frmPadding.numericUpDown3.Value = ((Padding)value).Right;
                _frmPadding.numericUpDown4.Value = ((Padding)value).Bottom;
                _frmPadding._wfes = wfes;

                wfes.DropDownControl(_frmPadding);
                int _Left = Convert.ToInt32(_frmPadding.numericUpDown1.Value);
                int _Top = Convert.ToInt32(_frmPadding.numericUpDown2.Value);
                int _Right = Convert.ToInt32(_frmPadding.numericUpDown3.Value);
                int _Bottom = Convert.ToInt32(_frmPadding.numericUpDown4.Value);
                value = new Padding(_Left, _Top, _Right, _Bottom);
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }

    public class frmPadding : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown numericUpDown1;
        public System.Windows.Forms.NumericUpDown numericUpDown2;
        public System.Windows.Forms.NumericUpDown numericUpDown3;
        public System.Windows.Forms.NumericUpDown numericUpDown4;
        private Container components = null;

        public IWindowsFormsEditorService _wfes;

        public frmPadding()
        {
            ClientSize = new Size(192, 130);
            ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmPadding";
            ShowInTaskbar = false;
            Closed += FrmWidthHeight_Closed;

            label1 = new System.Windows.Forms.Label();
            label1.Parent = this;
            label1.Location = new Point(10, 10);
            label1.Text = "Лево";
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
            label2.Text = "Верх";
            label2.Width = 55;

            numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            numericUpDown2.Parent = this;
            numericUpDown2.Location = new Point(label2.Left + label2.Width + 5, label2.Top);
            numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            numericUpDown2.ThousandsSeparator = true;
            numericUpDown2.Minimum = 0;
            numericUpDown2.Maximum = 4800;

            label3 = new System.Windows.Forms.Label();
            label3.Parent = this;
            label3.Location = new Point(label2.Left, label2.Bottom + 5);
            label3.Text = "Право";
            label3.Width = 55;

            numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            numericUpDown3.Parent = this;
            numericUpDown3.Location = new Point(label3.Left + label3.Width + 5, label3.Top);
            numericUpDown3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            numericUpDown3.ThousandsSeparator = true;
            numericUpDown3.Minimum = 0;
            numericUpDown3.Maximum = 4800;

            label4 = new System.Windows.Forms.Label();
            label4.Parent = this;
            label4.Location = new Point(label3.Left, label3.Bottom + 5);
            label4.Text = "Низ";
            label4.Width = 55;

            numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            numericUpDown4.Parent = this;
            numericUpDown4.Location = new Point(label4.Left + label4.Width + 5, label4.Top);
            numericUpDown4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            numericUpDown4.ThousandsSeparator = true;
            numericUpDown4.Minimum = 0;
            numericUpDown4.Maximum = 4800;

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
