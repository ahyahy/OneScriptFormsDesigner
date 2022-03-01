using System.ComponentModel;
using System;

namespace osfDesigner
{
    public class ListItemComboBox : FilterablePropertyBase
    {
        private string M_Text;
        private object M_Value;

        public ListItemComboBox()
        {
        }

        [DisplayName("ТипЗначения")]
        [Description("Указывает тип значения элемента.")]
        [Category("Прочее")]
        [Browsable(true)]
        public DataType ValueType { get; set; }

        [DisplayName("Текст")]
        [Description("Возвращает или задает текст, отображаемый в элементе списка.")]
        [Category("Прочее")]
        [Browsable(true)]
        [ReadOnly(true)]
        public string Text
        {
            get { return M_Text; }
            set { M_Text = value; }
        }

        [Browsable(false)]
        public object Value
        {
            get { return M_Value; }
            set
            {
                M_Value = value;
                if (value.GetType() == typeof(bool))
                {
                    if ((bool)value)
                    {
                        M_Text = "Истина";
                    }
                    else
                    {
                        M_Text = "Ложь";
                    }
                }
                else
                {
                    M_Text = Convert.ToString(M_Value);
                }
            }
        }

        [DisplayName("Значение")]
        [Description("Возвращает или задает значение, связанное с ЭлементСписка (ListItem).")]
        [Category("Прочее")]
        [TypeConverter(typeof(MyBooleanConverter))]
        [DynamicPropertyFilter("ValueType", "Булево")]
        public bool ValueBool
        {
            get { return (bool)Value; }
            set { Value = value; }
        }

        [DisplayName("Значение")]
        [Description("Возвращает или задает значение, связанное с ЭлементСписка (ListItem).")]
        [Category("Прочее")]
        [DynamicPropertyFilter("ValueType", "Дата")]
        public DateTime ValueDateTime
        {
            get { return (DateTime)Value; }
            set { Value = value; }
        }

        [DisplayName("Значение")]
        [Description("Возвращает или задает значение, связанное с ЭлементСписка (ListItem).")]
        [Category("Прочее")]
        [DynamicPropertyFilter("ValueType", "Строка")]
        public string ValueString
        {
            get { return (string)Value; }
            set { Value = value; }
        }

        [DisplayName("Значение")]
        [Description("Возвращает или задает значение, связанное с ЭлементСписка (ListItem). Для разделения разрядов используйте запятую.")]
        [Category("Прочее")]
        [DynamicPropertyFilter("ValueType", "Число")]
        [DefaultValue(0)]
        public decimal ValueNumber
        {
            get { return Convert.ToDecimal(Value); }
            set { Value = value; }
        }
    }
}
