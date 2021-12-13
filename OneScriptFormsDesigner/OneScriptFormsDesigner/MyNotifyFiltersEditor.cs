using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace osfDesigner
{
    class MyNotifyFiltersEditor : CursorEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService wfes = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (wfes != null)
            {
                TypeConverter Converter1 = TypeDescriptor.GetConverter(typeof(osfDesigner.NotifyFilters));
                frmNotifyFilters _frmNotifyFilters = new frmNotifyFilters(Converter1.ConvertToString(value), (int)value);
                _frmNotifyFilters._value = (int)value;

                wfes.DropDownControl(_frmNotifyFilters);
                value = _frmNotifyFilters.Value;
            }

            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override bool IsDropDownResizable
        {
            get
            {
                return true;
            }
        }

        private class frmNotifyFilters : System.Windows.Forms.ListBox
        {
            public int _value;
            public int oldValue;
            public string _strvalue;

            public frmNotifyFilters(string strvalue, int value)
            {
                Height = 180;
                ItemHeight = Font.Height + 4;
                DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
                BorderStyle = System.Windows.Forms.BorderStyle.None;
                SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
                _strvalue = strvalue;
                oldValue = value;
                System.Type enumType = Type.GetType("osfDesigner.NotifyFilters");
                var names = Enum.GetNames(enumType);
                for (int i = 0; i < names.Length; i++)
                {
                    int index = Items.Add(names[i]);
                    if (_strvalue.Contains(names[i]))
                    {
                        this.SetSelected(index, true);
                    }
                }
            }

            public object Value
            {
                get
                {
                    return _value;
                }
            }

            protected override void OnClick(EventArgs e)
            {
                int num = 0;
                System.Windows.Forms.ListBox.SelectedObjectCollection SelectedObjectCollection1 = this.SelectedItems;
                for (int i = 0; i < SelectedObjectCollection1.Count; i++)
                {
                    string str = (string)SelectedObjectCollection1[i];
                    num = num + (int)Enum.Parse(typeof(osfDesigner.NotifyFilters), (string)SelectedObjectCollection1[i]);
                }
                if (SelectedObjectCollection1.Count > 0)
                {
                    _value = num;
                }
                else
                {
                    _value = oldValue;
                }
            }

            protected override void OnDrawItem(DrawItemEventArgs e)
            {
                if (e.Index != -1)
                {
                    string text = (string)Items[e.Index];
                    Font font = e.Font;
                    Brush brushText = new SolidBrush(e.ForeColor);
                    e.DrawBackground();
                    e.Graphics.DrawString(text, font, brushText, e.Bounds.X, e.Bounds.Y);
                    brushText.Dispose();
                }
            }
        }
    }
}
