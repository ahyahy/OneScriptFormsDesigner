using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;

namespace osfDesigner
{
    public class PropertyGridHost : System.Windows.Forms.UserControl
    {
        private System.ComponentModel.IContainer components = null;
        protected System.Windows.Forms.PropertyGrid pgrdPropertyGrid;
        protected System.Windows.Forms.ComboBox pgrdComboBox;

        protected System.Windows.Forms.Splitter pgrdsplitter;
        protected System.Windows.Forms.TreeView pgrdTreeView;
        protected System.Windows.Forms.ToolBar pgrdToolBar;
        protected System.Windows.Forms.ToolBarButton buttonSort;

        private const string _Name_ = "PropertyGridHost";

        // Используется для подавления событий. Установите в значение True перед изменением свойства, которое вызовет событие
        private bool _bSuppressEvents = false;
        private bool _bSuppressEvents2 = false;

        public PropertyGridHost(DesignSurfaceManagerExt surfaceManager)
        {
            const string _signature_ = _Name_ + @"::ctor()";

            this.pgrdPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.pgrdComboBox = new System.Windows.Forms.ComboBox();
            this.pgrdsplitter = new System.Windows.Forms.Splitter();
            this.pgrdTreeView = new System.Windows.Forms.TreeView();
            this.pgrdToolBar = new System.Windows.Forms.ToolBar();
            this.buttonSort = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // pgrdPropertyGrid
            // 
            this.pgrdPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgrdPropertyGrid.Location = new System.Drawing.Point(0, 229);
            this.pgrdPropertyGrid.Name = "pgrdPropertyGrid";
            this.pgrdPropertyGrid.Size = new System.Drawing.Size(150, 0);
            this.pgrdPropertyGrid.TabIndex = 3;
            // 
            // pgrdComboBox
            // 
            this.pgrdComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.pgrdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pgrdComboBox.FormattingEnabled = true;
            this.pgrdComboBox.Location = new System.Drawing.Point(0, 205);
            this.pgrdComboBox.Name = "pgrdComboBox";
            this.pgrdComboBox.Size = new System.Drawing.Size(150, 24);
            this.pgrdComboBox.Sorted = true;
            this.pgrdComboBox.TabIndex = 2;
            // 
            // pgrdsplitter
            // 
            this.pgrdsplitter.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pgrdsplitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pgrdsplitter.Location = new System.Drawing.Point(0, 200);
            this.pgrdsplitter.Name = "pgrdsplitter";
            this.pgrdsplitter.Size = new System.Drawing.Size(150, 5);
            this.pgrdsplitter.TabIndex = 4;
            this.pgrdsplitter.TabStop = false;
            // 
            // pgrdTreeView
            // 
            this.pgrdTreeView.Dock = System.Windows.Forms.DockStyle.Top;
            this.pgrdTreeView.HideSelection = false;
            this.pgrdTreeView.Location = new System.Drawing.Point(0, 0);
            this.pgrdTreeView.Name = "pgrdTreeView";
            this.pgrdTreeView.Size = new System.Drawing.Size(150, 200);
            this.pgrdTreeView.NodeMouseClick += PgrdTreeView_NodeMouseClick;
            this.pgrdTreeView.TabIndex = 5;

            // 
            // buttonSort
            // 
            string str_sort = "AAABAAEAICAQAAEABADoAgAAFgAAACgAAAAgAAAAQAAAAAEABAAAAAAAgAIAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAACAAACAAAAAgIAAgAAAAIAAgACAgAAAgICAAMDAwAAAAP8AAP8AAAD//wD/AAAA/wD/AP//AAD///8AAAAAAAAAAAAAAAAAAAAAAAEREQAAERERAAAAAAAAAAAAEREQAAEREAAAAAAAAAAAAAEREQABERAAAAAAAAAAAAAAEREQAREQAAAAAAcAAAAAAIEREQEREAAAAAB3cAAAAAAIERERERAAAAAABwAAAAAAAREREREQAAAAAAcAAAAAABEREREREAAAAAAHAAAAAAEREQARERAAAAAABwAAAAABERAAAREQAAAAAAcAAAAAAREQAAEREAAAAAAHAAAAAAEREQARERAAAAAABwAAAAAAEREREREQAAAAAAcAAAAAAAEREREREQAAAAAHAAAAAAAAAAAAAAAAAAAABwAAAAAAAAAAAAAAAAAAAAcAAAAMzMAAAAzMzAAAAAAHAAAADMzAAAAMzMwAAAAABwAAAADMzAAADMzAAAAAAAcAAAAAzMwAAMzMwAAAAAAHAAAAAAzMzMzMzMAAAAAABwAAAAAMzMzMzMwAAAAAAAcAAAAADMzAAMzMAAAAAAAHAAAAAADMwADMwAAAAAAABwAAAAAAzMwMzMAAAAAAAAcAAAAAAAzMDMzAAAAAAAAHAAAAAAAMzMzMAAAAAAAABwAAAAAADMzMzAAAAAAAAAcAAAAAAADMzMAAAAAAAAAAAAAAAAAADMzAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD/////g8D/v8Hh/x/g4f4P8GH8B/Ah+AP4AfAB+AHwAfAB/g/gwf4P4eH+D+Hh/g/gwf4P8AH+D/gA/g////4P///+D4fg/g+H4P4Pw+H+D8PB/g/gAf4P4AP+D+HD/g/xx/4P8If+D/iH/g/4D/4P+A/+D/wf/g/+H/4P/////w==";
            System.Drawing.Image Image1 = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_sort);
            System.Windows.Forms.ImageList ImageList1 = new System.Windows.Forms.ImageList();
            ImageList1.Images.Add(Image1);
            this.buttonSort.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.buttonSort.ImageIndex = 0;
            this.buttonSort.Name = "buttonSort";
            this.buttonSort.Pushed = true;

