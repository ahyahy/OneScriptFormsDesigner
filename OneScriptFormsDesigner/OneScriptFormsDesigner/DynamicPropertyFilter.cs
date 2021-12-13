using System;
using System.ComponentModel;

namespace osfDesigner
{
    // Атрибут для поддержки динамически показываемых свойств
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    class DynamicPropertyFilterAttribute : Attribute
    {
        string _propertyName;// Название свойства, от которого будет зависеть видимость
        string _showOn;// Значения свойства, от которого зависит видимость (через запятую, если несколько), при котором свойство, к которому применен атрибут, будет видимо. 
        
        public string PropertyName
        {
            get { return _propertyName; }
        }

        public string ShowOn
        {
            get { return _showOn; }
        }

        // Конструктор  
        // <param name="propName">Название свойства, от которого будет зависеть видимость</param>
        // <param name="value">Значения свойства (через запятую, если несколько), при котором свойство, к которому применен атрибут, будет видимо.</param>
        public DynamicPropertyFilterAttribute(string propertyName, string value)
        {
            _propertyName = propertyName;
            _showOn = value;
        }
    }

    // Базовый класс для объектов, поддерживающих динамическое отображение свойств в PropertyGrid
    public class FilterablePropertyBase : ICustomTypeDescriptor
    {
        protected PropertyDescriptorCollection GetFilteredProperties(Attribute[] attributes)
        {
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(this, attributes, true);
            PropertyDescriptorCollection finalProps = new PropertyDescriptorCollection(new PropertyDescriptor[0]);

            foreach (PropertyDescriptor pd in pdc)
            {
                bool include = false;
                bool dynamic = false;

                foreach (Attribute a in pd.Attributes)
                {
                    if (a is DynamicPropertyFilterAttribute)
                    {
                        dynamic = true;
                        DynamicPropertyFilterAttribute dpf = (DynamicPropertyFilterAttribute)a;
                        PropertyDescriptor temp = pdc[dpf.PropertyName];

                        if (dpf.ShowOn.IndexOf(temp.GetValue(this).ToString()) > -1)
                        {
                            include = true;
                        }
                    }
                }

                if (!dynamic || include)
                {
                    finalProps.Add(pd);
                }
            }

            return finalProps;
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetFilteredProperties(attributes);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return GetFilteredProperties(new Attribute[0]);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }
    }
}
