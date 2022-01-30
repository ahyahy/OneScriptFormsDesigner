using System.Reflection;
using System.Text;
using System.Xml;
using System;

namespace osfDesigner
{
    public static class Serializer
    {
        public static string DeserializeObjects(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            //dynamic obj1 = null;

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                if (!line.Contains(" ") && !line.Contains("/"))
                {
                    System.Windows.Forms.MessageBox.Show(line);// имя объекта
                                                               // создаем объект исходя из его типа
                                                               //obj1 = ...;
                }
                if (line.Contains("/>"))
                {
                    // свойство объекта
                    // делим строку в массив по пробелам
                    // массив[0] - имя свойства
                    // массив[1] - тип свойства
                    // массив[2] - значение свойства
                    string propName = "";
                    string propType = "";
                    string propValue = "";
                    string[] separators = new string[] { " " };
                    string[] result = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    if (result.Length < 3)
                    {
                        continue;
                    }

                    //OneScriptFormsDesigner.StrFindBetween(, ,);

                    for (int i = 0; i < result.Length; i++)
                    {
                        if (i == 0)
                        {
                            propName = result[0];
                        }
                        if (i == 1)
                        {
                            propType = result[1];
                        }
                        if (i == 2)
                        {
                            propValue = result[2];
                        }
                    }
                    System.Windows.Forms.MessageBox.Show(
                        "line = " + line + "\r\n" +
                        "propName = " + propName + "\r\n" +
                        "propType = " + propType + "\r\n" +
                        "propValue = " + propValue);

                }
                if (line.Contains("</"))
                {
                    System.Windows.Forms.MessageBox.Show(line);// свойства объекта закончились
                    //obj1 = null;
                }

            }

            //System.Collections.ArrayList ArrayList1 = OneScriptFormsDesigner.StrFindBetween(str1, "<", ">");
            //System.Windows.Forms.MessageBox.Show("222");
            //for (int i = 0; i < ArrayList1.Count; i++)
            //{
            //    string element = (string)ArrayList1[i];
            //    if (!element.Contains(" ") && !element.Contains("/"))
            //    {
            //        System.Windows.Forms.MessageBox.Show(element);
            //    }
            //}

            sr.Close();
            return "str1";
        }

