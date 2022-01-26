using System;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Reflection;

namespace osfDesigner
{
    public class MyTabPageCollectionEditor : CollectionEditor
    {
        private System.ComponentModel.Design.CollectionEditor.CollectionForm collectionForm;
        private System.Windows.Forms.Form frmCollectionEditorForm;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel AddRemoveTableLayoutPanel1;
        private System.Windows.Forms.Label PropertiesLabel1 = null;
        private System.Windows.Forms.Label MembersLabel1 = null;
        private System.Windows.Forms.ListBox ListBox1;
        private System.Windows.Forms.PropertyGrid PropertyGrid1;
        private System.Windows.Forms.TableLayoutPanel OkCancelTableLayoutPanel1;
        private System.Windows.Forms.Button ButtonOk1 = null;
        private System.Windows.Forms.Button ButtonCancel1 = null;
        private System.Windows.Forms.Button ButtonAdd1 = null;
        private System.Windows.Forms.Button ButtonRemove1 = null;
        private System.Windows.Forms.Button ButtonUp1 = null;
        private System.Windows.Forms.Button ButtonDown1 = null;
        private System.Windows.Forms.PropertyGrid TopLevelPropertyGrid1;
        private osfDesigner.TabControl TabControl1;

        // Определите статическое событие, чтобы отобразить внутреннюю сетку свойств
        public delegate void MyPropertyValueChangedEventHandler(object sender, PropertyValueChangedEventArgs e);

        // Унаследуйте конструктор по умолчанию из стандартного редактора коллекций...
        public MyTabPageCollectionEditor(Type type) : base(type)
        {
        }

        // Переопределите этот метод, чтобы получить доступ к форме редактора коллекции. 
        protected override CollectionForm CreateCollectionForm()
        {
            // Получение макета редактора коллекции по умолчанию...
            collectionForm = base.CreateCollectionForm();
            TabControl1 = (osfDesigner.TabControl)this.Context.Instance;
            collectionForm.Text = "Редактор коллекции Вкладки";

            collectionForm.Shown += delegate (object sender, EventArgs e)
            {
                TopLevelPropertyGrid1 = pDesigner.DSME.PropertyGridHost.PropertyGrid;
                PropertiesLabel1.Text = "Свойства:";
                OneScriptFormsDesigner.block1 = true;
            };

            collectionForm.FormClosed += delegate (object sender, FormClosedEventArgs e)
            {
                OneScriptFormsDesigner.block1 = false;
                TopLevelPropertyGrid1.SelectedObject = TabControl1;
            };

            frmCollectionEditorForm = (System.Windows.Forms.Form)collectionForm;
            TableLayoutPanel1 = (System.Windows.Forms.TableLayoutPanel)frmCollectionEditorForm.Controls[0];
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
                        AddRemoveTableLayoutPanel1 = (System.Windows.Forms.TableLayoutPanel)TableLayoutPanel1.Controls[1];
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
                    // Получите ссылку на внутреннюю сетку свойств и подключите к ней обработчик событий.
                    if (i == 5)
                    {
                        PropertyGrid1 = (System.Windows.Forms.PropertyGrid)TableLayoutPanel1.Controls[5];
                        PropertyGrid1.SelectedGridItemChanged += PropertyGrid1_SelectedGridItemChanged;
                        PropertyGrid1.SelectedObjectsChanged += PropertyGrid_SelectedObjectsChanged;
                        PropertyGrid1.PropertyValueChanged += PropertyGrid1_PropertyValueChanged;

                        // также сделать доступным окно с подсказками по параметрам в нижней части 
                        PropertyGrid1.HelpVisible = true;
                        PropertyGrid1.HelpBackColor = System.Drawing.SystemColors.Info;
                    }
                    if (i == 6)
                    {
                        OkCancelTableLayoutPanel1 = (System.Windows.Forms.TableLayoutPanel)TableLayoutPanel1.Controls[6];
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
	
        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (PropertyGrid1.SelectedGridItem.Label == "(Name)")
            {
                System.Windows.Forms.MessageBox.Show(
                    "Для правильного формирования файла сценария не допускается изменять имя компонента.",
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1
                    );
                System.ComponentModel.PropertyDescriptor pd = System.ComponentModel.TypeDescriptor.GetProperties(((dynamic)PropertyGrid1.SelectedObject).M_TabPage)["Name"];
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
            PropertyGrid1.SelectedObject = osfDesigner.OneScriptFormsDesigner.RevertSimilarObj(OriginalObj);
        }

        private void ButtonAdd1_Click(object sender, EventArgs e)
        {
            int index = ListBox1.SelectedIndex;
            System.Windows.Forms.TabPage OriginalObj = TabControl1.TabPages[index];
            OriginalObj.Text = OriginalObj.Name;
        }
    }
}
