using osfDesigner.Properties;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System;

namespace osfDesigner
{
    public class pDesignerMainFormPFL : System.Windows.Forms.Form, IDesignerMainForm
    {
        private string _version = string.Empty;
        public pDesigner pDesignerCore = new pDesigner();
        private IpDesigner IpDesignerCore = null;
        private IContainer components = null;
        private MenuStrip menuStrip1;

        private ToolStripMenuItem _file;
        private ToolStripMenuItem _generateScript;

        private ToolStripSeparator _stripSeparator2;
        private ToolStripMenuItem _loadForm;
        private ToolStripMenuItem _saveForm;
        private ToolStripSeparator _stripSeparator4;
        private ToolStripMenuItem _exit;

        private ToolStripMenuItem _edit;
        private ToolStripMenuItem _unDo;
        private ToolStripMenuItem _reDo;
        private ToolStripSeparator _stripSeparator3;
        private ToolStripMenuItem _cut;
        private ToolStripMenuItem _copy;
        private ToolStripMenuItem _paste;
        private ToolStripMenuItem _delete;

        private ToolStripMenuItem _view;
        private ToolStripMenuItem _form;
        private ToolStripMenuItem _code;

        private ToolStripMenuItem _tools;
        private ToolStripMenuItem _tabOrder;
        private static ToolStripMenuItem _tabOrder1;

        private ToolStripSeparator _stripSeparator5;
        private ToolStripMenuItem _run;

        private ToolStripSeparator _stripSeparator6;
        private ToolStripMenuItem _settings;

        private ToolStripMenuItem _help;
        private ToolStripMenuItem _about;

        private System.Windows.Forms.Panel pnl4Toolbox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel pnl4pDesigner;
        private System.Windows.Forms.Splitter pnl4splitter;

        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Timer timerLoad;

        private System.Windows.Forms.Form settingsForm;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label label_os;
        private System.Windows.Forms.Label label_dll;
        private System.Windows.Forms.TextBox textBox_osPath;
        private System.Windows.Forms.TextBox textBox_dllPath;
        private System.Windows.Forms.Button button_osPath;
        private System.Windows.Forms.Button button_dllPath;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;

        private void timerLoad_Tick(object sender, EventArgs e)
        {
            timerLoad.Stop();
            try
            {
                // Через try, потому что, при загрузке, элемент управления не завершил создание самого себя.
                Application.AddMessageFilter(new PropertyGridMessageFilter(propertyGrid1.GetChildAtPoint(new Point(40, 40)), new MouseEventHandler(propGridView_MouseUp)));
            }
            catch { }
        }

