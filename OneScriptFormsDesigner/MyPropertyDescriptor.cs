using System.ComponentModel;
using System;

namespace osfDesigner
{
    // Один из вариантов показать в сетке свойств расширяемое свойство.
    // Так можно вывести русские названия свойств. Тип раскрываемого свойства должен соответствовать унаследованному классу, если 
    // их нужно будет редактировать тут же в раскытом списке. На расскытые свойства возможно нужно будет указать свой редактор.
    // Вызывается в конвертере примерно так:
    ////public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
    ////{
    ////    //MySelectionRange MySelectionRange1 = new MySelectionRange((SelectionRange)value);
    ////    //PropertyDescriptorCollection originalCollection = TypeDescriptor.GetProperties(MySelectionRange1, attributes);

    ////    //PropertyDescriptorCollection originalCollection = TypeDescriptor.GetProperties(value, attributes);

    ////    PropertyDescriptorCollection originalCollection = TypeDescriptor.GetProperties(typeof(MySelectionRange), attributes);
    ////    PropertyDescriptor[] pds = new PropertyDescriptor[originalCollection.Count];
    ////    originalCollection.CopyTo(pds, 0);
    ////    PropertyDescriptorCollection newCollection = new PropertyDescriptorCollection(pds);
    ////    for (int i = 0; i < originalCollection.Count; i++)
    ////    {
    ////        PropertyDescriptor pd = originalCollection[i];
    ////        List<Attribute> la = new List<Attribute>();
    ////        foreach (Attribute attribute in pd.Attributes)
    ////        {
    ////            la.Add(attribute);
    ////        }
    ////        MyPropertyDescriptor cp = new MyPropertyDescriptor(pd, la.ToArray());
    ////        newCollection.RemoveAt(i);
    ////        newCollection.Insert(i, cp);
    ////    }
    ////    return newCollection;
    ////}

    public class MyPropertyDescriptor : PropertyDescriptor
    {
        private PropertyDescriptor _innerPropertyDescriptor;
        private bool _ronly;

        public MyPropertyDescriptor(PropertyDescriptor inner, Attribute[] attrs) : base(GetDisplayName(inner), attrs)
        {
            _innerPropertyDescriptor = inner;
            _ronly = inner.IsReadOnly;
        }

        public static string GetDisplayName(PropertyDescriptor inner)
        {
            if (inner.Name == "Start")
            {
                return "Начало";
            }
            else if (inner.Name == "End")
            {
                return "Конец";
            }
            else if (inner.Name == "Width")
            {
                return "Ширина";
            }
            else if (inner.Name == "Height")
            {
                return "Высота";
            }
            else if (inner.Name == "X")
            {
                return "Икс";
            }
            else if (inner.Name == "Y")
            {
                return "Игрек";
            }
            return inner.Name;
        }

        public override object GetValue(object component)
        {
            return _innerPropertyDescriptor.GetValue(component);
        }

        public override bool SupportsChangeEvents
        {
            get { return true; }
        }

        public override Type PropertyType
        {
            get { return _innerPropertyDescriptor.PropertyType; }
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
            _innerPropertyDescriptor = (MyPropertyDescriptor)value;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get { return _innerPropertyDescriptor.PropertyType; }
        }

        public override bool IsReadOnly
        {
            get
            {
                return true;
            }
        }
    }
}
