using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms; 

namespace osfDesigner
{
    public class MyImageIndexConverter : ImageIndexConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            object res = base.ConvertTo(context, culture, value, destinationType);

            if (value.ToString() == "-1")
            {
                return "(Нет)";
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                if (value.ToString() == "(Нет)" || value.ToString() == "(none)")
                {
                    value = "-1";
                    return base.ConvertFrom(context, culture, value);
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
