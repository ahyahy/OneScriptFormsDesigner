using System;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace osfDesigner
{
    public class MyTreeNodeCollectionEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService wfes = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            if (wfes != null)
            {
                frmNodes _frmNodes = new frmNodes(context, value);
                _frmNodes._wfes = wfes;

                if (wfes.ShowDialog(_frmNodes) == System.Windows.Forms.DialogResult.OK)
                {
                }
            }
            return null;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            // Вызов модального диалогового окна, в котором запрашивается ввод пользователем данных.
            return UITypeEditorEditStyle.Modal;
        }
    }

    public class frmNodes : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label2;
        public TreeView TreeView1;
        public TreeView TreeViewOld;
        public TreeView TreeViewOriginal;
        private System.Windows.Forms.Button ButtonAddRoot;
        private System.Windows.Forms.Button ButtonAddChild;
        private System.Windows.Forms.Button ButtonMoveUp;
        private System.Windows.Forms.Button ButtonMoveDown;
        private System.Windows.Forms.Button ButtonDelete;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonCollapse;
        private System.Windows.Forms.Button ButtonExpand;
        private System.Windows.Forms.Button ButtonOK;
        private System.Windows.Forms.PropertyGrid PropertyGrid1;
        private System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.Panel Panel3;
        private System.Windows.Forms.Panel Panel4;
        private System.Windows.Forms.Panel Panel5;
        private System.Windows.Forms.Panel Panel6;
        private System.Windows.Forms.Panel Panel7;
        private System.Windows.Forms.Panel Panel8;
        private System.Windows.Forms.Panel Panel9;
        private System.Windows.Forms.Panel Panel10;
        private System.Windows.Forms.Panel Panel11;
        private System.Windows.Forms.Panel Panel12;
        private System.Windows.Forms.Panel Panel13;
        private System.Windows.Forms.Panel Panel14;
        private System.ComponentModel.Container components = null;
        private ITypeDescriptorContext _context;
        private object _value;
        public IWindowsFormsEditorService _wfes;

        public frmNodes(ITypeDescriptorContext context, object value)
        {
            _context = context;
            _value = value;
            this.Size = new System.Drawing.Size(864, 485);
            this.Text = "Редактор узлов дерева";
            this.ControlBox = true;
            this.HelpButton = true;
            this.ShowIcon = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.SizeGripStyle = SizeGripStyle.Auto;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNodes";
            this.ShowInTaskbar = false;
            this.MinimumSize = new System.Drawing.Size(797, 485);
            this.Closed += frmNodes_Closed;
            this.Load += FrmNodes_Load;
            this.CenterToScreen();

            // правая панель с сеткой свойств PropertyGrid1, надписью Label2 и кнопками ButtonOK и ButtonCancel
            Panel2 = new System.Windows.Forms.Panel();
            Panel2.Parent = this;
            Panel2.Dock = System.Windows.Forms.DockStyle.Fill;

            // левая панель с деревом TreeView1, надписью Label1 и кнопками ButtonAddRoot, ButtonAddChild, ButtonMoveUp, ButtonMoveDown и ButtonDelete и ButtonCollapse и ButtonExpand
            Panel1 = new System.Windows.Forms.Panel();
            Panel1.Parent = this;
            Panel1.Dock = System.Windows.Forms.DockStyle.Left;
            Panel1.Width = 430;

            // панель с ButtonMoveUp, ButtonMoveDown и ButtonDelete и ButtonCollapse и ButtonExpand
            Panel3 = new System.Windows.Forms.Panel();
            Panel3.Parent = Panel1;
            Panel3.Dock = System.Windows.Forms.DockStyle.Fill;

            // панель с Label1, TreeView1, ButtonAddRoot и ButtonAddChild
            Panel4 = new System.Windows.Forms.Panel();
            Panel4.Parent = Panel1;
            Panel4.Dock = System.Windows.Forms.DockStyle.Left;
            Panel4.Width = 353;

            // отступ слева от края формы
            Panel5 = new System.Windows.Forms.Panel();
            Panel5.Parent = Panel1;
            Panel5.Dock = System.Windows.Forms.DockStyle.Left;
            Panel5.Width = 16;

            // панель с TreeView1
            Panel6 = new System.Windows.Forms.Panel();
            Panel6.Parent = Panel4;
            Panel6.Dock = System.Windows.Forms.DockStyle.Fill;

            // панель с Label1
            Panel7 = new System.Windows.Forms.Panel();
            Panel7.Parent = Panel4;
            Panel7.Dock = System.Windows.Forms.DockStyle.Top;
            Panel7.Height = 40;

            // панель с ButtonAddRoot и ButtonAddChild
            Panel8 = new System.Windows.Forms.Panel();
            Panel8.Parent = Panel4;
            Panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            Panel8.Height = 87;

            // панель с ButtonOK и ButtonCancel PropertyGrid1 Label2
            Panel9 = new System.Windows.Forms.Panel();
            Panel9.Parent = Panel2;
            Panel9.Dock = System.Windows.Forms.DockStyle.Fill;

            // отступ справа от края формы
            Panel10 = new System.Windows.Forms.Panel();
            Panel10.Parent = Panel2;
            Panel10.Dock = System.Windows.Forms.DockStyle.Right;
            Panel10.Width = 16;

            // панель с PropertyGrid1
            Panel12 = new System.Windows.Forms.Panel();
            Panel12.Parent = Panel9;
            Panel12.Dock = System.Windows.Forms.DockStyle.Fill;

            // панель с Label2
            Panel11 = new System.Windows.Forms.Panel();
            Panel11.Parent = Panel9;
            Panel11.Dock = System.Windows.Forms.DockStyle.Top;
            Panel11.Height = 40;

            // панель с ButtonOK и ButtonCancel
            Panel13 = new System.Windows.Forms.Panel();
            Panel13.Parent = Panel9;
            Panel13.Dock = System.Windows.Forms.DockStyle.Bottom;
            Panel13.Height = 53;

            // панель для выравнивания справа кнопок ButtonOK и ButtonCancel
            Panel14 = new System.Windows.Forms.Panel();
            Panel14.Parent = Panel13;
            Panel14.Dock = System.Windows.Forms.DockStyle.Right;
            Panel14.Width = 234;

            ButtonAddRoot = new System.Windows.Forms.Button();
            ButtonAddRoot.Parent = Panel8;
            ButtonAddRoot.Bounds = new System.Drawing.Rectangle(1, 8, 172, 28);
            ButtonAddRoot.Text = "Добавить ко&рень";
            ButtonAddRoot.Click += ButtonAddRoot_Click;

            ButtonAddChild = new System.Windows.Forms.Button();
            ButtonAddChild.Parent = Panel8;
            ButtonAddChild.Bounds = new System.Drawing.Rectangle(181, 8, 173, 28);
            ButtonAddChild.Text = "Добавить &ветвь";
            ButtonAddChild.Click += ButtonAddChild_Click;

            Label1 = new System.Windows.Forms.Label();
            Label1.Parent = Panel7;
            Label1.Bounds = new System.Drawing.Rectangle(1, 16, 176, 20);
            Label1.Text = "Выберите у&зел для правки:";

            TreeView1 = new osfDesigner.TreeView();
            TreeView1.Parent = Panel6;
            TreeView1.Dock = (DockStyle)5;
            TreeView1.HideSelection = false;
            TreeView1.AfterSelect += TreeView1_AfterSelect;
            TreeView1.AfterCheck += TreeView1_AfterCheck;

            ButtonMoveUp = new System.Windows.Forms.Button();
            ButtonMoveUp.Parent = Panel3;
            ButtonMoveUp.Bounds = new System.Drawing.Rectangle(10, 41, 35, 28);
            ButtonMoveUp.Text = "";
            ButtonMoveUp.Image = new System.Drawing.Bitmap((System.IO.Stream)new System.IO.MemoryStream(Convert.FromBase64String("AAABAAEAEBACAAEAAQCwAAAAFgAAACgAAAAQAAAAIAAAAAEAAQAAAAAAgAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAA////AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD8P////D////w////8P////D////w////8P///AAD//wAA//+AAf//wAP//+AH///wD///+B////w////+f///")));
            ButtonMoveUp.Enabled = false;
            ButtonMoveUp.Click += ButtonMoveUp_Click;

            ButtonMoveDown = new System.Windows.Forms.Button();
            ButtonMoveDown.Parent = Panel3;
            ButtonMoveDown.Bounds = new System.Drawing.Rectangle(10, 75, 35, 28);
            ButtonMoveDown.Text = "";
            ButtonMoveDown.Image = new System.Drawing.Bitmap((System.IO.Stream)new System.IO.MemoryStream(Convert.FromBase64String("AAABAAEAEBACAAEAAQCwAAAAFgAAACgAAAAQAAAAIAAAAAEAAQAAAAAAgAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAA////AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD+f////D////gf///wD///4Af//8AD//+AAf//AAD//wAA///8P////D////w////8P////D////w////8P///")));
            ButtonMoveDown.Enabled = false;
            ButtonMoveDown.Click += ButtonMoveDown_Click;

            ButtonDelete = new System.Windows.Forms.Button();
            ButtonDelete.Parent = Panel3;
            ButtonDelete.Bounds = new System.Drawing.Rectangle(10, 111, 35, 28);
            ButtonDelete.Text = "";
            ButtonDelete.Image = new System.Drawing.Bitmap((System.IO.Stream)new System.IO.MemoryStream(Convert.FromBase64String("AAABAAEAEBAQAAEABAAoAQAAFgAAACgAAAAQAAAAIAAAAAEABAAAAAAAwAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAACAAACAAAAAgIAAgAAAAIAAgACAgAAAgICAAMDAwAAAAP8AAP8AAAD//wD/AAAA/wD/AP//AAD///8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD//////////5/x//+P8f//g+P//+HD///wh///+A////w////4D///4IP//4HB//+H8P//n/j/////////////")));
            ButtonDelete.Enabled = false;
            ButtonDelete.Click += ButtonDelete_Click;

            ButtonExpand = new System.Windows.Forms.Button();
            ButtonExpand.Parent = Panel3;
            ButtonExpand.Bounds = new System.Drawing.Rectangle(10, 146, 35, 28);
            ButtonExpand.Text = "";
            ButtonExpand.Image = new System.Drawing.Bitmap((System.IO.Stream)new System.IO.MemoryStream(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAADAFBMVEUBAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACnEzGyAAAAAXRSTlMAQObYZgAAADpJREFUeNpjZIAARob/MAac+o8kACH/w5lQZWARRiQ+WATBYUAxFEmAkaAAYTOwWovuMEynY3oOyfsAOL4KE5tREc4AAAAASUVORK5CYII=")));
            ButtonExpand.Click += ButtonExpand_Click;
            System.Windows.Forms.ToolTip ToolTip4 = new System.Windows.Forms.ToolTip();
            ToolTip4.SetToolTip(ButtonExpand, "Развернуть все");

            ButtonCollapse = new System.Windows.Forms.Button();
            ButtonCollapse.Parent = Panel3;
            ButtonCollapse.Bounds = new System.Drawing.Rectangle(10, 181, 35, 28);
            ButtonCollapse.Text = "";
            ButtonCollapse.Image = new System.Drawing.Bitmap((System.IO.Stream)new System.IO.MemoryStream(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAADAFBMVEUBAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACnEzGyAAAAAXRSTlMAQObYZgAAADlJREFUeNpjZGBgYGSAgf9QDiOCD2UzwvkwSUYYH66aEcpHMpABlwAjQQHCZuCwFt1h6E5H9xyK9wE4vgoTXfUx9wAAAABJRU5ErkJggg==")));
            ButtonCollapse.Click += ButtonCollapse_Click;
            System.Windows.Forms.ToolTip ToolTip5 = new System.Windows.Forms.ToolTip();
            ToolTip5.SetToolTip(ButtonCollapse, "Свернуть все");

            ButtonOK = new System.Windows.Forms.Button();
            ButtonOK.Parent = Panel14;
            ButtonOK.Bounds = new System.Drawing.Rectangle(22, 10, 100, 28);
            ButtonOK.Text = "ОК";
            ButtonOK.Click += ButtonOK_Click;

            ButtonCancel = new System.Windows.Forms.Button();
            ButtonCancel.Parent = Panel14;
            ButtonCancel.Bounds = new System.Drawing.Rectangle(132, 10, 100, 28);
            ButtonCancel.Text = "Отмена";
            ButtonCancel.Click += ButtonCancel_Click;

            PropertyGrid1 = new System.Windows.Forms.PropertyGrid();
            PropertyGrid1.Parent = Panel12;
            PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            PropertyGrid1.SelectedObjectsChanged += PropertyGrid1_SelectedObjectsChanged;
            PropertyGrid1.PropertyValueChanged += PropertyGrid1_PropertyValueChanged;

            Label2 = new System.Windows.Forms.Label();
            Label2.Parent = Panel11;
            Label2.Bounds = new System.Drawing.Rectangle(1, 16, 68, 20);
            Label2.Text = "&Свойства:";

            TopLevel = true;
        }

        private void ButtonCollapse_Click(object sender, EventArgs e)
        {
            TreeView1.CollapseAll();
            if (TreeView1.Nodes.Count > 0)
            {
                TreeView1.SelectedNode = TreeView1.Nodes[0];
            }
        }

        private void ButtonExpand_Click(object sender, EventArgs e)
        {
            TreeView1.ExpandAll();
            if (TreeView1.Nodes.Count > 0)
            {
                TreeView1.SelectedNode = TreeView1.Nodes[0];
            }
        }

        public string GetDefaultValues(MyTreeNode comp)
        {
            // Заполним для компонента начальные свойства. Они нужны будут при создании скрипта.
            string DefaultValues1 = "";
            object pg = PropertyGrid1;
            ((System.Windows.Forms.PropertyGrid)pg).SelectedObject = comp;
            object view1 = pg.GetType().GetField("gridView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(pg);
            GridItemCollection GridItemCollection1 = (GridItemCollection)view1.GetType().InvokeMember("GetAllGridEntries", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, view1, null);
            foreach (GridItem GridItem in GridItemCollection1)
            {
                if (GridItem.PropertyDescriptor == null)// исключим из обхода категории
                {
                    continue;
                }
                if (GridItem.Label == "Locked")// исключим из обхода ненужные свойства
                {
                    continue;
                }
                if (GridItem.PropertyDescriptor.Category != GridItem.Label)
                {
                    string str7 = "";
                    string strTab = "            ";
                    str7 = str7 + osfDesigner.OneScriptFormsDesigner.ObjectConvertToString(GridItem.Value);
                    if (GridItem.GridItems.Count > 0)
                    {
                        strTab = strTab + "\t\t";
                        str7 = str7 + Environment.NewLine;
                        str7 = str7 + GetGridSubEntries(GridItem.GridItems, "", strTab);

                        DefaultValues1 = DefaultValues1 + "" + GridItem.Label + " == " + str7 + Environment.NewLine;

                        strTab = "\t\t";
                    }
                    else
                    {
                        DefaultValues1 = DefaultValues1 + "" + GridItem.Label + " == " + str7 + Environment.NewLine;
                    }
                }
            }
            return DefaultValues1;
        }

        private void ButtonMoveDown_Click(object sender, EventArgs e)
        {
            TreeNode TreeNode1 = TreeView1.SelectedNode;
            int Index1 = TreeNode1.Index;
            if (TreeNode1.Parent != null)
            {
                TreeNode Parent = TreeNode1.Parent;
                int Count1 = Parent.Nodes.Count;
                Parent.Nodes.Remove(TreeNode1);
                if (Index1 == Count1 - 1)
                {
                    if (Parent.Parent != null)
                    {
                        TreeNode Parent2 = Parent.Parent;
                        Parent2.Nodes.Insert(Parent.Index + 1, TreeNode1);
                    }
                    else
                    {
                        TreeView1.Nodes.Insert(Parent.Index + 1, TreeNode1);
                    }
                }
                else
                {
                    TreeNode Next = Parent.Nodes[Index1];
                    Next.Nodes.Insert(0, TreeNode1);
                }
            }
            else
            {
                TreeView1.Nodes.Remove(TreeNode1);
                TreeNode Next = TreeView1.Nodes[Index1];
                Next.Nodes.Insert(0, TreeNode1);
            }

            TreeView1.SelectedNode = TreeNode1;
            UpdateTreeViewOriginal();
        }

        private void ButtonMoveUp_Click(object sender, EventArgs e)
        {
            TreeNode TreeNode1 = TreeView1.SelectedNode;
            int Index1 = TreeNode1.Index;
            if (TreeNode1.Parent != null)
            {
                TreeNode Parent = TreeNode1.Parent;
                Parent.Nodes.Remove(TreeNode1);
                if (Index1 > 0)
                {
                    TreeNode Previos = Parent.Nodes[Index1 - 1];
                    Previos.Nodes.Add(TreeNode1);
                }
                else
                {
                    if (Parent.Parent != null)
                    {
                        TreeNode Parent2 = Parent.Parent;
                        Parent2.Nodes.Insert(Parent.Index, TreeNode1);
                    }
                    else
                    {
                        TreeView1.Nodes.Insert(Parent.Index, TreeNode1);
                    }
                }
            }
            else
            {
                TreeView1.Nodes.Remove(TreeNode1);
                TreeNode Previos = TreeView1.Nodes[Index1 - 1];
                Previos.Nodes.Add(TreeNode1);
            }

            TreeView1.SelectedNode = TreeNode1;
            UpdateTreeViewOriginal();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            TreeView1.Nodes.Remove(TreeView1.SelectedNode);
            UpdateTreeViewOriginal();

            if (TreeView1.Nodes.Count > 0)
            {
                ButtonDelete.Enabled = true;
                ButtonAddChild.Enabled = true;
            }
            else
            {
                ButtonDelete.Enabled = false;
                ButtonAddChild.Enabled = false;
            }
        }

        private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            MyTreeNode Node = (MyTreeNode)TreeView1.SelectedNode;
            TreeView1.SelectedNode = null;
            TreeView1.SelectedNode = Node;
            UpdateTreeViewOriginal();
        }

        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateTreeViewOriginal();
        }

        public string GetGridSubEntries(GridItemCollection gridItems, string str, string strTab)
        {
            foreach (var item in gridItems)
            {
                GridItem _item = (GridItem)item;
                str = str + strTab + _item.Label + " = " + _item.Value + Environment.NewLine;
                if (_item.GridItems.Count > 0)
                {
                    strTab = strTab + "\t\t";
                    str = GetGridSubEntries(_item.GridItems, str, strTab);
                    strTab = "\t\t";
                }
            }
            return str;
        }

        private void PropertyGrid1_SelectedObjectsChanged(object sender, EventArgs e)
        {
            MyTreeNode SelectedObject1 = (MyTreeNode)((System.Windows.Forms.PropertyGrid)sender).SelectedObject;

            if (SelectedObject1.Index == 0 && SelectedObject1.Parent == null)
            {
                if (TreeView1.Nodes.Count > 1)
                {
                    ButtonMoveDown.Enabled = true;
                    ButtonMoveUp.Enabled = false;
                }
                else
                {
                    ButtonMoveDown.Enabled = false;
                    ButtonMoveUp.Enabled = false;
                }
            }
            else if (SelectedObject1.Index == (TreeView1.Nodes.Count - 1) && SelectedObject1.Parent == null)
            {
                if (TreeView1.Nodes.Count > 1)
                {
                    ButtonMoveDown.Enabled = false;
                    ButtonMoveUp.Enabled = true;
                }
                else
                {
                    ButtonMoveDown.Enabled = false;
                    ButtonMoveUp.Enabled = false;
                }
            }
            else
            {
                ButtonMoveDown.Enabled = true;
                ButtonMoveUp.Enabled = true;
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            PropertyGrid1.SelectedObject = e.Node;
        }

        private void FrmNodes_Load(object sender, EventArgs e)
        {
            TreeViewOriginal = (TreeView)_context.Instance;
            TreeViewOld = new TreeView();
            TreeView1.CheckBoxes = TreeViewOriginal.CheckBoxes;
            TreeView1.Nodes.Clear();
            this.CopyTree(TreeViewOriginal, TreeView1);
            this.CopyTree(TreeViewOriginal, TreeViewOld);

            if (TreeView1.Nodes.Count > 0)
            {
                ButtonDelete.Enabled = true;
                TreeView1.SelectedNode = TreeView1.Nodes[0];
                ButtonAddChild.Enabled = true;
            }
            else
            {
                ButtonDelete.Enabled = false;
                ButtonAddChild.Enabled = false;
            }

            UpdateTreeViewOriginal();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            TreeViewOriginal.Nodes.Clear();
            this.CopyTree(this.TreeViewOld, TreeViewOriginal);
            this.Close();
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonAddRoot_Click(object sender, EventArgs e)
        {
            MyTreeNode TreeNode1 = new MyTreeNode();
            TreeNode1.TreeView = TreeView1;
            TreeNode1.Name = OneScriptFormsDesigner.RevertNodeName(TreeViewOriginal);
            TreeNode1.Text = TreeNode1.Name;
            TreeView1.Nodes.Add(TreeNode1);
            TreeNode1.DefaultValues = GetDefaultValues(TreeNode1);
            UpdateTreeViewOriginal();
            TreeView1.SelectedNode = TreeNode1;
            ButtonDelete.Enabled = true;
            ButtonAddChild.Enabled = true;
            TreeView1.Focus();
        }

        private void ButtonAddChild_Click(object sender, EventArgs e)
        {
            MyTreeNode TreeNode1 = new MyTreeNode();
            TreeNode1.TreeView = TreeView1;
            TreeNode1.Name = OneScriptFormsDesigner.RevertNodeName(TreeViewOriginal);
            TreeNode1.Text = TreeNode1.Name;
            TreeView1.SelectedNode.Nodes.Add(TreeNode1);
            TreeView1.SelectedNode.Expand();
            TreeNode1.DefaultValues = GetDefaultValues(TreeNode1);
            UpdateTreeViewOriginal();
            TreeView1.Focus();
        }

        public void UpdateTreeViewOriginal()
        {
            TreeViewOriginal.Nodes.Clear();
            this.CopyTree(TreeView1, TreeViewOriginal);
        }

        public void CopyNode(MyTreeNode node, System.Windows.Forms.TreeNodeCollection dest)
        {
            MyTreeNode copy = new MyTreeNode();
            copy.TreeView = TreeView1;
            copy.Name = node.Name;
            copy.Text = node.Text;
            copy.SelectedImageIndex = node.SelectedImageIndex;
            copy.ImageIndex = node.ImageIndex;
            copy.Checked = node.Checked;
            copy.DefaultValues = node.DefaultValues;
            try
            {
                copy.NodeFont = new System.Drawing.Font(node.NodeFont, node.NodeFont.Style);
            }
            catch { }
            dest.Add(copy);
            foreach (MyTreeNode child in node.Nodes)
            {
                CopyNode(child, copy.Nodes);
            }
        }

        public void CopyTree(TreeView src, TreeView dest)
        {
            dest.ImageList = src.ImageList;
            dest.ImageIndex = src.ImageIndex;
            dest.SelectedImageIndex = src.SelectedImageIndex;

            foreach (MyTreeNode node in src.Nodes)
            {
                CopyNode(node, dest.Nodes);
            }
        }

        private void frmNodes_Closed(object sender, EventArgs e)
        {
            _wfes.CloseDropDown();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}
