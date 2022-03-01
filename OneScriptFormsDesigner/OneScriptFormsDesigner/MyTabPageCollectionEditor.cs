using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class MyTabPageCollectionEditor : CollectionEditor
    {
        private CollectionForm collectionForm;
        private System.Windows.Forms.Form frmCollectionEditorForm;
        private TableLayoutPanel TableLayoutPanel1;
        private TableLayoutPanel AddRemoveTableLayoutPanel1;
        private System.Windows.Forms.Label PropertiesLabel1 = null;
        private System.Windows.Forms.Label MembersLabel1 = null;
        private System.Windows.Forms.ListBox ListBox1;
        private System.Windows.Forms.PropertyGrid PropertyGrid1;
        private TableLayoutPanel OkCancelTableLayoutPanel1;
        private System.Windows.Forms.Button ButtonOk1 = null;
        private System.Windows.Forms.Button ButtonCancel1 = null;
        private System.Windows.Forms.Button ButtonAdd1 = null;
        private System.Windows.Forms.Button ButtonRemove1 = null;
        private System.Windows.Forms.Button ButtonUp1 = null;
        private System.Windows.Forms.Button ButtonDown1 = null;
        private System.Windows.Forms.PropertyGrid TopLevelPropertyGrid1;
        private osfDesigner.TabControl TabControl1;

        public MyTabPageCollectionEditor(Type type) : base(type)
        {
        }

        protected override CollectionForm CreateCollectionForm()
        {
            collectionForm = base.CreateCollectionForm();
            TabControl1 = (osfDesigner.TabControl)Context.Instance;
            collectionForm.Text = "Редактор коллекции Вкладки";
            collectionForm.Shown += CollectionForm_Shown;
            collectionForm.FormClosed += delegate (object sender, FormClosedEventArgs e)
            {
                OneScriptFormsDesigner.block1 = false;
                OneScriptFormsDesigner.SetDesignSurfaceState();
                TopLevelPropertyGrid1.SelectedObject = TabControl1;
            };

            frmCollectionEditorForm = collectionForm;
            TableLayoutPanel1 = (TableLayoutPanel)frmCollectionEditorForm.Controls[0];
            if (TableLayoutPanel1 != null)
            {
                for (int i = 0; i < TableLayoutPanel1.Controls.Count; i++)
                {
                    if (i == 0)
                    {
                        ButtonDown1 = (System.Windows.Forms.Button)TableLayoutPanel1.Controls[0];
                        ButtonDown1.Click += ButtonDown1_Click;
                    }
                    if (i == 1)
                    {
                        AddRemoveTableLayoutPanel1 = (TableLayoutPanel)TableLayoutPanel1.Controls[1];
                    }
                    if (i == 2)
                    {
                        PropertiesLabel1 = (System.Windows.Forms.Label)TableLayoutPanel1.Controls[2];
                        PropertiesLabel1.Text = "Свойства:";
                    }
                    if (i == 3)
                    {
                        MembersLabel1 = (System.Windows.Forms.Label)TableLayoutPanel1.Controls[3];
                        MembersLabel1.Text = "Члены:";
                    }
                    if (i == 4)
                    {
                        ListBox1 = (System.Windows.Forms.ListBox)TableLayoutPanel1.Controls[4];
                        ListBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
                    }
                    if (i == 5)
                    {
                        PropertyGrid1 = (System.Windows.Forms.PropertyGrid)TableLayoutPanel1.Controls[5];
                        PropertyGrid1.SelectedGridItemChanged += PropertyGrid1_SelectedGridItemChanged;
                        PropertyGrid1.SelectedObjectsChanged += PropertyGrid_SelectedObjectsChanged;
                        PropertyGrid1.PropertyValueChanged += PropertyGrid1_PropertyValueChanged;
                        PropertyGrid1.HelpVisible = true;
                        PropertyGrid1.HelpBackColor = SystemColors.Info;
                    }
                    if (i == 6)
                    {
                        OkCancelTableLayoutPanel1 = (TableLayoutPanel)TableLayoutPanel1.Controls[6];
                    }
                    if (i == 7)
                    {
                        ButtonUp1 = (System.Windows.Forms.Button)TableLayoutPanel1.Controls[7];
                        ButtonUp1.Click += ButtonUp1_Click;
                    }
                }
            }
            if (AddRemoveTableLayoutPanel1 != null)
            {
                for (int i = 0; i < AddRemoveTableLayoutPanel1.Controls.Count; i++)
                {
                    if (i == 0)
                    {
                        ButtonAdd1 = (System.Windows.Forms.Button)AddRemoveTableLayoutPanel1.Controls[0];
                        ButtonAdd1.Click += ButtonAdd1_Click;
                        ButtonAdd1.Text = "Добавить";
                    }
                    if (i == 1)
                    {
                        ButtonRemove1 = (System.Windows.Forms.Button)AddRemoveTableLayoutPanel1.Controls[1];
                        ButtonRemove1.Click += ButtonRemove1_Click;
                        ButtonRemove1.Text = "Удалить";
                    }
                }
            }
            if (OkCancelTableLayoutPanel1 != null)
            {
                for (int i = 0; i < OkCancelTableLayoutPanel1.Controls.Count; i++)
                {
                    if (i == 0)
                    {
                        ButtonOk1 = (System.Windows.Forms.Button)OkCancelTableLayoutPanel1.Controls[0];
                        ButtonOk1.Text = "ОК";
                    }
                    if (i == 1)
                    {
                        ButtonCancel1 = (System.Windows.Forms.Button)OkCancelTableLayoutPanel1.Controls[1];
                        ButtonCancel1.Text = "Отмена";
                    }
                }
            }
            return collectionForm;
        }

        private void CollectionForm_Shown(object sender, EventArgs e)
        {
            TopLevelPropertyGrid1 = OneScriptFormsDesigner.PropertyGrid;
            PropertiesLabel1.Text = "Свойства:";
            OneScriptFormsDesigner.block1 = true;
        }

        private void PropertyGrid1_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            if (PropertyGrid1.SelectedGridItem.Label == "(Name)")
            {
                MessageBox.Show(
                    "Для правильного формирования файла сценария не допускается изменять имя компонента.",
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1
                    );
                PropertyDescriptor pd = TypeDescriptor.GetProperties(((dynamic)PropertyGrid1.SelectedObject).M_TabPage)["Name"];
                pd.SetValue(((dynamic)PropertyGrid1.SelectedObject).M_TabPage, (string)e.OldValue);
            }
            UpdateListBox1();
        }

        private void UpdateListBox1()
        {
            int index = ListBox1.SelectedIndex;
            TabControl1.TabPages.Clear();
            MethodInfo MethodInfo3 = collectionForm.GetType().GetMethod("AddItems", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo3.Invoke(collectionForm, new object[] { TabControl1.TabPages });
            ListBox1.SelectedIndex = index;
            if (index != ListBox1.Items.Count - 1)
            {
                ListBox1.SetSelected(ListBox1.Items.Count - 1, false);
            }
            TabControl1.SelectedIndex = index;
            collectionForm.Refresh();
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl1.SelectedIndex = ListBox1.SelectedIndex;
        }

        private void ButtonRemove1_Click(object sender, EventArgs e)
        {
            UpdateListBox1();
        }

        private void ButtonUp1_Click(object sender, EventArgs e)
        {
            UpdateListBox1();
        }

        private void ButtonDown1_Click(object sender, EventArgs e)
        {
            UpdateListBox1();
        }

        private void PropertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            PropertiesLabel1.Text = "Свойства:";
        }

        private void PropertyGrid_SelectedObjectsChanged(object sender, EventArgs e)
        {
            int index = ListBox1.SelectedIndex;
            System.Windows.Forms.TabPage OriginalObj = TabControl1.TabPages[index];
            PropertyGrid1.SelectedObject = OneScriptFormsDesigner.RevertSimilarObj(OriginalObj);
        }

        private void ButtonAdd1_Click(object sender, EventArgs e)
        {
            int index = ListBox1.SelectedIndex;
            System.Windows.Forms.TabPage OriginalObj = TabControl1.TabPages[index];
            OriginalObj.Text = OriginalObj.Name;
        }
    }
}
