using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace osfDesigner
{
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public class pDesigner : System.Windows.Forms.UserControl, IpDesigner
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.SplitContainer splitterpDesigner;
        public static System.Windows.Forms.SplitContainer SplitterpDesigner;
        private System.Windows.Forms.Panel codePanel;// панель с кодом скрипта
        private System.Windows.Forms.RichTextBox richTextBox;
        public static System.Windows.Forms.Panel CodePanel = null;
        public static System.Windows.Forms.RichTextBox RichTextBox = null;
        private System.Windows.Forms.TabControl tbCtrlpDesigner;
        public static System.Windows.Forms.TabControl TabControl;
        private const string _Name_ = "pDesigner";
        public static DesignSurfaceManagerExt DSME = null;
        private System.Windows.Forms.Form form;

        public pDesigner()
        {
            this.codePanel = new System.Windows.Forms.Panel();
            this.codePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.richTextBox.Parent = this.codePanel;
            this.richTextBox.WordWrap = false;
            this.richTextBox.ReadOnly = true;
            this.richTextBox.BackColor = Color.White;
            this.richTextBox.EnableContextMenu();

            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codePanel.Hide();

            this.splitterpDesigner = new System.Windows.Forms.SplitContainer();
            this.tbCtrlpDesigner = new System.Windows.Forms.TabControl();
            this.splitterpDesigner.Panel1.SuspendLayout();
            this.splitterpDesigner.SuspendLayout();
            this.SuspendLayout();
            //
            // splitterpDesigner
            //
            this.splitterpDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitterpDesigner.Location = new System.Drawing.Point(0, 0);
            this.splitterpDesigner.BackColor = Color.LightSteelBlue;
            this.splitterpDesigner.Name = "splitterpDesigner";
            //
            // splitterpDesigner.Panel1
            //
            this.splitterpDesigner.Panel1.Controls.Add(this.tbCtrlpDesigner);
            this.splitterpDesigner.Size = new System.Drawing.Size(635, 305);
            this.splitterpDesigner.SplitterDistance = 439;
            this.splitterpDesigner.TabIndex = 0;
            //
            // tbCtrlpDesigner
            //
            this.tbCtrlpDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCtrlpDesigner.Location = new System.Drawing.Point(0, 0);
            this.tbCtrlpDesigner.Name = "tbCtrlpDesigner";
            this.tbCtrlpDesigner.SelectedIndex = 0;
            this.tbCtrlpDesigner.Size = new System.Drawing.Size(439, 305);
            this.tbCtrlpDesigner.TabIndex = 0;
            this.tbCtrlpDesigner.SelectedIndexChanged += new System.EventHandler(this.tbCtrlpDesigner_SelectedIndexChanged);
            //
            // pDesigner
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.splitterpDesigner);
            this.Controls.Add(this.codePanel);
            this.Name = "pDesigner";
            this.Size = new System.Drawing.Size(635, 305);
            this.splitterpDesigner.Panel1.ResumeLayout(false);
            this.splitterpDesigner.ResumeLayout(false);
            this.ResumeLayout(false);

            DesignSurfaceManager = new DesignSurfaceManagerExt();
            DesignSurfaceManager.PropertyGridHost.Parent = this.splitterpDesigner.Panel2;
            Toolbox = null;
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            DSME = DesignSurfaceManager;
            SplitterpDesigner = splitterpDesigner;
            CodePanel = codePanel;
            RichTextBox = richTextBox;
            TabControl = tbCtrlpDesigner;
        }

        // Очистите все используемые ресурсы.
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Экземпляр класса DesignSurfaceManagerExt должен наблюдать за событиями которые изменяют активную область дизайнера
        // DesignSurfaceManager представлен как публичное геттер свойство в качестве средства тестирования
        public DesignSurfaceManagerExt DesignSurfaceManager { get; private set; }

        public static implicit operator bool(pDesigner d)
        {
            bool isValid = true;
            // Объект 'd' должен быть правильно инициализирован
            isValid &= ((null == d.Toolbox) ? false : true);
            return isValid;
        }

        private void tbCtrlpDesigner_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TabControl tabCtrl = sender as System.Windows.Forms.TabControl;
            int index = this.tbCtrlpDesigner.SelectedIndex;
            if (index >= 0)
            {
                DesignSurfaceManager.ActiveDesignSurface = (DesignSurfaceExt2)DesignSurfaceManager.DesignSurfaces[index];
            }
            else
            {
                DesignSurfaceManager.ActiveDesignSurface = null;
                DesignSurfaceManager.PropertyGridHost.PropertyGrid.SelectedObject = null;
                DesignSurfaceManager.PropertyGridHost.ComboBox.Items.Clear();
            }
        }

        // Получение и установка реальной панели элементов (Toolbox), предоставляемой пользователю
        public System.Windows.Forms.ListBox Toolbox { get; set; }

        public System.Windows.Forms.TabControl TabControlHostingDesignSurfaces
        {
            get { return this.tbCtrlpDesigner; }
        }

        public PropertyGridHost PropertyGridHost
        {
            get { return DesignSurfaceManager.PropertyGridHost; }
        }

        public DesignSurfaceExt2 ActiveDesignSurface
        {
            get { return DesignSurfaceManager.ActiveDesignSurface as DesignSurfaceExt2; }
        }

        // Создайте область дизайнера (DesignSurface) и корневой компонент (rootComponent) (элемент управления .NET)
        // используя IDesignSurfaceExt.CreateRootComponent() 
        // Если режим выравнивания (alignmentMode) не использует сетку (GRID), то параметр gridSize игнорируется
        // Примечание:
        //     параметры используются для определения элемента управления, используемого в качестве корневого компонента (RootComponent)
        //     TT запрашивается как производное от .NET класса элемента управления
        public DesignSurfaceExt2 AddDesignSurface<TT>(
            int startingFormWidth, 
            int startingFormHeight,
            AlignmentModeEnum alignmentMode, 
            Size gridSize,
            string formName = null
            ) where TT : Control
        {
            const string _signature_ = _Name_ + @"::AddDesignSurface<>()";

            if (!this)
            {
                throw new Exception(_signature_ + " - Exception: " + _Name_ + " is not initialized! Please set the Property: IpDesigner::Toolbox before calling any methods!");
            }
            // создание области дизайнера (DesignSurface)
            DesignSurfaceExt2 surface = DesignSurfaceManager.CreateDesignSurfaceExt2();
            this.DesignSurfaceManager.ActiveDesignSurface = surface;
            // Выбор режима выравнивания...
            switch (alignmentMode)
            {
                case AlignmentModeEnum.SnapLines:
                    surface.UseSnapLines();
                    break;
                case AlignmentModeEnum.Grid:
                    surface.UseGrid(gridSize);
                    break;
                case AlignmentModeEnum.GridWithoutSnapping:
                    surface.UseGridWithoutSnapping(gridSize);
                    break;
                case AlignmentModeEnum.NoGuides:
                    surface.UseNoGuides();
                    break;
                default:
                    surface.UseSnapLines();
                    break;
            }
            // задействуем UndoEngine
            ((IDesignSurfaceExt)surface).GetUndoEngineExt().Enabled = true;
            // Выбор службы IToolboxService и привязка её к ListBox
            ToolboxServiceImp tbox = ((IDesignSurfaceExt2)surface).GetIToolboxService() as ToolboxServiceImp;
            // Мы не проверяем, имеет ли Toolbox значение null, поскольку самая первая проверка: if(!this)...
            if (null != tbox)
            {
                tbox.Toolbox = this.Toolbox;
            }
            // создание корневого (Root) компонента, в случае если это Форма (Form)
            Control rootComponent = null;
            // приведение к .NET элементу управления, поскольку объект TT имеет ограничение: может быть только .NET "Control"
            rootComponent = surface.CreateRootComponent(typeof(TT), new Size(startingFormWidth, startingFormHeight)) as Control;
            // переименуем размещаемый компонент потому что пользователь может добавить более одной формы
            // и каждая новая форма будет называться "Form1" если мы не зададим её имя (Name))
            if (formName != null)
            {
                // организуем вопрос об имени загружаемой формы
                form = new System.Windows.Forms.Form();
                form.Size = new Size(390, 120);
                form.Text = "Имя для загружаемой формы:";
                form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                form.ControlBox = true;
                form.HelpButton = false;
                form.ShowIcon = false;
                form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                form.SizeGripStyle = SizeGripStyle.Hide;
                form.MaximizeBox = false;
                form.MinimizeBox = false;
                form.ShowInTaskbar = false;

                System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
                form.Controls.Add(textBox);
                textBox.Bounds = new Rectangle(10, 10, 350, 28);
                textBox.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
                textBox.Text = formName;

                System.Windows.Forms.Button buttonOK = new System.Windows.Forms.Button();
                buttonOK.Parent = form;
                buttonOK.Text = "OK";
                buttonOK.Bounds = new Rectangle(280, 45, 75, 28);
                buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
                buttonOK.Click += ButtonOK_Click;

                System.Windows.Forms.DialogResult dr = form.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.Cancel)
                {
                    rootComponent.Site.Name = textBox.Text;
                }
                else if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    rootComponent.Site.Name = textBox.Text;
                }
                form.Close();
                form.Dispose();
            }
            else
            {
                rootComponent.Site.Name = this.DesignSurfaceManager.GetValidFormName();
            }

            // разрешение перетаскивания (Drag&Drop) для RootComponent
            ((DesignSurfaceExt2)surface).EnableDragandDrop();
            // IComponentChangeService помечена как незаменяемая служба
            IComponentChangeService componentChangeService = (IComponentChangeService)(surface.GetService(typeof(IComponentChangeService)));
            if (null != componentChangeService)
            {
                // Тип "ComponentEventHandler Delegate" представляет метод который будет обрабатывать ComponentAdding, 
                // ComponentAdded, ComponentRemoving, и ComponentRemoved события, возникшие как события уровня компонента
                componentChangeService.ComponentChanged += (Object sender, ComponentChangedEventArgs e) =>
                {
                    dynamic OriginalObj = e.Component;
                    if (OriginalObj.GetType().ToString() == "System.Windows.Forms.TabPage")
                    {
                        dynamic SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(OriginalObj);
                        if (SimilarObj == null)
                        {
                            SimilarObj = new osfDesigner.TabPage();
                            OneScriptFormsDesigner.PassProperties(OriginalObj, SimilarObj);//передадим свойства
                            SimilarObj.OriginalObj = OriginalObj;
                            OriginalObj.Tag = SimilarObj;
                            OneScriptFormsDesigner.AddToHashtable(OriginalObj, SimilarObj);
                        }
                        DesignSurfaceManager.PropertyGridHost.PropertyGrid.SelectedObject = SimilarObj;
                    }
                    else
                    {
                        DesignSurfaceManager.PropertyGridHost.PropertyGrid.SelectedObject = e.Component;
                    }

                    PropertyGridHost.ReloadTreeView();
                    PropertyGridHost.ChangeSelectNode((Component)e.Component);
                };
                componentChangeService.ComponentAdded += (Object sender, ComponentEventArgs e) =>
                {
                    dynamic OriginalObj = e.Component;
                    if (OriginalObj.GetType().ToString() == "System.Windows.Forms.TabPage")
                    {
                        osfDesigner.TabPage SimilarObj = new osfDesigner.TabPage();
                        OneScriptFormsDesigner.PassProperties(OriginalObj, SimilarObj);//передадим свойства
                        SimilarObj.OriginalObj = OriginalObj;
                        OriginalObj.Tag = SimilarObj;
                        OneScriptFormsDesigner.AddToHashtable(OriginalObj, SimilarObj);
                    }

                    if (OriginalObj.GetType().ToString() == "System.Windows.Forms.ImageList" || 
                    OriginalObj.GetType().ToString() == "osfDesigner.TreeView" || 
                    OriginalObj.GetType().ToString() == "osfDesigner.DataGrid" || 
                    OriginalObj.GetType().ToString() == "osfDesigner.RichTextBox")
                    {
                        IDesignerHost designerHost = pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost();
                        if (designerHost != null)
                        {
                            dynamic designer = designerHost.GetDesigner(OriginalObj);
                            if (designer != null)
                            {
                                designer.ActionLists.Clear();
                            }
                        }
                    }

                    if (OriginalObj.GetType().ToString() == "osfDesigner.DataGrid" ||
                    OriginalObj.GetType().ToString() == "osfDesigner.TabControl")
                    {
                        IDesignerHost designerHost = pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost();
                        if (designerHost != null)
                        {
                            dynamic designer = designerHost.GetDesigner(OriginalObj);
                            if (designer != null)
                            {
                                ((IDesigner)designer).Verbs.Clear();
                            }
                        }
                    }

                    DesignSurfaceManager.UpdatePropertyGridHost(surface);

                    // Получим начальные значения свойств для компонента, они нужны для создания скрипта
                    if (OriginalObj.GetType().ToString() == "System.Windows.Forms.TabPage")
                    {
                        GetDefaultValues(osfDesigner.OneScriptFormsDesigner.RevertSimilarObj(OriginalObj));
                    }
                    else
                    {
                        GetDefaultValues(OriginalObj);
                    }

                    ((Form)rootComponent).ArrayListComponentsAddingOrder.Add(OriginalObj);

                    PropertyGridHost.ReloadTreeView();
                    PropertyGridHost.ChangeSelectNode((Component)e.Component);
                };

                componentChangeService.ComponentRemoving += (Object sender, ComponentEventArgs e) =>
                {
                    ((Form)rootComponent).ArrayListComponentsAddingOrder.Remove(e.Component);
                };

                componentChangeService.ComponentRemoved += (Object sender, ComponentEventArgs e) =>
                {
                    DesignSurfaceManager.UpdatePropertyGridHost(surface);
                    PropertyGridHost.ReloadTreeView();
                };
            }
            // Теперь установим свойство "Форма.Текст", (потому что это будет пустая строка если мы не установим его)
            Control view = surface.GetView();
            if (null == view)
            {
                return null;
            }
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(view);
            // Установим свойство PropertyDescriptor для конкретного свойства
            PropertyDescriptor pdS = pdc.Find("Text", false);
            if (null != pdS)
            {
                pdS.SetValue(rootComponent, rootComponent.Site.Name);
            }
            // отобразим область дизайнера (DesignSurface)
            System.Windows.Forms.TabPage newPage = new System.Windows.Forms.TabPage();
            string sTabPageText = OneScriptFormsDesigner.RevertDesignerTabName(rootComponent.Site.Name);
            // свяжем rootComponent и создаваемую для него вкладку дризайнера, чтобы потом при удалении формы корректно удалить и вкладку
            OneScriptFormsDesigner.AddToHashtableDesignerTabRootComponent(rootComponent, newPage);
            newPage.Text = sTabPageText;
            newPage.Name = sTabPageText;
            newPage.SuspendLayout();
            view.Dock = System.Windows.Forms.DockStyle.Fill;
            view.Parent = newPage; // Отметьте это назначение
            this.tbCtrlpDesigner.TabPages.Add(newPage);
            newPage.ResumeLayout();
            // Выберите созданную вкладку (TabPage)
            this.tbCtrlpDesigner.SelectedIndex = this.tbCtrlpDesigner.TabPages.Count - 1;
            this.PropertyGridHost.ReloadComboBox();

            // Получим начальные значения свойств формы, они нужны для создания скрипта
            GetDefaultValues(rootComponent);

            //* 18.12.2021 perfolenta 
            surface.Dirty = false;
            //***

            // Наконец, возвратим созданную область дизайнера, чтобы она снова изменялась пользователем
            return surface;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            form.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public void GetDefaultValues(dynamic comp)
        {
            // Заполним для компонента начальные свойства. Они нужны будут при создании скрипта.
            string DefaultValues1 = "";
            object pg = DesignSurfaceManager.PropertyGridHost.PropertyGrid;

            //System.Windows.Forms.MessageBox.Show("pg=" + pg);

            ((System.Windows.Forms.PropertyGrid)pg).SelectedObject = comp;

            //System.Windows.Forms.MessageBox.Show("((System.Windows.Forms.PropertyGrid)pg).SelectedObject=" + ((System.Windows.Forms.PropertyGrid)pg).SelectedObject.GetType());

            object view1 = pg.GetType().GetField("gridView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(pg);
            GridItemCollection GridItemCollection1 = (GridItemCollection)view1.GetType().InvokeMember("GetAllGridEntries", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, view1, null);
            if (GridItemCollection1 == null)
            {
                return;
            }
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
            if (comp.GetType() == typeof(System.Windows.Forms.ImageList) ||
                comp.GetType() == typeof(System.Windows.Forms.MainMenu))
            {
                OneScriptFormsDesigner.RevertSimilarObj(comp).DefaultValues = DefaultValues1;
            }
            else
            {
                comp.DefaultValues = DefaultValues1;
            }
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

        public void RemoveDesignSurface(DesignSurfaceExt2 surfaceToErase)
        {
            try
            {
                // Удалить вкладку связанную через hashtableDesignerTabRootComponent с корневым компонентом (RootComponent), который удаляют
                // Примечание:
                //     DesignSurfaceManager продолжает ссылаться на удаленную поверхность DesignSurface
                //     потому что Designsurface продолжает существовать, но он больше не доступен (не достижим)
                //     этот факт будет полезен при создании новых имен для Только что созданных поверхностей Designsurfaces
                //     чтобы избегать столкновения имен
                System.Windows.Forms.TabPage TabPage1 = OneScriptFormsDesigner.RevertDesignerTab(surfaceToErase.GetIDesignerHost().RootComponent);
                System.Windows.Forms.TabPage tpToRemove = null;
                foreach (System.Windows.Forms.TabPage tp in this.tbCtrlpDesigner.TabPages)
                {
                    if (tp.Equals(TabPage1))
                    {
                        tpToRemove = tp;
                        break;
                    }
                }
                if (null != tpToRemove)
                {
                    this.tbCtrlpDesigner.TabPages.Remove(tpToRemove);
                }

                // теперь удалите поверхность дизайнера
                this.DesignSurfaceManager.DeleteDesignSurfaceExt2(surfaceToErase);

                // наконец, диспетчер DesignSurfaceManager удалит поверхность DesignSurface
                // И установит в качестве активной поверхности дизайна последнюю
                // поэтому мы устанавливаем в качестве активной последнюю страницу вкладки
                this.tbCtrlpDesigner.SelectedIndex = this.tbCtrlpDesigner.TabPages.Count - 1;
            }
            catch { }
        }

        public void UndoOnDesignSurface()
        {
            IDesignSurfaceExt2 isurf = DesignSurfaceManager.ActiveDesignSurface;
            if (null != isurf)
            {
                isurf.GetUndoEngineExt().Undo();
            }
        }

        public void RedoOnDesignSurface()
        {
            IDesignSurfaceExt2 isurf = DesignSurfaceManager.ActiveDesignSurface;
            if (null != isurf)
            {
                isurf.GetUndoEngineExt().Redo();
            }
        }

        public void CutOnDesignSurface()
        {
            IDesignSurfaceExt isurf = DesignSurfaceManager.ActiveDesignSurface;
            if (null != isurf)
            {
                isurf.DoAction("Cut");
            }
        }

        public void CopyOnDesignSurface()
        {
            IDesignSurfaceExt isurf = DesignSurfaceManager.ActiveDesignSurface;
            if (null != isurf)
            {
                isurf.DoAction("Copy");
            }
        }

        public void PasteOnDesignSurface()
        {
            IDesignSurfaceExt isurf = DesignSurfaceManager.ActiveDesignSurface;
            if (null != isurf)
            {
                isurf.DoAction("Paste");
            }
        }

        public void DeleteOnDesignSurface()
        {
            IDesignSurfaceExt isurf = DesignSurfaceManager.ActiveDesignSurface;
            if (null != isurf)
            {
                try
                {
                    System.Windows.Forms.SplitContainer SplitContainer1 = (System.Windows.Forms.SplitContainer)this.ActiveControl;
                    osfDesigner.PropertyGridHost PropertyGridHost1 = (osfDesigner.PropertyGridHost)SplitContainer1.ActiveControl;
                    bool ToolbarVisible1 = PropertyGridHost1.PropertyGrid.ToolbarVisible;
                }
                catch
                {
                    System.Windows.Forms.DialogResult res1 = System.Windows.Forms.MessageBox.Show(
                        "Действительно удалить выбранные компоненты?",
                        "",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2
                        );

                    if (res1 == System.Windows.Forms.DialogResult.OK || res1 == System.Windows.Forms.DialogResult.Yes)
                    {
                        isurf.DoAction("Delete");
                    }
                }
            }
        }

        public void SwitchTabOrder()
        {
            IDesignSurfaceExt isurf = DesignSurfaceManager.ActiveDesignSurface;
            if (null != isurf)
            {
                isurf.SwitchTabOrder();
            }
        }

        static bool VerifyPasswords(string lhs, string rhs)
        {
            int minLength = Math.Min(lhs.Length, rhs.Length);
            for (int i = 0; i < minLength; i++)
            {
                if (lhs[i] == rhs[i])
                {
                };
                if (lhs[i] != rhs[i])
                {
                    return false;
                }
            }
            return true;
        }

        //* 17.12.2021 perfolenta
        public bool Dirty
        {
            get
            {
                //надо перебрать все дизайнеры форм и если хоть один модифицирован, то возвращаем Истина
                foreach (var dds in DSME.GetDesignSurfaces())
                {
                    if (dds.Dirty) return true;
                }
                return false;
            }
        }
        //***

    }
}
