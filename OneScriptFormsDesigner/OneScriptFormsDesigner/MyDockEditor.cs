using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class MyDockEditor : UITypeEditor
    {
        DockEditor editor;
        public MyDockEditor()
        {
            editor = new DockEditor();
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Type dockUiType = typeof(DockEditor).GetNestedType("DockUI", BindingFlags.NonPublic);
            var dockUiConstructor = dockUiType.GetConstructors()[0];
            var dockUiField = typeof(DockEditor).GetField("dockUI", BindingFlags.Instance | BindingFlags.NonPublic);
            var dockUiObject = dockUiConstructor.Invoke(new[] { editor }) as Control;
            dockUiField.SetValue(editor, dockUiObject);
            System.Windows.Forms.Control ContainerPlaceholder = dockUiObject.Controls[0];
            System.Windows.Forms.CheckBox CheckBox0 = (System.Windows.Forms.CheckBox)dockUiObject.Controls[1];
            System.Windows.Forms.CheckBox CheckBox1 = (System.Windows.Forms.CheckBox)ContainerPlaceholder.Controls[3];
            System.Windows.Forms.CheckBox CheckBox2 = (System.Windows.Forms.CheckBox)ContainerPlaceholder.Controls[4];
            System.Windows.Forms.CheckBox CheckBox3 = (System.Windows.Forms.CheckBox)ContainerPlaceholder.Controls[1];
            System.Windows.Forms.CheckBox CheckBox4 = (System.Windows.Forms.CheckBox)ContainerPlaceholder.Controls[2];
            System.Windows.Forms.CheckBox CheckBox5 = (System.Windows.Forms.CheckBox)ContainerPlaceholder.Controls[0];
            var none = dockUiObject.Controls[1];
            none.Text = "Отсутствие";

            if ((int)value == 1) // верх
            {
                CheckBox1.Checked = true;
            }
            else if ((int)value == 2) // низ
            {
                CheckBox2.Checked = true;
            }
            else if ((int)value == 3) // лево
            {
                CheckBox3.Checked = true;
            }
            else if ((int)value == 4) // право
            {
                CheckBox4.Checked = true;
            }
            else if ((int)value == 5) // заполнение
            {
                CheckBox5.Checked = true;
            }
            else // отсутствие
            {
                CheckBox0.Checked = true;
            }

            return editor.EditValue(context, provider, value);
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return editor.GetEditStyle(context);
        }
    }
}
