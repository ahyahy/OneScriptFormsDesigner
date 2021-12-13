using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace osfDesigner
{
    [DesignTimeVisible(false)]
    public class ImageEntry : Component
    {
        public System.Drawing.Image M_Bitmap;

        public ImageEntry()
        {
        }

        [DefaultValue(typeof(System.Drawing.Image), null)]
        [DisplayName("Изображение")]
        [Description("Возвращает изображение.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyImageConverter))]
        [Editor(typeof(MyImageFileNameEditor), typeof(UITypeEditor))]
        [ReadOnly(true)]
        public System.Drawing.Image Image
        {
            get { return M_Bitmap; }
            set { M_Bitmap = value; }
        }

        [DisplayName("Путь")]
        [Description("Путь до файла изображения.")]
        [Category("Прочее")]
        [Browsable(true)]
        [DefaultValue(typeof(String), null)]
        [ReadOnly(true)]
        public string Path { get; set; }

        [Browsable(false)]
        public string FileName { get; set; }

        [DisplayName("ФорматФайлаИзображения")]
        [Description("Возвращает объект ФорматИзображения (ImageFormat), представляющий формат этого объекта изображения.")]
        [Category("Прочее")]
        [Browsable(true)]
        public System.Drawing.Imaging.ImageFormat RawFormat
        {
            get { return M_Bitmap.RawFormat; }
        }

        [DisplayName("ФорматПикселей")]
        [Description("Возвращает формат пикселей для этого объекта Изображение (Image).")]
        [Category("Прочее")]
        [Browsable(true)]
        public System.Drawing.Imaging.PixelFormat PixelFormat
        {
            get { return M_Bitmap.PixelFormat; }
        }

        [DisplayName("Размер")]
        [Description("Возвращает ширину и высоту объекта.")]
        [Category("Макет")]
        [Browsable(true)]
        [TypeConverter(typeof(MySizeConverter))]
        [Editor(typeof(MySizeEditor), typeof(UITypeEditor))]
        public System.Drawing.Size Size
        {
            get { return M_Bitmap.Size; }
        }
    }
}
