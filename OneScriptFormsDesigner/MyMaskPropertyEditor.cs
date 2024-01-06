using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class MyMaskPropertyEditor : UITypeEditor
    {
        System.Windows.Forms.Form form;

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Assembly assembly = Assembly.Load("System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
            System.Type type = assembly.GetType("System.Windows.Forms.Design.MaskDesignerDialog");
            ConstructorInfo ci = type.GetConstructor(new System.Type[] { typeof(System.Windows.Forms.MaskedTextBox), typeof(System.ComponentModel.Design.IHelpService) });
            form = ci.Invoke(new object[] { context.Instance, null }) as System.Windows.Forms.Form;
            form.Text = "Маска ввода";
            form.HelpButton = false;
            form.Shown += Form_Shown;
            System.Windows.Forms.TableLayoutPanel tPanel1 = (System.Windows.Forms.TableLayoutPanel)form.Controls[0];
            System.Windows.Forms.TableLayoutPanel tPanel2 = (System.Windows.Forms.TableLayoutPanel)tPanel1.Controls[0];
            System.Windows.Forms.TableLayoutPanel tPanel3 = (System.Windows.Forms.TableLayoutPanel)tPanel1.Controls[1];
            System.Windows.Forms.Label label1 = (System.Windows.Forms.Label)tPanel1.Controls[2];
            System.Windows.Forms.ListView listView1 = (System.Windows.Forms.ListView)tPanel1.Controls[3];
            listView1.ColumnClick += ListView1_ColumnClick;
            label1.Text = "Выберите готовое описание маски из списка ниже или задайте специальную маску, выбрав команду " + "\u0022Специальный" + "\u0022.";
            System.Windows.Forms.CheckBox checkBox1 = (System.Windows.Forms.CheckBox)tPanel2.Controls[0];
            checkBox1.Text = "Использовать ValidatingType";
            checkBox1.Visible = false;
            System.Windows.Forms.Label label2 = (System.Windows.Forms.Label)tPanel2.Controls[1];
            label2.Text = "Маска:";
            System.Windows.Forms.Label label3 = (System.Windows.Forms.Label)tPanel2.Controls[3];
            label3.Text = "Предварительный просмотр:";
            listView1.Columns[0].Text = "Описание маски";
            listView1.Columns[1].Text = "Формат данных";
            listView1.Columns[2].Text = "Проверка типа";
            System.Windows.Forms.Button button1 = (System.Windows.Forms.Button)tPanel3.Controls[0];
            button1.Text = "Отмена";
            System.Windows.Forms.Button button2 = (System.Windows.Forms.Button)tPanel3.Controls[1];
            button2.Text = "ОК";

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PropertyInfo pi = type.GetProperty("Mask");
                ((MaskedTextBox)context.Instance).Mask = pi.GetValue(form, null) as string;
            }

            return base.EditValue(context, provider, value);
        }

        private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            System.Windows.Forms.TableLayoutPanel tPanel1 = (System.Windows.Forms.TableLayoutPanel)form.Controls[0];
            System.Windows.Forms.ListView listView1 = (System.Windows.Forms.ListView)tPanel1.Controls[3];
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                System.Windows.Forms.ListViewItem s = listView1.Items[i];
                if (s.SubItems[0].Text.Contains("Custom"))
                {
                    s.SubItems[0].Text = "\u0022Специальный\u0022";
                }
                else if (s.SubItems[0].Text == "Numeric (5-digits)")
                {
                    s.SubItems[0].Text = "Числовой (5 цифр)";
                }
                else if (s.SubItems[0].Text == "Phone number")
                {
                    s.SubItems[0].Text = "Номер телефона";
                }
                else if (s.SubItems[0].Text == "Phone number no area code")
                {
                    s.SubItems[0].Text = "Номер телефона без кода города";
                }
                else if (s.SubItems[0].Text == "Short date")
                {
                    s.SubItems[0].Text = "Короткая дата";
                }
                else if (s.SubItems[0].Text == "Short date and time (US)")
                {
                    s.SubItems[0].Text = "Короткая дата и время (US)";
                }
                else if (s.SubItems[0].Text == "Social security number")
                {
                    s.SubItems[0].Text = "Номер социального страхования";
                }
                else if (s.SubItems[0].Text == "Time (European/Military)")
                {
                    s.SubItems[0].Text = "Время (Европейский/Военный)";
                }
                else if (s.SubItems[0].Text == "Time (US)")
                {
                    s.SubItems[0].Text = "Время (US)";
                }
                else if (s.SubItems[0].Text == "Zip Code")
                {
                    s.SubItems[0].Text = "Zip код";
                }
            }
            listView1.Sort();
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            System.Windows.Forms.TableLayoutPanel tPanel1 = (System.Windows.Forms.TableLayoutPanel)form.Controls[0];
            System.Windows.Forms.ListView listView1 = (System.Windows.Forms.ListView)tPanel1.Controls[3];

            listView1.Items[0].SubItems[0].Text = "Числовой (5 цифр)";
            listView1.Items[1].SubItems[0].Text = "Номер телефона";
            listView1.Items[2].SubItems[0].Text = "Номер телефона без кода города";
            listView1.Items[3].SubItems[0].Text = "Короткая дата";
            listView1.Items[4].SubItems[0].Text = "Короткая дата и время (US)";
            listView1.Items[5].SubItems[0].Text = "Номер социального страхования";
            listView1.Items[6].SubItems[0].Text = "Время (Европейский/Военный)";
            listView1.Items[7].SubItems[0].Text = "Время (US)";
            listView1.Items[8].SubItems[0].Text = "Zip код";
            listView1.Items[9].SubItems[0].Text = "\u0022Специальный\u0022";
        }
    }
}
