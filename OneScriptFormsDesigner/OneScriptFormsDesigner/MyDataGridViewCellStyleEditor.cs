using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class MyDataGridViewCellStyleEditor : UITypeEditor
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
        private System.Windows.Forms.DataGridViewCellStyle DataGridViewCellStyle1;
        private osfDesigner.DataGridViewCellStyle DataGridViewCellStyle2;
        private DataGridViewColumn Instance;
        private DataGridViewCellStyle SimilarObj;
        private System.Windows.Forms.PropertyGrid TopLevelPropertyGrid1;
        private System.Windows.Forms.DataGridViewCellStyle initialDefaultCellStyle;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Assembly assembly = Assembly.Load("System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
            System.Type type = assembly.GetType("System.Windows.Forms.Design.DataGridViewCellStyleBuilder");
            ConstructorInfo ci = type.GetConstructor(new System.Type[] { typeof(IServiceProvider), typeof(IComponent) });
            Form1 = ci.Invoke(new object[] { provider, null }) as System.Windows.Forms.Form;//, Text: Построитель CellStyle
            Form1.Text = "Построитель СтильЯчейки";
            Form1.Shown += Form1_Shown;
            TPanel1 = (System.Windows.Forms.TableLayoutPanel)Form1.Controls[0];
            PropertyGrid1 = (System.Windows.Forms.PropertyGrid)TPanel1.Controls[0];
            Instance = (System.Windows.Forms.DataGridViewColumn)context.Instance;
            initialDefaultCellStyle = Instance.DefaultCellStyle;
            string nameStyle = Instance.DataGridView.Name + Instance.Name + "СтильЯчейки";
            SimilarObj = new DataGridViewCellStyle(nameStyle, (osfDesigner.DataGridView)Instance.DataGridView);
            // Передадим свойства стиля.
            try
            {
                SimilarObj.Font = new System.Drawing.Font(initialDefaultCellStyle.Font, initialDefaultCellStyle.Font.Style);
                SimilarObj.ForeColor = initialDefaultCellStyle.ForeColor;
                SimilarObj.SelectionForeColor = initialDefaultCellStyle.SelectionForeColor;
                SimilarObj.BackColor = initialDefaultCellStyle.BackColor;
                SimilarObj.SelectionBackColor = initialDefaultCellStyle.SelectionBackColor;
                SimilarObj.Alignment = (osfDesigner.DataGridViewContentAlignment)initialDefaultCellStyle.Alignment;
                SimilarObj.Padding = initialDefaultCellStyle.Padding;
                SimilarObj.WrapMode = (osfDesigner.DataGridViewTriState)initialDefaultCellStyle.WrapMode;
            }
            catch
            {
                SimilarObj.Font = new System.Drawing.Font(Instance.DataGridView.Font, Instance.DataGridView.Font.Style);
                SimilarObj.ForeColor = System.Drawing.Color.FromName(System.Drawing.KnownColor.ControlText.ToString());
                SimilarObj.SelectionForeColor = System.Drawing.Color.FromName(System.Drawing.KnownColor.HighlightText.ToString());
                SimilarObj.BackColor = System.Drawing.Color.FromName(System.Drawing.KnownColor.Window.ToString());
                SimilarObj.SelectionBackColor = System.Drawing.Color.FromName(System.Drawing.KnownColor.Highlight.ToString());
                SimilarObj.Alignment = osfDesigner.DataGridViewContentAlignment.СерединаЛево;
                SimilarObj.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
                SimilarObj.WrapMode = DataGridViewTriState.Ложь;
            }	
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
            DataGridView2.DefaultCellStyle = initialDefaultCellStyle;
            Label3 = (System.Windows.Forms.Label)TPanel4.Controls[2];//, Text: Выбрано:
            Label3.Text = "Выбрано";
            DataGridView3 = (System.Windows.Forms.DataGridView)TPanel4.Controls[3];
            DataGridView3.DefaultCellStyle = initialDefaultCellStyle;

            Form1.ShowDialog();
            return base.EditValue(context, provider, value);
        }

        private void ButtonCancel1_Click(object sender, EventArgs e)
        {
            Instance.DefaultCellStyle = initialDefaultCellStyle;
            DataGridViewCellStyle2 = new DataGridViewCellStyle(initialDefaultCellStyle);
            DataGridViewCellStyle2.NameStyle = Instance.DataGridView.Name + Instance.Name + "СтильЯчейки";
            Instance.DefaultCellStyle = DataGridViewCellStyle2;
        }

        private void ButtonOk1_Click(object sender, EventArgs e)
        {
            Instance.DefaultCellStyle = SimilarObj;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            SimilarObj.NameStyle = Instance.DataGridView.Name + Instance.Name + "СтильЯчейки";
            SimilarObj.DefaultValues = OneScriptFormsDesigner.GetDefaultValues(SimilarObj, PropertyGrid1);

            TopLevelPropertyGrid1 = OneScriptFormsDesigner.PropertyGrid;
        }

        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            DataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle(SimilarObj);
            Instance.DefaultCellStyle = DataGridViewCellStyle1;
            DataGridView2.DefaultCellStyle = DataGridViewCellStyle1;
            DataGridView3.DefaultCellStyle = DataGridViewCellStyle1;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
