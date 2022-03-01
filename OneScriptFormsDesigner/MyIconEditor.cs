using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    class MyIconEditor : IconEditor
    {
        private FileDialog _fileDialog;
        [DllImport("user32.dll")] static extern IntPtr GetFocus();
        [DllImport("user32.dll", SetLastError = true)] public static extern IntPtr SetFocus(IntPtr hWnd);

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider == null)
            {
                return value;
            }

            var edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc == null)
            {
                return value;
            }

            if (_fileDialog == null)
            {
                _fileDialog = new System.Windows.Forms.OpenFileDialog();
                var filter = CreateFilterEntry(this);
                _fileDialog.Filter = filter;
            }

            IntPtr hwndFocus = GetFocus();
            try
            {
                if (_fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var file = new FileStream(_fileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    value = LoadFromStream(file);
                }
                else
                {
                    return ((dynamic)context.Instance).Icon;
                }
            }
            finally
            {
                if (hwndFocus != IntPtr.Zero)
                {
                    SetFocus(hwndFocus);
                }
            }

            MyIcon MyIcon1 = new MyIcon((dynamic)value);
            MyIcon1.M_Icon = (dynamic)value;
            MyIcon1.Path = _fileDialog.FileName;
            return MyIcon1;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            if (((dynamic)e.Value).M_Icon.GetType() != typeof(Icon))
            {
                return;
            }

            Icon icon = ((MyIcon)e.Value).M_Icon;

            // Если значок меньше прямоугольника, просто отцентруйте его в прямоугольнике.
            Rectangle rectangle = e.Bounds;
            if (icon.Width < rectangle.Width)
            {
                rectangle.X = (rectangle.Width - icon.Width) / 2;
                rectangle.Width = icon.Width;
            }
            if (icon.Height < rectangle.Height)
            {
                rectangle.X = (rectangle.Height - icon.Height) / 2;
                rectangle.Height = icon.Height;
            }
            e.Graphics.DrawIcon(icon, rectangle);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