        public static void SerializeObjects(object[] obj1, string fileName)
        {
            System.Xml.XmlWriterSettings Settings1 = new XmlWriterSettings();
            Settings1.Encoding = new UTF8Encoding(true);
            Settings1.Indent = true;
            System.Xml.XmlWriter writer1 = System.Xml.XmlWriter.Create(fileName, Settings1);

            writer1.WriteStartElement("Objects");

            for (int i = 0; i < obj1.Length; i++)
            {
                dynamic comp = obj1[i];
                PropertyInfo[] properties2 = comp.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                System.Collections.Generic.List<PropertyInfo> la = new System.Collections.Generic.List<PropertyInfo>();
                for (int i1 = 0; i1 < properties2.Length; i1++)
                {
                    PropertyInfo pi = properties2[i1];
                    if (pi.CanRead)
                    {
                        // выгружаем только свойства перечисленные в свойстве RequiredValues объекта,
                        // и только свойства перечисленные в свойстве DefaultValues объекта и при этом 
                        // изменившиеся с момента создания объекта
                        string strDisplayName = OneScriptFormsDesigner.GetDisplayName(comp, pi.Name);
                        if (strDisplayName == "")
                        {
                            continue;
                        }

                        bool isBrowsable = false;
                        // найдите атрибут Browsable.
                        foreach (object attr in pi.GetCustomAttributes(false))
                        {
                            if (attr is System.ComponentModel.BrowsableAttribute)
                            {
                                System.ComponentModel.BrowsableAttribute ba = (System.ComponentModel.BrowsableAttribute)attr;
                                isBrowsable = ba.Browsable;
                            }
                        }
                        // пропустим не отображаемые свойства
                        if (!isBrowsable)
                        {
                            continue;
                        }

                        string str6 = Convert.ToString(pi.GetValue(comp, null));
                        try
                        {
                            str6 = OneScriptFormsDesigner.ObjectConvertToString(pi.GetValue(comp, null));
                        }
                        catch { }

                        //System.Windows.Forms.MessageBox.Show("0valueName = " + strDisplayName + " ==" + "\r\n" +
                        //    "0valueName+strValue = " + strDisplayName + " ==" + " " + str6);
                        if (comp.RequiredValues.Contains(strDisplayName + " =="))
                        {
                            la.Add(pi);
                        }
                        else if (comp.DefaultValues.Contains(strDisplayName + " ==") &&
                                !comp.DefaultValues.Contains(strDisplayName + " ==" + " " + str6))
                        {
                            //System.Windows.Forms.MessageBox.Show("1valueName = " + strDisplayName + " ==" + "\r\n" +
                            //    "1valueName+strValue = " + strDisplayName + " ==" + " " + str6);
                            la.Add(pi);
                        }
                    }

                }

                // добавим к свойствам родителя
                if (comp.GetType() == typeof(osfDesigner.ToolBarButton)) //.Tag
                {
                    comp = comp.OriginalObj;
                }
                else if (comp.GetType() == typeof(osfDesigner.DataGridTableStyle)) //AddToHashtable
                {
                    comp = OneScriptFormsDesigner.RevertOriginalObj(comp);
                }
                ////System.Reflection.PropertyInfo propertyParent = comp.GetType().GetProperty("Parent");
                ////la.Add(propertyParent);
                ////System.Reflection.PropertyInfo propertyName = comp.GetType().GetProperty("Name");
                ////la.Add(propertyName);

                PropertyInfo[] properties = new PropertyInfo[la.Count];
                la.CopyTo(properties, 0);

                writer1.WriteStartElement(comp.GetType().Name);

                try
                {
                    // запишем родителя в обязательном порядке
                    System.Reflection.PropertyInfo propertyParent = comp.GetType().GetProperty("Parent");
                    //var value = propertyParent.GetValue(comp, null);
                    var value = ((System.Windows.Forms.Control)comp).Parent.Name;

                    writer1.WriteStartElement(propertyParent.Name);
                    writer1.WriteAttributeString("Type", propertyParent.PropertyType.Name);
                    writer1.WriteAttributeString("Value", Convert.ToString(value));
                    writer1.WriteEndElement();
                }
                catch { }
                try
                {
                    // запишем имя в обязательном порядке
                    writer1.WriteStartElement("Name");
                    writer1.WriteAttributeString("Type", "String");
                    writer1.WriteAttributeString("Value", ((System.ComponentModel.Component)comp).Site.Name);
                    writer1.WriteEndElement();
                }
                catch { }

                if (properties.Length > 0)
                {
                    foreach (var property in properties)
                    {
                        try
                        {
                            if (property.PropertyType.Name == "MyList")
                            {
                                osfDesigner.MyList MyList1 = (osfDesigner.MyList)comp.Images;
                                if (MyList1.Count > 0)
                                {
                                    for (int i2 = 0; i2 < MyList1.Count; i2++)
                                    {
                                        System.Reflection.PropertyInfo propertyPath = MyList1[i2].GetType().GetProperty("Path");

                                        var value = propertyPath.GetValue(MyList1[i2], null);

                                        writer1.WriteStartElement(propertyPath.Name);
                                        writer1.WriteAttributeString("Type", propertyPath.PropertyType.Name);
                                        writer1.WriteAttributeString("Value", Convert.ToString(value));
                                        writer1.WriteEndElement();
                                    }
                                }
                            }
                            else
                            {
                                var value = property.GetValue(comp, null);

                                writer1.WriteStartElement(property.Name);
                                writer1.WriteAttributeString("Type", property.PropertyType.Name);
                                writer1.WriteAttributeString("Value", Convert.ToString(value));
                                writer1.WriteEndElement();
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                else if (comp.GetType().IsValueType)
                {
                    writer1.WriteValue(comp.ToString());
                }
                writer1.WriteEndElement();
            }
            writer1.Close();
        }
    }
}
