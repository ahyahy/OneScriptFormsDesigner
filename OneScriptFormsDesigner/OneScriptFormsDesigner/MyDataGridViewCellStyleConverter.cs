using System.ComponentModel;
using System.Globalization;
using System;

namespace osfDesigner
{
    class MyDataGridViewCellStyleConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return "СтильЯчейки{}";
            }
            return base.ConvertTo(context, culture, value, destinationType);
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
                pds[i] = new ExpandableDataGridViewCellStylePropertyDescriptor(originalCollection[i]);
            }
            return new PropertyDescriptorCollection(pds);
        }
    }

    public class ExpandableDataGridViewCellStylePropertyDescriptor : PropertyDescriptor
    {
        private PropertyDescriptor _pd;

        public ExpandableDataGridViewCellStylePropertyDescriptor(PropertyDescriptor pd) : base(GetDisplayName(pd.DisplayName), null)
        {
            _pd = pd;
        }

        private static string GetDisplayName(string str)
        {
            if (str == "Alignment")
            {
                return "Выравнивание";
            }
            else if (str == "Padding")
            {
                return "Заполнение";
            }
            else if (str == "ForeColor")
            {
                return "ОсновнойЦвет";
            }
            else if (str == "SelectionForeColor")
            {
                return "ОсновнойЦветВыделенного";
            }
            else if (str == "WrapMode")
            {
                return "Перенос";
            }
            else if (str == "BackColor")
            {
                return "ЦветФона";
            }
            else if (str == "SelectionBackColor")
            {
                return "ЦветФонаВыделенного";
            }
            else if (str == "Font")
            {
                return "Шрифт";
            }
            else if (str == "Format")
            {
                return "Формат";
            }
            else if (str == "NullValue")
            {
                return "СоответствиеДляНеопределено";
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
            object value = _pd.GetValue(component);
            Type valueType = value.GetType();
            string str = value.ToString();
            if (str == System.Windows.Forms.DataGridViewContentAlignment.TopLeft.ToString())
            {
                return "ВерхЛево";
            }
            else if (str == System.Windows.Forms.DataGridViewContentAlignment.TopRight.ToString())
            {
                return "ВерхПраво";
            }
            else if (str == System.Windows.Forms.DataGridViewContentAlignment.TopCenter.ToString())
            {
                return "ВерхЦентр";
            }
            else if (str == System.Windows.Forms.DataGridViewContentAlignment.NotSet.ToString())
            {
                return "НеУстановлено";
            }
            else if (str == System.Windows.Forms.DataGridViewContentAlignment.BottomLeft.ToString())
            {
                return "НизЛево";
            }
            else if (str == System.Windows.Forms.DataGridViewContentAlignment.BottomRight.ToString())
            {
                return "НизПраво";
            }
            else if (str == System.Windows.Forms.DataGridViewContentAlignment.BottomCenter.ToString())
            {
                return "НизЦентр";
            }
            else if (str == System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft.ToString())
            {
                return "СерединаЛево";
            }
            else if (str == System.Windows.Forms.DataGridViewContentAlignment.MiddleRight.ToString())
            {
                return "СерединаПраво";
            }
            else if (str == System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter.ToString())
            {
                return "СерединаЦентр";
            }
            else if (str == System.Windows.Forms.DataGridViewTriState.NotSet.ToString())
            {
                return "НеУстановлено";
            }
            else if (str == System.Windows.Forms.DataGridViewTriState.False.ToString())
            {
                return "Ложь";
            }
            else if (str == System.Windows.Forms.DataGridViewTriState.True.ToString())
            {
                return "Истина";
            }
            else if (valueType == typeof(System.Windows.Forms.Padding))
            {
                return str.Replace("Left=", "Лево=").Replace("Top=", "Верх=").Replace("Right=", "Право=").Replace("Bottom=", "Низ=");
            }
            else if (valueType == typeof(System.Drawing.Color))
            {
                return MyColorConverter.ConvertColorToString(value);
            }
            else if (valueType == typeof(System.Drawing.Font))
            {
                return str.Replace("[Font:", "[Шрифт:")
                    .Replace("Name=", "Имя=")
                    .Replace("Size=", "Размер=")
                    .Replace("Units=", "Единицы=")
                    .Replace("GdiCharSet=", "Набор знаков GDI=")
                    .Replace("GdiVerticalFont=", "Производный от вертикального GDI=")
                    .Replace("=False", "=Ложь")
                    .Replace("=True", "=Истина");
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
