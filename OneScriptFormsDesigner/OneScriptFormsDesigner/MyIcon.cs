using System.ComponentModel;
using System.Drawing;
using System.IO;
using System;

namespace osfDesigner
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MyIcon
    {
        public Icon M_Icon;

        public MyIcon()
        {
        }

        public MyIcon(MyIcon p1)
        {
            M_Icon = p1.M_Icon;
        }

        public MyIcon(Icon p1)
        {
            M_Icon = p1;
        }

        public MyIcon(string p1)
        {
            M_Icon = null;
            try
            {
                Bitmap Bitmap = new Bitmap(new MemoryStream(Convert.FromBase64String(p1)));
                IntPtr Hicon = Bitmap.GetHicon();
                Icon Icon1 = Icon.FromHandle(Hicon);
                M_Icon = Icon1;
            }
            catch { }
            try
            {
                M_Icon = new Icon(new MemoryStream(Convert.FromBase64String(p1)));
            }
            catch { }
            if (M_Icon == null)
            {
                M_Icon = new Icon(p1);
            }
        }

        [DisplayName("Высота")]
        [Description("Возвращает высоту этого объекта Значок (Icon).")]
        [Browsable(true)]
        public int Height
        {
            get { return M_Icon.Height; }
        }

        [DisplayName("Путь")]
        [Description("Путь до файла изображения.")]
        [Browsable(true)]
        [DefaultValue(typeof(String), null)]
        [ReadOnly(true)]
        public string Path { get; set; }

        [DisplayName("Ширина")]
        [Description("Возвращает ширину этого объекта Значок (Icon).")]
        [Browsable(true)]
        public int Width
        {
            get { return M_Icon.Width; }
        }
    }
}
