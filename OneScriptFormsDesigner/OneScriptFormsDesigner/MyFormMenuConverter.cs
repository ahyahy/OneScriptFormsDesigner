using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;

namespace osfDesigner
{
    public class MyFormMenuConverter : ReferenceConverter
    {
        public MyFormMenuConverter(Type type) : base(type)
        {
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

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
