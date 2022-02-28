using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System;

namespace osfDesigner
{
    class MyFontConverter: FontConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string))
            {
                return base.ConvertFrom(context, culture, value);
            }

            string str = ((string)value)
                .Replace("стиль=", "style=")
                .Replace("Жирный", "Bold")
                .Replace("Зачеркнутый", "Strikeout")
                .Replace("Курсив", "Italic")
                .Replace("Подчеркнутый", "Underline")
                .Replace("Стандартный", "Regular");
            return base.ConvertFrom(context, culture, str);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            string str = (string)base.ConvertTo(context, culture, value, destinationType);

            if (str == "(none)")
            {
                return "(отсутствует)";
            }

            return str
                .Replace("style=", "стиль=")
                .Replace("Bold", "Жирный")
                .Replace("Strikeout", "Зачеркнутый")
                .Replace("Italic", "Курсив")
                .Replace("Underline", "Подчеркнутый")
                .Replace("Regular", "Стандартный");
        }

        public new static string ConvertToString(object value)
        {
            return value.ToString()
                .Replace("style=", "стиль=")
                .Replace("Bold", "Жирный")
                .Replace("Strikeout", "Зачеркнутый")
                .Replace("Italic", "Курсив")
                .Replace("Underline", "Подчеркнутый")
                .Replace("Regular", "Стандартный");
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
                pds[i] = new ExpandableFontPropertyDescriptor(originalCollection[i]);
            }
            return new PropertyDescriptorCollection(pds);
        }
    }

    public class ExpandableFontPropertyDescriptor : PropertyDescriptor
    {
        private PropertyDescriptor _pd;

        public ExpandableFontPropertyDescriptor(PropertyDescriptor pd) : base(GetDisplayName(pd.DisplayName), null)
        {
            _pd = pd;
        }

        private static string GetDisplayName(string str)
        {
            if (str == "Bold")
            {
                return "Жирный";
            }
            else if (str == "Italic")
            {
                return "Курсив";
            }
            else if (str == "Underline")
            {
                return "Подчеркнутый";
            }
            else if (str == "Strikeout")
            {
                return "Зачеркнутый";
            }
            else if (str == "Size")
            {
                return "Размер";
            }
            else if (str == "Name")
            {
                return "Имя";
            }
            else if (str == "Unit")
            {
                return "Размерность";
            }
            else if (str == "GdiVerticalFont")
            {
                return "Производный от вертикального GDI";
            }
            else if (str == "GdiCharSet")
            {
                return "Набор знаков GDI";
            }
            return str;
        }

        private static string CSharpName(Type type)
        {
            var sb = new StringBuilder();
            var name = type.Name;
            if (!type.IsGenericType)
            {
                return name;
            }
            sb.Append(name.Substring(0, name.IndexOf('`')));
            sb.Append("<");
            sb.Append(string.Join(", ", type.GetGenericArguments().Select(CSharpName)));
            sb.Append(">");
            return sb.ToString();
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
            string str = _pd.GetValue(component).ToString();
            if (str == false.ToString())
            {
                return "Ложь";
            }
            else if (str == true.ToString())
            {
                return "Истина";
            }
            else if (str == GraphicsUnit.Display.ToString())
            {
                return "дисплей (пиксель для дисплеев)";
            }
            else if (str == GraphicsUnit.Document.ToString())
            {
                return "документ (1/300 дюйма)";
            }
            else if (str == GraphicsUnit.Inch.ToString())
            {
                return "дюйм";
            }
            else if (str == GraphicsUnit.Millimeter.ToString())
            {
                return "миллиметр";
            }
            else if (str == GraphicsUnit.Pixel.ToString())
            {
                return "пиксель";
            }
            else if (str == GraphicsUnit.Point.ToString())
            {
                return "пункт (1/72 дюйма)";
            }
            else if (str == GraphicsUnit.World.ToString())
            {
                return "мировая";
            }
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
