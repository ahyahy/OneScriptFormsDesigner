using System.ComponentModel.Design.Serialization;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System;

namespace osfDesigner
{
    public class MyIconConverter : IconConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(byte[]))
            {
                return true;
            }
            
            if (sourceType == typeof(InstanceDescriptor))
            {
                return false;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Image) || destinationType == typeof(Bitmap))
            {
                return true;
            }

            if (destinationType == typeof(byte[]))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is byte[])
            {
                MemoryStream MemoryStream1 = new MemoryStream((byte[])value);
                return new Icon(MemoryStream1);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            dynamic Instance1 = (dynamic)context.Instance;

            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }

            if (destinationType == typeof(Image) || destinationType == typeof(Bitmap))
            {
                Icon icon = value as Icon;
                if (icon != null)
                {
                    return icon.ToBitmap();
                }
            }
            if (destinationType == typeof(string))
            {
                if (value != null)
                {
                    if (Instance1.Icon.Path != null)
                    {
                        return Instance1.Icon.Path;
                    }
                    else
                    {
                        return "(Значок)";
                    }
                }
                return "(Значок)";
            }
            if (destinationType == typeof(byte[]))
            {
                if (value != null)
                {
                    MemoryStream MemoryStream1 = null;
                    try
                    {
                        MemoryStream1 = new MemoryStream();
                        Icon icon = value as Icon;
                        if (icon != null)
                        {
                            icon.Save(MemoryStream1);
                        }
                    }
                    finally
                    {
                        if (MemoryStream1 != null)
                        {
                            MemoryStream1.Close();
                        }
                    }
                    if (MemoryStream1 != null)
                    {
                        return MemoryStream1.ToArray();
                    }
                    return null;
                }
                return new byte[0];
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
	
        public new static string ConvertToString(object value)
        {
            if (value != null)
            {
                if (((dynamic)value).Path != null)
                {
                    return ((dynamic)value).Path;
                }
                else
                {
                    return "(Значок)";
                }
            }
            return "(Значок)";
        }
    }
}
