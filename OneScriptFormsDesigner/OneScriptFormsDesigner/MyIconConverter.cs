using System;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.ComponentModel.Design.Serialization;

namespace osfDesigner
{
    public class MyIconConverter : IconConverter
    {
        // Получает значение, указывающее, может ли этот преобразователь
        // преобразовать объект данного исходного типа в собственный тип преобразователя
        // используя контекст.
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

        // Получает значение, указывающее, может ли этот конвертер
        // преобразовать объект в заданный тип назначения с помощью контекста.
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

        // Преобразует данный объект в собственный тип преобразователя.
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is byte[])
            {
                MemoryStream MemoryStream1 = new MemoryStream((byte[])value);
                return new Icon(MemoryStream1);
            }

            return base.ConvertFrom(context, culture, value);
        }

        // Преобразует данный объект в другой тип.  Наиболее распространенные типы преобразования
        // относятся к преобразованию в строковый объект и из строкового объекта.  Реализация по умолчанию вызовет
        // преобразованию объекта в строку, если объект допустим и если место назначения
        // тип-строка. Если это невозможно, будет создано исключение NotSupportedException.
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
                else
                {
                    return "(Значок)";
                }
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
            else
            {
                return "(Значок)";
            }
        }
    }
}
