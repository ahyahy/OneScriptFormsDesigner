using System.ComponentModel;
using System.Drawing.Design; 

namespace osfDesigner
{
    public class ImageList : System.Windows.Forms.Timer
    {
        MyList _images = new MyList();
        public System.Windows.Forms.ImageList M_ImageList;

        public ImageList()
        {
            M_ImageList = new System.Windows.Forms.ImageList();
        }
        
        [Browsable(false)]
        public System.Windows.Forms.ImageList OriginalObj
        {
            get { return M_ImageList; }
            set { M_ImageList = value; }
        }

        [DisplayName("ГлубинаЦвета")]
        [Description("Возвращает или задает глубину цвета.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public ColorDepth ColorDepth
        {
            get { return (ColorDepth)M_ImageList.ColorDepth; }
            set { M_ImageList.ColorDepth = (System.Windows.Forms.ColorDepth)value; }
        }

        [DisplayName("Изображения")]
        [Description("Возвращает коллекцию Изображения (ImageCollection).")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyCollectionConverter))]
        [Editor(typeof(MyCollectionEditor), typeof(UITypeEditor))]
        public osfDesigner.MyList Images
        {
            get { return _images; }
        }

        [DisplayName("РазмерИзображения")]
        [Description("Возвращает или задает размер изображений в СписокИзображений (ImageList).")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MySizeConverter))]
        [Editor(typeof(MySizeEditor), typeof(UITypeEditor))]
        public System.Drawing.Size ImageSize
        {
            get { return M_ImageList.ImageSize; }
            set { M_ImageList.ImageSize = value; }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public new object Tag { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new bool Enabled { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new int Interval { get; set; }

        [Browsable(false)]
        public string DefaultValues { get; set; }
			
        [Browsable(false)]
        public string RequiredValues
        {
            get
            {
                return @"
Изображения ==
";
            }
        }
    }
}