            // 
            // pgrdToolBar
            // 
            this.pgrdToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pgrdToolBar.Location = new System.Drawing.Point(0, 0);
            this.pgrdToolBar.Name = "pgrdToolBar";
            this.pgrdToolBar.Size = new System.Drawing.Size(150, 24);
            this.pgrdToolBar.BackColor = pgrdPropertyGrid.BackColor;
            this.pgrdToolBar.Buttons.Add(buttonSort);
            this.pgrdToolBar.ImageList = ImageList1;
            this.pgrdToolBar.ButtonClick += PgrdToolBar_ButtonClick;

            // 
            // PropertyGridHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pgrdPropertyGrid);
            this.Controls.Add(this.pgrdComboBox);
            this.Controls.Add(this.pgrdsplitter);
            this.Controls.Add(this.pgrdTreeView);
            this.Controls.Add(this.pgrdToolBar);
            this.Name = "PropertyGridHost";
            this.ResumeLayout(false);

            this.Dock = System.Windows.Forms.DockStyle.Fill;

            // Диспетчер поверхности строго связан с PropertyGridHost
            if (null == surfaceManager)
            {
                throw new ArgumentNullException("surfaceManager", _signature_ + " - Exception: invalid argument (null)!");
            }

            SurfaceManager = surfaceManager;
            pgrdPropertyGrid.ToolbarVisible = true;
            pgrdPropertyGrid.HelpVisible = true;

