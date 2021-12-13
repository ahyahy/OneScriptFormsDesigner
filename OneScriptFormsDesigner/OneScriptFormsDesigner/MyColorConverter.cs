using System;
using System.Drawing;
using System.ComponentModel; 
using System.Globalization;
using System.Runtime.CompilerServices;

namespace osfDesigner
{
    public class MyColorConverter : ColorConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
        {
            return true;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo cultureInfo, object value, System.Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string res1 = value.ToString();
                string[] stringSeparators = new string[] { "[", "]" };
                string[] result = res1.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                //result[1] - это либо имя цвета (Green), либо его RGB значения (A = 255, R = 255, G = 224, B = 192)
                //меняем result[1] на значение из словаря colors
                string[] stringSeparators2 = new string[] { ", " };
                string[] result2 = result[1].Split(stringSeparators2, StringSplitOptions.RemoveEmptyEntries);
                if (result2.Length == 4)//цвет в ARGB
                {
                    return res1.Replace("Color", "Цвет");
                }
                else
                {
                    try
                    {
                        return "Цвет [" + osfDesigner.OneScriptFormsDesigner.colors[result[1]] + "]";
                    }
                    catch
                    {
                        System.Drawing.Color Color1 = (System.Drawing.Color)value;
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
                return osfDesigner.OneScriptFormsDesigner.colors[value.ToString()];
            }
            catch
            {
                return value.ToString();
            }
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
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
            //result[1] - это либо имя цвета (Green), либо его RGB значения (A = 255, R = 255, G = 224, B = 192)
            //из него нужно создать System.Drawing.Color
            string[] stringSeparators2 = new string[] { ", " };
            string[] result2 = result[1].Split(stringSeparators2, StringSplitOptions.RemoveEmptyEntries);
            if (result2.Length == 4)
            {
                int A = Convert.ToInt32(result2[0].Replace("A=", ""));
                int R = Convert.ToInt32(result2[1].Replace("R=", ""));
                int G = Convert.ToInt32(result2[2].Replace("G=", ""));
                int B = Convert.ToInt32(result2[3].Replace("B=", ""));
                return System.Drawing.Color.FromArgb(255, R, G, B);
            }
            else
            {
                System.Drawing.Color Color2 = System.Drawing.Color.FromName(result[1]);
                if ((Color2.A + Color2.R + Color2.G + Color2.B) == 0 )
                {
                    try
                    {
                        return System.Drawing.Color.FromName(osfDesigner.OneScriptFormsDesigner.colors[result[1]]);
                    }
                    catch
                    {
                        return System.Drawing.Color.FromArgb(255, 0, 0, 0);
                    }
                }
                else
                {
                    return System.Drawing.Color.FromName(result[1]);
                }
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
