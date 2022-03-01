using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class MyLinkAreaEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            System.Windows.Forms.LinkLabel LinkLabel1 = (System.Windows.Forms.LinkLabel)context.Instance;

            IWindowsFormsEditorService wfes = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (wfes != null)
            {
                string text = string.Empty;
                PropertyDescriptor property = null;

                if (LinkLabel1 != null)
                {
                    property = TypeDescriptor.GetProperties(LinkLabel1)["Text"];
                    if (property.PropertyType == typeof(string))
                    {
                        text = (string)property.GetValue(LinkLabel1);
                    }
                }

                string originalText = text;
                frmLinkArea frmLinkArea = new frmLinkArea(context, LinkLabel1);
                frmLinkArea._wfes = wfes;
                frmLinkArea.Start(value);

                if (wfes.ShowDialog(frmLinkArea) == System.Windows.Forms.DialogResult.OK)
                {
                    value = frmLinkArea.Value;
                    text = frmLinkArea.SampleText;
                    if (!originalText.Equals(text) && property.PropertyType == typeof(string))
                    {
                        property.SetValue(LinkLabel1, text);
                    }
                }
                frmLinkArea.End();
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }

    internal class frmLinkArea : System.Windows.Forms.Form
    {
        private dynamic _editor;
        private ITypeDescriptorContext _context;
        public IWindowsFormsEditorService _wfes;
        private Container components = null;
        private System.Windows.Forms.LinkLabel LinkLabel1;
        private System.Windows.Forms.Label Label1 = null;
        private System.Windows.Forms.TextBox TextBox1 = null;
        private System.Windows.Forms.Button OkButton1 = null;
        private System.Windows.Forms.Button CancelButton1 = null;
        private TableLayoutPanel OkCancelTableLayoutPanel;

        public frmLinkArea(ITypeDescriptorContext context, System.Windows.Forms.LinkLabel linkLabel)
        {
            _context = context;
            LinkLabel1 = linkLabel;
            FormClosed += FrmLinkArea_FormClosed;

            PropertyDescriptor pd = TypeDescriptor.GetProperties(LinkLabel1)["LinkArea"];
            _editor = (dynamic)pd.GetEditor(typeof(UITypeEditor));

            ComponentResourceManager resources = new ComponentResourceManager(_editor.GetType());
            Label1 = new System.Windows.Forms.Label();
            TextBox1 = new System.Windows.Forms.TextBox();
            OkButton1 = new System.Windows.Forms.Button();
            OkButton1.Click += OkButton1_Click;
            CancelButton1 = new System.Windows.Forms.Button();
            OkCancelTableLayoutPanel = new TableLayoutPanel();
            OkCancelTableLayoutPanel.SuspendLayout();
            SuspendLayout();

            resources.ApplyResources(Label1, "caption");
            Label1.Margin = new Padding(3, 1, 3, 0);
            Label1.Text = "Выберите часть текста для преобразования в ссылку:";

            resources.ApplyResources(TextBox1, "sampleEdit");
            TextBox1.Margin = new Padding(3, 2, 3, 3);
            TextBox1.HideSelection = false;
            TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            TextBox1.Multiline = true;
            TextBox1.Text = LinkLabel1.Text;

            resources.ApplyResources(OkButton1, "okButton");
            OkButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            OkButton1.Margin = new Padding(0, 0, 2, 0);
            OkButton1.Text = "ОК";

            resources.ApplyResources(CancelButton1, "cancelButton");
            CancelButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            CancelButton1.Margin = new Padding(3, 0, 0, 0);
            CancelButton1.Text = "Отмена";

            resources.ApplyResources(OkCancelTableLayoutPanel, "okCancelTableLayoutPanel");
            OkCancelTableLayoutPanel.ColumnCount = 2;
            OkCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            OkCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            OkCancelTableLayoutPanel.Controls.Add(OkButton1, 0, 0);
            OkCancelTableLayoutPanel.Controls.Add(CancelButton1, 1, 0);
            OkCancelTableLayoutPanel.Margin = new Padding(3, 1, 3, 3);
            OkCancelTableLayoutPanel.RowCount = 1;
            OkCancelTableLayoutPanel.RowStyles.Add(new RowStyle());
            OkCancelTableLayoutPanel.RowStyles.Add(new RowStyle());

            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = CancelButton1;
            Controls.Add(OkCancelTableLayoutPanel);
            Controls.Add(TextBox1);
            Controls.Add(Label1);
            HelpButton = true;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            OkCancelTableLayoutPanel.ResumeLayout(false);
            OkCancelTableLayoutPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
            Text = "Редактор ОбластьСсылки";
        }

        public string SampleText
        {
            get { return TextBox1.Text; }
        }

        public object Value { get; set; }

        private void OkButton1_Click(object sender, EventArgs e)
        {
            Value = new LinkArea(TextBox1.SelectionStart, TextBox1.SelectionLength);
        }

        public void Start(object value)
        {
            Value = value;
            UpdateSelection();
            ActiveControl = TextBox1;
        }

        public void End()
        {
            Value = null;
        }

        private void UpdateSelection()
        {
            if (Value.GetType() != typeof(LinkArea))
            {
                return;
            }

            try
            {
                TextBox1.SelectionStart = ((LinkArea)Value).Start;
                TextBox1.SelectionLength = ((LinkArea)Value).Length;
            }
            catch { }
        }

        private void FrmLinkArea_FormClosed(object sender, FormClosedEventArgs e)
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
