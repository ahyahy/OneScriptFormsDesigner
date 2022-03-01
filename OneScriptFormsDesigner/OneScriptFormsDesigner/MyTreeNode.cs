using System.ComponentModel;
using System.Drawing;

namespace osfDesigner
{
    public class MyTreeNode : System.Windows.Forms.TreeNode
    {
        [DisplayName("Помечен")]
        [Description("Возвращает или задает значение, указывающее, находится ли узел дерева в выбранном состоянии.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool Checked
        {
            get { return base.Checked; }
            set { base.Checked = value; }
        }

        [DisplayName("Индекс")]
        [Description("Возвращает позицию узла дерева в коллекции узлов дерева.")]
        [Category("Поведение")]
        [Browsable(true)]
        [ReadOnly(true)]
        public new int Index
        {
            get { return base.Index; }
        }

        [DisplayName("ИндексВыбранногоИзображения")]
        [Description("Возвращает или задает значение индекса в списке изображений для изображения, которое отображается, если узел дерева находится в выбранном состоянии.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyImageIndexConverter))]
        public new int SelectedImageIndex
        {
            get { return base.SelectedImageIndex; }
            set { base.SelectedImageIndex = value; }
        }

        [DisplayName("ИндексИзображения")]
        [Description("Возвращает или задает значение индекса в списке изображений для изображения, отображаемого, когда узел дерева находится в невыбранном состоянии.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyImageIndexConverter))]
        public new int ImageIndex
        {
            get { return base.ImageIndex; }
            set { base.ImageIndex = value; }
        }

        [DisplayName("ПолныйПуть")]
        [Description("Возвращает путь из корневого узла дерева к текущему узлу дерева.")]
        [Category("Прочее")]
        [Browsable(true)]
        [ReadOnly(true)]
        public new string FullPath
        {
            get { return base.FullPath; }
        }

        [DisplayName("Текст")]
        [Description("Возвращает или задает текст, отображаемый в надписи узла дерева.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        [DisplayName("(Name)")]
        [Description("Указывает имя, используемое в коде для идентификации объекта.")]
        [Category("Разработка")]
        [Browsable(true)]
        [ReadOnly(true)]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        [Browsable(false)]
        public new TreeView TreeView { get; set; }

        [DisplayName("ШрифтУзла")]
        [Description("Возвращает или задает шрифт, используемый для отображения текста надписи узла дерева.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyFontConverter))]
        public new Font NodeFont
        {
            get { return base.NodeFont; }
            set { base.NodeFont = value; }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public new string StateImageKey { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new string StateImageIndex { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new string SelectedImageKey { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new string ImageKey { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.ContextMenuStrip ContextMenu { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.ContextMenuStrip ContextMenuStrip { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new string ToolTipText { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new string BackColor { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new string ForeColor { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new object Tag { get; set; }
	
        [Browsable(false)]
        public string RequiredValues
        {
            get
            {
                return @"
Текст ==
";
            }
        }

        [Browsable(false)]
        public string DefaultValues { get; set; }
    }
}
