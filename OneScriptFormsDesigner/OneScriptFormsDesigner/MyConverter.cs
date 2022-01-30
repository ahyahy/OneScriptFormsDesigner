using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System;

namespace osfDesigner
{
    public class MyConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo cultureInfo, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value.GetType() == typeof(ComboBox))
                {
                    return "ПолеВыбора, Элементов: " + ((ComboBox)value).Items.Count;
                }
                return value.ToString();
            }
            return base.ConvertTo(context, cultureInfo, RuntimeHelpers.GetObjectValue(value), destinationType);
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
                pds[i] = new ExpandablePropertyDescriptor(originalCollection[i]);
            }
            return new PropertyDescriptorCollection(pds);
        }
    }

    public class ExpandablePropertyDescriptor : PropertyDescriptor
    {
        private PropertyDescriptor _pd;

        public ExpandablePropertyDescriptor(PropertyDescriptor pd) : base(GetDisplayName(pd.DisplayName), null)
        {
            _pd = pd;
        }

        private static string GetDisplayName(string p1)
        {
            return p1;
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
            object comp = _pd.GetValue(component);
            Type compType = null;
            try
            {
                compType = comp.GetType();
            }
            catch { }
            if (compType != null)
            {
                if (comp.GetType() == typeof(System.Windows.Forms.ComboBox.ObjectCollection))
                {
                    return "(Коллекция)";
                }
            }
            return OneScriptFormsDesigner.ObjectConvertToString(comp);
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
