using System;
using System.ComponentModel;
using System.Globalization;

namespace osfDesigner
{
    public class MyImageListConverter : ComponentConverter
    {
        public MyImageListConverter() : base(typeof(System.Windows.Forms.ImageList))
        {
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value != null)
                {
                    return base.ConvertTo(context, culture, value, destinationType);
                }

                return "(Нет)";
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
