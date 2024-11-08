using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System;
using ScriptEngine.Machine;

namespace osfDesigner
{
    public class MyDecimalConverter : DecimalConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo cultureInfo, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string str1 = ((IValue)value).AsNumber().ToString();
                return str1;
            }
            return base.ConvertTo(context, cultureInfo, RuntimeHelpers.GetObjectValue(value), destinationType);
        }

        public new static string ConvertToString(object value)
        {
            string str1 = value.ToString();
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
            IValue rez = ValueFactory.Create(Decimal.Parse((string)value));
            return rez;
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
