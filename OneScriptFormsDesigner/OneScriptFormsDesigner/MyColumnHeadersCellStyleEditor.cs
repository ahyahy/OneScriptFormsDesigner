using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class MyColumnHeadersCellStyleEditor : UITypeEditor
    {
        private System.Windows.Forms.Form Form1;
        private System.Windows.Forms.PropertyGrid PropertyGrid1;
        private System.Windows.Forms.TableLayoutPanel TPanel1;
        private System.Windows.Forms.TableLayoutPanel TPanel2;
        private System.Windows.Forms.Button ButtonOk1 = null;
        private System.Windows.Forms.Button ButtonCancel1 = null;
        private System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.TableLayoutPanel TPanel3;
        private System.Windows.Forms.TableLayoutPanel TPanel4;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.DataGridView DataGridView2;
        private System.Windows.Forms.DataGridView DataGridView3;
        private System.Windows.Forms.DataGridViewCellStyle DefaultCellStyle1;
        private System.Windows.Forms.DataGridViewCellStyle DataGridViewCellStyle1;
        private System.Windows.Forms.DataGridViewCellStyle initialColumnHeadersDefaultCellStyle;
        private DataGridView Instance;
        private DataGridViewCellStyleHeaders SimilarObj;
        private System.Windows.Forms.PropertyGrid TopLevelPropertyGrid1;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Assembly assembly = Assembly.Load("System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
            System.Type type = assembly.GetType("System.Windows.Forms.Design.DataGridViewCellStyleBuilder");
            ConstructorInfo ci = type.GetConstructor(new System.Type[] { typeof(IServiceProvider), typeof(IComponent) });
            Form1 = ci.Invoke(new object[] { provider, null }) as System.Windows.Forms.Form;//, Text: Построитель CellStyle
            Form1.Text = "Построитель СтильЗаголовковКолонок";
            Form1.FormClosed += Form1_FormClosed;
            Form1.Shown += Form1_Shown;
            TPanel1 = (System.Windows.Forms.TableLayoutPanel)Form1.Controls[0];
            PropertyGrid1 = (System.Windows.Forms.PropertyGrid)TPanel1.Controls[0];
            Instance = (DataGridView)context.Instance;

            SimilarObj = Instance.columnHeadersDefaultCellStyle;

            PropertyGrid1.SelectedObject = SimilarObj;
            PropertyGrid1.PropertyValueChanged += PropertyGrid1_PropertyValueChanged;

            TPanel2 = (System.Windows.Forms.TableLayoutPanel)TPanel1.Controls[1];
            ButtonOk1 = (System.Windows.Forms.Button)TPanel2.Controls[0];//, Text: ОК
            ButtonOk1.Text = "ОК";
            ButtonOk1.Click += ButtonOk1_Click;
            ButtonCancel1 = (System.Windows.Forms.Button)TPanel2.Controls[1];//, Text: Отмена
            ButtonCancel1.Text = "Отмена";
            ButtonCancel1.Click += ButtonCancel1_Click;
            GroupBox1 = (System.Windows.Forms.GroupBox)TPanel1.Controls[2];
            TPanel3 = (System.Windows.Forms.TableLayoutPanel)GroupBox1.Controls[0];
            TPanel4 = (System.Windows.Forms.TableLayoutPanel)TPanel3.Controls[0];
            Label1 = (System.Windows.Forms.Label)TPanel3.Controls[1];//, Text: Этот предварительный просмотр позволяет видеть свойства из унаследованных стилей CellStyles (Таблица, Столбец, Строка)
            Label1.Text = "Этот предварительный просмотр позволяет видеть свойства из унаследованных стилей CellStyles (Таблица, Столбец, Строка)";
            Label2 = (System.Windows.Forms.Label)TPanel4.Controls[0];//, Text: Обычный:
            Label2.Text = "Обычный";
            DataGridView2 = (System.Windows.Forms.DataGridView)TPanel4.Controls[1];
            Label3 = (System.Windows.Forms.Label)TPanel4.Controls[2];//, Text: Выбрано:
            Label3.Text = "Выбрано";
            DataGridView3 = (System.Windows.Forms.DataGridView)TPanel4.Controls[3];

            DefaultCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle(SimilarObj);
            DataGridView2.DefaultCellStyle = DefaultCellStyle1;
            DataGridView3.DefaultCellStyle = DefaultCellStyle1;

            Form1.ShowDialog();
            return base.EditValue(context, provider, value);
        }

        private void ButtonCancel1_Click(object sender, EventArgs e)
        {
            Instance.ColumnHeadersDefaultCellStyle = initialColumnHeadersDefaultCellStyle;
            Instance.columnHeadersDefaultCellStyle.NameStyle = Instance.Name + "СтильЗаголовковКолонок";
        }

        private void ButtonOk1_Click(object sender, EventArgs e)
        {
            Instance.columnHeadersDefaultCellStyle = SimilarObj;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            SimilarObj.NameStyle = Instance.Name + "СтильЗаголовковКолонок";
            initialColumnHeadersDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle(SimilarObj);

            TopLevelPropertyGrid1 = OneScriptFormsDesigner.PropertyGrid;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Instance.ColumnHeadersDefaultCellStyle = initialColumnHeadersDefaultCellStyle;
                Instance.columnHeadersDefaultCellStyle.NameStyle = Instance.Name + "СтильЗаголовковКолонок";
            }

            OneScriptFormsDesigner.SetDesignSurfaceState();
        }

        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            DataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle(SimilarObj);
            Instance.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1;
            DataGridView2.DefaultCellStyle = DataGridViewCellStyle1;
            DataGridView3.DefaultCellStyle = DataGridViewCellStyle1;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
