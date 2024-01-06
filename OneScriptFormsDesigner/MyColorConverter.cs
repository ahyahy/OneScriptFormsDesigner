using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System;

namespace osfDesigner
{
    public class MyColorConverter : ColorConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo cultureInfo, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string fact = value.ToString();
                string[] stringSeparators = new string[] { "[", "]" };
                string[] result = fact.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                // result[1] - это либо имя цвета (Green), либо его RGB значения (A = 255, R = 255, G = 224, B = 192).
                // Меняем result[1] на значение из словаря colors.
                string[] stringSeparators2 = new string[] { ", " };
                string[] result2 = result[1].Split(stringSeparators2, StringSplitOptions.RemoveEmptyEntries);
                if (result2.Length == 4) // Цвет в ARGB.
                {
                    return fact.Replace("Color", "Цвет");
                }
                else
                {
                    try
                    {
                        return "Цвет [" + OneScriptFormsDesigner.colors[result[1]] + "]";
                    }
                    catch
                    {
                        Color Color1 = (Color)value;
                        return "Цвет [A=" + Color1.A + ", R=" + Color1.R + ", G=" + Color1.G + ", B=" + Color1.B + "]";
                    }
                }
            }
            return base.ConvertTo(context, cultureInfo, RuntimeHelpers.GetObjectValue(value), destinationType);
        }

        public new static string ConvertToString(object value)
        {
            try
            {
                return OneScriptFormsDesigner.colors[value.ToString()];
            }
            catch
            {
                return value.ToString();
            }
        }
	
        public static string ConvertColorToString(object value)
        {
            string fact = value.ToString();
            string[] stringSeparators = new string[] { "[", "]" };
            string[] result = fact.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            // result[1] - это либо имя цвета (Green), либо его RGB значения (A = 255, R = 255, G = 224, B = 192).
            // Меняем result[1] на значение из словаря colors.
            string[] stringSeparators2 = new string[] { ", " };
            string[] result2 = result[1].Split(stringSeparators2, StringSplitOptions.RemoveEmptyEntries);
            if (result2.Length == 4) // Цвет в ARGB.
            {
                return fact.Replace("Color", "Цвет");
            }
            else
            {
                try
                {
                    return "Цвет [" + OneScriptFormsDesigner.colors[result[1]] + "]";
                }
                catch
                {
                    Color Color1 = (Color)value;
                    return "Цвет [A=" + Color1.A + ", R=" + Color1.R + ", G=" + Color1.G + ", B=" + Color1.B + "]";
                }
            }
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType.Equals(typeof(string)))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo cultureInfo, object value)
        {
            if (!(value is string))
            {
                return base.ConvertFrom(context, cultureInfo, RuntimeHelpers.GetObjectValue(value));
            }
            string[] stringSeparators = new string[] { "[", "]" };
            string[] result = ((string)value).Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            // result[1] - это либо имя цвета (Green), либо его RGB значения (A = 255, R = 255, G = 224, B = 192).
            // Из него нужно создать Color.
            string[] stringSeparators2 = new string[] { ", " };
            string[] result2 = result[1].Split(stringSeparators2, StringSplitOptions.RemoveEmptyEntries);
            if (result2.Length == 4)
            {
                int A = Convert.ToInt32(result2[0].Replace("A=", ""));
                int R = Convert.ToInt32(result2[1].Replace("R=", ""));
                int G = Convert.ToInt32(result2[2].Replace("G=", ""));
                int B = Convert.ToInt32(result2[3].Replace("B=", ""));
                return Color.FromArgb(255, R, G, B);
            }
            else
            {
                Color Color2 = Color.FromName(result[1]);
                if ((Color2.A + Color2.R + Color2.G + Color2.B) == 0)
                {
                    try
                    {
                        return Color.FromName(OneScriptFormsDesigner.colors[result[1]]);
                    }
                    catch
                    {
                        return Color.FromArgb(255, 0, 0, 0);
                    }
                }
                return Color.FromName(result[1]);
            }
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return false;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] Attribute)
        {
            return TypeDescriptor.GetProperties(RuntimeHelpers.GetObjectValue(value));
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return false;
        }
    }
}
