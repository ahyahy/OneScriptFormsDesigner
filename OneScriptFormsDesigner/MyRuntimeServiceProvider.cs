using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class MyRuntimeServiceProvider : IServiceProvider, ITypeDescriptorContext
    {
        dynamic instance;

        public MyRuntimeServiceProvider(object p1 = null)
        {
            instance = p1;
        }

        object IServiceProvider.GetService(Type serviceType)
        {
            if (serviceType == typeof(IWindowsFormsEditorService))
            {
                return new WindowsFormsEditorService();
            }

            return null;
        }

        public void OnComponentChanged()
        {
        }

        public IContainer Container
        {
            get { return null; }
        }

        public bool OnComponentChanging()
        {
            return true;
        }

        public object Instance
        {
            get { return instance; }
        }

        public PropertyDescriptor PropertyDescriptor
        {
            get { return null; }
        }

        internal class WindowsFormsEditorService : IWindowsFormsEditorService
        {
            public void DropDownControl(Control control)
            {
            }

            public void CloseDropDown()
            {
            }

            public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.Form dialog)
            {
                return dialog.ShowDialog();
            }
        }
    }
}
