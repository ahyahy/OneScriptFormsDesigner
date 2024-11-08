using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace osfDesigner
{
    public class MyDateTimeEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService wfes = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            if (wfes != null)
            {
                frmDateTime _frmDateTime = new frmDateTime();

                _frmDateTime.DateTimePicker1.Value = System.DateTime.Parse((string)value);
                _frmDateTime._wfes = wfes;
                _frmDateTime.Show();

                wfes.DropDownControl(_frmDateTime);
                DateTime _DateTime1 = _frmDateTime.DateTimePicker1.Value;
                value = _DateTime1.ToString();
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
    }

    public class frmDateTime : System.Windows.Forms.Form
    {
        public System.Windows.Forms.DateTimePicker DateTimePicker1;
        private Container components = null;
        public IWindowsFormsEditorService _wfes;

        public frmDateTime()
        {
            DateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            DateTimePicker1.Parent = this;
            DateTimePicker1.Dock = System.Windows.Forms.DockStyle.Fill;
            DateTimePicker1.CustomFormat = "dd.MM.yyyy";
            DateTimePicker1.Format = DateTimePickerFormat.Custom;

            ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmDateTime";
            ShowInTaskbar = false;
            Closed += frmDateTime_Closed;
            Width = 200;
            Height = DateTimePicker1.Height;

            TopLevel = false;
        }

        private void frmDateTime_Closed(object sender, EventArgs e)
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
