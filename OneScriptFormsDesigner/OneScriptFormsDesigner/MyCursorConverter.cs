using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace osfDesigner
{
    public class MyCursorConverter : CursorConverter
    {
        static TypeConverter cursorConverter = TypeDescriptor.GetConverter(typeof(Cursor));
        public static Dictionary<string, string> cursors = new Dictionary<string, string>
            {
                {"NoMove2D", "БезДвижения2D"},
                {"NoMoveVert", "БезДвиженияВекртикально"},
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

        public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
        {
            return true;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
        {
            if (destinationType.Equals(typeof(string)))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo cultureInfo, object value, System.Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string cursorName = cursorConverter.ConvertToString(value);
                try
                {
                    return cursors[cursorName];
                }
                catch
                {
                    return cursorName;
                }
            }
            return base.ConvertTo(context, cultureInfo, RuntimeHelpers.GetObjectValue(value), destinationType);
        }

        public new static string ConvertToString(object value)
        {
            string cursorName = cursorConverter.ConvertToString(value);
            try
            {
                return cursors[cursorName];
            }
            catch
            {
                return cursorName;
            }
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