        private void propGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && (
                propertyGrid1.SelectedGridItem.Label == "СписокИзображений" ||
                propertyGrid1.SelectedGridItem.Label == "СписокБольшихИзображений" ||
                propertyGrid1.SelectedGridItem.Label == "СписокМаленькихИзображений"))
            {
                // Пользователь щелкнул левой кнопкой мыши по свойству.
                try
                {
                    propertyGrid1.SelectedGridItem.Expanded = false;
                }
                catch { }
            }
        }

        public pDesignerMainFormPFL()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(pDesignerMainFormPFL));
            propertyGrid1 = OneScriptFormsDesigner.PropertyGrid;
            menuStrip1 = new MenuStrip();
            _file = new ToolStripMenuItem();
            _generateScript = new ToolStripMenuItem();
            _stripSeparator2 = new ToolStripSeparator();
            _loadForm = new ToolStripMenuItem();
            _saveForm = new ToolStripMenuItem();
            _stripSeparator4 = new ToolStripSeparator();
            _exit = new ToolStripMenuItem();

            _edit = new ToolStripMenuItem();
            _unDo = new ToolStripMenuItem();
            _reDo = new ToolStripMenuItem();
            _stripSeparator3 = new ToolStripSeparator();
            _cut = new ToolStripMenuItem();
            _copy = new ToolStripMenuItem();
            _paste = new ToolStripMenuItem();
            _delete = new ToolStripMenuItem();

            _view = new ToolStripMenuItem();
            _form = new ToolStripMenuItem();
            _code = new ToolStripMenuItem();

            _tools = new ToolStripMenuItem();
            _tabOrder = new ToolStripMenuItem();
            osfDesigner.pDesignerMainFormPFL._tabOrder1 = _tabOrder;

            _stripSeparator5 = new ToolStripSeparator();
            _run = new ToolStripMenuItem();

            _stripSeparator6 = new ToolStripSeparator();
            _settings = new ToolStripMenuItem();

            _help = new ToolStripMenuItem();
            _about = new ToolStripMenuItem();
            pnl4Toolbox = new System.Windows.Forms.Panel();
            listBox1 = new System.Windows.Forms.ListBox();
            pnl4pDesigner = new System.Windows.Forms.Panel();
            pnl4splitter = new System.Windows.Forms.Splitter();
            menuStrip1.SuspendLayout();
            pnl4Toolbox.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] {
            _file,
            _edit,
            _view,
            _tools,
            _help});
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Padding = new Padding(8, 2, 0, 2);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // _file
            // 
            _file.DropDownItems.AddRange(new ToolStripItem[] {
            _generateScript,
            _stripSeparator2,
            _loadForm,
            _saveForm,
            _stripSeparator4,
            _exit});
            _file.Name = "_file";
            _file.Size = new Size(54, 24);
            _file.Text = "Файл";
            // 
            // _generateScript
            // 
            string str_generateScript = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABzklEQVR42u2aMU7DMBSGn+NCGRjgDjAxInEIxMgtWBiYGEoAqQsgWLgFEyBAcAMkkBhYgDtQRJFomzTYQBzjkiZO4jiO/A+VWjkv/xf7vby4QRApALOE2IeB5hkE4s0f3fZQjmClaX2pyTwzAFPMixAWwAKoAjg+f6lUZVpbmfvjTxqg++mPBJ2ewvUBEE+gWoUvoeedBURiVAcgQ0CwAKoAqLnfMZA0hspV1FvNcB6VzAAPcHdxEju26c9CD79KxV9cXpUDkElimsD89ySALKIAbtQ9Q+i7EABaQvll1iFX5+n6Bt69NwtQOgAVOVHwcHYFHu7qA+DFm+PFVyWZJC4dII1qB1CpJSQDECax9hnIW4WMBaByq5ADFkA3gKwqlwNGA8TdedPK1b2EagEw33qMTWKx/68lwP3pJQSNDz0AVOPKaNIeUOWfB0SADrdfX4T+W35KyygFODhsgz8cst+8YEACp+NCnK/NjW09ALt7LQLgZzp+EjcsQG6A9v4W9H3PAmgFAHAIRF/6eOw4gJEzFkJ5MxdB/Eh2NiYwjq1G4bai0OIU/x9Z0feDUOGeqHKAvE2gpGoGUAREGQD0gUl81YDRGCg0UpYM0rf3L8g8IyNDfeOmAAAAAElFTkSuQmCC";
            _generateScript.Image = OneScriptFormsDesigner.Base64ToImage(str_generateScript);
            _generateScript.Name = "_generateScript";
            _generateScript.Size = new Size(221, 26);
            _generateScript.Text = "Сформировать сценарий";
            _generateScript.Click += _generateScript_Click;
            // 
            // _stripSeparator2
            // 
            _stripSeparator2.Name = "_stripSeparator2";
            // 
            // _loadForm
            // 
            string str_loadForm = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAA/klEQVR42u3Z0QqDMAwFUDO2vez/f3UwN+iqOKE2qdUVe2NzoS/6kmMMVEvdSUK1CzBIAuI0P4ihkLtfr0IPoypkTyfgMBHEZRRFMf7h1xMGkoNIYI6otTwEADPMdl8EAoAJLv4FOaBix19WBNmCgYdMRRpEailixt0JC1GECDRtQdDmZzk3WRA0BIeJIBq6sQuCijAIWlaHnRTORzc1wIX38bthEMScAsLthsXfQagIBjLvRgxiEIPIn70ShFC/qhaQy69+FoLaDQYS/GNQsbdqE6IMYZDqkV4rgwAgGoGgYoRTsTRESdLHCorCQq5+vWtXtiE3vz6rMq0xCFq+0/uOMfSQbkgAAAAASUVORK5CYII=";
            _loadForm.Image = OneScriptFormsDesigner.Base64ToImage(str_loadForm);
            _loadForm.Name = "_loadForm";
            _loadForm.Text = "Открыть форму";
            _loadForm.Click += _loadForm_Click;
            // 
            // _saveForm
            // 
            string str_saveForm = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABBUlEQVR42u3awQ6CMBAEUPai/v/f6qXCgYTAbruF0p3qTMIBgW1fS4RYZfqRSHQHCKmAJNB+uS94zNs7ELHmOW+fK5DImTiN2UPSbkeUC5J17GpEH0QXxoRYHQ2ALHlNhdu9O0Tr7FprW1s5LzszsBDjXHNmwiDbUbcgNRh4iBczBMTAiLmDDClhhoIomHjIrlFCwiAeZFdIa4y3bjOIUqxpSoPTFHIXxjPDzSFRIQQthKBliG+tE+3iPkcq28Z7sntrD/Gu5alPCCGEEEIIIYQQohTuDSksNfwhpNTQ3RDruBtiFYuE1Pwaf8AAJ7s+sgTlDwO5HBZGc7cH6syI+8MRQwhavjI5HUJUEs5VAAAAAElFTkSuQmCC";
            _saveForm.Image = OneScriptFormsDesigner.Base64ToImage(str_saveForm);
            _saveForm.Name = "_saveForm";
            _saveForm.Text = "Сохранить форму";
            _saveForm.Click += _saveForm_Click;
            // 
            // _stripSeparator4
            // 
            _stripSeparator4.Name = "_stripSeparator4";
            // 
            // _exit
            // 
            _exit.Name = "_exit";
            _exit.Text = "Выход";
            _exit.Click += _exit_Click;
            // 
            // _edit
            // 
            _edit.DropDownItems.AddRange(new ToolStripItem[] {
            _unDo,
            _reDo,
            _stripSeparator3,
            _cut,
            _copy,
            _paste,
            _delete});
            _edit.Name = "_edit";
            _edit.Size = new Size(69, 24);
            _edit.Text = "Правка";
            // 
            // _unDo
            // 
            string str_unDo = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAA2klEQVR42u3YSw6AIAwEULz/oTGamBAjyqdlZky7YSfzRH5uOWnXFoAABMDoQSnlfDaCgCP80UoCrvCSgDK8HOAeXgrwFF4GUAsvAXgLTw/4Ck8NaAk/WrPoT4BneAvIK2BV+BlIFYAIP4KgBPQg6D6hXgTNJHYDsCNgG1nrSzEDtHTavQQaIOCHudmRpThOz4wsxYUGAnjqWA5w71wSUAaQmcS1IBLLqFVBNjLvsL8DmB3mAjAAMLnQoAAmV0oUwOxSvxrg8ltlBcD9xxZ7BQBdAUBXANAlD9gBBDWIAQ4VHAYAAAAASUVORK5CYII=";
            _unDo.Image = OneScriptFormsDesigner.Base64ToImage(str_unDo);
            _unDo.Name = "_unDo";
            _unDo.ShortcutKeys = Keys.Control | Keys.Z;
            _unDo.Size = new Size(212, 26);
            _unDo.Text = "Отменить";
            _unDo.Click += _unDo_Click;
            // 
            // _reDo
            // 
            string str_reDo = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAA10lEQVR42u3YSw7EIAwDUHr/QzOaXqD52IkjJRt21A+JEnjumV3PAhawgIaPnnPvOwwG/EcEohWAQLQDsggJQAYhA4gipAARhBzAi5AEeBBpgCVMtCyIMIAZ3INwA6qCWxEuQEf4L4QZ0Bk+DVANPwKQ3sTK4aEA1+lZdZAxwlvmhbUSyJWyzgtt5pArZZkX3k5XAigXmioA7UpZAaBe6tmbmP6swvyNlj1ssRCoorYSFShqM7cAFCCKkAJEEHIAL0IS4IFIAyyQEYDuWkB3LaC7FtBd4wE/1ESIAWn6qDIAAAAASUVORK5CYII=";
            _reDo.Image = OneScriptFormsDesigner.Base64ToImage(str_reDo);
            _reDo.Name = "_reDo";
            _reDo.ShortcutKeys = Keys.Control | Keys.Y;
            _reDo.Size = new Size(212, 26);
            _reDo.Text = "Вернуть";
            _reDo.Click += _reDo_Click;
            // 
            // _stripSeparator3
            // 
            _stripSeparator3.Name = "_stripSeparator3";
            // 
            // _cut
            // 
            string str_cut = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABNUlEQVR42u2Y0Q7CIAxF4f8/ekYjZpmU3sItlIS+OTI9BxDa5ivtHfkIHIEjsHc0BXJK1eHrMxQjqgISeESRPwEUPooEJHCHfI6HF6gBektokwgLiC/9hnkCyNatTmYEgZFDY+kWshwY8BbSACPBiwKWH7AKsMBVAQ8JNrwqwBLxADcJWECeAJ7wZgEEqkDMSkm60+nWbTkznxqqB2qXnveWoQoU4Br8+9mMeoJWkQmr8f3IB6cKSP+H8vy+QvTMlbGFWuPS9goh0APPlqAeoyg8U8QsYLmNZzQHYIHuislZQhVgACxJ5jxmzuU7Zze23AsarWRklJRMiS2KelNNDLZVaI2t0dVYLmARcekLUfMaoNIbFgjf3NVmIhK8KIBIRIBvCrREosBDAtHjCKyOI7A6thd4ARhzzAEzNxSrAAAAAElFTkSuQmCC";
            _cut.Image = OneScriptFormsDesigner.Base64ToImage(str_cut);
            _cut.ImageTransparentColor = Color.Magenta;
            _cut.Name = "_cut";
            _cut.ShortcutKeys = Keys.Control | Keys.X;
            _cut.Size = new Size(212, 26);
            _cut.Text = "Вырезать";
            _cut.Click += OnMenuClick;
            // 
            // _copy
            // 
            string str_copy = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABOElEQVR42u3WrQ7CMBAH8DUIBAaD4AnAYtCEZ8DxEih4BhwejeMZCBoECRiQ6CUEMwghY7AlJCPpPq7tbddwfzOxNe2vubtMOJZHlH0ABqS8CwrYwwoAKiIT0F94oM1Xw1q0buf6otOoxC8BBYEGOHsvcbkFDjYCFRA+sRHoAGxEIQBMRGEALIQRwPfQBgIGUQOAEUYBs80DtPmoW43WHa++aNfVSosEIOyX+zNwVBBkAOFTBQECZNW6LkAFQQ4ARZAqIRUEWYAEoQ84rLfSD915TwuQMwww3sRAJC3A52Cy9bgASAkByyPxIhiAOUbzhAGqTTxdnnIfcjJo0QPIpkxW0KdQUmQlBGnitNJjgAbAaY73eQ0/ifcEAyAhOUZtBkBC8lfivwGmwgAG2ARADgNIhgFlx3rAG9GomUA3I+5MAAAAAElFTkSuQmCC";
            _copy.Image = OneScriptFormsDesigner.Base64ToImage(str_copy);
            _copy.ImageTransparentColor = Color.Magenta;
            _copy.Name = "_copy";
            _copy.ShortcutKeys = Keys.Control | Keys.C;
            _copy.Size = new Size(212, 26);
            _copy.Text = "Копировать";
            _copy.Click += OnMenuClick;
            // 
            // _paste
            // 
            string str_paste = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABIklEQVR42u3awQnCMBQG4OTkFL07gY6Rg6eCriAFcQoRiiso9CbFKcQNvIpLeHoWaaGWtEmapH2h77/12b68DxPxUM4CDx97ADSAY5ZHu1i86rX0fJsnG/FED5ANX+V0uUbb9eqNGsAYQK0lb6+hBzQHbasPC4DOy/92LQCtpXshVQ+BVqmBOGT5ch+Le4+ljRFdD4BR2Tjc+AMrAAD87i02iRNBsbnKfrzZzz2gGt4HQILwDeCs+3BqLcuh1mJQgI8QgAAEGBkg+S334vQKSB8fb99SspgBAQhAAAJMCFAuaBVJPwJoA1yHAAQIDWB6iFV4AkxuCxGAAJaA4P9KBA9wHWuAChEEQBWMACMEVoA2AjNAJyjPwLQAvoZXzRr82ypful5dQCOEOe0AAAAASUVORK5CYII=";
            _paste.Image = OneScriptFormsDesigner.Base64ToImage(str_paste);
            _paste.ImageTransparentColor = Color.Magenta;
            _paste.Name = "_paste";
            _paste.ShortcutKeys = Keys.Control | Keys.V;
            _paste.Size = new Size(212, 26);
            _paste.Text = "Вставить";
            _paste.Click += OnMenuClick;
            // 
            // _delete
            // 
            string str_delete = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAABmJLR0QA/wD/AP+gvaeTAAACJUlEQVRoge2YvW/TQBiHn7sjY9UFiSIYOjSiVGyt5HYAUkLyR0NVRwgJkBiRGGAEqaIsSQZKFfsYiFFUXXJ3vtdIiHtG34d/j+9svzZkMplMJhPIfFgcTEdFv6v5p4OTvfmwOIgZo0M7zsaH+wp7Zmr7av78+GF8vM1MR0XfmGqisOV8fPQodJwK6TQbH+7rypwDd5eHLqzSz7bO3n5sE/Ym01HRN7UtgXvLQ9+sqYdbL95/8I31CjjCN4hIOMI3BEl4t1DvuncF/HQ07Shbn6dspw3hAWrqW5VvjqAt9GNwvFuZugR2Hc2tVsITPnjOIAGQlZAKDxECICMhGR4iBSBNQjo8tBCAdhJdhIeWAhAn0VV4SBCAMIla2UVX4SFRADwSikssNXDHMVTkRZgsAN6VcCFWiogIQJSEaB0lJgC/JRamfq3c+x0L31H6iVR4iCinQ7ju2Z62mHXtoldriZhA86i0ip0N3W6nFoA3Ebkonue8C7H7IHkFNoa3XAIXjmHJpXhDkoD3Dav106oyj4EvjnYRidZbKKY8mA5O9oypSuC+r28srQTa1DZdSUQLpBRmXUhECUhUlUuJScocqwTfxFIl8fbkzedKq1Pgq6M5+sYOWoEu6vm/9lHf5ceIxNzeLaQWlV7TL/ltuv3y3acN20mjF2vrqj/5Qk40Oz16oLUu+Rd/LTasSCjJ8A0rEr3Q8NF0/nt9VPRjf69nMplM5v/mF/i6x8b172ZWAAAAAElFTkSuQmCC";
            _delete.Image = OneScriptFormsDesigner.Base64ToImage(str_delete);
            _delete.Name = "_delete";
            _delete.ShortcutKeys = Keys.Delete;
            _delete.Size = new Size(212, 26);
            _delete.Text = "Удалить";
            _delete.Click += OnMenuClick;
            // 
            // _view
            // 
            _view.DropDownItems.AddRange(new ToolStripItem[] {
            _form,
            _code});
            _view.Name = "_view";
            _view.Size = new Size(50, 24);
            _view.Text = "Вид";
            // 
            // _form
            // 
            string str_form = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAA1klEQVR42u3a4QqCMBQF4EaB9R69/wv5IAktavfHYippebVzdz0HJvhjcj5EpsxwcJKALkDIBOSJLqMxyKFJo0O3UeYskPJOCOiGbvVlLgLIJwJp07iiWynTCuSexgndRJkokJjGEd1EmQchxkKItYwgtb2y5DWQECshxFr2C0F/q8z1IoQQQlaCWA0hP119iwwKbgf5x8NUlCSEEEJQkDlMWDjvw/z9ruyEEELI9ASrIcRafELKrbdaIXG4q1sTptfb1fa0mx8GctCL3tK8f+FwEUKs5QXoOIWG//RH0wAAAABJRU5ErkJggg==";
            _form.Image = OneScriptFormsDesigner.Base64ToImage(str_form);
            _form.Name = "_form";
            _form.Size = new Size(50, 24);
            _form.Text = "Форма";
            _form.Click += _form_Click;
            _form.Enabled = false;
            _form.CheckState = System.Windows.Forms.CheckState.Checked;
            // 
            // _code
            // 
            string str_code = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAAvElEQVR42u3WQQqDMBSE4eQ2FWk3Hsoz9VBd6cLb6BNcdSE1IWby+g8ICYjOpz4wru8heEj8C0gcP+sNHSbr8PIA2bNYj644xM6JJdp/PagsjApkseMRMj4zCch+fVvPtuxD4puRgRz7ZIwUJAcjB0nFSEJSMLKQqxgJyI+ZrcfTA+S0R1XIVTAQIC1BSv3in90LCJCWIHcGCJAWIW6GHQgQhh0IkCoQN8MOBAjDDgQIECBAKkNUAqSluIFsV0sN9+kjczYAAAAASUVORK5CYII=";
            _code.Image = OneScriptFormsDesigner.Base64ToImage(str_code);
            _code.Name = "_code";
            _code.Size = new Size(50, 24);
            _code.Text = "Сценарий";
            _code.Click += _code_Click;
            _code.CheckState = System.Windows.Forms.CheckState.Unchecked;
            // 
            // _tools
            // 
            _tools.DropDownItems.AddRange(new ToolStripItem[] {
            _tabOrder,
            _stripSeparator5,
            _run,
            _stripSeparator6,
            _settings});
            _tools.Name = "_tools";
            _tools.Size = new Size(113, 24);
            _tools.Text = "Инструменты";
            // 
            // _tabOrder
            // 
            string str_tabOrder = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAA3ElEQVR42u3ZSw6AIAwEULj/oTExcdOApjD9menWiPOiQNU+Wu3qaEBvbXvIcZ8eCDgJv4v4BMhQqwsgwsMBq1DyIqjwIQBkeAJ2AGgEJ/FbuK/BUy6jGsApwmwj0wC8awqYpBzrQ9jS3j4CCLAATEC1JjEBlQHo5u6pZQuTrZXQItI1c1BARDvtDvAKT8DbQGkfoVm4UpNYhiu5jGoAlgiXjSyiCIiu/wJE0npvZAQQcAgQGPONbHeBSNVKmHwb9Wzm4ADvdjoEEPl3hgA0gpP4tFIuo14I040sc11VMcAB84B/6gAAAABJRU5ErkJggg==";
            _tabOrder.Image = OneScriptFormsDesigner.Base64ToImage(str_tabOrder);
            _tabOrder.Name = "_tabOrder";
            _tabOrder.Size = new Size(217, 26);
            _tabOrder.Text = "Порядок обхода";
            _tabOrder.Click += _tabOrder_Click;
            // 
            // _stripSeparator5
            // 
            _stripSeparator5.Name = "_stripSeparator5";
            // 
            // _run
            // 
            string str_run = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABkUlEQVR42t3Z0Q7CIAwFUPv/H42JmWYD2t4WCkUejDrAHjsGbPT6k0K7AwBL0RyZIVrw6SEWQEoIDyjVoTbsFBAcwIe+FeIDJIO0kSLBJ4OMIxJAnhF7AB3E7+UoxGaIjPgGZh/kSyE8gjuzDZfd5s1mBDX1exgmG9EQ7HRqYygghP+QAMFjhGxEQfR5Yhwiq5YgdIw5G7MhthnbMthJP4tmQXzLDuTyC2QDhdinYO8CENh3sF1sR2g4MFbp4DEIqYK+wKt/bCOCq3QcoldxD4LGr6g2SFKEDpFKIoTUSI4yGQJtOG+LGoSwNJY3PJsR6yFBiLWQQATa0ey7gtMRSGflLnlURjHkXrDGQcwYci9Y4yEwprcKAHZ6URAfhlvKrIaUK26q9tIFadx0Ztt7T4OUKt4hjJypOEhh1lgSJh2kl4n7dzVG+EOQWzoxEA2hYPhdZn+QT0d8OkQywWRGKtIEE3K/WQ1aGPweSNjdf5IGuBPBYUKfxbBjYRBRQ8KfjLEZGUQsLyrkBMQVJzQRpi9vUR3TM0g50FMAAAAASUVORK5CYII=";
            _run.Image = OneScriptFormsDesigner.Base64ToImage(str_run);
            _run.Name = "_run";
            _run.Text = "Запуск";
            _run.Click += _run_Click;
            _run.ShortcutKeys = Keys.Control | Keys.F6;
            _run.ShowShortcutKeys = true;
            // 
            // _stripSeparator6
            // 
            _stripSeparator6.Name = "_stripSeparator6";
            // 
            // _settings
            // 
            string str_settings = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABI0lEQVR42u3YwQ6EMAgEUP3/j3aTPRhFaGkLOLNZrqKZp9JY92P7jdr/kKgA29aNcHzbQCEeADTEAlzDXns8iHKIhpBBZxClEInQQs4iXoNEI8og2YhySCRCXjMd0gq6iqCGWItGGSTitWot3zQQ2fvajFhBPRh1JpCGXR7Xeqzz4SAaRisLCAXpYTxPCQZigbyLRCrEEyrqxqRBKhFpkGxECeQNRDhkBNHbLXq2xCmQVUSvup8wEZBMRNnPhxVExPyEQFAQSxAkxDQEDTEFQUQMQ1ARQxBkhBuCjnBBGBBdCAuiCWFCmBA2hAphRDwgrIgbhBlxQtgRKoQRMQRBRpwQGbT1ixIRYUKuYRkQN4gWWhYq4gFpYZARKkQDoSOaELb6AFfXABC6bvmCAAAAAElFTkSuQmCC";
            _settings.Image = OneScriptFormsDesigner.Base64ToImage(str_settings);
            _settings.Name = "_settings";
            _settings.Text = "Параметры";
            _settings.Click += _settings_Click;
            // 
            // _help
            // 
            _help.DropDownItems.AddRange(new ToolStripItem[] { _about });
            _help.Name = "_help";
            _help.Size = new Size(77, 24);
            _help.Text = "Помощь";
            // 
            // _about
            // 
            string str_about = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABCklEQVR42u3WwRKDIAyE4fLmvjntpRenSDbZTaBDzo7+n4NCu157TzuAA1gE8LlPB69vSwDQcDbGDWCEMyAwQBEeQUAAdbwHYQZkxaMIEyA7HkFMAVXxVsQjwBvfBw9tzvs9IaiAbl23xE1vCFDFsxEUABrvQUCAjHgUMEJIAd9AyzU0ABL/FHcPY/2Z7ogwYBS3FcCL3AbA3NjSAawPuATAjk8FKOKXAETi0wCq+FIAI/4AVpgpYGXEpTiN/hrVEjIDIois////A7yIjOODFDBCKN7+FMBCqOJNgAgiOrN4M6ACYYmHAJkIazwMyEAg8S6ACoKGhwEsiDecBvBCouF0QNUcQPVsD3gDeqycMcHL1j4AAAAASUVORK5CYII=";
            _about.Image = OneScriptFormsDesigner.Base64ToImage(str_about);
            _about.Name = "_about";
            _about.Size = new Size(187, 26);
            _about.Text = "О программе...";
            _about.Click += _about_Click;
            // 
            // pnl4Toolbox
            // 
            pnl4Toolbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            pnl4Toolbox.Controls.Add(listBox1);
            pnl4Toolbox.Dock = System.Windows.Forms.DockStyle.Left;
            pnl4Toolbox.Location = new Point(0, 26);
            pnl4Toolbox.Name = "pnl4Toolbox";
            pnl4Toolbox.Size = new Size(163, 489);
            pnl4Toolbox.TabIndex = 2;
            // 
            // listBox1
            // 
            listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 16;
            listBox1.Location = new Point(0, 0);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(159, 485);
            listBox1.TabIndex = 0;
            // 
            // pnl4pDesigner
            // 
            pnl4pDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            pnl4pDesigner.Location = new Point(163, 26);
            pnl4pDesigner.Name = "pnl4pDesigner";
            pnl4pDesigner.Size = new Size(726, 489);
            pnl4pDesigner.TabIndex = 3;
            // 
            // pnl4splitter
            // 
            pnl4splitter.BackColor = Color.LightSteelBlue;
            pnl4splitter.Location = new Point(163, 26);
            pnl4splitter.Name = "pnl4splitter";
            pnl4splitter.Size = new Size(5, 489);
            pnl4splitter.TabIndex = 4;
            pnl4splitter.TabStop = false;
            // 
            // pDesignerMainFormPFL
            // 
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(889, 515);
            Controls.Add(pnl4splitter);
            Controls.Add(pnl4pDesigner);
            Controls.Add(pnl4Toolbox);
            Controls.Add(menuStrip1);
            string str_Icon = "AAABAAEAAAAQAAEABABooAAAFgAAACgйQAAAAIAAAEABйAAKййAAEййAAAAACAAACAAAAAgIAAgAAAAIAAgACAgAAAgICAAMDAwAAAAP8AAP8AAAD//wD/AAAA/wD/AP//AAD///8ккккккккккккккккккккккккккккккккккккккккккйййййAAAеееец////8ййAAAееееццййAAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDееццццwййеццййAPееццццййDец/////8ййAеецццц8ййPец/////wййDееццццwййеццййAPееццццййDец/////8ййAеецццц8ййPец/////wййDееццццwййеццййAPееццццййDец/////8ййAеецццц8ййPец/////wййDееццццwййеццййAPееццццййDец/////8ййAеецццц8ййPец/////wййDееццццwййеццййAPее//8Aццц//ййDццц/8Pцццц//8ййAее//8AAP8йййййPAAAADцц/8ADцццц//wййDгкййAAAP8AAAD/ййAAAADwйй8AAAAADц/////8AAAцццц//ййAPццц/wкййAAAP8AAAAA/wййAAAPййDwйц////8AAAAPцццц8ййAццц/wкййAAAP8йP8ййAAA8ййP/wAAAADц///8AAAAADццццwййDццц/кййAAAP8йAD/ййD//wййAP//AAAAц//8йAццццййAPцццкййAAAP8йAAA/wйAAD///ййй//AADц/8йAAPг/8ййAццц8кййAAP8йAAAAP8йA//йййAAAA//8Pц8йAAADг/wййDцццwкййAP8йAAAAAD/AAAAD//wййййDц/8йAAAAAг/ййAPцццкййAP8ййA/wAA//ййййAAAAD/////8ййPццц//8ййAццц8кййP8ййAAP///wййййй////8ййADццц//wййDцццwйDе/////8ййAAAD//wйййййAA//8ййAAAццц//ййAPцццйDе/////8ййAAAAA8йййййAAAAA8ййAAAAPццц8ййAццц8йPе/////wкйййййAAAAццц/wййDцццwйец8кйййййAAAццц//ййAPцццйDец/кйййййAAццц//8ййAццц8йPец/wкйййййгwййDцццwйец//8кййййAAAAAг/ййAPцццйDец///кййййAAAAг/8ййAццц8йPец///wкййййAAг//wййDцццwйец////8кййййAццццййAPцццйDец/////кййййцццц8ййAццц8йPец/////wкйййAAAAцццц/wййDцццwйецц8кйййAAAцццц//ййAPцццйDецц/кйййAAцццц//8ййAццц8йPецц/wкйййцгwййDцццwйецц//кйййDцгййAPцццйDецц/8кйййAцццц//8ййAццц8йPецц8кйййADцццц//wййDцццwйецц/wкйййAAцццц//ййAPцццйDеццwкйййAADцццц/8ййAццц8йPеццкйййAAAAцццц/wййDцццwйеццкйййAAAAAPццццййAPцццйDец/////8ййййADц/ййййAAцццц8ййAццц8йPец////8ййййAц////8йййAAAAADццццwййDцццwйец/////wйййAAAADцц/8йййAAAAAццццййAPцццйDец////wйййAAAADцц///йййAAAADг//8ййAццц8йPец////йййAAAAцццйййAAAAг//wййDцццwйец////8йййAAAццц/wйййAADг//ййAPцццйDец////wйййAAццц//8йййAAPг/8ййAццц8йPец///wйййAADццц//wйййAADг/wййDцццwйец////йййAADг8йййAAPг/ййAPцццйDец///8йййADг//йййAAг/8ййAццц8йPец//8йййAAPг/8йййADг/wййDцццwйццгwйййййAPццццйййййAPц////ййAPцццйDццгйййййAAцццц/wйййййц////8ййAццц8йPццццц//8йййййAцццц//йййййDц////wййDцццwйццгwйййййDцццц//wййййAAAAAPц////ййAPцццйDццгйййййAPцццц//йййййц////8ййAццц8йPццццц//8йййййAцццц//8ййййAAAAADц////wййDцццwйццгwйййййцгwййййAAAAAPц////ййAPцццйDццгйййййDцг8ййййAAAAAц////8ййAццц8йPццццц//8йййййPцгwййййAAAADц////wййDцццwйццгwйййййцг/ййййAAAAAPц////ййAPцццйDццгйййййDцг8ййййAAAAAц////8ййAццц8йPццццц//8йййййPцццц//8ййййAAAAADц////wййDцццwйццгwйййййDцццц//wййййAAAAAPц////ййAPцццйDццгйййййAPцццц//йййййц////8ййAццц8йPццццц//8йййййAцццц//8ййййAAAAADц////wййDцццwйццгwйййййDцццц/8йййййPц////ййAPцццйDццгйййййAAцццц/wйййййц////8ййAццц8йPццццц//8йййййADццццwйййййDц////wййDцццwйец//йййAAAAг//wйййAPг/ййAPцццйDец///8йййADг//йййAAг/8ййAццц8йPец///wйййAAг/йййAADг/wййDцццwйец////йййAADгйййAAAPг/ййAPцццйDец///8йййAAAццц//8йййAAPг/8ййAццц8йPец////йййAAAPццц8йййAAAг//wййDцццwйец////8йййAAADцц/////8йййAAADг//ййAPцццйDец/////йййAAAAцц////8йййAAADг//8ййAццц8йPец////8йййAAAAAцц//йййAAAAAPг//wййDцццwйец/////wййййц/////wййййццццййAPцццйDец/////8ййййAц//8ййййAцццц8ййAццц8йPец/////wййййAAAA8PййййAAAADццццwййDцццwйецц8кйййAAADцццц/ййAPцццйDеццwкйййAAAPцццц8ййAццц8йPецц8кйййAAPцццц/wййDцццwйецц/wкйййAPцццц//ййAPцццйDецц/8кйййAцццц//8ййAццц8йPецц//кйййцгwййDцццwйецц//8кййAAAAADцгййAPцццйDецц/8кйййAцццц//8ййAццц8йPецц8кйййAAPцццц/wййDцццwйецц8кйййAAADцццц/ййAPцццйDец/////8кйййAAAAAцццц8ййAццц8йPец////8кййййPг//wййDцццwйец////8кййййADг//ййAPцццйDец///8кййййAAAг/8ййAццц8йPец//8кййййAAAAPгwййDцццwйец//8кййййAAAAADгййAPцццйDец/8кйййййAццц//8ййAццц8йPец8кйййййAAPццц/wййDцццwйец8кйййййAAADццц/ййAPцццйDецwййAAAA8йййййAAAAA8ййAAAAPццц8ййAццц8йPец8ййAAA//йййййAAAA//ййAAAPццц/wййDцццwйец//ййAA////йййййAP///wййAPццц//ййAPцццйDец//wййцййййAAAAц8ййPццц//8ййAццц8йPец//8йAAAAAц//ййййAPц//йAAAAAPгwййDцццwкййAAAPйAAAA8йAD/8йййAAAD/8Aц/wйAAAPг/ййAPцццкййAAAADwйAA8йAAAD//wййAAAAD///AADц/8йAAPг/8ййAццц8кййAAAAA8йA8ййP//8ййP//AAAAAPц//йAPг//wййDцццwкййAAAAAPй8ййAAADwйй/wйц///wAAAAAPццццййAPцццкйййDwAAAA8ййAAAAPййD8йDц///8AAAAPцццц8ййAццц8кйййA8AAA8ййAAAAA8ййPwйPц////AAAPцццц/wййDцццwкйййAPAA8ййAAAAADwйй/йAц/////wAPцццц//ййAPцццкйййAADw8йййPййD8йDц/////8Pцццц//8ййAццц8кйййAAA8йййA8ййPwйPе///wййDцццwйнуууууzMzMzwйй/йAе////ййAPцццйDMнуууууzMzPййD8йDе///8ййAццц8йMнуууууzMzM8ййPwйPе///wййDцццwйнуууууzMzMzwйй/йAе////ййAPцццйDMzMzMzйййййAууwAAAуzMAAAMzMzMzPййD8йDе///8ййAццц8йMzMzMwAццццц8AуzMzMAP//8AzMzMzAD///AMzMzM8ййPwйPе///wййDцццwйzMzMwPццццц//8MуzMD/////8MzMzAцDMzMzwйй/йAе////ййAPцццйDMzMwPццг/DMуDц/DMzAц/wzMwPййD8йDе///8ййAццц8йMzMzAццг/8MуwPц8MzMDц/DMzA8ййPwйPе///wййDцццwйzMzAеDMzMzMwPц//DMDц//wzA/wйй/йAе////ййAPцццйDMzMDццг//8MzMzMzAц//8MwPц//DMD/ййD8йDе///8ййAццц8йMzMwPццг//wуDц//wzAц//8MwP8ййPwйPе///wййDцццwйzMzAеDMzMzMwPц//DMDц//wzAцц///йAе////ййAPцццйDMzMDццг//8MzMzMzAц//8MwPц//DMDц//wуйDе///8ййAццц8йMzMzAццг/8MуwPц8MzMDц/DMzAц/wуwйPе///wййDцццwйzMzMDццг/wуzAц/wzMwPц8MzMDц/DMzMzMzйAе////ййAPцццйDMzMzAццгwуzMwP/////wzMzMD/////8MzMzAцDMуйDе///8ййAццц8йMzMzMwAццццц8AуzMzMAP//8AzMzMzAD///AMzMzMwA///wDMуwйPе///wййDцццwйуwйййййMууAAAMуzAAADMуwAAAуzMzйAе////ййAPцццйDMннууйDе///8ййAццц8йMннууwйPе///wййDцццwйннууwйAе////ййAPцццйAMннуzMzMwйADе///8ййAццц8ккййййAAPе///wййDцццwккййййAAе////ййAPцццккййййAADе///8ййAццц8ккййййAAPе///wййDцццwккййййAAе////ййAPццц8ккййййAе////8ййAццц/wккййййDе////wййDццц/8ккйййAAAAADе/////ййAPццц//8ккйййAAAец8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8кккккккккккккккккккккккккккккккккккккккккккккккййййAAADMннннууййAMннннууwййAннннууzййADMннннууййAMннннууwййAннннууzййADMннннууййAMннннууwййAннннууzййADMуzMккAAуууzMAAAAAMууzMAAAAAMууzMAAAAAMуzMййAMуzADее/8AууzMzMzAD/////AMуzMzMzAD/////AMуzMzMzAD/////AMуwййAуwAее////8AууzMwAц//AMуzMwAц//AMуzMwAц//AMzMzMzййADMzMzMwPеец8MууwPц////DMуwPц////DMуwPц////DMzMzMййAMzMzMwPеец//DMуzMzMwPц/////wуwPц/////wуwPц/////wzMzMwййAzMzMwPеец///wуzMzMwPцц8MzMzMwPцц8MzMzMwPцц8MzMzййADMzMzAеец////DMуzMzAцц/wzMzMzAцц/wzMzMzAцц/wzMzMййAMzMzAеец/////wуzMzAцц//8MzMzAцц//8MzMzAцц//8MzMwййAzMzMDеец/////DMуzMDцц//wzMzMDцц//wzMzMDцц//wzMzййADMzMDееццwуzMDцц///8MzMDцц///8MzMDцц///8MzMййAMzMwPееццDMуwPцц///wzMwPцц///wzMwPцц///wzMwййAzMzAеецц8MуzAцц////DMzAцц////DMzAцц////DMzййADMzMDееццwуzMDцц///8MzMDцц///8MzMDцц///8MzMййAMzMwPееццDMуwPцц///wzMwPцц///wzMwPцц///wzMwййAzMzAеецц8MуzAцц////DMzAцц////DMzAцц////DMzййADMzMDееццwуzMDцц///8MzMDцц///8MzMDцц///8MzMййAMzMwPееццDMуwPцц///wzMwPцц///wzMwPцц///wzMwййAzMzMDеец/////DMуzMDцц//wzMzMDцц//wzMzMDцц//wzMzййADMzMwPеец////8MуzMwPцц//DMzMwPцц//DMzMwPцц//DMzMййAMzMzMDеец///8MуzMzMDцц/DMzMzMDцц/DMzMzMDцц/DMzMwййAzMzMwPеец///wуzMzMwPцц8MzMzMwPцц8MzMzMwPцц8MzMzййADMzMzMDеец//wууDц/////8MуDц/////8MуDц/////8MzMzMййAMzMzMzAеец/wууzAц////8MуzAц////8MуzAц////8MzMzMwййAуwAее////8AууzMwAц//AMуzMwAц//AMуzMwAц//AMzMzMzййADMуwAее//AMууzMzMwA/////wDMуzMzMwA/////wDMуzMzMwA/////wDMуййAMуzMwккADMуууwAAAAAууzMwAAAAAууzMwAAAAAуzMwййAннннууzййADMннннууййAMннннууwййAннннууzййADMннннууййAAннннууwййAAMннннуzMzMwкккккккккккккккккккккккккккккккккккккккккккккккйййAAAD/gкAAB//4кAAAB/+кAAAAB/wкAAAAD+кAAAAAHwкAAAAAPкй4кAAAAABgкAAAAAEккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккAAIкAAAAABgкAAAAAHкй8кAAAAAD4кAAAAAfwкAAAAD/gкAAAAf/gкAAAH//gкAAB/w==";
            str_Icon = str_Icon.Replace("г", "ццц///");
            str_Icon = str_Icon.Replace("н", "уууууу");
            str_Icon = str_Icon.Replace("е", "цццццц");
            str_Icon = str_Icon.Replace("к", "йййййй");
            str_Icon = str_Icon.Replace("у", "zMzMzM");
            str_Icon = str_Icon.Replace("ц", "//////");
            str_Icon = str_Icon.Replace("й", "AAAAAA");
            Icon = new Icon(new MemoryStream(Convert.FromBase64String(str_Icon)));
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4);
            Name = "pDesignerMainFormPFL";
            Text = "Дизайнер форм для OneScriptForms";
            Load += pDesignerMainForm_Load;
            //* 18.12.2021 perfolenta
            FormClosing += pDesignerMainForm_Closing;
            //***
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            pnl4Toolbox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

            // Элемент управления: (pDesigner)pDesignerCore 
            IpDesignerCore = pDesignerCore as IpDesigner;
            pDesignerCore.Parent = pnl4pDesigner;

            // Добавим элементы (toolboxItems) к будущей панели элементов (toolbox) указатель
            ToolboxItem toolPointer = new ToolboxItem();
            toolPointer.DisplayName = "<Указатель>";
            toolPointer.Bitmap = new Bitmap(16, 16);
            listBox1.Items.Add(toolPointer);

            // Элементы управления
            ToolboxItem toolButton = new ToolboxItem(typeof(Button));
            toolButton.DisplayName = "Кнопка (Button)";
            listBox1.Items.Add(toolButton);

            ToolboxItem toolCheckBox = new ToolboxItem(typeof(CheckBox));
            toolCheckBox.DisplayName = "Флажок (CheckBox)";
            listBox1.Items.Add(toolCheckBox);

            ToolboxItem toolColorDialog = new ToolboxItem(typeof(ColorDialog));
            toolColorDialog.DisplayName = "ДиалогВыбораЦвета (ColorDialog)";
            listBox1.Items.Add(toolColorDialog);

            ToolboxItem toolComboBox = new ToolboxItem(typeof(ComboBox));
            toolComboBox.DisplayName = "ПолеВыбора (ComboBox)";
            listBox1.Items.Add(toolComboBox);

            ToolboxItem toolDataGrid = new ToolboxItem(typeof(DataGrid));
            toolDataGrid.DisplayName = "СеткаДанных (DataGrid)";
            listBox1.Items.Add(toolDataGrid);

            ToolboxItem toolDateTimePicker = new ToolboxItem(typeof(DateTimePicker));
            toolDateTimePicker.DisplayName = "ПолеКалендаря (DateTimePicker)";
            listBox1.Items.Add(toolDateTimePicker);

            ToolboxItem toolFileSystemWatcher = new ToolboxItem(typeof(FileSystemWatcher));
            toolFileSystemWatcher.DisplayName = "НаблюдательФайловойСистемы (FileSystemWatcher)";
            listBox1.Items.Add(toolFileSystemWatcher);

            ToolboxItem toolFontDialog = new ToolboxItem(typeof(FontDialog));
            toolFontDialog.DisplayName = "ДиалогВыбораШрифта (FontDialog)";
            listBox1.Items.Add(toolFontDialog);

            ToolboxItem toolFolderBrowserDialog = new ToolboxItem(typeof(FolderBrowserDialog));
            toolFolderBrowserDialog.DisplayName = "ДиалогВыбораКаталога (FolderBrowserDialog)";
            listBox1.Items.Add(toolFolderBrowserDialog);

            ToolboxItem toolGroupBox = new ToolboxItem(typeof(GroupBox));
            toolGroupBox.DisplayName = "РамкаГруппы (GroupBox)";
            listBox1.Items.Add(toolGroupBox);

            ToolboxItem toolHProgressBar = new ToolboxItem(typeof(HProgressBar));
            toolHProgressBar.DisplayName = "ИндикаторГоризонтальный (HProgressBar)";
            listBox1.Items.Add(toolHProgressBar);

            ToolboxItem toolVProgressBar = new ToolboxItem(typeof(VProgressBar));
            toolVProgressBar.DisplayName = "ИндикаторВертикальный (VProgressBar)";
            listBox1.Items.Add(toolVProgressBar);

            ToolboxItem toolHScrollBar = new ToolboxItem(typeof(HScrollBar));
            toolHScrollBar.DisplayName = "ГоризонтальнаяПрокрутка (HScrollBar)";
            listBox1.Items.Add(toolHScrollBar);

            ToolboxItem toolImageList = new ToolboxItem(typeof(System.Windows.Forms.ImageList));
            toolImageList.DisplayName = "СписокИзображений (ImageList)";
            listBox1.Items.Add(toolImageList);

            ToolboxItem toolLabel = new ToolboxItem(typeof(Label));
            toolLabel.DisplayName = "Надпись (Label)";
            listBox1.Items.Add(toolLabel);

            ToolboxItem toolLinkLabel = new ToolboxItem(typeof(LinkLabel));
            toolLinkLabel.DisplayName = "НадписьСсылка (LinkLabel)";
            listBox1.Items.Add(toolLinkLabel);

            ToolboxItem toolListBox = new ToolboxItem(typeof(ListBox));
            toolListBox.DisplayName = "ПолеСписка (ListBox)";
            listBox1.Items.Add(toolListBox);

            ToolboxItem toolListView = new ToolboxItem(typeof(ListView));
            toolListView.DisplayName = "СписокЭлементов (ListView)";
            listBox1.Items.Add(toolListView);

            ToolboxItem toolMainMenu = new ToolboxItem(typeof(System.Windows.Forms.MainMenu));
            toolMainMenu.DisplayName = "ГлавноеМеню (MainMenu)";
            listBox1.Items.Add(toolMainMenu);

            ToolboxItem toolMonthCalendar = new ToolboxItem(typeof(MonthCalendar));
            toolMonthCalendar.DisplayName = "Календарь (MonthCalendar)";
            listBox1.Items.Add(toolMonthCalendar);

            ToolboxItem toolNotifyIcon = new ToolboxItem(typeof(NotifyIcon));
            toolNotifyIcon.DisplayName = "ЗначокУведомления (NotifyIcon)";
            listBox1.Items.Add(toolNotifyIcon);

            ToolboxItem toolNumericUpDown = new ToolboxItem(typeof(NumericUpDown));
            toolNumericUpDown.DisplayName = "РегуляторВверхВниз (NumericUpDown)";
            listBox1.Items.Add(toolNumericUpDown);

            ToolboxItem toolOpenFileDialog = new ToolboxItem(typeof(OpenFileDialog));
            toolOpenFileDialog.DisplayName = "ДиалогОткрытияФайла (OpenFileDialog)";
            listBox1.Items.Add(toolOpenFileDialog);

            ToolboxItem toolPanel = new ToolboxItem(typeof(Panel));
            toolPanel.DisplayName = "Панель (Panel)";
            listBox1.Items.Add(toolPanel);

            ToolboxItem toolPictureBox = new ToolboxItem(typeof(PictureBox));
            toolPictureBox.DisplayName = "ПолеКартинки (PictureBox)";
            listBox1.Items.Add(toolPictureBox);

            ToolboxItem toolPropertyGrid = new ToolboxItem(typeof(PropertyGrid));
            toolPropertyGrid.DisplayName = "СеткаСвойств (PropertyGrid)";
            listBox1.Items.Add(toolPropertyGrid);

            ToolboxItem toolRadioButton = new ToolboxItem(typeof(RadioButton));
            toolRadioButton.DisplayName = "Переключатель (RadioButton)";
            listBox1.Items.Add(toolRadioButton);

            ToolboxItem toolRichTextBox = new ToolboxItem(typeof(RichTextBox));
            toolRichTextBox.DisplayName = "ФорматированноеПолеВвода (RichTextBox)";
            listBox1.Items.Add(toolRichTextBox);

            ToolboxItem toolSaveFileDialog = new ToolboxItem(typeof(SaveFileDialog));
            toolSaveFileDialog.DisplayName = "ДиалогСохраненияФайла (SaveFileDialog)";
            listBox1.Items.Add(toolSaveFileDialog);

            ToolboxItem toolSplitter = new ToolboxItem(typeof(Splitter));
            toolSplitter.DisplayName = "Разделитель (Splitter)";
            listBox1.Items.Add(toolSplitter);

            ToolboxItem toolStatusBar = new ToolboxItem(typeof(StatusBar));
            toolStatusBar.DisplayName = "СтрокаСостояния (StatusBar)";
            listBox1.Items.Add(toolStatusBar);

            ToolboxItem toolTabControl = new ToolboxItem(typeof(TabControl));
            toolTabControl.DisplayName = "ПанельВкладок (TabControl)";
            listBox1.Items.Add(toolTabControl);

            ToolboxItem toolTextBox = new ToolboxItem(typeof(TextBox));
            toolTextBox.DisplayName = "ПолеВвода (TextBox)";
            listBox1.Items.Add(toolTextBox);

            ToolboxItem toolTimer = new ToolboxItem(typeof(Timer));
            toolTimer.DisplayName = "Таймер (Timer)";
            listBox1.Items.Add(toolTimer);

            ToolboxItem toolToolBar = new ToolboxItem(typeof(ToolBar));
            toolToolBar.DisplayName = "ПанельИнструментов (ToolBar)";
            listBox1.Items.Add(toolToolBar);

            ToolboxItem toolToolTip = new ToolboxItem(typeof(ToolTip));
            toolToolTip.DisplayName = "Подсказка (ToolTip)";
            listBox1.Items.Add(toolToolTip);

            ToolboxItem toolTreeView = new ToolboxItem(typeof(TreeView));
            toolTreeView.DisplayName = "Дерево (TreeView)";
            listBox1.Items.Add(toolTreeView);

            ToolboxItem toolUserControl = new ToolboxItem(typeof(UserControl));
            toolUserControl.DisplayName = "ПользовательскийЭлементУправления (UserControl)";
            listBox1.Items.Add(toolUserControl);

            ToolboxItem toolVScrollBar = new ToolboxItem(typeof(VScrollBar));
            toolVScrollBar.DisplayName = "ВертикальнаяПрокрутка (VScrollBar)";
            listBox1.Items.Add(toolVScrollBar);

            listBox1.Sorted = true;
            listBox1.HorizontalScrollbar = true;
            IpDesignerCore.Toolbox = listBox1;
        }

        private void _run_Click(object sender, EventArgs e)
        {
            string strTempFile = String.Format(Path.GetTempPath() + "oscript_{0}_{1}.os", DateTime.Now.ToString("yyyyMMddHHmmssfff"), Guid.NewGuid().ToString().Replace("-", ""));
            File.WriteAllText(strTempFile, SaveScript.GetScriptText(), Encoding.UTF8);

            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            psi.Arguments = strTempFile;
            psi.FileName = (string)Settings.Default["osPath"];
            System.Diagnostics.Process.Start(psi);
        }

        private void _settings_Click(object sender, EventArgs e)
        {
            settingsForm = new System.Windows.Forms.Form();
            settingsForm.Text = "Параметры";
            settingsForm.Width = 600;
            settingsForm.Height = 500;
            settingsForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            string str_settingsForm = "AAABAAEAMjIAAAEAIADIKAAAFgAAACgAAAAyAAAAZAAAAAEAIеAoCgеееAAAAADцццццццццццццццццццццццццццццццццццццццццг/////AAAACAAAAHUAAAC4AAAAxAAAAI4AAAAcццг/wAAAAkAAADFAкAAOgAAADbAAAA/gAAAOsAAAAzццццццй//AAAAdgAAAP8ункAAO4AAAAzцццццц//8AAAC5AAAA5wAAAP8ункAAO4AAAAzцгййй///wAAAMYAAADYAннкAAO4AAAAzцгйй////AAAAkAAAAP4уннAAA8QAAAO4AAAAzцгй////8AAAAeAAAA7AAAAPEуннAAA8QAAAO4AAAAzцгй////8AAAAxAAAA7QAAAPEуннAAA8QAAAO8AAAA2цгй////8AAAAxAAAA7QAAAPEуннAAA8QAAAO8AAAA2цгй////8AAAAxAAAA7QAAAPEуннAAA8QAAAO8AAAA2цгй////8AAAAxAAAA7QAAAPEуннкAAO8AAAA2цгй////8AAAAxAAAA7QAAAPEуннкAAO8AAAA2цгй////8AAAAxAAAA7QAAAPEуннкAAO8AAAA2цгй////8AAAAxAAAA7QAAAPEуннAAA8QAAAO8AAAA2цгй////8AAAAxAAAA7QAAAP8уннкAAO8AAAA2цгй////8AAAAxAAAA7QAAAP8уннкAAO8AAAA2цгй////8AAAAxAнннкAAO8AAAA2цгй////8AAAAxAнннкAAO8AAAA2цгй////8AAAAzAAAA7gAAAP8уннкAAO4AAABLAAAAawAAAKgAAADDAAAAxAAAAKsAAAB2AAAAJvгй//8AAAAzAAAA7gAAAPEунннAAA+gAAAOEAAADeAAAA+AAAAP8уAAArwAAACbг///8AAAAzAAAA7gAAAPEуннннкAAP8AAAD9AAAA9AAAAFPг///8AAAAzAAAA7gAAAPEуннннкAAP8AAADgAAAA/gAAAFXг///8AAAAzAAAA7gAAAP8уннннкAAP8AAADfAAAA9AAAACnг///8AAAAzAAAA7gAAAPEунннннAAAгй////8AAABLAннннннAAALPг///wAAAGwуннкAAO0уAAA9QAAAOUункAAP8AAADг/////AAAAqAAAAPkуннAAA0йй////wAAANcунAAA8QAAAPг////8AAADDAAAA4AAAAP8унAAA7QAAANbйййй//wAAANQукAAP8AAADWAAAAг/////wAAAMYAAADdAнкAAP8AAADц//wAAANMAAAD+AкAANsAAADг/////AAAArAAAAPYунAAAwgAAAPXцйwAAANUAAAD+AAAA/QAAAPг////8AAAB4AннAAA5fцй/////wAAANQуAAAг/////wAAACgункAAP8AAAD+AAAA1цййwAAANTгйй//wAAALIAAAD8AннAAA1vцццццц//AAAAKQAAAPUAAADdAннAAA1fцццццц//AAAAVgAAAP4уннAAA1fцццццц//AAAAWAAAAPYAAAD7AнкAAP8AAAD+AAAA1Pцццццц//AAAALAAAALcукAAPAAAADUAAAA2wAAAP0уAAA1Pццццццй/wAAAC8ункAAPццццццццццццццццццццццццццццццццццццццццццг//еееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееее==";
            str_settingsForm = str_settingsForm.Replace("г", "ццццйй");
            str_settingsForm = str_settingsForm.Replace("н", "кAAP8у");
            str_settingsForm = str_settingsForm.Replace("е", "AAAAAA");
            str_settingsForm = str_settingsForm.Replace("к", "AAA/wA");
            str_settingsForm = str_settingsForm.Replace("у", "AAAD/A");
            str_settingsForm = str_settingsForm.Replace("ц", "йййййй");
            str_settingsForm = str_settingsForm.Replace("й", "//////");
            settingsForm.Icon = new Icon(new MemoryStream(Convert.FromBase64String(str_settingsForm)));

            tabControl = new System.Windows.Forms.TabControl();
            tabControl.Parent = settingsForm;
            tabControl.Left = 15;
            tabControl.Top = 15;
            tabControl.Anchor = System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right |
                System.Windows.Forms.AnchorStyles.Bottom;
            tabControl.Size = new Size(settingsForm.Width - 120, settingsForm.Height - 50);

            tabPage1 = new System.Windows.Forms.TabPage("Файлы");
            tabPage1.Parent = tabControl;

            groupBox = new System.Windows.Forms.GroupBox();
            groupBox.Parent = tabPage1;
            groupBox.Text = "Пути";
            groupBox.Anchor = System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;
            groupBox.Left = 25;
            groupBox.Top = 25;
            groupBox.Width = 150;
            groupBox.Height = 170;

            label_os = new System.Windows.Forms.Label();
            label_os.Parent = groupBox;
            label_os.Left = 10;
            label_os.Top = groupBox.Top;
            label_os.Width = 80;
            label_os.Text = "oscript.exe:";
            label_os.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            textBox_osPath = new System.Windows.Forms.TextBox();
            textBox_osPath.Parent = groupBox;
            textBox_osPath.Left = label_os.Left;
            textBox_osPath.Top = label_os.Bottom + 3;
            textBox_osPath.Anchor = System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;
            textBox_osPath.Text = (string)Settings.Default["osPath"];

            button_osPath = new System.Windows.Forms.Button();
            button_osPath.Parent = groupBox;
            button_osPath.Font = new Font(groupBox.Font, FontStyle.Bold);
            button_osPath.Text = "...";
            button_osPath.Left = 115;
            button_osPath.Top = textBox_osPath.Top;
            button_osPath.Width = 27;
            button_osPath.Anchor = System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;
            button_osPath.Click += button_osPath_Click;

            label_dll = new System.Windows.Forms.Label();
            label_dll.Parent = groupBox;
            label_dll.Left = textBox_osPath.Left;
            label_dll.Top = textBox_osPath.Bottom + 10;
            label_dll.Width = 140;
            label_dll.Text = "OneScriptForms.dll:";
            label_dll.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            textBox_dllPath = new System.Windows.Forms.TextBox();
            textBox_dllPath.Parent = groupBox;
            textBox_dllPath.Left = label_dll.Left;
            textBox_dllPath.Top = label_dll.Bottom + 3;
            textBox_dllPath.Anchor = System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;
            textBox_dllPath.Text = (string)Settings.Default["dllPath"];

            button_dllPath = new System.Windows.Forms.Button();
            button_dllPath.Parent = groupBox;
            button_dllPath.Font = new Font(groupBox.Font, FontStyle.Bold);
            button_dllPath.Text = "...";
            button_dllPath.Left = 115;
            button_dllPath.Top = textBox_dllPath.Top;
            button_dllPath.Width = 27;
            button_dllPath.Anchor = System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;
            button_dllPath.Click += Button_dllPath_Click;

            buttonOK = new System.Windows.Forms.Button();
            buttonOK.Parent = settingsForm;
            buttonOK.Text = "OK";
            buttonOK.Left = 507;
            buttonOK.Top = 387;
            buttonOK.Width = 75;
            buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Right |
                System.Windows.Forms.AnchorStyles.Bottom;
            buttonOK.Click += ButtonOK_Click;

            buttonCancel = new System.Windows.Forms.Button();
            buttonCancel.Parent = settingsForm;
            buttonCancel.Text = "Отмена";
            buttonCancel.Left = 507;
            buttonCancel.Top = 420;
            buttonCancel.Width = 75;
            buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Right |
                System.Windows.Forms.AnchorStyles.Bottom;
            buttonCancel.Click += ButtonCancel_Click;

            if (settingsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Записываем значения в Settings.
                Settings.Default["osPath"] = textBox_osPath.Text;
                Settings.Default["dllPath"] = textBox_dllPath.Text;
                Settings.Default.Save();
            }
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

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            settingsForm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            settingsForm.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button_osPath_Click(object sender, EventArgs e)
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

        private void _exit_Click(object sender, EventArgs e)
        {
            //* 18.12.2021 perfolenta
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
            //***
        }

        private void _saveForm_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.OverwritePrompt = true;

            saveFileDialog1.Filter = "OSD files(*.osd)|*.osd|All files(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            File.WriteAllText(saveFileDialog1.FileName, SaveForm.GetScriptText(saveFileDialog1.FileName), Encoding.UTF8);
            //File.WriteAllText("C:\\444\\Форма1сохран.osd", SaveForm.GetScriptText(), Encoding.UTF8);
        }

        private void _loadForm_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            OpenFileDialog1.RestoreDirectory = true;
            OpenFileDialog1.Filter = "OSD files(*.osd)|*.osd|All files(*.*)|*.*";
            if (OpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string strOSD = File.ReadAllText(OpenFileDialog1.FileName);
            //////string strOSD = File.ReadAllText("C:\\444\\Форма1сохран.osd");
            strOSD = strOSD.Replace(" ", "");

            string[] result = null;
            string[] stringSeparators = new string[] { Environment.NewLine };
            string ComponentBlok = null;
            string rootBlok = null;

            // соберем из блока конструкторов имена компонентов в CompNames. ///////////////////////////////////////////////////////////////////////////////////////////
            List<string> CompNames = new List<string>();
            Dictionary<string, Component> dictComponents = new Dictionary<string, Component>();
            string ConstructorBlok = OneScriptFormsDesigner.ParseBetween(strOSD, @"[<Конструкторы]", @"[Конструкторы>]");
            result = ConstructorBlok.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < result.Length; i++)
            {
                string s = OneScriptFormsDesigner.ParseBetween(result[i], null, @"=Ф.");
                if (s != null && s.Contains("МенюЗначкаУведомления"))
                {
                    continue;
                }
                if (s != null)
                {
                    if (s.Substring(0, 2) != @"//")
                    {
                        CompNames.Add(s);
                        dictComponents.Add(s, null);
                    }
                }
            }
            result = null;

            // Добавим вкладку и создадим на ней загружаемую форму.
            DesignSurfaceExt2 var1 = IpDesignerCore.AddDesignSurface<Form>(670, 600, AlignmentModeEnum.SnapLines, new Size(1, 1), CompNames[0]);
            Component rootComponent = (Component)var1.ComponentContainer.Components[0];

            dictComponents[CompNames[0]] = rootComponent;

            string formName = CompNames[0];
            rootComponent.GetType().GetProperty("Text").SetValue(rootComponent, formName);
            rootBlok = OneScriptFormsDesigner.ParseBetween(strOSD, @"[<" + formName + @"]", @"[" + formName + @">]");
            if (rootBlok != null)
            {
                // Установим для формы свойства.
                result = rootBlok.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                for (int i1 = 0; i1 < result.Length; i1++)
                {
                    string strCurrent = result[i1];
                    if (strCurrent.Length >= 2)
                    {
                        if (strCurrent.Substring(0, 2) != @"//")
                        {
                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, formName + ".", "=");
                            if (displayName != "КнопкаОтмена" && displayName != "КнопкаПринять" && !strCurrent.Contains("Подсказка"))
                            {
                                string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                PropValueConverter.SetPropValue(rootComponent, displayName, strPropertyValue);
                            }
                        }
                    }
                }
                propertyGrid1.Refresh();
                result = null;
            }

            // Создадим остальные компоненты но пока не устанавливаем для них свойства, так как могут быть не все родители созданы.
            IDesignSurfaceExt surface = OneScriptFormsDesigner.ActiveDesignSurface;
            for (int i = 1; i < CompNames.Count; i++)
            {
                string componentName = CompNames[i];
                string type_NameRu = componentName;
                for (int i1 = 0; i1 < 10; i1++)
                {
                    type_NameRu = type_NameRu.Replace(i1.ToString(), "");
                }

                string type_NameEn = "osfDesigner." + OneScriptFormsDesigner.namesRuEn[type_NameRu];
                Type type = Type.GetType(type_NameEn);

                if (type == typeof(osfDesigner.ImageList))
                {
                    ToolboxItem toolImageList1 = new ToolboxItem(typeof(System.Windows.Forms.ImageList));
                    Component comp1 = (Component)toolImageList1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                    // Для comp1 уже создан дублер, получим его.
                    osfDesigner.ImageList SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(comp1);
                    dictComponents[componentName] = SimilarObj;
                }
                else if (type == typeof(osfDesigner.MainMenu))
                {
                    ToolboxItem toolMainMenu1 = new ToolboxItem(typeof(System.Windows.Forms.MainMenu));
                    Component comp1 = (Component)toolMainMenu1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                    // Для comp1 уже создан дублер, получим его.
                    osfDesigner.MainMenu SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(comp1);
                    dictComponents[componentName] = SimilarObj;
                }
                else if (type == typeof(osfDesigner.TabPage))
                {
                    MessageBox.Show("osfDesigner.TabPage");

                    ////Component control = (Component)surface.CreateControl(type, new Size(200, 20), new Point(10, 200));

                    //ToolboxItem toolTabPage1 = new System.Drawing.Design.ToolboxItem(typeof(System.Windows.Forms.TabPage));
                    //Component comp1 = (Component)toolTabPage1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                    //// Для comp1 уже создан дублер, получим его.
                    //osfDesigner.TabPage SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(comp1);
                    //SimilarObj.OriginalObj = (System.Windows.Forms.TabPage)comp1;
                    ////////////////////////OneScriptFormsDesigner.AddToDictionary(comp1, SimilarObj);
                    //// присвоим дублёру значения всех свойств исходного объекта
                    //OneScriptFormsDesigner.PassProperties(comp1, SimilarObj);//без этой строки компонент глючит
                    //dictComponents[componentName] = comp1;

                }
                else if (type == typeof(osfDesigner.FileSystemWatcher) ||
                    type == typeof(osfDesigner.FolderBrowserDialog) ||
                    type == typeof(osfDesigner.ColorDialog) ||
                    type == typeof(osfDesigner.FontDialog) ||
                    type == typeof(osfDesigner.OpenFileDialog) ||
                    type == typeof(osfDesigner.SaveFileDialog) ||
                    type == typeof(osfDesigner.NotifyIcon) ||
                    type == typeof(osfDesigner.ToolTip) ||
                    type == typeof(osfDesigner.Timer))
                {
                    ToolboxItem toolComp1 = new ToolboxItem(type);
                    Component comp1 = (Component)toolComp1.CreateComponents(OneScriptFormsDesigner.DesignerHost)[0];
                    dictComponents[componentName] = comp1;
                }
                else
                {
                    Component control1 = surface.CreateControl(type, new Size(200, 20), new Point(10, 200));
                    dictComponents[componentName] = control1;
                }
                dictComponents[componentName].Site.Name = componentName;
            }

            // Установим свойства компонентов.
            for (int i = 1; i < CompNames.Count; i++)
            {
                string componentName = CompNames[i];
                Component control = dictComponents[componentName];
                ComponentBlok = OneScriptFormsDesigner.ParseBetween(strOSD, @"[<" + componentName + @"]", @"[" + componentName + @">]");
                if (ComponentBlok != null)
                {
                    result = ComponentBlok.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    for (int i1 = 0; i1 < result.Length; i1++)
                    {
                        string strCurrent = result[i1];
                        if (strCurrent.Length >= 2)
                        {
                            if (strCurrent.Substring(0, 2) != @"//")
                            {
                                if (componentName.Contains("СписокИзображений"))
                                {
                                    if (strCurrent.Contains("="))
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, null);
                                    }
                                    else
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", ".");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "Ф.Картинка(\u0022", "\u0022)");
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, null);
                                    }
                                }
                                else if (componentName.Contains("Календарь"))
                                {
                                    if (strCurrent.Contains("="))
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                        Control parent = (Control)dictComponents[parentName];
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                    }
                                    else
                                    {
                                        //Календарь1.ВыделенныеДаты.Добавить(Дата(2021, 11, 01, 00, 00, 00));
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", ".");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "Дата(", "))");
                                        string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                        Control parent = (Control)dictComponents[parentName];
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                    }
                                }
                                else if (componentName.Contains("ГлавноеМеню"))
                                {
                                    //Меню0 = ГлавноеМеню1.ЭлементыМеню.Добавить(Ф.ЭлементМеню("Меню0"));
                                    //Меню1 = Меню0.ЭлементыМеню.Добавить(Ф.ЭлементМеню("Меню1"));
                                    if (strCurrent.Contains(".ЭлементыМеню.Добавить(Ф.ЭлементМеню("))// создаем элемент меню или подменю
                                    {
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", ".Добавить(Ф.ЭлементМеню(");
                                        string strPropertyValue = strCurrent;
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, null);
                                    }
                                }
                                else if (control.GetType() == typeof(osfDesigner.FileSystemWatcher) ||
                                    control.GetType() == typeof(osfDesigner.FolderBrowserDialog) ||
                                    control.GetType() == typeof(osfDesigner.ColorDialog) ||
                                    control.GetType() == typeof(osfDesigner.FontDialog) ||
                                    control.GetType() == typeof(osfDesigner.OpenFileDialog) ||
                                    control.GetType() == typeof(osfDesigner.SaveFileDialog) ||
                                    control.GetType() == typeof(osfDesigner.NotifyIcon) ||
                                    control.GetType() == typeof(osfDesigner.ToolTip) ||
                                    control.GetType() == typeof(osfDesigner.Timer))
                                {
                                    string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                    string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                    PropValueConverter.SetPropValue(control, displayName, strPropertyValue, null);
                                }
                                else if (strCurrent.Contains("Подсказка"))
                                {
                                    //Подсказка1.УстановитьПодсказку(Форма_0, "фор");

                                    string displayName = "ToolTip на " + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".УстановитьПодсказку");
                                    string strPropertyValue = strCurrent;
                                    PropValueConverter.SetPropValue(control, displayName, strPropertyValue, null);
                                }

                                //else if (componentName.Contains("НаблюдательФайловойСистемы"))
                                //{
                                //    string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                //    string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                //    PropValueConverter.SetPropValue(control, displayName, strPropertyValue, null);
                                //}
                                else if (componentName.Contains("Дерево"))
                                {
                                    string header = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                    if (header.Contains("Дерево"))// обрабатываем как свойство дерева
                                    {
                                        if (strCurrent.Contains("Узлы"))
                                        {
                                            //Узел0 = Дерево1.Узлы.Добавить("Узел0");
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", ".");
                                            PropValueConverter.SetPropValue(control, displayName, strCurrent, null);
                                        }
                                        else
                                        {
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                            Control parent = (Control)dictComponents[parentName];
                                            PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                        }
                                    }
                                    else// обрабатываем как свойство узла
                                    {
                                        if (strCurrent.Contains("Узлы"))
                                        {
                                            //Узел1 = Узел0.Узлы.Добавить("Узел1");
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", ".");
                                            PropValueConverter.SetPropValue(control, displayName, strCurrent, null);
                                        }
                                        else
                                        {
                                            //Узел3.ШрифтУзла = Ф.Шрифт("Microsoft Sans Serif", 7.8, Ф.СтильШрифта.Жирный);
                                            string displayName = "Узлы";
                                            PropValueConverter.SetPropValue(control, displayName, strCurrent, null);
                                        }
                                    }
                                }
                                else
                                {
                                    string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                    string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                    string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                    Control parent = (Control)dictComponents[parentName];
                                    PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                }
                            }
                        }
                    }
                    propertyGrid1.Refresh();
                    result = null;
                    ComponentBlok = null;
                }

                // если компонент Дерево, нужно найти узел с самым большим номером в имени и занести его в OneScriptFormsDesigner.hashtableNodeName
                // чтобы нумерация вновь создаваемых узлов не повторялась
                if (componentName.Contains("Дерево"))
                {
                    string maxNodeName = "Узел0";
                    // найдем компонент по имени
                    osfDesigner.TreeView TreeView1 = null;
                    IDesignerEventService des = (IDesignerEventService)pDesigner.DSME.GetService(typeof(IDesignerEventService));
                    if (des != null)
                    {
                        ComponentCollection components1 = des.ActiveDesigner.Container.Components;
                        foreach (Component comp in components1)
                        {
                            if (comp.Site.Name == componentName)
                            {
                                TreeView1 = (osfDesigner.TreeView)comp;
                                break;
                            }
                        }
                        if (TreeView1 != null)
                        {
                            maxNodeName = MaxNodeSearch(TreeView1, ref maxNodeName, null);
                            string nodeName = "";
                            if (maxNodeName != "Узел0")
                            {
                                while (maxNodeName != nodeName)
                                {
                                    nodeName = OneScriptFormsDesigner.RevertNodeName(TreeView1);
                                }
                            }
                        }
                    }
                }
            }

            // если для формы заданы КнопкаОтмена и/или КнопкаПринять, установим их
            if (rootBlok != null)
            {
                // Установим для формы свойства.
                result = rootBlok.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                for (int i1 = 0; i1 < result.Length; i1++)
                {
                    string strCurrent = result[i1];
                    if (strCurrent.Length >= 2)
                    {
                        if (strCurrent.Substring(0, 2) != @"//")
                        {
                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, formName + ".", "=");
                            if (displayName == "КнопкаОтмена" || displayName == "КнопкаПринять")
                            {
                                string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                PropValueConverter.SetPropValue(rootComponent, displayName, strPropertyValue);
                            }

                            if (strCurrent.Contains("Подсказка"))
                            {
                                displayName = "ToolTip на " + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".УстановитьПодсказку");
                                string strPropertyValue = strCurrent;
                                PropValueConverter.SetPropValue(rootComponent, displayName, strPropertyValue);
                            }
                        }
                    }
                }
                propertyGrid1.Refresh();
            }

            ComponentCollection ctrlsExisting = OneScriptFormsDesigner.DesignerHost.Container.Components;
            ISelectionService iSel = (ISelectionService)OneScriptFormsDesigner.DesignerHost.GetService(typeof(ISelectionService));
            if (iSel == null)
            {
                return;
            }
            iSel.SetSelectedComponents(new IComponent[] { ctrlsExisting[0] });

            OneScriptFormsDesigner.PropertyGridHost.ReloadTreeView();
            OneScriptFormsDesigner.PropertyGridHost.ChangeSelectNode((Component)ctrlsExisting[0]);

        }

        public static string MaxNodeSearch(osfDesigner.TreeView treeView, ref string maxNodeName, TreeNodeCollection treeNodes = null)
        {
            TreeNodeCollection _treeNodes;
            if (treeNodes == null)
            {
                _treeNodes = treeView.Nodes;
            }
            else
            {
                _treeNodes = treeNodes;
            }
            MyTreeNode treeNode = null;
            for (int i = 0; i < _treeNodes.Count; i++)
            {
                treeNode = (MyTreeNode)_treeNodes[i];
                int numTreeNodeName = Int32.Parse(treeNode.Name.Replace("Узел", ""));
                int num_maxNodeName = Int32.Parse(maxNodeName.Replace("Узел", ""));
                if (numTreeNodeName > num_maxNodeName)
                {
                    maxNodeName = treeNode.Name;
                }
                if (treeNode.Nodes.Count > 0)
                {
                    MaxNodeSearch(treeView, ref maxNodeName, treeNode.Nodes);
                }
            }
            return maxNodeName;
        }

        private void _form_Click(object sender, EventArgs e)
        {
            pDesigner.SplitterpDesigner.Visible = true;
            pDesigner.CodePanel.Visible = false;
            _edit.Enabled = true;//"Правка"
            _tools.Enabled = true;//"Инструменты"
            pDesigner.SplitterpDesigner.Panel2Collapsed = false;
            pnl4Toolbox.Visible = true;
            _form.Enabled = false;
            _code.Enabled = true;
            _form.CheckState = System.Windows.Forms.CheckState.Checked;
            _code.CheckState = System.Windows.Forms.CheckState.Unchecked;
        }

        private void _code_Click(object sender, EventArgs e)
        {
            SaveScript.comps.Clear();
            pDesigner.SplitterpDesigner.Visible = false;
            pDesigner.CodePanel.Visible = true;
            _edit.Enabled = false;//"Правка"
            _tools.Enabled = false;//"Инструменты"
            pDesigner.SplitterpDesigner.Panel2Collapsed = true;
            pnl4Toolbox.Visible = false;
            _form.Enabled = true;
            _code.Enabled = false;
            _form.CheckState = System.Windows.Forms.CheckState.Unchecked;
            _code.CheckState = System.Windows.Forms.CheckState.Checked;
            pDesigner.RichTextBox.Text = SaveScript.GetScriptText();
        }

        public string Version
        {
            get
            {
                if (string.IsNullOrEmpty(_version))
                {
                    // Получение версии файла запущенной сборки
                    System.Diagnostics.FileVersionInfo FVI = System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
                    _version = FVI.ProductVersion;
                }
                return _version;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void ChangeImage(bool change)
        {
            if (change)
            {
                _tabOrder1.CheckState = System.Windows.Forms.CheckState.Checked;
            }
            else
            {
                _tabOrder1.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
        }

        public Control GetmainForm()
        {
            return this;
        }

        private void pDesignerMainForm_Load(object sender, EventArgs e)
        {
            // Таймер для обеспечения срабатывания по правой кнопке мыши сворачивания раскрытого свойства СписокИзображений.
            timerLoad = new System.Windows.Forms.Timer();
            timerLoad.Enabled = true;
            timerLoad.Tick += new System.EventHandler(timerLoad_Tick);
        }

        private void _generateScript_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.OverwritePrompt = true;

            saveFileDialog1.Filter = "OS files(*.os)|*.os|All files(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            SaveScript.comps.Clear();
            File.WriteAllText(saveFileDialog1.FileName, SaveScript.GetScriptText());
        }

        private void _unDo_Click(object sender, EventArgs e)
        {
            IpDesignerCore.UndoOnDesignSurface();
        }

        private void _reDo_Click(object sender, EventArgs e)
        {
            IpDesignerCore.RedoOnDesignSurface();
        }

        private void OnMenuClick(object sender, EventArgs e)
        {
            string cmd = (sender as ToolStripMenuItem).Text;
            if (cmd == "Вырезать")
            {
                IpDesignerCore.CutOnDesignSurface();
            }
            else if (cmd == "Копировать")
            {
                IpDesignerCore.CopyOnDesignSurface();
            }
            else if (cmd == "Вставить")
            {
                IpDesignerCore.PasteOnDesignSurface();
            }
            else if (cmd == "Удалить")
            {
                IpDesignerCore.DeleteOnDesignSurface();
            }
        }

        private void _tabOrder_Click(object sender, EventArgs e)
        {
            IpDesignerCore.SwitchTabOrder();

            if (_tabOrder.CheckState == System.Windows.Forms.CheckState.Unchecked)
            {
                _tabOrder.CheckState = System.Windows.Forms.CheckState.Checked;
            }
            else if (_tabOrder.CheckState == System.Windows.Forms.CheckState.Checked)
            {
                _tabOrder.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
        }

        private void _about_Click(object sender, EventArgs e)
        {
            string str1 = "Дизайнер форм от ahyahy " + Environment.NewLine + "Версия 1.0.0.0 " + Environment.NewLine + "(Создана на основе программы: " + Environment.NewLine + "picoFormDesigner coded by Paolo Foti " + Environment.NewLine + "Version is: " + Version + ")";
            MessageBox.Show(str1, "Дизайнер форм для OneScriptForms", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        //* 17.12.2021 perfolenta

        private bool ГотовоКЗакрытию()
        {
            if (pDesignerCore.Dirty)
            {
                string str1 = "Редактируемая форма изменена! Изменения будут потеряны!\n\nВыйти из конструктора форм?";
                if (MessageBox.Show(str1, "Дизайнер форм для OneScriptForms", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                    return false;
            }
            return true;
        }

        private void pDesignerMainForm_Closing(object sender, CancelEventArgs e)
        {
            if (!ГотовоКЗакрытию())
            {
                e.Cancel = true;
                return;
            }

            if (DestroyDesignSurfaces())
                e.Cancel = false;
            else
                e.Cancel = true;

        }

        private bool DestroyDesignSurfaces()
        {
            //????????????? тут надо уничтожить все DesignSurfaces и вернуть успешность этой операции

            return true;
        }

        //***
    }
}
