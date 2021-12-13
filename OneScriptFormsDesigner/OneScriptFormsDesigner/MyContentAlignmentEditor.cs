using System;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;

namespace osfDesigner
{
    public class MyContentAlignmentEditor : UITypeEditor
    {
        System.Drawing.Design.ContentAlignmentEditor editor;
        public MyContentAlignmentEditor()
        {
            editor = new ContentAlignmentEditor();
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Type contentUiType = typeof(ContentAlignmentEditor).GetNestedType("ContentUI", BindingFlags.NonPublic);
            var contentUiConstructor = contentUiType.GetConstructors()[0];
            var contentUiField = typeof(ContentAlignmentEditor).GetField("contentUI", BindingFlags.Instance | BindingFlags.NonPublic);
            object[] args = new object[0];
            var contentUiObject = contentUiConstructor.Invoke(args) as Control;
            contentUiField.SetValue(editor, contentUiObject);

            return editor.EditValue(context, provider, value);
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return editor.GetEditStyle(context);
        }
    }
}
