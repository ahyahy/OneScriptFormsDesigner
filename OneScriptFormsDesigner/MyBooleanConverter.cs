using System.ComponentModel;
using System.Globalization;
using System;

namespace osfDesigner
{
    public class MyBooleanConverter : BooleanConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
        {
            return (bool)value ? "Истина" : "Ложь";
        }
	
        public new static string ConvertToString(object value)
        {
            return (bool)value ? "Истина" : "Ложь";
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (string)value == "Истина";
        }
    }
}
