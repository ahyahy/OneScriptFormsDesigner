﻿using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class PropertyGridHost : System.Windows.Forms.UserControl
    {
        private IContainer components = null;
        protected System.Windows.Forms.PropertyGrid pgrdPropertyGrid;
        protected System.Windows.Forms.ComboBox pgrdComboBox;
        protected System.Windows.Forms.Splitter pgrdsplitter;
        protected System.Windows.Forms.TreeView pgrdTreeView;
        protected System.Windows.Forms.ToolBar pgrdToolBar;
        protected System.Windows.Forms.ToolBarButton buttonSort;

        // Используется для подавления событий. Установите в значение True перед изменением свойства, которое вызовет событие.
        private bool _bSuppressEvents = false;
        private bool _bSuppressEvents2 = false;

        public PropertyGridHost(DesignSurfaceManagerExt surfaceManager)
        {
            pgrdPropertyGrid = new System.Windows.Forms.PropertyGrid();
            pgrdComboBox = new System.Windows.Forms.ComboBox();
            pgrdsplitter = new System.Windows.Forms.Splitter();
            pgrdTreeView = new System.Windows.Forms.TreeView();
            pgrdToolBar = new System.Windows.Forms.ToolBar();
            buttonSort = new System.Windows.Forms.ToolBarButton();
            SuspendLayout();
            // 
            // pgrdPropertyGrid
            // 
            pgrdPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            pgrdPropertyGrid.Location = new Point(0, 229);
            pgrdPropertyGrid.Name = "pgrdPropertyGrid";
            pgrdPropertyGrid.Size = new Size(150, 0);
            pgrdPropertyGrid.TabIndex = 3;
            // 
            // pgrdComboBox
            // 
            pgrdComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            pgrdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            pgrdComboBox.FormattingEnabled = true;
            pgrdComboBox.Location = new Point(0, 205);
            pgrdComboBox.Name = "pgrdComboBox";
            pgrdComboBox.Size = new Size(150, 24);
            pgrdComboBox.Sorted = true;
            pgrdComboBox.TabIndex = 2;
            // 
            // pgrdsplitter
            // 
            pgrdsplitter.BackColor = Color.LightSteelBlue;
            pgrdsplitter.Dock = System.Windows.Forms.DockStyle.Top;
            pgrdsplitter.Location = new Point(0, 200);
            pgrdsplitter.Name = "pgrdsplitter";
            pgrdsplitter.Size = new Size(150, 5);
            pgrdsplitter.TabIndex = 4;
            pgrdsplitter.TabStop = false;
            // 
            // pgrdTreeView
            // 
            pgrdTreeView.Dock = System.Windows.Forms.DockStyle.Top;
            pgrdTreeView.HideSelection = false;
            pgrdTreeView.Location = new Point(0, 0);
            pgrdTreeView.Name = "pgrdTreeView";
            pgrdTreeView.Size = new Size(150, 200);
            pgrdTreeView.NodeMouseClick += PgrdTreeView_NodeMouseClick;
            pgrdTreeView.TabIndex = 5;
            // 
            // buttonSort
            // 
            string str_sort = "AAABAAEAICAQAAEABADoAgAAFgAAACgAAAAgAAAAQAAAAAEABAAAAAAAgAIAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAACAAACAAAAAgIAAgAAAAIAAgACAgAAAgICAAMDAwAAAAP8AAP8AAAD//wD/AAAA/wD/AP//AAD///8AAAAAAAAAAAAAAAAAAAAAAAEREQAAERERAAAAAAAAAAAAEREQAAEREAAAAAAAAAAAAAEREQABERAAAAAAAAAAAAAAEREQAREQAAAAAAcAAAAAAIEREQEREAAAAAB3cAAAAAAIERERERAAAAAABwAAAAAAAREREREQAAAAAAcAAAAAABEREREREAAAAAAHAAAAAAEREQARERAAAAAABwAAAAABERAAAREQAAAAAAcAAAAAAREQAAEREAAAAAAHAAAAAAEREQARERAAAAAABwAAAAAAEREREREQAAAAAAcAAAAAAAEREREREQAAAAAHAAAAAAAAAAAAAAAAAAAABwAAAAAAAAAAAAAAAAAAAAcAAAAMzMAAAAzMzAAAAAAHAAAADMzAAAAMzMwAAAAABwAAAADMzAAADMzAAAAAAAcAAAAAzMwAAMzMwAAAAAAHAAAAAAzMzMzMzMAAAAAABwAAAAAMzMzMzMwAAAAAAAcAAAAADMzAAMzMAAAAAAAHAAAAAADMwADMwAAAAAAABwAAAAAAzMwMzMAAAAAAAAcAAAAAAAzMDMzAAAAAAAAHAAAAAAAMzMzMAAAAAAAABwAAAAAADMzMzAAAAAAAAAcAAAAAAADMzMAAAAAAAAAAAAAAAAAADMzAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD/////g8D/v8Hh/x/g4f4P8GH8B/Ah+AP4AfAB+AHwAfAB/g/gwf4P4eH+D+Hh/g/gwf4P8AH+D/gA/g////4P///+D4fg/g+H4P4Pw+H+D8PB/g/gAf4P4AP+D+HD/g/xx/4P8If+D/iH/g/4D/4P+A/+D/wf/g/+H/4P/////w==";
            Image Image1 = OneScriptFormsDesigner.Base64ToImage(str_sort);
            System.Windows.Forms.ImageList ImageList1 = new System.Windows.Forms.ImageList();
            ImageList1.Images.Add(Image1);
            buttonSort.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            buttonSort.ImageIndex = 0;
            buttonSort.Name = "buttonSort";
            buttonSort.Pushed = true;
            // 
            // pgrdToolBar
            // 
            pgrdToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            pgrdToolBar.Location = new Point(0, 0);
            pgrdToolBar.Name = "pgrdToolBar";
            pgrdToolBar.Size = new Size(150, 24);
            pgrdToolBar.BackColor = pgrdPropertyGrid.BackColor;
            pgrdToolBar.Buttons.Add(buttonSort);
            pgrdToolBar.ImageList = ImageList1;
            pgrdToolBar.ButtonClick += PgrdToolBar_ButtonClick;
            // 
            // PropertyGridHost
            // 
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pgrdPropertyGrid);
            Controls.Add(pgrdComboBox);
            Controls.Add(pgrdsplitter);
            Controls.Add(pgrdTreeView);
            Controls.Add(pgrdToolBar);
            Name = "PropertyGridHost";
            ResumeLayout(false);
            Dock = System.Windows.Forms.DockStyle.Fill;

            // Диспетчер поверхности строго связан с PropertyGridHost.
            if (null == surfaceManager)
            {
                throw new ArgumentNullException("surfaceManager", @"PropertyGridHost::ctor() - Исключение: недопустимый аргумент (null)!");
            }

            SurfaceManager = surfaceManager;
            pgrdPropertyGrid.ToolbarVisible = true;
            pgrdPropertyGrid.HelpVisible = true;

            // Каждый раз, когда кто-либо выбирает новый объект внутри PropertyGridHost
            // событие PropertyGridHost.SelectedObjectsChanged вызывает метод ReloadComboBox().
            pgrdPropertyGrid.SelectedObjectsChanged += (object sender, EventArgs e) =>
            {
                // Здесь делаем подмену исходного компонента, указанного в свойстве pgrdPropertyGrid.SelectedObject на 
                // наш компонент - двойник (similar). Связь между ними через OneScriptFormsDesigner.hashtable.
                dynamic OriginalObj = SelectedObject;
                dynamic SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(OriginalObj);

                // Сделаем невидимыми свойства, которые хотим скрыть.
                // Компонент System.Windows.Forms.ImageList и System.Windows.Forms.MainMenu используем от Майкрософт для помещения на форму, а 
                // в сетке свойств показываем свойства нашего osfDesigner.ImageList и osfDesigner.MainMenu.
                if (SimilarObj == null)
                {
                    if (OriginalObj.GetType() == typeof(System.Windows.Forms.ImageList))
                    {
                        SimilarObj = new osfDesigner.ImageList();
                        ((osfDesigner.ImageList)SimilarObj).OriginalObj = OriginalObj;
                        OneScriptFormsDesigner.AddToDictionary(OriginalObj, SimilarObj);
                        OneScriptFormsDesigner.PassProperties(OriginalObj, SimilarObj); // Передадим свойства.
                        SelectedObject = SimilarObj;
                    }
                    else if (OriginalObj.GetType() == typeof(System.Windows.Forms.MainMenu))
                    {
                        SimilarObj = new osfDesigner.MainMenu();
                        OneScriptFormsDesigner.AddToDictionary(OriginalObj, SimilarObj);
                        OneScriptFormsDesigner.PassProperties(OriginalObj, SimilarObj); // Передадим свойства.
                        SelectedObject = SimilarObj;
                    }
                }
                else
                {
                    SelectedObject = SimilarObj;
                }

                if (OriginalObj.GetType() == typeof(System.Windows.Forms.TabPage))
                {
                    SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(OriginalObj);
                    if (OneScriptFormsDesigner.tic > 1)
                    {
                        SelectedObject = SimilarObj;
                    }
                    else
                    {
                        OneScriptFormsDesigner.tic = OneScriptFormsDesigner.tic + 1;
                    }
                }
                ReloadComboBox();
                ChangeSelectNode(OriginalObj);
            };

            pgrdPropertyGrid.SelectedGridItemChanged += (object sender, SelectedGridItemChangedEventArgs e) =>
            {
                object view = pgrdPropertyGrid.GetType().GetField("gridView", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(pgrdPropertyGrid);
                GridItemCollection GridItemCollection1 = (GridItemCollection)view.GetType().InvokeMember("GetAllGridEntries", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, view, null);
                foreach (GridItem GridItem in GridItemCollection1)
                {
                    if (GridItem.Label == "СписокИзображений" || 
                    GridItem.Label == "СписокБольшихИзображений" || 
                    GridItem.Label == "СписокМаленькихИзображений")
                    {
                        GridItem.Expanded = false;
                    }
                }

                // Выделим компонент на форме используя pgrdPropertyGrid.SelectedObject.
                // Это может понадобиться, если компонент, например кнопка, меняет родителя,
                // иначе не синхронно обновляется сетка свойств, поле выбора над ним, и выделенный на форме объект.
                if (!OneScriptFormsDesigner.block1)
                {
                    ISelectionService iSel = (ISelectionService)OneScriptFormsDesigner.DesignerHost.GetService(typeof(ISelectionService));
                    if (iSel != null)
                    {
                        if (iSel.GetSelectedComponents().Count == 1)
                        {
                            ComponentCollection ctrlsExisting = OneScriptFormsDesigner.DesignerHost.Container.Components;
                            for (int i = 0; i < ctrlsExisting.Count; i++)
                            {
                                if (ctrlsExisting[i].Site.Name == ((Component)pgrdPropertyGrid.SelectedObject).Site.Name)
                                {
                                    IComponent[] arr = { ctrlsExisting[i] };
                                    iSel.SetSelectedComponents(arr);
                                    break;
                                }
                            }
                        }
                    }
                }
            };

            pgrdPropertyGrid.PropertyValueChanged += (object sender, PropertyValueChangedEventArgs e) =>
            {
                dynamic selectedObject = SelectedObject;
                string Label1 = pgrdPropertyGrid.SelectedGridItem.Label;
                if (Label1 == "СписокИзображений")
                {
                    if (e.ChangedItem.Value != e.OldValue)
                    {
                        try
                        {
                            selectedObject.ImageIndex = -1;
                        }
                        catch { }
                    }
                }
                if (Label1 == "Стыковка")
                {
                    PropertyGrid.TopLevelControl.Refresh();
                }

                object view = pgrdPropertyGrid.GetType().GetField("gridView", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(pgrdPropertyGrid);
                GridItemCollection GridItemCollection1 = (GridItemCollection)view.GetType().InvokeMember("GetAllGridEntries", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, view, null);
                foreach (GridItem GridItem in GridItemCollection1)
                {
                    if (GridItem.Label == "СписокИзображений" || 
                    GridItem.Label == "СписокБольшихИзображений" || 
                    GridItem.Label == "СписокМаленькихИзображений")
                    {
                        GridItem.Expanded = false;
                    }
                }
	
                if (Label1 == "ИмяОбъектаФормыДляОдноСкрипта")
                {
                    System.ComponentModel.Design.Serialization.INameCreationService iName = OneScriptFormsDesigner.ActiveDesignSurface.GetIDesignerHost().GetService(typeof(System.ComponentModel.Design.Serialization.INameCreationService)) as System.ComponentModel.Design.Serialization.INameCreationService;
                    if (iName != null)
                    {
                        if (!iName.IsValidName(selectedObject.NameObjectOneScriptForms))
                        {
                            MessageBox.Show("Имя не должно быть пустым, должно начинаться с буквы, не должно содержать пробелы.");
                            PropertyDescriptor pd = TypeDescriptor.GetProperties(selectedObject)["NameObjectOneScriptForms"];
                            pd.SetValue(((Form)selectedObject), (string)e.OldValue);
                            return;
                        }
                    }
                }	
	
                if (Label1 == "(Name)" && selectedObject.GetType() != typeof(Form))
                {
                    MessageBox.Show(
                        "Для правильного формирования файла сценария не допускается изменять имя компонента.",
                        "",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1
                        );
                    if (selectedObject.GetType() == typeof(osfDesigner.TabPage))
                    {
                        PropertyDescriptor pd = TypeDescriptor.GetProperties(selectedObject.M_TabPage)["Name"];
                        pd.SetValue(selectedObject.M_TabPage, (string)e.OldValue);
                    }
                    else
                    {
                        PropertyDescriptor pd = TypeDescriptor.GetProperties(selectedObject)["Name"];
                        pd.SetValue(selectedObject, (string)e.OldValue);
                    }
                }
                if (Label1.Contains("ToolTip"))
                {
                    if ((string)pgrdPropertyGrid.SelectedGridItem.Value != (string)e.OldValue)
                    {
                        string nameToolTip = Label1.Substring(Label1.LastIndexOf(' ') + 1);
                        selectedObject.ToolTip[nameToolTip] = (string)pgrdPropertyGrid.SelectedGridItem.Value;
                    }
                }
                if (Label1 == "Размер")
                {
                    if (selectedObject.GetType() == typeof(osfDesigner.CircularProgressBar))
                    {
                        if (selectedObject.Width < 33 || selectedObject.Height < 33)
                        {
                            PropertyDescriptor pd = TypeDescriptor.GetProperties(selectedObject)["Size"];
                            pd.SetValue(selectedObject, new System.Drawing.Size(33, 33));
                        }
                        int old_si = pgrdComboBox.SelectedIndex;
                        try
                        {
                            pgrdComboBox.SelectedIndex = old_si + 1;
                        }
                        catch
                        {
                            pgrdComboBox.SelectedIndex = old_si - 1;
                        }
                        pgrdComboBox.SelectedIndex = old_si;
                    }
                }
            };

            // Каждый раз, когда кто-либо выбирает новый объект внутри ComboBox
            // событие ComboBox.SelectedIndexChanged вызывает метод OrientPropertyGridTowardsObject().
            pgrdComboBox.SelectedIndexChanged += (object sender, EventArgs e) =>
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
        }

        public void ChangeSelectNode(Component comp, TreeNodeCollection treeNodes = null)
        {
            if (_bSuppressEvents2)
            {
                return;
            }

            TreeNodeCollection _treeNodes;
            if (treeNodes == null)
            {
                _treeNodes = TreeView.Nodes;
            }
            else
            {
                _treeNodes = treeNodes;
            }

            ISelectionService iSel = (ISelectionService)(OneScriptFormsDesigner.DesignerHost.GetService(typeof(ISelectionService)));
            if (iSel != null)
            {
                ICollection collection = iSel.GetSelectedComponents();
                Component[] arr = new Component[collection.Count];
                collection.CopyTo(arr, 0);
                Component comp1 = null;
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
                        TreeNode treeNode;
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
            }
        }

        private void PgrdTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            _bSuppressEvents2 = true;
            IDesignerHost host = OneScriptFormsDesigner.DesignerHost;
            ISelectionService iSel = (ISelectionService)host.GetService(typeof(ISelectionService));
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

        // Используйте свойство SurfaceManager.ActiveDesignSurface которое указывает на АКТИВНУЮ область дизайнера (DesignSurface).
        private void OrientPropertyGridTowardsObject() // Ориентация сетки свойств по отношению к объекту.
        {
            IDesignerEventService des = (IDesignerEventService)SurfaceManager.GetService(typeof(IDesignerEventService));
            if (null != des)
            {
                IDesignerHost host = des.ActiveDesigner;
                // Получим ISelectionService из активной поверхности дизайнера.
                ISelectionService iSel = (ISelectionService)host.GetService(typeof(ISelectionService));
                if (iSel != null)
                {
                    // Получим имя элемента управления, выбранного в comboBox.
                    string sName = pgrdComboBox.SelectedItem.ToString();
                    if (!string.IsNullOrEmpty(sName))
                    {
                        // Сохраним коллекцию выбранных объектов. Циклично пройдем через элементы управления внутри текущей поверхности дизайнера.
                        // Если мы найдем выбранный в comboBox элемент, то используем ISelectionService, чтобы выбрать его и он будет выбран в PropertyGridHost.
                        ComponentCollection ctrlsExisting = host.Container.Components;
                        Debug.Assert(0 != ctrlsExisting.Count);
                        foreach (Component comp in ctrlsExisting)
                        {
                            if (sName == comp.Site.Name)
                            {
                                Component[] arr = { comp };
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
                Form1 = (Form)OneScriptFormsDesigner.DesignerHost.Container.Components[0];
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
                return;
            }

            IDesignerEventService des = (IDesignerEventService)SurfaceManager.GetService(typeof(IDesignerEventService));
            if (null == des)
            {
                return;
            }

            TreeView.Nodes.Clear();

            ComponentCollection ctrls = des.ActiveDesigner.Container.Components;
            ArrayList ctrlsExisting = new ArrayList();
            ArrayList ctrlsExisting1 = new ArrayList();
            for (int i = 0; i < ctrls.Count; i++)
            {
                ctrlsExisting.Add(ctrls[i]);
                ctrlsExisting1.Add(ctrls[i]);
            }

            if (!buttonSort.Pushed)
            {
                Dictionary<string, IComponent> comps = new Dictionary<string, IComponent>();
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
                Dictionary<string, TreeNode> comps2 = new Dictionary<string, TreeNode>();
                foreach (KeyValuePair<string, IComponent> keyValue in comps)
                {
                    string parentName = "";
                    Control parent1 = null;
                    try
                    {
                        parent1 = ((dynamic)keyValue.Value).Parent;
                    }
                    catch { }

                    if (parent1 != null)
                    {
                        for (int i1 = 0; i1 < ctrlsExisting1.Count; i1++)
                        {
                            if (parent1 == ctrlsExisting1[i1])
                            {
                                parentName = ((IComponent)ctrlsExisting1[i1]).Site.Name;
                                break;
                            }
                        }
                    }

                    TreeNode TreeNode1 = new TreeNode(keyValue.Key);
                    TreeNode1.Tag = parentName;
                    TreeNode1.Name = keyValue.Key;
                    comps2.Add(keyValue.Key, TreeNode1);
                }
                foreach (KeyValuePair<string, IComponent> keyValue in comps)
                {
                    TreeNode TreeNode1 = comps2[keyValue.Key];
                    if ((string)TreeNode1.Tag != "")
                    {
                        TreeNode NodeParent = comps2[(string)TreeNode1.Tag];
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
                SortedList SortedList1 = new SortedList();
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

                SortedList SortedList2 = new SortedList();
                foreach (DictionaryEntry de in SortedList1)
                {
                    string parentName = "";
                    Control parent1 = null;
                    try
                    {
                        parent1 = ((dynamic)de.Value).Parent;
                    }
                    catch { }

                    if (parent1 != null)
                    {
                        for (int i1 = 0; i1 < ctrlsExisting1.Count; i1++)
                        {
                            if (parent1 == ctrlsExisting1[i1])
                            {
                                parentName = ((IComponent)ctrlsExisting1[i1]).Site.Name;
                                break;
                            }
                        }
                    }

                    TreeNode TreeNode1 = new TreeNode((string)de.Key);
                    TreeNode1.Tag = parentName;
                    TreeNode1.Name = (string)de.Key;
                    SortedList2.Add(de.Key, TreeNode1);
                }
                foreach (DictionaryEntry de in SortedList1)
                {
                    TreeNode TreeNode1 = (TreeNode)SortedList2[(string)de.Key];
                    if ((string)TreeNode1.Tag != "")
                    {
                        TreeNode NodeParent = (TreeNode)SortedList2[TreeNode1.Tag];
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

            IDesignerEventService des = (IDesignerEventService)SurfaceManager.GetService(typeof(IDesignerEventService));
            if (null == des)
            {
                return;
            }
            IDesignerHost host = des.ActiveDesigner;

            object selectedObj = pgrdPropertyGrid.SelectedObject;
            if (null == selectedObj)
            {
                return;
            }

            // Получим имя элемента управления, выбранного в comboBox.
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
                sName = ((Component)selectedObj).Site.Name; // Чтобы не визуальные компоненты меняли индекс в ComboBox.
            }

            // Подготовка данных для перезагрузки combobox (начало).
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
            // Обновим combobox (фиксация).
            pgrdComboBox.Items.Clear();
            pgrdComboBox.Items.AddRange(ctrlsToAdd.ToArray());
            pgrdComboBox.Text = pgrdComboBox_Text;

            _bSuppressEvents = false;
        }

        // Свернем одиночный элемент сетки (GridItem) тот, который имеет значение параметра "sGridItemLabel".
        public GridItem CollapseGridItem(string sGridItemLabel)
        {
            return CollapseExpandGridItem(sGridItemLabel, false);
        }

        // Развернем одиночный элемент сетки (GridItem) тот, который имеет значение параметра "sGridItemLabel".
        public GridItem ExpandGridItem(string sGridItemLabel)
        {
            return CollapseExpandGridItem(sGridItemLabel, true);
        }

        private GridItem CollapseExpandGridItem(string sGridItemLabel, bool bExpanded)
        {
            // Получим корневой элемент GridItem.
            GridItem root = PropertyGrid.SelectedGridItem;
            if (null == root)
            {
                return null;
            }

            while (null != root.Parent)
            {
                root = root.Parent;
            }

            if (null == root)
            {
                return null;
            }

            // И начнем поиск из корня.
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
