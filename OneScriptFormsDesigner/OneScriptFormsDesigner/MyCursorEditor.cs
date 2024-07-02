using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class MyCursorEditor : CursorEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService wfes = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (wfes != null)
            {
                frmCursor _frmCursor = new frmCursor();
                _frmCursor._wfes = wfes;
                _frmCursor._value = value;

                if (value != null)
                {
                    for (int i = 0; i < _frmCursor.Items.Count; i++)
                    {
                        if (_frmCursor.Items[i] == value)
                        {
                            _frmCursor.SelectedIndex = i;
                            break;
                        }
                    }
                }

                wfes.DropDownControl(_frmCursor);
                value = _frmCursor.Value;
                _frmCursor._wfes = null;
                _frmCursor._value = null;
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

        private class frmCursor : System.Windows.Forms.ListBox
        {
            public object _value;
            public IWindowsFormsEditorService _wfes;
            private TypeConverter cursorConverter;
            Dictionary<string, string> cursors;

            public frmCursor()
            {
                Height = 310;
                ItemHeight = Math.Max(4 + Cursors.Default.Size.Height, Font.Height);
                DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
                BorderStyle = System.Windows.Forms.BorderStyle.None;

                cursorConverter = TypeDescriptor.GetConverter(typeof(Cursor));
                if (cursorConverter.GetStandardValuesSupported())
                {
                    foreach (object obj in cursorConverter.GetStandardValues())
                    {
                        Items.Add(obj);
                    }
                }

                cursors = new Dictionary<string, string>
                {
                    {"NoMove2D", "БезДвижения2D"},
                    {"NoMoveVert", "БезДвиженияВертикально"},
                    {"NoMoveHoriz", "БезДвиженияГоризонтально"},
                    {"VSplit", "ВРазделитель"},
                    {"HSplit", "ГРазделитель"},
                    {"PanEast", "КурсорВ"},
                    {"PanWest", "КурсорЗ"},
                    {"WaitCursor", "КурсорОжидания"},
                    {"PanNorth", "КурсорС"},
                    {"PanNE", "КурсорСВ"},
                    {"PanNW", "КурсорСЗ"},
                    {"PanSouth", "КурсорЮ"},
                    {"PanSE", "КурсорЮВ"},
                    {"PanSW", "КурсорЮЗ"},
                    {"Hand", "Ладонь"},
                    {"IBeam", "Луч"},
                    {"No", "Нет"},
                    {"Cross", "Перекрестие"},
                    {"Default", "ПоУмолчанию"},
                    {"AppStarting", "ПриСтарте"},
                    {"SizeWE", "РазмерЗВ"},
                    {"SizeNESW", "РазмерСВЮЗ"},
                    {"SizeNWSE", "РазмерСЗЮВ"},
                    {"SizeNS", "РазмерСЮ"},
                    {"SizeAll", "РазмерЧетырехконечный"},
                    {"Help", "Справка"},
                    {"Arrow", "Стрелка"},
                    {"UpArrow", "СтрелкаВверх"}
                };
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
                base.OnClick(e);
                _value = SelectedItem;
                _wfes.CloseDropDown();
            }

            protected override void OnDrawItem(DrawItemEventArgs e)
            {
                base.OnDrawItem(e);

                if (e.Index != -1)
                {
                    Cursor cursor = (Cursor)Items[e.Index];
                    string text = cursorConverter.ConvertToString(cursor);
                    Font font = e.Font;
                    Brush brushText = new SolidBrush(e.ForeColor);

                    e.DrawBackground();
                    e.Graphics.FillRectangle(SystemBrushes.Control, new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 32, e.Bounds.Height - 4));
                    e.Graphics.DrawRectangle(SystemPens.WindowText, new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 32 - 1, e.Bounds.Height - 4 - 1));

                    cursor.DrawStretched(e.Graphics, new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 32, e.Bounds.Height - 4));

                    string cursorName = text;
                    try
                    {
                        cursorName = cursors[cursorName];
                    }
                    catch { }

                    e.Graphics.DrawString(cursorName, font, brushText, e.Bounds.X + 36, e.Bounds.Y + (e.Bounds.Height - font.Height) / 2);

                    brushText.Dispose();
                }
            }
        }
    }
}
