using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class MySelectionRangeEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService wfes = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            if (wfes != null)
            {
                frmSelectionRange _frmSelectionRange = new frmSelectionRange();

                _frmSelectionRange.DateTimePicker1.Value = ((System.Windows.Forms.SelectionRange)value).Start;
                _frmSelectionRange.DateTimePicker2.Value = ((System.Windows.Forms.SelectionRange)value).End;
                _frmSelectionRange._wfes = wfes;

                wfes.DropDownControl(_frmSelectionRange);
                DateTime _DateTime1 = _frmSelectionRange.DateTimePicker1.Value;
                DateTime _DateTime2 = _frmSelectionRange.DateTimePicker2.Value;
                value = new System.Windows.Forms.SelectionRange(_DateTime1, _DateTime2);
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
    }

    public class frmSelectionRange : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DateTimePicker DateTimePicker1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.DateTimePicker DateTimePicker2;
        private Container components = null;

        public IWindowsFormsEditorService _wfes;

        public frmSelectionRange()
        {
            this.ClientSize = new Size(192, 70);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectionRange";
            this.ShowInTaskbar = false;
            this.Closed += FrmSelectionRange_Closed;

            label1 = new System.Windows.Forms.Label();
            label1.Parent = this;
            label1.Location = new Point(10, 10);
            label1.Text = "Начало";
            label1.Width = 55;

            DateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            DateTimePicker1.Parent = this;
            DateTimePicker1.Location = new Point(label1.Left + label1.Width + 5, label1.Top);
            DateTimePicker1.CustomFormat = "dd.MM.yyyy";
            DateTimePicker1.Format = DateTimePickerFormat.Custom;
            DateTimePicker1.Width = 100;

            label2 = new System.Windows.Forms.Label();
            label2.Parent = this;
            label2.Location = new Point(label1.Left, label1.Bottom + 5);
            label2.Text = "Конец";
            label2.Width = 55;

            DateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            DateTimePicker2.Parent = this;
            DateTimePicker2.Location = new Point(label2.Left + label2.Width + 5, label2.Top);
            DateTimePicker2.CustomFormat = "dd.MM.yyyy";
            DateTimePicker2.Format = DateTimePickerFormat.Custom;
            DateTimePicker2.Width = 100;

            TopLevel = false;
        }

        private void FrmSelectionRange_Closed(object sender, EventArgs e)
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
