using System;
using System.Linq;
using System.ComponentModel.Design.Serialization;
using System.ComponentModel;

namespace osfDesigner
{
    // NameCreationServiceImp - реализовывает INameCreationService
    // Интерфейс INameCreationService используется для предоставления имени только что созданному элементу управления
    // В CreateName() используется тот же алгоритм именования, что и Visual Studio:
    // приращение целочисленного счетчика до тех пор, пока не будет найдено имя, которое еще не используется.

    internal class NameCreationServiceImp : INameCreationService
    {
        private const string _Name_ = "NameCreationServiceImp";

        public NameCreationServiceImp()
        {
        }

        public string CreateName( IContainer container, Type type )
        {
            string str1;
            string type_Name = type.Name;

            if ( null == container)
            {
                return string.Empty;
            }

            ComponentCollection cc = container.Components;
            int min = Int32.MaxValue;
            int max = Int32.MinValue;
            int count = 0;

            int i = 0;
            while ( i < cc.Count )
            {
                Component comp = cc[i] as Component;
                for (int i2 = 0; i2 < osfDesigner.OneScriptFormsDesigner.namesEnRu.Count; i2++)
                {
                    string key1 = osfDesigner.OneScriptFormsDesigner.namesEnRu.ElementAt(i2).Key;
                    string value1 = osfDesigner.OneScriptFormsDesigner.namesEnRu.ElementAt(i2).Value;

                    if (type_Name.Contains(key1))
                    {
                        type_Name = type_Name.Replace(key1, value1);
                    }
                }

                if ( comp.GetType() == type )
                {
                    count++;
                    string name = comp.Site.Name;

                    for (int i2 = 0; i2 < osfDesigner.OneScriptFormsDesigner.namesEnRu.Count; i2++)
                    {
                        string key1 = osfDesigner.OneScriptFormsDesigner.namesEnRu.ElementAt(i2).Key;
                        string value1 = osfDesigner.OneScriptFormsDesigner.namesEnRu.ElementAt(i2).Value;

                        if (name.Contains(key1))
                        {
                            name = name.Replace(key1, value1);
                        }
                    }

                    if ( name.StartsWith (type_Name) )
                    {
                        try
                        {
                            int value = Int32.Parse ( name.Substring (type_Name.Length ) );
                            if ( value < min ) min = value;
                            if ( value > max ) max = value;
                        }
                        catch { }
                    }
                }
                i++;
            }

            if ( 0 == count )
            {
                str1 = type_Name + "1";
            }
            else if ( min > 1 )
            {
                int j = min - 1;
                str1 = type_Name + j.ToString();
            }
            else
            {
                int j = max + 1;
                str1 = type_Name + j.ToString();
            }
            return str1;
        }

        public bool IsValidName ( string name )
        {
            // Проверка того, что имя не пустое и что это строка хотя бы из одного символа
            if ( String.IsNullOrEmpty ( name ))
            {
                return false;
            }

            // далее первый символ должен быть буквой
            if ( ! ( char.IsLetter ( name, 0 ) ))
            {
                return false;
            }

            // затем не допустим ведущего подчеркивания
            if ( name.StartsWith ( "_" ))
            {
                return false;
            }

            // хорошо, это допустимое имя
            return true;
        }

        public void ValidateName ( string name )
        {
            const string _signature_ = _Name_ + @"::ValidateName()";

            //  Используем этот метод для проверки, если он завершится неудачей, создадим исключение
            if ( ! ( IsValidName ( name ) ))
            {
                throw new ArgumentException( _signature_ + " - Exception: Invalid name: " + name  );
            }
        }
    }
}
