using System.ComponentModel.Design.Serialization;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class pDesigner : System.Windows.Forms.UserControl, IpDesigner
    {
        private IContainer components = null;
        private SplitContainer splitterpDesigner;
        public static SplitContainer SplitterpDesigner;
        private System.Windows.Forms.Panel codePanel; // Панель с кодом скрипта.
        private System.Windows.Forms.RichTextBox richTextBox;
        public static System.Windows.Forms.Panel CodePanel = null;
        public static System.Windows.Forms.RichTextBox RichTextBox = null;
        private System.Windows.Forms.TabControl tbCtrlpDesigner;
        public static System.Windows.Forms.TabControl TabControl;
        public static DesignSurfaceManagerExt DSME = null;
        private System.Windows.Forms.Form form;
        private Image closeImage;
        private Image closeImageBlack;
        private Image saveYesImage;
        private Image saveNoImage;
        private System.Windows.Forms.TextBox textBox;

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

            this.splitterpDesigner = new SplitContainer();
            this.tbCtrlpDesigner = new System.Windows.Forms.TabControl();
            this.splitterpDesigner.Panel1.SuspendLayout();
            this.splitterpDesigner.SuspendLayout();
            this.SuspendLayout();
            //
            // splitterpDesigner
            //
            this.splitterpDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitterpDesigner.Location = new Point(0, 0);
            this.splitterpDesigner.BackColor = Color.LightSteelBlue;
            this.splitterpDesigner.Name = "splitterpDesigner";
            //
            // splitterpDesigner.Panel1
            //
            this.splitterpDesigner.Panel1.Controls.Add(this.tbCtrlpDesigner);
            this.splitterpDesigner.Size = new Size(635, 305);
            this.splitterpDesigner.SplitterDistance = 439;
            this.splitterpDesigner.TabIndex = 0;
            //
            // tbCtrlpDesigner
            //
            this.tbCtrlpDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCtrlpDesigner.Location = new Point(0, 0);
            this.tbCtrlpDesigner.Name = "tbCtrlpDesigner";
            this.tbCtrlpDesigner.SelectedIndex = 0;
            this.tbCtrlpDesigner.Size = new Size(439, 305);
            this.tbCtrlpDesigner.TabIndex = 0;
            this.tbCtrlpDesigner.SelectedIndexChanged += new EventHandler(this.tbCtrlpDesigner_SelectedIndexChanged);
            this.tbCtrlpDesigner.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tbCtrlpDesigner.DrawItem += tbCtrlpDesigner_DrawItem;
            this.tbCtrlpDesigner.MouseDown += tbCtrlpDesigner_MouseDown;
            string str_Close = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAASUlEQVR42mNkoBAwUtuA/0QYiqKGEU2CkMsw1DASUkBIDptN2BTiNJgYp+JVjy/A/hOjlmYGUOQFigKRomikWkIiOymTBSg2AAD6ABQR9GDqGgAAAABJRU5ErkJggg==";
            closeImage = OneScriptFormsDesigner.Base64ToImage(str_Close);
            string str_CloseBlack = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAU0lEQVR42mNkoBAwUtWAefPm/U9KSsJrKLoaRmQJGBuXIdjUMBJSQEiOkRhb8BlM0KnIAJvXcAYYuiG4woU2BlDkBYoCkaJopFpCIjspkwsoNgAA24ZQEaHwYTIAAAAASUVORK5CYII=";
            closeImageBlack = OneScriptFormsDesigner.Base64ToImage(str_CloseBlack);
            string str_SaveYes = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAUUlEQVR42mNkgID/DOQBRkaY5v9tbQyMVVVgQRyK/4PUwHVC1IIVwyVIMQCmniYG4AQEDSA5BIehAYQCEQaQ1ZMSjXD9owZgGgAWJCr4MQEjAF/ZYAN2oSypAAAAAElFTkSuQmCC";
            saveYesImage = OneScriptFormsDesigner.Base64ToImage(str_SaveYes);
            string str_SaveNo = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAUElEQVR42u2RQQoAIAgE9Wc+3Z8ZWRtBRBl0ieZo0xyUqWB0BjM+qyqJiA8nsmUHVNfl9hAJwL8SmLIMRHkxsFoi6P3IGYH9wBjw4db6RzgBtFFgA13ClvoAAAAASUVORK5CYII=";
            saveNoImage = OneScriptFormsDesigner.Base64ToImage(str_SaveNo);
            //
            // pDesigner
            //
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.splitterpDesigner);
            this.Controls.Add(this.codePanel);
            this.Name = "pDesigner";
            this.Size = new Size(635, 305);
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

        private void tbCtrlpDesigner_MouseDown(object sender, MouseEventArgs e)
        {
            for (var i = 0; i < tbCtrlpDesigner.TabPages.Count; i++)
            {
                var tabRect = tbCtrlpDesigner.GetTabRect(i);
                tabRect.Inflate(-2, -2);

                var imageRect = new Rectangle(
                    (tabRect.Right - closeImage.Width),
                    tabRect.Top + (tabRect.Height - closeImage.Height) / 2,
                    closeImage.Width,
                    closeImage.Height);
                if (imageRect.Contains(e.Location))
                {
                    if (tbCtrlpDesigner.TabPages.Count <= 1)
                    {
                        System.Windows.Forms.MessageBox.Show(
                            "Удалить единственную форму не допускается.",
                            "",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button1
                            );
                    }
                    else
                    {
                        System.Windows.Forms.DialogResult res1 = System.Windows.Forms.MessageBox.Show(
                            "Действительно удалить форму " + tbCtrlpDesigner.TabPages[i].Text.Trim() + "?",
                            "",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button2
                           );
                        if (res1 == System.Windows.Forms.DialogResult.OK || res1 == System.Windows.Forms.DialogResult.Yes)
                        {
                            OneScriptFormsDesigner.dictionaryTabPageChanged.Remove(TabControl.SelectedTab);
                            RemoveDesignSurface(pDesigner.DSME.ActiveDesignSurface);
                        }
                    }
                    break;
                }
            }
        }
        private void tbCtrlpDesigner_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tabPage = tbCtrlpDesigner.TabPages[e.Index];
            var tabRect = tbCtrlpDesigner.GetTabRect(e.Index);
            tabRect.Inflate(-2, -2);
            
            if (OneScriptFormsDesigner.dictionaryTabPageChanged[tabPage])
            {
                e.Graphics.DrawImage(saveYesImage, (tabRect.Left + 1), tabRect.Top + (tabRect.Height - saveYesImage.Height) / 2);
            }
            else
            {
                e.Graphics.DrawImage(saveNoImage, (tabRect.Left + 1), tabRect.Top + (tabRect.Height - saveNoImage.Height) / 2);
            }

            if (e.State == DrawItemState.Selected)
            {
                e.Graphics.DrawImage(closeImage, (tabRect.Right - closeImage.Width), tabRect.Top + (tabRect.Height - closeImage.Height) / 2);
                e.Graphics.DrawString(tabPage.Text, new Font(tabPage.Font, FontStyle.Bold), new SolidBrush(Color.Black), e.Bounds.X + 4, e.Bounds.Y + 2);
            }
            else
            {
                e.Graphics.DrawImage(closeImageBlack, (tabRect.Right - closeImageBlack.Width), tabRect.Top + (tabRect.Height - closeImageBlack.Height) / 2);
                e.Graphics.DrawString(tabPage.Text, tabPage.Font, new SolidBrush(tabPage.ForeColor), e.Bounds.X + 4, e.Bounds.Y + 2);
            }
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

        // Экземпляр класса DesignSurfaceManagerExt должен наблюдать за событиями которые изменяют активную область дизайнера.
        public DesignSurfaceManagerExt DesignSurfaceManager { get; private set; }

        public static implicit operator bool(pDesigner d)
        {
            bool isValid = true;
            // Объект 'd' должен быть правильно инициализирован.
            isValid &= ((null == d.Toolbox) ? false : true);
            return isValid;
        }

        private void tbCtrlpDesigner_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tbCtrlpDesigner.SelectedIndex;
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

        // Получение и установка реальной панели элементов (Toolbox), предоставляемой пользователю.
        public System.Windows.Forms.ListBox Toolbox { get; set; }

        public System.Windows.Forms.TabControl TabControlHostingDesignSurfaces
        {
            get { return tbCtrlpDesigner; }
        }

        public PropertyGridHost PropertyGridHost
        {
            get { return DesignSurfaceManager.PropertyGridHost; }
        }

        public DesignSurfaceExt2 ActiveDesignSurface
        {
            get { return DesignSurfaceManager.ActiveDesignSurface as DesignSurfaceExt2; }
        }

        // Создайте область дизайнера (DesignSurface) и корневой компонент (rootComponent) (элемент управления .NET) используя IDesignSurfaceExt.CreateRootComponent().
        // Если режим выравнивания (alignmentMode) не использует сетку (GRID), то параметр gridSize игнорируется.
        // Примечание:
        //     Параметры используются для определения элемента управления, используемого в качестве корневого компонента (RootComponent).
        //     TT запрашивается как производное от .NET класса элемента управления.
        public DesignSurfaceExt2 AddDesignSurface<TT>(
            int startingFormWidth, 
            int startingFormHeight,
            AlignmentModeEnum alignmentMode, 
            Size gridSize,
            string formName = null
           ) where TT : Control
        {
            if (!this)
            {
                throw new Exception(@"pDesigner::AddDesignSurface<>() - Исключение: " + "pDesigner" + " не инициализирован! Пожалуйста, установите свойство: IpDesigner::Toolbox перед вызовом любых методов!");
            }
            // Создание области дизайнера (DesignSurface).
            DesignSurfaceExt2 surface = DesignSurfaceManager.CreateDesignSurfaceExt2();

            OneScriptFormsDesigner.dictionaryDesignSurfaceState.Add(surface, "");

            this.DesignSurfaceManager.ActiveDesignSurface = surface;
            // Выбор режима выравнивания.
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
            // Задействуем UndoEngine.
            ((IDesignSurfaceExt)surface).GetUndoEngineExt().Enabled = true;
            // Выбор службы IToolboxService и привязка её к ListBox.
            ToolboxServiceImp tbox = ((IDesignSurfaceExt2)surface).GetIToolboxService() as ToolboxServiceImp;
            // Мы не проверяем, имеет ли Toolbox значение null, поскольку самая первая проверка: if (!this)...
            if (null != tbox)
            {
                tbox.Toolbox = this.Toolbox;
            }
            // Создание корневого (Root) компонента, в случае если это Форма (Form).
            Control rootComponent = null;
            // Приведение к .NET элементу управления, поскольку объект TT имеет ограничение: может быть только .NET "Control".
            rootComponent = surface.CreateRootComponent(typeof(TT), new Size(startingFormWidth, startingFormHeight)) as Control;
            // Переименуем размещаемый компонент, потому что пользователь может добавить более одной формы
            // и каждая новая форма будет называться "Form1", если мы не зададим её имя (Name).
            if (formName != null)
            {
                // Организуем вопрос об имени загружаемой формы.
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

                textBox = new System.Windows.Forms.TextBox();
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

            // Разрешение перетаскивания (Drag&Drop) для RootComponent.
            ((DesignSurfaceExt2)surface).EnableDragandDrop();
            // IComponentChangeService помечена как незаменяемая служба.
            IComponentChangeService componentChangeService = (IComponentChangeService)(surface.GetService(typeof(IComponentChangeService)));
            if (null != componentChangeService)
            {
                // Тип "ComponentEventHandler Delegate" представляет метод который будет обрабатывать ComponentAdding, 
                // ComponentAdded, ComponentRemoving, и ComponentRemoved события, возникшие как события уровня компонента.
                componentChangeService.ComponentChanged += (Object sender, ComponentChangedEventArgs e) =>
                {
                    dynamic OriginalObj = e.Component;
                    if (OriginalObj.GetType().ToString() == "System.Windows.Forms.TabPage")
                    {
                        dynamic SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(OriginalObj);
                        if (SimilarObj == null)
                        {
                            SimilarObj = new osfDesigner.TabPage();
                            OneScriptFormsDesigner.PassProperties(OriginalObj, SimilarObj); // Передадим свойства.
                            SimilarObj.OriginalObj = OriginalObj;
                            OriginalObj.Tag = SimilarObj;
                            OneScriptFormsDesigner.AddToDictionary(OriginalObj, SimilarObj);
                        }
                        DesignSurfaceManager.PropertyGridHost.PropertyGrid.SelectedObject = SimilarObj;
                    }
                    else
                    {
                        DesignSurfaceManager.PropertyGridHost.PropertyGrid.SelectedObject = e.Component;
                    }

                    PropertyGridHost.ReloadTreeView();
                    PropertyGridHost.ChangeSelectNode((Component)e.Component);

                    OneScriptFormsDesigner.SetDesignSurfaceState();
                };
                componentChangeService.ComponentAdded += (Object sender, ComponentEventArgs e) =>
                {
                    dynamic OriginalObj = e.Component;
                    if (OriginalObj.GetType().ToString() == "System.Windows.Forms.TabPage")
                    {
                        osfDesigner.TabPage SimilarObj = new osfDesigner.TabPage();
                        OneScriptFormsDesigner.PassProperties(OriginalObj, SimilarObj); // Передадим свойства.
                        SimilarObj.OriginalObj = OriginalObj;
                        OriginalObj.Tag = SimilarObj;
                        OneScriptFormsDesigner.AddToDictionary(OriginalObj, SimilarObj);
                    }
	
                    if (OriginalObj.GetType().ToString() == "osfDesigner.ToolBar")
                    {
                        // Избежим некорректного сохранения-восстановления панели инструментов.
                        osfDesigner.ToolBar toolBar = (osfDesigner.ToolBar)OriginalObj;
                        toolBar.ButtonSize = new Size(toolBar.ButtonSize.Width, toolBar.ButtonSize.Height);
                    }

                    if (OriginalObj.GetType().ToString() == "System.Windows.Forms.ImageList" || 
                    OriginalObj.GetType().ToString() == "osfDesigner.TreeView" || 
                    OriginalObj.GetType().ToString() == "osfDesigner.DataGrid" || 
                    OriginalObj.GetType().ToString() == "osfDesigner.RichTextBox")
                    {
                        IDesignerHost designerHost = DSME.ActiveDesignSurface.GetIDesignerHost();
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
                        IDesignerHost designerHost = DSME.ActiveDesignSurface.GetIDesignerHost();
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
                    if (OriginalObj.GetType().ToString() == "System.Windows.Forms.TabPage" ||
                        OriginalObj.GetType().ToString() == "System.Windows.Forms.ImageList" ||
                        OriginalObj.GetType().ToString() == "System.Windows.Forms.MainMenu")
                    {
                        dynamic SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(OriginalObj);
                        DesignSurfaceManager.PropertyGridHost.PropertyGrid.SelectedObject = SimilarObj;
                        SimilarObj.DefaultValues = OneScriptFormsDesigner.GetDefaultValues(SimilarObj, DesignSurfaceManager.PropertyGridHost.PropertyGrid);
                    }
                    else
                    {
                        DesignSurfaceManager.PropertyGridHost.PropertyGrid.SelectedObject = OriginalObj;
                        OriginalObj.DefaultValues = OneScriptFormsDesigner.GetDefaultValues(OriginalObj, DesignSurfaceManager.PropertyGridHost.PropertyGrid);
                    }

                    PropertyGridHost.ReloadTreeView();
                    PropertyGridHost.ChangeSelectNode((Component)e.Component);

                    OneScriptFormsDesigner.SetDesignSurfaceState();
                };

                componentChangeService.ComponentRemoving += (Object sender, ComponentEventArgs e) =>
                {
                };

                componentChangeService.ComponentRemoved += (Object sender, ComponentEventArgs e) =>
                {
                    DesignSurfaceManager.UpdatePropertyGridHost(surface);
                    PropertyGridHost.ReloadTreeView();

                    OneScriptFormsDesigner.SetDesignSurfaceState();
                };
            }
            // Теперь установим свойство "Форма.Текст", потому что это будет пустая строка если мы не установим его.
            Control view = surface.GetView();
            if (null == view)
            {
                return null;
            }
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(view);
            // Установим значение свойства через PropertyDescriptor для конкретного свойства.
            PropertyDescriptor pdS = pdc.Find("Text", false);
            if (null != pdS)
            {
                pdS.SetValue(rootComponent, rootComponent.Site.Name);
            }
            // Отобразим область дизайнера (DesignSurface).
            System.Windows.Forms.TabPage newPage = new System.Windows.Forms.TabPage();
            OneScriptFormsDesigner.dictionaryTabPageChanged.Add(newPage, false);
            string sTabPageText = OneScriptFormsDesigner.RevertDesignerTabName(rootComponent.Site.Name);
            // Свяжем rootComponent и создаваемую для него вкладку дризайнера, чтобы потом при удалении формы корректно удалить и вкладку.
            OneScriptFormsDesigner.AddToDictionaryDesignerTabRootComponent(rootComponent, newPage);
            newPage.Text = "     " + sTabPageText + "       ";
            newPage.Name = sTabPageText;
            newPage.SuspendLayout();
            view.Dock = System.Windows.Forms.DockStyle.Fill;
            view.Parent = newPage;
            tbCtrlpDesigner.TabPages.Add(newPage);
            newPage.ResumeLayout();
            // Выберите созданную вкладку (TabPage).
            tbCtrlpDesigner.SelectedIndex = tbCtrlpDesigner.TabPages.Count - 1;
            this.PropertyGridHost.ReloadComboBox();

            // Получим начальные значения свойств формы, они нужны для создания скрипта.
            ((dynamic)rootComponent).DefaultValues = OneScriptFormsDesigner.GetDefaultValues(rootComponent, DesignSurfaceManager.PropertyGridHost.PropertyGrid);

            //* 18.12.2021 perfolenta 
            surface.Dirty = false;
            //***

            // Запомним начальное состояние дизайнера с этой формой.
            OneScriptFormsDesigner.DesignSurfaceState(true);
            OneScriptFormsDesigner.dictionaryTabPageChanged[pDesigner.TabControl.SelectedTab] = true;

            // Возвратим созданную область дизайнера.
            return surface;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            INameCreationService iName = DSME.ActiveDesignSurface.GetIDesignerHost().GetService(typeof(INameCreationService)) as INameCreationService;
            if (!iName.IsValidName(textBox.Text))
            {
                System.Windows.Forms.MessageBox.Show("Имя не должно быть пустым, должно начинаться с буквы, не должно содержать пробелы.");
                textBox.Focus();
                return;
            }
            else
            {
                form.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        public void RemoveDesignSurface(DesignSurfaceExt2 surfaceToErase)
        {
            try
            {
                // Удалить вкладку связанную через hashtableDesignerTabRootComponent с удаляемым корневым компонентом (RootComponent).
                // Примечание:
                //     DesignSurfaceManager продолжает ссылаться на удаленную поверхность DesignSurface, потому что Designsurface продолжает
                //     существовать, но он больше не доступен (не достижим). Этот факт будет полезен при создании новых имен для только
                //     что созданных поверхностей Designsurfaces, чтобы избежать конфликта имен.
                System.Windows.Forms.TabPage TabPage1 = OneScriptFormsDesigner.RevertDesignerTab(surfaceToErase.GetIDesignerHost().RootComponent);
                System.Windows.Forms.TabPage tpToRemove = null;
                foreach (System.Windows.Forms.TabPage tp in tbCtrlpDesigner.TabPages)
                {
                    if (tp.Equals(TabPage1))
                    {
                        tpToRemove = tp;
                        break;
                    }
                }
                if (null != tpToRemove)
                {
                    tbCtrlpDesigner.TabPages.Remove(tpToRemove);
                }

                // Теперь удалите поверхность дизайнера.
                this.DesignSurfaceManager.DeleteDesignSurfaceExt2(surfaceToErase);

                // Наконец, диспетчер DesignSurfaceManager удалит поверхность DesignSurface и установит в качестве активной 
                // поверхности дизайна последнюю, поэтому мы устанавливаем в качестве активной последнюю страницу вкладки.
                tbCtrlpDesigner.SelectedIndex = tbCtrlpDesigner.TabPages.Count - 1;
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
                    SplitContainer SplitContainer1 = (SplitContainer)this.ActiveControl;
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
