using System;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace osfDesigner
{
    public class MyDateCollectionConverter : CollectionConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return "(Коллекция)";
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
	
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            dynamic DatesList1 = value;

            int count1 = DatesList1.Count;
            DateTime[] DateTime1 = new DateTime[count1];
            for (int i = 0; i < DatesList1.Count; i++)
            {
                DateTime1[i] = DatesList1[i].Value;
            }

            if (DatesList1 == null || DatesList1.Count == 0)
            {
                return base.GetProperties(context, value, attributes);
            }

            var items = new PropertyDescriptorCollection(null);
            for (int i = 0; i < DateTime1.Length; i++)
            {
                object item = DateTime1[i];
                items.Add(new ExpandableDatesCollectionPropertyDescriptor(DateTime1, i));
            }
            return items;
        }
    }

    public class ExpandableDatesCollectionPropertyDescriptor : PropertyDescriptor
    {
        private System.DateTime[] collection;
        private readonly int _index;

        public ExpandableDatesCollectionPropertyDescriptor(System.DateTime[] coll, int idx) : base(GetDisplayName(coll, idx), null)
        {
            collection = coll;
            _index = idx;
        }

        private static string GetDisplayName(System.DateTime[] list, int index)
        {
            return "[" + index + "]";
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
            get { return this.collection.GetType(); }
        }

        public override object GetValue(object component)
        {
            return collection[_index];
        }

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override string Name
        {
            get { return _index.ToString(CultureInfo.InvariantCulture); }
        }

        public override Type PropertyType
        {
            get { return collection[_index].GetType(); }
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
            collection[_index] = (DateTime)value;
        }
    }
}
