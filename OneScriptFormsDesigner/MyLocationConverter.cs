using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System;

namespace osfDesigner
{
    public class MyLocationConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo cultureInfo, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string str1 = value.ToString();
                str1 = str1.Replace("X", "Икс");
                str1 = str1.Replace("Y", " Игрек");

                return str1;
            }
            return base.ConvertTo(context, cultureInfo, RuntimeHelpers.GetObjectValue(value), destinationType);
        }

        public new static string ConvertToString(object value)
        {
            string str1 = value.ToString();
            str1 = str1.Replace("X", "Икс");
            str1 = str1.Replace("Y", " Игрек");

            return str1;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType.Equals(typeof(string)))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo cultureInfo, object value)
        {
            if (!(value is string))
            {
                return base.ConvertFrom(context, cultureInfo, RuntimeHelpers.GetObjectValue(value));
            }

            string[] stringSeparators = new string[] { ", " };
            string[] result = Convert.ToString(value).Split(stringSeparators, StringSplitOptions.None);
            string _X = result[0];
            _X = _X.Replace("{Икс=", "");
            string _Y = result[1];
            _Y = _Y.Replace("Игрек=", "");
            _Y = _Y.Replace("}", "");
            Point Point1 = new Point(Convert.ToInt32(_X), Convert.ToInt32(_Y));

            return Point1;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return false;
        }
    }
}
