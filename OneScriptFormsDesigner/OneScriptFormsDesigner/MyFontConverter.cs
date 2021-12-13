using System;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace osfDesigner
{
    class MyFontConverter: FontConverter
    {
        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
        {
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, Type destinationType)
        {
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (!(value is string))
            {
                return base.ConvertFrom(context, culture, value);
            }

            string str3 = ((string)value).Replace("стиль=", "style=");
            str3 = ((string)str3).Replace("Жирный", "Bold");
            str3 = ((string)str3).Replace("Зачеркнутый", "Strikeout");
            str3 = ((string)str3).Replace("Курсив", "Italic");
            str3 = ((string)str3).Replace("Подчеркнутый", "Underline");
            str3 = ((string)str3).Replace("Стандартный", "Regular");
            return base.ConvertFrom(context, culture, (object)str3);
        }

        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            object str2 = base.ConvertTo(context, culture, value, destinationType);

            if ((string)str2 == "(none)")
            {
                return "(отсутствует)";
            }

            string str3 = ((string)str2).Replace("style=", "стиль=");
            str3 = ((string)str3).Replace("Bold", "Жирный");
            str3 = ((string)str3).Replace("Strikeout", "Зачеркнутый");
            str3 = ((string)str3).Replace("Italic", "Курсив");
            str3 = ((string)str3).Replace("Underline", "Подчеркнутый");
            str3 = ((string)str3).Replace("Regular", "Стандартный");
            return str3;
        }

        public new static string ConvertToString(object value)
        {
            string str3 = value.ToString().Replace("style=", "стиль=");
            str3 = ((string)str3).Replace("Bold", "Жирный");
            str3 = ((string)str3).Replace("Strikeout", "Зачеркнутый");
            str3 = ((string)str3).Replace("Italic", "Курсив");
            str3 = ((string)str3).Replace("Underline", "Подчеркнутый");
            str3 = ((string)str3).Replace("Regular", "Стандартный");
            return str3;
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

        private static string GetDisplayName(string p1)
        {
            if (p1 == "Bold")
            {
                return "Жирный";
            }
            else if (p1 == "Italic")
            {
                return "Курсив";
            }
            else if (p1 == "Underline")
            {
                return "Подчеркнутый";
            }
            else if (p1 == "Strikeout")
            {
                return "Зачеркнутый";
            }
            else if (p1 == "Size")
            {
                return "Размер";
            }
            else if (p1 == "Name")
            {
                return "Имя";
            }
            else if (p1 == "Unit")
            {
                return "Размерность";
            }
            else if (p1 == "GdiVerticalFont")
            {
                return "Производный от вертикального GDI";
            }
            else if (p1 == "GdiCharSet")
            {
                return "Набор знаков GDI";
            }
            else
            {
                return p1;
            }
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
            sb.Append(string.Join(", ", type.GetGenericArguments()
                                            .Select(CSharpName)));
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
            string var1 = _pd.GetValue(component).ToString();
            if (var1 == false.ToString())
            {
                return "Ложь";
            }
            else if(var1 == true.ToString())
            {
                return "Истина";
            }
            else if(var1 == System.Drawing.GraphicsUnit.Display.ToString())
            {
                return "дисплей (пиксель для дисплеев)";
            }
            else if (var1 == System.Drawing.GraphicsUnit.Document.ToString())
            {
                return "документ (1/300 дюйма)";
            }
            else if (var1 == System.Drawing.GraphicsUnit.Inch.ToString())
            {
                return "дюйм";
            }
            else if (var1 == System.Drawing.GraphicsUnit.Millimeter.ToString())
            {
                return "миллиметр";
            }
            else if (var1 == System.Drawing.GraphicsUnit.Pixel.ToString())
            {
                return "пиксель";
            }
            else if (var1 == System.Drawing.GraphicsUnit.Point.ToString())
            {
                return "пункт (1/72 дюйма)";
            }
            else if (var1 == System.Drawing.GraphicsUnit.World.ToString())
            {
                return "мировая";
            }
            else
            {
                return _pd.GetValue(component);
            }
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
