using System.ComponentModel;
using System.Globalization;
using System;

namespace osfDesigner
{
    public class MyCollectionConverter : CollectionConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return "(Коллекция)";
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
