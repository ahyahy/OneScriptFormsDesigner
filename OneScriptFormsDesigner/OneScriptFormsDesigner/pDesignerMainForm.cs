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
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public class pDesignerMainForm : System.Windows.Forms.Form, IDesignerMainForm
    {
        private string _version = string.Empty;
        public pDesigner pDesignerCore = new pDesigner();
        private IpDesigner IpDesignerCore = null;
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;

        private System.Windows.Forms.ToolStripMenuItem _file;
        private System.Windows.Forms.ToolStripMenuItem _addForm;
        private System.Windows.Forms.ToolStripMenuItem _useSnapLines;
        private System.Windows.Forms.ToolStripMenuItem _useGrid;
        private System.Windows.Forms.ToolStripMenuItem _useGridWithoutSnapping;
        private System.Windows.Forms.ToolStripMenuItem _useNoGuides;
        private System.Windows.Forms.ToolStripMenuItem _deleteForm;
        private System.Windows.Forms.ToolStripSeparator _stripSeparator1;
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
            //DesignSurfaceManagerExt DesignSurfaceManagerExt = pDesigner.DSME;
            //propertyGrid1 = DesignSurfaceManagerExt.PropertyGridHost.PropertyGrid;
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

        public pDesignerMainForm()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pDesignerMainForm));
            propertyGrid1 = pDesigner.DSME.PropertyGridHost.PropertyGrid;
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this._file = new System.Windows.Forms.ToolStripMenuItem();
            this._addForm = new System.Windows.Forms.ToolStripMenuItem();
            this._useSnapLines = new System.Windows.Forms.ToolStripMenuItem();
            this._useGrid = new System.Windows.Forms.ToolStripMenuItem();
            this._useGridWithoutSnapping = new System.Windows.Forms.ToolStripMenuItem();
            this._useNoGuides = new System.Windows.Forms.ToolStripMenuItem();
            this._deleteForm = new System.Windows.Forms.ToolStripMenuItem();
            this._stripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
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
            osfDesigner.pDesignerMainForm._tabOrder1 = this._tabOrder;

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
            this._addForm,
            this._deleteForm,
            this._stripSeparator1,
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
            // _addForm
            // 
            this._addForm.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._useSnapLines,
            this._useGrid,
            this._useGridWithoutSnapping,
            this._useNoGuides});
            string str_addForm = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABR0lEQVR42u3avQ/BQBgGcBcEia2LSESE1WK3+p/tdoNuxcBgsxJKPTc0/ZBe2lzPfXjf5Fxocvf8KqmrK2s4Ukx3AIIIIJHuMDIG/tJBu+tOI1ldDkl/Exx0052qZPU4IH7DIQHaTHcqyQo45InW0p1EskIOCdGaupNI1osghhVBTKsviG1Llvg3kCCmFEHqTHDOf4bJR1ZBouJbhgOrtu4zFnJCgDFBSkBk7x4z4xKEIIrqHyBXBPCMgUTq/lp6s+xKXR0EI6/RrRRBePkIOf8FZI9uqhCyQcilC5AtQi5cgBxZMr5SyAXdQCEkfYlWftXy0Q0LDrfR+gXHHmg7wdAeAk6yU1WDlL2cljoRNi1RhCCbIELYPyxRCEIQgggwrPr8+iH1nY8Ekt56sxUS5nd1bcJkcju1Pe3MAwNxWf8IhxNFENPqA2/lwIZlxdeeAAAAAElFTkSuQmCC";
            this._addForm.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_addForm);
            this._addForm.Name = "_addForm";
            this._addForm.Size = new System.Drawing.Size(221, 26);
            this._addForm.Text = "Добавить Форму";
            // 
            // _useSnapLines
            // 
            string str_useSnapLines = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAAd0lEQVR42u3YSw7AIAgFwHr/Q7er7uuvoM7bC0xiSLRcm6RED5ARckfOCgKyIKS3dlU9EBAQkO7GowMCsjokNCCVea9dS79PZ0FAQEBAQEBAQEDSQKbnCMjsp+vIWUFAMkD+XgzDvkxBQEAOhUQHBCQbZKlsA3kAIttEM9KSwFkAAAAASUVORK5CYII=";
            this._useSnapLines.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_useSnapLines);
            this._useSnapLines.Text = "Использовать линии привязки";
            this._useSnapLines.Click += _useSnapLines_Click;
            // 
            // _useGrid
            // 
            string str_useGrid = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAAf0lEQVR42u3UsRGAMBDEQNwX5dMXkDvgM3Q30oyzC7zJr6Ok9fcHhHxA7iEUt0uFXO87GyDbrhYSmxBaQmjVQnBndboTQtvVQmITQksIrVoI7qxOd0Jou1pIbEJoCaFVC8Gd1elOCG1XC4lNCC0htGohuLM63Qmh7WohsQmh9QCKkWUzuKisgQAAAABJRU5ErkJggg==";
            this._useGrid.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_useGrid);
            this._useGrid.Text = "Использовать сетку";
            this._useGrid.Click += _useGrid_Click;
            // 
            // _useGridWithoutSnapping
            // 
            this._useGridWithoutSnapping.Text = "Использовать сетку без привязки";
            this._useGridWithoutSnapping.Click += _useGridWithoutSnapping_Click;
            // 
            // _useNoGuides
            // 
            this._useNoGuides.Name = "_useNoGuides";
            this._useNoGuides.Size = new System.Drawing.Size(316, 26);
            this._useNoGuides.Text = "Не использовать ориентиры";
            this._useNoGuides.Click += _useNoGuides_Click;
            // 
            // _deleteForm
            // 
            string str_deleteForm = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAABLklEQVR42u2asQrCMBCGDSoquLmIi4iuLu6uvrO7u4PdKg4uPoKi1XoHVlultTVc7xrvg2soheT/KCVpU1NzBMMdQEUyRELuMDYOeGhBnbjTWNJGkfidQKEjd6qcdFAgOkERH2rCncoSH0UuUA3uJJYEKBJA1bmTWHJVEWGoiDQ+RKq2ZInmQBWRgopIQ0Wk8b8i3G+P33KpSJHBKPnfZ0QqtCIh3TN1M8mVOp0I9LyEZkEkgngQclqGyBaaMaHICkLOXRBZQ8iZCyI78+qfVOQATZ9QZA8hh+Qij949aAYpl5tQ3ZRrZ6hNRtc9CDhKDlVMxJmZPe+8ULYM6aKxTBldokhDRaShItJIiMS33qoqErzv6lZJJpHbqe1pZ34YiOD+dvUrz184nEBFpHEHIdCPhqDjZfIAAAAASUVORK5CYII=";
            this._deleteForm.Image = osfDesigner.OneScriptFormsDesigner.Base64ToImage(str_deleteForm);
            this._deleteForm.Name = "_deleteForm";
            this._deleteForm.Size = new System.Drawing.Size(221, 26);
            this._deleteForm.Text = "Удалить Форму";
            this._deleteForm.Click += _deleteForm_Click;
            // 
            // _stripSeparator1
            // 
            this._stripSeparator1.Name = "_stripSeparator1";
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
            // pDesignerMainForm
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
            this.Name = "pDesignerMainForm";
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

            //System.IO.File.WriteAllText("C:\\444\\Форма1сохран\\Форма1сохран.osd", SaveForm.GetScriptText("C:\\444\\Форма1сохран\\"), Encoding.UTF8);
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

            //string strOSD = File.ReadAllText("C:\\444\\Форма1сохран\\Форма1сохран.osd");

            strOSD = strOSD.Replace(" ", "");

            string[] result = null;
            string[] stringSeparators = new string[] { Environment.NewLine };
            string ComponentBlok = null;
            string rootBlok = null;

            // соберем из блока конструкторов имена компонентов в CompNames. ///////////////////////////////////////////////////////////////////////////////////////////
            List<string> CompNames = new List<string>();
            //Dictionary<string, Component> dictComponents = new Dictionary<string, Component>();// словарь для соответствия имени переменной в скрипте объекту в библиотеке
            Dictionary<string, object> dictObjects = new Dictionary<string, object>();// словарь для соответствия имени переменной в скрипте объекту в библиотеке
            string ConstructorBlok = OneScriptFormsDesigner.ParseBetween(strOSD, @"[<Конструкторы]", @"[Конструкторы>]");
            result = ConstructorBlok.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < result.Length; i++)
            {
                string s = OneScriptFormsDesigner.ParseBetween(result[i], null, @"=Ф.");
                if (s != null)
                {
                    if (s.Substring(0, 2) != @"//")
                    {
                        CompNames.Add(s);
                        dictObjects.Add(s, null);
                    }
                }
            }
            result = null;

            ////// добавим вкладку и создадим на ней загружаемую форму. ////////////////////////////////////////////////////////////////////////////////////////////////
            DesignSurfaceExt2 var1 = IpDesignerCore.AddDesignSurface<Form>(670, 600, AlignmentModeEnum.SnapLines, new Size(1, 1), CompNames[0]);
            Component rootComponent = (Component)var1.ComponentContainer.Components[0];

            dictObjects[CompNames[0]] = rootComponent;

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
                            if (displayName != "КнопкаОтмена" && displayName != "КнопкаПринять" && !strCurrent.StartsWith("Подсказка"))
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
                    dictObjects[componentName] = SimilarObj;
                }
                else if (type == typeof(osfDesigner.MainMenu))
                {
                    ToolboxItem toolMainMenu1 = new System.Drawing.Design.ToolboxItem(typeof(System.Windows.Forms.MainMenu));
                    Component comp1 = (Component)toolMainMenu1.CreateComponents(pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost())[0];
                    //  для comp1 уже создан дублер, получим его
                    osfDesigner.MainMenu SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(comp1);
                    dictObjects[componentName] = SimilarObj;
                }
                else if (type == typeof(osfDesigner.TabPage))
                {
                    ToolboxItem toolTabPage1 = new System.Drawing.Design.ToolboxItem(typeof(System.Windows.Forms.TabPage));
                    Component comp1 = (Component)toolTabPage1.CreateComponents(pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost())[0];
                    //  для comp1 уже создан дублер, получим его
                    osfDesigner.TabPage SimilarObj = OneScriptFormsDesigner.RevertSimilarObj(comp1);
                    SimilarObj.OriginalObj = (System.Windows.Forms.TabPage)comp1;
                    OneScriptFormsDesigner.PassProperties(comp1, SimilarObj);//передадим свойства
                    dictObjects[componentName] = SimilarObj;

                    GetDefaultValues(SimilarObj);
                }
                else if (type == typeof(osfDesigner.TabControl))
                {
                    ToolboxItem toolTabControl1 = new System.Drawing.Design.ToolboxItem(typeof(osfDesigner.TabControl));
                    Component comp1 = (Component)toolTabControl1.CreateComponents(pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost())[0];
                    // удалим две вкладки, которые панель вкладок создает автоматически
                    IDesignerEventService des = (IDesignerEventService)pDesigner.DSME.GetService(typeof(IDesignerEventService));
                    if (des != null)
                    {
                        for (int i1 = 0; i1 < ((osfDesigner.TabControl)comp1).TabPages.Count; i1++)
                        {
                            des.ActiveDesigner.Container.Remove(((osfDesigner.TabControl)comp1).TabPages[i1]);
                        }
                        ((osfDesigner.TabControl)comp1).TabPages.Clear();
                    }
                    dictObjects[componentName] = comp1;
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
                    dictObjects[componentName] = comp1;
                }
                else
                {
                    Component control1 = surface.CreateControl(type, new Size(200, 20), new Point(10, 200));
                    dictObjects[componentName] = control1;
                }
                ((Component)dictObjects[componentName]).Site.Name = componentName;
            }

            ////// установим для компонентов свойства. //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            for (int i = 1; i < CompNames.Count; i++)
            {
                string componentName = CompNames[i];
                Component control = (Component)dictObjects[componentName];
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
                                        Control parent = (Control)dictObjects[parentName];
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                    }
                                    else
                                    {
                                        //Календарь1.ВыделенныеДаты.Добавить(Дата(2021, 11, 01, 00, 00, 00));
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", ".");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "Дата(", "))");
                                        string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                        Control parent = (Control)dictObjects[parentName];
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                    }
                                }
                                
                                else if (componentName.Contains("ГлавноеМеню"))
                                {
                                    string controlName = ((osfDesigner.MainMenu)control).Name;
                                    if (strCurrent.Contains(".ЭлементыМеню.Добавить(Ф.ЭлементМеню("))
                                    {
                                        ////Меню0 = ГлавноеМеню1.ЭлементыМеню.Добавить(Ф.ЭлементМеню("Меню0"));
                                        ////Меню1 = Меню0.ЭлементыМеню.Добавить(Ф.ЭлементМеню("Меню1"));
                                        osfDesigner.MainMenu MainMenu1 = (osfDesigner.MainMenu)control;
                                        System.Windows.Forms.TreeView TreeView1 = MainMenu1.TreeView;
                                        string Text = OneScriptFormsDesigner.ParseBetween(strCurrent, "(\u0022", "\u0022)");
                                        string Name = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                        if (strCurrent.Contains(componentName + "."))// создаем элемент главного меню
                                        {
                                            if (!Name.Contains("Сепаратор"))
                                            {
                                                MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                                                dictObjects.Add(componentName + Name, MenuItemEntry1);
                                                MenuItemEntry1.DefaultValues = @"Доступность == Истина
Нажатие == 
Отображать == Истина
Переключатель == Ложь
Помечен == Ложь
ПорядокСлияния == 0
СочетаниеКлавиш == Отсутствие
Текст == 
ТипСлияния == Добавить
(Name) == ";
                                                MenuItemEntry1.Text = Text;
                                                MenuItemEntry1.Name = Name;
                                                MainMenu1.MenuItems.Add(MenuItemEntry1.M_MenuItem);
                                                OneScriptFormsDesigner.AddToHashtable(MenuItemEntry1.M_MenuItem, MenuItemEntry1);
                                                System.Windows.Forms.TreeNode TreeNode1 = new System.Windows.Forms.TreeNode();
                                                TreeNode1.Tag = MenuItemEntry1;
                                                TreeNode1.Text = MenuItemEntry1.Text;
                                                TreeView1.Nodes.Add(TreeNode1);
                                                TreeView1.SelectedNode = TreeNode1;
                                            }
                                            else
                                            {
                                                MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                                                dictObjects.Add(componentName + Name, MenuItemEntry1);
                                                MenuItemEntry1.DefaultValues = @"Доступность == Истина
Нажатие == 
Отображать == Истина
Переключатель == Ложь
Помечен == Ложь
ПорядокСлияния == 0
СочетаниеКлавиш == Отсутствие
Текст == 
ТипСлияния == Добавить
(Name) == ";
                                                MenuItemEntry1.Name = Name;
                                                //имя в виде тире не присваивать, заменять на тире только во время формирования сценария
                                                MenuItemEntry1.Text = MenuItemEntry1.Name;
                                                MainMenu1.MenuItems.Add(MenuItemEntry1.M_MenuItem);
                                                OneScriptFormsDesigner.AddToHashtable(MenuItemEntry1.M_MenuItem, MenuItemEntry1);

                                                System.Windows.Forms.TreeNode TreeNode1 = new System.Windows.Forms.TreeNode();
                                                TreeNode1.Tag = MenuItemEntry1;
                                                TreeNode1.Text = MenuItemEntry1.Name;
                                                TreeView1.Nodes.Add(TreeNode1);
                                                TreeView1.SelectedNode = TreeNode1;

                                                // свойство Checked у родителя нужно установить в false
                                                if (MenuItemEntry1.Parent.GetType() != typeof(osfDesigner.MainMenu))
                                                {
                                                    ((MenuItem)MenuItemEntry1.Parent).Checked = false;
                                                }
                                                TreeView1.SelectedNode.Expand();
                                            }
                                        }
                                        else// создаем элемент подменю
                                        {
                                            if (!Name.Contains("Сепаратор"))
                                            {
                                                MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                                                dictObjects.Add(componentName + Name, MenuItemEntry1);
                                                MenuItemEntry1.DefaultValues = @"Доступность == Истина
Нажатие == 
Отображать == Истина
Переключатель == Ложь
Помечен == Ложь
ПорядокСлияния == 0
СочетаниеКлавиш == Отсутствие
Текст == 
ТипСлияния == Добавить
(Name) == ";
                                                MenuItemEntry1.Text = Text;
                                                MenuItemEntry1.Name = Name;

                                                string nameNodeParent = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ".");
                                                System.Windows.Forms.TreeNode SelectedNode = null;
                                                SelectedNodeSearch(TreeView1, nameNodeParent, ref SelectedNode, null);
                                                TreeView1.SelectedNode = SelectedNode;

                                                MenuItemEntry MenuItemParent = (MenuItemEntry)TreeView1.SelectedNode.Tag;
                                                MenuItemParent.MenuItems.Add(MenuItemEntry1.M_MenuItem);
                                                OneScriptFormsDesigner.AddToHashtable(MenuItemEntry1.M_MenuItem, MenuItemEntry1);
                                                System.Windows.Forms.TreeNode TreeNode1 = new System.Windows.Forms.TreeNode();
                                                TreeNode1.Tag = MenuItemEntry1;
                                                TreeNode1.Text = MenuItemEntry1.Text;
                                                TreeView1.SelectedNode.Nodes.Add(TreeNode1);

                                                // свойство Checked у родителя нужно установить в false
                                                if (MenuItemEntry1.Parent.GetType() != typeof(osfDesigner.MainMenu))
                                                {
                                                    ((MenuItem)MenuItemEntry1.Parent).Checked = false;
                                                }
                                                TreeView1.SelectedNode = TreeNode1;
                                            }
                                            else
                                            {
                                                MenuItemEntry MenuItemEntry1 = new MenuItemEntry();
                                                dictObjects.Add(componentName + Name, MenuItemEntry1);
                                                MenuItemEntry1.DefaultValues = @"Доступность == Истина
Нажатие == 
Отображать == Истина
Переключатель == Ложь
Помечен == Ложь
ПорядокСлияния == 0
СочетаниеКлавиш == Отсутствие
Текст == 
ТипСлияния == Добавить
(Name) == ";
                                                MenuItemEntry1.Name = Name;
                                                //имя в виде тире не присваивать, заменять на тире только во время формирования сценария
                                                MenuItemEntry1.Text = MenuItemEntry1.Name;
                                                string nameNodeParent = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ".");

                                                System.Windows.Forms.TreeNode SelectedNode = null;
                                                SelectedNodeSearch(TreeView1, nameNodeParent, ref SelectedNode, null);
                                                TreeView1.SelectedNode = SelectedNode;

                                                MenuItemEntry MenuItemParent = (MenuItemEntry)TreeView1.SelectedNode.Tag;
                                                MenuItemParent.MenuItems.Add(MenuItemEntry1.M_MenuItem);
                                                OneScriptFormsDesigner.AddToHashtable(MenuItemEntry1.M_MenuItem, MenuItemEntry1);

                                                System.Windows.Forms.TreeNode TreeNode1 = new System.Windows.Forms.TreeNode();
                                                TreeNode1.Tag = MenuItemEntry1;
                                                TreeNode1.Text = MenuItemEntry1.Name;
                                                TreeView1.SelectedNode.Nodes.Add(TreeNode1);

                                                // свойство Checked у родителя нужно установить в false
                                                if (MenuItemEntry1.Parent.GetType() != typeof(osfDesigner.MainMenu))
                                                {
                                                    ((MenuItem)MenuItemEntry1.Parent).Checked = false;
                                                }
                                                TreeView1.SelectedNode.Expand();
                                            }
                                        }
                                    }
                                    else // обрабатываем как свойство элемента меню
                                    {
                                        //Меню1.Нажатие = Ф.Действие(ЭтотОбъект, "й111");
                                        //Меню1.Переключатель = Истина;
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                        string nameObj = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                        object control2 = (object)dictObjects[nameObj];
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, null);
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
                                else if (strCurrent.StartsWith("Подсказка"))
                                {
                                    //Подсказка1.УстановитьПодсказку(Форма_0, "фор");

                                    string displayName = "ToolTip на " + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".УстановитьПодсказку");
                                    string strPropertyValue = strCurrent;
                                    PropValueConverter.SetPropValue(control, displayName, strPropertyValue, null);
                                }
                                else if (componentName.Contains("Вкладка"))
                                {
                                    string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                    string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                    string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                    Control parent = (Control)dictObjects[parentName];
                                    PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                }

                                else if (componentName.Contains("СтрокаСостояния"))
                                {
                                    //СтрокаСостояния1.Родитель = Форма_0;
                                    //СтрокаСостояния1.Текст = "СтрокаСостояния1";

                                    string controlName = ((osfDesigner.StatusBar)control).Name;
                                    string header = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                    if (header.Contains("СтрокаСостояния"))
                                    {
                                        if (strCurrent.Contains(".Панели.Добавить(")) // добавляем панель
                                        {
                                            //СтрокаСостояния2.Панели.Добавить(ПанельСтрокиСостояния0);
                                            string nameItem = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                            StatusBarPanel StatusBarPanel1 = (StatusBarPanel)dictObjects[nameItem];
                                            ((osfDesigner.StatusBar)control).Panels.Add(StatusBarPanel1);
                                        }
                                        else // обрабатываем как свойство строки состояния
                                        {
                                            //СтрокаСостояния2.Родитель = Форма_0;
                                            //СтрокаСостояния2.ПорядокОбхода = 1;
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                            Control parent = (Control)dictObjects[parentName];
                                            PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                        }
                                    }
                                    else 
                                    {
                                        if (strCurrent.Contains("Ф.ПанельСтрокиСостояния();")) // создаем панель
                                        {
                                            //ПанельСтрокиСостояния0 = Ф.ПанельСтрокиСостояния();
                                            StatusBarPanel StatusBarPanel1 = new StatusBarPanel();
                                            string nameItem = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            StatusBarPanel1.Name = nameItem;
                                            dictObjects.Add(controlName + nameItem, StatusBarPanel1);

                                            StatusBarPanel1.DefaultValues = @"АвтоРазмер == Отсутствие
Значок == 
СтильГраницы == Утопленная
Текст == 
Ширина == 100
МинимальнаяШирина == 10
(Name) == 
";
                                        }
                                        else // обрабатываем как свойство панели строки состояния
                                        {
                                            //ПанельСтрокиСостояния1.Ширина = 150;
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                            string nameObj = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                            object control2 = dictObjects[nameObj];
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, null);
                                        }
                                    }
                                }




                                else if (componentName.Contains("ПанельИнструментов"))
                                {
                                    string controlName = ((osfDesigner.ToolBar)control).Name;
                                    string header = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                    if (header.Contains("ПанельИнструментов"))
                                    {
                                        if (strCurrent.Contains(".Кнопки.Добавить(")) // добавляем кнопку панели инструментов
                                        {
                                            //Кн0 = ПанельИнструментов1.Кнопки.Добавить(Ф.КнопкаПанелиИнструментов());
                                            System.Windows.Forms.ToolBarButton OriginalObj = new System.Windows.Forms.ToolBarButton();
                                            osfDesigner.ToolBarButton SimilarObj = new osfDesigner.ToolBarButton();
                                            OneScriptFormsDesigner.PassProperties(OriginalObj, SimilarObj);//передадим свойства
                                            SimilarObj.OriginalObj = OriginalObj;
                                            SimilarObj.Parent = OriginalObj.Parent;
                                            SimilarObj.Style = (osfDesigner.ToolBarButtonStyle)OriginalObj.Style;
                                            OriginalObj.Tag = SimilarObj;

                                            string nameItem = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            SimilarObj.Name = nameItem;
                                            ((osfDesigner.ToolBar)control).Buttons.Add(OriginalObj);
                                            dictObjects.Add(controlName + nameItem, SimilarObj);

                                            SimilarObj.DefaultValues = @"Доступность == Истина
ИндексИзображения == -1
Нажата == Ложь
НейтральноеПоложение == Ложь
Отображать == Истина
Прямоугольник == 
Стиль == СтандартнаяТрехмерная
Текст == 
ТекстПодсказки == 
(Name) == ";
                                        }
                                        else // обрабатываем как свойство панели инструментов
                                        {
                                            //ПанельИнструментов1.Положение = Ф.Точка(0, 0);
                                            //ПанельИнструментов1.ПорядокОбхода = 0;
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                            Control parent = (Control)dictObjects[parentName];
                                            PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                        }
                                    }
                                    else // обрабатываем как свойство кнопки панели инструментов
                                    {
                                        //Кн0.Текст = "Кн000";
                                        //Кн0.ТекстПодсказки = "фыфыфы";
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                        string nameObj = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                        object control2 = dictObjects[nameObj];
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, null);
                                    }
                                }
                                else if (componentName.Contains("СписокЭлементов"))
                                {
                                    string controlName = ((osfDesigner.ListView)control).Name;
                                    string header = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                    if (header.Contains("СписокЭлементов"))// обрабатываем как свойство списка элементов
                                    {
                                        if (strCurrent.Contains(".Элементы.Добавить(")) // добавляем элемент списка элементов
                                        {
                                            //СписокЭлементов1.Элементы.Добавить(Элемент0);
                                            string nameItem = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                            ListViewItem ListViewItem1 = (ListViewItem)dictObjects[nameItem];
                                            ((osfDesigner.ListView)control).Items.Add(ListViewItem1);
                                        }
                                        else if(strCurrent.Contains(".Колонки.Добавить(")) // добавляем колонку списка элементов
                                        {
                                            //СписокЭлементов2.Колонки.Добавить(Колонка0);
                                            string nameItem = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                            ColumnHeader ColumnHeader1 = (ColumnHeader)dictObjects[nameItem];
                                            ((osfDesigner.ListView)control).Columns.Add(ColumnHeader1);
                                        }
                                        else // обрабатываем как свойство для СписокЭлементов
                                        {
                                            //СписокЭлементов1.Родитель = Форма_0;
                                            //СписокЭлементов1.Положение = Ф.Точка(49, 51);
                                            //СписокЭлементов1.ПорядокОбхода = 0;
                                            //СписокЭлементов1.Размер = Ф.Размер(121, 97);
                                            //СписокЭлементов1.Элементы.Добавить(Элемент2);
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                            Control parent = (Control)dictObjects[parentName];
                                            PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                        }
                                    }
                                    else
                                    {
                                        if (strCurrent.Contains("Ф.ЭлементСпискаЭлементов();")) // создаем элемент списка элементов
                                        {
                                            //Элемент0 = Ф.ЭлементСпискаЭлементов();
                                            ListViewItem ListViewItem1 = new ListViewItem();
                                            string nameItem = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            ListViewItem1.Name = nameItem;
                                            dictObjects.Add(controlName + nameItem, ListViewItem1);

                                            ListViewItem1.DefaultValues = @"ИспользоватьСтильДляПодэлементов == Истина
ОсновнойЦвет == ТекстОкна
Помечен == Ложь
Текст == 
ЦветФона == Окно
Шрифт == Microsoft Sans Serif; 7,8pt
Подэлементы == (Коллекция)
ИндексИзображения == -1
(Name) == ";
                                        }
                                        else if (strCurrent.Contains("Ф.ПодэлементСпискаЭлементов();")) // создаем подэлемент списка элементов
                                        {
                                            //Подэлемент1 = Ф.ПодэлементСпискаЭлементов();
                                            ListViewSubItem ListViewSubItem1 = new ListViewSubItem();
                                            string nameItem = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            ListViewSubItem1.Name = nameItem;
                                            dictObjects.Add(controlName + nameItem, ListViewSubItem1);

                                            ListViewSubItem1.DefaultValues = @"ОсновнойЦвет == ТекстОкна
Текст == 
ЦветФона == Окно
Шрифт == Microsoft Sans Serif; 7,8pt
(Name) == ";
                                        }
                                        else if (strCurrent.Contains("Ф.Колонка();")) // создаем колонку списка элементов
                                        {
                                            //Колонка0 = Ф.Колонка();
                                            ColumnHeader ColumnHeader1 = new ColumnHeader();
                                            string nameItem = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            ColumnHeader1.Name = nameItem;
                                            dictObjects.Add(controlName + nameItem, ColumnHeader1);

                                            ColumnHeader1.DefaultValues = @"ВыравниваниеТекста == Лево
Текст == 
ТипСортировки == Текст
Ширина == 60
(Name) == ";
                                        }
                                        else if (strCurrent.Contains(".Подэлементы.Добавить(")) // добавляем подэлемент списка элементов
                                        {
                                            //Элемент0.Подэлементы.Добавить(Подэлемент1);
                                            string nameItem = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                            ListViewItem ListViewItem1 = (osfDesigner.ListViewItem)dictObjects[nameItem];
                                            string nameSubItem = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                            ListViewSubItem ListViewSubItem1 = (osfDesigner.ListViewSubItem)dictObjects[nameSubItem];
                                            ListViewItem1.SubItems.Add(ListViewSubItem1);
                                        }
                                        else // обрабатываем как свойство для элемента или подэлемента СписокЭлементов
                                        {
                                            //Элемент0.Текст = "Элемент0";
                                            //Подэлемент2.Текст = "Подэлемент2";
                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                            string nameObj = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                            object control2 = dictObjects[nameObj];
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, null);
                                        }
                                    }
                                }

                                else if (componentName.Contains("ПолеСписка"))
                                {
                                    if (strCurrent.Contains(".Элементы.Добавить(Ф.ЭлементСписка(")) // добавляем элемент поля списка
                                    {
                                        //ПолеСписка1.Элементы.Добавить(Ф.ЭлементСписка("ййй", "ййй"));
                                        //ПолеСписка1.Элементы.Добавить(Ф.ЭлементСписка("3", 3));
                                        //ПолеСписка1.Элементы.Добавить(Ф.ЭлементСписка("Истина", Истина));
                                        //ПолеСписка1.Элементы.Добавить(Ф.ЭлементСписка("01.01.0001 0:00:00", Дата(0001, 01, 01, 00, 00, 00)));
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "(Ф.ЭлементСписка", ");");
                                        string itemText = OneScriptFormsDesigner.ParseBetween(strPropertyValue, "(", ",").Replace("\u0022", "");
                                        string itemValue = OneScriptFormsDesigner.ParseBetween(strPropertyValue, ",", ")");

                                        osfDesigner.ListItemListBox ListItemListBox1 = new ListItemListBox();
                                        ListItemListBox1.Text = itemText;

                                        if (itemValue.StartsWith("\u0022") && itemValue.EndsWith("\u0022")) // это тип Строка
                                        {
                                            itemValue = itemValue.Replace("\u0022", "");
                                            ListItemListBox1.Value = itemValue;
                                            ListItemListBox1.ValueType = DataType.Строка;
                                        }
                                        else if (strPropertyValue.Contains("Ложь") || strPropertyValue.Contains("Истина")) // это тип Булево
                                        {
                                            ListItemListBox1.Value = true;
                                            ListItemListBox1.ValueType = DataType.Булево;
                                            if (itemValue == "Ложь")
                                            {
                                                ListItemListBox1.Value = false;
                                            }
                                        }
                                        else if (strPropertyValue.Contains("Дата")) // это тип Дата
                                        {
                                            DateTime rez1 = new DateTime();
                                            //("19.01.2022 0:00:00", Дата(2022, 01, 19, 00, 00, 00))
                                            string[] result1 = OneScriptFormsDesigner.ParseBetween(strPropertyValue, "Дата(", "))").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                            for (int i2 = 0; i2 < result1.Length; i2++)
                                            {
                                                if (i2 == 0)
                                                {
                                                    rez1 = rez1.AddYears(Int32.Parse(result1[0]) - 1);
                                                }
                                                if (i2 == 1)
                                                {
                                                    rez1 = rez1.AddMonths(Int32.Parse(result1[1]) - 1);
                                                }
                                                if (i2 == 2)
                                                {
                                                    rez1 = rez1.AddDays(Int32.Parse(result1[2]) - 1);
                                                }
                                                if (i2 == 3)
                                                {
                                                    rez1 = rez1.AddHours(Int32.Parse(result1[3]));
                                                }
                                                if (i2 == 4)
                                                {
                                                    rez1 = rez1.AddMinutes(Int32.Parse(result1[4]));
                                                }
                                                if (i2 == 5)
                                                {
                                                    rez1 = rez1.AddSeconds(Int32.Parse(result1[5]));
                                                }
                                            }
                                            ListItemListBox1.Value = rez1;
                                            ListItemListBox1.ValueType = DataType.Дата;
                                        }
                                        else // это тип Число
                                        {
                                            ListItemListBox1.Value = Int32.Parse(itemValue);
                                            ListItemListBox1.ValueType = DataType.Число;
                                        }
                                        ((ListBox)control).Items.Add(ListItemListBox1);
                                    }
                                    else // обрабатываем как свойство поля списка
                                    {
                                        //ПолеВыбора1.Родитель = Форма_0;
                                        //ПолеВыбора1.ВысотаЭлемента = 16;
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                        Control parent = (Control)dictObjects[parentName];
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                    }
                                }

                                else if (componentName.Contains("ПолеВыбора"))
                                {
                                    if (strCurrent.Contains(".Элементы.Добавить(Ф.ЭлементСписка(")) // добавляем элемент поля выбора
                                    {
                                        //ПолеВыбора1.Элементы.Добавить(Ф.ЭлементСписка("фыфы", "фыфы"));
                                        //ПолеВыбора1.Элементы.Добавить(Ф.ЭлементСписка("0", 0));
                                        //ПолеВыбора1.Элементы.Добавить(Ф.ЭлементСписка("Ложь", Ложь));
                                        //ПолеВыбора1.Элементы.Добавить(Ф.ЭлементСписка("19.01.2022 0:00:00", Дата(2022, 01, 19, 00, 00, 00)));
                                        //Определяем тип элемента списка и создаем его
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "(Ф.ЭлементСписка", ");");
                                        string itemText = OneScriptFormsDesigner.ParseBetween(strPropertyValue, "(", ",").Replace("\u0022", "");
                                        string itemValue = OneScriptFormsDesigner.ParseBetween(strPropertyValue, ",", ")");

                                        osfDesigner.ListItemComboBox ListItemComboBox1 = new ListItemComboBox();
                                        ListItemComboBox1.Text = itemText;

                                        if (itemValue.StartsWith("\u0022") && itemValue.EndsWith("\u0022")) // это тип Строка
                                        {
                                            itemValue = itemValue.Replace("\u0022", "");
                                            ListItemComboBox1.Value = itemValue;
                                            ListItemComboBox1.ValueType = DataType.Строка;
                                        }
                                        else if (strPropertyValue.Contains("Ложь") || strPropertyValue.Contains("Истина")) // это тип Булево
                                        {
                                            ListItemComboBox1.Value = true;
                                            ListItemComboBox1.ValueType = DataType.Булево;
                                            if (itemValue == "Ложь")
                                            {
                                                ListItemComboBox1.Value = false;
                                            }
                                        }
                                        else if (strPropertyValue.Contains("Дата")) // это тип Дата
                                        {
                                            DateTime rez1 = new DateTime();
                                            //("19.01.2022 0:00:00", Дата(2022, 01, 19, 00, 00, 00))
                                            string[] result1 = OneScriptFormsDesigner.ParseBetween(strPropertyValue, "Дата(", "))").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                            for (int i2 = 0; i2 < result1.Length; i2++)
                                            {
                                                if (i2 == 0)
                                                {
                                                    rez1 = rez1.AddYears(Int32.Parse(result1[0]) - 1);
                                                }
                                                if (i2 == 1)
                                                {
                                                    rez1 = rez1.AddMonths(Int32.Parse(result1[1]) - 1);
                                                }
                                                if (i2 == 2)
                                                {
                                                    rez1 = rez1.AddDays(Int32.Parse(result1[2]) - 1);
                                                }
                                                if (i2 == 3)
                                                {
                                                    rez1 = rez1.AddHours(Int32.Parse(result1[3]));
                                                }
                                                if (i2 == 4)
                                                {
                                                    rez1 = rez1.AddMinutes(Int32.Parse(result1[4]));
                                                }
                                                if (i2 == 5)
                                                {
                                                    rez1 = rez1.AddSeconds(Int32.Parse(result1[5]));
                                                }
                                            }
                                            ListItemComboBox1.Value = rez1;
                                            ListItemComboBox1.ValueType = DataType.Дата;
                                        }
                                        else // это тип Число
                                        {
                                            ListItemComboBox1.Value = Int32.Parse(itemValue);
                                            ListItemComboBox1.ValueType = DataType.Число;
                                        }
                                        ((ComboBox)control).Items.Add(ListItemComboBox1);
                                    }
                                    else // обрабатываем как свойство поля выбора
                                    {
                                        //ПолеВыбора1.Родитель = Форма_0;
                                        //ПолеВыбора1.ВысотаЭлемента = 16;
                                        string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                        string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                        string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                        Control parent = (Control)dictObjects[parentName];
                                        PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                    }
                                }

                                else if (componentName.Contains("СеткаДанных"))
                                {
                                    //СеткаДанных1.Родитель = Форма_0;
                                    //СеткаДанных1.Положение = Ф.Точка(45, 49);
                                    //СеткаДанных1.ПорядокОбхода = 0;
                                    //СеткаДанных1.Размер = Ф.Размер(255, 168);
                                    string controlName = ((osfDesigner.DataGrid)control).Name;
                                    string header = OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                    if (header.Contains("СеткаДанных"))// обрабатываем как свойство сетки данных
                                    {
                                        if (!strCurrent.Contains(".СтилиТаблицы.Добавить("))
                                        {
                                            //СеткаДанных1.Родитель = Форма_0;
                                            //СеткаДанных1.Положение = Ф.Точка(10, 200);
                                            //СеткаДанных1.ПорядокОбхода = 0;
                                            //СеткаДанных1.Размер = Ф.Размер(200, 137);
                                            //Стиль0 = Ф.СтильТаблицыСеткиДанных();
                                            //Стиль0.ИмяОтображаемого = "";
                                            //СеткаДанных1.СтилиТаблицы.Добавить(Стиль0);

                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, componentName + ".", "=");
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            string parentName = OneScriptFormsDesigner.ParseBetween(ComponentBlok, componentName + @".Родитель=", @";");
                                            Control parent = (Control)dictObjects[parentName];
                                            PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                        }
                                    }
                                    else
                                    {
                                        if (strCurrent.Contains("Ф.СтильТаблицыСеткиДанных();"))
                                        {
                                            //Стиль0 = Ф.СтильТаблицыСеткиДанных();
                                            //СеткаДанных1.СтилиТаблицы.Добавить(Стиль0);
                                            osfDesigner.DataGridTableStyle SimilarObj = new osfDesigner.DataGridTableStyle();
                                            System.Windows.Forms.DataGridTableStyle OriginalObj = new System.Windows.Forms.DataGridTableStyle();
                                            SimilarObj.OriginalObj = OriginalObj;
                                            OneScriptFormsDesigner.AddToHashtable(OriginalObj, SimilarObj);
                                            OneScriptFormsDesigner.PassProperties(OriginalObj, SimilarObj);//передадим свойства
                                            string nameStyle = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            SimilarObj.NameStyle = nameStyle;
                                            ((osfDesigner.DataGrid)control).TableStyles.Add(OriginalObj);
                                            dictObjects.Add(controlName + nameStyle, SimilarObj);

                                            SimilarObj.DefaultValues = @"ШрифтЗаголовков == Microsoft Sans Serif; 7,8pt
ПредпочтительнаяВысотаСтрок == 18
ПредпочтительнаяШиринаСтолбцов == 75
ШиринаЗаголовковСтрок == 35
РазрешитьСортировку == Истина
ОтображатьЗаголовкиСтолбцов == Истина
ОтображатьЗаголовкиСтрок == Истина
ИмяОтображаемого == 
СтилиКолонкиСеткиДанных == (Коллекция)
ТолькоЧтение == Ложь
ОсновнойЦвет == ТекстОкна
ОсновнойЦветЗаголовков == ТекстЭлемента
ЦветСетки == ЛицеваяЭлемента
ЦветФона == Окно
ЦветФонаЗаголовков == ЛицеваяЭлемента
ЦветФонаНечетныхСтрок == Окно";
                                        }
                                        else if (strCurrent.Contains("Ф.СтильКолонкиБулево();"))
                                        {
                                            //СтильКолонкиБулево0 = Ф.СтильКолонкиБулево();
                                            string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            DataGridBoolColumn DataGridBoolColumn1 = new DataGridBoolColumn();
                                            dictObjects.Add(controlName + nameObj, DataGridBoolColumn1);
                                            DataGridBoolColumn1.NameStyle = nameObj;

                                            DataGridBoolColumn1.DefaultValues = @"Ширина == 75
Выравнивание == Лево
ТекстЗаголовка == 
ИмяОтображаемого == 
ТолькоЧтение == Ложь";
                                        }
                                        else if(strCurrent.Contains("Ф.СтильКолонкиПолеВвода();"))
                                        {
                                            //СтильКолонкиПолеВвода0 = Ф.СтильКолонкиПолеВвода();
                                            string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            DataGridTextBoxColumn DataGridTextBoxColumn1 = new DataGridTextBoxColumn();
                                            dictObjects.Add(controlName + nameObj, DataGridTextBoxColumn1);
                                            DataGridTextBoxColumn1.NameStyle = nameObj;

                                            DataGridTextBoxColumn1.DefaultValues = @"Ширина == 75
Выравнивание == Лево
ТекстЗаголовка == 
ДвойноеНажатие == 
ИмяОтображаемого == 
ТолькоЧтение == Ложь";
                                        }
                                        else if(strCurrent.Contains("Ф.СтильКолонкиПолеВыбора();"))
                                        {
                                            //СтильКолонкиПолеВыбора2 = Ф.СтильКолонкиПолеВыбора();
                                            string nameObj = OneScriptFormsDesigner.ParseBetween(strCurrent, null, "=");
                                            DataGridComboBoxColumnStyle DataGridComboBoxColumnStyle1 = new DataGridComboBoxColumnStyle();
                                            dictObjects.Add(controlName + nameObj, DataGridComboBoxColumnStyle1);
                                            DataGridComboBoxColumnStyle1.NameStyle = nameObj;

                                            DataGridComboBoxColumnStyle1.DefaultValues = @"Ширина == 75
Выравнивание == Лево
ТекстЗаголовка == 
ИмяОтображаемого == 
ТолькоЧтение == Ложь";
                                        }
                                        else if (strCurrent.Contains(".СтилиКолонкиСеткиДанных.Добавить("))
                                        {
                                            //Стиль0.СтилиКолонкиСеткиДанных.Добавить(СтильКолонкиПолеВвода0);
                                            string nameTableStyle = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                            osfDesigner.DataGridTableStyle tableStyle = (osfDesigner.DataGridTableStyle)dictObjects[nameTableStyle];
                                            string nameColumnStyle = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, "(", ")");
                                            Component columnStyle = (Component)dictObjects[nameColumnStyle];
                                            tableStyle.OriginalObj.GridColumnStyles.Add((DataGridColumnStyle)columnStyle);
                                        }
                                        else
                                        {
                                            //СтильКолонкиБулево0.ИмяОтображаемого = "хх";

                                            string displayName = OneScriptFormsDesigner.ParseBetween(strCurrent, ".", "=");
                                            string nameObj = controlName + OneScriptFormsDesigner.ParseBetween(strCurrent, null, ".");
                                            Component control2 = (Component)dictObjects[nameObj];
                                            string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                            PropValueConverter.SetPropValue(control2, displayName, strPropertyValue, null);
                                        }
                                    }
                                }

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
                                            Control parent = (Control)dictObjects[parentName];
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
                                    Control parent = (Control)dictObjects[parentName];

                                    //System.Windows.Forms.MessageBox.Show("1control=" + control.GetType() + Environment.NewLine +
                                    //    "displayName=" + displayName + Environment.NewLine +
                                    //    "strPropertyValue=" + strPropertyValue + Environment.NewLine +
                                    //    "parent.GetType=" + parent.GetType() + Environment.NewLine +
                                    //    "");

                                    if (parent.GetType() == typeof(osfDesigner.TabPage))
                                    {
                                        parent = OneScriptFormsDesigner.RevertOriginalObj(parent);
                                    }

                                    PropValueConverter.SetPropValue(control, displayName, strPropertyValue, parent);
                                }
                            }
                        }
                    }

                    if (control.GetType() == typeof(osfDesigner.ToolBar) || 
                        control.GetType() == typeof(osfDesigner.Splitter) || 
                        control.GetType() == typeof(osfDesigner.StatusBar))
                    {
                        ((Control)control).BringToFront();
                    }

                    result = null;
                    ComponentBlok = null;
                    propertyGrid1.Refresh();
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
	
                            if (displayName == "Меню")
                            {
                                //Форма_0.Меню = ГлавноеМеню2;
                                string strPropertyValue = OneScriptFormsDesigner.ParseBetween(strCurrent, "=", ";");
                                ((Form)rootComponent).Menu = (System.Windows.Forms.MainMenu)dictObjects[strPropertyValue];
                            }

                            if (strCurrent.StartsWith("Подсказка"))
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

            ComponentCollection ctrlsExisting = pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost().Container.Components;
            ISelectionService iSel = pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost().GetService(typeof(ISelectionService)) as ISelectionService;
            if (iSel == null)
            {
                return;
            }
            iSel.SetSelectedComponents(new IComponent[] { ctrlsExisting[0] });

            pDesigner.DSME.PropertyGridHost.ReloadTreeView();
            pDesigner.DSME.PropertyGridHost.ChangeSelectNode((Component)ctrlsExisting[0]);
        }

        private void _form_Click(object sender, EventArgs e)
        {
            osfDesigner.pDesigner.SplitterpDesigner.Visible = true;
            osfDesigner.pDesigner.CodePanel.Visible = false;
            this._addForm.Enabled = true;//"Добавить Форму"
            this._deleteForm.Enabled = true;//"Удалить Форму"
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
            this._addForm.Enabled = false;//"Добавить Форму"
            this._deleteForm.Enabled = false;//"Удалить Форму"
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

        private void _deleteForm_Click(object sender, EventArgs e)
        {
            if (pDesigner.TabControl.TabPages.Count <= 1)
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
                IpDesignerCore.RemoveDesignSurface(IpDesignerCore.ActiveDesignSurface);
            }
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
            System.IO.File.WriteAllText(saveFileDialog1.FileName, SaveScript.GetScriptText(saveFileDialog1.FileName), Encoding.UTF8);

            //System.IO.File.WriteAllText("C:\\444\\Проба.os", SaveScript.GetScriptText("C:\\444\\"), Encoding.UTF8);
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

        private void _useSnapLines_Click(object sender, EventArgs e)
        {
            IpDesignerCore.AddDesignSurface<Form>(640, 480, AlignmentModeEnum.SnapLines, new Size(1, 1));
        }

        private void _useGrid_Click(object sender, EventArgs e)
        {
            IpDesignerCore.AddDesignSurface<Form>(640, 480, AlignmentModeEnum.Grid, new Size(16, 16));
        }

        private void _useGridWithoutSnapping_Click(object sender, EventArgs e)
        {
            IpDesignerCore.AddDesignSurface<Form>(640, 480, AlignmentModeEnum.GridWithoutSnapping, new Size(16, 16));
        }

        private void _useNoGuides_Click(object sender, EventArgs e)
        {
            IpDesignerCore.AddDesignSurface<Form>(640, 480, AlignmentModeEnum.NoGuides, new Size(1, 1));
        }

        //* 17.12.2021 perfolenta

        private bool ГотовоКЗакрытию()
        {
            if (pDesignerCore.Dirty)
            {
                string str1 = "Одна из редактируемых форм изменена! Изменения будут потеряны!\n\nВыйти из конструктора форм?";
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
        public void GetDefaultValues(dynamic comp)
        {
            // Заполним для компонента начальные свойства. Они нужны будут при создании скрипта.
            string DefaultValues1 = "";
            object pg = pDesigner.DSME.PropertyGridHost.PropertyGrid;

            //System.Windows.Forms.MessageBox.Show("222pg=" + pg);

            ((System.Windows.Forms.PropertyGrid)pg).SelectedObject = comp;

            //System.Windows.Forms.MessageBox.Show("222((System.Windows.Forms.PropertyGrid)pg).SelectedObject=" + ((System.Windows.Forms.PropertyGrid)pg).SelectedObject.GetType());

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
            comp.DefaultValues = DefaultValues1;
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

        public static void SelectedNodeSearch(System.Windows.Forms.TreeView treeView, string nameNodeParent, ref System.Windows.Forms.TreeNode node, System.Windows.Forms.TreeNodeCollection treeNodes = null)
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
            System.Windows.Forms.TreeNode treeNode = null;
            for (int i = 0; i < _treeNodes.Count; i++)
            {
                treeNode = (System.Windows.Forms.TreeNode)_treeNodes[i];
                if (treeNode.Name == nameNodeParent)
                {
                    node = treeNode;
                    break;
                }
                if (treeNode.Nodes.Count > 0)
                {
                    SelectedNodeSearch(treeView, nameNodeParent, ref node, treeNode.Nodes);
                }
            }
        }

        //////public static void NodeSearch(System.Windows.Forms.TreeView treeView, string nameNodeParent, ref osfDesigner.MyTreeNode node, System.Windows.Forms.TreeNodeCollection treeNodes = null)
        //////{
        //////    System.Windows.Forms.TreeNodeCollection _treeNodes;
        //////    if (treeNodes == null)
        //////    {
        //////        _treeNodes = treeView.Nodes;
        //////    }
        //////    else
        //////    {
        //////        _treeNodes = treeNodes;
        //////    }
        //////    osfDesigner.MyTreeNode treeNode = null;
        //////    for (int i = 0; i < _treeNodes.Count; i++)
        //////    {
        //////        treeNode = (osfDesigner.MyTreeNode)_treeNodes[i];
        //////        if (treeNode.Name == nameNodeParent)
        //////        {
        //////            node = treeNode;
        //////            break;
        //////        }
        //////        if (treeNode.Nodes.Count > 0)
        //////        {
        //////            NodeSearch(treeView, nameNodeParent, ref node, treeNode.Nodes);
        //////        }
        //////    }
        //////}

    }

    public class PropertyGridMessageFilter : IMessageFilter
    {
        public Control Control;// Элемент управления для мониторинга

        public MouseEventHandler MouseUp;

        public PropertyGridMessageFilter(Control c, MouseEventHandler meh)
        {
            this.Control = c;
            MouseUp = meh;
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (!this.Control.IsDisposed && m.HWnd == this.Control.Handle && MouseUp != null)
            {
                System.Windows.Forms.MouseButtons mb = System.Windows.Forms.MouseButtons.None;

                switch (m.Msg)
                {
                    case 0x0202:/*WM_LBUTTONUP, see winuser.h*/
                        mb = System.Windows.Forms.MouseButtons.Left;
                        break;
                    case 0x0205:/*WM_RBUTTONUP*/
                        mb = System.Windows.Forms.MouseButtons.Right;
                        break;
                }

                if (mb != System.Windows.Forms.MouseButtons.None)
                {
                    MouseEventArgs e = new MouseEventArgs(mb, 1, m.LParam.ToInt32() & 0xFFff, m.LParam.ToInt32() >> 16, 0);
                    MouseUp(Control, e);
                }
            }
            return false;
        }
    }
}
