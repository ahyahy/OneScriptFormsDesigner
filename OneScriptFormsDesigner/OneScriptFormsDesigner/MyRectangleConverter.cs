using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace osfDesigner
{
    public class MyRectangleConverter : RectangleConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection originalCollection = TypeDescriptor.GetProperties(value, attributes);
            PropertyDescriptor[] pds = new PropertyDescriptor[originalCollection.Count];
            originalCollection.CopyTo(pds, 0);
            PropertyDescriptorCollection newCollection = new PropertyDescriptorCollection(pds);
            for (int i = 0; i < originalCollection.Count; i++)
            {
                PropertyDescriptor pd = originalCollection[i];
                List<Attribute> la = new List<Attribute>();
                foreach (Attribute attribute in pd.Attributes)
                {
                    la.Add(attribute);
                }
                MyPropertyDescriptor cp = new MyPropertyDescriptor(pd, la.ToArray());
                newCollection.RemoveAt(i);
                newCollection.Insert(i, cp);
            }
            return newCollection.Sort(new string[] { "Икс", "Игрек", "Ширина", "Высота" });
        }
    }
}
