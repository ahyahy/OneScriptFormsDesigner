using System.ComponentModel.Design.Serialization;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System;

namespace osfDesigner
{
    public class MyImageConverter : ImageConverter
    {
        Type IconType1 = typeof(Icon);

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == IconType1)
            {
                return true;
            }

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
            if (destinationType == typeof(byte[]))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is Icon)
            {
                Icon Icon1 = (Icon)value;
                return Icon1.ToBitmap();
            }

            byte[] bytes = value as byte[];

            if (bytes != null)
            {
                Stream Stream1 = new MemoryStream(bytes);
                return Image.FromStream(Stream1);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                return null;
            }

            if (destinationType == typeof(string))
            {
                if (value != null)
                {
                    if (context.Instance.GetType() == typeof(osfDesigner.ImageEntry))
                    {
                        return ((osfDesigner.ImageEntry)context.Instance).FileName;
                    }
                    else 
                    {
                        return "" + ((Image)value).Tag;
                    }
                }
                else
                {
                    return "(отсутствует)";
                }
            }
            else if (destinationType == typeof(byte[]))
            {
                if (value != null)
                {
                    bool createdNewImage = false;
                    MemoryStream MemoryStream1 = null;
                    Image Image1 = null;
                    try
                    {
                        MemoryStream1 = new MemoryStream();
                        Image1 = (Image)value;

                        // Создадим новое и допустимое растровое изображение из нашего значка с нужным размером.
                        if (Image1.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Icon))
                        {
                            createdNewImage = true;
                            Image1 = new Bitmap(Image1, Image1.Width, Image1.Height);
                        }

                        Image1.Save(MemoryStream1, Image1.RawFormat);
                    }
                    finally
                    {
                        if (MemoryStream1 != null)
                        {
                            MemoryStream1.Close();
                        }
                        if (createdNewImage && Image1 != null)
                        {
                            Image1.Dispose();
                        }
                    }

                    if (MemoryStream1 != null)
                    {
                        return MemoryStream1.ToArray();
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return new byte[0];
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(Image), attributes);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return false;
        }
    }
}
