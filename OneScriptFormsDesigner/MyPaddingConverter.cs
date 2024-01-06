using System.ComponentModel.Design.Serialization;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    class MyPaddingConverter : TypeConverter
    {
        // Определяет, может ли этот преобразователь преобразовать объект данного исходного типа в собственный тип конвертера.
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        // Преобразует данный объект в собственный тип преобразователя.
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string))
            {
                return base.ConvertFrom(context, culture, value);
            }
            if (value is string)
            {
                string stringValue = ((string)value).Trim();
                if (stringValue.Length == 0)
                {
                    return null;
                }

                // Разобрать 4 целочисленных значения.
                culture = CultureInfo.CurrentCulture;

                string[] tokens = stringValue.Split(new char[] { culture.TextInfo.ListSeparator[0] });
                int[] values = new int[tokens.Length];
                TypeConverter intConverter = TypeDescriptor.GetConverter(typeof(int));
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = (int)intConverter.ConvertFromString(context, culture, tokens[i]);
                }

                if (values.Length != 4)
                {
                    throw new ArgumentException("TextParseFailedFormat - left, top, right, bottom", nameof(value));
                }
                return new Padding(values[0], values[1], values[2], values[3]);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is Padding)
        {
                Padding padding = (Padding)value;
                if (destinationType == typeof(string))
                {
                    culture = CultureInfo.CurrentCulture;

                    string sep = culture.TextInfo.ListSeparator + " ";
                    TypeConverter intConverter = TypeDescriptor.GetConverter(typeof(int));

                    string[] args = new string[]
                    {
                        intConverter.ConvertToString(context, culture, padding.Left),
                        intConverter.ConvertToString(context, culture, padding.Top),
                        intConverter.ConvertToString(context, culture, padding.Right),
                        intConverter.ConvertToString(context, culture, padding.Bottom)
                    };
                    return string.Join(sep, args);
                }
                else if (destinationType == typeof(InstanceDescriptor))
                {
                    if (padding.All == -1)
                    {
                        return new InstanceDescriptor(typeof(Padding).GetConstructor(new Type[] { typeof(int) }), new object[] { padding.All });
                    }
                    else
                    {
                        return new InstanceDescriptor(typeof(Padding).GetConstructor(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int) }), new object[] { padding.Left, padding.Top, padding.Right, padding.Bottom });
                    }
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection originalCollection = TypeDescriptor.GetProperties(value, attributes);
            PropertyDescriptor[] pds = new PropertyDescriptor[originalCollection.Count];
            for (int i = 0; i < originalCollection.Count; i++)
            {
                pds[i] = new ExpandablePaddingPropertyDescriptor(originalCollection[i]);
            }
            return new PropertyDescriptorCollection(pds);
        }
    }

    public class ExpandablePaddingPropertyDescriptor : PropertyDescriptor
    {
        private PropertyDescriptor _pd;

        public ExpandablePaddingPropertyDescriptor(PropertyDescriptor pd) : base(GetDisplayName(pd.DisplayName), null)
        {
            _pd = pd;
        }

        private static string GetDisplayName(string str)
        {
            if (str == "All")
            {
                return "Все";
            }
            else if (str == "Left")
            {
                return "Лево";
            }
            else if (str == "Top")
            {
                return "Верх";
            }
            else if (str == "Right")
            {
                return "Право";
            }
            else if (str == "Bottom")
            {
                return "Низ";
            }
            return str;
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get { return _pd.GetType(); }
        }

        public override object GetValue(object component)
        {
            return _pd.GetValue(component);
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override string Name
        {
            get { return _pd.Name; }
        }

        public override Type PropertyType
        {
            get { return _pd.GetType(); }
        }

        public override void ResetValue(object component)
        {
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        public override void SetValue(object component, object value)
        {
        }
    }
}
