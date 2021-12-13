using System;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Reflection;

namespace osfDesigner
{
    public class MyAnchorEditor : UITypeEditor
    {
        AnchorEditor editor;

        public MyAnchorEditor()
        {
            editor = new System.Windows.Forms.Design.AnchorEditor();
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Type anchorUiType = typeof(AnchorEditor).GetNestedType("AnchorUI", BindingFlags.NonPublic);
            var anchorUiConstructor = anchorUiType.GetConstructors()[0];
            var anchorUiField = typeof(AnchorEditor).GetField("anchorUI", BindingFlags.Instance | BindingFlags.NonPublic);
            var anchorUiObject = anchorUiConstructor.Invoke(new[] { editor }) as Control;
            anchorUiField.SetValue(editor, anchorUiObject);

            return editor.EditValue(context, provider, value);
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return editor.GetEditStyle(context);
        }
    }
}