            // ComboBox - СЛЕДИТ за событием PropertyGridHost: SelectedObjectsChanged
            // Каждый раз, когда кто-либо выбирает новый объект внутри PropertyGridHost
            // событие PropertyGridHost.SelectedObjectsChanged вызывает метод ReloadComboBox()
            pgrdPropertyGrid.SelectedObjectsChanged += (object sender, System.EventArgs e) =>
            {
                //!!!здесь делаем подмену исходного компонента, указанного в свойстве pgrdPropertyGrid.SelectedObject на 
                //наш компонент - двойник (similar). Связь между ними через OneScriptFormsDesigner.hashtable.
                System.Windows.Forms.PropertyGrid propertyGrid = (System.Windows.Forms.PropertyGrid)sender;
                dynamic OriginalObj = propertyGrid.SelectedObject;
                dynamic SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(OriginalObj);

                //!!!делаем невидимыми свойства, которые хотим скрыть
                // компонент System.Windows.Forms.ImageList и System.Windows.Forms.MainMenu используем от Майкрософт для помещения на форму, а 
                // в сетке свойств показываем свойства нашего osfDesigner.ImageList и osfDesigner.MainMenu.
                if (SimilarObj == null)
                {
                    if (OriginalObj.GetType().ToString() == "System.Windows.Forms.ImageList")
                    {
                        SimilarObj = new osfDesigner.ImageList();
                        ((osfDesigner.ImageList)SimilarObj).OriginalObj = OriginalObj;
                        OneScriptFormsDesigner.AddToHashtable(OriginalObj, SimilarObj);
                        OneScriptFormsDesigner.PassProperties(OriginalObj, SimilarObj);//передадим свойства
                        propertyGrid.SelectedObject = SimilarObj;
                    }
                    else if (OriginalObj.GetType().ToString() == "System.Windows.Forms.MainMenu")
                    {
                        SimilarObj = new osfDesigner.MainMenu();
                        OneScriptFormsDesigner.AddToHashtable(OriginalObj, SimilarObj);
                        OneScriptFormsDesigner.PassProperties(OriginalObj, SimilarObj);//передадим свойства
                        propertyGrid.SelectedObject = SimilarObj;
                    }
                }
                else
                {
                    propertyGrid.SelectedObject = SimilarObj;
                }

                if (OriginalObj.GetType().ToString() == "System.Windows.Forms.TabPage")
                {
                    SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(OriginalObj);
                    if (OneScriptFormsDesigner.tic1 > 1)
                    {
                        propertyGrid.SelectedObject = SimilarObj;
                    }
                    else
                    {
                        OneScriptFormsDesigner.tic1 = OneScriptFormsDesigner.tic1 + 1;
                    }
                }
                ReloadComboBox();
                
                ChangeSelectNode(OriginalObj);
            };

