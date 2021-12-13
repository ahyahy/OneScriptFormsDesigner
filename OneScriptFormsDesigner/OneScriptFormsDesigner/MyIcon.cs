using System;
using System.ComponentModel;
using System.Drawing;

namespace osfDesigner
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MyIcon
    {
        public System.Drawing.Icon M_Icon;

        public MyIcon()
        {
        }

        public MyIcon(MyIcon p1)
        {
            M_Icon = p1.M_Icon;
        }

        public MyIcon(System.Drawing.Icon p1)
        {
            M_Icon = p1;
        }

        public MyIcon(string p1)
        {
            M_Icon = null;
            try
            {
                System.Drawing.Bitmap Bitmap = new System.Drawing.Bitmap((System.IO.Stream)new System.IO.MemoryStream(Convert.FromBase64String(p1)));
                IntPtr Hicon = Bitmap.GetHicon();
                System.Drawing.Icon Icon1 = System.Drawing.Icon.FromHandle(Hicon);
                M_Icon = Icon1;
            }
            catch { }
            try
            {
                M_Icon = new System.Drawing.Icon((System.IO.Stream)new System.IO.MemoryStream(Convert.FromBase64String(p1)));
            }
            catch { }
            if (M_Icon == null)
            {
                M_Icon = new System.Drawing.Icon(p1);
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
