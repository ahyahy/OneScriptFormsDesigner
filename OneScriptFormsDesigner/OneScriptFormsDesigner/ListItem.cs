using System;

namespace osfDesigner
{
    public class ListItem
    {
        private string M_Text;
        public object M_Value;

        public ListItem(object p1)
        {
        }

        public string Text
        {
            get
            {
                if (M_Text != null)
                {
                    return M_Text;
                }
                if (M_Value != null)
                {
                    return Convert.ToString(M_Value);
                }
                return "";
            }
            set { M_Text = value; }
        }

        public object Value
        {
            get
            {
                if (M_Value != null)
                {
                    return M_Value;
                }
                if (M_Text != null)
                {
                    return M_Text;
                }
                return "";
            }
            set { M_Value = value; }
        }

        //Методы============================================================

        public override string ToString()
        {
            return Text;
        }
    }
}
