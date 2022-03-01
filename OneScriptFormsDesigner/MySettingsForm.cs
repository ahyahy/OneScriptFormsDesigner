using osfDesigner.Properties;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class MySettingsForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TreeView setTreeView;
        private TreeNode filesNode;
        private System.Windows.Forms.Panel setPanel;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label_os;
        private System.Windows.Forms.Label label_dll;
        private System.Windows.Forms.TextBox textBox_osPath;
        private System.Windows.Forms.TextBox textBox_dllPath;
        private System.Windows.Forms.Button button_osPath;
        private System.Windows.Forms.Button button_dllPath;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;

        public MySettingsForm()
        {
            Text = "Параметры";
            Width = 800;
            Height = 500;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Shown += SettingsForm_Shown;

            string str_settingsForm = "AAABAAEAMjIAAAEAIADIKAAAFgAAACgAAAAyAAAAZAAAAAEAIеAoCgеееAAAAADцццццццццццццццццццццццццццццццццццццццццг/////AAAACAAAAHUAAAC4AAAAxAAAAI4AAAAcццг/wAAAAkAAADFAкAAOgAAADbAAAA/gAAAOsAAAAzццццццй//AAAAdgAAAP8ункAAO4AAAAzцццццц//8AAAC5AAAA5wAAAP8ункAAO4AAAAzцгййй///wAAAMYAAADYAннкAAO4AAAAzцгйй////AAAAkAAAAP4уннAAA8QAAAO4AAAAzцгй////8AAAAeAAAA7AAAAPEуннAAA8QAAAO4AAAAzцгй////8AAAAxAAAA7QAAAPEуннAAA8QAAAO8AAAA2цгй////8AAAAxAAAA7QAAAPEуннAAA8QAAAO8AAAA2цгй////8AAAAxAAAA7QAAAPEуннAAA8QAAAO8AAAA2цгй////8AAAAxAAAA7QAAAPEуннкAAO8AAAA2цгй////8AAAAxAAAA7QAAAPEуннкAAO8AAAA2цгй////8AAAAxAAAA7QAAAPEуннкAAO8AAAA2цгй////8AAAAxAAAA7QAAAPEуннAAA8QAAAO8AAAA2цгй////8AAAAxAAAA7QAAAP8уннкAAO8AAAA2цгй////8AAAAxAAAA7QAAAP8уннкAAO8AAAA2цгй////8AAAAxAнннкAAO8AAAA2цгй////8AAAAxAнннкAAO8AAAA2цгй////8AAAAzAAAA7gAAAP8уннкAAO4AAABLAAAAawAAAKgAAADDAAAAxAAAAKsAAAB2AAAAJvгй//8AAAAzAAAA7gAAAPEунннAAA+gAAAOEAAADeAAAA+AAAAP8уAAArwAAACbг///8AAAAzAAAA7gAAAPEуннннкAAP8AAAD9AAAA9AAAAFPг///8AAAAzAAAA7gAAAPEуннннкAAP8AAADgAAAA/gAAAFXг///8AAAAzAAAA7gAAAP8уннннкAAP8AAADfAAAA9AAAACnг///8AAAAzAAAA7gAAAPEунннннAAAгй////8AAABLAннннннAAALPг///wAAAGwуннкAAO0уAAA9QAAAOUункAAP8AAADг/////AAAAqAAAAPkуннAAA0йй////wAAANcунAAA8QAAAPг////8AAADDAAAA4AAAAP8унAAA7QAAANbйййй//wAAANQукAAP8AAADWAAAAг/////wAAAMYAAADdAнкAAP8AAADц//wAAANMAAAD+AкAANsAAADг/////AAAArAAAAPYунAAAwgAAAPXцйwAAANUAAAD+AAAA/QAAAPг////8AAAB4AннAAA5fцй/////wAAANQуAAAг/////wAAACgункAAP8AAAD+AAAA1цййwAAANTгйй//wAAALIAAAD8AннAAA1vцццццц//AAAAKQAAAPUAAADdAннAAA1fцццццц//AAAAVgAAAP4уннAAA1fцццццц//AAAAWAAAAPYAAAD7AнкAAP8AAAD+AAAA1Pцццццц//AAAALAAAALcукAAPAAAADUAAAA2wAAAP0уAAA1Pццццццй/wAAAC8ункAAPццццццццццццццццццццццццццццццццццццццццццг//еееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееее==";
            str_settingsForm = str_settingsForm.Replace("г", "ццццйй");
            str_settingsForm = str_settingsForm.Replace("н", "кAAP8у");
            str_settingsForm = str_settingsForm.Replace("е", "AAAAAA");
            str_settingsForm = str_settingsForm.Replace("к", "AAA/wA");
            str_settingsForm = str_settingsForm.Replace("у", "AAAD/A");
            str_settingsForm = str_settingsForm.Replace("ц", "йййййй");
            str_settingsForm = str_settingsForm.Replace("й", "//////");
            Icon = new Icon(new MemoryStream(Convert.FromBase64String(str_settingsForm)));
            //
            //buttonsPanel
            //
            System.Windows.Forms.Panel buttonsPanel = new System.Windows.Forms.Panel();
            buttonsPanel.Parent = this;
            buttonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            buttonsPanel.Height = 45;
            buttonsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            buttonOK = new System.Windows.Forms.Button();
            buttonOK.Parent = buttonsPanel;
            buttonOK.Text = "OK";
            buttonOK.Width = 75;
            buttonOK.Left = buttonsPanel.Width - 170;
            buttonOK.Top = 13;
            buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Right |
                System.Windows.Forms.AnchorStyles.Bottom;
            buttonOK.Click += ButtonOK_Click;

            buttonCancel = new System.Windows.Forms.Button();
            buttonCancel.Parent = buttonsPanel;
            buttonCancel.Text = "Отмена";
            buttonCancel.Left = buttonOK.Right + 10;
            buttonCancel.Top = buttonOK.Top;
            buttonCancel.Width = buttonOK.Width;
            buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Right |
                System.Windows.Forms.AnchorStyles.Bottom;
            buttonCancel.Click += ButtonCancel_Click;
            //
            //setPanel
            //
            setPanel = new System.Windows.Forms.Panel();
            setPanel.Parent = this;
            setPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            setPanel.BackColor = Color.LightSteelBlue;
            //
            //setTreeView
            //
            setTreeView = new System.Windows.Forms.TreeView();
            setTreeView.Parent = setPanel;
            setTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            setTreeView.Width = 200;
            setTreeView.NodeMouseClick += SetTreeView_NodeMouseClick;
            setTreeView.HideSelection = false;
            //
            //filesPanel
            //
            System.Windows.Forms.Panel filesPanel = new System.Windows.Forms.Panel();
            filesPanel.Parent = setPanel;
            filesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            filesPanel.BringToFront();
            filesPanel.BackColor = Color.Gainsboro;
            filesNode = setTreeView.Nodes.Add("Файлы");
            filesNode.Tag = filesPanel;
            filesPanel.Hide();

            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox1.Parent = filesPanel;
            groupBox1.Text = "Путь до файла";
            groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;
            groupBox1.Left = 15;
            groupBox1.Top = 15;
            groupBox1.Width = 550;
            groupBox1.Height = 150;

            label_os = new System.Windows.Forms.Label();
            label_os.Parent = groupBox1;
            label_os.Left = 10;
            label_os.Top = groupBox1.Top;
            label_os.Width = 80;
            label_os.Text = "oscript.exe:";
            label_os.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            textBox_osPath = new System.Windows.Forms.TextBox();
            textBox_osPath.Parent = groupBox1;
            textBox_osPath.Left = 10;
            textBox_osPath.Top = label_os.Bottom + 4;
            textBox_osPath.Width = groupBox1.Width - 57;
            textBox_osPath.Anchor = System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;
            textBox_osPath.Text = (string)Settings.Default["osPath"];

            button_osPath = new System.Windows.Forms.Button();
            button_osPath.Parent = groupBox1;
            button_osPath.Font = new Font(groupBox1.Font, FontStyle.Bold);
            button_osPath.Text = "...";
            button_osPath.Left = textBox_osPath.Width + 17;
            button_osPath.Top = textBox_osPath.Top;
            button_osPath.Width = 27;
            button_osPath.Anchor = System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;
            button_osPath.Click += Button_osPath_Click;

            label_dll = new System.Windows.Forms.Label();
            label_dll.Parent = groupBox1;
            label_dll.Left = textBox_osPath.Left;
            label_dll.Top = textBox_osPath.Bottom + 10;
            label_dll.Width = 150;
            label_dll.Text = "OneScriptForms.dll:";
            label_dll.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            textBox_dllPath = new System.Windows.Forms.TextBox();
            textBox_dllPath.Parent = groupBox1;
            textBox_dllPath.Left = label_dll.Left;
            textBox_dllPath.Top = label_dll.Bottom + 3;
            textBox_dllPath.Width = textBox_osPath.Width;
            textBox_dllPath.Anchor = System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;
            textBox_dllPath.Text = (string)Settings.Default["dllPath"];

            button_dllPath = new System.Windows.Forms.Button();
            button_dllPath.Parent = groupBox1;
            button_dllPath.Font = new Font(groupBox1.Font, FontStyle.Bold);
            button_dllPath.Text = "...";
            button_dllPath.Left = button_osPath.Left;
            button_dllPath.Top = textBox_dllPath.Top;
            button_dllPath.Width = button_osPath.Width;
            button_dllPath.Anchor = System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;
            button_dllPath.Click += Button_dllPath_Click;
            //
            //stylesPanel
            //
            System.Windows.Forms.Panel stylesPanel = new System.Windows.Forms.Panel();
            stylesPanel.Parent = setPanel;
            stylesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            stylesPanel.BringToFront();
            stylesPanel.BackColor = Color.Gainsboro;
            TreeNode stylesNode = setTreeView.Nodes.Add("Стиль сценария");
            stylesNode.Tag = stylesPanel;
            stylesPanel.Hide();

            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox2.Parent = stylesPanel;
            groupBox2.Text = "Стиль формируемого сценария";
            groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;
            groupBox2.Left = 15;
            groupBox2.Top = 15;
            groupBox2.Width = 550;
            groupBox2.Height = 100;

            radioButton1 = new System.Windows.Forms.RadioButton();
            radioButton1.Parent = groupBox2;
            radioButton1.Left = 10;
            radioButton1.Top = 25;
            radioButton1.Width = groupBox2.Width - 20;
            radioButton1.Anchor = System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;
            radioButton1.Text = "Стиль скрипта.";
            radioButton1.Checked = (bool)Settings.Default["styleScript"];

            radioButton2 = new System.Windows.Forms.RadioButton();
            radioButton2.Parent = groupBox2;
            radioButton2.Left = label_dll.Left;
            radioButton2.Top = radioButton1.Bottom + 5;
            radioButton2.Width = radioButton1.Width;
            radioButton2.Anchor = System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;
            radioButton2.Text = "Стиль приложения.";
            radioButton2.Checked = !(bool)Settings.Default["styleScript"];
            //
            //visualStylesPanel
            //
            System.Windows.Forms.Panel visualStylesPanel = new System.Windows.Forms.Panel();
            visualStylesPanel.Parent = setPanel;
            visualStylesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            visualStylesPanel.BringToFront();
            visualStylesPanel.BackColor = Color.Gainsboro;
            TreeNode visualStylesNode = setTreeView.Nodes.Add("Визуальные стили");
            visualStylesNode.Tag = visualStylesPanel;
            stylesPanel.Hide();

            groupBox3 = new System.Windows.Forms.GroupBox();
            groupBox3.Parent = visualStylesPanel;
            groupBox3.Text = "Включить визуальные стили для";
            groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;
            groupBox3.Left = 15;
            groupBox3.Top = 15;
            groupBox3.Width = 550;
            groupBox3.Height = 100;

            checkBox1 = new System.Windows.Forms.CheckBox();
            checkBox1.Parent = groupBox3;
            checkBox1.Left = 10;
            checkBox1.Top = 19;
            checkBox1.Width = groupBox3.Width - 20;
            checkBox1.Height = 45;
            checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;
            checkBox1.Text = "Включить визуальные стили для дизайнера. ( Требуется перезапуск дизайнера! )";
            checkBox1.Checked = (bool)Settings.Default["visualSyleDesigner"];

             checkBox2 = new System.Windows.Forms.CheckBox();
            checkBox2.Parent = groupBox3;
            checkBox2.Left = 10;
            checkBox2.Top = radioButton1.Bottom + 15;
            checkBox2.Width = groupBox3.Width - 20;
            checkBox2.Anchor = System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;
            checkBox2.Text = "Включить визуальные стили для форм.";
            checkBox2.Checked = (bool)Settings.Default["visualSyleForms"];

            SetPanelVisible(setTreeView, filesNode);
        }

        public bool SyleDesigner
        {
            get { return checkBox1.Checked; }
        }

        public bool SyleForms
        {
            get { return checkBox2.Checked; }
        }

        public string OSPath
        {
            get { return textBox_osPath.Text; }
        }

        public string DLLPath
        {
            get { return textBox_dllPath.Text; }
        }

        public bool StyleScript
        {
            get { return radioButton1.Checked; }
        }

        private void Button_dllPath_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            OpenFileDialog1.InitialDirectory = "C:\\";
            OpenFileDialog1.Filter = "DLL files (*.dll)|*.dll|All files (*.*)|*.*";
            OpenFileDialog1.FilterIndex = 1;
            OpenFileDialog1.RestoreDirectory = true;
            OpenFileDialog1.Multiselect = false;
            OpenFileDialog1.SupportMultiDottedExtensions = true;

            if (OpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            textBox_dllPath.Text = OpenFileDialog1.FileName;
        }

        private void Button_osPath_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            OpenFileDialog1.InitialDirectory = "C:\\";
            OpenFileDialog1.Filter = "EXE files (*.exe)|*.exe|All files (*.*)|*.*";
            OpenFileDialog1.FilterIndex = 1;
            OpenFileDialog1.RestoreDirectory = true;
            OpenFileDialog1.Multiselect = false;
            OpenFileDialog1.SupportMultiDottedExtensions = true;

            if (OpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            textBox_osPath.Text = OpenFileDialog1.FileName;
        }

        private void SetTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SetPanelVisible(e.Node.TreeView, e.Node);
        }

        private static void SetPanelVisible(dynamic treeView_treeNode, TreeNode node)
        {
            for (int i = 0; i < treeView_treeNode.Nodes.Count; i++)
            {
                TreeNode TreeNode1 = treeView_treeNode.Nodes[i];
                (TreeNode1.Tag as System.Windows.Forms.Panel).Visible = false;
                if (TreeNode1.Equals(node))
                {
                    (TreeNode1.Tag as System.Windows.Forms.Panel).Visible = true;
                }

                if (TreeNode1.Nodes.Count > 0)
                {
                    SetPanelVisible(treeView_treeNode, node);
                }
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void SettingsForm_Shown(object sender, EventArgs e)
        {
            setTreeView.SelectedNode = filesNode;
            setTreeView.Focus();
        }
    }
}
