using System.ComponentModel;
using System.Drawing.Design; 

namespace osfDesigner
{
    public class MainMenu : System.Windows.Forms.MainMenu
    {
        System.Windows.Forms.Menu.MenuItemCollection _menuItems;
        private System.Windows.Forms.TreeView treeView;

        public MainMenu()
        {
            _menuItems = base.MenuItems;
            this.Tag = new System.Windows.Forms.TreeView();
            treeView = (System.Windows.Forms.TreeView)this.Tag;
        }

        [Browsable(false)]
        public osfDesigner.frmMenuItems FrmMenuItems { get; set; }
		
        [Browsable(false)]
        public System.Windows.Forms.TreeView TreeView
        {
            get { return treeView; }
        }

        [DisplayName("ЭлементыМеню")]
        [Description("Возвращает значение, указывающее на коллекцию объектов ЭлементМеню (MenuItem), связанных с меню.")]
        [Category("Прочее")]
        [Browsable(true)]
        [TypeConverter(typeof(MyCollectionConverter))]
        [Editor(typeof(MyMenuItemsEditor), typeof(UITypeEditor))]
        public new System.Windows.Forms.Menu.MenuItemCollection MenuItems
        {
            get { return _menuItems; }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public new object Tag { get; set; }

        [Browsable(false)]
        [ReadOnly(true)]
        public new System.Windows.Forms.RightToLeft RightToLeft { get; set; }

        [Browsable(false)]
        public string DefaultValues { get; set; }
			
        [Browsable(false)]
        public string RequiredValues
        {
            get
            {
                return @"
ЭлементыМеню ==
";
            }
        }
    }
}
