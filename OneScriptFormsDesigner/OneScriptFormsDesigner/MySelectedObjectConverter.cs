using System.ComponentModel.Design;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System;

namespace osfDesigner
{
    public class MySelectedObjectConverter : ReferenceConverter
    {
        dynamic Converter;

        public MySelectedObjectConverter(Type type) : base(type)
        {
            Type ConverterType = typeof(System.Windows.Forms.PropertyGrid).GetNestedType("SelectedObjectConverter", BindingFlags.NonPublic);
            var ConverterConstructor = ConverterType.GetConstructors()[0];
            Converter = ConverterConstructor.Invoke(null);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }

            if (destinationType == typeof(string))
            {
                if (value != null)
                {
                    if (context != null)
                    {
                        IReferenceService refSvc = (IReferenceService)context.GetService(typeof(IReferenceService));
                        if (refSvc != null)
                        {
                            string name = refSvc.GetName(value);
                            if (name != null)
                            {
                                return name;
                            }
                        }
                    }
                    if (!System.Runtime.InteropServices.Marshal.IsComObject(value) && value is IComponent)
                    {
                        IComponent comp = (IComponent)value;
                        ISite site = comp.Site;
                        if (site != null)
                        {
                            string name = site.Name;
                            if (name != null)
                            {
                                return name;
                            }
                        }
                    }
                    return String.Empty;
                }
                return "(отсутствует)";
            }

            return Converter.ConvertTo(context, culture, value, destinationType);
        }
    }
}