            pgrdPropertyGrid.SelectedGridItemChanged += (object s, SelectedGridItemChangedEventArgs e) =>
            {
                object comp = pgrdPropertyGrid;
                System.Type compType = comp.GetType();
                object view = compType.GetField("gridView", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(comp);
                GridItemCollection GridItemCollection1 = (GridItemCollection)view.GetType().InvokeMember("GetAllGridEntries", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, view, null);
                foreach (GridItem GridItem in GridItemCollection1)
                {
                    if (GridItem.Label == "СписокИзображений" || 
                    GridItem.Label == "СписокБольшихИзображений" || 
                    GridItem.Label == "СписокМаленькихИзображений" || 
                    GridItem.Label == "DoubleBuffered")
                    {
                        GridItem.Expanded = false;
                    }
                }

                // выделим компонент на форме используя pgrdPropertyGrid.SelectedObject
                // это может понадобиться, если компонент, например кнопка, меняет родителя
                // иначе не синхронно обновляется сетка свойств, поле выбора над ним, и выделенный на форме объект
                if (!OneScriptFormsDesigner.block1)
                {
                    IDesignerHost host = pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost();
                    ISelectionService iSel = host.GetService(typeof(ISelectionService)) as ISelectionService;
                    if (iSel != null)
                    {
                        if (iSel.GetSelectedComponents().Count == 1)
                        {
                            ComponentCollection ctrlsExisting = host.Container.Components;
                            for (int i = 0; i < ctrlsExisting.Count; i++)
                            {
                                if (ctrlsExisting[i].Site.Name == ((System.ComponentModel.Component)pgrdPropertyGrid.SelectedObject).Site.Name)
                                {
                                    IComponent[] arr = { ctrlsExisting[i] };
                                    iSel.SetSelectedComponents(arr);
                                    break;
                                }
                            }
                        }
                    }
                }

                if (pgrdPropertyGrid.SelectedObject is System.Windows.Forms.Form )
                {

                }
                
                ////////////////////////////////System.Windows.Forms.MessageBox.Show("(Form)control).Menu=" + (Form)GridItem).Menu);

            };

            pgrdPropertyGrid.PropertyValueChanged += (object s, PropertyValueChangedEventArgs e) =>
            {
                System.Windows.Forms.PropertyGrid PropertyGrid1 = (System.Windows.Forms.PropertyGrid)s;
                dynamic SelectedObject1 = PropertyGrid1.SelectedObject;
                string Label1 = PropertyGrid1.SelectedGridItem.Label;
                if (Label1 == "СписокИзображений")
                {
                    if (e.ChangedItem.Value != e.OldValue)
                    {
                        try
                        {
                            SelectedObject1.ImageIndex = -1;
                        }
                        catch { }
                    }
                }
                if (Label1 == "Значок")
                {
                }
                if (Label1 == "Стыковка")
                {
                    PropertyGrid.TopLevelControl.Refresh();
                }

                object comp = pgrdPropertyGrid;
                System.Type compType = comp.GetType();
                object view = compType.GetField("gridView", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(comp);
                GridItemCollection GridItemCollection1 = (GridItemCollection)view.GetType().InvokeMember("GetAllGridEntries", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, view, null);
                foreach (GridItem GridItem in GridItemCollection1)
                {
                    if (GridItem.Label == "СписокИзображений" || 
                    GridItem.Label == "СписокБольшихИзображений" || 
                    GridItem.Label == "СписокМаленькихИзображений" || 
                    GridItem.Label == "DoubleBuffered")
                    {
                        GridItem.Expanded = false;
                    }
                }
	
                if (Label1 == "(Name)" && SelectedObject1.GetType() != typeof(Form))
                {
                    System.Windows.Forms.MessageBox.Show(
                        "Для правильного формирования файла сценария не допускается изменять имя компонента.",
                        "",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1
                        );
                    if (SelectedObject1.GetType().ToString() == "osfDesigner.TabPage")
                    {
                        PropertyDescriptor pd = TypeDescriptor.GetProperties(SelectedObject1.M_TabPage)["Name"];
                        pd.SetValue(SelectedObject1.M_TabPage, (string)e.OldValue);
                    }
                    else
                    {
                        PropertyDescriptor pd = TypeDescriptor.GetProperties(SelectedObject1)["Name"];
                        pd.SetValue(SelectedObject1, (string)e.OldValue);
                    }
                }
                if (Label1.Contains("ToolTip"))
                {
                    if ((string)PropertyGrid1.SelectedGridItem.Value != (string)e.OldValue)
                    {
                        string nameToolTip = Label1.Substring(Label1.LastIndexOf(' ') + 1);
                        SelectedObject1.ToolTip[nameToolTip] = (string)PropertyGrid1.SelectedGridItem.Value;
                    }
                }
            };

            // PropertyGridHost - СЛЕДИТ за событием ComboBox: SelectedIndexChanged
            // каждый раз, когда кто-либо выбирает новый объект внутри ComboBox
            // событие ComboBox.SelectedIndexChanged вызывает метод OrientPropertyGridTowardsObject()
            pgrdComboBox.SelectedIndexChanged += (object sender, System.EventArgs e) =>
            {
                if (_bSuppressEvents)
                {
                    return;
                }
                OrientPropertyGridTowardsObject();
            };
        }

        private void PgrdToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {

            if (e.Button.Name == "buttonSort")
            {
                if (buttonSort.Pushed)
                {
                    buttonSort.ToolTipText = "В порядке создания";
                }
                else
                {
                    buttonSort.ToolTipText = "В алфавитном порядке";
                }

                Component comp = OneScriptFormsDesigner.HighlightedComponent();
                ReloadTreeView();
                if (comp != null)
                {
                    ChangeSelectNode(comp);
                }
            }
            //System.Windows.Forms.MessageBox.Show("Name=" + e.Button.Name + "  Pushed=" + e.Button.Pushed);
        }

        public void ChangeSelectNode(Component comp, System.Windows.Forms.TreeNodeCollection treeNodes = null)
        {
            if (_bSuppressEvents2)
            {
                return;
            }

            System.Windows.Forms.TreeNodeCollection _treeNodes;
            if (treeNodes == null)
            {
                _treeNodes = TreeView.Nodes;
            }
            else
            {
                _treeNodes = treeNodes;
            }

            ISelectionService iSel = (ISelectionService)(pDesigner.DSME.ActiveDesignSurface.GetService(typeof(ISelectionService)));
            System.Collections.ICollection collection1 = iSel.GetSelectedComponents();
            System.ComponentModel.Component[] arr = new System.ComponentModel.Component[collection1.Count];
            collection1.CopyTo(arr, 0);
            System.ComponentModel.Component comp1 = null;
            try
            {
                comp1 = arr[0];
            }
            catch { }
            if (comp1 != null)
            {
                try
                {
                    string _nodeKey = comp1.Site.Name;
                    System.Windows.Forms.TreeNode treeNode;
                    for (int i = 0; i < _treeNodes.Count; i++)
                    {
                        treeNode = _treeNodes[i];
                        if (treeNode.Name == _nodeKey)
                        {
                            TreeView.SelectedNode = treeNode;
                            return;
                        }
                        if (treeNode.Nodes.Count > 0)
                        {
                            ChangeSelectNode(comp, treeNode.Nodes);
                        }
                    }
                }
                catch { }
            }
            else { }
        }

        private void PgrdTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            _bSuppressEvents2 = true;
            IDesignerHost host = pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost();
            ISelectionService iSel = host.GetService(typeof(ISelectionService)) as ISelectionService;
            if (iSel != null)
            {
                ComponentCollection ctrlsExisting = host.Container.Components;
                for (int i = 0; i < ctrlsExisting.Count; i++)
                {
                    if (ctrlsExisting[i].Site.Name == e.Node.Text)
                    {
                        IComponent[] arr = { ctrlsExisting[i] };
                        iSel.SetSelectedComponents(arr);
                        break;
                    }
                }
            }
            _bSuppressEvents2 = false;
        }

