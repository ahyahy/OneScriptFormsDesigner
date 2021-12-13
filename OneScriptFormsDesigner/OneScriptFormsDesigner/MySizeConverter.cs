using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Drawing;

namespace osfDesigner
{
    public class MySizeConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo cultureInfo, object value, System.Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string str1 = value.ToString();
                str1 = str1.Replace("Width", "Ширина");
                str1 = str1.Replace("Height", "Высота");

                return str1;
            }
            return base.ConvertTo(context, cultureInfo, RuntimeHelpers.GetObjectValue(value), destinationType);
        }
	
        public new static string ConvertToString(object value)
        {
            string str1 = value.ToString();
            str1 = str1.Replace("Width", "Ширина");
            str1 = str1.Replace("Height", "Высота");

            return str1;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
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
            string _Width = result[0];
            _Width = _Width.Replace("{Ширина=", "");
            string _Height = result[1];
            _Height = _Height.Replace("Высота=", "");
            _Height = _Height.Replace("}", "");
            Size Size1 = new Size(Convert.ToInt32(_Width), Convert.ToInt32(_Height));

            return Size1;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
        {
            return true;
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return false;
        }
    }
}
