using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.Design;
using osfDesigner.Properties;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace osfDesigner
{
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public class pDesignerMainFormPFL : System.Windows.Forms.Form, IDesignerMainForm
    {
        private string _version = string.Empty;
        public pDesigner pDesignerCore = new pDesigner();
        private IpDesigner IpDesignerCore = null;
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;

        private System.Windows.Forms.ToolStripMenuItem _file;
        private System.Windows.Forms.ToolStripMenuItem _generateScript;

        private System.Windows.Forms.ToolStripSeparator _stripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem _loadForm;
        private System.Windows.Forms.ToolStripMenuItem _saveForm;
        private System.Windows.Forms.ToolStripSeparator _stripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem _exit;

        private System.Windows.Forms.ToolStripMenuItem _edit;
        private System.Windows.Forms.ToolStripMenuItem _unDo;
        private System.Windows.Forms.ToolStripMenuItem _reDo;
        private System.Windows.Forms.ToolStripSeparator _stripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem _cut;
        private System.Windows.Forms.ToolStripMenuItem _copy;
        private System.Windows.Forms.ToolStripMenuItem _paste;
        private System.Windows.Forms.ToolStripMenuItem _delete;

        private System.Windows.Forms.ToolStripMenuItem _view;
        private System.Windows.Forms.ToolStripMenuItem _form;
        private System.Windows.Forms.ToolStripMenuItem _code;

        private System.Windows.Forms.ToolStripMenuItem _tools;
        private System.Windows.Forms.ToolStripMenuItem _tabOrder;
        private static System.Windows.Forms.ToolStripMenuItem _tabOrder1;

        private System.Windows.Forms.ToolStripSeparator _stripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem _run;

        private System.Windows.Forms.ToolStripSeparator _stripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem _settings;

        private System.Windows.Forms.ToolStripMenuItem _help;
        private System.Windows.Forms.ToolStripMenuItem _about;

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

        private void timerLoad_Tick(object sender, System.EventArgs e)
        {
            timerLoad.Stop();
            DesignSurfaceManagerExt DesignSurfaceManagerExt = pDesigner.DSME;
            propertyGrid1 = DesignSurfaceManagerExt.PropertyGridHost.PropertyGrid;
            try
            {
                // это не удается, если вызывается непосредственно при загрузке, так как элемент управления не завершил создание самого себя: 
                Application.AddMessageFilter(new PropertyGridMessageFilter(propertyGrid1.GetChildAtPoint(new Point(40, 40)), new MouseEventHandler(propGridView_MouseUp)));
            }
            catch
            {
            }
        }

        private void propGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && (
                propertyGrid1.SelectedGridItem.Label == "СписокИзображений" ||
                propertyGrid1.SelectedGridItem.Label == "СписокБольшихИзображений" ||
                propertyGrid1.SelectedGridItem.Label == "СписокМаленькихИзображений" ||
                propertyGrid1.SelectedGridItem.Label == "DoubleBuffered"))
            {
                // пользователь щелкнул левой кнопкой мыши по свойству, чтобы увидеть контекстное меню:
                try
                {
                    propertyGrid1.SelectedGridItem.Expanded = false;
                }
                catch
                {
                }
            }
        }

        public pDesignerMainFormPFL()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pDesignerMainFormPFL));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this._file = new System.Windows.Forms.ToolStripMenuItem();
            this._generateScript = new System.Windows.Forms.ToolStripMenuItem();
            this._stripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._loadForm = new System.Windows.Forms.ToolStripMenuItem();
            this._saveForm = new System.Windows.Forms.ToolStripMenuItem();
            this._stripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this._exit = new System.Windows.Forms.ToolStripMenuItem();

            this._edit = new System.Windows.Forms.ToolStripMenuItem();
            this._unDo = new System.Windows.Forms.ToolStripMenuItem();
            this._reDo = new System.Windows.Forms.ToolStripMenuItem();
            this._stripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._cut = new System.Windows.Forms.ToolStripMenuItem();
            this._copy = new System.Windows.Forms.ToolStripMenuItem();
            this._paste = new System.Windows.Forms.ToolStripMenuItem();
            this._delete = new System.Windows.Forms.ToolStripMenuItem();

            this._view = new System.Windows.Forms.ToolStripMenuItem();
            this._form = new System.Windows.Forms.ToolStripMenuItem();
            this._code = new System.Windows.Forms.ToolStripMenuItem();

            this._tools = new System.Windows.Forms.ToolStripMenuItem();
            this._tabOrder = new System.Windows.Forms.ToolStripMenuItem();
            osfDesigner.pDesignerMainFormPFL._tabOrder1 = this._tabOrder;

            this._stripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this._run = new System.Windows.Forms.ToolStripMenuItem();

            this._stripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this._settings = new System.Windows.Forms.ToolStripMenuItem();

            this._help = new System.Windows.Forms.ToolStripMenuItem();
            this._about = new System.Windows.Forms.ToolStripMenuItem();
            this.pnl4Toolbox = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.pnl4pDesigner = new System.Windows.Forms.Panel();
            this.pnl4splitter = new System.Windows.Forms.Splitter();
            this.menuStrip1.SuspendLayout();
            this.pnl4Toolbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._file,
            this._edit,
            this._view,
            this._tools,
            this._help});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // _file
            // 
            this._file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._generateScript,
            this._stripSeparator2,
            this._loadForm,
            this._saveForm,
            this._stripSeparator4,
            this._exit});
            this._file.Name = "_file";
            this._file.Size = new System.Drawing.Size(54, 24);
            this._file.Text = "Файл";
            // 
            // _generateScript
            // 
            string str_generateScript = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABzklEQVR42u2aMU7DMBSGn+NCGRjgDjAxInEIxMgtWBiYGEoAqQsgWLgFEyBAcAMkkBhYgDtQRJFomzTYQBzjkiZO4jiO/A+VWjkv/xf7vby4QRApALOE2IeB5hkE4s0f3fZQjmClaX2pyTwzAFPMixAWwAKoAjg+f6lUZVpbmfvjTxqg++mPBJ2ewvUBEE+gWoUvoeedBURiVAcgQ0CwAKoAqLnfMZA0hspV1FvNcB6VzAAPcHdxEju26c9CD79KxV9cXpUDkElimsD89ySALKIAbtQ9Q+i7EABaQvll1iFX5+n6Bt69NwtQOgAVOVHwcHYFHu7qA+DFm+PFVyWZJC4dII1qB1CpJSQDECax9hnIW4WMBaByq5ADFkA3gKwqlwNGA8TdedPK1b2EagEw33qMTWKx/68lwP3pJQSNDz0AVOPKaNIeUOWfB0SADrdfX4T+W35KyygFODhsgz8cst+8YEACp+NCnK/NjW09ALt7LQLgZzp+EjcsQG6A9v4W9H3PAmgFAHAIRF/6eOw4gJEzFkJ5MxdB/Eh2NiYwjq1G4bai0OIU/x9Z0feDUOGeqHKAvE2gpGoGUAREGQD0gUl81YDRGCg0UpYM0rf3L8g8IyNDfeOmAAAAAElFTkSuQmCC";
            this._generateScript.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_generateScript);
            this._generateScript.Name = "_generateScript";
            this._generateScript.Size = new System.Drawing.Size(221, 26);
            this._generateScript.Text = "Сформировать сценарий";
            this._generateScript.Click += _generateScript_Click;
            // 
            // _stripSeparator2
            // 
            this._stripSeparator2.Name = "_stripSeparator2";
            // 
            // _loadForm
            // 
            string str_loadForm = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAA/klEQVR42u3Z0QqDMAwFUDO2vez/f3UwN+iqOKE2qdUVe2NzoS/6kmMMVEvdSUK1CzBIAuI0P4ihkLtfr0IPoypkTyfgMBHEZRRFMf7h1xMGkoNIYI6otTwEADPMdl8EAoAJLv4FOaBix19WBNmCgYdMRRpEailixt0JC1GECDRtQdDmZzk3WRA0BIeJIBq6sQuCijAIWlaHnRTORzc1wIX38bthEMScAsLthsXfQagIBjLvRgxiEIPIn70ShFC/qhaQy69+FoLaDQYS/GNQsbdqE6IMYZDqkV4rgwAgGoGgYoRTsTRESdLHCorCQq5+vWtXtiE3vz6rMq0xCFq+0/uOMfSQbkgAAAAASUVORK5CYII=";
            this._loadForm.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_loadForm);
            this._loadForm.Name = "_loadForm";
            this._loadForm.Text = "Открыть форму";
            this._loadForm.Click += _loadForm_Click;
            // 
            // _saveForm
            // 
            string str_saveForm = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABBUlEQVR42u3awQ6CMBAEUPai/v/f6qXCgYTAbruF0p3qTMIBgW1fS4RYZfqRSHQHCKmAJNB+uS94zNs7ELHmOW+fK5DImTiN2UPSbkeUC5J17GpEH0QXxoRYHQ2ALHlNhdu9O0Tr7FprW1s5LzszsBDjXHNmwiDbUbcgNRh4iBczBMTAiLmDDClhhoIomHjIrlFCwiAeZFdIa4y3bjOIUqxpSoPTFHIXxjPDzSFRIQQthKBliG+tE+3iPkcq28Z7sntrD/Gu5alPCCGEEEIIIYQQohTuDSksNfwhpNTQ3RDruBtiFYuE1Pwaf8AAJ7s+sgTlDwO5HBZGc7cH6syI+8MRQwhavjI5HUJUEs5VAAAAAElFTkSuQmCC";
            this._saveForm.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_saveForm);
            this._saveForm.Name = "_saveForm";
            this._saveForm.Text = "Сохранить форму";
            this._saveForm.Click += _saveForm_Click;
            // 
            // _stripSeparator4
            // 
            this._stripSeparator4.Name = "_stripSeparator4";
            // 
            // _exit
            // 
            this._exit.Name = "_exit";
            this._exit.Text = "Выход";
            this._exit.Click += _exit_Click;
            // 
            // _edit
            // 
            this._edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._unDo,
            this._reDo,
            this._stripSeparator3,
            this._cut,
            this._copy,
            this._paste,
            this._delete});
            this._edit.Name = "_edit";
            this._edit.Size = new System.Drawing.Size(69, 24);
            this._edit.Text = "Правка";
            // 
            // _unDo
            // 
            string str_unDo = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAA2klEQVR42u3YSw6AIAwEULz/oTGamBAjyqdlZky7YSfzRH5uOWnXFoAABMDoQSnlfDaCgCP80UoCrvCSgDK8HOAeXgrwFF4GUAsvAXgLTw/4Ck8NaAk/WrPoT4BneAvIK2BV+BlIFYAIP4KgBPQg6D6hXgTNJHYDsCNgG1nrSzEDtHTavQQaIOCHudmRpThOz4wsxYUGAnjqWA5w71wSUAaQmcS1IBLLqFVBNjLvsL8DmB3mAjAAMLnQoAAmV0oUwOxSvxrg8ltlBcD9xxZ7BQBdAUBXANAlD9gBBDWIAQ4VHAYAAAAASUVORK5CYII=";
            this._unDo.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_unDo);
            this._unDo.Name = "_unDo";
            this._unDo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this._unDo.Size = new System.Drawing.Size(212, 26);
            this._unDo.Text = "Отменить";
            this._unDo.Click += _unDo_Click;
            // 
            // _reDo
            // 
            string str_reDo = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAA10lEQVR42u3YSw7EIAwDUHr/QzOaXqD52IkjJRt21A+JEnjumV3PAhawgIaPnnPvOwwG/EcEohWAQLQDsggJQAYhA4gipAARhBzAi5AEeBBpgCVMtCyIMIAZ3INwA6qCWxEuQEf4L4QZ0Bk+DVANPwKQ3sTK4aEA1+lZdZAxwlvmhbUSyJWyzgtt5pArZZkX3k5XAigXmioA7UpZAaBe6tmbmP6swvyNlj1ssRCoorYSFShqM7cAFCCKkAJEEHIAL0IS4IFIAyyQEYDuWkB3LaC7FtBd4wE/1ESIAWn6qDIAAAAASUVORK5CYII=";
            this._reDo.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_reDo);
            this._reDo.Name = "_reDo";
            this._reDo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this._reDo.Size = new System.Drawing.Size(212, 26);
            this._reDo.Text = "Вернуть";
            this._reDo.Click += _reDo_Click;
            // 
            // _stripSeparator3
            // 
            this._stripSeparator3.Name = "_stripSeparator3";
            // 
            // _cut
            // 
            string str_cut = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABNUlEQVR42u2Y0Q7CIAxF4f8/ekYjZpmU3sItlIS+OTI9BxDa5ivtHfkIHIEjsHc0BXJK1eHrMxQjqgISeESRPwEUPooEJHCHfI6HF6gBektokwgLiC/9hnkCyNatTmYEgZFDY+kWshwY8BbSACPBiwKWH7AKsMBVAQ8JNrwqwBLxADcJWECeAJ7wZgEEqkDMSkm60+nWbTkznxqqB2qXnveWoQoU4Br8+9mMeoJWkQmr8f3IB6cKSP+H8vy+QvTMlbGFWuPS9goh0APPlqAeoyg8U8QsYLmNZzQHYIHuislZQhVgACxJ5jxmzuU7Zze23AsarWRklJRMiS2KelNNDLZVaI2t0dVYLmARcekLUfMaoNIbFgjf3NVmIhK8KIBIRIBvCrREosBDAtHjCKyOI7A6thd4ARhzzAEzNxSrAAAAAElFTkSuQmCC";
            this._cut.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_cut);
            this._cut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._cut.Name = "_cut";
            this._cut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this._cut.Size = new System.Drawing.Size(212, 26);
            this._cut.Text = "Вырезать";
            this._cut.Click += OnMenuClick;
            // 
            // _copy
            // 
            string str_copy = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABOElEQVR42u3WrQ7CMBAH8DUIBAaD4AnAYtCEZ8DxEih4BhwejeMZCBoECRiQ6CUEMwghY7AlJCPpPq7tbddwfzOxNe2vubtMOJZHlH0ABqS8CwrYwwoAKiIT0F94oM1Xw1q0buf6otOoxC8BBYEGOHsvcbkFDjYCFRA+sRHoAGxEIQBMRGEALIQRwPfQBgIGUQOAEUYBs80DtPmoW43WHa++aNfVSosEIOyX+zNwVBBkAOFTBQECZNW6LkAFQQ4ARZAqIRUEWYAEoQ84rLfSD915TwuQMwww3sRAJC3A52Cy9bgASAkByyPxIhiAOUbzhAGqTTxdnnIfcjJo0QPIpkxW0KdQUmQlBGnitNJjgAbAaY73eQ0/ifcEAyAhOUZtBkBC8lfivwGmwgAG2ARADgNIhgFlx3rAG9GomUA3I+5MAAAAAElFTkSuQmCC";
            this._copy.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_copy);
            this._copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._copy.Name = "_copy";
            this._copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this._copy.Size = new System.Drawing.Size(212, 26);
            this._copy.Text = "Копировать";
            this._copy.Click += OnMenuClick;
            // 
            // _paste
            // 
            string str_paste = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABIklEQVR42u3awQnCMBQG4OTkFL07gY6Rg6eCriAFcQoRiiso9CbFKcQNvIpLeHoWaaGWtEmapH2h77/12b68DxPxUM4CDx97ADSAY5ZHu1i86rX0fJsnG/FED5ANX+V0uUbb9eqNGsAYQK0lb6+hBzQHbasPC4DOy/92LQCtpXshVQ+BVqmBOGT5ch+Le4+ljRFdD4BR2Tjc+AMrAAD87i02iRNBsbnKfrzZzz2gGt4HQILwDeCs+3BqLcuh1mJQgI8QgAAEGBkg+S334vQKSB8fb99SspgBAQhAAAJMCFAuaBVJPwJoA1yHAAQIDWB6iFV4AkxuCxGAAJaA4P9KBA9wHWuAChEEQBWMACMEVoA2AjNAJyjPwLQAvoZXzRr82ypful5dQCOEOe0AAAAASUVORK5CYII=";
            this._paste.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_paste);
            this._paste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._paste.Name = "_paste";
            this._paste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this._paste.Size = new System.Drawing.Size(212, 26);
            this._paste.Text = "Вставить";
            this._paste.Click += OnMenuClick;
            // 
            // _delete
            // 
            string str_delete = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAABmJLR0QA/wD/AP+gvaeTAAACJUlEQVRoge2YvW/TQBiHn7sjY9UFiSIYOjSiVGyt5HYAUkLyR0NVRwgJkBiRGGAEqaIsSQZKFfsYiFFUXXJ3vtdIiHtG34d/j+9svzZkMplMJhPIfFgcTEdFv6v5p4OTvfmwOIgZo0M7zsaH+wp7Zmr7av78+GF8vM1MR0XfmGqisOV8fPQodJwK6TQbH+7rypwDd5eHLqzSz7bO3n5sE/Ym01HRN7UtgXvLQ9+sqYdbL95/8I31CjjCN4hIOMI3BEl4t1DvuncF/HQ07Shbn6dspw3hAWrqW5VvjqAt9GNwvFuZugR2Hc2tVsITPnjOIAGQlZAKDxECICMhGR4iBSBNQjo8tBCAdhJdhIeWAhAn0VV4SBCAMIla2UVX4SFRADwSikssNXDHMVTkRZgsAN6VcCFWiogIQJSEaB0lJgC/JRamfq3c+x0L31H6iVR4iCinQ7ju2Z62mHXtoldriZhA86i0ip0N3W6nFoA3Ebkonue8C7H7IHkFNoa3XAIXjmHJpXhDkoD3Dav106oyj4EvjnYRidZbKKY8mA5O9oypSuC+r28srQTa1DZdSUQLpBRmXUhECUhUlUuJScocqwTfxFIl8fbkzedKq1Pgq6M5+sYOWoEu6vm/9lHf5ceIxNzeLaQWlV7TL/ltuv3y3acN20mjF2vrqj/5Qk40Oz16oLUu+Rd/LTasSCjJ8A0rEr3Q8NF0/nt9VPRjf69nMplM5v/mF/i6x8b172ZWAAAAAElFTkSuQmCC";
            this._delete.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_delete);
            this._delete.Name = "_delete";
            this._delete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this._delete.Size = new System.Drawing.Size(212, 26);
            this._delete.Text = "Удалить";
            this._delete.Click += OnMenuClick;
            // 
            // _view
            // 
            this._view.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._form,
            this._code});
            this._view.Name = "_view";
            this._view.Size = new System.Drawing.Size(50, 24);
            this._view.Text = "Вид";
            // 
            // _form
            // 
            string str_form = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAA1klEQVR42u3a4QqCMBQF4EaB9R69/wv5IAktavfHYippebVzdz0HJvhjcj5EpsxwcJKALkDIBOSJLqMxyKFJo0O3UeYskPJOCOiGbvVlLgLIJwJp07iiWynTCuSexgndRJkokJjGEd1EmQchxkKItYwgtb2y5DWQECshxFr2C0F/q8z1IoQQQlaCWA0hP119iwwKbgf5x8NUlCSEEEJQkDlMWDjvw/z9ruyEEELI9ASrIcRafELKrbdaIXG4q1sTptfb1fa0mx8GctCL3tK8f+FwEUKs5QXoOIWG//RH0wAAAABJRU5ErkJggg==";
            this._form.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_form);
            this._form.Name = "_form";
            this._form.Size = new System.Drawing.Size(50, 24);
            this._form.Text = "Форма";
            this._form.Click += _form_Click;
            this._form.Enabled = false;
            this._form.CheckState = System.Windows.Forms.CheckState.Checked;
            // 
            // _code
            // 
            string str_code = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAAvElEQVR42u3WQQqDMBSE4eQ2FWk3Hsoz9VBd6cLb6BNcdSE1IWby+g8ICYjOpz4wru8heEj8C0gcP+sNHSbr8PIA2bNYj644xM6JJdp/PagsjApkseMRMj4zCch+fVvPtuxD4puRgRz7ZIwUJAcjB0nFSEJSMLKQqxgJyI+ZrcfTA+S0R1XIVTAQIC1BSv3in90LCJCWIHcGCJAWIW6GHQgQhh0IkCoQN8MOBAjDDgQIECBAKkNUAqSluIFsV0sN9+kjczYAAAAASUVORK5CYII=";
            this._code.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_code);
            this._code.Name = "_code";
            this._code.Size = new System.Drawing.Size(50, 24);
            this._code.Text = "Скрипт";
            this._code.Click += _code_Click;
            this._code.CheckState = System.Windows.Forms.CheckState.Unchecked;
            // 
            // _tools
            // 
            this._tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._tabOrder,
            this._stripSeparator5,
            this._run,
            this._stripSeparator6,
            this._settings});
            this._tools.Name = "_tools";
            this._tools.Size = new System.Drawing.Size(113, 24);
            this._tools.Text = "Инструменты";
            // 
            // _tabOrder
            // 
            string str_tabOrder = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAA3ElEQVR42u3ZSw6AIAwEULj/oTExcdOApjD9menWiPOiQNU+Wu3qaEBvbXvIcZ8eCDgJv4v4BMhQqwsgwsMBq1DyIqjwIQBkeAJ2AGgEJ/FbuK/BUy6jGsApwmwj0wC8awqYpBzrQ9jS3j4CCLAATEC1JjEBlQHo5u6pZQuTrZXQItI1c1BARDvtDvAKT8DbQGkfoVm4UpNYhiu5jGoAlgiXjSyiCIiu/wJE0npvZAQQcAgQGPONbHeBSNVKmHwb9Wzm4ADvdjoEEPl3hgA0gpP4tFIuo14I040sc11VMcAB84B/6gAAAABJRU5ErkJggg==";
            this._tabOrder.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_tabOrder);
            this._tabOrder.Name = "_tabOrder";
            this._tabOrder.Size = new System.Drawing.Size(217, 26);
            this._tabOrder.Text = "Порядок обхода";
            this._tabOrder.Click += _tabOrder_Click;
            // 
            // _stripSeparator5
            // 
            this._stripSeparator5.Name = "_stripSeparator5";
            // 
            // _run
            // 
            string str_run = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABkUlEQVR42t3Z0Q7CIAwFUPv/H42JmWYD2t4WCkUejDrAHjsGbPT6k0K7AwBL0RyZIVrw6SEWQEoIDyjVoTbsFBAcwIe+FeIDJIO0kSLBJ4OMIxJAnhF7AB3E7+UoxGaIjPgGZh/kSyE8gjuzDZfd5s1mBDX1exgmG9EQ7HRqYygghP+QAMFjhGxEQfR5Yhwiq5YgdIw5G7MhthnbMthJP4tmQXzLDuTyC2QDhdinYO8CENh3sF1sR2g4MFbp4DEIqYK+wKt/bCOCq3QcoldxD4LGr6g2SFKEDpFKIoTUSI4yGQJtOG+LGoSwNJY3PJsR6yFBiLWQQATa0ey7gtMRSGflLnlURjHkXrDGQcwYci9Y4yEwprcKAHZ6URAfhlvKrIaUK26q9tIFadx0Ztt7T4OUKt4hjJypOEhh1lgSJh2kl4n7dzVG+EOQWzoxEA2hYPhdZn+QT0d8OkQywWRGKtIEE3K/WQ1aGPweSNjdf5IGuBPBYUKfxbBjYRBRQ8KfjLEZGUQsLyrkBMQVJzQRpi9vUR3TM0g50FMAAAAASUVORK5CYII=";
            this._run.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_run);
            this._run.Name = "_run";
            this._run.Text = "Запуск";
            this._run.Click += _run_Click;
            this._run.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F6)));
            this._run.ShowShortcutKeys = true;
            // 
            // _stripSeparator6
            // 
            this._stripSeparator6.Name = "_stripSeparator6";
            // 
            // _settings
            // 
            string str_settings = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABI0lEQVR42u3YwQ6EMAgEUP3/j3aTPRhFaGkLOLNZrqKZp9JY92P7jdr/kKgA29aNcHzbQCEeADTEAlzDXns8iHKIhpBBZxClEInQQs4iXoNEI8og2YhySCRCXjMd0gq6iqCGWItGGSTitWot3zQQ2fvajFhBPRh1JpCGXR7Xeqzz4SAaRisLCAXpYTxPCQZigbyLRCrEEyrqxqRBKhFpkGxECeQNRDhkBNHbLXq2xCmQVUSvup8wEZBMRNnPhxVExPyEQFAQSxAkxDQEDTEFQUQMQ1ARQxBkhBuCjnBBGBBdCAuiCWFCmBA2hAphRDwgrIgbhBlxQtgRKoQRMQRBRpwQGbT1ixIRYUKuYRkQN4gWWhYq4gFpYZARKkQDoSOaELb6AFfXABC6bvmCAAAAAElFTkSuQmCC";
            this._settings.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_settings);
            this._settings.Name = "_settings";
            this._settings.Text = "Параметры";
            this._settings.Click += _settings_Click;
            // 
            // _help
            // 
            this._help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this._about });
            this._help.Name = "_help";
            this._help.Size = new System.Drawing.Size(77, 24);
            this._help.Text = "Помощь";
            // 
            // _about
            // 
            string str_about = "iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAABCklEQVR42u3WwRKDIAyE4fLmvjntpRenSDbZTaBDzo7+n4NCu157TzuAA1gE8LlPB69vSwDQcDbGDWCEMyAwQBEeQUAAdbwHYQZkxaMIEyA7HkFMAVXxVsQjwBvfBw9tzvs9IaiAbl23xE1vCFDFsxEUABrvQUCAjHgUMEJIAd9AyzU0ABL/FHcPY/2Z7ogwYBS3FcCL3AbA3NjSAawPuATAjk8FKOKXAETi0wCq+FIAI/4AVpgpYGXEpTiN/hrVEjIDIois////A7yIjOODFDBCKN7+FMBCqOJNgAgiOrN4M6ACYYmHAJkIazwMyEAg8S6ACoKGhwEsiDecBvBCouF0QNUcQPVsD3gDeqycMcHL1j4AAAAASUVORK5CYII=";
            this._about.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_about);
            this._about.Name = "_about";
            this._about.Size = new System.Drawing.Size(187, 26);
            this._about.Text = "О программе...";
            this._about.Click += _about_Click;
            // 
            // pnl4Toolbox
            // 
            this.pnl4Toolbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl4Toolbox.Controls.Add(this.listBox1);
            this.pnl4Toolbox.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl4Toolbox.Location = new System.Drawing.Point(0, 26);
            this.pnl4Toolbox.Name = "pnl4Toolbox";
            this.pnl4Toolbox.Size = new System.Drawing.Size(163, 489);
            this.pnl4Toolbox.TabIndex = 2;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(159, 485);
            this.listBox1.TabIndex = 0;
            // 
            // pnl4pDesigner
            // 
            this.pnl4pDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl4pDesigner.Location = new System.Drawing.Point(163, 26);
            this.pnl4pDesigner.Name = "pnl4pDesigner";
            this.pnl4pDesigner.Size = new System.Drawing.Size(726, 489);
            this.pnl4pDesigner.TabIndex = 3;
            // 
            // pnl4splitter
            // 
            this.pnl4splitter.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnl4splitter.Location = new System.Drawing.Point(163, 26);
            this.pnl4splitter.Name = "pnl4splitter";
            this.pnl4splitter.Size = new System.Drawing.Size(5, 489);
            this.pnl4splitter.TabIndex = 4;
            this.pnl4splitter.TabStop = false;
            // 
            // pDesignerMainFormPFL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 515);
            this.Controls.Add(this.pnl4splitter);
            this.Controls.Add(this.pnl4pDesigner);
            this.Controls.Add(this.pnl4Toolbox);
            this.Controls.Add(this.menuStrip1);
            string str_Icon = "AAABAAEAAAAQAAEABABooAAAFgAAACgйQAAAAIAAAEABйAAKййAAEййAAAAACAAACAAAAAgIAAgAAAAIAAgACAgAAAgICAAMDAwAAAAP8AAP8AAAD//wD/AAAA/wD/AP//AAD///8ккккккккккккккккккккккккккккккккккккккккккйййййAAAеееец////8ййAAAееееццййAAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDееццццwййеццййAPееццццййDец/////8ййAеецццц8ййPец/////wййDееццццwййеццййAPееццццййDец/////8ййAеецццц8ййPец/////wййDееццццwййеццййAPееццццййDец/////8ййAеецццц8ййPец/////wййDееццццwййеццййAPееццццййDец/////8ййAеецццц8ййPец/////wййDееццццwййеццййAPее//8Aццц//ййDццц/8Pцццц//8ййAее//8AAP8йййййPAAAADцц/8ADцццц//wййDгкййAAAP8AAAD/ййAAAADwйй8AAAAADц/////8AAAцццц//ййAPццц/wкййAAAP8AAAAA/wййAAAPййDwйц////8AAAAPцццц8ййAццц/wкййAAAP8йP8ййAAA8ййP/wAAAADц///8AAAAADццццwййDццц/кййAAAP8йAD/ййD//wййAP//AAAAц//8йAццццййAPцццкййAAAP8йAAA/wйAAD///ййй//AADц/8йAAPг/8ййAццц8кййAAP8йAAAAP8йA//йййAAAA//8Pц8йAAADг/wййDцццwкййAP8йAAAAAD/AAAAD//wййййDц/8йAAAAAг/ййAPцццкййAP8ййA/wAA//ййййAAAAD/////8ййPццц//8ййAццц8кййP8ййAAP///wййййй////8ййADццц//wййDцццwйDе/////8ййAAAD//wйййййAA//8ййAAAццц//ййAPцццйDе/////8ййAAAAA8йййййAAAAA8ййAAAAPццц8ййAццц8йPе/////wкйййййAAAAццц/wййDцццwйец8кйййййAAAццц//ййAPцццйDец/кйййййAAццц//8ййAццц8йPец/wкйййййгwййDцццwйец//8кййййAAAAAг/ййAPцццйDец///кййййAAAAг/8ййAццц8йPец///wкййййAAг//wййDцццwйец////8кййййAццццййAPцццйDец/////кййййцццц8ййAццц8йPец/////wкйййAAAAцццц/wййDцццwйецц8кйййAAAцццц//ййAPцццйDецц/кйййAAцццц//8ййAццц8йPецц/wкйййцгwййDцццwйецц//кйййDцгййAPцццйDецц/8кйййAцццц//8ййAццц8йPецц8кйййADцццц//wййDцццwйецц/wкйййAAцццц//ййAPцццйDеццwкйййAADцццц/8ййAццц8йPеццкйййAAAAцццц/wййDцццwйеццкйййAAAAAPццццййAPцццйDец/////8ййййADц/ййййAAцццц8ййAццц8йPец////8ййййAц////8йййAAAAADццццwййDцццwйец/////wйййAAAADцц/8йййAAAAAццццййAPцццйDец////wйййAAAADцц///йййAAAADг//8ййAццц8йPец////йййAAAAцццйййAAAAг//wййDцццwйец////8йййAAAццц/wйййAADг//ййAPцццйDец////wйййAAццц//8йййAAPг/8ййAццц8йPец///wйййAADццц//wйййAADг/wййDцццwйец////йййAADг8йййAAPг/ййAPцццйDец///8йййADг//йййAAг/8ййAццц8йPец//8йййAAPг/8йййADг/wййDцццwйццгwйййййAPццццйййййAPц////ййAPцццйDццгйййййAAцццц/wйййййц////8ййAццц8йPццццц//8йййййAцццц//йййййDц////wййDцццwйццгwйййййDцццц//wййййAAAAAPц////ййAPцццйDццгйййййAPцццц//йййййц////8ййAццц8йPццццц//8йййййAцццц//8ййййAAAAADц////wййDцццwйццгwйййййцгwййййAAAAAPц////ййAPцццйDццгйййййDцг8ййййAAAAAц////8ййAццц8йPццццц//8йййййPцгwййййAAAADц////wййDцццwйццгwйййййцг/ййййAAAAAPц////ййAPцццйDццгйййййDцг8ййййAAAAAц////8ййAццц8йPццццц//8йййййPцццц//8ййййAAAAADц////wййDцццwйццгwйййййDцццц//wййййAAAAAPц////ййAPцццйDццгйййййAPцццц//йййййц////8ййAццц8йPццццц//8йййййAцццц//8ййййAAAAADц////wййDцццwйццгwйййййDцццц/8йййййPц////ййAPцццйDццгйййййAAцццц/wйййййц////8ййAццц8йPццццц//8йййййADццццwйййййDц////wййDцццwйец//йййAAAAг//wйййAPг/ййAPцццйDец///8йййADг//йййAAг/8ййAццц8йPец///wйййAAг/йййAADг/wййDцццwйец////йййAADгйййAAAPг/ййAPцццйDец///8йййAAAццц//8йййAAPг/8ййAццц8йPец////йййAAAPццц8йййAAAг//wййDцццwйец////8йййAAADцц/////8йййAAADг//ййAPцццйDец/////йййAAAAцц////8йййAAADг//8ййAццц8йPец////8йййAAAAAцц//йййAAAAAPг//wййDцццwйец/////wййййц/////wййййццццййAPцццйDец/////8ййййAц//8ййййAцццц8ййAццц8йPец/////wййййAAAA8PййййAAAADццццwййDцццwйецц8кйййAAADцццц/ййAPцццйDеццwкйййAAAPцццц8ййAццц8йPецц8кйййAAPцццц/wййDцццwйецц/wкйййAPцццц//ййAPцццйDецц/8кйййAцццц//8ййAццц8йPецц//кйййцгwййDцццwйецц//8кййAAAAADцгййAPцццйDецц/8кйййAцццц//8ййAццц8йPецц8кйййAAPцццц/wййDцццwйецц8кйййAAADцццц/ййAPцццйDец/////8кйййAAAAAцццц8ййAццц8йPец////8кййййPг//wййDцццwйец////8кййййADг//ййAPцццйDец///8кййййAAAг/8ййAццц8йPец//8кййййAAAAPгwййDцццwйец//8кййййAAAAADгййAPцццйDец/8кйййййAццц//8ййAццц8йPец8кйййййAAPццц/wййDцццwйец8кйййййAAADццц/ййAPцццйDецwййAAAA8йййййAAAAA8ййAAAAPццц8ййAццц8йPец8ййAAA//йййййAAAA//ййAAAPццц/wййDцццwйец//ййAA////йййййAP///wййAPццц//ййAPцццйDец//wййцййййAAAAц8ййPццц//8ййAццц8йPец//8йAAAAAц//ййййAPц//йAAAAAPгwййDцццwкййAAAPйAAAA8йAD/8йййAAAD/8Aц/wйAAAPг/ййAPцццкййAAAADwйAA8йAAAD//wййAAAAD///AADц/8йAAPг/8ййAццц8кййAAAAA8йA8ййP//8ййP//AAAAAPц//йAPг//wййDцццwкййAAAAAPй8ййAAADwйй/wйц///wAAAAAPццццййAPцццкйййDwAAAA8ййAAAAPййD8йDц///8AAAAPцццц8ййAццц8кйййA8AAA8ййAAAAA8ййPwйPц////AAAPцццц/wййDцццwкйййAPAA8ййAAAAADwйй/йAц/////wAPцццц//ййAPцццкйййAADw8йййPййD8йDц/////8Pцццц//8ййAццц8кйййAAA8йййA8ййPwйPе///wййDцццwйнуууууzMzMzwйй/йAе////ййAPцццйDMнуууууzMzPййD8йDе///8ййAццц8йMнуууууzMzM8ййPwйPе///wййDцццwйнуууууzMzMzwйй/йAе////ййAPцццйDMzMzMzйййййAууwAAAуzMAAAMzMzMzPййD8йDе///8ййAццц8йMzMzMwAццццц8AуzMzMAP//8AzMzMzAD///AMzMzM8ййPwйPе///wййDцццwйzMzMwPццццц//8MуzMD/////8MzMzAцDMzMzwйй/йAе////ййAPцццйDMzMwPццг/DMуDц/DMzAц/wzMwPййD8йDе///8ййAццц8йMzMzAццг/8MуwPц8MzMDц/DMzA8ййPwйPе///wййDцццwйzMzAеDMzMzMwPц//DMDц//wzA/wйй/йAе////ййAPцццйDMzMDццг//8MzMzMzAц//8MwPц//DMD/ййD8йDе///8ййAццц8йMzMwPццг//wуDц//wzAц//8MwP8ййPwйPе///wййDцццwйzMzAеDMzMzMwPц//DMDц//wzAцц///йAе////ййAPцццйDMzMDццг//8MzMzMzAц//8MwPц//DMDц//wуйDе///8ййAццц8йMzMzAццг/8MуwPц8MzMDц/DMzAц/wуwйPе///wййDцццwйzMzMDццг/wуzAц/wzMwPц8MzMDц/DMzMzMzйAе////ййAPцццйDMzMzAццгwуzMwP/////wzMzMD/////8MzMzAцDMуйDе///8ййAццц8йMzMzMwAццццц8AуzMzMAP//8AzMzMzAD///AMzMzMwA///wDMуwйPе///wййDцццwйуwйййййMууAAAMуzAAADMуwAAAуzMzйAе////ййAPцццйDMннууйDе///8ййAццц8йMннууwйPе///wййDцццwйннууwйAе////ййAPцццйAMннуzMzMwйADе///8ййAццц8ккййййAAPе///wййDцццwккййййAAе////ййAPцццккййййAADе///8ййAццц8ккййййAAPе///wййDцццwккййййAAе////ййAPццц8ккййййAе////8ййAццц/wккййййDе////wййDццц/8ккйййAAAAADе/////ййAPццц//8ккйййAAAец8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8ййAеееецц/wййDеееецц/ййAPеееецц8кккккккккккккккккккккккккккккккккккккккккккккккййййAAADMннннууййAMннннууwййAннннууzййADMннннууййAMннннууwййAннннууzййADMннннууййAMннннууwййAннннууzййADMуzMккAAуууzMAAAAAMууzMAAAAAMууzMAAAAAMуzMййAMуzADее/8AууzMzMzAD/////AMуzMzMzAD/////AMуzMzMzAD/////AMуwййAуwAее////8AууzMwAц//AMуzMwAц//AMуzMwAц//AMzMzMzййADMzMzMwPеец8MууwPц////DMуwPц////DMуwPц////DMzMzMййAMzMzMwPеец//DMуzMzMwPц/////wуwPц/////wуwPц/////wzMzMwййAzMzMwPеец///wуzMzMwPцц8MzMzMwPцц8MzMzMwPцц8MzMzййADMzMzAеец////DMуzMzAцц/wzMzMzAцц/wzMzMzAцц/wzMzMййAMzMzAеец/////wуzMzAцц//8MzMzAцц//8MzMzAцц//8MzMwййAzMzMDеец/////DMуzMDцц//wzMzMDцц//wzMzMDцц//wzMzййADMzMDееццwуzMDцц///8MzMDцц///8MzMDцц///8MzMййAMzMwPееццDMуwPцц///wzMwPцц///wzMwPцц///wzMwййAzMzAеецц8MуzAцц////DMzAцц////DMzAцц////DMzййADMzMDееццwуzMDцц///8MzMDцц///8MzMDцц///8MzMййAMzMwPееццDMуwPцц///wzMwPцц///wzMwPцц///wzMwййAzMzAеецц8MуzAцц////DMzAцц////DMzAцц////DMzййADMzMDееццwуzMDцц///8MzMDцц///8MzMDцц///8MzMййAMzMwPееццDMуwPцц///wzMwPцц///wzMwPцц///wzMwййAzMzMDеец/////DMуzMDцц//wzMzMDцц//wzMzMDцц//wzMzййADMzMwPеец////8MуzMwPцц//DMzMwPцц//DMzMwPцц//DMzMййAMzMzMDеец///8MуzMzMDцц/DMzMzMDцц/DMzMzMDцц/DMzMwййAzMzMwPеец///wуzMzMwPцц8MzMzMwPцц8MzMzMwPцц8MzMzййADMzMzMDеец//wууDц/////8MуDц/////8MуDц/////8MzMzMййAMzMzMzAеец/wууzAц////8MуzAц////8MуzAц////8MzMzMwййAуwAее////8AууzMwAц//AMуzMwAц//AMуzMwAц//AMzMzMzййADMуwAее//AMууzMzMwA/////wDMуzMzMwA/////wDMуzMzMwA/////wDMуййAMуzMwккADMуууwAAAAAууzMwAAAAAууzMwAAAAAуzMwййAннннууzййADMннннууййAMннннууwййAннннууzййADMннннууййAAннннууwййAAMннннуzMzMwкккккккккккккккккккккккккккккккккккккккккккккккйййAAAD/gкAAB//4кAAAB/+кAAAAB/wкAAAAD+кAAAAAHwкAAAAAPкй4кAAAAABgкAAAAAEккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккккAAIкAAAAABgкAAAAAHкй8кAAAAAD4кAAAAAfwкAAAAD/gкAAAAf/gкAAAH//gкAAB/w==";
            str_Icon = str_Icon.Replace("г", "ццц///");
            str_Icon = str_Icon.Replace("н", "уууууу");
            str_Icon = str_Icon.Replace("е", "цццццц");
            str_Icon = str_Icon.Replace("к", "йййййй");
            str_Icon = str_Icon.Replace("у", "zMzMzM");
            str_Icon = str_Icon.Replace("ц", "//////");
            str_Icon = str_Icon.Replace("й", "AAAAAA");
            this.Icon = new System.Drawing.Icon((System.IO.Stream)new System.IO.MemoryStream(Convert.FromBase64String(str_Icon)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "pDesignerMainFormPFL";
            this.Text = "Дизайнер форм для OneScriptForms";
            this.Load += pDesignerMainForm_Load;
            //* 18.12.2021 perfolenta
            this.FormClosing += pDesignerMainForm_Closing;
            //***
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnl4Toolbox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

            // элемент управления: (pDesigner)pDesignerCore 
            IpDesignerCore = this.pDesignerCore as IpDesigner;
            pDesignerCore.Parent = this.pnl4pDesigner;

            // Добавим элементы (toolboxItems) к будущей панели элементов (toolbox) указатель
            ToolboxItem toolPointer = new System.Drawing.Design.ToolboxItem();
            toolPointer.DisplayName = "<Указатель>";
            toolPointer.Bitmap = new System.Drawing.Bitmap(16, 16);
            listBox1.Items.Add(toolPointer);

            // элементы управления
            ToolboxItem toolButton = new System.Drawing.Design.ToolboxItem(typeof(Button));
            toolButton.DisplayName = "Кнопка (Button)";
            listBox1.Items.Add(toolButton);

            ToolboxItem toolCheckBox = new System.Drawing.Design.ToolboxItem(typeof(CheckBox));
            toolCheckBox.DisplayName = "Флажок (CheckBox)";
            listBox1.Items.Add(toolCheckBox);

            ToolboxItem toolColorDialog = new System.Drawing.Design.ToolboxItem(typeof(ColorDialog));
            toolColorDialog.DisplayName = "ДиалогВыбораЦвета (ColorDialog)";
            listBox1.Items.Add(toolColorDialog);

            ToolboxItem toolComboBox = new System.Drawing.Design.ToolboxItem(typeof(ComboBox));
            toolComboBox.DisplayName = "ПолеВыбора (ComboBox)";
            listBox1.Items.Add(toolComboBox);

            ToolboxItem toolDataGrid = new System.Drawing.Design.ToolboxItem(typeof(DataGrid));
            toolDataGrid.DisplayName = "СеткаДанных (DataGrid)";
            listBox1.Items.Add(toolDataGrid);

            ToolboxItem toolDateTimePicker = new System.Drawing.Design.ToolboxItem(typeof(DateTimePicker));
            toolDateTimePicker.DisplayName = "ПолеКалендаря (DateTimePicker)";
            listBox1.Items.Add(toolDateTimePicker);

            ToolboxItem toolFileSystemWatcher = new System.Drawing.Design.ToolboxItem(typeof(FileSystemWatcher));
            toolFileSystemWatcher.DisplayName = "НаблюдательФайловойСистемы (FileSystemWatcher)";
            listBox1.Items.Add(toolFileSystemWatcher);

            ToolboxItem toolFontDialog = new System.Drawing.Design.ToolboxItem(typeof(FontDialog));
            toolFontDialog.DisplayName = "ДиалогВыбораШрифта (FontDialog)";
            listBox1.Items.Add(toolFontDialog);

            ToolboxItem toolFolderBrowserDialog = new System.Drawing.Design.ToolboxItem(typeof(FolderBrowserDialog));
            toolFolderBrowserDialog.DisplayName = "ДиалогВыбораКаталога (FolderBrowserDialog)";
            listBox1.Items.Add(toolFolderBrowserDialog);

            ToolboxItem toolGroupBox = new System.Drawing.Design.ToolboxItem(typeof(GroupBox));
            toolGroupBox.DisplayName = "РамкаГруппы (GroupBox)";
            listBox1.Items.Add(toolGroupBox);

            ToolboxItem toolHProgressBar = new System.Drawing.Design.ToolboxItem(typeof(HProgressBar));
            toolHProgressBar.DisplayName = "ИндикаторГоризонтальный (HProgressBar)";
            listBox1.Items.Add(toolHProgressBar);

            ToolboxItem toolVProgressBar = new System.Drawing.Design.ToolboxItem(typeof(VProgressBar));
            toolVProgressBar.DisplayName = "ИндикаторВертикальный (VProgressBar)";
            listBox1.Items.Add(toolVProgressBar);

            ToolboxItem toolHScrollBar = new System.Drawing.Design.ToolboxItem(typeof(HScrollBar));
            toolHScrollBar.DisplayName = "ГоризонтальнаяПрокрутка (HScrollBar)";
            listBox1.Items.Add(toolHScrollBar);

            ToolboxItem toolImageList = new System.Drawing.Design.ToolboxItem(typeof(System.Windows.Forms.ImageList));
            toolImageList.DisplayName = "СписокИзображений (ImageList)";
            listBox1.Items.Add(toolImageList);

            ToolboxItem toolLabel = new System.Drawing.Design.ToolboxItem(typeof(Label));
            toolLabel.DisplayName = "Надпись (Label)";
            listBox1.Items.Add(toolLabel);

            ToolboxItem toolLinkLabel = new System.Drawing.Design.ToolboxItem(typeof(LinkLabel));
            toolLinkLabel.DisplayName = "НадписьСсылка (LinkLabel)";
            listBox1.Items.Add(toolLinkLabel);

            ToolboxItem toolListBox = new System.Drawing.Design.ToolboxItem(typeof(ListBox));
            toolListBox.DisplayName = "ПолеСписка (ListBox)";
            listBox1.Items.Add(toolListBox);

            ToolboxItem toolListView = new System.Drawing.Design.ToolboxItem(typeof(ListView));
            toolListView.DisplayName = "СписокЭлементов (ListView)";
            listBox1.Items.Add(toolListView);

            ToolboxItem toolMainMenu = new System.Drawing.Design.ToolboxItem(typeof(System.Windows.Forms.MainMenu));
            toolMainMenu.DisplayName = "ГлавноеМеню (MainMenu)";
            listBox1.Items.Add(toolMainMenu);

            ToolboxItem toolMonthCalendar = new System.Drawing.Design.ToolboxItem(typeof(MonthCalendar));
            toolMonthCalendar.DisplayName = "Календарь (MonthCalendar)";
            listBox1.Items.Add(toolMonthCalendar);

            ToolboxItem toolNotifyIcon = new System.Drawing.Design.ToolboxItem(typeof(NotifyIcon));
            toolNotifyIcon.DisplayName = "ЗначокУведомления (NotifyIcon)";
            listBox1.Items.Add(toolNotifyIcon);

            ToolboxItem toolNumericUpDown = new System.Drawing.Design.ToolboxItem(typeof(NumericUpDown));
            toolNumericUpDown.DisplayName = "РегуляторВверхВниз (NumericUpDown)";
            listBox1.Items.Add(toolNumericUpDown);

            ToolboxItem toolOpenFileDialog = new System.Drawing.Design.ToolboxItem(typeof(OpenFileDialog));
            toolOpenFileDialog.DisplayName = "ДиалогОткрытияФайла (OpenFileDialog)";
            listBox1.Items.Add(toolOpenFileDialog);

            ToolboxItem toolPanel = new System.Drawing.Design.ToolboxItem(typeof(Panel));
            toolPanel.DisplayName = "Панель (Panel)";
            listBox1.Items.Add(toolPanel);

            ToolboxItem toolPictureBox = new System.Drawing.Design.ToolboxItem(typeof(PictureBox));
            toolPictureBox.DisplayName = "ПолеКартинки (PictureBox)";
            listBox1.Items.Add(toolPictureBox);

            ToolboxItem toolPropertyGrid = new System.Drawing.Design.ToolboxItem(typeof(PropertyGrid));
            toolPropertyGrid.DisplayName = "СеткаСвойств (PropertyGrid)";
            listBox1.Items.Add(toolPropertyGrid);

            ToolboxItem toolRadioButton = new System.Drawing.Design.ToolboxItem(typeof(RadioButton));
            toolRadioButton.DisplayName = "Переключатель (RadioButton)";
            listBox1.Items.Add(toolRadioButton);

            ToolboxItem toolRichTextBox = new System.Drawing.Design.ToolboxItem(typeof(RichTextBox));
            toolRichTextBox.DisplayName = "ФорматированноеПолеВвода (RichTextBox)";
            listBox1.Items.Add(toolRichTextBox);

            ToolboxItem toolSaveFileDialog = new System.Drawing.Design.ToolboxItem(typeof(SaveFileDialog));
            toolSaveFileDialog.DisplayName = "ДиалогСохраненияФайла (SaveFileDialog)";
            listBox1.Items.Add(toolSaveFileDialog);

            ToolboxItem toolSplitter = new System.Drawing.Design.ToolboxItem(typeof(Splitter));
            toolSplitter.DisplayName = "Разделитель (Splitter)";
            listBox1.Items.Add(toolSplitter);

            ToolboxItem toolStatusBar = new System.Drawing.Design.ToolboxItem(typeof(StatusBar));
            toolStatusBar.DisplayName = "СтрокаСостояния (StatusBar)";
            listBox1.Items.Add(toolStatusBar);

            ToolboxItem toolTabControl = new System.Drawing.Design.ToolboxItem(typeof(TabControl));
            toolTabControl.DisplayName = "ПанельВкладок (TabControl)";
            listBox1.Items.Add(toolTabControl);

            ToolboxItem toolTextBox = new System.Drawing.Design.ToolboxItem(typeof(TextBox));
            toolTextBox.DisplayName = "ПолеВвода (TextBox)";
            listBox1.Items.Add(toolTextBox);

            ToolboxItem toolTimer = new System.Drawing.Design.ToolboxItem(typeof(Timer));
            toolTimer.DisplayName = "Таймер (Timer)";
            listBox1.Items.Add(toolTimer);

            ToolboxItem toolToolBar = new System.Drawing.Design.ToolboxItem(typeof(ToolBar));
            toolToolBar.DisplayName = "ПанельИнструментов (ToolBar)";
            listBox1.Items.Add(toolToolBar);

            ToolboxItem toolToolTip = new System.Drawing.Design.ToolboxItem(typeof(ToolTip));
            toolToolTip.DisplayName = "Подсказка (ToolTip)";
            listBox1.Items.Add(toolToolTip);

            ToolboxItem toolTreeView = new System.Drawing.Design.ToolboxItem(typeof(TreeView));
            toolTreeView.DisplayName = "Дерево (TreeView)";
            listBox1.Items.Add(toolTreeView);

            ToolboxItem toolUserControl = new System.Drawing.Design.ToolboxItem(typeof(UserControl));
            toolUserControl.DisplayName = "ПользовательскийЭлементУправления (UserControl)";
            listBox1.Items.Add(toolUserControl);

            ToolboxItem toolVScrollBar = new System.Drawing.Design.ToolboxItem(typeof(VScrollBar));
            toolVScrollBar.DisplayName = "ВертикальнаяПрокрутка (VScrollBar)";
            listBox1.Items.Add(toolVScrollBar);

            listBox1.Sorted = true;
            listBox1.HorizontalScrollbar = true;
            IpDesignerCore.Toolbox = listBox1;
        }

        private void _run_Click(object sender, EventArgs e)
        {
            string strTempFile = String.Format(System.IO.Path.GetTempPath() + "oscript_{0}_{1}.os", DateTime.Now.ToString("yyyyMMddHHmmssfff"), Guid.NewGuid().ToString().Replace("-", ""));
            System.IO.File.WriteAllText(strTempFile, SaveScript.GetScriptText(), System.Text.Encoding.UTF8);

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
            settingsForm.Icon = new System.Drawing.Icon((System.IO.Stream)new System.IO.MemoryStream(Convert.FromBase64String(str_settingsForm)));

            tabControl = new System.Windows.Forms.TabControl();
            tabControl.Parent = settingsForm;
            tabControl.Left = 15;
            tabControl.Top = 15;
            tabControl.Anchor = System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right |
                System.Windows.Forms.AnchorStyles.Bottom;
            tabControl.Size = new System.Drawing.Size(settingsForm.Width - 120, settingsForm.Height - 50);

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
            button_osPath.Font = new System.Drawing.Font(groupBox.Font, System.Drawing.FontStyle.Bold);
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
            button_dllPath.Font = new System.Drawing.Font(groupBox.Font, System.Drawing.FontStyle.Bold);
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

            settingsForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            if (settingsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Записываем значения в Settings
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
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
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
            System.IO.File.WriteAllText(saveFileDialog1.FileName, SaveForm.GetScriptText(saveFileDialog1.FileName), Encoding.UTF8);

            //System.IO.File.WriteAllText("C:\\444\\Форма1сохран.osd", SaveForm.GetScriptText(), Encoding.UTF8);
        }

        private void _loadForm_Click(object sender, EventArgs e)
        {
            ////////////////////OneScriptFormsDesigner.loadForm = true;

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

            ////// добавим вкладку и создадим на ней загружаемую форму. ////////////////////////////////////////////////////////////////////////////////////////////////
            DesignSurfaceExt2 var1 = IpDesignerCore.AddDesignSurface<Form>(670, 600, AlignmentModeEnum.SnapLines, new Size(1, 1), CompNames[0]);
            Component rootComponent = (Component)var1.ComponentContainer.Components[0];

            dictComponents[CompNames[0]] = rootComponent;

            string formName = CompNames[0];
            rootComponent.GetType().GetProperty("Text").SetValue(rootComponent, formName);
            rootBlok = OneScriptFormsDesigner.ParseBetween(strOSD, @"[<" + formName + @"]", @"[" + formName + @">]");
            if (rootBlok != null)
            {
                // установим для формы свойства
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

            ////// создадим остальные компоненты но пока не устанавливаем для них свойства, так как могут быть не все родители созданы. ///////////////////////////////
            IDesignSurfaceExt surface = pDesigner.DSME.ActiveDesignSurface;
            for (int i = 1; i < CompNames.Count; i++)
            {
                string componentName = CompNames[i];
                string type_NameRu = componentName;
                for (int i1 = 0; i1 < 10; i1++)
                {
                    type_NameRu = type_NameRu.Replace(i1.ToString(), "");
                }

                string type_NameEn = "osfDesigner." + osfDesigner.OneScriptFormsDesigner.namesRuEn[type_NameRu];
                System.Type type = Type.GetType(type_NameEn);

                if (type == typeof(osfDesigner.ImageList))
                {
                    ToolboxItem toolImageList1 = new System.Drawing.Design.ToolboxItem(typeof(System.Windows.Forms.ImageList));
                    Component comp1 = (Component)toolImageList1.CreateComponents(pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost())[0];
                    //  для comp1 уже создан дублер, получим его
                    osfDesigner.ImageList SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(comp1);
                    dictComponents[componentName] = SimilarObj;
                }
                else if (type == typeof(osfDesigner.MainMenu))
                {
                    ToolboxItem toolMainMenu1 = new System.Drawing.Design.ToolboxItem(typeof(System.Windows.Forms.MainMenu));
                    Component comp1 = (Component)toolMainMenu1.CreateComponents(pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost())[0];
                    //  для comp1 уже создан дублер, получим его
                    osfDesigner.MainMenu SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(comp1);
                    dictComponents[componentName] = SimilarObj;
                }
                else if (type == typeof(osfDesigner.TabPage))
                {
                    System.Windows.Forms.MessageBox.Show("osfDesigner.TabPage");

                    ////Component control = (Component)surface.CreateControl(type, new Size(200, 20), new Point(10, 200));

                    //ToolboxItem toolTabPage1 = new System.Drawing.Design.ToolboxItem(typeof(System.Windows.Forms.TabPage));
                    //Component comp1 = (Component)toolTabPage1.CreateComponents(pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost())[0];
                    ////  для comp1 уже создан дублер, получим его
                    //osfDesigner.TabPage SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(comp1);
                    //SimilarObj.OriginalObj = (System.Windows.Forms.TabPage)comp1;
                    ////////////////////////OneScriptFormsDesigner.AddToHashtable(comp1, SimilarObj);
                    //OneScriptFormsDesigner.PassProperties(comp1, SimilarObj);//передадим свойства
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
                    Component comp1 = (Component)toolComp1.CreateComponents(pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost())[0];
                    dictComponents[componentName] = comp1;
                }
                else
                {
                    Component control1 = surface.CreateControl(type, new Size(200, 20), new Point(10, 200));
                    dictComponents[componentName] = control1;
                }
                dictComponents[componentName].Site.Name = componentName;
            }

            ////// установим для компонентов свойства. //////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                // установим для формы свойства
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

            // установим правильный порядок компонентов для дерева компонентов при загрузке сохраненной формы
            string HierarchyBlok = OneScriptFormsDesigner.ParseBetween(strOSD, @"[<Иерархия]", @"[Иерархия>]");
            if (ComponentBlok != null)
            {
                result = HierarchyBlok.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                for (int i1 = 0; i1 < result.Length; i1++)
                {
                    ((Form)rootComponent).ArrayListComponentsAddingOrder.Add(result[i1]);
                }
            }



            ComponentCollection ctrlsExisting = pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost().Container.Components;
            ISelectionService iSel = pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost().GetService(typeof(ISelectionService)) as ISelectionService;
            if (iSel == null)
            {
                return;
            }
            iSel.SetSelectedComponents(new IComponent[] { ctrlsExisting[0] });

            pDesigner.DSME.PropertyGridHost.ReloadTreeView();
            pDesigner.DSME.PropertyGridHost.ChangeSelectNode((Component)ctrlsExisting[0]);

            //////////////////OneScriptFormsDesigner.loadForm = false;
        }

        public static string MaxNodeSearch(osfDesigner.TreeView treeView, ref string maxNodeName, System.Windows.Forms.TreeNodeCollection treeNodes = null)
        {
            System.Windows.Forms.TreeNodeCollection _treeNodes;
            if (treeNodes == null)
            {
                _treeNodes = treeView.Nodes;
            }
            else
            {
                _treeNodes = treeNodes;
            }
            osfDesigner.MyTreeNode treeNode = null;
            for (int i = 0; i < _treeNodes.Count; i++)
            {
                treeNode = (osfDesigner.MyTreeNode)_treeNodes[i];
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
            osfDesigner.pDesigner.SplitterpDesigner.Visible = true;
            osfDesigner.pDesigner.CodePanel.Visible = false;
            this._edit.Enabled = true;//"Правка"
            this._tools.Enabled = true;//"Инструменты"
            osfDesigner.pDesigner.SplitterpDesigner.Panel2Collapsed = false;
            pnl4Toolbox.Visible = true;
            this._form.Enabled = false;
            this._code.Enabled = true;
            this._form.CheckState = System.Windows.Forms.CheckState.Checked;
            this._code.CheckState = System.Windows.Forms.CheckState.Unchecked;
        }

        private void _code_Click(object sender, EventArgs e)
        {
            SaveScript.comps.Clear();
            osfDesigner.pDesigner.SplitterpDesigner.Visible = false;
            osfDesigner.pDesigner.CodePanel.Visible = true;
            this._edit.Enabled = false;//"Правка"
            this._tools.Enabled = false;//"Инструменты"
            osfDesigner.pDesigner.SplitterpDesigner.Panel2Collapsed = true;
            pnl4Toolbox.Visible = false;
            this._form.Enabled = true;
            this._code.Enabled = false;
            this._form.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this._code.CheckState = System.Windows.Forms.CheckState.Checked;
            osfDesigner.pDesigner.RichTextBox.Text = SaveScript.GetScriptText();
        }

        public string Version
        {
            get
            {
                if (string.IsNullOrEmpty(_version))
                {
                    // Получение версии файла запущенной сборки
                    System.Diagnostics.FileVersionInfo FVI = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    _version = FVI.ProductVersion;
                }
                return _version;
            }
        }

        // Очистка используемых ресурсов.
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
            // таймер для обеспечения срабатывания по правой кнопке мыши сворачивания раскрытого свойства СписокИзображений
            this.timerLoad = new System.Windows.Forms.Timer();
            this.timerLoad.Enabled = true;
            this.timerLoad.Tick += new System.EventHandler(this.timerLoad_Tick);
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
            System.IO.File.WriteAllText(saveFileDialog1.FileName, SaveScript.GetScriptText());
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