        // Метод очистки всех используемых ресурсов.
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private DesignSurfaceManagerExt SurfaceManager { get; set; }
        
        public System.Windows.Forms.ComboBox ComboBox
        {
            get { return pgrdComboBox; }
        }

        public System.Windows.Forms.TreeView TreeView
        {
            get { return pgrdTreeView; }
        }

        public System.Windows.Forms.ToolBarButton ButtonSort
        {
            get { return buttonSort; }
        }

        public System.Windows.Forms.PropertyGrid PropertyGrid
        {
            get { return pgrdPropertyGrid; }
        }

        public object SelectedObject
        {
            get { return pgrdPropertyGrid.SelectedObject; }
            set { pgrdPropertyGrid.SelectedObject = value; }
        }

        // полагайтесь на свойство SurfaceManager.ActiveDesignSurface которое эффективно указывает на АКТИВНУЮ область дизайнера (DesignSurface)
        private void OrientPropertyGridTowardsObject() // Ориентация Сетки Свойств По Отношению К Объекту
        {
            // IDesignerEventService предоставляет общий механизм событий для событий конструктора. С этим механизмом,
            // приложение информируется, когда конструктор становится активным. Сервис предоставляет коллекцию
            // дизайнеров и единое место, где глобальные объекты, такие как окно свойств, могут контролировать 
            // события изменения выбора.
            IDesignerEventService des = (IDesignerEventService)SurfaceManager.GetService(typeof(IDesignerEventService));
            if (null != des)
            {
                IDesignerHost host = des.ActiveDesigner;

                // получим ISelectionService из активной поверхности дизайнера
                ISelectionService iSel = host.GetService(typeof(ISelectionService)) as ISelectionService;
                if (iSel != null)
                {
                    // получим имя элемента управления, выбранного в comboBox
                    string sName = pgrdComboBox.SelectedItem.ToString();
                    if (!string.IsNullOrEmpty(sName))
                    {
                        // Сохраним коллекцию выбранных объектов. Циклично пройдем через элементы управления внутри текущей поверхности дизайнера.
                        // Если мы найдем выбранный в comboBox элемент, то используем ISelectionService, чтобы выбрать его 
                        // (и он будет выбран в PropertyGridHost)
                        ComponentCollection ctrlsExisting = host.Container.Components;
                        Debug.Assert(0 != ctrlsExisting.Count);
                        foreach (Component comp in ctrlsExisting)
                        {
                            if (sName == comp.Site.Name)
                            {
                                Component[] arr = { comp };
                                // ISelectionService in action...
                                iSel.SetSelectedComponents(arr);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void ReloadTreeView()
        {
            Form Form1 = null;
            try
            {
                Form1 = (Form)pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost().Container.Components[0];
            }
            catch { }
            if (Form1 == null)
            {
                TreeView.Nodes.Clear();
                return;
            }

            object selectedObj = pgrdPropertyGrid.SelectedObject;
            if (null == selectedObj)
            {
                return; // Не перезагружать
            }

            // IDesignerEventService предоставляет глобальный событийный механизм для событий конструктора. С этим механизмом,
            // приложение информируется, когда конструктор становится активным. Сервис предоставляет коллекцию дизайнеров и единое
            // место, где глобальные объекты, такие как окно "Свойства", могут контролировать события изменения выбора.
            IDesignerEventService des = (IDesignerEventService)SurfaceManager.GetService(typeof(IDesignerEventService));
            if (null == des)
            {
                return;
            }

            TreeView.Nodes.Clear();
            ////////////IDesignerHost host = des.ActiveDesigner;
            ////////////ComponentCollection ctrlsExisting = host.Container.Components;

            System.ComponentModel.ComponentCollection ctrls = des.ActiveDesigner.Container.Components;
            System.Collections.ArrayList ctrlsArray = new System.Collections.ArrayList();
            System.Collections.ArrayList ctrlsExisting = new System.Collections.ArrayList();
            System.Collections.ArrayList ctrlsExisting1 = new System.Collections.ArrayList();
            for (int i = 0; i < ctrls.Count; i++)
            {
                ctrlsArray.Add(ctrls[i]);
            }

            for (int i = 0; i < Form1.ArrayListComponentsAddingOrder.Count; i++)
            {
                Component comp4 = (Component)Form1.ArrayListComponentsAddingOrder[i];
                if (ctrlsArray.Contains(comp4))
                {
                    ctrlsExisting.Add(comp4);
                    ctrlsExisting1.Add(comp4);
                }
            }

            if (!buttonSort.Pushed)
            {
                System.Collections.Generic.Dictionary<string, System.ComponentModel.IComponent> comps = new Dictionary<string, System.ComponentModel.IComponent>();
                for (int i = 0; i < ctrlsExisting.Count; i++)
                {
                    string sName = string.Empty;
                    if (ctrlsExisting[i] is Form)
                    {
                        if (((Form)ctrlsExisting[i]).Text == "")
                        {
                            sName = pDesigner.DSME.GetValidFormName();
                        }
                        else
                        {
                            sName = ((Form)ctrlsExisting[i]).Site.Name;
                        }
                    }
                    else if (ctrlsExisting[i] is Control)
                    {
                        sName = ((Control)ctrlsExisting[i]).Site.Name;
                    }
                    if (string.IsNullOrEmpty(sName))
                    {
                        sName = ((Component)ctrlsExisting[i]).Site.Name;
                    }
                    comps.Add(sName, (IComponent)ctrlsExisting[i]);
                }
                System.Collections.Generic.Dictionary<string, System.Windows.Forms.TreeNode> comps2 = new Dictionary<string, System.Windows.Forms.TreeNode>();
                foreach (KeyValuePair<string, System.ComponentModel.IComponent> keyValue in comps)
                {
                    string parentName = "";
                    System.Windows.Forms.Control parent1 = null;
                    try
                    {
                        parent1 = ((dynamic)keyValue.Value).Parent;
                    }
                    catch { }

                    if (parent1 != null)
                    {
                        //////ComponentCollection ctrlsExisting1 = host.Container.Components;
                        for (int i1 = 0; i1 < ctrlsExisting1.Count; i1++)
                        {
                            if (parent1 == ctrlsExisting1[i1])
                            {
                                parentName = ((System.ComponentModel.IComponent)ctrlsExisting1[i1]).Site.Name;
                                break;
                            }
                        }
                    }

                    System.Windows.Forms.TreeNode TreeNode1 = new System.Windows.Forms.TreeNode((string)keyValue.Key);
                    TreeNode1.Tag = parentName;
                    TreeNode1.Name = (string)keyValue.Key;
                    comps2.Add(keyValue.Key, TreeNode1);
                }
                foreach (KeyValuePair<string, System.ComponentModel.IComponent> keyValue in comps)
                {
                    System.Windows.Forms.TreeNode TreeNode1 = (System.Windows.Forms.TreeNode)comps2[(string)keyValue.Key];
                    if ((string)TreeNode1.Tag != "")
                    {
                        System.Windows.Forms.TreeNode NodeParent = (System.Windows.Forms.TreeNode)comps2[(string)TreeNode1.Tag];
                        NodeParent.Nodes.Add(TreeNode1);
                    }
                    else
                    {
                        TreeView.Nodes.Add(TreeNode1);
                    }

                }
            }
            else
            {
                System.Collections.SortedList SortedList1 = new System.Collections.SortedList();
                for (int i = 0; i < ctrlsExisting.Count; i++)
                {
                    string sName = string.Empty;
                    if (ctrlsExisting[i] is Form)
                    {
                        if (((Form)ctrlsExisting[i]).Text == "")
                        {
                            sName = pDesigner.DSME.GetValidFormName();
                        }
                        else
                        {
                            sName = ((Form)ctrlsExisting[i]).Site.Name;
                        }
                    }
                    else if (ctrlsExisting[i] is Control)
                    {
                        sName = ((Control)ctrlsExisting[i]).Site.Name;
                    }
                    if (string.IsNullOrEmpty(sName))
                    {
                        sName = ((Component)ctrlsExisting[i]).Site.Name;
                    }
                    SortedList1.Add(sName, ctrlsExisting[i]);
                }

                System.Collections.SortedList SortedList2 = new System.Collections.SortedList();
                foreach (System.Collections.DictionaryEntry de in SortedList1)
                {
                    string parentName = "";
                    System.Windows.Forms.Control parent1 = null;
                    try
                    {
                        parent1 = ((dynamic)de.Value).Parent;
                    }
                    catch { }

                    if (parent1 != null)
                    {
                        ////////////////////////ComponentCollection ctrlsExisting1 = host.Container.Components;
                        for (int i1 = 0; i1 < ctrlsExisting1.Count; i1++)
                        {
                            if (parent1 == ctrlsExisting1[i1])
                            {
                                parentName = ((System.ComponentModel.IComponent)ctrlsExisting1[i1]).Site.Name;
                                break;
                            }
                        }
                    }

                    System.Windows.Forms.TreeNode TreeNode1 = new System.Windows.Forms.TreeNode((string)de.Key);
                    TreeNode1.Tag = parentName;
                    TreeNode1.Name = (string)de.Key;
                    SortedList2.Add(de.Key, TreeNode1);
                }
                foreach (System.Collections.DictionaryEntry de in SortedList1)
                {
                    System.Windows.Forms.TreeNode TreeNode1 = (System.Windows.Forms.TreeNode)SortedList2[(string)de.Key];
                    if ((string)TreeNode1.Tag != "")
                    {
                        System.Windows.Forms.TreeNode NodeParent = (System.Windows.Forms.TreeNode)SortedList2[TreeNode1.Tag];
                        NodeParent.Nodes.Add(TreeNode1);
                    }
                    else
                    {
                        TreeView.Nodes.Add(TreeNode1);
                    }
                }
            }
            TreeView.ExpandAll();
        }

        public void ReloadComboBox()
        {
            _bSuppressEvents = true;

            // IDesignerEventService предоставляет глобальный событийный механизм для событий конструктора. С этим механизмом,
            // приложение информируется, когда конструктор становится активным. Сервис предоставляет коллекцию дизайнеров и единое
            // место, где глобальные объекты, такие как окно "Свойства", могут контролировать события изменения выбора.
            IDesignerEventService des = (IDesignerEventService)SurfaceManager.GetService(typeof(IDesignerEventService));
            if (null == des)
            {
                return;
            }
            IDesignerHost host = des.ActiveDesigner;

            object selectedObj = pgrdPropertyGrid.SelectedObject;
            if (null == selectedObj)
            {
                return; // Не перезагружать
            }

            // Получим имя элемента управления, выбранного в comboBox
            string sName = string.Empty;
            if (selectedObj is Form)
            {
                sName = ((Form)selectedObj).Name;
            }
            else if (selectedObj is Control)
            {
                sName = ((Control)selectedObj).Site.Name;
            }
            if (string.IsNullOrEmpty(sName))
            {
                sName = ((Component)selectedObj).Site.Name;//чтобы не визуальные компоненты меняли индекс в ComboBox
            }

            // подготовка данных для перезагрузки combobox (начало)
            List<object> ctrlsToAdd = new List<object>();
            string pgrdComboBox_Text = string.Empty;
            try
            {
                ComponentCollection ctrlsExisting = host.Container.Components;
                Debug.Assert(0 != ctrlsExisting.Count);

                foreach (Component comp in ctrlsExisting)
                {
                    string sItemText = comp.Site.Name;
                    ctrlsToAdd.Add(sItemText);
                    if (sName == comp.Site.Name)
                    {
                        pgrdComboBox_Text = sItemText;
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
            // обновим combobox (фиксация)
            pgrdComboBox.Items.Clear();
            pgrdComboBox.Items.AddRange(ctrlsToAdd.ToArray());
            pgrdComboBox.Text = pgrdComboBox_Text;

            _bSuppressEvents = false;
        }

        // Свернем одиночный элемент сетки (GridItem) тот, который имеет значение параметра "sGridItemLabel"
        public GridItem CollapseGridItem(string sGridItemLabel)
        {
            return CollapseExpandGridItem(sGridItemLabel, false);
        }

        // развернем одиночный элемент сетки (GridItem) тот, который имеет значение параметра "sGridItemLabel"
        public GridItem ExpandGridItem(string sGridItemLabel)
        {
            return CollapseExpandGridItem(sGridItemLabel, true);
        }

        private GridItem CollapseExpandGridItem(string sGridItemLabel, bool bExpanded)
        {
            // Получим корневой элемент GridItem
            GridItem root = this.PropertyGrid.SelectedGridItem;
            if (null == root)
            {
                return null;
            }

            while (null != root.Parent)
                root = root.Parent;

            if (null == root)
            {
                return null;
            }

            // и начнем поиск из корня
            foreach (GridItem g in root.GridItems)
            {
                if (g.Label == sGridItemLabel)
                {
                    if (g.Expandable)
                    {
                        g.Expanded = bExpanded;
                    }
                    return g;
                }
            }
            return null;
        }
    }
}
